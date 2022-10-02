function TreeBrowserControl::newControl(%parent, %name)
{
    if (!isObject(%parent))
    {
        return ;
    }
    %ctrl = new GuiArray2Ctrl()
    {
        profile = "GuiDefaultProfile";
        position = "0 0";
        extent = %parent.getExtent();
        childrenClassName = "GuiControl";
        childrenExtent = %parent.getExtent();
        spacing = 0;
        numRowsOrCols = 1;
        inRows = 1;
        sluggishness = 0.5;
    };
    %ctrl.bindClassName("TreeBrowserControl");
    %ctrl.bindClassName(%name);
    %ctrl.setName(%name);
    %parent.add(%ctrl);
    %ctrl.Parent = %parent;
    %ctrl.idCounter = 0;
    %ctrl.level = 0;
    %ctrl.numButtons = 0;
    %ctrl.buttonWidth = 20;
    %ctrl.buttonPadding = 1;
    %ctrl.title = "";
    %ctrl.menuProfile = "ETSMenuProfile";
    %ctrl.selectedProfile = "ETSSelectedMenuItemProfile";
    %ctrl.menuTextProfile = "ETSUnselectedMenuTextProfile";
    %ctrl.menuTextSelectedProfile = "ETSSelectedMenuTextProfile";
    %ctrl.adjustMenuCellHeight = 0;
    %ctrl.isExpanded = 0;
    %ctrl.expandDelta = "250 0";
    %ctrl.root = new SimGroup();
    %ctrl.nodeDictionary = safeNewScriptObject("StringMap", "", 0);
    if (isObject(RootGroup))
    {
        RootGroup.add(%ctrl.root);
    }
    %ctrl.Path = "";
    %ctrl.goToPath("");
    return %ctrl;
}
function TreeBrowserControl::onResized(%this)
{
    %curMenu = %this.getCurrentMenu();
    %hilitedIdx = -1;
    if (isObject(%curMenu))
    {
        %hilitedCell = %curMenu.getHilitedCell();
        if (isObject(%hilitedCell))
        {
            %hilitedIdx = %curMenu.getObjectIndex(%hilitedCell);
        }
    }
    %parentExtent = %this.getParent().getTrgExtent();
    if (%this.isExpanded)
    {
        %this.collapsedParentExtent = getWords(VectorSub(%parentExtent SPC 0, %this.expandDelta SPC 0), 0, 1);
    }
    %this.childrenExtent = %parentExtent;
    %this.setNumChildren(0);
    %this.goToCurrentPath();
    if (%hilitedIdx >= 0)
    {
        %curMenu = %this.getCurrentMenu();
        if (isObject(%curMenu))
        {
            if (%curMenu.getCount() > %hilitedIdx)
            {
                %curMenu.hiliteCell(%curMenu.getObject(%hilitedIdx));
            }
        }
    }
    return ;
}
function TreeBrowserControl::onCreatedChild(%this, %child, %x, %unused)
{
    %leftPadding = (%this.buttonWidth + %this.buttonPadding) * %x;
    %contentsExtentX = %this.isExpanded ? getWord(%this.collapsedParentExtent, 0) : getWord(%child.getExtent(), 0) - %leftPadding;
    %contentsExtentY = %this.isExpanded ? getWord(%this.collapsedParentExtent, 1) : getWord(%child.getExtent(), 1);
    %child.expandedPane = new GuiControl()
    {
        profile = "FocusableDefaultProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = %child.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        hiliteProxy = %this.getHiliteProxy();
        treeBrowser = %this;
    };
    %child.add(%child.expandedPane);
    %child.contentPane = new GuiControl()
    {
        profile = "FocusableDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%leftPadding - %this.buttonWidth) - %this.buttonPadding SPC 0;
        extent = (%contentsExtentX + %this.buttonWidth) + %this.buttonPadding SPC %contentsExtentY;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        hiliteProxy = %this.getHiliteProxy();
        treeBrowser = %this;
    };
    %child.contentPane.bindClassName("TreeBrowserContentPane");
    %child.add(%child.contentPane);
    %child.scroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %leftPadding SPC 0;
        extent = %contentsExtentX SPC %contentsExtentY;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
    };
    %menuCellSpacing = 2;
    %menuTrgCellHeight = 24;
    if (%this.adjustMenuCellHeight)
    {
        %numCanFit = mFloor(%contentsExtentY / (%menuTrgCellHeight + %menuCellSpacing));
        %menuTrgCellHeight = (%contentsExtentY / %numCanFit) - %menuCellSpacing;
        %d = %menuTrgCellHeight - mFloor(%menuTrgCellHeight);
        if (%d >= 0.5)
        {
            %menuTrgCellHeight = mCeil(%menuTrgCellHeight);
        }
        else
        {
            %menuTrgCellHeight = mFloor(%menuTrgCellHeight);
        }
    }
    %child.menu = new GuiArray2Ctrl()
    {
        profile = %this.menuProfile;
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = %contentsExtentX - 6 SPC %menuTrgCellHeight;
        spacing = %menuCellSpacing;
        numRowsOrCols = 1;
        inRows = 0;
        hiliteProxy = %this.getHiliteProxy();
        hilited = 0;
        unselectedProfile = "GuiDefaultProfile";
        selectedProfile = %this.selectedProfile;
        menuTextProfile = %this.menuTextProfile;
        menuTextSelectedProfile = %this.menuTextSelectedProfile;
        treeBrowser = %this;
    };
    %child.menu.bindClassName("MenuControl");
    %child.menu.bindClassName("TreeBrowserFrame");
    %child.menu.layer = 0;
    %child.scroll.add(%child.menu);
    %child.menu.scroll = %child.scroll;
    %child.add(%child.scroll);
    return ;
}
function TreeBrowserControl::getHiliteProxy(%this)
{
    return "";
}
function TreeBrowserControl::scrollToLevel(%this, %level)
{
    %this.level = %level;
    %this.setTrgPosition(-%level * getWord(%this.childrenExtent, 0), 0);
    return ;
}
function TreeBrowserControl::goToCurrentPath(%this, %focus)
{
    if (!isDefined("%focus"))
    {
        %focus = 1;
    }
    %this.goToPath(%this.Path, %focus);
    return ;
}
function TreeBrowserControl::goToParentPath(%this)
{
    %currentNodeName = %this.getNode(%this.Path).name;
    %parentPath = getFields(%this.Path, 0, getFieldCount(%this.Path) - 2);
    %this.goToPath(%parentPath);
    %menu = %this.getCurrentMenu();
    %count = %menu.getCount();
    %i = 0;
    while (%i < %count)
    {
        %menuItem = %menu.getObject(%i);
        if (%menuItem.name $= %currentNodeName)
        {
            %menu.hiliteCell(0, %i);
            break;
        }
        %i = %i + 1;
    }
}

