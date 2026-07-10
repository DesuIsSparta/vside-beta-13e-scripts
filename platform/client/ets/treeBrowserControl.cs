function TreeBrowserControl::newControl(%parent, %name) {
    if (!(isObject(%parent))) {
        return;
    }
    %ctrl = new GuiArray2Ctrl("") {
        profile = 0 @ "GuiDefaultProfile";
        position = "0 0";
        extent = %parent.getExtent();
        childrenClassName = "GuiControl";
        childrenExtent = %parent.getExtent();
        spacing = 0;
        numRowsOrCols = 1;
        inRows = 1;
        sluggishness = 0.5;
    };
    "TreeBrowserControl".bindClassName(%ctrl);
    %name.bindClassName(%ctrl);
    %name.setName(%ctrl);
    %ctrl.add(%parent);
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
        name = 0 @ "";
    };
    %ctrl.nodeDictionary = safeNewScriptObject("StringMap", "", 0);
    if (isObject(RootGroup)) {
        %ctrl.root.add(RootGroup);
    }
    %ctrl.Path = "";
    "".goToPath(%ctrl);
    return %ctrl;
};
function TreeBrowserControl::onResized(%this) {
    %curMenu = %this.getCurrentMenu();
    %hilitedIdx = -(1.0);
    if (isObject(%curMenu)) {
        %hilitedCell = %curMenu.getHilitedCell();
        if (isObject(%hilitedCell)) {
            %hilitedIdx = %hilitedCell.getObjectIndex(%curMenu);
        }
    }
    %parentExtent = %this.getParent().getTrgExtent();
    if (%this.isExpanded) {
        %this.collapsedParentExtent = getWords(VectorSub(%parentExtent @ " " @ 0, %this.expandDelta @ " " @ 0), 0, 1);
    }
    %this.childrenExtent = %parentExtent;
    0.setNumChildren(%this);
    %this.goToCurrentPath();
    if ((%hilitedIdx >= 0.0)) {
        %curMenu = %this.getCurrentMenu();
        if (isObject(%curMenu) && (%curMenu.getCount() > %hilitedIdx)) {
            %hilitedIdx.getObject(%curMenu).hiliteCell(%curMenu);
        }
    }
};
function TreeBrowserControl::onCreatedChild(%this, %child, %x, %unused) {
    %leftPadding = ((%this.buttonWidth + %this.buttonPadding) * %x);
    if (%this.isExpanded) {
    }
    %contentsExtentX = (getWord(%child.getExtent(), 0) - getWord(%this.collapsedParentExtent, 0));
    %leftPadding;
    if (%this.isExpanded) {
    }
    %contentsExtentY = getWord(%child.getExtent(), 1);
    getWord(%this.collapsedParentExtent, 1);
    %child.expandedPane = new GuiControl("") {
        profile = 0 @ "FocusableDefaultProfile";
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
    %child.expandedPane.add(%child);
    %child.contentPane = new GuiControl("") {
        profile = 0 @ "FocusableDefaultProfile";
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
    "TreeBrowserContentPane".bindClassName(%child.contentPane);
    %child.contentPane.add(%child);
    %child.scroll = new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
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
    if (%this.adjustMenuCellHeight) {
        %numCanFit = mFloor((%contentsExtentY / (%menuTrgCellHeight + %menuCellSpacing)));
        %menuTrgCellHeight = ((%contentsExtentY / %numCanFit) - %menuCellSpacing);
        %d = (%menuTrgCellHeight - mFloor(%menuTrgCellHeight));
        if ((%d >= 0.5)) {
            %menuTrgCellHeight = mCeil(%menuTrgCellHeight);
        }
        %menuTrgCellHeight = mFloor(%menuTrgCellHeight);
    }
    %child.menu = new GuiArray2Ctrl("") {
        profile = 0 @ %this.menuProfile;
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = (%contentsExtentX - 6.0) @ " " @ %menuTrgCellHeight;
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
    "MenuControl".bindClassName(%child.menu);
    "TreeBrowserFrame".bindClassName(%child.menu);
    %child.menu.layer = 0;
    %child.menu.add(%child.scroll);
    %child.menu.scroll = %child.scroll;
    %child.scroll.add(%child);
};
function TreeBrowserControl::getHiliteProxy(%this) {
    return "";
};
function TreeBrowserControl::scrollToLevel(%this, %level) {
    %this.level = %level;
    0.setTrgPosition(%this, (-(%level) * getWord(%this.childrenExtent, 0)));
};
function TreeBrowserControl::goToCurrentPath(%this, %focus) {
    if (!(isDefined("%focus"))) {
        %focus = 1;
    }
    %focus.goToPath(%this, %this.Path);
};
function TreeBrowserControl::goToParentPath(%this) {
    %currentNodeName = %this.Path.getNode(%this).name;
    %parentPath = getFields(%this.Path, 0, (getFieldCount(%this.Path) - 2.0));
    %parentPath.goToPath(%this);
    %menu = %this.getCurrentMenu();
    %count = %menu.getCount();
    %i = 0;
    while ((%i < %count)) {
        %menuItem = %i.getObject(%menu);
        if ((%menuItem.name $= %currentNodeName)) {
            %i.hiliteCell(%menu, 0);
        }
        %i = (%i + 1.0);
    }
};
function TreeBrowserControl::getMenuText(%this, %text) {
    return %text;
};
function TreeBrowserControl::goToPath(%this, %path, %focus) {
    if (!(isDefined("%focus"))) {
        %focus = 1;
    }
    %path = trim(%path);
    %node = %path.getNode(%this);
    if (!(isObject(%node))) {
        return 0;
    }
    %pathchanged = !(%this.Path $= %path);
    %this.Path = %path;
    %oldLevel = %this.level;
    %this.level = getFieldCount(%path);
    %level = ;
    if ((%this.getCount() <= %level)) {
        (%level + 1.0).setNumChildren(%this);
    }
    %level.scrollToLevel(%this);
    %leafNode = 0;
    %expanded = %this.Path.isNodeExpanded(%this);
    if (%this.isExpanded) {
    }
    if ((%level != %oldLevel)) {
        %oldChild = 0.getChild(%this, %oldLevel);
        %oldChild.expandedPane.clear();
    }
    if (%expanded) {
    }
    if (!(%this.isExpanded)) {
        %expandDelta = "expandDelta".getFieldValue(%this);
        if ((%expandDelta $= "")) {
            warn(getScopeName() @ "->trying to expand view but no expandDelta is set. returning!");
        }
        %expandDelta.expandView(%this);
        return;
    }
    if (%this.isExpanded) {
    }
    if (!(%expanded)) {
        %this.collapseView();
        %this.focusCurrentFrame();
        return;
    }
    %child = 0.getChild(%this, %level);
    %count = %node.getCount();
    if ((%count == 0.0)) {
        1.setVisible(%child.contentPane);
        0.setVisible(%child.scroll);
        0.setVisible(%child.menu);
        %leafNode = 1;
        %child.contentPane.node = %node;
        %child.contentPane.clear();
        %child.contentPane.fillLeafPane(%this);
        if (%expanded) {
        }
        if (%this.isExpanded) {
            %child.expandedPane.clear();
            1.setVisible(%child.expandedPane);
            %child.expandedPane.fillExpandedContentPane(%this);
        }
        0.setVisible(%child.expandedPane);
        if (%focus) {
        }
        if (%this.isVisibleRecursive()) {
        }
        if (%pathchanged) {
            1.makeFirstResponder(%child.contentPane);
        }
    }
    0.setVisible(%child.contentPane);
    1.setVisible(%child.scroll);
    1.setVisible(%child.menu);
    if (%expanded) {
    }
    if (%this.isExpanded) {
        %child.expandedPane.clear();
        1.setVisible(%child.expandedPane);
        %child.expandedPane.fillExpandedFrame(%this);
    }
    0.setVisible(%child.expandedPane);
    %currentCount = %child.menu.getCount();
    %this.filterText = strlwr(%this.filterText);
    %this.filterText = trim(%this.filterText);
    %count = 0;
    %n = (%node.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %subNode = %n.getObject(%node);
        %subNode.passesFilter = %this.filterText.nodePassesFilter(%this, %subNode);
        if (%subNode.passesFilter) {
            %count = (%count + 1.0);
        }
        %n = (%n - 1.0);
    }
    if ((%currentCount != %count)) {
        %child.Path = (%n >= 0.0) @ "ForceUpdatePlease!!!";
    }
    if (!(%child.Path $= %path)) {
        %child.menu.clear();
        %child.menu.deferReseat = 1;
        %totalCount = %node.getCount();
        %n = 0;
        while ((%n < %totalCount)) {
            %subNode = %n.getObject(%node);
            if (%subNode.passesFilter) {
                %menuItem = "".addMenuItem(%child.menu, %subNode.name.getMenuText(%this), %this.getId() @ ".select(\"" @ %subNode.name @ "\");", "");
                %menuItem.name = %subNode.name;
            }
            %n = (%n + 1.0);
        }
        %child.menu.reseatChildren();
        0.hiliteCell(%child.menu, 0);
    }
    if (%focus) {
    }
    if (%this.isVisibleRecursive()) {
    }
    if (%pathchanged) {
        1.makeFirstResponder(%child.menu);
    }
    %child.Path = (%n < %totalCount) @ %path;
    if (%leafNode) {
    }
    %numButtons = %level;
    (%level - 1.0);
    %offset = 0;
    if (%this.isExpanded) {
    }
    %height = getWord(%this.getExtent(), 1);
    getWord(%this.collapsedParentExtent, 1);
    %i = 0;
    while ((%i < mMax(%numButtons, %this.numButtons))) {
        if ((%i < %numButtons)) {
            if (!(isObject(%i, %this.button))) {
                %this.button = new GuiBitmapButtonCtrl("") {
                    profile = 0 @ "ETSVerticalButtonProfile";
                    horizSizing = "right";
                    vertSizing = "bottom";
                    position = %offset @ " " @ 0;
                    extent = %this.buttonWidth @ " " @ %height;
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
                }; @ %i
                %this.button.add(%this.Parent, %i);
            }
            if (!(%this.button.getExtent(%i) $= %this.buttonWidth @ " " @ %height)) {
                %height.resize(%i, %this.button, %this.buttonWidth);
            }
            %button = %this.button;
            %i;
            1.setVisible(%button);
            %button.command = %this.getId() @ ".goToPath(\"" @ getFields(%this.Path, 0, %i) @ "\");";
            %button.text = getField(%this.Path, %i);
            (%i < (%level - 1.0)).setActive(%button);
        }
        0.setVisible(%i, %this.button);
        %offset = (%offset + (%this.buttonWidth + %this.buttonPadding));
        %i = (%i + 1.0);
    }
    %this.numButtons = (%i < mMax(%numButtons, %this.numButtons)) @ %numButtons;
    return 1;
};
function TreeBrowserControl::nodePassesFilter(%this, %node, %filterText) {
    if ((%filterText $= "")) {
        return 1;
    }
    %searchText = %node.getNodeSearchText(%this);
    %ret = (strstr(%searchText, %filterText) >= 0.0);
    return %ret;
};
function TreeBrowserControl::getNodeSearchText(%this, %node) {
    if (!(%node.searchText $= "")) {
        return %node.searchText;
    }
    %sku = %node.sku;
    if (!(%sku $= "")) {
        %ret = %sku.findBySku(SkuManager).searchText;
    }
    %ret = %node.name;
    %n = (%node.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %subNode = %n.getObject(%node);
        %subNodeSearchText = %subNode.getNodeSearchText(%this);
        %w = (getWordCount(%subNodeSearchText) - 1.0);
        while ((%w >= 0.0)) {
            %word = getWord(%subNodeSearchText, %w);
            if (!(hasWord(%ret, %word))) {
                %ret = %ret @ " " @ %word;
            }
            %w = (%w - 1.0);
        }
        %n = (%n - 1.0);
        (%w >= 0.0);
    }
    %ret = trim(%ret);
    (%n >= 0.0);
    %node.searchText = %ret;
    return %ret;
};
function TreeBrowserControl::expandView(%this, %delta) {
    if (%this.isExpanded) {
        return;
    }
    %this.isExpanded = 1;
    %collapsedParentExtent = %this.getParent().getTrgExtent();
    %delta.resizeParentsBy(%this);
    %this.onResized();
    %this.collapsedParentExtent = %collapsedParentExtent;
    %trg = %this.getTrgPosition();
    getWord(%trg, 1).reposition(%this, getWord(%trg, 0));
};
function TreeBrowserControl::resizeParentsBy(%this, %delta) {
    %extent = %this.getParent().getTrgExtent();
    %newExtent = VectorAdd(%extent @ " " @ 0, %delta @ " " @ 0);
    %newExtent = getWords(%newExtent, 0, 1);
    getWord(%newExtent, 1).resize(%this.getParent(), getWord(%newExtent, 0));
};
function TreeBrowserControl::collapseView(%this) {
    if (!(%this.isExpanded)) {
        return;
    }
    %this.isExpanded = 0;
    %delta = VectorSub(%this.collapsedParentExtent @ " " @ 0, %this.getParent().getTrgExtent() @ " " @ 0);
    getWords(%delta, 0, 1).resizeParentsBy(%this);
    %this.onResized();
    %trg = %this.getTrgPosition();
    getWord(%trg, 1).reposition(%this, getWord(%trg, 0));
};
function TreeBrowserControl::isNodeExpanded(%this, %path) {
    return 0;
};
function TreeBrowserControl::fillExpandedFrame(%this, %expandedFrame) {
    %frame = %expandedFrame.getParent();
    %rightEdgeOfMenu = (getWord(%frame.menu.getExtent(), 0) + getWord(%frame.menu.getPosition(), 0));
    new GuiMLTextCtrl("") {
        position = 0 @ %rightEdgeOfMenu @ " " @ 0;
        extent = "50 18";
        text = "<color:ffffff>override me!";
        visible = 1;
    };.add(%expandedFrame);
};
function TreeBrowserControl::fillExpandedContentPane(%this, %expandedPane) {
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = (getWord(%frame.contentPane.getExtent(), 0) + getWord(%frame.contentPane.getPosition(), 0));
    new GuiMLTextCtrl("") {
        position = 0 @ %rightEdgeOfContentPane @ " " @ 0;
        extent = "50 18";
        text = "<color:ffffff>override me!";
        visible = 1;
    };.add(%expandedPane);
};
function TreeBrowserControl::isInSubdirOfPath(%this, %path) {
    %depth = getFieldCount(%path);
    return (%path $= getFields(%this.Path, %depth));
};
function TreeBrowserControl::fillLeafPane(%this, %pane) {
    %level = %this.level;
    new GuiTextCtrl("") {
        profile = 0 @ "GuiTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = getWord(%pane.getExtent(), 0) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = getField(%this.Path, (%level - 1.0));
        maxLength = 255;
    };.add(%pane);
};
function TreeBrowserControl::select(%this, %value) {
    %this.Path @ "\t" @ %value.goToPath(%this);
};
function TreeBrowserControl::selectNextLeaf(%this, %forward, %slide) {
    %path = %forward.getNextLeaf(%this, %this.Path);
    if (!(%path $= "")) {
        if (isDefined("%slide")) {
            %level = getFieldCount(%path);
            if (!(%slide)) {
            }
            if ((getFieldCount(%this.Path) != %level)) {
                %this.level = %level;
                0.reposition(%this, (-(%level) * getWord(%this.childrenExtent, 0)));
            }
        }
        %path.goToPath(%this);
    }
};
function TreeBrowserControl::getNextLeaf(%this, %path, %forward) {
    %node = %path.getNode(%this);
    if (!(isObject(%node))) {
        return "";
    }
    if ((%node.getCount() > 0.0)) {
        %foundChildBearingNode = 1;
    }
    %foundChildBearingNode = 0;
    while (!(%foundChildBearingNode)) {
        %depth = getFieldCount(%path);
        %name = getField(%path, (%depth - 1.0));
        if ((%depth <= 1.0)) {
            return "";
        }
        %ppath = getFields(%path, 0, (%depth - 2.0));
        %pnode = %ppath.getNode(%this);
        %childCount = %pnode.getCount();
        %nidx = -(1.0);
        %i = 0;
        while ((%i < %childCount)) {
            %child = %i.getObject(%pnode);
            if ((%child.name $= %name)) {
                %nidx = %i;
            }
            %i = (%i + 1.0);
        }
        if ((%nidx < 0.0)) {
            error(getScopeName() @ "->nidx < 0.");
            return "";
        }
        %tidx = (%nidx + %forward);
        if ((%tidx >= 0.0)) {
        }
        if ((%tidx < %childCount)) {
            %node = %tidx.getObject(%pnode);
            %path = %ppath @ "\t" @ %node.name;
            if ((%node.getCount() > 0.0)) {
                %foundChildBearingNode = 1;
            }
            return %path;
        }
        %node = %pnode;
        %path = %ppath;
    }
    %cnt = %node.getCount();
    while ((0.0 > !(%foundChildBearingNode))) {
        if ((%forward > 0.0)) {
        }
        %slot = (%cnt - 1.0);
        0;
        %node = %slot.getObject(%node);
        %path = %path @ "\t" @ %node.name;
        %cnt = %node.getCount();
    }
    return %path;
};
function TreeBrowserControl::addNode(%this, %path) {
    %path.addNodeAt(%this, "");
};
function TreeBrowserControl::addNodeAt(%this, %prefix, %subpath) {
    %prefix = trim(%prefix);
    %subpath = trim(%subpath);
    %baseNode = %prefix.getNode(%this);
    if (!(isObject(%baseNode))) {
        return 0;
    }
    %childNodeName = getField(%subpath, 0);
    if ((%childNodeName $= "")) {
        return %baseNode;
    }
    %fullPath = %prefix @ "\t" @ %childNodeName;
    %childNode = %fullPath.get(%this.nodeDictionary);
    if (!(isObject(%childNode))) {
        %newSet = new SimGroup("") {
            name = 0 @ %childNodeName;
        };
        %newSet.add(%baseNode);
        %newSet.put(%this.nodeDictionary, %fullPath);
    }
    return getFields(%subpath, 1).addNodeAt(%this, %fullPath);
};
function TreeBrowserControl::getNodePath(%this, %node) {
    %path = "";
    %delim = "";
    if (isObject(%node)) {
    }
    while (!(%node.name $= "")) {
        %path = %node.name @ %delim @ %path;
        %delim = "\t";
        if ((%node.getId() $= %this.root.getId())) {
            %node = "";
        }
        %node = %node.getGroup();
        if (isObject(%node)) {
        }
    }
    return %path;
};
function TreeBrowserControl::deleteNodeAtPath(%this, %path) {
    %node = %path.getNode(%this);
    if (!(isObject(%node))) {
        return;
    }
    %node.deleteNode(%this);
    if (%path.isInSubdirOfPath(%this)) {
        %depth = getFieldCount(%path);
        getFields(%path, 0, (%depth - 2.0)).goToPath(%this);
    }
};
function TreeBrowserControl::deleteNode(%this, %node) {
    if (!(isObject(%node))) {
        return;
    }
    %i = (%node.getCount() - 1.0);
    while ((%i >= 0.0)) {
        %i.getObject(%node).deleteNode(%this);
        %i = (%i - 1.0);
    }
    %node.delete();
};
function TreeBrowserControl::addMenuData(%this, %prefix, %list) {
    %node = %prefix.getNode(%this);
    if (!(isObject(%node))) {
        return;
    }
    %listCount = getFieldCount(%list);
    %i = 0;
    while ((%i < %listCount)) {
        %itemName = getField(%list, %i);
        if (!(%itemName $= "")) {
            new SimGroup("") {
                name = 0 @ %itemName;
            };.add(%node);
        }
        %i = (%i + 1.0);
    }
    %this.goToCurrentPath();
};
function TreeBrowserControl::setDataTree(%this, %tree) {
    if (isObject(%tree)) {
    }
    if (!(%tree.text $= "")) {
        %this.title = %tree.text;
        %this.title.addMenuData(%this, "");
        %this.title.addDataTree(%this, %tree);
        %this.title.goToPath(%this);
    }
};
function TreeBrowserControl::addDataTree(%this, %tree, %prefix) {
    %count = %tree.getCount();
    %items = "";
    %i = 0;
    while ((%i < %count)) {
        %obj = %i.getObject(%tree);
        %items = %items @ "\t" @ %obj.text;
        %i = (%i + 1.0);
    }
    %items.addMenuData(%this, %prefix);
    %i = 0;
    (%i < %count);
    while ((%i < %count)) {
        %obj = %i.getObject(%tree);
        %prefix @ "\t" @ %obj.text.addDataTree(%this, %obj);
        %i = (%i + 1.0);
    }
};
function TreeBrowserControl::getNode(%this, %path) {
    %node = %path.get(%this.nodeDictionary);
    if (isObject(%node)) {
        return %node;
    }
    %pathCount = getFieldCount(%path);
    %node = %this.root;
    %i = 0;
    while ((%i < %pathCount)) {
        %dirName = getField(%path, %i);
        if ((%dirName $= "")) {
        }
        %nodeCount = %node.getCount();
        %match = 0;
        %j = 0;
        while ((%j < %nodeCount)) {
            %subNode = %j.getObject(%node);
            if ((%subNode.name $= %dirName)) {
                %node = %subNode;
                %match = 1;
            }
            %j = (%j + 1.0);
        }
        if (!(%match)) {
            return 0;
        }
        %i = (%i + 1.0);
    }
    %node.put(%this.nodeDictionary, %path);
    return %node;
};
function TreeBrowserControl::clear(%this) {
    %this.root.deleteMembers();
};
function TreeBrowserControl::getCurrentNode(%this) {
    return %this.Path.getNode(%this);
};
function TreeBrowserControl::getCurrentFrame(%this) {
    return getFieldCount(%this.Path).getObject(%this);
};
function TreeBrowserControl::getCurrentMenu(%this) {
    %frame = %this.getCurrentFrame();
    return %frame.menu;
};
function TreeBrowserControl::getCurrentContentPane(%this) {
    %frame = %this.getCurrentFrame();
    return %frame.contentPane;
};
function TreeBrowserControl::focusCurrentFrame(%this) {
    if (!(%this.isVisible())) {
        return;
    }
    %frame = %this.getCurrentFrame();
    %contentPane = %this.getCurrentContentPane();
    %menu = %this.getCurrentMenu();
    if (%menu.isVisibleRecursive()) {
        1.makeFirstResponder(%menu);
    }
    if (%contentPane.isVisibleRecursive()) {
        1.makeFirstResponder(%contentPane);
    }
};
function TreeBrowserFrame::onCreatedChild(%this, %child) {
    Parent::onCreatedChild(%this, %child);
    2.reposition(%child.menuText, 5);
    if (!(getWord(%child.getNamespaceList(), 0) $= "TreeBrowserItem")) {
        "TreeBrowserItem".bindClassName(%child);
    }
};
function TreeBrowserFrame::onKeyDown(%this, %unused, %keyCode) {
    if ((%keyCode.getStringFromKeyCode(%this) $= "left")) {
        %this.treeBrowser.goToParentPath();
        return 1;
    }
    if ((%keyCode.getStringFromKeyCode(%this) $= "right")) {
        %this.getHilitedCell().onSelect();
        return 1;
    }
    return 0;
};
function TreeBrowserContentPane::onKeyDown(%this, %unused, %keyCode) {
    if ((%keyCode.getStringFromKeyCode(%this) $= "left")) {
        %this.treeBrowser.goToParentPath();
        return 1;
    }
    if ((%keyCode.getStringFromKeyCode(%this) $= "up")) {
        0.selectNextLeaf(%this.treeBrowser, -(1.0));
        return 1;
    }
    if ((%keyCode.getStringFromKeyCode(%this) $= "down")) {
        0.selectNextLeaf(%this.treeBrowser, 1);
        return 1;
    }
    return 0;
};
function TreeBrowserControl::makeSomeTreeData() {
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
        text = 0 @ "My Stuff";
    };
    new SimGroup("") {
        text = new SimGroup("") {
        text = new SimGroup("") {
        text = "Seating";
    }; @ "Lights";
    }; @ "Appliances";
    };
    if (isObject(RootGroup)) {
        %root.add(RootGroup);
    }
    return %root;
};
function TreeBrowserControl::test() {
    new GuiControl(BrowserParent) {
        position = "50 50";
        extent = "250 100";
    };
    %rootCtrl = Canvas.getContent();
    %rootCtrl.add();
    TreeBrowserControl::newControl(BrowserParent, "TheBrowser");
    1.setNumChildren(TheBrowser);
    %data = TreeBrowserControl::makeSomeTreeData();
    BrowserParent;
    %data.setDataTree(TheBrowser);
};
function dumpTree(%tree) {
    dumpSubtree(%tree, "");
};
function dumpSubtree(%subtree, %prefix) {
    echo(%prefix @ %subtree.text);
    %count = %subtree.getCount();
    %i = 0;
    while ((%i < %count)) {
        %obj = %i.getObject(%subtree);
        dumpSubtree(%obj, %prefix @ "   ");
        %i = (%i + 1.0);
    }
};
function deleteTree(%tree) {
    %count = %tree.getCount();
    %i = 0;
    while ((%i < %count)) {
        %obj = 0.getObject(%tree);
        deleteTree(%obj);
        %i = (%i + 1.0);
    }
    %tree.delete();
};
function textToTree(%text) {
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
};
function treeToText(%tree) {
    %text = "";
    if (isObject(%tree)) {
        %text = "[\"" @ %tree.text @ "\"";
        %count = %tree.getCount();
        %i = 0;
        while ((%i < %count)) {
            %obj = %i.getObject(%tree);
            %text = %text @ treeToText(%obj);
            %i = (%i + 1.0);
        }
        %text = %text @ "]";
        (%i < %count);
    }
    return %text;
};
