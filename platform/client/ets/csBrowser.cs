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
    if ((%this > getFieldCount(Path))) {
        Parent::goToParentPath(%this);
    }
};
function CSBrowser::getMenuText(%this, %text) {
    return getField(strreplace(%text, "|", "\t"), 0);
};
function CSBrowser::getPathForSku(%this, %sku) {
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return "";
    }
    %name = descShrt;
    %si;
    %firstPath = getField(strreplace(drwrName, ";", "" @ "\t" @ ""), 0);
    %si;
    %path = strreplace(%firstPath, "/", "" @ "\t" @ "");
    if ((%name $= "")) {
    }
    if ((%path $= "")) {
        return "";
    }
    return %this @ baseDir @ "\t" @ %path @ "\t" @ %name @ "|" @ %sku;
};
function CSBrowser::getPathsForSku(%this, %sku) {
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return "";
    }
    %name = descShrt;
    %si;
    if ((%name $= "")) {
        return "";
    }
    %paths = trim(strreplace(drwrName, ";", "\n"));
    %si;
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
        %toReturn = %toReturn @ "\n" @ %this @ baseDir @ "\t" @ %path @ "\t" @ %name @ "|" @ %sku;
        %i = (1.0 + %i);
    }
    return trim(%toReturn);
};
$CSBrowser::NewFurnishingPath = "New Items";
function CSBrowser::getAddlPathsForSku(%this, %sku) {
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    %brand = brand;
    %si;
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
            sku = %sku @ %node;
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
        %this.goToPath(baseDir);
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
    if ((%node SPC sku $= "")) {
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
        %sku = getSubStr(strchr(name, "|"), 1);
        %menuItem;
        %this.modifyListViewForSku(%sku, %menuItem);
        if ((%sku $= "")) {
        }
        if ((%menuItem >= findRecord($CSBrowser::TopOfListCategories, name))) {
            %menu.reorderChild(%menuItem, %menu.getChild(0, 0));
        }
        %i = (1.0 + %i);
        0.0;
    }
    if ((%this == level)) {
    }
    if (!(%this.otherBrowsersVisible())) {
        command = (%count < %i) @ 1.0 @ %this.getId() @ ".switchToOtherBrowser();" @ 0 @ %this @ button;
        button.setActive(1);
    }
};
$CSBrowser::NewFurnishingIconBitmap = "platform/client/ui/new_logo_small";
$CSBrowser::FurnishingFolderBitmap = "platform/client/ui/folderIcon";
function CSBrowser::modifyListViewForSku(%this, %sku, %menuItem) {
    %leftIcon = leftIcon;
    %menuItem;
    %rightIcon = rightIcon;
    %menuItem;
    if (!(%sku $= "")) {
        %si = %sku.findBySku();
        SkuManager;
        %rIconBmp = "";
        if ((%si SPC brand $= "new")) {
            %rIconBmp = $CSBrowser::NewFurnishingIconBitmap;
        }
        if (!(%rIconBmp $= "")) {
            %rightIcon.setBitmap(%rIconBmp);
        }
        %lIconBmp = %this.getThumbnailPathForSku(%sku, 32);
        if (!(%lIconBmp $= "")) {
            %leftIcon.setBitmap(%lIconBmp);
            menuText.reposition(33, getWord(menuText.getPosition(), 1));
        }
    }
    %leftIcon.setBitmap($CSBrowser::FurnishingFolderBitmap);
    menuText.reposition(33, getWord(menuText.getPosition(), 1));
};
function CSBrowser::onCreatedChild(%this, %child, %x, %y) {
    Parent::onCreatedChild(%this, %child, %x, %y);
    menu.bindClassName("CSBrowserFrame");
};
function CSBrowserFrame::onCreatedChild(%this, %child, %x, %y) {
    Parent::onCreatedChild(%this, %child, %x, %y);
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = mMax((24.0 - getWord(%child.getExtent(), 0)), 0) @ " " @ 0;
    extent = "24 24";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    rightIcon = %child;
    %child.add(rightIcon);
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = %child @ "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "24 24";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    leftIcon = %child;
    %child.add(leftIcon);
};
function CSBrowserNextPrevLink::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %dir = getWord(%url, 1);
    }
    return;
    if ((%this SPC %dir $= "prev")) {
    }
    browser.selectNextLeaf(1, 0);
};
function CSBrowser::fillLeafPane(%this, %pane) {
    %desc = getField(Path, (%this - level));
    1.0;
    %desc = %this.getMenuText(%desc);
    %this;
    if ((%desc $= "")) {
    }
    if ((%this $= baseDir)) {
        return %desc;
    }
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    %sku = getSubStr(strchr(getField(Path, (%this - level)), "|"), 1);
    1.0;
    if (!(%this SPC %sku $= "")) {
        %si = %sku.findBySku();
        SkuManager;
        if ((%si SPC brand $= "new")) {
            %desc = %desc @ "\n" @ "<spush><color:ff0000>New!<spop>";
        }
    }
    profile = GuiMLTextCtrl @ new ""() @ "H2Profile";
    0;
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
    %itemText = ;
    %pane.add(%itemText);
    itemText = %itemText @ %pane;
    %itemText.forceReflow();
    class = GuiMLTextCtrl @ new ""() @ "CSBrowserNextPrevLink";
    0;
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
    %nextPrevText = ;
    %pane.add(%nextPrevText);
    nextPrevText = %nextPrevText @ %pane;
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
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    if ((%si SPC descLong $= "")) {
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
        if ((%this $= Path)) {
            %pathToUse = %aPath;
            trim(%aPath);
        }
        %i = (1.0 + %i);
    }
    showMoreInfo = (%count < %i) @ 1 @ %this;
    %this.goToPath(%pathToUse);
};
function CSBrowser::isNodeExpanded(%this, %path) {
    %sku = getSubStr(strchr(getField(%path, (1.0 - getFieldCount(%path))), "|"), 1);
    if ((%sku $= "")) {
        return 0;
    }
    %si = %sku.findBySku();
    SkuManager;
    if (!(%si SPC descLong $= "")) {
    }
    return (1.0 == %this.getFieldValue("showMoreInfo"));
};
function CSBrowser::fillExpandedContentPane(%this, %expandedPane) {
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = (%frame + getWord(contentPane.getExtent(), 0));
    getWord(contentPane.getPosition(), 0);
    %rightEdgeOfContentPane = (10.0 + %rightEdgeOfContentPane);
    %frame;
    %bottomOfItemText = (contentPane + getWord(itemText.getPosition(), 1));
    %frame;
    %sku = getSubStr(strchr(getField(Path, (%this - level)), "|"), 1);
    1.0;
    if ((%this SPC %sku $= "")) {
        warn(getWord(itemText.getExtent(), 1) @ getScopeName() @ "-> couldn't parse sku from path");
        return contentPane;
    }
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        warn(getScopeName() @ "-> couldn't find sku = " @ %sku @ " in skumanager!");
        return;
    }
    position = GuiMLTextCtrl @ new ""() @ %rightEdgeOfContentPane @ " " @ %bottomOfItemText;
    0;
    extent = (5.0 - (%rightEdgeOfContentPane - getWord(%expandedPane.getExtent(), 0))) @ " " @ 18;
    text = "<color:ffffff><spush><color:00ff00>" @ %si @ descShrt @ "<spop>\n" @ %si @ descLong;
    visible = 1;
    %descText = ;
    %expandedPane.add(%descText);
    descText = %descText @ %expandedPane;
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
    if (isVisible()) {
    }
    return isVisible();
};