function TreeBrowserControl::getMenuText(%this, %text)
{
    return %text;
}
function TreeBrowserControl::goToPath(%this, %path, %focus)
{
    if (!isDefined("%focus"))
    {
        %focus = 1;
    }
    %path = trim(%path);
    %node = %this.getNode(%path);
    if (!isObject(%node))
    {
        return 0;
    }
    %pathchanged = !(%this.Path $= %path);
    %this.Path = %path;
    %oldLevel = %this.level;
    %level = %this.level = getFieldCount(%path);
    if (%this.getCount() <= %level)
    {
        %this.setNumChildren(%level + 1);
    }
    %this.scrollToLevel(%level);
    %leafNode = 0;
    %expanded = %this.isNodeExpanded(%this.Path);
    if (%this.isExpanded && (%level != %oldLevel))
    {
        %oldChild = %this.getChild(%oldLevel, 0);
        %oldChild.expandedPane.clear();
    }
    if (%expanded && !(%this.isExpanded))
    {
        %expandDelta = %this.getFieldValue("expandDelta");
        if (%expandDelta $= "")
        {
            warn(getScopeName() @ "->trying to expand view but no expandDelta is set. returning!");
        }
        else
        {
            %this.expandView(%expandDelta);
            return ;
        }
    }
    else
    {
        if (%this.isExpanded && !%expanded)
        {
            %this.collapseView();
            %this.focusCurrentFrame();
            return ;
        }
    }
    %child = %this.getChild(%level, 0);
    %count = %node.getCount();
    if (%count == 0)
    {
        %child.contentPane.setVisible(1);
        %child.scroll.setVisible(0);
        %child.menu.setVisible(0);
        %leafNode = 1;
        %child.contentPane.node = %node;
        %child.contentPane.clear();
        %this.fillLeafPane(%child.contentPane);
        if (%expanded && %this.isExpanded)
        {
            %child.expandedPane.clear();
            %child.expandedPane.setVisible(1);
            %this.fillExpandedContentPane(%child.expandedPane);
        }
        else
        {
            %child.expandedPane.setVisible(0);
        }
        if ((%focus && %this.isVisibleRecursive()) && %pathchanged)
        {
            %child.contentPane.makeFirstResponder(1);
        }
    }
    else
    {
        %child.contentPane.setVisible(0);
        %child.scroll.setVisible(1);
        %child.menu.setVisible(1);
        if (%expanded && %this.isExpanded)
        {
            %child.expandedPane.clear();
            %child.expandedPane.setVisible(1);
            %this.fillExpandedFrame(%child.expandedPane);
        }
        else
        {
            %child.expandedPane.setVisible(0);
        }
        %currentCount = %child.menu.getCount();
        %this.filterText = strlwr(%this.filterText);
        %this.filterText = trim(%this.filterText);
        %count = 0;
        %n = %node.getCount() - 1;
        while (%n >= 0)
        {
            %subNode = %node.getObject(%n);
            %subNode.passesFilter = %this.nodePassesFilter(%subNode, %this.filterText);
            if (%subNode.passesFilter)
            {
                %count = %count + 1;
            }
            %n = %n - 1;
        }
        if (%currentCount != %count)
        {
            %child.Path = "ForceUpdatePlease!!!";
        }
        if (!(%child.Path $= %path))
        {
            %child.menu.clear();
            %child.menu.deferReseat = 1;
            %totalCount = %node.getCount();
            %n = 0;
            while (%n < %totalCount)
            {
                %subNode = %node.getObject(%n);
                if (%subNode.passesFilter)
                {
                    %menuItem = %child.menu.addMenuItem(%this.getMenuText(%subNode.name), %this.getId() @ ".select(\"" @ %subNode.name @ "\");", "", "");
                    %menuItem.name = %subNode.name;
                }
                %n = %n + 1;
            }
            %child.menu.reseatChildren();
            %child.menu.hiliteCell(0, 0);
        }
        if ((%focus && %this.isVisibleRecursive()) && %pathchanged)
        {
            %child.menu.makeFirstResponder(1);
        }
    }
    %child.Path = %path;
    %numButtons = %leafNode ? 1 : %level;
    %offset = 0;
    %height = %this.isExpanded ? getWord(%this.collapsedParentExtent, 1) : getWord(%this.getExtent(), 1);
    %i = 0;
    while (%i < mMax(%numButtons, %this.numButtons))
    {
        if (%i < %numButtons)
        {
            if (!isObject(%this.button[%i]))
            {
                %this.button[%i] = new GuiBitmapButtonCtrl()
                {
                    profile = "ETSVerticalButtonProfile";
                    horizSizing = "right";
                    vertSizing = "bottom";
                    position = %offset SPC 0;
                    extent = %this.buttonWidth SPC %height;
                    minExtent = "1 1";
                    sluggishness = -1;
                    visible = 1;
                    command = "";
                    text = "";
                    groupNum = -1;
                    buttonType = "PushButton";
                    bitmap = "platform/client/buttons/vbutton";
                    drawText = 1;
                    textRotation = 90;
                };
                %this.Parent.add(%this.button[%i]);
            }
            else
            {
                if (!(%this.button[%i].getExtent() $= (%this.buttonWidth SPC %height)))
                {
                    %this.button[%i].resize(%this.buttonWidth, %height);
                }
            }
            %button = %this.button[%i];
            %button.setVisible(1);
            %button.command = %this.getId() @ ".goToPath(\"" @ getFields(%this.Path, 0, %i) @ "\");";
            %button.text = getField(%this.Path, %i);
            %button.setActive(%i < (%level - 1));
        }
        else
        {
            %this.button[%i].setVisible(0);
        }
        %offset = %offset + (%this.buttonWidth + %this.buttonPadding);
        %i = %i + 1;
    }
    %this.numButtons = %numButtons;
    return 1;
}
function TreeBrowserControl::nodePassesFilter(%this, %node, %filterText)
{
    if (%filterText $= "")
    {
        return 1;
    }
    %searchText = %this.getNodeSearchText(%node);
    %ret = strstr(%searchText, %filterText) >= 0;
    return %ret;
}
function TreeBrowserControl::getNodeSearchText(%this, %node)
{
    if (!(%node.searchText $= ""))
    {
        return %node.searchText;
    }
    %sku = %node.sku;
    if (!(%sku $= ""))
    {
        %ret = SkuManager.findBySku(%sku).searchText;
    }
    else
    {
        %ret = %node.name;
        %n = %node.getCount() - 1;
        while (%n >= 0)
        {
            %subNode = %node.getObject(%n);
            %subNodeSearchText = %this.getNodeSearchText(%subNode);
            %w = getWordCount(%subNodeSearchText) - 1;
            while (%w >= 0)
            {
                %word = getWord(%subNodeSearchText, %w);
                if (!hasWord(%ret, %word))
                {
                    %ret = %ret SPC %word;
                }
                %w = %w - 1;
            }
            %n = %n - 1;
        }
    }
    %ret = trim(%ret);
    %node.searchText = %ret;
    return %ret;
}
function TreeBrowserControl::expandView(%this, %delta)
{
    if (%this.isExpanded)
    {
        return ;
    }
    %this.isExpanded = 1;
    %collapsedParentExtent = %this.getParent().getTrgExtent();
    %this.resizeParentsBy(%delta);
    %this.onResized();
    %this.collapsedParentExtent = %collapsedParentExtent;
    %trg = %this.getTrgPosition();
    %this.reposition(getWord(%trg, 0), getWord(%trg, 1));
    return ;
}
function TreeBrowserControl::resizeParentsBy(%this, %delta)
{
    %extent = %this.getParent().getTrgExtent();
    %newExtent = VectorAdd(%extent SPC 0, %delta SPC 0);
    %newExtent = getWords(%newExtent, 0, 1);
    %this.getParent().resize(getWord(%newExtent, 0), getWord(%newExtent, 1));
    return ;
}
function TreeBrowserControl::collapseView(%this)
{
    if (!%this.isExpanded)
    {
        return ;
    }
    %this.isExpanded = 0;
    %delta = VectorSub(%this.collapsedParentExtent SPC 0, %this.getParent().getTrgExtent() SPC 0);
    %this.resizeParentsBy(getWords(%delta, 0, 1));
    %this.onResized();
    %trg = %this.getTrgPosition();
    %this.reposition(getWord(%trg, 0), getWord(%trg, 1));
    return ;
}
function TreeBrowserControl::isNodeExpanded(%this, %path)
{
    return 0;
}
function TreeBrowserControl::fillExpandedFrame(%this, %expandedFrame)
{
    %frame = %expandedFrame.getParent();
    %rightEdgeOfMenu = getWord(%frame.menu.getExtent(), 0) + getWord(%frame.menu.getPosition(), 0);
    return ;
}
function TreeBrowserControl::fillExpandedContentPane(%this, %expandedPane)
{
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = getWord(%frame.contentPane.getExtent(), 0) + getWord(%frame.contentPane.getPosition(), 0);
    return ;
}
function TreeBrowserControl::isInSubdirOfPath(%this, %path)
{
    %depth = getFieldCount(%path);
    return %path $= getFields(%this.Path, %depth);
}
function TreeBrowserControl::fillLeafPane(%this, %pane)
{
    %level = %this.level;
    return ;
}
function TreeBrowserControl::select(%this, %value)
{
    %this.goToPath(%this.Path TAB %value);
    return ;
}
function TreeBrowserControl::selectNextLeaf(%this, %forward, %slide)
{
    %path = %this.getNextLeaf(%this.Path, %forward);
    if (!(%path $= ""))
    {
        if (isDefined("%slide"))
        {
            %level = getFieldCount(%path);
            if (!%slide && (getFieldCount(%this.Path) != %level))
            {
                %this.level = %level;
                %this.reposition(-%level * getWord(%this.childrenExtent, 0), 0);
            }
        }
        %this.goToPath(%path);
    }
    return ;
}
function TreeBrowserControl::getNextLeaf(%this, %path, %forward)
{
    %node = %this.getNode(%path);
    if (!isObject(%node))
    {
        return "";
    }
    if (%node.getCount() > 0)
    {
        %foundChildBearingNode = 1;
    }
    else
    {
        %foundChildBearingNode = 0;
    }
    while (!%foundChildBearingNode)
    {
        %depth = getFieldCount(%path);
        %name = getField(%path, %depth - 1);
        if (%depth <= 1)
        {
            return "";
        }
        %ppath = getFields(%path, 0, %depth - 2);
        %pnode = %this.getNode(%ppath);
        %childCount = %pnode.getCount();
        %nidx = -1;
        %i = 0;
        while (%i < %childCount)
        {
            %child = %pnode.getObject(%i);
            if (%child.name $= %name)
            {
                %nidx = %i;
            }
            %i = %i + 1;
        }
        if (%nidx < 0)
        {
            error(getScopeName() @ "->nidx < 0.");
            return "";
        }
        %tidx = %nidx + %forward;
        if ((%tidx >= 0) && (%tidx < %childCount))
        {
            %node = %pnode.getObject(%tidx);
            %path = %ppath TAB %node.name;
            if (%node.getCount() > 0)
            {
                %foundChildBearingNode = 1;
            }
            else
            {
                return %path;
            }
        }
        else
        {
            %node = %pnode;
            %path = %ppath;
        }
    }
    while (%cnt = %node.getCount() > 0)
    {
        %slot = %forward > 0 ? 0 : 1;
        %node = %node.getObject(%slot);
        %path = %path TAB %node.name;
    }
    return %path;
}
function TreeBrowserControl::addNode(%this, %path)
{
    %this.addNodeAt("", %path);
    return ;
}
function TreeBrowserControl::addNodeAt(%this, %prefix, %subpath)
{
    %prefix = trim(%prefix);
    %subpath = trim(%subpath);
    %baseNode = %this.getNode(%prefix);
    if (!isObject(%baseNode))
    {
        return 0;
    }
    %childNodeName = getField(%subpath, 0);
    if (%childNodeName $= "")
    {
        return %baseNode;
    }
    else
    {
        %fullPath = %prefix TAB %childNodeName;
        %childNode = %this.nodeDictionary.get(%fullPath);
        if (!isObject(%childNode))
        {
            %newSet = new SimGroup();
            %baseNode.add(%newSet);
            %this.nodeDictionary.put(%fullPath, %newSet);
        }
        return %this.addNodeAt(%fullPath, getFields(%subpath, 1));
    }
    return ;
}
function TreeBrowserControl::getNodePath(%this, %node)
{
    %path = "";
    %delim = "";
    while (!(%node.name $= ""))
    {
        %path = %node.name @ %delim @ %path;
        %delim = "\t";
        if (%node.getId() $= %this.root.getId())
        {
            %node = "";
        }
        else
        {
            %node = %node.getGroup();
        }
    }
    return %path;
}
function TreeBrowserControl::deleteNodeAtPath(%this, %path)
{
    %node = %this.getNode(%path);
    if (!isObject(%node))
    {
        return ;
    }
    %this.deleteNode(%node);
    if (%this.isInSubdirOfPath(%path))
    {
        %depth = getFieldCount(%path);
        %this.goToPath(getFields(%path, 0, %depth - 2));
    }
    return ;
}
function TreeBrowserControl::deleteNode(%this, %node)
{
    if (!isObject(%node))
    {
        return ;
    }
    %i = %node.getCount() - 1;
    while (%i >= 0)
    {
        %this.deleteNode(%node.getObject(%i));
        %i = %i - 1;
    }
    %node.delete();
    return ;
}
function TreeBrowserControl::addMenuData(%this, %prefix, %list)
{
    %node = %this.getNode(%prefix);
    if (!isObject(%node))
    {
        return ;
    }
    %listCount = getFieldCount(%list);
    %i = 0;
    while (%i < %listCount)
    {
        %itemName = getField(%list, %i);
        if (!(%itemName $= ""))
        {
        }
        %i = %i + 1;
    }
    %this.goToCurrentPath();
    return ;
}
function TreeBrowserControl::setDataTree(%this, %tree)
{
    if (isObject(%tree) && !((%tree.text $= "")))
    {
        %this.title = %tree.text;
        %this.addMenuData("", %this.title);
        %this.addDataTree(%tree, %this.title);
        %this.goToPath(%this.title);
    }
    return ;
}
function TreeBrowserControl::addDataTree(%this, %tree, %prefix)
{
    %count = %tree.getCount();
    %items = "";
    %i = 0;
    while (%i < %count)
    {
        %obj = %tree.getObject(%i);
        %items = %items TAB %obj.text;
        %i = %i + 1;
    }
    %this.addMenuData(%prefix, %items);
    %i = 0;
    while (%i < %count)
    {
        %obj = %tree.getObject(%i);
        %this.addDataTree(%obj, %prefix TAB %obj.text);
        %i = %i + 1;
    }
}

