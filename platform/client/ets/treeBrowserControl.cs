function TreeBrowserControl::newControl(%parent, %name)
{
    if (!isObject(%parent))
    {
        return;
    }
    %ctrl = new GuiArray2Ctrl("") {
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
    %ctrl.root = new SimGroup("") {
        name = "";
    };
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
    %hilitedIdx = -(1);
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
        %this.collapsedParentExtent = getWords(VectorSub(%parentExtent @ " " @ 0, %this.expandDelta @ " " @ 0), 0, 1);
    }
    %this.childrenExtent = %parentExtent;
    %this.setNumChildren(0);
    %this.goToCurrentPath();
    if (%hilitedIdx >= 0)
    {
        %curMenu = %this.getCurrentMenu();
        if (isObject(%curMenu) && (%curMenu.getCount() > %hilitedIdx))
        {
            %curMenu.hiliteCell(%curMenu.getObject(%hilitedIdx));
        }
    }
}
function TreeBrowserControl::onCreatedChild(%this, %child, %x, %unused)
{
    %leftPadding = (%this.buttonWidth + %this.buttonPadding) * %x;
    if (%this.isExpanded)
    {
    }
    else
    {
    }
    %contentsExtentX = getWord(%child.getExtent(), 0) - getWord(%this.collapsedParentExtent, 0);
    %leftPadding;
    if (%this.isExpanded)
    {
    }
    else
    {
    }
    %contentsExtentY = getWord(%child.getExtent(), 1);
    getWord(%this.collapsedParentExtent, 1);
    %child.expandedPane = new GuiControl("") {
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
    %child.contentPane = new GuiControl("") {
        profile = "FocusableDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%leftPadding - %this.buttonWidth) - %this.buttonPadding) @ " " @ 0;
        extent = ((%contentsExtentX + %this.buttonWidth) + %this.buttonPadding) @ " " @ %contentsExtentY;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        hiliteProxy = %this.getHiliteProxy();
        treeBrowser = %this;
    };
    %child.contentPane.bindClassName("TreeBrowserContentPane");
    %child.add(%child.contentPane);
    %child.scroll = new GuiScrollCtrl("") {
        profile = "ETSScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %leftPadding @ " " @ 0;
        extent = %contentsExtentX @ " " @ %contentsExtentY;
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
        %numCanFit = mFloor((%contentsExtentY / (%menuTrgCellHeight + %menuCellSpacing)));
        %menuTrgCellHeight = (%contentsExtentY / %numCanFit) - %menuCellSpacing;
        %d = %menuTrgCellHeight - mFloor(%menuTrgCellHeight);
        if (%d >= 0.5)
        {
            %menuTrgCellHeight = mCeil(%menuTrgCellHeight);
        }
        %menuTrgCellHeight = mFloor(%menuTrgCellHeight);
    }
    %child.menu = new GuiArray2Ctrl("") {
        profile = %this.menuProfile;
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = (%contentsExtentX - 6) @ " " @ %menuTrgCellHeight;
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
}
function TreeBrowserControl::getHiliteProxy(%this)
{
    return "";
}
function TreeBrowserControl::scrollToLevel(%this, %level)
{
    %this.level = %level;
    %this.setTrgPosition((-(%level) * getWord(%this.childrenExtent, 0)), 0);
}
function TreeBrowserControl::goToCurrentPath(%this, %focus)
{
    if (!isDefined("%focus"))
    {
        %focus = 1;
    }
    %this.goToPath(%this.Path, %focus);
}
function TreeBrowserControl::goToParentPath(%this)
{
    %currentNodeName = %this.getNode(%this.Path).name;
    %parentPath = getFields(%this.Path, 0, (getFieldCount(%this.Path) - 2));
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
    %this.level = getFieldCount(%path);
    %level =;
    if (%this.getCount() <= %level)
    {
        %this.setNumChildren((%level + 1));
    }
    %this.scrollToLevel(%level);
    %leafNode = 0;
    %expanded = %this.isNodeExpanded(%this.Path);
    if (%this.isExpanded && (%level != %oldLevel))
    {
        %oldChild = %this.getChild(%oldLevel, 0);
        %oldChild.expandedPane.clear();
    }
    if (%expanded && !%this.isExpanded)
    {
        %expandDelta = %this.getFieldValue("expandDelta");
        if (%expandDelta $= "")
        {
            warn(getScopeName() @ "->trying to expand view but no expandDelta is set. returning!");
        }
        else
        {
            %this.expandView(%expandDelta);
            return;
        }
    }
    else
    {
        if (%this.isExpanded && !%expanded)
        {
            %this.collapseView();
            %this.focusCurrentFrame();
            return;
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
        if (%focus && %this.isVisibleRecursive() && %pathchanged)
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
            %child.Path = (%n >= 0) @ "ForceUpdatePlease!!!";
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
        if (%focus && %this.isVisibleRecursive() && %pathchanged)
        {
            %child.menu.makeFirstResponder(1);
        }
    }
    %child.Path = (%n < %totalCount) @ %path;
    if (%leafNode)
    {
    }
    else
    {
    }
    %numButtons = %level;
    %level - 1;
    %offset = 0;
    if (%this.isExpanded)
    {
    }
    else
    {
    }
    %height = getWord(%this.getExtent(), 1);
    getWord(%this.collapsedParentExtent, 1);
    %i = 0;
    while (%i < mMax(%numButtons, %this.numButtons))
    {
        if (%i < %numButtons)
        {
            if (!isObject(%this.button[%i]))
            {
                %this.button[" ",0;
                    extent = %this.buttonWidth," ",%height;
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
                };,%i] = new GuiBitmapButtonCtrl("") {
                    profile = "ETSVerticalButtonProfile";
                    horizSizing = "right";
                    vertSizing = "bottom";
                    position = %offset;
                %this.Parent.add(%this.button[%i]);
            }
            else
            {
                if (!(%this.button[%i].getExtent() $= %this.buttonWidth @ " " @ %height))
                {
                    %this.button[%i].resize(%this.buttonWidth, %height);
                }
            }
            %button = %this.button[%i];
            %button.setVisible(1);
            %button.command = %this.getId() @ ".goToPath(\"" @ getFields(%this.Path, 0, %i) @ "\");";
            %button.text = getField(%this.Path, %i);
            %button.setActive((%i < (%level - 1)));
        }
        else
        {
            %this.button[%i].setVisible(0);
        }
        %offset = %offset + (%this.buttonWidth + %this.buttonPadding);
        %i = %i + 1;
    }
    %this.numButtons = (%i < mMax(%numButtons, %this.numButtons)) @ %numButtons;
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
                    %ret = %ret @ " " @ %word;
                }
                %w = %w - 1;
            }
            %n = %n - 1;
            %w >= 0;
        }
    }
    %ret = trim(%ret);
    %n >= 0;
    %node.searchText = %ret;
    return %ret;
}
function TreeBrowserControl::expandView(%this, %delta)
{
    if (%this.isExpanded)
    {
        return;
    }
    %this.isExpanded = 1;
    %collapsedParentExtent = %this.getParent().getTrgExtent();
    %this.resizeParentsBy(%delta);
    %this.onResized();
    %this.collapsedParentExtent = %collapsedParentExtent;
    %trg = %this.getTrgPosition();
    %this.reposition(getWord(%trg, 0), getWord(%trg, 1));
}
function TreeBrowserControl::resizeParentsBy(%this, %delta)
{
    %extent = %this.getParent().getTrgExtent();
    %newExtent = VectorAdd(%extent @ " " @ 0, %delta @ " " @ 0);
    %newExtent = getWords(%newExtent, 0, 1);
    %this.getParent().resize(getWord(%newExtent, 0), getWord(%newExtent, 1));
}
function TreeBrowserControl::collapseView(%this)
{
    if (!%this.isExpanded)
    {
        return;
    }
    %this.isExpanded = 0;
    %delta = VectorSub(%this.collapsedParentExtent @ " " @ 0, %this.getParent().getTrgExtent() @ " " @ 0);
    %this.resizeParentsBy(getWords(%delta, 0, 1));
    %this.onResized();
    %trg = %this.getTrgPosition();
    %this.reposition(getWord(%trg, 0), getWord(%trg, 1));
}
function TreeBrowserControl::isNodeExpanded(%this, %path)
{
    return 0;
}
function TreeBrowserControl::fillExpandedFrame(%this, %expandedFrame)
{
    %frame = %expandedFrame.getParent();
    %rightEdgeOfMenu = getWord(%frame.menu.getExtent(), 0) + getWord(%frame.menu.getPosition(), 0);
    %expandedFrame.add(new GuiMLTextCtrl("") {
        position = %rightEdgeOfMenu @ " " @ 0;
        extent = "50 18";
        text = "<color:ffffff>override me!";
        visible = 1;
    };);
}
function TreeBrowserControl::fillExpandedContentPane(%this, %expandedPane)
{
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = getWord(%frame.contentPane.getExtent(), 0) + getWord(%frame.contentPane.getPosition(), 0);
    %expandedPane.add(new GuiMLTextCtrl("") {
        position = %rightEdgeOfContentPane @ " " @ 0;
        extent = "50 18";
        text = "<color:ffffff>override me!";
        visible = 1;
    };);
}
function TreeBrowserControl::isInSubdirOfPath(%this, %path)
{
    %depth = getFieldCount(%path);
    return %path $= getFields(%this.Path, %depth);
}
function TreeBrowserControl::fillLeafPane(%this, %pane)
{
    %level = %this.level;
    %pane.add(new GuiTextCtrl("") {
        profile = "GuiTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = getWord(%pane.getExtent(), 0) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = getField(%this.Path, (%level - 1));
        maxLength = 255;
    };);
}
function TreeBrowserControl::select(%this, %value)
{
    %this.goToPath(%this.Path @ "\t" @ %value);
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
                %this.reposition((-(%level) * getWord(%this.childrenExtent, 0)), 0);
            }
        }
        %this.goToPath(%path);
    }
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
        %name = getField(%path, (%depth - 1));
        if (%depth <= 1)
        {
            return "";
        }
        %ppath = getFields(%path, 0, (%depth - 2));
        %pnode = %this.getNode(%ppath);
        %childCount = %pnode.getCount();
        %nidx = -(1);
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
            %path = %ppath @ "\t" @ %node.name;
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
    while ((%cnt = %node.getCount()) > 0)
    {
        if (%forward > 0)
        {
        }
        else
        {
        }
        %slot = %cnt - 1;
        0;
        %node = %node.getObject(%slot);
        !%foundChildBearingNode;
        %path = %path @ "\t" @ %node.name;
    }
    return %path;
}
function TreeBrowserControl::addNode(%this, %path)
{
    %this.addNodeAt("", %path);
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
        %fullPath = %prefix @ "\t" @ %childNodeName;
        %childNode = %this.nodeDictionary.get(%fullPath);
        if (!isObject(%childNode))
        {
            %newSet = new SimGroup("") {
                name = %childNodeName;
            };
            %baseNode.add(%newSet);
            %this.nodeDictionary.put(%fullPath, %newSet);
        }
        return %this.addNodeAt(%fullPath, getFields(%subpath, 1));
    }
}
function TreeBrowserControl::getNodePath(%this, %node)
{
    %path = "";
    %delim = "";
    while (isObject(%node) && !(%node.name $= ""))
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
        return;
    }
    %this.deleteNode(%node);
    if (%this.isInSubdirOfPath(%path))
    {
        %depth = getFieldCount(%path);
        %this.goToPath(getFields(%path, 0, (%depth - 2)));
    }
}
function TreeBrowserControl::deleteNode(%this, %node)
{
    if (!isObject(%node))
    {
        return;
    }
    %i = %node.getCount() - 1;
    while (%i >= 0)
    {
        %this.deleteNode(%node.getObject(%i));
        %i = %i - 1;
    }
    %node.delete();
}
function TreeBrowserControl::addMenuData(%this, %prefix, %list)
{
    %node = %this.getNode(%prefix);
    if (!isObject(%node))
    {
        return;
    }
    %listCount = getFieldCount(%list);
    %i = 0;
    while (%i < %listCount)
    {
        %itemName = getField(%list, %i);
        if (!(%itemName $= ""))
        {
            %node.add(new SimGroup("") {
                name = %itemName;
            };);
        }
        %i = %i + 1;
    }
    %this.goToCurrentPath();
}
function TreeBrowserControl::setDataTree(%this, %tree)
{
    if (isObject(%tree) && !(%tree.text $= ""))
    {
        %this.title = %tree.text;
        %this.addMenuData("", %this.title);
        %this.addDataTree(%tree, %this.title);
        %this.goToPath(%this.title);
    }
}
function TreeBrowserControl::addDataTree(%this, %tree, %prefix)
{
    %count = %tree.getCount();
    %items = "";
    %i = 0;
    while (%i < %count)
    {
        %obj = %tree.getObject(%i);
        %items = %items @ "\t" @ %obj.text;
        %i = %i + 1;
    }
    %this.addMenuData(%prefix, %items);
    %i = 0;
    %i < %count;
    while (%i < %count)
    {
        %obj = %tree.getObject(%i);
        %this.addDataTree(%obj, %prefix @ "\t" @ %obj.text);
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
        }
        else
        {
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
                }
                %j = %j + 1;
            }
            if (!%match)
            {
                return 0;
            }
        }
        %i = %i + 1;
    }
    %this.nodeDictionary.put(%path, %node);
    return %node;
}
function TreeBrowserControl::clear(%this)
{
    %this.root.deleteMembers();
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
        return;
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
}
function TreeBrowserFrame::onCreatedChild(%this, %child)
{
    Parent::onCreatedChild(%this, %child);
    %child.menuText.reposition(5, 2);
    if (!(getWord(%child.getNamespaceList(), 0) $= "TreeBrowserItem"))
    {
        %child.bindClassName("TreeBrowserItem");
    }
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
            %this.treeBrowser.selectNextLeaf(-(1), 0);
            return 1;
        }
        if (%this.getStringFromKeyCode(%keyCode) $= "down")
        {
            %this.treeBrowser.selectNextLeaf(1, 0);
            return 1;
        }
    }
    return 0;
}
function TreeBrowserControl::makeSomeTreeData()
{
    new SimGroup("") {
        text = new SimGroup("") {
        text = new SimGroup("") {
        text = "Bar stool";
    }; @ "Orange plush chair";
    }; @ "Zen pillow";
    };
    new SimGroup("") {
        text = new SimGroup("") {
        text = "Chairs";
    }; @ "Sofas";
    };
    new SimGroup("") {
        text = new SimGroup("") {
        text = new SimGroup("") {
        text = "Halogen torchiere";
    }; @ "Track lighting";
    }; @ "Glow in the dark stars";
    };
    %root = new SimGroup("") {
        text = "My Stuff";
    };
    new SimGroup("") {
        text = new SimGroup("") {
        text = new SimGroup("") {
        text = "Seating";
    }; @ "Lights";
    }; @ "Appliances";
    };
    if (isObject(RootGroup))
    {
        RootGroup.add(%root);
    }
    return %root;
}
function TreeBrowserControl::test()
{
    new GuiControl(BrowserParent) {
        position = "50 50";
        extent = "250 100";
    };
    %rootCtrl = Canvas.getContent();
    %rootCtrl.add(BrowserParent);
    TreeBrowserControl::newControl(BrowserParent, "TheBrowser");
    TheBrowser.setNumChildren(1);
    %data = TreeBrowserControl::makeSomeTreeData();
    TheBrowser.setDataTree(%data);
}
function dumpTree(%tree)
{
    dumpSubtree(%tree, "");
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
}
function textToTree(%text)
{
    %tree = 0;
    %text = strreplace(%text, "'", "\"");
    %text = strreplace(%text, "[\"", "new SimGroup() { text = |");
    %text = strreplace(%text, "\"", "|; ");
    %text = strreplace(%text, "|", "\"");
    %text = strreplace(%text, "[", "new SimGroup() { ");
    %text = strreplace(%text, "]", "};");
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
        %i < %count;
    }
    return %text;
}
