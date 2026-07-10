function CSBrowser::getHiliteProxy(%this) {
    %ancestor = %this;
    while (isObject(%ancestor)) {
        %ancestor = %ancestor.getParent();
        if ((%ancestor.getClassName() $= "GuiWindowCtrl")) {
            return %ancestor;
        }
    }
    return "";
};
function CSBrowser::goToParentPath(%this) {
    if ((getFieldCount(%this.Path) > 1.0)) {
        Parent::goToParentPath(%this);
    }
};
function CSBrowser::getMenuText(%this, %text) {
    return getField(strreplace(%text, "|", "\t"), 0);
};
function CSBrowser::getPathForSku(%this, %sku) {
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return "";
    }
    %name = %si.descShrt;
    %firstPath = getField(strreplace(%si.drwrName, ";", "" @ "\t" @ ""), 0);
    %path = strreplace(%firstPath, "/", "" @ "\t" @ "");
    if ((%name $= "")) {
    }
    if ((%path $= "")) {
        return "";
    }
    return %this.baseDir @ "\t" @ %path @ "\t" @ %name @ "|" @ %sku;
};
function CSBrowser::getPathsForSku(%this, %sku) {
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return "";
    }
    %name = %si.descShrt;
    if ((%name $= "")) {
        return "";
    }
    %paths = trim(strreplace(%si.drwrName, ";", "\n"));
    %additionalPaths = %sku.getAddlPathsForSku(%this);
    if (!(%additionalPaths $= "")) {
        %paths = %paths @ "\n" @ %additionalPaths;
    }
    %paths = "All Items" @ "\n" @ %paths;
    if ((%paths $= "")) {
        return "";
    }
    %toReturn = "";
    %numRecords = getRecordCount(%paths);
    %i = 0;
    while ((%i < %numRecords)) {
        %path = getRecord(%paths, %i);
        %path = trim(strreplace(%path, "/", "\t"));
        %toReturn = %toReturn @ "\n" @ %this.baseDir @ "\t" @ %path @ "\t" @ %name @ "|" @ %sku;
        %i = (%i + 1.0);
    }
    return trim(%toReturn);
};
$CSBrowser::NewFurnishingPath = "New Items";
function CSBrowser::getAddlPathsForSku(%this, %sku) {
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return;
    }
    %brand = %si.brand;
    %path = "";
    if ((%brand $= "new")) {
        %path = $CSBrowser::NewFurnishingPath;
    }
    return %path;
};
function CSBrowser::addSku(%this, %sku) {
    %paths = %sku.getPathsForSku(%this);
    %numPaths = getRecordCount(%paths);
    %i = 0;
    while ((%i < %numPaths)) {
        %path = getRecord(%paths, %i);
        if (!(%path $= "")) {
            %node = %path.addNode(%this);
            %node.sku = %sku;
        }
        %i = (%i + 1.0);
    }
};
function CSBrowser::removeSku(%this, %sku) {
    %paths = %sku.getPathsForSku(%this);
    %numPaths = getRecordCount(%paths);
    %i = 0;
    while ((%i < %numPaths)) {
        %path = getRecord(%paths, %i);
        if (!(%path $= "")) {
            %path.deleteNodeAtPath(%this);
        }
        %i = (%i + 1.0);
    }
    %this.clearEmptyCategories();
    %this.update();
};
function CSBrowser::navigateToSku(%this, %sku) {
    %path = %sku.getPathForSku(%this);
    if (!(%path $= "")) {
        0.goToPath(%this, %path);
    }
};
function CSBrowser::clearEmptyCategories(%this) {
    "".getNode(%this).clearEmptyCategoriesAt(%this);
    if (!(isObject(%this.getCurrentNode()))) {
        %this.baseDir.goToPath(%this);
    }
};
function CSBrowser::clearEmptyCategoriesAt(%this, %node) {
    if (!(isObject(%node))) {
        return;
    }
    %numChildren = %node.getCount();
    %i = (%numChildren - 1.0);
    while ((%i >= 0.0)) {
        %i.getObject(%node).clearEmptyCategoriesAt(%this);
        %i = (%i - 1.0);
    }
    if ((%node.getCount() == 0.0)) {
    }
    if (((%i >= 0.0) @ " " @ %node.sku $= "")) {
        %node.deleteNode(%this);
    }
};
function CSBrowser::update(%this) {
    0.goToCurrentPath(%this);
};
$CSBrowser::TopOfListCategories = $CSBrowser::NewFurnishingPath;
function CSBrowser::goToPath(%this, %path, %focus) {
    if (!(isDefined("%focus"))) {
        %focus = 1;
    }
    Parent::goToPath(%this, %path, %focus);
    %menu = %this.getCurrentMenu();
    %count = %menu.getCount();
    %i = 0;
    while ((%i < %count)) {
        %menuItem = %i.getChild(%menu, 0);
        %sku = getSubStr(strchr(%menuItem.name, "|"), 1);
        %menuItem.modifyListViewForSku(%this, %sku);
        if ((%sku $= "")) {
        }
        if ((findRecord($CSBrowser::TopOfListCategories, %menuItem.name) >= 0.0)) {
            0.getChild(%menu, 0).reorderChild(%menu, %menuItem);
        }
        %i = (%i + 1.0);
    }
    if ((%this.level == 1.0)) {
    }
    if (!(%this.otherBrowsersVisible())) {
        %this.button.command = (%i < %count) @ %this.getId() @ ".switchToOtherBrowser();" @ 0;
        1.setActive(0, %this.button);
    }
};
$CSBrowser::NewFurnishingIconBitmap = "platform/client/ui/new_logo_small";
$CSBrowser::FurnishingFolderBitmap = "platform/client/ui/folderIcon";
function CSBrowser::modifyListViewForSku(%this, %sku, %menuItem) {
    %leftIcon = %menuItem.leftIcon;
    %rightIcon = %menuItem.rightIcon;
    if (!(%sku $= "")) {
        %si = %sku.findBySku(SkuManager);
        %rIconBmp = "";
        if ((%si.brand $= "new")) {
            %rIconBmp = $CSBrowser::NewFurnishingIconBitmap;
        }
        if (!(%rIconBmp $= "")) {
            %rIconBmp.setBitmap(%rightIcon);
        }
        %lIconBmp = 32.getThumbnailPathForSku(%this, %sku);
        if (!(%lIconBmp $= "")) {
            %lIconBmp.setBitmap(%leftIcon);
            getWord(%menuItem.menuText.getPosition(), 1).reposition(%menuItem.menuText, 33);
        }
    }
    $CSBrowser::FurnishingFolderBitmap.setBitmap(%leftIcon);
    getWord(%menuItem.menuText.getPosition(), 1).reposition(%menuItem.menuText, 33);
};
function CSBrowser::onCreatedChild(%this, %child, %x, %y) {
    Parent::onCreatedChild(%this, %child, %x, %y);
    "CSBrowserFrame".bindClassName(%child.menu);
};
function CSBrowserFrame::onCreatedChild(%this, %child, %x, %y) {
    Parent::onCreatedChild(%this, %child, %x, %y);
    %child.rightIcon = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = mMax((getWord(%child.getExtent(), 0) - 24.0), 0) @ " " @ 0;
        extent = "24 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %child.rightIcon.add(%child);
    %child.leftIcon = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "24 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %child.leftIcon.add(%child);
};
function CSBrowserNextPrevLink::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %dir = getWord(%url, 1);
    }
    return;
    if ((%dir $= "prev")) {
    }
    0.selectNextLeaf(%this.browser, -(1.0), 1);
};
function CSBrowser::fillLeafPane(%this, %pane) {
    %desc = getField(%this.Path, (%this.level - 1.0));
    %desc = %desc.getMenuText(%this);
    if ((%desc $= "")) {
    }
    if ((%desc $= %this.baseDir)) {
        return;
    }
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    %sku = getSubStr(strchr(getField(%this.Path, (%this.level - 1.0)), "|"), 1);
    if (!(%sku $= "")) {
        %si = %sku.findBySku(SkuManager);
        if ((%si.brand $= "new")) {
            %desc = %desc @ "\n" @ "<spush><color:ff0000>New!<spop>";
        }
    }
    %itemText = new GuiMLTextCtrl("") {
        profile = "H2Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = (%paneWidth - 5.0) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = %desc;
    };
    %itemText.add(%pane);
    %pane.itemText = %itemText;
    %itemText.forceReflow();
    %nextPrevText = new GuiMLTextCtrl("") {
        class = "CSBrowserNextPrevLink";
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%paneWidth - 30.0) @ " " @ (getWord(%pane.getExtent(), 1) - 21.0);
        extent = "30 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<linkcolor:e553ff><linkcolorhl:ff93f8><a:gamelink prev><<</a>  <a:gamelink next>>></a>";
    };
    %nextPrevText.add(%pane);
    %pane.nextPrevText = %nextPrevText;
};
$CSBrowser::ThumbnailFilename = "projects/common/inventory/[sku]/thumb_[size]x[size]_[sku]";
$CSBrowser::MissingThumbFilename = "platform/client/ui/thumbNotFound_[size]x[size]";
function CSBrowser::getThumbnailPathForSku(%this, %sku, %size) {
    %fileName = strreplace($CSBrowser::ThumbnailFilename, "[size]", %size);
    %fileName = strreplace(%fileName, "[sku]", %sku);
    return %fileName;
};
function CSBrowser::ShowMoreFor(%this, %sku) {
    if ((%sku $= "")) {
        return;
    }
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return;
    }
    if ((%si.descLong $= "")) {
        return;
    }
    %pathRecords = %sku.getPathsForSku(%this);
    %count = getRecordCount(%pathRecords);
    if ((%count == 0.0)) {
        error(getScopeName() @ "->this CSBrowser doesn't have a path for sku = " @ %sku @ ", which is a presumably valid sku as it is in the SKUManager.");
        return;
    }
    %pathToUse = getRecord(%pathRecords, 0);
    %i = 0;
    while ((%i < %count)) {
        %aPath = getRecord(%pathRecords, %i);
        if ((trim(%aPath) $= %this.Path)) {
            %pathToUse = %aPath;
        }
        %i = (%i + 1.0);
    }
    %this.showMoreInfo = (%i < %count) @ 1;
    %pathToUse.goToPath(%this);
};
function CSBrowser::isNodeExpanded(%this, %path) {
    %sku = getSubStr(strchr(getField(%path, (getFieldCount(%path) - 1.0)), "|"), 1);
    if ((%sku $= "")) {
        return 0;
    }
    %si = %sku.findBySku(SkuManager);
    if (!(%si.descLong $= "")) {
    }
    return ("showMoreInfo".getFieldValue(%this) == 1.0);
};
function CSBrowser::fillExpandedContentPane(%this, %expandedPane) {
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = (getWord(%frame.contentPane.getExtent(), 0) + getWord(%frame.contentPane.getPosition(), 0));
    %rightEdgeOfContentPane = (%rightEdgeOfContentPane + 10.0);
    %bottomOfItemText = (getWord(%frame.contentPane.itemText.getPosition(), 1) + getWord(%frame.contentPane.itemText.getExtent(), 1));
    %sku = getSubStr(strchr(getField(%this.Path, (%this.level - 1.0)), "|"), 1);
    if ((%sku $= "")) {
        warn(getScopeName() @ "-> couldn't parse sku from path");
        return;
    }
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        warn(getScopeName() @ "-> couldn't find sku = " @ %sku @ " in skumanager!");
        return;
    }
    %descText = new GuiMLTextCtrl("") {
        position = %rightEdgeOfContentPane @ " " @ %bottomOfItemText;
        extent = ((getWord(%expandedPane.getExtent(), 0) - %rightEdgeOfContentPane) - 5.0) @ " " @ 18;
        text = "<color:ffffff><spush><color:00ff00>" @ %si.descShrt @ "<spop>\n" @ %si.descLong;
        visible = 1;
    };
    %descText.add(%expandedPane);
    %expandedPane.descText = %descText;
};
function CSBrowser::collapseView(%this) {
    Parent::collapseView(%this);
};
function CSBrowser::onResized(%this) {
    Parent::onResized(%this);
};
function CSBrowser::resizeParentsBy(%this, %delta) {
    %window = %this.getParent().getParent();
    %windowExt = %window.getTrgExtent();
    (getWord(%windowExt, 1) + getWord(%delta, 1)).setTrgExtent(%window, (getWord(%windowExt, 0) + getWord(%delta, 0)));
    Parent::resizeParentsBy(%this, %delta);
};
function CSBrowser::otherBrowsersVisible(%this) {
    if (CSInventoryBrowserWindow.isVisible()) {
    }
    return CSShoppingBrowserWindow.isVisible();
};