function TreeBrowserControl::getNode(%this, %path)
{
    %node = %this.nodeDictionary.get(%path);
    if (isObject(%node))
    {
        return %node;
    }
    %pathCount = getFieldCount(%path);
    %node = %this.root;
    %i = 0;
    while (%i < %pathCount)
    {
        %dirName = getField(%path, %i);
        if (%dirName $= "")
        {
            continue;
        }
        %nodeCount = %node.getCount();
        %match = 0;
        %j = 0;
        while (%j < %nodeCount)
        {
            %subNode = %node.getObject(%j);
            if (%subNode.name $= %dirName)
            {
                %node = %subNode;
                %match = 1;
                break;
            }
            %j = %j + 1;
        }
        if (!%match)
        {
            return 0;
        }
        %i = %i + 1;
    }
    %this.nodeDictionary.put(%path, %node);
    return %node;
}
function TreeBrowserControl::clear(%this)
{
    %this.root.deleteMembers();
    return ;
}
function TreeBrowserControl::getCurrentNode(%this)
{
    return %this.getNode(%this.Path);
}
function TreeBrowserControl::getCurrentFrame(%this)
{
    return %this.getObject(getFieldCount(%this.Path));
}
function TreeBrowserControl::getCurrentMenu(%this)
{
    %frame = %this.getCurrentFrame();
    return %frame.menu;
}
function TreeBrowserControl::getCurrentContentPane(%this)
{
    %frame = %this.getCurrentFrame();
    return %frame.contentPane;
}
function TreeBrowserControl::focusCurrentFrame(%this)
{
    if (!%this.isVisible())
    {
        return ;
    }
    %frame = %this.getCurrentFrame();
    %contentPane = %this.getCurrentContentPane();
    %menu = %this.getCurrentMenu();
    if (%menu.isVisibleRecursive())
    {
        %menu.makeFirstResponder(1);
    }
    else
    {
        if (%contentPane.isVisibleRecursive())
        {
            %contentPane.makeFirstResponder(1);
        }
    }
    return ;
}
function TreeBrowserFrame::onCreatedChild(%this, %child)
{
    Parent::onCreatedChild(%this, %child);
    %child.menuText.reposition(5, 2);
    if (!(getWord(%child.getNamespaceList(), 0) $= "TreeBrowserItem"))
    {
        %child.bindClassName("TreeBrowserItem");
    }
    return ;
}
function TreeBrowserFrame::onKeyDown(%this, %unused, %keyCode)
{
    if (%this.getStringFromKeyCode(%keyCode) $= "left")
    {
        %this.treeBrowser.goToParentPath();
        return 1;
    }
    else
    {
        if (%this.getStringFromKeyCode(%keyCode) $= "right")
        {
            %this.getHilitedCell().onSelect();
            return 1;
        }
    }
    return 0;
}
function TreeBrowserContentPane::onKeyDown(%this, %unused, %keyCode)
{
    if (%this.getStringFromKeyCode(%keyCode) $= "left")
    {
        %this.treeBrowser.goToParentPath();
        return 1;
    }
    else
    {
        if (%this.getStringFromKeyCode(%keyCode) $= "up")
        {
            %this.treeBrowser.selectNextLeaf(-1, 0);
            return 1;
        }
        else
        {
            if (%this.getStringFromKeyCode(%keyCode) $= "down")
            {
                %this.treeBrowser.selectNextLeaf(1, 0);
                return 1;
            }
        }
    }
    return 0;
}
function TreeBrowserControl::makeSomeTreeData()
{
    %root = new SimGroup();
    if (isObject(RootGroup))
    {
        RootGroup.add(%root);
    }
    return %root;
}
function TreeBrowserControl::test()
{
    new GuiControl(BrowserParent)
    {
        position = "50 50";
        extent = "250 100";
    };
    %rootCtrl = Canvas.getContent();
    %rootCtrl.add(BrowserParent);
    TreeBrowserControl::newControl(BrowserParent, "TheBrowser");
    TheBrowser.setNumChildren(1);
    %data = TreeBrowserControl::makeSomeTreeData();
    TheBrowser.setDataTree(%data);
    return ;
}
function dumpTree(%tree)
{
    dumpSubtree(%tree, "");
    return ;
}
function dumpSubtree(%subtree, %prefix)
{
    echo(%prefix @ %subtree.text);
    %count = %subtree.getCount();
    %i = 0;
    while (%i < %count)
    {
        %obj = %subtree.getObject(%i);
        dumpSubtree(%obj, %prefix @ "   ");
        %i = %i + 1;
    }
}

function deleteTree(%tree)
{
    %count = %tree.getCount();
    %i = 0;
    while (%i < %count)
    {
        %obj = %tree.getObject(0);
        deleteTree(%obj);
        %i = %i + 1;
    }
    %tree.delete();
    return ;
}
function textToTree(%text)
{
    %tree = 0;
    %text = strreplace(%text, "\'", "\"");
    %text = strreplace(%text, "[\"", "new SimGroup() { text = |");
    %text = strreplace(%text, "\"", "|; ");
    %text = strreplace(%text, "|", "\"");");
    %text = "%tree = " @ %text;
    eval(%text);
    return %tree;
}
function treeToText(%tree)
{
    %text = "";
    if (isObject(%tree))
    {
        %text = "[\"" @ %tree.text @ "\"";
        %count = %tree.getCount();
        %i = 0;
        while (%i < %count)
        {
            %obj = %tree.getObject(%i);
            %text = %text @ treeToText(%obj);
            %i = %i + 1;
        }
        %text = %text @ "]";
    }
    return %text;
}
