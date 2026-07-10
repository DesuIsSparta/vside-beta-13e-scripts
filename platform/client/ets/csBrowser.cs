function CSBrowser::getHiliteProxy(%this) {
    %ancestor = %this;
    if (isObject(%ancestor)) {
        %ancestor = %ancestor.getParent();
        if ((%ancestor.getClassName() $= "GuiWindowCtrl")) {
            return %ancestor;
        }
    }
    return "";
};
function CSBrowser::goToParentPath(%this) {
    if ((1.0 > getFieldCount(%this.Path))) {
        Parent::goToParentPath(%this);
    }
};
function CSBrowser::getMenuText(%this, %text) {
    return getField(strreplace(%text, "|", "\t"), 0);
};
function CSBrowser::getPathForSku(%this, %sku) {
    %si = SkuManager.findBySku(%sku);
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
    %si = SkuManager.findBySku(%sku);
    if (!(isObject(%si))) {
        return "";
    }
    %name = %si.descShrt;
    if ((%name $= "")) {
        return "";
    }
    %paths = trim(strreplace(%si.drwrName, ";", "\n"));
    %additionalPaths = %this.getAddlPathsForSku(%sku);
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
    if ((%numRecords < %i)) {
        %path = getRecord(%paths, %i);
        %path = trim(strreplace(%path, "/", "\t"));
        %toReturn = %toReturn @ "\n" @ %this.baseDir @ "\t" @ %path @ "\t" @ %name @ "|" @ %sku;
        %i = (1.0 + %i);
    }
    return trim(%toReturn);
};
$CSBrowser::NewFurnishingPath = "New Items";
function CSBrowser::getAddlPathsForSku(%this, %sku) {
    %si = SkuManager.findBySku(%sku);
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
    %paths = %this.getPathsForSku(%sku);
    %numPaths = getRecordCount(%paths);
    %i = 0;
    if ((%numPaths < %i)) {
        %path = getRecord(%paths, %i);
        if (!(%path $= "")) {
            %node = %this.addNode(%path);
            %node.sku = %sku;
        }
        %i = (1.0 + %i);
    }
};
function CSBrowser::removeSku(%this, %sku) {
    %paths = %this.getPathsForSku(%sku);
    %numPaths = getRecordCount(%paths);
    %i = 0;
    if ((%numPaths < %i)) {
        %path = getRecord(%paths, %i);
        if (!(%path $= "")) {
            %this.deleteNodeAtPath(%path);
        }
        %i = (1.0 + %i);
    }
    %this.clearEmptyCategories();
    %this.update();
};
function CSBrowser::navigateToSku(%this, %sku) {
    %path = %this.getPathForSku(%sku);
    if (!(%path $= "")) {
        %this.goToPath(%path, 0);
    }
};
function CSBrowser::clearEmptyCategories(%this) {
    %this.clearEmptyCategoriesAt(%this.getNode(""));
    if (!(isObject(%this.getCurrentNode()))) {
        %this.goToPath(%this.baseDir);
    }
};
function CSBrowser::clearEmptyCategoriesAt(%this, %node) {
    if (!(isObject(%node))) {
        return;
    }
    %numChildren = %node.getCount();
    %i = (1.0 - %numChildren);
    if ((0.0 >= %i)) {
        %this.clearEmptyCategoriesAt(%node.getObject(%i));
        %i = (1.0 - %i);
    }
    if ((0.0 == %node.getCount())) {
    }
    if (((0.0 >= %i) @ " " @ %node.sku $= "")) {
        %this.deleteNode(%node);
    }
};
function CSBrowser::update(%this) {
    %this.goToCurrentPath(0);
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
    if ((%count < %i)) {
        %menuItem = %menu.getChild(0, %i);
        %sku = getSubStr(strchr(%menuItem.name, "|"), 1);
        %this.modifyListViewForSku(%sku, %menuItem);
        if ((%sku $= "")) {
        }
        if ((0.0 >= findRecord($CSBrowser::TopOfListCategories, %menuItem.name))) {
            %menu.reorderChild(%menuItem, %menu.getChild(0, 0));
        }
        %i = (1.0 + %i);
    }
    if ((1.0 == %this.level)) {
    }
    if (!(%this.otherBrowsersVisible())) {
        %this.button.command = (%count < %i) @ %this.getId() @ ".switchToOtherBrowser();" @ 0;
        0.setActive(%this.button, 1);
    }
};
$CSBrowser::NewFurnishingIconBitmap = "platform/client/ui/new_logo_small";
$CSBrowser::FurnishingFolderBitmap = "platform/client/ui/folderIcon";
function CSBrowser::modifyListViewForSku(%this, %sku, %menuItem) {
    %leftIcon = %menuItem.leftIcon;
    %rightIcon = %menuItem.rightIcon;
    if (!(%sku $= "")) {
        %si = SkuManager.findBySku(%sku);
        %rIconBmp = "";
        if ((%si.brand $= "new")) {
            %rIconBmp = $CSBrowser::NewFurnishingIconBitmap;
        }
        if (!(%rIconBmp $= "")) {
            %rightIcon.setBitmap(%rIconBmp);
        }
        %lIconBmp = %this.getThumbnailPathForSku(%sku, 32);
        if (!(%lIconBmp $= "")) {
            %leftIcon.setBitmap(%lIconBmp);
            %menuItem.menuText.reposition(33, getWord(%menuItem.menuText.getPosition(), 1));
        }
    }
    %leftIcon.setBitmap($CSBrowser::FurnishingFolderBitmap);
    %menuItem.menuText.reposition(33, getWord(%menuItem.menuText.getPosition(), 1));
};
function CSBrowser::onCreatedChild(%this, %child, %x, %y) {
    Parent::onCreatedChild(%this, %child, %x, %y);
    %child.menu.bindClassName("CSBrowserFrame");
};
function CSBrowserFrame::onCreatedChild(%this, %child, %x, %y) {
    Parent::onCreatedChild(%this, %child, %x, %y);
    %child.rightIcon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = mMax((24.0 - getWord(%child.getExtent(), 0)), 0) @ " " @ 0;
        extent = "24 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %child.add(%child.rightIcon);
    %child.leftIcon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "24 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %child.add(%child.leftIcon);
};
function CSBrowserNextPrevLink::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %dir = getWord(%url, 1);
    }
    return;
    if ((%dir $= "prev")) {
    }
    %this.browser.selectNextLeaf(-(1.0), 1, 0);
};
function CSBrowser::fillLeafPane(%this, %pane) {
    %desc = getField(%this.Path, (1.0 - %this.level));
    %desc = %this.getMenuText(%desc);
    if ((%desc $= "")) {
    }
    if ((%desc $= %this.baseDir)) {
        return;
    }
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    %sku = getSubStr(strchr(getField(%this.Path, (1.0 - %this.level)), "|"), 1);
    if (!(%sku $= "")) {
        %si = SkuManager.findBySku(%sku);
        if ((%si.brand $= "new")) {
            %desc = %desc @ "\n" @ "<spush><color:ff0000>New!<spop>";
        }
    }
    %itemText = new GuiMLTextCtrl("") {
        profile = 0 @ "H2Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = (5.0 - %paneWidth) @ " " @ 18;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = %desc;
    };
    %pane.add(%itemText);
    %pane.itemText = %itemText;
    %itemText.forceReflow();
    %nextPrevText = new GuiMLTextCtrl("") {
        class = 0 @ "CSBrowserNextPrevLink";
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (30.0 - %paneWidth) @ " " @ (21.0 - getWord(%pane.getExtent(), 1));
        extent = "30 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<linkcolor:e553ff><linkcolorhl:ff93f8><a:gamelink prev><<</a>  <a:gamelink next>>></a>";
    };
    %pane.add(%nextPrevText);
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
    %si = SkuManager.findBySku(%sku);
    if (!(isObject(%si))) {
        return;
    }
    if ((%si.descLong $= "")) {
        return;
    }
    %pathRecords = %this.getPathsForSku(%sku);
    %count = getRecordCount(%pathRecords);
    if ((0.0 == %count)) {
        error(getScopeName() @ "->this CSBrowser doesn't have a path for sku = " @ %sku @ ", which is a presumably valid sku as it is in the SKUManager.");
        return;
    }
    %pathToUse = getRecord(%pathRecords, 0);
    %i = 0;
    if ((%count < %i)) {
        %aPath = getRecord(%pathRecords, %i);
        if ((trim(%aPath) $= %this.Path)) {
            %pathToUse = %aPath;
        }
        %i = (1.0 + %i);
    }
    %this.showMoreInfo = (%count < %i) @ 1;
    %this.goToPath(%pathToUse);
};
function CSBrowser::isNodeExpanded(%this, %path) {
    %sku = getSubStr(strchr(getField(%path, (1.0 - getFieldCount(%path))), "|"), 1);
    if ((%sku $= "")) {
        return 0;
    }
    %si = SkuManager.findBySku(%sku);
    if (!(%si.descLong $= "")) {
    }
    return (1.0 == %this.getFieldValue("showMoreInfo"));
};
function CSBrowser::fillExpandedContentPane(%this, %expandedPane) {
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = (getWord(%frame.contentPane.getPosition(), 0) + getWord(%frame.contentPane.getExtent(), 0));
    %rightEdgeOfContentPane = (10.0 + %rightEdgeOfContentPane);
    %bottomOfItemText = (getWord(%frame.contentPane.itemText.getExtent(), 1) + getWord(%frame.contentPane.itemText.getPosition(), 1));
    %sku = getSubStr(strchr(getField(%this.Path, (1.0 - %this.level)), "|"), 1);
    if ((%sku $= "")) {
        warn(getScopeName() @ "-> couldn't parse sku from path");
        return;
    }
    %si = SkuManager.findBySku(%sku);
    if (!(isObject(%si))) {
        warn(getScopeName() @ "-> couldn't find sku = " @ %sku @ " in skumanager!");
        return;
    }
    %descText = new GuiMLTextCtrl("") {
        position = 0 @ %rightEdgeOfContentPane @ " " @ %bottomOfItemText;
        extent = (5.0 - (%rightEdgeOfContentPane - getWord(%expandedPane.getExtent(), 0))) @ " " @ 18;
        text = "<color:ffffff><spush><color:00ff00>" @ %si.descShrt @ "<spop>\n" @ %si.descLong;
        visible = 1;
    };
    %expandedPane.add(%descText);
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
    %window.setTrgExtent((getWord(%delta, 0) + getWord(%windowExt, 0)), (getWord(%delta, 1) + getWord(%windowExt, 1)));
    Parent::resizeParentsBy(%this, %delta);
};
function CSBrowser::otherBrowsersVisible(%this) {
    if (CSInventoryBrowserWindow.isVisible()) {
    }
    return CSShoppingBrowserWindow.isVisible();
};
