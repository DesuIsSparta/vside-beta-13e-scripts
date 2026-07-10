function TreeBrowserControl::newControl(%parent, %name) {
    if (!(isObject(%parent))) {
        return;
    }
    0;
    %ctrl = new ""() {
        profile = GuiArray2Ctrl @ "GuiDefaultProfile";
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
    0;
    %ctrl.root = new ""() {
        name = SimGroup @ "";
    };
    %ctrl.nodeDictionary = safeNewScriptObject("StringMap", "", 0);
    if (isObject(RootGroup)) {
        %ctrl.root.add();
    }
    %ctrl.Path = RootGroup @ "";
    %ctrl.goToPath("");
    return %ctrl;
};
function TreeBrowserControl::onResized(%this) {
    %curMenu = %this.getCurrentMenu();
    %hilitedIdx = -(1.0);
    if (isObject(%curMenu)) {
        %hilitedCell = %curMenu.getHilitedCell();
        if (isObject(%hilitedCell)) {
            %hilitedIdx = %curMenu.getObjectIndex(%hilitedCell);
        }
    }
    %parentExtent = %this.getParent().getTrgExtent();
    if (%this.isExpanded) {
        %this.collapsedParentExtent = getWords(VectorSub(%parentExtent @ " " @ 0, %this.expandDelta @ " " @ 0), 0, 1);
    }
    %this.childrenExtent = %parentExtent;
    %this.setNumChildren(0);
    %this.goToCurrentPath();
    if ((0.0 >= %hilitedIdx)) {
        %curMenu = %this.getCurrentMenu();
        if (isObject(%curMenu)) {
            if ((%hilitedIdx > %curMenu.getCount())) {
                %curMenu.hiliteCell(%curMenu.getObject(%hilitedIdx));
            }
        }
    }
};
function TreeBrowserControl::onCreatedChild(%this, %child, %x, %unused) {
    %leftPadding = (%x * (%this.buttonPadding + %this.buttonWidth));
    if (%this.isExpanded) {
    }
    %contentsExtentX = (getWord(%this.collapsedParentExtent, 0) - getWord(%child.getExtent(), 0));
    %leftPadding;
    if (%this.isExpanded) {
    }
    %contentsExtentY = getWord(%child.getExtent(), 1);
    getWord(%this.collapsedParentExtent, 1);
    0;
    %child.expandedPane = new ""() {
        profile = GuiControl @ "FocusableDefaultProfile";
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
    0;
    %child.contentPane = new ""() {
        profile = GuiControl @ "FocusableDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%this.buttonPadding - (%this.buttonWidth - %leftPadding)) @ " " @ 0;
        extent = (%this.buttonPadding + (%this.buttonWidth + %contentsExtentX)) @ " " @ %contentsExtentY;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        hiliteProxy = %this.getHiliteProxy();
        treeBrowser = %this;
    };
    %child.contentPane.bindClassName("TreeBrowserContentPane");
    %child.add(%child.contentPane);
    0;
    %child.scroll = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
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
        %numCanFit = mFloor(((%menuCellSpacing + %menuTrgCellHeight) / %contentsExtentY));
        %menuTrgCellHeight = (%menuCellSpacing - (%numCanFit / %contentsExtentY));
        %d = (mFloor(%menuTrgCellHeight) - %menuTrgCellHeight);
        if ((0.5 >= %d)) {
            %menuTrgCellHeight = mCeil(%menuTrgCellHeight);
        }
        %menuTrgCellHeight = mFloor(%menuTrgCellHeight);
    }
    0;
    %child.menu = new ""() {
        profile = GuiArray2Ctrl @ %this.menuProfile;
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = (6.0 - %contentsExtentX) @ " " @ %menuTrgCellHeight;
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
};
function TreeBrowserControl::getHiliteProxy(%this) {
    return "";
};
function TreeBrowserControl::scrollToLevel(%this, %level) {
    %this.level = %level;
    %this.setTrgPosition((getWord(%this.childrenExtent, 0) * -(%level)), 0);
};
function TreeBrowserControl::goToCurrentPath(%this, %focus) {
    if (!(isDefined("%focus"))) {
        %focus = 1;
    }
    %this.goToPath(%this.Path, %focus);
};
function TreeBrowserControl::goToParentPath(%this) {
    %currentNodeName = %this.getNode(%this.Path).name;
    %parentPath = getFields(%this.Path, 0, (2.0 - getFieldCount(%this.Path)));
    %this.goToPath(%parentPath);
    %menu = %this.getCurrentMenu();
    %count = %menu.getCount();
    %i = 0;
    if ((%count < %i)) {
        %menuItem = %menu.getObject(%i);
        if ((%menuItem.name $= %currentNodeName)) {
            %menu.hiliteCell(0, %i);
        }
        %i = (1.0 + %i);
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
    %node = %this.getNode(%path);
    if (!(isObject(%node))) {
        return 0;
    }
    %pathchanged = !(%this.Path $= %path);
    %this.Path = %path;
    %oldLevel = %this.level;
    %this.level = getFieldCount(%path);
    %level = ;
    if ((%level <= %this.getCount())) {
        %this.setNumChildren((1.0 + %level));
    }
    %this.scrollToLevel(%level);
    %leafNode = 0;
    %expanded = %this.isNodeExpanded(%this.Path);
    if (%this.isExpanded) {
    }
    if ((%oldLevel != %level)) {
        %oldChild = %this.getChild(%oldLevel, 0);
        %oldChild.expandedPane.clear();
    }
    if (%expanded) {
    }
    if (!(%this.isExpanded)) {
        %expandDelta = %this.getFieldValue("expandDelta");
        if ((%expandDelta $= "")) {
            warn(getScopeName() @ "->trying to expand view but no expandDelta is set. returning!");
        }
        %this.expandView(%expandDelta);
        return;
    }
    if (%this.isExpanded) {
    }
    if (!(%expanded)) {
        %this.collapseView();
        %this.focusCurrentFrame();
        return;
    }
    %child = %this.getChild(%level, 0);
    %count = %node.getCount();
    if ((0.0 == %count)) {
        %child.contentPane.setVisible(1);
        %child.scroll.setVisible(0);
        %child.menu.setVisible(0);
        %leafNode = 1;
        %child.contentPane.node = %node;
        %child.contentPane.clear();
        %this.fillLeafPane(%child.contentPane);
        if (%expanded) {
        }
        if (%this.isExpanded) {
            %child.expandedPane.clear();
            %child.expandedPane.setVisible(1);
            %this.fillExpandedContentPane(%child.expandedPane);
        }
        %child.expandedPane.setVisible(0);
        if (%focus) {
        }
        if (%this.isVisibleRecursive()) {
        }
        if (%pathchanged) {
            %child.contentPane.makeFirstResponder(1);
        }
    }
    %child.contentPane.setVisible(0);
    %child.scroll.setVisible(1);
    %child.menu.setVisible(1);
    if (%expanded) {
    }
    if (%this.isExpanded) {
        %child.expandedPane.clear();
        %child.expandedPane.setVisible(1);
        %this.fillExpandedFrame(%child.expandedPane);
    }
    %child.expandedPane.setVisible(0);
    %currentCount = %child.menu.getCount();
    %this.filterText = strlwr(%this.filterText);
    %this.filterText = trim(%this.filterText);
    %count = 0;
    %n = (1.0 - %node.getCount());
    if ((0.0 >= %n)) {
        %subNode = %node.getObject(%n);
        %subNode.passesFilter = %this.nodePassesFilter(%subNode, %this.filterText);
        if (%subNode.passesFilter) {
            %count = (1.0 + %count);
        }
        %n = (1.0 - %n);
    }
    if ((%count != %currentCount)) {
        %child.Path = (0.0 >= %n) @ "ForceUpdatePlease!!!";
    }
    if (!(%child.Path $= %path)) {
        %child.menu.clear();
        %child.menu.deferReseat = 1;
        %totalCount = %node.getCount();
        %n = 0;
        if ((%totalCount < %n)) {
            %subNode = %node.getObject(%n);
            if (%subNode.passesFilter) {
                %menuItem = %child.menu.addMenuItem(%this.getMenuText(%subNode.name), %this.getId() @ ".select(\"" @ %subNode.name @ "\");", "", "");
                %menuItem.name = %subNode.name;
            }
            %n = (1.0 + %n);
        }
        %child.menu.reseatChildren();
        %child.menu.hiliteCell(0, 0);
    }
    if (%focus) {
    }
    if (%this.isVisibleRecursive()) {
    }
    if (%pathchanged) {
        %child.menu.makeFirstResponder(1);
    }
    %child.Path = (%totalCount < %n) @ %path;
    if (%leafNode) {
    }
    %numButtons = %level;
    (1.0 - %level);
    %offset = 0;
    if (%this.isExpanded) {
    }
    %height = getWord(%this.getExtent(), 1);
    getWord(%this.collapsedParentExtent, 1);
    %i = 0;
    if ((mMax(%numButtons, %this.numButtons) < %i)) {
        if ((%numButtons < %i)) {
            if (!(isObject(%this.button))) {
                0;
                %this.button = new ""() {
                    profile = GuiBitmapButtonCtrl @ "ETSVerticalButtonProfile";
                    horizSizing = %i @ "right";
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
                %this.Parent.add(%this.button);
            }
            if (!(%i @ %i @ " " @ %this.button.getExtent() $= %this.buttonWidth @ " " @ %height)) {
                %this.button.resize(%this.buttonWidth, %height);
            }
            %button = %this.button;
            %i @ %i;
            %button.setVisible(1);
            %button.command = %this.getId() @ ".goToPath(\"" @ getFields(%this.Path, 0, %i) @ "\");";
            %button.text = getField(%this.Path, %i);
            %button.setActive(((1.0 - %level) < %i));
        }
        %this.button.setVisible(0);
        %offset = ((%this.buttonPadding + %this.buttonWidth) + %offset);
        %i;
        %i = (1.0 + %i);
    }
    %this.numButtons = (mMax(%numButtons, %this.numButtons) < %i) @ %numButtons;
    return 1;
};
function TreeBrowserControl::nodePassesFilter(%this, %node, %filterText) {
    if ((%filterText $= "")) {
        return 1;
    }
    %searchText = %this.getNodeSearchText(%node);
    %ret = (0.0 >= strstr(%searchText, %filterText));
    return %ret;
};
function TreeBrowserControl::getNodeSearchText(%this, %node) {
    if (!(%node.searchText $= "")) {
        return %node.searchText;
    }
    %sku = %node.sku;
    if (!(%sku $= "")) {
        %ret = %sku.findBySku().searchText;
        SkuManager;
    }
    %ret = %node.name;
    %n = (1.0 - %node.getCount());
    if ((0.0 >= %n)) {
        %subNode = %node.getObject(%n);
        %subNodeSearchText = %this.getNodeSearchText(%subNode);
        %w = (1.0 - getWordCount(%subNodeSearchText));
        if ((0.0 >= %w)) {
            %word = getWord(%subNodeSearchText, %w);
            if (!(hasWord(%ret, %word))) {
                %ret = %ret @ " " @ %word;
            }
            %w = (1.0 - %w);
        }
        %n = (1.0 - %n);
        (0.0 >= %w);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    %node.searchText = %ret;
    return %ret;
};
function TreeBrowserControl::expandView(%this, %delta) {
    if (%this.isExpanded) {
        return;
    }
    %this.isExpanded = 1;
    %collapsedParentExtent = %this.getParent().getTrgExtent();
    %this.resizeParentsBy(%delta);
    %this.onResized();
    %this.collapsedParentExtent = %collapsedParentExtent;
    %trg = %this.getTrgPosition();
    %this.reposition(getWord(%trg, 0), getWord(%trg, 1));
};
function TreeBrowserControl::resizeParentsBy(%this, %delta) {
    %extent = %this.getParent().getTrgExtent();
    %newExtent = VectorAdd(%extent @ " " @ 0, %delta @ " " @ 0);
    %newExtent = getWords(%newExtent, 0, 1);
    %this.getParent().resize(getWord(%newExtent, 0), getWord(%newExtent, 1));
};
function TreeBrowserControl::collapseView(%this) {
    if (!(%this.isExpanded)) {
        return;
    }
    %this.isExpanded = 0;
    %delta = VectorSub(%this.collapsedParentExtent @ " " @ 0, %this.getParent().getTrgExtent() @ " " @ 0);
    %this.resizeParentsBy(getWords(%delta, 0, 1));
    %this.onResized();
    %trg = %this.getTrgPosition();
    %this.reposition(getWord(%trg, 0), getWord(%trg, 1));
};
function TreeBrowserControl::isNodeExpanded(%this, %path) {
    return 0;
};
function TreeBrowserControl::fillExpandedFrame(%this, %expandedFrame) {
    %frame = %expandedFrame.getParent();
    %rightEdgeOfMenu = (getWord(%frame.menu.getPosition(), 0) + getWord(%frame.menu.getExtent(), 0));
    0;
    %expandedFrame.add(new ""() {
        position = GuiMLTextCtrl @ %rightEdgeOfMenu @ " " @ 0;
        extent = "50 18";
        text = "<color:ffffff>override me!";
        visible = 1;
    };);
};
function TreeBrowserControl::fillExpandedContentPane(%this, %expandedPane) {
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = (getWord(%frame.contentPane.getPosition(), 0) + getWord(%frame.contentPane.getExtent(), 0));
    0;
    %expandedPane.add(new ""() {
        position = GuiMLTextCtrl @ %rightEdgeOfContentPane @ " " @ 0;
        extent = "50 18";
        text = "<color:ffffff>override me!";
        visible = 1;
    };);
};
function TreeBrowserControl::isInSubdirOfPath(%this, %path) {
    %depth = getFieldCount(%path);
    return (%path $= getFields(%this.Path, %depth));
};
function TreeBrowserControl::fillLeafPane(%this, %pane) {
    %level = %this.level;
    0;
    %pane.add(new ""() {
        profile = GuiTextCtrl @ "GuiTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = getWord(%pane.getExtent(), 0) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = getField(%this.Path, (1.0 - %level));
        maxLength = 255;
    };);
};
function TreeBrowserControl::select(%this, %value) {
    %this.goToPath(%this.Path @ "\t" @ %value);
};
function TreeBrowserControl::selectNextLeaf(%this, %forward, %slide) {
    %path = %this.getNextLeaf(%this.Path, %forward);
    if (!(%path $= "")) {
        if (isDefined("%slide")) {
            %level = getFieldCount(%path);
            if (!(%slide)) {
            }
            if ((%level != getFieldCount(%this.Path))) {
                %this.level = %level;
                %this.reposition((getWord(%this.childrenExtent, 0) * -(%level)), 0);
            }
        }
        %this.goToPath(%path);
    }
};
function TreeBrowserControl::getNextLeaf(%this, %path, %forward) {
    %node = %this.getNode(%path);
    if (!(isObject(%node))) {
        return "";
    }
    if ((0.0 > %node.getCount())) {
        %foundChildBearingNode = 1;
    }
    %foundChildBearingNode = 0;
    if (!(%foundChildBearingNode)) {
        %depth = getFieldCount(%path);
        %name = getField(%path, (1.0 - %depth));
        if ((1.0 <= %depth)) {
            return "";
        }
        %ppath = getFields(%path, 0, (2.0 - %depth));
        %pnode = %this.getNode(%ppath);
        %childCount = %pnode.getCount();
        %nidx = -(1.0);
        %i = 0;
        if ((%childCount < %i)) {
            %child = %pnode.getObject(%i);
            if ((%child.name $= %name)) {
                %nidx = %i;
            }
            %i = (1.0 + %i);
        }
        if ((0.0 < %nidx)) {
            error(getScopeName() @ "->nidx < 0.");
            return "";
        }
        %tidx = (%forward + %nidx);
        if ((0.0 >= %tidx)) {
        }
        if ((%childCount < %tidx)) {
            %node = %pnode.getObject(%tidx);
            %path = %ppath @ "\t" @ %node.name;
            if ((0.0 > %node.getCount())) {
                %foundChildBearingNode = 1;
            }
            return %path;
        }
        %node = %pnode;
        %path = %ppath;
    }
    %cnt = %node.getCount();
    if ((!(%foundChildBearingNode) > 0.0)) {
        if ((0.0 > %forward)) {
        }
        %slot = (1.0 - %cnt);
        0;
        %node = %node.getObject(%slot);
        %path = %path @ "\t" @ %node.name;
        %cnt = %node.getCount();
    }
    return %path;
};
function TreeBrowserControl::addNode(%this, %path) {
    %this.addNodeAt("", %path);
};
function TreeBrowserControl::addNodeAt(%this, %prefix, %subpath) {
    %prefix = trim(%prefix);
    %subpath = trim(%subpath);
    %baseNode = %this.getNode(%prefix);
    if (!(isObject(%baseNode))) {
        return 0;
    }
    %childNodeName = getField(%subpath, 0);
    if ((%childNodeName $= "")) {
        return %baseNode;
    }
    %fullPath = %prefix @ "\t" @ %childNodeName;
    %childNode = %this.nodeDictionary.get(%fullPath);
    if (!(isObject(%childNode))) {
        0;
        %newSet = new ""() {
            name = SimGroup @ %childNodeName;
        };
        %baseNode.add(%newSet);
        %this.nodeDictionary.put(%fullPath, %newSet);
    }
    return %this.addNodeAt(%fullPath, getFields(%subpath, 1));
};
function TreeBrowserControl::getNodePath(%this, %node) {
    %path = "";
    %delim = "";
    if (isObject(%node)) {
    }
    if (!(%node.name $= "")) {
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
    %node = %this.getNode(%path);
    if (!(isObject(%node))) {
        return;
    }
    %this.deleteNode(%node);
    if (%this.isInSubdirOfPath(%path)) {
        %depth = getFieldCount(%path);
        %this.goToPath(getFields(%path, 0, (2.0 - %depth)));
    }
};
function TreeBrowserControl::deleteNode(%this, %node) {
    if (!(isObject(%node))) {
        return;
    }
    %i = (1.0 - %node.getCount());
    if ((0.0 >= %i)) {
        %this.deleteNode(%node.getObject(%i));
        %i = (1.0 - %i);
    }
    %node.delete();
};
function TreeBrowserControl::addMenuData(%this, %prefix, %list) {
    %node = %this.getNode(%prefix);
    if (!(isObject(%node))) {
        return;
    }
    %listCount = getFieldCount(%list);
    %i = 0;
    if ((%listCount < %i)) {
        %itemName = getField(%list, %i);
        if (!(%itemName $= "")) {
            0;
            %node.add(new ""() {
                name = SimGroup @ %itemName;
            };);
        }
        %i = (1.0 + %i);
    }
    %this.goToCurrentPath();
};
function TreeBrowserControl::setDataTree(%this, %tree) {
    if (isObject(%tree)) {
    }
    if (!(%tree.text $= "")) {
        %this.title = %tree.text;
        %this.addMenuData("", %this.title);
        %this.addDataTree(%tree, %this.title);
        %this.goToPath(%this.title);
    }
};
function TreeBrowserControl::addDataTree(%this, %tree, %prefix) {
    %count = %tree.getCount();
    %items = "";
    %i = 0;
    if ((%count < %i)) {
        %obj = %tree.getObject(%i);
        %items = %items @ "\t" @ %obj.text;
        %i = (1.0 + %i);
    }
    %this.addMenuData(%prefix, %items);
    %i = 0;
    (%count < %i);
    if ((%count < %i)) {
        %obj = %tree.getObject(%i);
        %this.addDataTree(%obj, %prefix @ "\t" @ %obj.text);
        %i = (1.0 + %i);
    }
};
function TreeBrowserControl::getNode(%this, %path) {
    %node = %this.nodeDictionary.get(%path);
    if (isObject(%node)) {
        return %node;
    }
    %pathCount = getFieldCount(%path);
    %node = %this.root;
    %i = 0;
    if ((%pathCount < %i)) {
        %dirName = getField(%path, %i);
        if ((%dirName $= "")) {
        }
        %nodeCount = %node.getCount();
        %match = 0;
        %j = 0;
        if ((%nodeCount < %j)) {
            %subNode = %node.getObject(%j);
            if ((%subNode.name $= %dirName)) {
                %node = %subNode;
                %match = 1;
            }
            %j = (1.0 + %j);
        }
        if (!(%match)) {
            return 0;
        }
        %i = (1.0 + %i);
    }
    %this.nodeDictionary.put(%path, %node);
    return %node;
};
function TreeBrowserControl::clear(%this) {
    %this.root.deleteMembers();
};
function TreeBrowserControl::getCurrentNode(%this) {
    return %this.getNode(%this.Path);
};
function TreeBrowserControl::getCurrentFrame(%this) {
    return %this.getObject(getFieldCount(%this.Path));
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
        %menu.makeFirstResponder(1);
    }
    if (%contentPane.isVisibleRecursive()) {
        %contentPane.makeFirstResponder(1);
    }
};
function TreeBrowserFrame::onCreatedChild(%this, %child) {
    Parent::onCreatedChild(%this, %child);
    %child.menuText.reposition(5, 2);
    if (!(getWord(%child.getNamespaceList(), 0) $= "TreeBrowserItem")) {
        %child.bindClassName("TreeBrowserItem");
    }
};
function TreeBrowserFrame::onKeyDown(%this, %unused, %keyCode) {
    if ((%this.getStringFromKeyCode(%keyCode) $= "left")) {
        %this.treeBrowser.goToParentPath();
        return 1;
    }
    if ((%this.getStringFromKeyCode(%keyCode) $= "right")) {
        %this.getHilitedCell().onSelect();
        return 1;
    }
    return 0;
};
function TreeBrowserContentPane::onKeyDown(%this, %unused, %keyCode) {
    if ((%this.getStringFromKeyCode(%keyCode) $= "left")) {
        %this.treeBrowser.goToParentPath();
        return 1;
    }
    if ((%this.getStringFromKeyCode(%keyCode) $= "up")) {
        %this.treeBrowser.selectNextLeaf(-(1.0), 0);
        return 1;
    }
    if ((%this.getStringFromKeyCode(%keyCode) $= "down")) {
        %this.treeBrowser.selectNextLeaf(1, 0);
        return 1;
    }
    return 0;
};
function TreeBrowserControl::makeSomeTreeData() {
    0;
    new ""() {
        text = SimGroup @ "Bar stool";
    };
    new ""() {
        text = SimGroup @ "Orange plush chair";
    };
    new ""() {
        text = SimGroup @ "Chairs";
    };
    new ""() {
        text = SimGroup @ "Seating";
    };
    new ""() {
        text = SimGroup @ "Sofas";
    };
    new ""() {
        text = SimGroup @ "Halogen torchiere";
    };
    new ""() {
        text = SimGroup @ "Track lighting";
    };
    new ""() {
        text = SimGroup @ "Lights";
    };
    %root = new ""() {
        text = SimGroup @ "My Stuff";
    };
    new ""() {
        text = SimGroup @ "Appliances";
    };
    if (isObject(RootGroup)) {
        %root.add();
    }
    return %root;
};
function TreeBrowserControl::test() {
    new GuiControl(BrowserParent) {
        position = "50 50";
        extent = "250 100";
    };
    %rootCtrl = BrowserParent.getContent(Canvas);
    %rootCtrl.add();
    TreeBrowserControl::newControl("TheBrowser");
    1.setNumChildren();
    %data = TreeBrowserControl::makeSomeTreeData();
    TheBrowser;
    %data.setDataTree();
};
function dumpTree(%tree) {
    dumpSubtree(%tree, "");
};
function dumpSubtree(%subtree, %prefix) {
    echo(%prefix @ %subtree.text);
    %count = %subtree.getCount();
    %i = 0;
    if ((%count < %i)) {
        %obj = %subtree.getObject(%i);
        dumpSubtree(%obj, %prefix @ "   ");
        %i = (1.0 + %i);
    }
};
function deleteTree(%tree) {
    %count = %tree.getCount();
    %i = 0;
    if ((%count < %i)) {
        %obj = %tree.getObject(0);
        deleteTree(%obj);
        %i = (1.0 + %i);
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
        if ((%count < %i)) {
            %obj = %tree.getObject(%i);
            %text = %text @ treeToText(%obj);
            %i = (1.0 + %i);
        }
        %text = %text @ "]";
        (%count < %i);
    }
    return %text;
};
