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
    vars;
    %listBox = new GuiArray2Ctrl("") {
        profile = 0 @ "CSProfileListBox";
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
        extent = ((vars + "textAreaWidth" @ CustomSpacesSelector) - (%windowWidth - 750.0)) @ " " @ "headerboxHeight" @ CustomSpacesSelector @ vars;
        minExtent = "textAreaIndentation" @ CustomSpacesSelector @ "1 1";
        sluggishness = -1;
        visible = 1;
        scroll = "CSSelectorScrollCtrl";
    };
    "MenuControl".bindClassName(%listBox);
    "TabbedTextControl".bindClassName(%listBox);
    "CSSelectorCtrl".bindClassName(%listBox);
    %fieldWidths = "90 16" @ " " @ (197.0 + (%windowWidth - 750.0)) @ " " @ "25 0 68";
    %listBox.descriptionFieldNumber = 2;
    if ((%type $= "MODEL")) {
        %listBox.unselectedProfile = "CSProfileModelListingUnselected";
        %listBox.selectedProfile = "CSProfileModelListingSelected";
        %listBox.menuTextProfile = "CSProfileModelListingMenuText";
        %listBox.menuTextSelectedProfile = "CSProfileModelListingMenuTextSelected";
        %fieldWidths = 111 @ " " @ (227.0 + (%windowWidth - 750.0)) @ " " @ "0 68";
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
        %listBox.entriesSet = 0 @ new SimSet("");;
        if (isObject(MissionCleanup)) {
            %listBox.entriesSet.add(MissionCleanup);
        }
    }
    %listBox.vars.setFieldWidths(%listBox, %fieldWidths, "spacesColumnPadding" @ CustomSpacesSelector);
    %listBox.clear();
    return %listBox;
};
function CSSelectorCtrl::refreshFromSet(%this, %doSort) {
    %this.clear();
    %count = %this.entriesSet.getCount();
    if (%doSort) {
        1.sortEntries(%this, "owner");
        0.sortEntries(%this, "occupancy");
        1.sortEntries(%this, "access");
    }
    %nameFieldWidth = getWord(%this.fieldWidths, 0);
    %descriptionFieldWidth = 10000;
    %n = 0;
    while ((%n < %count)) {
        %entry = %n.getObject(%this.entriesSet);
        if (!(%this.filterByText.includeSpaceInList(%this, %entry, CustomSpacesSelector))) {
        }
        %amCurrentlyHere = (%entry.name $= $CSSpaceName);
        %isFriend = %entry.owner.ownerIsFriend(%this);
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
        if ((stricmp(%entry.access, "PasswordProtected") == 0.0)) {
        }
        if ((stricmp(%entry.access, "Locked") == 0.0)) {
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
        %line = %lineText.addLine(%this);
        %line.entryName = %entry.name;
        %line.horizSizing = "width";
        if ((%this.descriptionFieldNumber != -(1.0))) {
            %this.descriptionFieldNumber.getObject(%line).horizSizing = "width";
            %i = (%line.getCount() - 1.0);
            while ((%i > %this.descriptionFieldNumber)) {
                %i.getObject(%line).horizSizing = "left";
                %i = (%i - 1.0);
            }
        }
        if ((%accessIconIndex != -(1.0))) {
            %position = %accessIconIndex.getObject(%line).getPosition();
            (%i > %this.descriptionFieldNumber);
            %position = getWord(%position, 0) @ " " @ (getWord(%position, 1) - 1.0);
            %line.accessIcon = %position.createAccessIcon(%this, %entry.access, %isFriend);
            if (!(%line.accessIcon $= "")) {
                %line.accessIcon.add(%line);
            }
        }
        if (!(%amCurrentlyHere)) {
            %visitNowTextBox = %visitNowIndex.getObject(%line);
            %visitNowTextBox.profile @ "Modal".setProfile(%visitNowTextBox);
            "CSSelectorLineVisitNowMLText".bindClassName(%visitNowTextBox);
        }
        %n = (%n + 1.0);
    }
    %extentWidth = getWord(CSSelectorListCtrl.getExtent(), 0);
    (%n < %count);
    %extentHeight = ((getWord(%this.getPosition(), 1) + getWord(%this.getExtent(), 1)) + %visitNowTextBox.vars);
    "textAreaPadding" @ CustomSpacesSelector;
    if ((%extentHeight < (%visitNowTextBox.vars - "scrollBarHeight" @ CustomSpacesSelector))) {
        %extentHeight = (%visitNowTextBox.vars - "scrollBarHeight" @ CustomSpacesSelector);
        2.0;
    }
    %extentHeight.resize(CSSelectorListCtrl, getWord(CSSelectorListCtrl.getPosition(), 0), getWord(CSSelectorListCtrl.getPosition(), 1), %extentWidth);
};
function CSSelectorCtrl::sortEntries(%this, %sortField, %increasing) {
    %set = %this.entriesSet;
    %num = %set.getCount();
    %sortArray = new Array("");;
    0;
    %n = 0;
    while ((%n < %num)) {
        %entry = %n.getObject(%set);
        %cmd = "%key = %entry." @ %sortField @ ";";
        eval(%cmd);
        if ((%sortField $= "access")) {
            if ((strlwr(%key) $= "friendsonly")) {
                %key = %entry.owner.ownerIsFriend(%this) ? 00 : 10;
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
        %entry.push_back(%sortArray, %key);
        %n = (%n + 1.0);
    }
    if (%increasing) {
        %sortArray.ssortka();
    }
    %sortArray.ssortkd();
    1.clear(%this.entriesSet);
    %count = %sortArray.count();
    (%n < %num);
    %i = 0;
    while ((%i < %count)) {
        %i.getValue(%sortArray).add(%this.entriesSet);
        %i = (%i + 1.0);
    }
    %sortArray.delete();
};
function CSSelectorCtrl::dumpSpaces(%this) {
    %set = %this.entriesSet;
    %num = %set.getCount();
    %n = 0;
    while ((%n < %num)) {
        %entry = %n.getObject(%set);
        echo(formatString("%20s", %entry.owner) @ " " @ formatInt("%6d", %entry.occupancy) @ " " @ %entry);
        %n = (%n + 1.0);
    }
};
function CSSelectorCtrl::createAccessIcon(%this, %accessType, %isFriend, %position) {
    %icon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %position;
        extent = "16 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    if ((stricmp(%accessType, "PasswordProtected") == 0.0)) {
    }
    if ((stricmp(%accessType, "Locked") == 0.0)) {
        if (%isFriend) {
            "platform/client/ui/buildingDir_key_green".setBitmap(%icon);
        }
        "platform/client/ui/buildingDir_key_white".setBitmap(%icon);
    }
    if ((stricmp(%accessType, "FriendsOnly") == 0.0)) {
        if (%isFriend) {
            "platform/client/ui/buildingDir_heart_green".setBitmap(%icon);
        }
        "platform/client/ui/buildingDir_heart_white".setBitmap(%icon);
    }
    %icon.delete();
    %icon = "";
    return %icon;
};
function CSSelectorCtrl::updateEntry(%this, %space) {
    %entry = %space.name.getEntryByName(%this);
    if (!(isObject(%entry))) {
        %entry = new ScriptObject("") {
            name = 0 @ %space.name;
        };
        if (isObject(MissionCleanup)) {
            %entry.add(MissionCleanup);
        }
        %entry.add(%this.entriesSet);
    }
    %entry.name = %space.name;
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
    %entry.audioStream = "" @ %space.audioStream.get($musicStreamIDMap);
    %entry.videoStream = %space.videoStream;
    return %entry;
};
function CSSelectorCtrl::deleteEntry(%this, %name) {
    %entry = %name.getEntryByName(%this);
    %entry.remove(%this.entriesSet);
    %entry.delete();
};
function CSSelectorCtrl::getEntryByName(%this, %name) {
    %n = (%this.entriesSet.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %entry = %n.getObject(%this.entriesSet);
        if ((%entry.name $= %name)) {
            return %entry;
        }
        %n = (%n - 1.0);
    }
    return -(1.0);
};
function CSSelectorCtrl::childSelected(%this, %child) {
};
function CSSelectorCtrl::onCreatedChild(%this, %child) {
    Parent::onCreatedChild(%this, %child);
    %position = %child.getPosition();
    %extent = %child.getExtent();
    %newHeight = (getWord(%extent, 1) + %this.spacing);
    %newHeight.resize(%child, getWord(%position, 0), getWord(%position, 1), getWord(%extent, 0));
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSSelectorLine")) {
        "CSSelectorLine".bindClassName(%child);
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
    return (%spaceOwner.getFriendStatus(BuddyHudWin) $= "friends");
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
    if (($gCustomSpacesDoubleClickSched != 0.0)) {
        cancel($gCustomSpacesDoubleClickSched);
        $gCustomSpacesDoubleClickSched = 0;
    }
    $gCustomSpacesDoubleClickSched = 500.schedule(%this);
    forgetFirstClick;
    %this.lastSelectedEntry = %this.entryName @ CustomSpacesSelector;
    %entry = %this.entryName.getEntryByName(%this.getParent());
    if (isObject(%entry)) {
        %entry.doGetDescription(%this);
        %amCurrentlyHere = (%entry.name $= $CSSpaceName);
        if ((%entry.type $= "MODEL")) {
            0.setVisible(CustomSpacesSelector_GOBUTTON);
            0.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
            !(%amCurrentlyHere).setActive(CustomSpacesSelector_VISITandBUYBUTTON);
            1.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
        }
        if (($Player::Name $= %entry.owner)) {
        }
        if ("customspaceMaster".rolesPermissionCheckNoWarn($player)) {
            0.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
            0.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
            !(%amCurrentlyHere).setActive(CustomSpacesSelector_GOBUTTON);
            1.setVisible(CustomSpacesSelector_GOBUTTON);
        }
        if ((stricmp(%entry.access, "PasswordProtected") == 0.0)) {
            0.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
            0.setVisible(CustomSpacesSelector_GOBUTTON);
            !(%amCurrentlyHere).setActive(CustomSpacesSelector_ENTERPASSWORDBUTTON);
            1.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
        }
        if ((stricmp(%entry.access, "Locked") == 0.0)) {
            0.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
            0.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
            0.setActive(CustomSpacesSelector_GOBUTTON);
            1.setVisible(CustomSpacesSelector_GOBUTTON);
        }
        if ((stricmp(%entry.access, "FriendOnly") == 0.0)) {
            0.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
            0.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
            if (!(%amCurrentlyHere)) {
            }
            %entry.owner.ownerIsFriend(%this).setActive(CustomSpacesSelector_GOBUTTON);
            1.setVisible(CustomSpacesSelector_GOBUTTON);
        }
        0.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
        0.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
        !(%amCurrentlyHere).setActive(CustomSpacesSelector_GOBUTTON);
        1.setVisible(CustomSpacesSelector_GOBUTTON);
    }
    0.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
    0.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
    0.setVisible(CustomSpacesSelector_GOBUTTON);
    "".setTextAndUpdate(CSSelectorDescriptionCtrl);
    Parent::onMouseDown(%this);
};
function CSSelectorLine::doGetDescription(%this, %entry) {
    %existingDesc = %entry.vars;
    "descriptions" @ %entry.name @ CustomSpacesSelector;
    if (!(%existingDesc $= "")) {
        %existingDesc.setTextAndUpdate(CSSelectorDescriptionCtrl);
        return;
    }
    if ((%entry.type $= "MODEL")) {
        %entry.setTextAndUpdateWithCallback(CSSelectorDescriptionCtrl, "(loading...)", %this, "doGetModelDescription");
    }
    %entry.setTextAndUpdateWithCallback(CSSelectorDescriptionCtrl, "(loading...)", %this, "doGetNonModelDescription");
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
    "".applyUrl(dlMgr, %url, "CSSelectorLine_gotDescriptionPhoto", "CSSelectorLine_gotDescriptionPhotoFailed", %this);
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
    %descText.setTextAndUpdate(CSSelectorDescriptionCtrl);
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
        %titleLineWidth = (getWord(CustomSpacesSelector_VISITandBUYBUTTON.getPosition(), 0) - 5.0);
        %pricingText = CSSpacePurchasePriceFormatting(%entry.floorplan.priceVPoints, %entry.floorplan.priceVBux);
        if ((%pricingText $= "")) {
            %pricingText = "Currently unavailable for purchase - check back soon!";
        }
        %desc = "<spush>" @ %pricingText[$gMlStyle @ "CSProfileDescriptionTitleModel"] @ chopTextToFitLineWidths(%entry.name, CSProfileDescriptionTitleModel, %titleLineWidth, "") @ "\n" @ %pricingText @ "\n\n" @ %pricingText[$gMlStyle @ "CSProfileDescriptionHeaderModel"] @ "Currently Playing:" @ " " @ %pricingText[$gMlStyle @ "CSProfileDescriptionHeaderModel"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ csGetCurrentlyPlaying(%entry.audioStream, %entry.videoStream) @ "\n\n" @ %bitmapText @ "\n" @ " " @ %bitmapText[$gMlStyle @ "CSProfileDescriptionTextNormal"] @ TryFixBadWords(%entry.longDescription) @ "<spop>";
    }
    if (%entry.isFeatured) {
    }
    if ((stricmp(%entry.access, "PasswordProtected") == 0.0)) {
    }
    if ((stricmp(%entry.access, "Locked") == 0.0)) {
    }
    %titleLineWidth = ((getWord(CustomSpacesSelector_GOBUTTON.getPosition(), 0) - getWord(CustomSpacesSelector_ENTERPASSWORDBUTTON.getPosition(), 0)) - 0.0);
    35.0;
    if (("<spush>" @ %entry.isFeatured ? "<bitmap:platform/client/ui/buildingDir_featured_star_lg>" : "" @ %entry[$gMlStyle @ "CSProfileDescriptionTitleNormal"] @ " " @ %entry.type $= "CELEBSPACE")) {
    }
    %desc = 5.0 @ %entry.name @ chopTextToFitLineWidths(%entry.owner @ "'s Pad", CSProfileDescriptionTitleNormal, %titleLineWidth, "") @ "\n" @ TryFixBadWords(%entry.description) @ "\n\n" @ "Currently Playing: " @ " " @ csGetCurrentlyPlaying(%entry.audioStream, %entry.videoStream) @ "\n\n" @ %bitmapText @ "\n" @ %bitmapText[$gMlStyle @ "CSProfileDescriptionHeaderNormal"] @ "vURL: " @ " " @ %bitmapText[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ vurlClearResolution(%entry.vurl) @ "<a: ></a>" @ " " @ "(<a:COPY_VURL>copy</a>)" @ "\n\n" @ " " @ TryFixBadWords(%entry.longDescription) @ "<spop>";
    return %desc;
};
function CSSelectorLine::onHilite(%this) {
    %this.Parent.selectedProfile.setProfile(%this);
    %this.Parent.setSelectedList(CSSelectorListCtrl);
    %doScroll = 1;
    %scrollHeight = getWord(CSSelectorScrollCtrl.getExtent(), 1);
    %listHeight = getWord(CSSelectorListCtrl.getExtent(), 1);
    if ((%listHeight <= %scrollHeight)) {
        return;
    }
    %listTop = getWord(CSSelectorListCtrl.getPosition(), 1);
    %extremeEdge = ((getWord(%this.getParent().getPosition(), 1) + getWord(%this.getPosition(), 1)) - 2.0);
    if (((%listTop + %extremeEdge) >= 0.0)) {
        %extremeEdge = (%extremeEdge + ((getWord(%this.getExtent(), 1) - %scrollHeight) + 4.0));
        if (((%listTop + %extremeEdge) <= 0.0)) {
            %doScroll = 0;
        }
    }
    if (%doScroll) {
        %extremeEdge.scrollTo(CSSelectorScrollCtrl, 0);
    }
};
function CSSelectorLine::onUnhilite(%this) {
    Parent::onUnhilite(%this);
};
function CSSelectorDescriptionCtrl::setTextAndUpdate(%this, %text) {
    "".setTextAndUpdateWithCallback(%this, %text, 0, "");
};
function CSSelectorDescriptionCtrl::setTextAndUpdateWithCallback(%this, %text, %callbackObject, %callbackFunction, %callbackParameters) {
    %text.setText(%this);
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
    if (($gCustomSpacesKeyStrokeRepeatSched != 0.0)) {
        cancel($gCustomSpacesKeyStrokeRepeatSched);
        $gCustomSpacesKeyStrokeRepeatSched = 0;
    }
};
function CustomSpacesSelector::fromKeyStartMovingUp(%this) {
    %selectedListBox = CSSelectorListCtrl.getSelectedList();
    if (!(isObject(%selectedListBox))) {
        return;
    }
    %indexOfHilitedCell = %selectedListBox.getHilitedCell().getObjectIndex(%selectedListBox);
    %lastIndexPossible = (%selectedListBox.getCount() - 1.0);
    if ((%indexOfHilitedCell == 0.0)) {
        if ((%selectedListBox.myIndex > 0.0)) {
            %nextListBox = %selectedListBox.vars;
            "aptCategoryListBoxes" @ (%selectedListBox.myIndex - 1.0) @ CustomSpacesSelector;
            (%nextListBox.getCount() - 1.0).getObject(%nextListBox).onMouseDown();
        }
        CSSelectorScrollCtrl.scrollToTop();
    }
    (%indexOfHilitedCell - 1.0).getObject(%selectedListBox).onMouseDown();
    $gCustomSpacesKeyStrokeRepeatSched = $gCustomSpacesKeyStrokeRepFrequency.schedule(%this);
    fromKeyStartMovingUp;
};
function CustomSpacesSelector::fromKeyStartMovingDown(%this) {
    %selectedListBox = CSSelectorListCtrl.getSelectedList();
    if (!(isObject(%selectedListBox))) {
        if ((%selectedListBox.vars > "aptCategoryCount" @ CustomSpacesSelector)) {
            0.getObject(%selectedListBox.vars).onMouseDown();
        }
        return 0.0 TAB "aptCategoryListBoxes" @ 0 @ CustomSpacesSelector;
    }
    %indexOfHilitedCell = %selectedListBox.getHilitedCell().getObjectIndex(%selectedListBox);
    %lastIndexPossible = (%selectedListBox.getCount() - 1.0);
    if ((%indexOfHilitedCell == %lastIndexPossible)) {
        if ((%selectedListBox.myIndex < (%selectedListBox.vars - "aptCategoryCount" @ CustomSpacesSelector))) {
            %nextListBox = %selectedListBox.vars;
            1.0 TAB "aptCategoryListBoxes" @ (%selectedListBox.myIndex + 1.0) @ CustomSpacesSelector;
            0.getObject(%nextListBox).onMouseDown();
        }
    }
    (%indexOfHilitedCell + 1.0).getObject(%selectedListBox).onMouseDown();
    $gCustomSpacesKeyStrokeRepeatSched = $gCustomSpacesKeyStrokeRepFrequency.schedule(%this);
    fromKeyStartMovingDown;
};
function CSSelectorDescriptionCtrl::onURL(%this, %text) {
    if ((%text $= "COPY_VURL")) {
        %entryName = CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName;
        %entry = %entryName.getEntryByName(CSSelectorListCtrl.getSelectedList());
        setClipboard(vurlClearResolution(%entry.vurl));
    }
};
function CSSelectorListCtrl::getSelectedList(%this) {
    return %this.selectedCSSelectorCtrl;
};
function CSSelectorListCtrl::setSelectedList(%this, %listBox) {
    if (isObject(%this.selectedCSSelectorCtrl)) {
        if ((%this.selectedCSSelectorCtrl.getId() == %listBox.getId())) {
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
    %entry = %entryName.getEntryByName(%selectorCtrl);
    if (!($Player::Name $= %entry.owner)) {
    }
    if (!("customspaceMaster".rolesPermissionCheckNoWarn($player))) {
        if ((stricmp(%entry.access, "FriendsOnly") == 0.0)) {
        }
        if (!(%entry.owner.ownerIsFriend(%selectorCtrl))) {
            MessageBoxOK(, , "");
            return;
        }
        if ((stricmp(%entry.access, "PasswordProtected") == 0.0)) {
            MessageBoxTextEntryWithCancel(, , CSSelectorListCtrl_tackOnPassword, "", 0);
            return;
        }
        if ((stricmp(%entry.access, "Locked") == 0.0)) {
            MessageBoxOK(, , "");
            return;
        }
    }
    "".teleportToSpaceName(%this, %entryName);
};
function CSSelectorListCtrl_tackOnPassword(%password) {
    %entryName = CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName;
    %password.teleportToSpaceName(CSSelectorListCtrl, %entryName);
};
function CustomSpacesSelector::doTeleportToMyApartment(%this) {
    if (!($Player::myPlaceVURL $= "")) {
        "".doTeleportToSpace(%this, $Player::myPlaceVURL, "");
    }
};
function CSSelectorListCtrl::teleportToSpaceName(%this, %spaceName, %password) {
    %selectedCSSelectorCtrl = %this.getSelectedList();
    if (!(isObject(%selectedCSSelectorCtrl))) {
        error("error in retrieving list of space entries <- " @ getScopeName());
        return;
    }
    %space = %spaceName.getEntryByName(%selectedCSSelectorCtrl);
    if (!(isObject(%space))) {
        error("error in retrieving object record of space entry in list <- " @ getScopeName());
        return;
    }
    %password.doTeleportToSpace(CustomSpacesSelector, %space.vurl, %space.name);
};
function CustomSpacesSelector::doTeleportToSpace(%this, %vurl, %spaceName, %password) {
    if (!(%spaceName $= "")) {
        showTransitionMessage(%spaceName, 0);
    }
    %vurlObject = vurlGetParsedVurl(%vurl);
    %password.setPassword(%vurlObject);
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
    if ((stricmp(%errorCode, "missingdoorcode") == 0.0)) {
    }
    if ((stricmp(%errorCode, "missingpassword") == 0.0)) {
    }
    if ((stricmp(%errorCode, "incorrectdoorcode") == 0.0)) {
    }
    if ((stricmp(%errorCode, "incorrectpassword") == 0.0)) {
        MessageBoxTextEntryWithCancel(, , CSSelectorListCtrl_tackOnPassword, "", 0);
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
    %this.vars = "textAreaPadding" @ ((getWord(CSSelectorScrollCtrl.getExtent(), 0) - %this.vars) - (2.0 * %this.vars) @ "scrollBarWidth") @ "textAreaWidth";
    0.setVisible(CustomSpacesSelector_TITLE);
    0.setVisible(CustomSpacesSelector_NO_APARTMENTS);
    0.setVisible(CustomSpacesSelector_VISITandBUYBUTTON);
    0.setVisible(CustomSpacesSelector_GOBUTTON);
    0.setVisible(CustomSpacesSelector_ENTERPASSWORDBUTTON);
    !(CustomSpacesSelector_RETURNTOLOBBY @ " " @ CustomSpaceClient::GetSpaceImIn() $= "").setVisible();
    1.setVisible(CustomSpacesSelector_LOADING);
    1.setVisible(%this.container);
    %this.container.focusAndRaise(PlayGui);
    if ($ETS::devMode) {
    }
    if ($gGetFakeBuildingDirectory) {
        "".setTextAndUpdateWithCallback(CSSelectorDescriptionCtrl, "(loading...)", %this, "getFakeBuildingDirectory");
    }
    %building @ ", customSpaceSelGotData, customSpaceSelFailed".setTextAndUpdateWithCallback(CSSelectorDescriptionCtrl, "(loading...)", "", "getBuildingDirectory");
    CustomSpacesSelector_MYPLACE.applyBaseText();
    CustomSpacesSelector_RETURNTOLOBBY.applyBaseText();
};
function CustomSpacesSelector::close(%this) {
    %this.buildingName = "";
    0.setVisible(%this.container);
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
    %headerText = new GuiBitmapCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "textTitleLeft" @ %this.vars @ " " @ "textTitleTop" @ %this.vars;
        extent = "120 9";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    if ((%type $= "MODEL")) {
        "platform/client/ui/buildingDir_check_out_the_models".setBitmap(%headerText);
    }
    if ((%type $= "FEATURED")) {
        "platform/client/ui/buildingDir_featured_apartments".setBitmap(%headerText);
    }
    if ((%type $= "CELEBSPACE")) {
        "platform/client/ui/buildingDir_celebrity_apartments".setBitmap(%headerText);
    }
    if ((%type $= "MYPLACE")) {
        "platform/client/ui/buildingDir_my_apartment".setBitmap(%headerText);
    }
    if ((%type $= "RESIDENCE")) {
        "platform/client/ui/buildingDir_resident_apartments".setBitmap(%headerText);
    }
    "".setBitmap(%headerText);
    %headerBox = new GuiControl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "textAreaPadding" @ %this.vars @ " " @ "aptCategoryCount" @ %this.vars.calculateHeaderTop(%this);
        extent = (getWord(%headerText.getExtent(), 0) + 3.0) @ " " @ "headerboxHeight" @ %this.vars;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 64;
    };
    %headerText.add(%headerBox);
    %headerBox.add(CSSelectorListCtrl);
    %this.vars = %headerBox TAB "aptCategoryHeaderBoxes" @ %newHeaderAndListBoxIndex;
    %listBox = CSSelectorCtrl::makeNewListBox(%type);
    %positionLeft = (getWord(%headerBox.getPosition(), 0) + %this.vars);
    "textAreaIndentation" @ CustomSpacesSelector;
    %positionTop = ((getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1)) + %this.vars);
    "spaceBetweenHeaderAndListBoxes";
    %extentWidth = getWord(%listBox.getExtent(), 0);
    %extentHeight = getWord(%listBox.getExtent(), 1);
    %extentHeight.resize(%listBox, %positionLeft, %positionTop, %extentWidth);
    %listBox.add(CSSelectorListCtrl);
    %this.vars = %listBox TAB "aptCategoryListBoxes" @ %newHeaderAndListBoxIndex;
    %listBox.myIndex = %newHeaderAndListBoxIndex;
    if (("aptCategoriesInUse" @ CustomSpacesSelector @ " " @ %listBox.vars $= "")) {
        %listBox.vars = %type @ "aptCategoriesInUse" @ CustomSpacesSelector;
    }
    %listBox.vars = %listBox.vars @ "\t" @ %type @ "aptCategoriesInUse" @ CustomSpacesSelector;
    "aptCategoriesInUse" @ CustomSpacesSelector;
    %this.vars = %newHeaderAndListBoxIndex TAB "aptCategoryIndexes" @ %type;
    %this.vars = (%newHeaderAndListBoxIndex + 1.0) @ "aptCategoryCount";
    return %listBox;
};
function CustomSpacesSelector::calculateHeaderTop(%this, %ordinal) {
    if ((%ordinal < 0.0)) {
        %ordinal = 0;
    }
    if ((%ordinal > %this.vars)) {
        %ordinal = %this.vars;
        "aptCategoryCount" @ "aptCategoryCount";
    }
    if ((%ordinal == 0.0)) {
        %top = %this.vars;
        "textAreaPadding";
    }
    %top = getWord(%this.vars.getPosition("aptCategoryListBoxes" @ (%ordinal - 1.0)), 1);
    %top = (%top + getWord(%this.vars.getExtent("aptCategoryListBoxes" @ (%ordinal - 1.0)), 1));
    %top = (%top + %this.vars);
    "textAreaPadding";
    %top = (%top + %this.vars);
    "textAreaInterCategoryPadding";
    return %top;
};
function CustomSpacesSelector::clearHeaderAndListBoxes(%this) {
    CSSelectorListCtrl.clear();
    %i = 0;
    while ((%i < %this.vars)) {
        %this.vars.delete();
        %this.vars = "aptCategoryCount" @ CustomSpacesSelector TAB "aptCategoryHeaderBoxes" @ %i @ "" TAB "aptCategoryHeaderBoxes" @ %i;
        %this.vars.delete("aptCategoryListBoxes" @ %i);
        %this.vars = "" TAB "aptCategoryListBoxes" @ %i;
        %i = (%i + 1.0);
    }
    %this.vars = (%i < %this.vars) @ 0 @ "aptCategoryCount";
    "aptCategoryCount" @ CustomSpacesSelector;
    %this.vars = "" @ "aptCategoriesInUse";
};
function CustomSpacesSelector::setTypeIndexes(%this, %typeList) {
    %currentIndex = 0;
    %i = 0;
    while ((%i < getFieldCount("aptCategories", %this.vars))) {
        %type = getField("aptCategories", %this.vars, %i);
        if ((findField(%typeList, %type) >= 0.0)) {
            %this.vars = %currentIndex TAB "aptCategoryIndexes" @ %type;
            %currentIndex = (%currentIndex + 1.0);
        }
        %i = (%i + 1.0);
    }
};
function CustomSpacesSelector::getTypeIndex(%this, %type) {
    %index = %this.vars;
    "aptCategoryIndexes" @ %type;
    return %index;
};
function CustomSpacesSelector::getListBox(%this, %type) {
    %listBox = %this.vars;
    "aptCategoryListBoxes" @ %type.getTypeIndex(%this);
    return %listBox;
};
function CustomSpacesSelector::saveListBox(%this, %listBox, %type) {
    %this.vars = %listBox TAB "aptCategoryListBoxes" @ %type.getTypeIndex(%this);
};
function CustomSpacesSelector::rearrangeListBoxes(%this) {
    %typesAvailable = %this.vars;
    "aptCategories";
    %typesInUse = %this.vars;
    "aptCategoriesInUse";
    %numTypesAvailable = getFieldCount(%typesAvailable);
    %typesSkipped = 0;
    %i = 0;
    while ((%i < (%numTypesAvailable - 1.0))) {
        %currentTypeToExamine = getField(%typesAvailable, %i);
        %indexOfCurrentType = findField(%typesInUse, %currentTypeToExamine);
        if ((%indexOfCurrentType == (%i - %typesSkipped))) {
        }
        if ((%indexOfCurrentType == -(1.0))) {
            %typesSkipped = (%typesSkipped + 1.0);
        }
        %indexOfWhereTypeShouldBe = (findField(%typesAvailable, %currentTypeToExamine) - %typesSkipped);
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
        %i = (%i + 1.0);
    }
    %this.vars = (%i < (%numTypesAvailable - 1.0)) @ %typesAvailable @ "aptCategories";
    %this.vars = %typesInUse @ "aptCategoriesInUse";
    %this.vars.setTypeIndexes(%this, "aptCategoriesInUse");
    1.adjustListBoxPositions(%this);
};
function CustomSpacesSelector::adjustListBoxPositions(%this, %doSort) {
    if ((%this.vars < 1.0 @ "aptCategoryCount")) {
        return;
    }
    %lastListBoxBottomEdgeY = 0;
    %i = 0;
    while ((%i < %this.vars)) {
        %headerBox = %this.vars;
        "aptCategoryCount" TAB "aptCategoryHeaderBoxes" @ %i;
        %listBox = %this.vars;
        "aptCategoryListBoxes" @ %i;
        %i.calculateHeaderTop(%this).reposition(%headerBox, "textAreaPadding", %this.vars);
        %doSort.refreshFromSet(%listBox);
        %positionLeft = (getWord(%headerBox.getPosition(), 0) + %this.vars);
        "textAreaIndentation" @ CustomSpacesSelector;
        %positionTop = ((getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1)) + %this.vars);
        "spaceBetweenHeaderAndListBoxes";
        %extentWidth = (%this.vars - "textAreaWidth" @ CustomSpacesSelector);
        %this.vars;
        %extentHeight = getWord(%listBox.getExtent(), 1);
        "textAreaIndentation" @ CustomSpacesSelector;
        %extentHeight.resize(%listBox, %positionLeft, %positionTop, %extentWidth);
        %lastListBoxBottomEdgeY = (%positionTop + getWord(%listBox.getExtent(), 1));
        %i = (%i + 1.0);
    }
    %newHeight = (%lastListBoxBottomEdgeY + %this.vars);
    (%i < %this.vars) @ "textAreaPadding";
    %newHeight.resize(CSSelectorListCtrl, getWord(CSSelectorListCtrl.getPosition(), 0), getWord(CSSelectorListCtrl.getPosition(), 1), getWord(CSSelectorListCtrl.getExtent(), 0));
};
function CustomSpacesSelector::getHeaderBox(%this, %type) {
    return %this.vars;
};
function customSpaceSelGotData(%buildingInfo, %buildingDir) {
    0.setVisible(CustomSpacesSelector_LOADING);
    1.setVisible(CSLegendContainer);
    if (!(%buildingInfo.name $= "")) {
        if ((%buildingInfo[$gMlStyle @ "CSProfileTitleText"] @ " " @ Buildings::GetDescription(%buildingInfo.name) $= "")) {
        }
        %titleBarText = %buildingInfo.name @ Buildings::GetDescription(%buildingInfo.name) @ " " @ "building directory";
        %titleBarText.setText(CustomSpacesSelector_TITLE);
    }
    "Building Directory".setText(CustomSpacesSelector_TITLE);
    1.setVisible(CustomSpacesSelector_TITLE);
    %buildingInfo.description.schedule(CSSelectorDescriptionCtrl, 50, setTextAndUpdate);
    CustomSpacesSelector.clearHeaderAndListBoxes();
    %buildingInfo.myApartment = 0 @ CustomSpacesSelector;
    %myPlaceIsInThisBuilding = 0;
    %spacesCount = %buildingDir.getCount();
    if ((%spacesCount == 0.0)) {
        0.setVisible(CSSelectorListCtrl);
        1.setVisible(CustomSpacesSelector_NO_APARTMENTS);
    }
    0.setVisible(CustomSpacesSelector_NO_APARTMENTS);
    1.setVisible(CSSelectorListCtrl);
    %i = 0;
    while ((%i < %spacesCount)) {
        %space = %i.getObject(%buildingDir);
        %floorPlanFound = 0;
        %j = 0;
        if ((%j < %buildingInfo.floorPlanCount)) {
        }
        while (!(%floorPlanFound)) {
            %floorPlanFound = (%space.floorPlanName @ %j $= %buildingInfo.floorplan.name);
            %j = (%j + 1.0);
            if ((%j < %buildingInfo.floorPlanCount)) {
            }
        }
        if (!(%floorPlanFound)) {
            warn("customSpaceSelGotData() got listing for apartment '" @ %space.name @ "' with owner '" @ %space.owner @ "' that refers to non-existent floorPlan '" @ %space.floorPlanName @ "'");
        }
        %space.city = !(%floorPlanFound) @ %buildingInfo.city;
        %info = %space.owner.get(PlayerInfoMap);
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
            if ((findField("aptCategoriesInUse" @ CustomSpacesSelector, %space.vars, %type) >= 0.0)) {
                %listBox = %type.getListBox(CustomSpacesSelector);
            }
            %listBox = %type.addHeaderAndListBoxes(CustomSpacesSelector);
            %type.saveListBox(CustomSpacesSelector, %listBox);
            %myPlaceIsInThisBuilding = 1;
        }
        if (!($Player::Name $= %space.owner)) {
        }
        if (%space.isFeatured) {
            if (%space.isFeatured) {
                %type = "FEATURED";
            }
            if ((findField("aptCategoriesInUse" @ CustomSpacesSelector, %space.vars, %type) >= 0.0)) {
                %listBox = %type.getListBox(CustomSpacesSelector);
            }
            %listBox = %type.addHeaderAndListBoxes(CustomSpacesSelector);
            %type.saveListBox(CustomSpacesSelector, %listBox);
        }
        %space.updateEntry(%listBox);
        %space.vars = "" TAB "descriptions" @ %space.name @ CustomSpacesSelector;
        %i = (%i + 1.0);
    }
    CustomSpacesSelector.rearrangeListBoxes();
    if (0) {
        if (!((%i < %spacesCount) @ " " @ $Player::myPlaceVURL $= "")) {
        }
        if (($CSSpaceInfo != 0.0)) {
        }
        %amAtHome = (stricmp($CSSpaceInfo.owner, $player.getShapeName()) == 0.0);
        !(CustomSpacesSelector_MYPLACE_container @ " " @ $Player::myPlaceVURL $= "").setVisible();
        %style = %amAtHome ? "CSProfileSpecialLinkDisabled" : "CSProfileSpecialLink";
        %alpha = %amAtHome ? 85 : 255;
        %style.applyBaseTextWithStyle(CustomSpacesSelector_MYPLACE);
        !(%amAtHome).setActive(CustomSpacesSelector_MYPLACE);
        $CSSpaceInfo.modulationColor = "255 255 255" @ " " @ %alpha @ CustomSpacesSelector_MYPLACEIcon;
    }
    if (($CSSpaceInfo.vars == "aptCategoryCount" @ CustomSpacesSelector)) {
        0.setVisible(CSSelectorListCtrl);
        1.setVisible(CustomSpacesSelector_NO_APARTMENTS);
    }
    %buildingDir.delete();
};
function customSpaceSelFailed(%building) {
    0.setVisible(CustomSpacesSelector_LOADING);
    0.setVisible(CSLegendContainer);
    if (!(%building $= "")) {
        if ((%building[$gMlStyle @ "CSProfileTitleText"] @ " " @ Buildings::GetDescription(%building) $= "")) {
        }
        %titleBarText = %building @ Buildings::GetDescription(%building) @ " " @ "building directory";
        %titleBarText.setText(CustomSpacesSelector_TITLE);
    }
    "Building Directory".setText(CustomSpacesSelector_TITLE);
    1.setVisible(CustomSpacesSelector_TITLE);
    "Could not connect! Please try back later.".setTextAndUpdate(CSSelectorDescriptionCtrl);
};
function CustomSpacesSelector_MYPLACE::onURL(%this, %url) {
    %url = getWords(%url, 1);
    if ((%url $= "MY_PLACE")) {
        "myplace".openToTabName(geTGF);
    }
};
function CustomSpacesSelector_RETURNTOLOBBY::onURL(%this, %url) {
    %url = getWords(%url, 1);
    if ((%url $= "EXIT_TO_LOBBY")) {
        MessageBoxYesNo(%url[$MsgCat::custSpacSel @ "RETURN-TO-LOBBY-TITLE"], , "CustomSpacesSelector.doReturnToLobby();", "");
    }
};
function CustomSpacesSelector::doReturnToLobby(%this) {
    "".doTeleportToSpace(%this, Buildings::getReturnVURL($CSBuildingName), "Lobby");
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
    %this.buildingName.open(%this);
    1.setVisible(CustomSpacesSelector_TITLE);
};
function CustomSpacesSelector_FILTER::onKeyDown(%this, %unused, %unused) {
    0.setVisible(CustomSpacesSelector_FILTER_OVERLAY);
    return 0;
};
function CustomSpacesSelector_FILTER::onKeyUp(%this, %unused, %unused) {
    %fieldIsVisible = %this.isVisible();
    (CustomSpacesSelector_FILTER_OVERLAY @ " " @ %this.getValue() $= "").setVisible();
    %this.getValue().doFilter(CustomSpacesSelector);
    return 0;
};
function CustomSpacesSelector::doFilter(%this, %filterByText) {
    %this.filterByText = %filterByText;
    0.adjustListBoxPositions(%this);
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
    if ((strstr(strlwr(%entry.owner), %filterByText) > -(1.0))) {
        return 1;
    }
    if ((strstr(strlwr(%entry.description), %filterByText) > -(1.0))) {
        return 1;
    }
    return 0;
};
