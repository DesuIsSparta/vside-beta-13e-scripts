function CSBrowser::getHiliteProxy(%this)
{
    %ancestor = %this;
    while (isObject(%ancestor))
    {
        %ancestor = %ancestor.getParent();
        if (%ancestor.getClassName() $= "GuiWindowCtrl")
        {
            return %ancestor;
        }
    }
    return "";
}
function CSBrowser::goToParentPath(%this)
{
    if (getFieldCount(%this.Path) > 1)
    {
        Parent::goToParentPath(%this);
    }
    return ;
}
function CSBrowser::getMenuText(%this, %text)
{
    return getField(strreplace(%text, "|", "\t"), 0);
}
function CSBrowser::getPathForSku(%this, %sku)
{
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        return "";
    }
    %name = %si.descShrt;
    %firstPath = getField(strreplace(%si.drwrName, ";", "" TAB ""), 0);
    %path = strreplace(%firstPath, "/", "" TAB "");
    if ((%name $= "") && (%path $= ""))
    {
        return "";
    }
    return %this.baseDir TAB %path TAB %name @ "|" @ %sku;
}
function CSBrowser::getPathsForSku(%this, %sku)
{
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        return "";
    }
    %name = %si.descShrt;
    if (%name $= "")
    {
        return "";
    }
    %paths = trim(strreplace(%si.drwrName, ";", "\n"));
    %additionalPaths = %this.getAddlPathsForSku(%sku);
    if (!(%additionalPaths $= ""))
    {
        %paths = %paths NL %additionalPaths;
    }
    %paths = "All Items" NL %paths;
    if (%paths $= "")
    {
        return "";
    }
    %toReturn = "";
    %numRecords = getRecordCount(%paths);
    %i = 0;
    while (%i < %numRecords)
    {
        %path = getRecord(%paths, %i);
        %path = trim(strreplace(%path, "/", "\t"));
        %toReturn = %toReturn NL %this.baseDir TAB %path TAB %name @ "|" @ %sku;
        %i = %i + 1;
    }
    return trim(%toReturn);
}
$CSBrowser::NewFurnishingPath = "New Items";
function CSBrowser::getAddlPathsForSku(%this, %sku)
{
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        return ;
    }
    %brand = %si.brand;
    %path = "";
    if (%brand $= "new")
    {
        %path = $CSBrowser::NewFurnishingPath;
    }
    return %path;
}
function CSBrowser::addSku(%this, %sku)
{
    %paths = %this.getPathsForSku(%sku);
    %numPaths = getRecordCount(%paths);
    %i = 0;
    while (%i < %numPaths)
    {
        %path = getRecord(%paths, %i);
        if (!(%path $= ""))
        {
            %node = %this.addNode(%path);
            %node.sku = %sku;
        }
        %i = %i + 1;
    }
}

