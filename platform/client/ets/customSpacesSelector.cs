filterByText = "" @ CustomSpacesSelector;
$gCustomSpacesDoubleClickSched = 0;
$gCustomSpacesKeyStrokeRepeatSched = 0;
$gCustomSpacesKeyStrokeRepFrequency = 120;
vars = "MODEL" @ "\t" @ "FEATURED" @ "\t" @ "CELEBSPACE" @ "\t" @ "MYPLACE" @ "\t" @ "RESIDENCE" @ "aptCategories" @ CustomSpacesSelector;
vars = "" @ "aptCategoriesInUse" @ CustomSpacesSelector;
vars = 0 @ "aptCategoryCount" @ CustomSpacesSelector;
vars = 200 @ "scrollBarHeight" @ CustomSpacesSelector;
vars = 13 @ "scrollBarWidth" @ CustomSpacesSelector;
vars = 11 @ "headerboxHeight" @ CustomSpacesSelector;
vars = 0 @ "textTitleLeft" @ CustomSpacesSelector;
vars = 1 @ "textTitleTop" @ CustomSpacesSelector;
vars = 0 @ "spaceBetweenHeaderAndListBoxes" @ CustomSpacesSelector;
vars = 5 @ "textAreaIndentation" @ CustomSpacesSelector;
vars = 2 @ "textAreaPadding" @ CustomSpacesSelector;
vars = 470 @ "textAreaWidth" @ CustomSpacesSelector;
vars = 10 @ "textAreaInterCategoryPadding" @ CustomSpacesSelector;
vars = 5 @ "spacesColumnPadding" @ CustomSpacesSelector;
vars = 0 @ "spacesRowPadding" @ CustomSpacesSelector;
function CSSelectorCtrl::makeNewListBox(%type) {
    %windowWidth = getWord(CustomSpacesSelector.getExtent(), 0);
    0;
    vars;
    %listBox = new ""() {
        profile = GuiArray2Ctrl @ "CSProfileListBox";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "16 16";
        spacing = "spacesRowPadding" @ CustomSpacesSelector @ vars;
        numRowsOrCols = 1;
        inRows = 0;
        hilited = 0;
        lastClicked = 0;
        paddingAboveText = 1;
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = ((750.0 - %windowWidth) - ("textAreaWidth" @ CustomSpacesSelector + vars)) @ " " @ "headerboxHeight" @ CustomSpacesSelector @ vars;
        minExtent = "textAreaIndentation" @ CustomSpacesSelector @ "1 1";
        sluggishness = -1;
        visible = 1;
        scroll = "CSSelectorScrollCtrl";
    };
    %listBox.bindClassName("MenuControl");
    %listBox.bindClassName("TabbedTextControl");
    %listBox.bindClassName("CSSelectorCtrl");
    %fieldWidths = "90 16" @ " " @ ((750.0 - %windowWidth) + 197.0) @ " " @ "25 0 68";
    %listBox.descriptionFieldNumber = 2;
    if ((%type $= "MODEL")) {
        %listBox.unselectedProfile = "CSProfileModelListingUnselected";
        %listBox.selectedProfile = "CSProfileModelListingSelected";
        %listBox.menuTextProfile = "CSProfileModelListingMenuText";
        %listBox.menuTextSelectedProfile = "CSProfileModelListingMenuTextSelected";
        %fieldWidths = 111 @ " " @ ((750.0 - %windowWidth) + 227.0) @ " " @ "0 68";
        %listBox.descriptionFieldNumber = 1;
    }
    if ((%type $= "FEATURED")) {
        %listBox.unselectedProfile = "CSProfileFeaturedListingUnselected";
        %listBox.selectedProfile = "CSProfileFeaturedListingSelected";
        %listBox.menuTextProfile = "CSProfileFeaturedListingMenuText";
        %listBox.menuTextSelectedProfile = "CSProfileFeaturedListingMenuTextSelected";
    }
    if ((%type $= "CELEBSPACE")) {
        %listBox.unselectedProfile = "CSProfileCelebListingUnselected";
        %listBox.selectedProfile = "CSProfileCelebListingSelected";
        %listBox.menuTextProfile = "CSProfileCelebListingMenuText";
        %listBox.menuTextSelectedProfile = "CSProfileCelebListingMenuTextSelected";
    }
    if ((%type $= "MYPLACE")) {
        %listBox.unselectedProfile = "CSProfileNormalListingUnselected";
        %listBox.selectedProfile = "CSProfileNormalListingSelected";
        %listBox.menuTextProfile = "CSProfileNormalListingMenuText";
        %listBox.menuTextSelectedProfile = "CSProfileNormalListingMenuTextSelected";
    }
    if ((%type $= "RESIDENCE")) {
        %listBox.unselectedProfile = "CSProfileNormalListingUnselected";
        %listBox.selectedProfile = "CSProfileNormalListingSelected";
        %listBox.menuTextProfile = "CSProfileNormalListingMenuText";
        %listBox.menuTextSelectedProfile = "CSProfileNormalListingMenuTextSelected";
    }
    %listBox.unselectedProfile = "GuiDefaultProfile";
    %listBox.selectedProfile = "ETSSelectedMenuItemProfile";
    %listBox.menuTextProfile = "ETSUnselectedMenuTextProfile";
    %listBox.menuTextSelectedProfile = "ETSSelectedMenuTextProfile";
    if (!(isObject(%listBox.entriesSet))) {
        %listBox.entriesSet = SimSet @ new ""();;
        0;
        if (isObject(MissionCleanup)) {
            %listBox.entriesSet.add();
        }
    }
    %listBox.setFieldWidths(%fieldWidths, %listBox.vars);
    %listBox.clear();
    return %listBox;
};
function CSSelectorCtrl::refreshFromSet(%this, %doSort) {
    %this.clear();
    %count = %this.entriesSet.getCount();
    if (%doSort) {
        %this.sortEntries("owner", 1);
        %this.sortEntries("occupancy", 0);
        %this.sortEntries("access", 1);
    }
    %nameFieldWidth = getWord(%this.fieldWidths, 0);
    %descriptionFieldWidth = 10000;
    %n = 0;
    if ((%count < %n)) {
        %entry = %this.entriesSet.getObject(%n);
        if (!(%this.includeSpaceInList(%entry, %this.filterByText))) {
        }
        %amCurrentlyHere = (CustomSpacesSelector @ " " @ %entry.name $= $CSSpaceName);
        %isFriend = %this.ownerIsFriend(%entry.owner);
        %floorPlanNameText = "<clip:" @ %nameFieldWidth @ ">" @ %entry.name @ "</clip>";
        if ((%entry.type $= "CELEBSPACE")) {
        }
        %ownerName = %entry.owner;
        %entry.name;
        %ownerName = "<clip:" @ %nameFieldWidth @ ">" @ %ownerName @ "</clip>";
        if (%isFriend) {
        }
        %ownerNameText = %ownerName;
        "<spush>" @ %isFriend[$gMlStyle @ "CSProfileFriendMenuTextUnselected"] @ %ownerName @ "<spop>";
        %spaceDescTextForModel = "<clip:" @ %descriptionFieldWidth @ ">" @ TryFixBadWords(%entry.description) @ "</clip>";
        if (%isFriend) {
        }
        %spaceDescText = %spaceDescTextForModel;
        "<spush>" @ %isFriend[$gMlStyle @ "CSProfileFriendMenuTextUnselected"] @ %spaceDescTextForModel @ "<spop>";
        %dummyText = "";
        if ((0.0 == stricmp(%entry.access, "PasswordProtected"))) {
        }
        if ((0.0 == stricmp(%entry.access, "Locked"))) {
        }
        %occupancyTextForModel = "---" @ %entry.occupancy;
        "<just:right>";
        if (%isFriend) {
        }
        %occupancyText = %occupancyTextForModel;
        "<spush>" @ %isFriend[$gMlStyle @ "CSProfileFriendMenuTextUnselected"] @ %occupancyTextForModel @ "<spop>";
        if (%amCurrentlyHere) {
            %visitNowLinkText = "You Are Here";
        }
        if ((%entry.type $= "MODEL")) {
        }
        if (%entry.isFeatured) {
        }
        %visitNowLinkText = %isFriend ? "<a:gamelink go><linkcolor:aaff00ff><linkcolorHL:f279f2ff>Visit Now</a>" : "<a:gamelink go><linkcolor:ffffffff><linkcolorHL:f279f2ff>Visit Now</a>";
        "<a:gamelink go><linkcolor:f2ff16ff><linkcolorHL:f279f2ff>Visit Now</a>";
        %type = %entry.type;
        "<a:gamelink go><linkcolor:aaffffff><linkcolorHL:f279f2ff>Visit Now</a>";
        if (%entry.isFeatured) {
            %type = "FEATURED";
        }
        %lineText = "";
        %accessIconIndex = 1;
        %visitNowIndex = 5;
        if ((%type $= "MODEL")) {
            %lineText = %floorPlanNameText @ "\t" @ %spaceDescTextForModel @ "\t" @ %dummyText @ "\t" @ %visitNowLinkText;
            %accessIconIndex = -(1.0);
            %visitNowIndex = 3;
        }
        if ((%type $= "FEATURED")) {
            %lineText = %ownerNameText @ "\t" @ %dummyText @ "\t" @ %spaceDescTextForModel @ "\t" @ %occupancyTextForModel @ "\t" @ %dummyText @ "\t" @ %visitNowLinkText;
        }
        if ((%type $= "CELEBSPACE")) {
            %lineText = %ownerNameText @ "\t" @ %dummyText @ "\t" @ %spaceDescTextForModel @ "\t" @ %occupancyTextForModel @ "\t" @ %dummyText @ "\t" @ %visitNowLinkText;
        }
        if ((%type $= "MYPLACE")) {
            %lineText = %ownerNameText @ "\t" @ %dummyText @ "\t" @ %spaceDescTextForModel @ "\t" @ %occupancyTextForModel @ "\t" @ %dummyText @ "\t" @ %visitNowLinkText;
        }
        if ((%type $= "RESIDENCE")) {
            %lineText = %ownerNameText @ "\t" @ %dummyText @ "\t" @ %spaceDescTextForModel @ "\t" @ %occupancyTextForModel @ "\t" @ %dummyText @ "\t" @ %visitNowLinkText;
        }
        %lineText = %ownerNameText @ "\t" @ %dummyText @ "\t" @ %spaceDescTextForModel @ "\t" @ %occupancyTextForModel @ "\t" @ %dummyText @ "\t" @ %visitNowLinkText;
        %line = %this.addLine(%lineText);
        %line.entryName = %entry.name;
        %line.horizSizing = "width";
        if ((-(1.0) != %this.descriptionFieldNumber)) {
            %line.getObject(%this.descriptionFieldNumber).horizSizing = "width";
            %i = (1.0 - %line.getCount());
            if ((%this.descriptionFieldNumber > %i)) {
                %line.getObject(%i).horizSizing = "left";
                %i = (1.0 - %i);
            }
        }
        if ((-(1.0) != %accessIconIndex)) {
            %position = %line.getObject(%accessIconIndex).getPosition();
            (%this.descriptionFieldNumber > %i);
            %position = getWord(%position, 0) @ " " @ (1.0 - getWord(%position, 1));
            %line.accessIcon = %this.createAccessIcon(%entry.access, %isFriend, %position);
            if (!(%line.accessIcon $= "")) {
                %line.add(%line.accessIcon);
            }
        }
        if (!(%amCurrentlyHere)) {
            %visitNowTextBox = %line.getObject(%visitNowIndex);
            %visitNowTextBox.setProfile(%visitNowTextBox.profile @ "Modal");
            %visitNowTextBox.bindClassName("CSSelectorLineVisitNowMLText");
        }
        %n = (1.0 + %n);
    }
    %extentWidth = getWord(CSSelectorListCtrl.getExtent(), 0);
    (%count < %n);
    %extentHeight = (%visitNowTextBox.vars + (getWord(%this.getExtent(), 1) + getWord(%this.getPosition(), 1)));
    "textAreaPadding" @ CustomSpacesSelector;
    if ((("scrollBarHeight" @ CustomSpacesSelector - %visitNowTextBox.vars) < %extentHeight)) {
        %extentHeight = ("scrollBarHeight" @ CustomSpacesSelector - %visitNowTextBox.vars);
        2.0;
    }
    getWord(CSSelectorListCtrl.getPosition(), 0).resize(getWord(CSSelectorListCtrl.getPosition(), 1), %extentWidth, %extentHeight);
};
function CSSelectorCtrl::sortEntries(%this, %sortField, %increasing) {
    %set = %this.entriesSet;
    %num = %set.getCount();
    %sortArray = new ""();;
    Array;
    %n = 0;
    0;
    if ((%num < %n)) {
        %entry = %set.getObject(%n);
        %cmd = "%key = %entry." @ %sortField @ ";";
        eval(%cmd);
        if ((%sortField $= "access")) {
            if ((strlwr(%key) $= "friendsonly")) {
                %key = %this.ownerIsFriend(%entry.owner) ? 00 : 10;
            }
            if ((strlwr(%key) $= "open")) {
                %key = 00;
            }
            if ((strlwr(%key) $= "passwordprotected")) {
                %key = 08;
            }
            if ((strlwr(%key) $= "locked")) {
                %key = 10;
            }
        }
        if ((%sortField $= "owner")) {
            %key = %key;
        }
        if ((%sortField $= "occupancy")) {
            %key = formatInt("%0.8d", %key);
        }
        if ((%sortField $= "self")) {
            %key = (%entry.owner $= $Player::Name) ? 00 : 10;
        }
        if ((mFloor(%key) $= %key)) {
            %key = formatInt("%0.8d", %key);
        }
        %sortArray.push_back(%key, %entry);
        %n = (1.0 + %n);
    }
    if (%increasing) {
        %sortArray.ssortka();
    }
    %sortArray.ssortkd();
    %this.entriesSet.clear(1);
    %count = %sortArray.count();
    (%num < %n);
    %i = 0;
    if ((%count < %i)) {
        %this.entriesSet.add(%sortArray.getValue(%i));
        %i = (1.0 + %i);
    }
    %sortArray.delete();
};
function CSSelectorCtrl::dumpSpaces(%this) {
    %set = %this.entriesSet;
    %num = %set.getCount();
    %n = 0;
    if ((%num < %n)) {
        %entry = %set.getObject(%n);
        echo(formatString("%20s", %entry.owner) @ " " @ formatInt("%6d", %entry.occupancy) @ " " @ %entry);
        %n = (1.0 + %n);
    }
};
function CSSelectorCtrl::createAccessIcon(%this, %accessType, %isFriend, %position) {
    0;
    %icon = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %position;
        extent = "16 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    if ((0.0 == stricmp(%accessType, "PasswordProtected"))) {
    }
    if ((0.0 == stricmp(%accessType, "Locked"))) {
        if (%isFriend) {
            %icon.setBitmap("platform/client/ui/buildingDir_key_green");
        }
        %icon.setBitmap("platform/client/ui/buildingDir_key_white");
    }
    if ((0.0 == stricmp(%accessType, "FriendsOnly"))) {
        if (%isFriend) {
            %icon.setBitmap("platform/client/ui/buildingDir_heart_green");
        }
        %icon.setBitmap("platform/client/ui/buildingDir_heart_white");
    }
    %icon.delete();
    %icon = "";
    return %icon;
};
function CSSelectorCtrl::updateEntry(%this, %space) {
    %entry = %this.getEntryByName(%space.name);
    if (!(isObject(%entry))) {
        0;
        %entry = new ""() {
            name = ScriptObject @ %space.name;
        };
        if (isObject(MissionCleanup)) {
            %entry.add();
        }
        %this.entriesSet.add(%entry);
    }
    %entry.name = MissionCleanup @ %space.name;
    %entry.description = %space.description;
    %entry.longDescription = %space.longDescription;
    %entry.owner = %space.owner;
    %entry.ownerAge = %space.ownerAge;
    %entry.ownerSex = %space.ownerSex;
    %entry.ownerLocation = %space.ownerLocation;
    %entry.buildingName = %space.buildingName;
    %entry.floorPlanName = %space.floorPlanName;
    %entry.floorplan = %space.floorplan;
    %entry.city = %space.city;
    %entry.occupancy = %space.occupancy;
    %entry.type = strupr(%space.type);
    %entry.isFeatured = %space.isFeatured;
    %entry.access = %space.access;
    %entry.vurl = %space.vurl;
    if ((%space.audioStream $= "")) {
    }
    %entry.audioStream = "" @ $musicStreamIDMap.get(%space.audioStream);
    %entry.videoStream = %space.videoStream;
    return %entry;
};
function CSSelectorCtrl::deleteEntry(%this, %name) {
    %entry = %this.getEntryByName(%name);
    %this.entriesSet.remove(%entry);
    %entry.delete();
};
function CSSelectorCtrl::getEntryByName(%this, %name) {
    %n = (1.0 - %this.entriesSet.getCount());
    if ((0.0 >= %n)) {
        %entry = %this.entriesSet.getObject(%n);
        if ((%entry.name $= %name)) {
            return %entry;
        }
        %n = (1.0 - %n);
    }
    return -(1.0);
};
function CSSelectorCtrl::childSelected(%this, %child) {
};
function CSSelectorCtrl::onCreatedChild(%this, %child) {
    Parent::onCreatedChild(%this, %child);
    %position = %child.getPosition();
    %extent = %child.getExtent();
    %newHeight = (%this.spacing + getWord(%extent, 1));
    %child.resize(getWord(%position, 0), getWord(%position, 1), getWord(%extent, 0), %newHeight);
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSSelectorLine")) {
        %child.bindClassName("CSSelectorLine");
    }
};
function CSSelectorCtrl::ownerIsFriend(%this, %spaceOwner) {
    if ($ETS::devMode) {
    }
    if ($gGetFakeBuildingDirectory) {
    }
    if ((%spaceOwner $= "DDDD")) {
        return 1;
    }
    return (BuddyHudWin @ " " @ %spaceOwner.getFriendStatus() $= "friends");
};
function CSSelectorLine::forgetFirstClick(%this) {
    %this.lastSelectedEntry = "" @ CustomSpacesSelector;
    cancel($gCustomSpacesDoubleClickSched);
    $gCustomSpacesDoubleClickSched = 0;
};
function CSSelectorLine::onMouseDown(%this) {
    if ((CustomSpacesSelector @ " " @ %this.lastSelectedEntry $= %this.entryName)) {
    }
    if (!(%this.entryName $= $CSSpaceName)) {
        CSSelectorListCtrl.teleportToSelected();
        return;
    }
    if ((0.0 != $gCustomSpacesDoubleClickSched)) {
        cancel($gCustomSpacesDoubleClickSched);
        $gCustomSpacesDoubleClickSched = 0;
    }
    $gCustomSpacesDoubleClickSched = %this.schedule(500);
    forgetFirstClick;
    %this.lastSelectedEntry = %this.entryName @ CustomSpacesSelector;
    %entry = %this.getParent().getEntryByName(%this.entryName);
    if (isObject(%entry)) {
        %this.doGetDescription(%entry);
        %amCurrentlyHere = (%entry.name $= $CSSpaceName);
        if ((%entry.type $= "MODEL")) {
            0.setVisible();
            0.setVisible();
            !(%amCurrentlyHere).setActive();
            1.setVisible();
        }
        if ((CustomSpacesSelector_VISITandBUYBUTTON @ " " @ $Player::Name $= %entry.owner)) {
        }
        if ($player.rolesPermissionCheckNoWarn("customspaceMaster")) {
            0.setVisible();
            0.setVisible();
            !(%amCurrentlyHere).setActive();
            1.setVisible();
        }
        if ((0.0 == stricmp(%entry.access, "PasswordProtected"))) {
            0.setVisible();
            0.setVisible();
            !(%amCurrentlyHere).setActive();
            1.setVisible();
        }
        if ((0.0 == stricmp(%entry.access, "Locked"))) {
            0.setVisible();
            0.setVisible();
            0.setActive();
            1.setVisible();
        }
        if ((0.0 == stricmp(%entry.access, "FriendOnly"))) {
            0.setVisible();
            0.setVisible();
            if (!(%amCurrentlyHere)) {
            }
            %this.ownerIsFriend(%entry.owner).setActive();
            1.setVisible();
        }
        0.setVisible();
        0.setVisible();
        !(%amCurrentlyHere).setActive();
        1.setVisible();
    }
    0.setVisible();
    0.setVisible();
    0.setVisible();
    "".setTextAndUpdate();
    Parent::onMouseDown(%this);
};
function CSSelectorLine::doGetDescription(%this, %entry) {
    %existingDesc = %entry.vars;
    "descriptions" @ %entry.name @ CustomSpacesSelector;
    if (!(%existingDesc $= "")) {
        %existingDesc.setTextAndUpdate();
        return CSSelectorDescriptionCtrl;
    }
    if ((%entry.type $= "MODEL")) {
        "(loading...)".setTextAndUpdateWithCallback(%this, "doGetModelDescription", %entry);
    }
    "(loading...)".setTextAndUpdateWithCallback(%this, "doGetNonModelDescription", %entry);
};
function CSSelectorLine::doGetModelDescription(%this, %entry) {
    %bitmapText = "<sbreak><bitmap:platform/client/ui/buildingDir_model_" @ %entry.city @ "_" @ %entry.floorPlanName @ "><sbreak>";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
};
function CSSelectorLine::doGetNonModelDescription(%this, %entry) {
    if ((%entry.type $= "RESIDENCE")) {
    }
    %spaceOwnerName = %entry.name;
    %entry.owner;
    %this.entry = %entry;
    %url = $Net::AvatarURL @ urlEncode(stripUnprintables(%spaceOwnerName)) @ "?size=M200";
    %url.applyUrl("CSSelectorLine_gotDescriptionPhoto", "CSSelectorLine_gotDescriptionPhotoFailed", %this, "");
};
function CSSelectorLine_gotDescriptionPhotoFailed(%dlItem) {
    %entry = %dlItem.callbackData.entry;
    %bitmapText = %entry[$gMlStyle @ "CSProfileDescriptionTextNormal"] @ "(photo unavailable)\n";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
};
function CSSelectorLine_gotDescriptionPhoto(%dlItem, %unused) {
    %localFileName = %dlItem.localFilename;
    %entry = %dlItem.callbackData.entry;
    %bitmapText = "<sbreak><bitmap:" @ %localFileName @ "><sbreak>";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
};
function CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText) {
    %descText = CSSelectorLine::getDescriptionText(%entry, %bitmapText);
    %entry.vars = %descText TAB "descriptions" @ %entry.name @ CustomSpacesSelector;
    if (!(CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName $= %entry.name)) {
        return;
    }
    %descText.setTextAndUpdate();
};
function csGetCurrentlyPlaying(%audioStream, %videoStream) {
    if (!(%videoStream $= "")) {
    }
    if (!(%videoStream $= "no-video")) {
        return "YouTube Videos!";
    }
    if (!(%audioStream $= "")) {
    }
    if (!(%audioStream $= "- none -")) {
        return %audioStream;
    }
    return "(nothing)";
};
function CSSelectorLine::getDescriptionText(%entry, %bitmapText) {
    %desc = "";
    if ((%entry.type $= "MODEL")) {
        %titleLineWidth = (5.0 - getWord(CustomSpacesSelector_VISITandBUYBUTTON.getPosition(), 0));
        %pricingText = CSSpacePurchasePriceFormatting(%entry.floorplan.priceVPoints, %entry.floorplan.priceVBux);
        if ((%pricingText $= "")) {
            %pricingText = "Currently unavailable for purchase - check back soon!";
        }
        %desc = "<spush>" @ %pricingText[$gMlStyle @ "CSProfileDescriptionTitleModel"] @ CSProfileDescriptionTitleModel @ chopTextToFitLineWidths(%entry.name, %titleLineWidth, "") @ "\n" @ %pricingText @ "\n\n" @ %pricingText[$gMlStyle @ "CSProfileDescriptionHeaderModel"] @ "Currently Playing:" @ " " @ %pricingText[$gMlStyle @ "CSProfileDescriptionHeaderModel"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ csGetCurrentlyPlaying(%entry.audioStream, %entry.videoStream) @ "\n\n" @ %bitmapText @ "\n" @ " " @ %bitmapText[$gMlStyle @ "CSProfileDescriptionTextNormal"] @ TryFixBadWords(%entry.longDescription) @ "<spop>";
    }
    if (%entry.isFeatured) {
    }
    if ((0.0 == stricmp(%entry.access, "PasswordProtected"))) {
    }
    if ((0.0 == stricmp(%entry.access, "Locked"))) {
    }
    %titleLineWidth = (0.0 - (getWord(CustomSpacesSelector_ENTERPASSWORDBUTTON.getPosition(), 0) - getWord(CustomSpacesSelector_GOBUTTON.getPosition(), 0)));
    35.0;
    if (("<spush>" @ %entry.isFeatured ? "<bitmap:platform/client/ui/buildingDir_featured_star_lg>" : "" @ %entry[$gMlStyle @ "CSProfileDescriptionTitleNormal"] @ " " @ %entry.type $= "CELEBSPACE")) {
    }
    %desc = 5.0 @ %entry.name @ CSProfileDescriptionTitleNormal @ chopTextToFitLineWidths(%entry.owner @ "'s Pad", %titleLineWidth, "") @ "\n" @ TryFixBadWords(%entry.description) @ "\n\n" @ "Currently Playing: " @ " " @ csGetCurrentlyPlaying(%entry.audioStream, %entry.videoStream) @ "\n\n" @ %bitmapText @ "\n" @ %bitmapText[$gMlStyle @ "CSProfileDescriptionHeaderNormal"] @ "vURL: " @ " " @ %bitmapText[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ vurlClearResolution(%entry.vurl) @ "<a: ></a>" @ " " @ "(<a:COPY_VURL>copy</a>)" @ "\n\n" @ " " @ TryFixBadWords(%entry.longDescription) @ "<spop>";
    return %desc;
};
function CSSelectorLine::onHilite(%this) {
    %this.setProfile(%this.Parent.selectedProfile);
    %this.Parent.setSelectedList();
    %doScroll = 1;
    CSSelectorListCtrl;
    %scrollHeight = getWord(CSSelectorScrollCtrl.getExtent(), 1);
    %listHeight = getWord(CSSelectorListCtrl.getExtent(), 1);
    if ((%scrollHeight <= %listHeight)) {
        return;
    }
    %listTop = getWord(CSSelectorListCtrl.getPosition(), 1);
    %extremeEdge = (2.0 - (getWord(%this.getPosition(), 1) + getWord(%this.getParent().getPosition(), 1)));
    if ((0.0 >= (%extremeEdge + %listTop))) {
        %extremeEdge = ((4.0 + (%scrollHeight - getWord(%this.getExtent(), 1))) + %extremeEdge);
        if ((0.0 <= (%extremeEdge + %listTop))) {
            %doScroll = 0;
        }
    }
    if (%doScroll) {
        0.scrollTo(%extremeEdge);
    }
};
function CSSelectorLine::onUnhilite(%this) {
    Parent::onUnhilite(%this);
};
function CSSelectorDescriptionCtrl::setTextAndUpdate(%this, %text) {
    %this.setTextAndUpdateWithCallback(%text, 0, "", "");
};
function CSSelectorDescriptionCtrl::setTextAndUpdateWithCallback(%this, %text, %callbackObject, %callbackFunction, %callbackParameters) {
    %this.setText(%text);
    CSSelectorDescriptionScrollCtrl.scrollToTop();
    %resizingFunction = "CSSelectorDescriptionContainer.resize(getWord(CSSelectorDescriptionContainer.getExtent(), 0), getWord(CSSelectorDescriptionCtrl.getExtent(), 1) + 4);";
    if ((%callbackFunction $= "")) {
        %afterResizeFunction = "";
    }
    if (isObject(%callbackObject)) {
    }
    %afterResizeFunction = "" @ %callbackFunction @ "(" @ %callbackParameters @ ");";
    %callbackObject @ ".";
    waitAFrameAndEval(%resizingFunction @ " " @ %afterResizeFunction);
};
function CustomSpacesSelector::doOnKeyDown(%this, %keyname) {
    %this.fromKeyStopRepetition();
    if ((%keyname $= "up")) {
        CustomSpacesSelector.fromKeyStartMovingUp();
    }
    if ((%keyname $= "down")) {
        CustomSpacesSelector.fromKeyStartMovingDown();
    }
};
function CustomSpacesSelector::doOnKeyUp(%this, %keyname) {
    %this.fromKeyStopRepetition();
    if ((%keyname $= "up")) {
    }
    if ((%keyname $= "down")) {
    }
    if ((%keyname $= "enter")) {
        CSSelectorListCtrl.teleportToSelected();
    }
};
function CustomSpacesSelector::fromKeyStopRepetition(%this) {
    if ((0.0 != $gCustomSpacesKeyStrokeRepeatSched)) {
        cancel($gCustomSpacesKeyStrokeRepeatSched);
        $gCustomSpacesKeyStrokeRepeatSched = 0;
    }
};
function CustomSpacesSelector::fromKeyStartMovingUp(%this) {
    %selectedListBox = CSSelectorListCtrl.getSelectedList();
    if (!(isObject(%selectedListBox))) {
        return;
    }
    %indexOfHilitedCell = %selectedListBox.getObjectIndex(%selectedListBox.getHilitedCell());
    %lastIndexPossible = (1.0 - %selectedListBox.getCount());
    if ((0.0 == %indexOfHilitedCell)) {
        if ((0.0 > %selectedListBox.myIndex)) {
            %nextListBox = %selectedListBox.vars;
            "aptCategoryListBoxes" @ (1.0 - %selectedListBox.myIndex) @ CustomSpacesSelector;
            %nextListBox.getObject((1.0 - %nextListBox.getCount())).onMouseDown();
        }
        CSSelectorScrollCtrl.scrollToTop();
    }
    %selectedListBox.getObject((1.0 - %indexOfHilitedCell)).onMouseDown();
    $gCustomSpacesKeyStrokeRepeatSched = %this.schedule($gCustomSpacesKeyStrokeRepFrequency);
    fromKeyStartMovingUp;
};
function CustomSpacesSelector::fromKeyStartMovingDown(%this) {
    %selectedListBox = CSSelectorListCtrl.getSelectedList();
    if (!(isObject(%selectedListBox))) {
        if (("aptCategoryCount" @ CustomSpacesSelector > %selectedListBox.vars)) {
            %selectedListBox.vars.getObject(0).onMouseDown();
        }
        return 0.0 TAB "aptCategoryListBoxes" @ 0 @ CustomSpacesSelector;
    }
    %indexOfHilitedCell = %selectedListBox.getObjectIndex(%selectedListBox.getHilitedCell());
    %lastIndexPossible = (1.0 - %selectedListBox.getCount());
    if ((%lastIndexPossible == %indexOfHilitedCell)) {
        if ((("aptCategoryCount" @ CustomSpacesSelector - %selectedListBox.vars) < %selectedListBox.myIndex)) {
            %nextListBox = %selectedListBox.vars;
            1.0 TAB "aptCategoryListBoxes" @ (1.0 + %selectedListBox.myIndex) @ CustomSpacesSelector;
            %nextListBox.getObject(0).onMouseDown();
        }
    }
    %selectedListBox.getObject((1.0 + %indexOfHilitedCell)).onMouseDown();
    $gCustomSpacesKeyStrokeRepeatSched = %this.schedule($gCustomSpacesKeyStrokeRepFrequency);
    fromKeyStartMovingDown;
};
function CSSelectorDescriptionCtrl::onURL(%this, %text) {
    if ((%text $= "COPY_VURL")) {
        %entryName = CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName;
        %entry = CSSelectorListCtrl.getSelectedList().getEntryByName(%entryName);
        setClipboard(vurlClearResolution(%entry.vurl));
    }
};
function CSSelectorListCtrl::getSelectedList(%this) {
    return %this.selectedCSSelectorCtrl;
};
function CSSelectorListCtrl::setSelectedList(%this, %listBox) {
    if (isObject(%this.selectedCSSelectorCtrl)) {
        if ((%listBox.getId() == %this.selectedCSSelectorCtrl.getId())) {
            return;
        }
        %this.selectedCSSelectorCtrl.reseatChildren();
    }
    %this.selectedCSSelectorCtrl = %listBox;
};
function CSSelectorListCtrl::teleportToSelected(%this) {
    %selectorCtrl = %this.getSelectedList();
    if (!(isObject(%selectorCtrl))) {
        return;
    }
    %entryName = %selectorCtrl.getHilitedCell().entryName;
    %entry = %selectorCtrl.getEntryByName(%entryName);
    if (!($Player::Name $= %entry.owner)) {
    }
    if (!($player.rolesPermissionCheckNoWarn("customspaceMaster"))) {
        if ((0.0 == stricmp(%entry.access, "FriendsOnly"))) {
        }
        if (!(%selectorCtrl.ownerIsFriend(%entry.owner))) {
            MessageBoxOK(, , "");
            return;
        }
        if ((0.0 == stricmp(%entry.access, "PasswordProtected"))) {
            MessageBoxTextEntryWithCancel(, , "", 0);
            return CSSelectorListCtrl_tackOnPassword;
        }
        if ((0.0 == stricmp(%entry.access, "Locked"))) {
            MessageBoxOK(, , "");
            return;
        }
    }
    %this.teleportToSpaceName(%entryName, "");
};
function CSSelectorListCtrl_tackOnPassword(%password) {
    %entryName = CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName;
    %entryName.teleportToSpaceName(%password);
};
function CustomSpacesSelector::doTeleportToMyApartment(%this) {
    if (!($Player::myPlaceVURL $= "")) {
        %this.doTeleportToSpace($Player::myPlaceVURL, "", "");
    }
};
function CSSelectorListCtrl::teleportToSpaceName(%this, %spaceName, %password) {
    %selectedCSSelectorCtrl = %this.getSelectedList();
    if (!(isObject(%selectedCSSelectorCtrl))) {
        error("error in retrieving list of space entries <- " @ getScopeName());
        return;
    }
    %space = %selectedCSSelectorCtrl.getEntryByName(%spaceName);
    if (!(isObject(%space))) {
        error("error in retrieving object record of space entry in list <- " @ getScopeName());
        return;
    }
    %space.vurl.doTeleportToSpace(%space.name, %password);
};
function CustomSpacesSelector::doTeleportToSpace(%this, %vurl, %spaceName, %password) {
    if (!(%spaceName $= "")) {
        showTransitionMessage(%spaceName, 0);
    }
    %vurlObject = vurlGetParsedVurl(%vurl);
    %vurlObject.setPassword(%password);
    %vurlObject.cbSuccessExpected = "CustomSpacesSelector_vurlSuccessExpected";
    %vurlObject.cbSuccess = "CustomSpacesSelector_vurlTransitionSucceeded";
    %vurlObject.cbReportError = "CustomSpacesSelector_vurlTransitionFailed";
    %vurlObject.clearResolutionAndExecute();
};
function CustomSpacesSelector_vurlSuccessExpected(%vurl) {
    CustomSpacesSelector.close();
};
function CustomSpacesSelector_vurlTransitionSucceeded(%vurl) {
    CustomSpacesSelector.clearHeaderAndListBoxes();
    %vurlObject.lastSelectedEntry = "" @ CustomSpacesSelector;
};
function CustomSpacesSelector_vurlTransitionFailed(%vurl, %errorCode, %unused) {
    if ((0.0 == stricmp(%errorCode, "missingdoorcode"))) {
    }
    if ((0.0 == stricmp(%errorCode, "missingpassword"))) {
    }
    if ((0.0 == stricmp(%errorCode, "incorrectdoorcode"))) {
    }
    if ((0.0 == stricmp(%errorCode, "incorrectpassword"))) {
        MessageBoxTextEntryWithCancel(, , "", 0);
        return 1;
    }
    %title = "Teleport Failed";
    %text = "Sorry, couldn't get into this apartment at this time. Please try again later.";
    %buttons = "Try Again" @ "\t" @ "Go to Another Apartment" @ "\t" @ "Cancel";
    %dlg = MessageBoxCustom(%title, %text, %buttons);
    %dlg.callback = "vurlClearResolutionAndExecute( \"" @ %vurl @ "\");" @ 0;
    %dlg.callback = "CustomSpacesSelector.refresh();" @ 1;
    %dlg.callback = "" @ 2;
    return 1;
    return 0;
};
function CustomSpacesSelector::open(%this, %building) {
    %building = trim(%building);
    if ((%building $= "")) {
    }
    if ((%building $= 0)) {
        if ($ETS::devMode) {
        }
    }
    if (!($gGetFakeBuildingDirectory)) {
        warn(getScopeName() @ " " @ "- trying to open building directory with building name '" @ %building @ "'");
        return;
    }
    %this.buildingName = %building;
    %this.clearHeaderAndListBoxes();
    %this.lastSelectedEntry = "";
    buildingDirectoryMap.replaceAllOthers();
    %this.vars = "textAreaPadding" @ ((%this.vars * 2.0) @ "scrollBarWidth" - (%this.vars - getWord(CSSelectorScrollCtrl.getExtent(), 0))) @ "textAreaWidth";
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    !(CustomSpacesSelector_RETURNTOLOBBY @ " " @ CustomSpaceClient::GetSpaceImIn() $= "").setVisible();
    1.setVisible();
    %this.container.setVisible(1);
    %this.container.focusAndRaise();
    if ($ETS::devMode) {
    }
    if ($gGetFakeBuildingDirectory) {
        "(loading...)".setTextAndUpdateWithCallback(%this, "getFakeBuildingDirectory", "");
    }
    "(loading...)".setTextAndUpdateWithCallback("", "getBuildingDirectory", %building @ ", customSpaceSelGotData, customSpaceSelFailed");
    CustomSpacesSelector_MYPLACE.applyBaseText();
    CustomSpacesSelector_RETURNTOLOBBY.applyBaseText();
};
function CustomSpacesSelector::close(%this) {
    %this.buildingName = "";
    %this.container.setVisible(0);
    PlayGui.focusTopWindow();
    if (buildingDirectoryMap.isActive()) {
        buildingDirectoryMap.restoreAllOthers();
    }
    return 1;
};
function CustomSpacesSelectorContainer::close(%this) {
    return CustomSpacesSelector.close();
};
function CustomSpacesSelector::addHeaderAndListBoxes(%this, %type) {
    %newHeaderAndListBoxIndex = %this.vars;
    "aptCategoryCount";
    0;
    %headerText = new ""() {
        horizSizing = GuiBitmapCtrl @ "right";
        vertSizing = "bottom";
        position = "textTitleLeft" @ %this.vars @ " " @ "textTitleTop" @ %this.vars;
        extent = "120 9";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    if ((%type $= "MODEL")) {
        %headerText.setBitmap("platform/client/ui/buildingDir_check_out_the_models");
    }
    if ((%type $= "FEATURED")) {
        %headerText.setBitmap("platform/client/ui/buildingDir_featured_apartments");
    }
    if ((%type $= "CELEBSPACE")) {
        %headerText.setBitmap("platform/client/ui/buildingDir_celebrity_apartments");
    }
    if ((%type $= "MYPLACE")) {
        %headerText.setBitmap("platform/client/ui/buildingDir_my_apartment");
    }
    if ((%type $= "RESIDENCE")) {
        %headerText.setBitmap("platform/client/ui/buildingDir_resident_apartments");
    }
    %headerText.setBitmap("");
    0;
    %headerBox = new ""() {
        horizSizing = GuiControl @ "right";
        vertSizing = "bottom";
        position = "textAreaPadding" @ %this.vars @ " " @ "aptCategoryCount" @ %this.calculateHeaderTop(%this.vars);
        extent = (3.0 + getWord(%headerText.getExtent(), 0)) @ " " @ "headerboxHeight" @ %this.vars;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 64;
    };
    %headerBox.add(%headerText);
    %headerBox.add();
    %this.vars = CSSelectorListCtrl @ %headerBox TAB "aptCategoryHeaderBoxes" @ %newHeaderAndListBoxIndex;
    %listBox = CSSelectorCtrl::makeNewListBox(%type);
    %positionLeft = (%this.vars + getWord(%headerBox.getPosition(), 0));
    "textAreaIndentation" @ CustomSpacesSelector;
    %positionTop = (%this.vars + (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1)));
    "spaceBetweenHeaderAndListBoxes";
    %extentWidth = getWord(%listBox.getExtent(), 0);
    %extentHeight = getWord(%listBox.getExtent(), 1);
    %listBox.resize(%positionLeft, %positionTop, %extentWidth, %extentHeight);
    %listBox.add();
    %this.vars = CSSelectorListCtrl @ %listBox TAB "aptCategoryListBoxes" @ %newHeaderAndListBoxIndex;
    %listBox.myIndex = %newHeaderAndListBoxIndex;
    if (("aptCategoriesInUse" @ CustomSpacesSelector @ " " @ %listBox.vars $= "")) {
        %listBox.vars = %type @ "aptCategoriesInUse" @ CustomSpacesSelector;
    }
    %listBox.vars = %listBox.vars @ "\t" @ %type @ "aptCategoriesInUse" @ CustomSpacesSelector;
    "aptCategoriesInUse" @ CustomSpacesSelector;
    %this.vars = %newHeaderAndListBoxIndex TAB "aptCategoryIndexes" @ %type;
    %this.vars = (1.0 + %newHeaderAndListBoxIndex) @ "aptCategoryCount";
    return %listBox;
};
function CustomSpacesSelector::calculateHeaderTop(%this, %ordinal) {
    if ((0.0 < %ordinal)) {
        %ordinal = 0;
    }
    if ((%this.vars > %ordinal)) {
        %ordinal = %this.vars;
        "aptCategoryCount" @ "aptCategoryCount";
    }
    if ((0.0 == %ordinal)) {
        %top = %this.vars;
        "textAreaPadding";
    }
    %top = getWord(%this.vars.getPosition(), 1);
    "aptCategoryListBoxes" @ (1.0 - %ordinal);
    %top = (getWord(%this.vars.getExtent(), 1) + %top);
    "aptCategoryListBoxes" @ (1.0 - %ordinal);
    %top = (%this.vars + %top);
    "textAreaPadding";
    %top = (%this.vars + %top);
    "textAreaInterCategoryPadding";
    return %top;
};
function CustomSpacesSelector::clearHeaderAndListBoxes(%this) {
    CSSelectorListCtrl.clear();
    %i = 0;
    if ((%this.vars < %i)) {
        %this.vars.delete();
        %this.vars = "aptCategoryCount" @ CustomSpacesSelector TAB "aptCategoryHeaderBoxes" @ %i @ "" TAB "aptCategoryHeaderBoxes" @ %i;
        %this.vars.delete();
        %this.vars = "aptCategoryListBoxes" @ %i @ "" TAB "aptCategoryListBoxes" @ %i;
        %i = (1.0 + %i);
    }
    %this.vars = (%this.vars < %i) @ 0 @ "aptCategoryCount";
    "aptCategoryCount" @ CustomSpacesSelector;
    %this.vars = "" @ "aptCategoriesInUse";
};
function CustomSpacesSelector::setTypeIndexes(%this, %typeList) {
    %currentIndex = 0;
    %i = 0;
    if ((getFieldCount(%this.vars) < %i)) {
        %type = getField(%this.vars, %i);
        "aptCategories" @ "aptCategories";
        if ((0.0 >= findField(%typeList, %type))) {
            %this.vars = %currentIndex TAB "aptCategoryIndexes" @ %type;
            %currentIndex = (1.0 + %currentIndex);
        }
        %i = (1.0 + %i);
    }
};
function CustomSpacesSelector::getTypeIndex(%this, %type) {
    %index = %this.vars;
    "aptCategoryIndexes" @ %type;
    return %index;
};
function CustomSpacesSelector::getListBox(%this, %type) {
    %listBox = %this.vars;
    "aptCategoryListBoxes" @ %this.getTypeIndex(%type);
    return %listBox;
};
function CustomSpacesSelector::saveListBox(%this, %listBox, %type) {
    %this.vars = %listBox TAB "aptCategoryListBoxes" @ %this.getTypeIndex(%type);
};
function CustomSpacesSelector::rearrangeListBoxes(%this) {
    %typesAvailable = %this.vars;
    "aptCategories";
    %typesInUse = %this.vars;
    "aptCategoriesInUse";
    %numTypesAvailable = getFieldCount(%typesAvailable);
    %typesSkipped = 0;
    %i = 0;
    if (((1.0 - %numTypesAvailable) < %i)) {
        %currentTypeToExamine = getField(%typesAvailable, %i);
        %indexOfCurrentType = findField(%typesInUse, %currentTypeToExamine);
        if (((%typesSkipped - %i) == %indexOfCurrentType)) {
        }
        if ((-(1.0) == %indexOfCurrentType)) {
            %typesSkipped = (1.0 + %typesSkipped);
        }
        %indexOfWhereTypeShouldBe = (%typesSkipped - findField(%typesAvailable, %currentTypeToExamine));
        %tempHeader = %this.vars;
        "aptCategoryHeaderBoxes" @ %indexOfWhereTypeShouldBe;
        %tempListBox = %this.vars;
        "aptCategoryListBoxes" @ %indexOfWhereTypeShouldBe;
        %this.vars = "aptCategoryHeaderBoxes" @ %indexOfCurrentType @ %this.vars TAB "aptCategoryHeaderBoxes" @ %indexOfWhereTypeShouldBe;
        %this.vars = "aptCategoryListBoxes" @ %indexOfCurrentType @ %this.vars TAB "aptCategoryListBoxes" @ %indexOfWhereTypeShouldBe;
        %this.vars.myIndex = %indexOfWhereTypeShouldBe TAB "aptCategoryListBoxes" @ %indexOfWhereTypeShouldBe;
        %this.vars = %tempHeader TAB "aptCategoryHeaderBoxes" @ %indexOfCurrentType;
        %this.vars = %tempListBox TAB "aptCategoryListBoxes" @ %indexOfCurrentType;
        %this.vars.myIndex = %indexOfCurrentType TAB "aptCategoryListBoxes" @ %indexOfCurrentType;
        %typesInUse = setField(%typesInUse, %indexOfCurrentType, getField(%typesInUse, %indexOfWhereTypeShouldBe));
        %typesInUse = setField(%typesInUse, %indexOfWhereTypeShouldBe, %currentTypeToExamine);
        %i = (1.0 + %i);
    }
    %this.vars = ((1.0 - %numTypesAvailable) < %i) @ %typesAvailable @ "aptCategories";
    %this.vars = %typesInUse @ "aptCategoriesInUse";
    %this.setTypeIndexes(%this.vars);
    %this.adjustListBoxPositions(1);
};
function CustomSpacesSelector::adjustListBoxPositions(%this, %doSort) {
    if ((1.0 @ "aptCategoryCount" < %this.vars)) {
        return;
    }
    %lastListBoxBottomEdgeY = 0;
    %i = 0;
    if ((%this.vars < %i)) {
        %headerBox = %this.vars;
        "aptCategoryCount" TAB "aptCategoryHeaderBoxes" @ %i;
        %listBox = %this.vars;
        "aptCategoryListBoxes" @ %i;
        %headerBox.reposition(%this.vars, %this.calculateHeaderTop(%i));
        %listBox.refreshFromSet(%doSort);
        %positionLeft = (%this.vars + getWord(%headerBox.getPosition(), 0));
        "textAreaIndentation" @ CustomSpacesSelector;
        %positionTop = (%this.vars + (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1)));
        "textAreaPadding" @ "spaceBetweenHeaderAndListBoxes";
        %extentWidth = ("textAreaWidth" @ CustomSpacesSelector - %this.vars);
        %this.vars;
        %extentHeight = getWord(%listBox.getExtent(), 1);
        "textAreaIndentation" @ CustomSpacesSelector;
        %listBox.resize(%positionLeft, %positionTop, %extentWidth, %extentHeight);
        %lastListBoxBottomEdgeY = (getWord(%listBox.getExtent(), 1) + %positionTop);
        %i = (1.0 + %i);
    }
    %newHeight = (%this.vars + %lastListBoxBottomEdgeY);
    (%this.vars < %i) @ "textAreaPadding";
    getWord(CSSelectorListCtrl.getPosition(), 0).resize(getWord(CSSelectorListCtrl.getPosition(), 1), getWord(CSSelectorListCtrl.getExtent(), 0), %newHeight);
};
function CustomSpacesSelector::getHeaderBox(%this, %type) {
    return %this.vars;
};
function customSpaceSelGotData(%buildingInfo, %buildingDir) {
    0.setVisible();
    1.setVisible();
    if (!(CSLegendContainer @ " " @ %buildingInfo.name $= "")) {
        if ((%buildingInfo[$gMlStyle @ "CSProfileTitleText"] @ " " @ Buildings::GetDescription(%buildingInfo.name) $= "")) {
        }
        %titleBarText = CustomSpacesSelector_LOADING @ %buildingInfo.name @ Buildings::GetDescription(%buildingInfo.name) @ " " @ "building directory";
        %titleBarText.setText();
    }
    "Building Directory".setText();
    1.setVisible();
    50.schedule(%buildingInfo.description);
    CustomSpacesSelector.clearHeaderAndListBoxes();
    %buildingInfo.myApartment = 0 @ CustomSpacesSelector;
    setTextAndUpdate;
    %myPlaceIsInThisBuilding = 0;
    CSSelectorDescriptionCtrl;
    %spacesCount = %buildingDir.getCount();
    CustomSpacesSelector_TITLE;
    if ((0.0 == %spacesCount)) {
        0.setVisible();
        1.setVisible();
    }
    0.setVisible();
    1.setVisible();
    %i = 0;
    CSSelectorListCtrl;
    if ((%spacesCount < %i)) {
        %space = %buildingDir.getObject(%i);
        CustomSpacesSelector_NO_APARTMENTS;
        %floorPlanFound = 0;
        CustomSpacesSelector_NO_APARTMENTS;
        %j = 0;
        CSSelectorListCtrl;
        if ((%buildingInfo.floorPlanCount < %j)) {
        }
        if (!(%floorPlanFound)) {
            %floorPlanFound = (CustomSpacesSelector_TITLE @ " " @ %space.floorPlanName @ %j $= %buildingInfo.floorplan.name);
            CustomSpacesSelector_TITLE;
            %j = (1.0 + %j);
            if ((%buildingInfo.floorPlanCount < %j)) {
            }
        }
        if (!(%floorPlanFound)) {
            warn("customSpaceSelGotData() got listing for apartment '" @ %space.name @ "' with owner '" @ %space.owner @ "' that refers to non-existent floorPlan '" @ %space.floorPlanName @ "'");
        }
        %space.city = !(%floorPlanFound) @ %buildingInfo.city;
        %info = %space.owner.get();
        PlayerInfoMap;
        if (isObject(%info)) {
            %space.ownerAge = StripMLControlChars(%info.age);
            if ((%space.ownerAge $= "")) {
                %space.ownerAge = "-";
            }
            %space.ownerAge = %space.ownerAge @ " " @ "yrs";
            %space.ownerSex = %info.gender;
            if ((%space.ownerSex $= "f")) {
                %space.ownerSex = "F";
            }
            if ((%space.ownerSex $= "m")) {
                %space.ownerSex = "M";
            }
            %space.ownerSex = "-";
            %space.ownerLocation = StripMLControlChars(%info.location);
            if ((%space.ownerLocation $= "")) {
                %space.ownerLocation = "(hidden)";
            }
        }
        %space.ownerAge = "-";
        %space.ownerSex = "-";
        %space.ownerLocation = "-";
        %type = %space.type;
        if (($Player::Name $= %space.owner)) {
            %type = "MYPLACE";
            if (("aptCategoriesInUse" @ CustomSpacesSelector >= findField(%space.vars, %type))) {
                %listBox = %type.getListBox();
                CustomSpacesSelector;
            }
            %listBox = %type.addHeaderAndListBoxes();
            CustomSpacesSelector;
            %listBox.saveListBox(%type);
            %myPlaceIsInThisBuilding = 1;
            CustomSpacesSelector;
        }
        if (!(0.0 @ " " @ $Player::Name $= %space.owner)) {
        }
        if (%space.isFeatured) {
            if (%space.isFeatured) {
                %type = "FEATURED";
            }
            if (("aptCategoriesInUse" @ CustomSpacesSelector >= findField(%space.vars, %type))) {
                %listBox = %type.getListBox();
                CustomSpacesSelector;
            }
            %listBox = %type.addHeaderAndListBoxes();
            CustomSpacesSelector;
            %listBox.saveListBox(%type);
        }
        %listBox.updateEntry(%space);
        %space.vars = CustomSpacesSelector @ "" TAB "descriptions" @ %space.name @ CustomSpacesSelector;
        0.0;
        %i = (1.0 + %i);
    }
    CustomSpacesSelector.rearrangeListBoxes();
    if (0) {
        if (!((%spacesCount < %i) @ " " @ $Player::myPlaceVURL $= "")) {
        }
        if ((0.0 != $CSSpaceInfo)) {
        }
        %amAtHome = (0.0 == stricmp($CSSpaceInfo.owner, $player.getShapeName()));
        !(CustomSpacesSelector_MYPLACE_container @ " " @ $Player::myPlaceVURL $= "").setVisible();
        %style = %amAtHome ? "CSProfileSpecialLinkDisabled" : "CSProfileSpecialLink";
        %alpha = %amAtHome ? 85 : 255;
        %style.applyBaseTextWithStyle();
        !(%amAtHome).setActive();
        $CSSpaceInfo.modulationColor = "255 255 255" @ " " @ %alpha @ CustomSpacesSelector_MYPLACEIcon;
        CustomSpacesSelector_MYPLACE;
    }
    if (("aptCategoryCount" @ CustomSpacesSelector == $CSSpaceInfo.vars)) {
        0.setVisible();
        1.setVisible();
    }
    %buildingDir.delete();
};
function customSpaceSelFailed(%building) {
    0.setVisible();
    0.setVisible();
    if (!(CSLegendContainer @ " " @ %building $= "")) {
        if ((%building[$gMlStyle @ "CSProfileTitleText"] @ " " @ Buildings::GetDescription(%building) $= "")) {
        }
        %titleBarText = CustomSpacesSelector_LOADING @ %building @ Buildings::GetDescription(%building) @ " " @ "building directory";
        %titleBarText.setText();
    }
    "Building Directory".setText();
    1.setVisible();
    "Could not connect! Please try back later.".setTextAndUpdate();
};
function CustomSpacesSelector_MYPLACE::onURL(%this, %url) {
    %url = getWords(%url, 1);
    if ((%url $= "MY_PLACE")) {
        "myplace".openToTabName();
    }
};
function CustomSpacesSelector_RETURNTOLOBBY::onURL(%this, %url) {
    %url = getWords(%url, 1);
    if ((%url $= "EXIT_TO_LOBBY")) {
        MessageBoxYesNo(%url[$MsgCat::custSpacSel @ "RETURN-TO-LOBBY-TITLE"], , "CustomSpacesSelector.doReturnToLobby();", "");
    }
};
function CustomSpacesSelector::doReturnToLobby(%this) {
    %this.doTeleportToSpace(Buildings::getReturnVURL($CSBuildingName), "Lobby", "");
};
function CustomSpacesSelector_CLOSEWINDOW::onURL(%this, %url) {
    %url = getWords(%url, 1);
    if ((%url $= "CLOSE_WINDOW")) {
        CustomSpacesSelector.close();
    }
};
function CSSelectorLineVisitNowMLText::onURL(%this, %url) {
    if ((%url $= "gamelink go")) {
        %this.getParent().onMouseDown();
        CSSelectorListCtrl.teleportToSelected();
    }
};
function CustomSpacesSelector::refresh(%this) {
    if (buildingDirectoryMap.isActive()) {
        buildingDirectoryMap.restoreAllOthers();
    }
    %this.open(%this.buildingName);
    1.setVisible();
};
function CustomSpacesSelector_FILTER::onKeyDown(%this, %unused, %unused) {
    0.setVisible();
    return 0;
};
function CustomSpacesSelector_FILTER::onKeyUp(%this, %unused, %unused) {
    %fieldIsVisible = %this.isVisible();
    (CustomSpacesSelector_FILTER_OVERLAY @ " " @ %this.getValue() $= "").setVisible();
    %this.getValue().doFilter();
    return 0;
};
function CustomSpacesSelector::doFilter(%this, %filterByText) {
    %this.filterByText = %filterByText;
    %this.adjustListBoxPositions(0);
};
function CSSelectorCtrl::includeSpaceInList(%this, %entry, %filterByText) {
    if (!(isObject(%entry))) {
        return 0;
    }
    if (!(%entry.type $= "RESIDENCE")) {
        return 1;
    }
    if (%entry.isFeatured) {
        return 1;
    }
    if (($Player::Name $= %entry.owner)) {
        return 1;
    }
    %filterByText = strlwr(%filterByText);
    if ((%filterByText $= "")) {
        return 1;
    }
    if ((-(1.0) > strstr(strlwr(%entry.owner), %filterByText))) {
        return 1;
    }
    if ((-(1.0) > strstr(strlwr(%entry.description), %filterByText))) {
        return 1;
    }
    return 0;
};
