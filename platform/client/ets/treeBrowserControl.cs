function TreeBrowserControl::newControl(%parent, %name) {
    if (!(isObject(%parent))) {
        return;
    }
    profile = GuiArray2Ctrl @ new ""() @ "GuiDefaultProfile";
    0;
    position = "0 0";
    extent = %parent.getExtent();
    childrenClassName = "GuiControl";
    childrenExtent = %parent.getExtent();
    spacing = 0;
    numRowsOrCols = 1;
    inRows = 1;
    sluggishness = 0.5;
    %ctrl = ;
    %ctrl.bindClassName("TreeBrowserControl");
    %ctrl.bindClassName(%name);
    %ctrl.setName(%name);
    %parent.add(%ctrl);
    Parent = %parent @ %ctrl;
    idCounter = 0 @ %ctrl;
    level = 0 @ %ctrl;
    numButtons = 0 @ %ctrl;
    buttonWidth = 20 @ %ctrl;
    buttonPadding = 1 @ %ctrl;
    title = "" @ %ctrl;
    menuProfile = "ETSMenuProfile" @ %ctrl;
    selectedProfile = "ETSSelectedMenuItemProfile" @ %ctrl;
    menuTextProfile = "ETSUnselectedMenuTextProfile" @ %ctrl;
    menuTextSelectedProfile = "ETSSelectedMenuTextProfile" @ %ctrl;
    adjustMenuCellHeight = 0 @ %ctrl;
    isExpanded = 0 @ %ctrl;
    expandDelta = "250 0" @ %ctrl;
    name = SimGroup @ new ""() @ "";
    0;
    root = %ctrl;
    nodeDictionary = safeNewScriptObject("StringMap", "", 0) @ %ctrl;
    if (isObject()) {
        root.add();
    }
    Path = %ctrl @ "" @ %ctrl;
    RootGroup;
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
    if (isExpanded) {
        collapsedParentExtent = %this @ getWords(VectorSub(%parentExtent @ " " @ 0, expandDelta @ " " @ 0), 0, 1) @ %this;
        %this;
    }
    childrenExtent = %parentExtent @ %this;
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
    %leftPadding = (buttonPadding * (%this + buttonWidth));
    %this;
    if (isExpanded) {
    }
    %contentsExtentX = (getWord(collapsedParentExtent, 0) - getWord(%child.getExtent(), 0));
    %this;
    if (isExpanded) {
    }
    %contentsExtentY = getWord(%child.getExtent(), 1);
    getWord(collapsedParentExtent, 1);
    profile = GuiControl @ new ""() @ "FocusableDefaultProfile";
    0;
    horizSizing = %this @ %this @ "width";
    %this;
    vertSizing = %x @ %leftPadding @ "height";
    position = "0 0";
    extent = %child.getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    hiliteProxy = %this.getHiliteProxy();
    treeBrowser = %this;
    expandedPane = %child;
    %child.add(expandedPane);
    profile = GuiControl @ new ""() @ "FocusableDefaultProfile";
    0;
    horizSizing = %child @ "right";
    vertSizing = "bottom";
    position = %this @ buttonPadding @ (%this - (buttonWidth - %leftPadding)) @ " " @ 0;
    extent = %this @ buttonPadding @ (%this + (buttonWidth + %contentsExtentX)) @ " " @ %contentsExtentY;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    hiliteProxy = %this.getHiliteProxy();
    treeBrowser = %this;
    contentPane = %child;
    contentPane.bindClassName("TreeBrowserContentPane");
    %child.add(contentPane);
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
    horizSizing = %child @ %child @ "right";
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
    scroll = %child;
    %menuCellSpacing = 2;
    %menuTrgCellHeight = 24;
    if (adjustMenuCellHeight) {
        %numCanFit = mFloor(((%menuCellSpacing + %menuTrgCellHeight) / %contentsExtentY));
        %this;
        %menuTrgCellHeight = (%menuCellSpacing - (%numCanFit / %contentsExtentY));
        %d = (mFloor(%menuTrgCellHeight) - %menuTrgCellHeight);
        if ((0.5 >= %d)) {
            %menuTrgCellHeight = mCeil(%menuTrgCellHeight);
        }
        %menuTrgCellHeight = mFloor(%menuTrgCellHeight);
    }
    profile = new ""() @ %this @ menuProfile;
    GuiArray2Ctrl;
    childrenClassName = 0 @ "GuiMouseEventCtrl";
    childrenExtent = (6.0 - %contentsExtentX) @ " " @ %menuTrgCellHeight;
    spacing = %menuCellSpacing;
    numRowsOrCols = 1;
    inRows = 0;
    hiliteProxy = %this.getHiliteProxy();
    hilited = 0;
    unselectedProfile = "GuiDefaultProfile";
    selectedProfile = %this @ selectedProfile;
    menuTextProfile = %this @ menuTextProfile;
    menuTextSelectedProfile = %this @ menuTextSelectedProfile;
    treeBrowser = %this;
    menu = %child;
    menu.bindClassName("MenuControl");
    menu.bindClassName("TreeBrowserFrame");
    layer = %child @ menu;
    %child @ 0;
    scroll.add(menu);
    scroll = %child @ menu;
    %child @ scroll;
    %child.add(scroll);
};
function TreeBrowserControl::getHiliteProxy(%this) {
    return "";
};
function TreeBrowserControl::scrollToLevel(%this, %level) {
    level = %level @ %this;
    %this.setTrgPosition((getWord(childrenExtent, 0) * -(%level)), 0);
};
function TreeBrowserControl::goToCurrentPath(%this, %focus) {
    if (!(isDefined("%focus"))) {
        %focus = 1;
    }
    %this.goToPath(Path, %focus);
};
function TreeBrowserControl::goToParentPath(%this) {
    %currentNodeName = name;
    %this.getNode(Path);
    %parentPath = getFields(Path, 0, (%this - getFieldCount(Path)));
    2.0;
    %this.goToPath(%parentPath);
    %menu = %this.getCurrentMenu();
    %this;
    %count = %menu.getCount();
    %this;
    %i = 0;
    if ((%count < %i)) {
        %menuItem = %menu.getObject(%i);
        if ((%menuItem SPC name $= %currentNodeName)) {
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
    %pathchanged = !(%this SPC Path $= %path);
    Path = %path @ %this;
    %oldLevel = level;
    %this;
    level = getFieldCount(%path) @ %this;
    %level = ;
    if ((%level <= %this.getCount())) {
        %this.setNumChildren((1.0 + %level));
    }
    %this.scrollToLevel(%level);
    %leafNode = 0;
    %expanded = %this.isNodeExpanded(Path);
    %this;
    if (isExpanded) {
    }
    if ((%oldLevel != %level)) {
        %oldChild = %this.getChild(%oldLevel, 0);
        %this;
        expandedPane.clear();
    }
    if (%expanded) {
    }
    if (!(isExpanded)) {
        %expandDelta = %this.getFieldValue("expandDelta");
        %this;
        if ((%oldChild SPC %expandDelta $= "")) {
            warn(getScopeName() @ "->trying to expand view but no expandDelta is set. returning!");
        }
        %this.expandView(%expandDelta);
        return;
    }
    if (isExpanded) {
    }
    if (!(%expanded)) {
        %this.collapseView();
        %this.focusCurrentFrame();
        return %this;
    }
    %child = %this.getChild(%level, 0);
    %count = %node.getCount();
    if ((0.0 == %count)) {
        contentPane.setVisible(1);
        scroll.setVisible(0);
        menu.setVisible(0);
        %leafNode = 1;
        %child;
        node = %child @ contentPane;
        %child @ %node;
        contentPane.clear();
        %this.fillLeafPane(contentPane);
        if (%expanded) {
        }
        if (isExpanded) {
            expandedPane.clear();
            expandedPane.setVisible(1);
            %this.fillExpandedContentPane(expandedPane);
        }
        expandedPane.setVisible(0);
        if (%focus) {
        }
        if (%this.isVisibleRecursive()) {
        }
        if (%pathchanged) {
            contentPane.makeFirstResponder(1);
        }
    }
    contentPane.setVisible(0);
    scroll.setVisible(1);
    menu.setVisible(1);
    if (%expanded) {
    }
    if (isExpanded) {
        expandedPane.clear();
        expandedPane.setVisible(1);
        %this.fillExpandedFrame(expandedPane);
    }
    expandedPane.setVisible(0);
    %currentCount = menu.getCount();
    %child;
    filterText = %this @ strlwr(filterText) @ %this;
    %child;
    filterText = %this @ trim(filterText) @ %this;
    %child;
    %count = 0;
    %child;
    %n = (1.0 - %node.getCount());
    %child;
    if ((0.0 >= %n)) {
        %subNode = %node.getObject(%n);
        %this;
        passesFilter = %this @ %this.nodePassesFilter(%subNode, filterText) @ %subNode;
        %child;
        if (passesFilter) {
            %count = (1.0 + %count);
            %subNode;
        }
        %n = (1.0 - %n);
        %child;
    }
    if ((%count != %currentCount)) {
        Path = (0.0 >= %n) @ "ForceUpdatePlease!!!" @ %child;
        %child;
    }
    if (!(%child SPC Path $= %path)) {
        menu.clear();
        deferReseat = %child @ menu;
        %child @ 1;
        %totalCount = %node.getCount();
        %child;
        %n = 0;
        %child;
        if ((%totalCount < %n)) {
            %subNode = %node.getObject(%n);
            %child;
            if (passesFilter) {
                %menuItem = menu.addMenuItem(%this.getMenuText(name), %child @ %subNode @ %this.getId() @ ".select(\"" @ %subNode @ name @ "\");", "", "");
                %subNode;
                name = %subNode @ name @ %menuItem;
                %child;
            }
            %n = (1.0 + %n);
            %child;
        }
        menu.reseatChildren();
        menu.hiliteCell(0, 0);
    }
    if (%focus) {
    }
    if (%this.isVisibleRecursive()) {
    }
    if (%pathchanged) {
        menu.makeFirstResponder(1);
    }
    Path = %child @ %path @ %child;
    %child;
    if (%leafNode) {
    }
    %numButtons = %level;
    (1.0 - %level);
    %offset = 0;
    %child;
    if (isExpanded) {
    }
    %height = getWord(%this.getExtent(), 1);
    getWord(collapsedParentExtent, 1);
    %i = 0;
    %this;
    if ((mMax(%numButtons, numButtons) < %i)) {
        if ((%numButtons < %i)) {
            if (!(isObject(button))) {
                profile = GuiBitmapButtonCtrl @ new ""() @ "ETSVerticalButtonProfile";
                0;
                horizSizing = %this @ %this @ %i @ %this @ "right";
                (%totalCount < %n);
                vertSizing = %child @ %this @ "bottom";
                %child;
                position = %child @ %offset @ " " @ 0;
                extent = %this @ buttonWidth @ " " @ %height;
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
                button = %i @ %this;
                Parent.add(button);
            }
            if (!(%this $= buttonWidth @ " " @ %height)) {
                button.resize(buttonWidth, %height);
            }
            %button = button;
            %this @ %i @ %this;
            %button.setVisible(1);
            command = %this @ %i @ %this @ %i @ %this SPC button.getExtent() @ %i @ %this @ %this.getId() @ ".goToPath(\"" @ %this @ getFields(Path, 0, %i) @ "\");" @ %button;
            text = %this @ getField(Path, %i) @ %button;
            %button.setActive(((1.0 - %level) < %i));
        }
        button.setVisible(0);
        %offset = ((%this + buttonWidth) + %offset);
        buttonPadding;
        %i = (1.0 + %i);
        %this;
    }
    numButtons = (mMax(%numButtons, numButtons) < %i) @ %numButtons @ %this;
    %this;
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
    if (!(%node SPC searchText $= "")) {
        return searchText;
    }
    %sku = sku;
    %node;
    if (!(%sku $= "")) {
        %ret = searchText;
        %sku.findBySku();
    }
    %ret = name;
    %node;
    %n = (1.0 - %node.getCount());
    SkuManager;
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
    searchText = %ret @ %node;
    return %ret;
};
function TreeBrowserControl::expandView(%this, %delta) {
    if (isExpanded) {
        return %this;
    }
    isExpanded = 1 @ %this;
    %collapsedParentExtent = %this.getParent().getTrgExtent();
    %this.resizeParentsBy(%delta);
    %this.onResized();
    collapsedParentExtent = %collapsedParentExtent @ %this;
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
    if (!(isExpanded)) {
        return %this;
    }
    isExpanded = 0 @ %this;
    %delta = VectorSub(collapsedParentExtent @ " " @ 0, %this.getParent().getTrgExtent() @ " " @ 0);
    %this;
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
    %rightEdgeOfMenu = (%frame + getWord(menu.getExtent(), 0));
    getWord(menu.getPosition(), 0);
    position = GuiMLTextCtrl @ new ""() @ %rightEdgeOfMenu @ " " @ 0;
    0;
    extent = %frame @ "50 18";
    text = "<color:ffffff>override me!";
    visible = 1;
    %expandedFrame.add();
};
function TreeBrowserControl::fillExpandedContentPane(%this, %expandedPane) {
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = (%frame + getWord(contentPane.getExtent(), 0));
    getWord(contentPane.getPosition(), 0);
    position = GuiMLTextCtrl @ new ""() @ %rightEdgeOfContentPane @ " " @ 0;
    0;
    extent = %frame @ "50 18";
    text = "<color:ffffff>override me!";
    visible = 1;
    %expandedPane.add();
};
function TreeBrowserControl::isInSubdirOfPath(%this, %path) {
    %depth = getFieldCount(%path);
    return (%this $= getFields(Path, %depth));
};
function TreeBrowserControl::fillLeafPane(%this, %pane) {
    %level = level;
    %this;
    profile = GuiTextCtrl @ new ""() @ "GuiTextProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "5 0";
    extent = getWord(%pane.getExtent(), 0) @ " " @ 18;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = %this @ getField(Path, (1.0 - %level));
    maxLength = 255;
    %pane.add();
};
function TreeBrowserControl::select(%this, %value) {
    %this.goToPath(Path @ "\t" @ %value);
};
function TreeBrowserControl::selectNextLeaf(%this, %forward, %slide) {
    %path = %this.getNextLeaf(Path, %forward);
    %this;
    if (!(%path $= "")) {
        if (isDefined("%slide")) {
            %level = getFieldCount(%path);
            if (!(%slide)) {
            }
            if ((%this != getFieldCount(Path))) {
                level = %level @ %level @ %this;
                %this.reposition((getWord(childrenExtent, 0) * -(%level)), 0);
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
            if ((%child SPC name $= %name)) {
                %nidx = %i;
            }
            %i = (1.0 + %i);
        }
        if ((0.0 < %nidx)) {
            error((%childCount < %i) @ getScopeName() @ "->nidx < 0.");
            return "";
        }
        %tidx = (%forward + %nidx);
        if ((0.0 >= %tidx)) {
        }
        if ((%childCount < %tidx)) {
            %node = %pnode.getObject(%tidx);
            %path = %node @ name;
            %ppath @ "\t";
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
        %path = %node @ name;
        %path @ "\t";
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
    %childNode = nodeDictionary.get(%fullPath);
    %this;
    if (!(isObject(%childNode))) {
        name = SimGroup @ new ""() @ %childNodeName;
        0;
        %newSet = ;
        %baseNode.add(%newSet);
        nodeDictionary.put(%fullPath, %newSet);
    }
    return %this.addNodeAt(%fullPath, getFields(%subpath, 1));
};
function TreeBrowserControl::getNodePath(%this, %node) {
    %path = "";
    %delim = "";
    if (isObject(%node)) {
    }
    if (!(%node SPC name $= "")) {
        %path = %node @ name @ %delim @ %path;
        %delim = "\t";
        if ((%this $= root.getId())) {
            %node = "";
            %node.getId();
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
            name = SimGroup @ new ""() @ %itemName;
            0;
            %node.add();
        }
        %i = (1.0 + %i);
    }
    %this.goToCurrentPath();
};
function TreeBrowserControl::setDataTree(%this, %tree) {
    if (isObject(%tree)) {
    }
    if (!(%tree SPC text $= "")) {
        title = %tree @ text @ %this;
        %this.addMenuData("", title);
        %this.addDataTree(%tree, title);
        %this.goToPath(title);
    }
};
function TreeBrowserControl::addDataTree(%this, %tree, %prefix) {
    %count = %tree.getCount();
    %items = "";
    %i = 0;
    if ((%count < %i)) {
        %obj = %tree.getObject(%i);
        %items = %obj @ text;
        %items @ "\t";
        %i = (1.0 + %i);
    }
    %this.addMenuData(%prefix, %items);
    %i = 0;
    (%count < %i);
    if ((%count < %i)) {
        %obj = %tree.getObject(%i);
        %this.addDataTree(%obj, %obj @ text);
        %i = (1.0 + %i);
        %prefix @ "\t";
    }
};
function TreeBrowserControl::getNode(%this, %path) {
    %node = nodeDictionary.get(%path);
    %this;
    if (isObject(%node)) {
        return %node;
    }
    %pathCount = getFieldCount(%path);
    %node = root;
    %this;
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
            if ((%subNode SPC name $= %dirName)) {
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
    nodeDictionary.put(%path, %node);
    return %node;
};
function TreeBrowserControl::clear(%this) {
    root.deleteMembers();
};
function TreeBrowserControl::getCurrentNode(%this) {
    return %this.getNode(Path);
};
function TreeBrowserControl::getCurrentFrame(%this) {
    return %this.getObject(getFieldCount(Path));
};
function TreeBrowserControl::getCurrentMenu(%this) {
    %frame = %this.getCurrentFrame();
    return menu;
};
function TreeBrowserControl::getCurrentContentPane(%this) {
    %frame = %this.getCurrentFrame();
    return contentPane;
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
    menuText.reposition(5, 2);
    if (!(%child SPC getWord(%child.getNamespaceList(), 0) $= "TreeBrowserItem")) {
        %child.bindClassName("TreeBrowserItem");
    }
};
function TreeBrowserFrame::onKeyDown(%this, %unused, %keyCode) {
    if ((%this.getStringFromKeyCode(%keyCode) $= "left")) {
        treeBrowser.goToParentPath();
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
        treeBrowser.goToParentPath();
        return 1;
    }
    if ((%this.getStringFromKeyCode(%keyCode) $= "up")) {
        treeBrowser.selectNextLeaf(-(1.0), 0);
        return 1;
    }
    if ((%this.getStringFromKeyCode(%keyCode) $= "down")) {
        treeBrowser.selectNextLeaf(1, 0);
        return 1;
    }
    return 0;
};
function TreeBrowserControl::makeSomeTreeData() {
    text = SimGroup @ new ""() @ "My Stuff";
    0;
    text = SimGroup @ new ""() @ "Seating";
    text = SimGroup @ new ""() @ "Chairs";
    text = SimGroup @ new ""() @ "Bar stool";
    text = SimGroup @ new ""() @ "Orange plush chair";
    text = SimGroup @ new ""() @ "Zen pillow";
    text = SimGroup @ new ""() @ "Sofas";
    text = SimGroup @ new ""() @ "Lights";
    text = SimGroup @ new ""() @ "Halogen torchiere";
    text = SimGroup @ new ""() @ "Track lighting";
    text = SimGroup @ new ""() @ "Glow in the dark stars";
    text = SimGroup @ new ""() @ "Appliances";
    %root = ;
    if (isObject()) {
        %root.add();
    }
    return %root;
};
function TreeBrowserControl::test() {
    position = new GuiControl(BrowserParent) @ "50 50";
    extent = "250 100";
    %rootCtrl = getContent();
    Canvas;
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
    echo(%subtree @ text);
    %count = %subtree.getCount();
    %prefix;
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
        %text = "[\"" @ %tree @ text @ "\"";
        %count = %tree.getCount();
        %i = 0;
        if ((%count < %i)) {
            %obj = %tree.getObject(%i);
            %text = %text @ treeToText(%obj);
            %i = (1.0 + %i);
        }
        %text = (%count < %i) @ %text @ "]";
    }
    return %text;
};