function CSBrowser::removeSku(%this, %sku)
{
    %paths = %this.getPathsForSku(%sku);
    %numPaths = getRecordCount(%paths);
    %i = 0;
    while (%i < %numPaths)
    {
        %path = getRecord(%paths, %i);
        if (!(%path $= ""))
        {
            %this.deleteNodeAtPath(%path);
        }
        %i = %i + 1;
    }
    %this.clearEmptyCategories();
    %this.update();
    return ;
}
function CSBrowser::navigateToSku(%this, %sku)
{
    %path = %this.getPathForSku(%sku);
    if (!(%path $= ""))
    {
        %this.goToPath(%path, 0);
    }
    return ;
}
function CSBrowser::clearEmptyCategories(%this)
{
    %this.clearEmptyCategoriesAt(%this.getNode(""));
    if (!isObject(%this.getCurrentNode()))
    {
        %this.goToPath(%this.baseDir);
    }
    return ;
}
function CSBrowser::clearEmptyCategoriesAt(%this, %node)
{
    if (!isObject(%node))
    {
        return ;
    }
    %numChildren = %node.getCount();
    %i = %numChildren - 1;
    while (%i >= 0)
    {
        %this.clearEmptyCategoriesAt(%node.getObject(%i));
        %i = %i - 1;
    }
    if ((%node.getCount() == 0) && (%node.sku $= ""))
    {
        %this.deleteNode(%node);
    }
    return ;
}
function CSBrowser::update(%this)
{
    %this.goToCurrentPath(0);
    return ;
}
$CSBrowser::TopOfListCategories = $CSBrowser::NewFurnishingPath;
function CSBrowser::goToPath(%this, %path, %focus)
{
    if (!isDefined("%focus"))
    {
        %focus = 1;
    }
    Parent::goToPath(%this, %path, %focus);
    %menu = %this.getCurrentMenu();
    %count = %menu.getCount();
    %i = 0;
    while (%i < %count)
    {
        %menuItem = %menu.getChild(0, %i);
        %sku = getSubStr(strchr(%menuItem.name, "|"), 1);
        %this.modifyListViewForSku(%sku, %menuItem);
        if ((%sku $= "") && (findRecord($CSBrowser::TopOfListCategories, %menuItem.name) >= 0))
        {
            %menu.reorderChild(%menuItem, %menu.getChild(0, 0));
        }
        %i = %i + 1;
    }
    if ((%this.level == 1) && !%this.otherBrowsersVisible())
    {
        %this.button[0].command = %this.getId() @ ".switchToOtherBrowser();";
        %this.button[0].setActive(1);
    }
    return ;
}
$CSBrowser::NewFurnishingIconBitmap = "platform/client/ui/new_logo_small";
$CSBrowser::FurnishingFolderBitmap = "platform/client/ui/folderIcon";
function CSBrowser::modifyListViewForSku(%this, %sku, %menuItem)
{
    %leftIcon = %menuItem.leftIcon;
    %rightIcon = %menuItem.rightIcon;
    if (!(%sku $= ""))
    {
        %si = SkuManager.findBySku(%sku);
        %rIconBmp = "";
        if (%si.brand $= "new")
        {
            %rIconBmp = $CSBrowser::NewFurnishingIconBitmap;
        }
        if (!(%rIconBmp $= ""))
        {
            %rightIcon.setBitmap(%rIconBmp);
        }
        %lIconBmp = %this.getThumbnailPathForSku(%sku, 32);
        if (!(%lIconBmp $= ""))
        {
            %leftIcon.setBitmap(%lIconBmp);
            %menuItem.menuText.reposition(33, getWord(%menuItem.menuText.getPosition(), 1));
        }
    }
    else
    {
        %leftIcon.setBitmap($CSBrowser::FurnishingFolderBitmap);
        %menuItem.menuText.reposition(33, getWord(%menuItem.menuText.getPosition(), 1));
    }
    return ;
}
function CSBrowser::onCreatedChild(%this, %child, %x, %y)
{
    Parent::onCreatedChild(%this, %child, %x, %y);
    %child.menu.bindClassName("CSBrowserFrame");
    return ;
}
function CSBrowserFrame::onCreatedChild(%this, %child, %x, %y)
{
    Parent::onCreatedChild(%this, %child, %x, %y);
    %child.rightIcon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = mMax(getWord(%child.getExtent(), 0) - 24, 0) SPC 0;
        extent = "24 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %child.add(%child.rightIcon);
    %child.leftIcon = new GuiBitmapCtrl()
    {
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
    %child.add(%child.leftIcon);
    return ;
}
function CSBrowserNextPrevLink::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "gamelink")
    {
        %dir = getWord(%url, 1);
    }
    else
    {
        return ;
    }
    %this.browser.selectNextLeaf(%dir $= "prev" ? 1 : 1, 0);
    return ;
}
function CSBrowser::fillLeafPane(%this, %pane)
{
    %desc = getField(%this.Path, %this.level - 1);
    %desc = %this.getMenuText(%desc);
    if ((%desc $= "") && (%desc $= %this.baseDir))
    {
        return ;
    }
    %paneWidth = getWord(%pane.getExtent(), 0);
    %paneHeight = getWord(%pane.getExtent(), 1);
    %sku = getSubStr(strchr(getField(%this.Path, %this.level - 1), "|"), 1);
    if (!(%sku $= ""))
    {
        %si = SkuManager.findBySku(%sku);
        if (%si.brand $= "new")
        {
            %desc = %desc NL "<spush><color:ff0000>New!<spop>";
        }
    }
    %itemText = new GuiMLTextCtrl()
    {
        profile = "H2Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = %paneWidth - 5 SPC 18;
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
    %nextPrevText = new GuiMLTextCtrl()
    {
        class = "CSBrowserNextPrevLink";
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %paneWidth - 30 SPC getWord(%pane.getExtent(), 1) - 21;
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
    return ;
}
$CSBrowser::ThumbnailFilename = "projects/common/inventory/[sku]/thumb_[size]x[size]_[sku]";
$CSBrowser::MissingThumbFilename = "platform/client/ui/thumbNotFound_[size]x[size]";
function CSBrowser::getThumbnailPathForSku(%this, %sku, %size)
{
    %fileName = strreplace($CSBrowser::ThumbnailFilename, "[size]", %size);
    %fileName = strreplace(%fileName, "[sku]", %sku);
    return %fileName;
}
function CSBrowser::ShowMoreFor(%this, %sku)
{
    if (%sku $= "")
    {
        return ;
    }
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        return ;
    }
    if (%si.descLong $= "")
    {
        return ;
    }
    %pathRecords = %this.getPathsForSku(%sku);
    %count = getRecordCount(%pathRecords);
    if (%count == 0)
    {
        error(getScopeName() @ "->this CSBrowser doesn\'t have a path for sku = " @ %sku @ ", which is a presumably valid sku as it is in the SKUManager.");
        return ;
    }
    %pathToUse = getRecord(%pathRecords, 0);
    %i = 0;
    while (%i < %count)
    {
        %aPath = getRecord(%pathRecords, %i);
        if (trim(%aPath) $= %this.Path)
        {
            %pathToUse = %aPath;
        }
        %i = %i + 1;
    }
    %this.showMoreInfo = 1;
    %this.goToPath(%pathToUse);
    return ;
}
function CSBrowser::isNodeExpanded(%this, %path)
{
    %sku = getSubStr(strchr(getField(%path, getFieldCount(%path) - 1), "|"), 1);
    if (%sku $= "")
    {
        return 0;
    }
    %si = SkuManager.findBySku(%sku);
    return !((%si.descLong $= "")) && (%this.getFieldValue("showMoreInfo") == 1);
}
function CSBrowser::fillExpandedContentPane(%this, %expandedPane)
{
    %frame = %expandedPane.getParent();
    %rightEdgeOfContentPane = getWord(%frame.contentPane.getExtent(), 0) + getWord(%frame.contentPane.getPosition(), 0);
    %rightEdgeOfContentPane = %rightEdgeOfContentPane + 10;
    %bottomOfItemText = getWord(%frame.contentPane.itemText.getPosition(), 1) + getWord(%frame.contentPane.itemText.getExtent(), 1);
    %sku = getSubStr(strchr(getField(%this.Path, %this.level - 1), "|"), 1);
    if (%sku $= "")
    {
        warn(getScopeName() @ "-> couldn\'t parse sku from path");
        return ;
    }
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        warn(getScopeName() @ "-> couldn\'t find sku = " @ %sku @ " in skumanager!");
        return ;
    }
    %descText = new GuiMLTextCtrl()
    {
        position = %rightEdgeOfContentPane SPC %bottomOfItemText;
        extent = (getWord(%expandedPane.getExtent(), 0) - %rightEdgeOfContentPane) - 5 SPC 18;
        text = "<color:ffffff><spush><color:00ff00>" @ %si.descShrt @ "<spop>\n" @ %si.descLong;
        visible = 1;
    };
    %expandedPane.add(%descText);
    %expandedPane.descText = %descText;
    return ;
}
function CSBrowser::collapseView(%this)
{
    Parent::collapseView(%this);
    return ;
}
function CSBrowser::onResized(%this)
{
    Parent::onResized(%this);
    return ;
}
function CSBrowser::resizeParentsBy(%this, %delta)
{
    %window = %this.getParent().getParent();
    %windowExt = %window.getTrgExtent();
    %window.setTrgExtent(getWord(%windowExt, 0) + getWord(%delta, 0), getWord(%windowExt, 1) + getWord(%delta, 1));
    Parent::resizeParentsBy(%this, %delta);
    return ;
}
function CSBrowser::otherBrowsersVisible(%this)
{
    return CSInventoryBrowserWindow.isVisible() && CSShoppingBrowserWindow.isVisible();
}
