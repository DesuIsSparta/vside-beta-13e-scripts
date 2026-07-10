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
    %windowWidth = getWord(getExtent(), 0);
    CustomSpacesSelector;
    profile = GuiArray2Ctrl @ new ""() @ "CSProfileListBox";
    0;
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
    extent = "textAreaIndentation" @ CustomSpacesSelector @ (vars - ((750.0 - %windowWidth) @ "textAreaWidth" @ CustomSpacesSelector + vars)) @ " " @ "headerboxHeight" @ CustomSpacesSelector @ vars;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    scroll = "CSSelectorScrollCtrl";
    %listBox = ;
    %listBox.bindClassName("MenuControl");
    %listBox.bindClassName("TabbedTextControl");
    %listBox.bindClassName("CSSelectorCtrl");
    %fieldWidths = "90 16" @ " " @ ((750.0 - %windowWidth) + 197.0) @ " " @ "25 0 68";
    descriptionFieldNumber = 2 @ %listBox;
    if ((%type $= "MODEL")) {
        unselectedProfile = "CSProfileModelListingUnselected" @ %listBox;
        selectedProfile = "CSProfileModelListingSelected" @ %listBox;
        menuTextProfile = "CSProfileModelListingMenuText" @ %listBox;
        menuTextSelectedProfile = "CSProfileModelListingMenuTextSelected" @ %listBox;
        %fieldWidths = 111 @ " " @ ((750.0 - %windowWidth) + 227.0) @ " " @ "0 68";
        descriptionFieldNumber = 1 @ %listBox;
    }
    if ((%type $= "FEATURED")) {
        unselectedProfile = "CSProfileFeaturedListingUnselected" @ %listBox;
        selectedProfile = "CSProfileFeaturedListingSelected" @ %listBox;
        menuTextProfile = "CSProfileFeaturedListingMenuText" @ %listBox;
        menuTextSelectedProfile = "CSProfileFeaturedListingMenuTextSelected" @ %listBox;
    }
    if ((%type $= "CELEBSPACE")) {
        unselectedProfile = "CSProfileCelebListingUnselected" @ %listBox;
        selectedProfile = "CSProfileCelebListingSelected" @ %listBox;
        menuTextProfile = "CSProfileCelebListingMenuText" @ %listBox;
        menuTextSelectedProfile = "CSProfileCelebListingMenuTextSelected" @ %listBox;
    }
    if ((%type $= "MYPLACE")) {
        unselectedProfile = "CSProfileNormalListingUnselected" @ %listBox;
        selectedProfile = "CSProfileNormalListingSelected" @ %listBox;
        menuTextProfile = "CSProfileNormalListingMenuText" @ %listBox;
        menuTextSelectedProfile = "CSProfileNormalListingMenuTextSelected" @ %listBox;
    }
    if ((%type $= "RESIDENCE")) {
        unselectedProfile = "CSProfileNormalListingUnselected" @ %listBox;
        selectedProfile = "CSProfileNormalListingSelected" @ %listBox;
        menuTextProfile = "CSProfileNormalListingMenuText" @ %listBox;
        menuTextSelectedProfile = "CSProfileNormalListingMenuTextSelected" @ %listBox;
    }
    unselectedProfile = "GuiDefaultProfile" @ %listBox;
    selectedProfile = "ETSSelectedMenuItemProfile" @ %listBox;
    menuTextProfile = "ETSUnselectedMenuTextProfile" @ %listBox;
    menuTextSelectedProfile = "ETSSelectedMenuTextProfile" @ %listBox;
    if (!(isObject(entriesSet))) {
        entriesSet = SimSet @ new ""() @ %listBox;
        0;
        if (isObject()) {
            entriesSet.add();
        }
    }
    %listBox.setFieldWidths(%fieldWidths, vars);
    %listBox.clear();
    return %listBox;
};
function CSSelectorCtrl::refreshFromSet(%this, %doSort) {
    %this.clear();
    %count = entriesSet.getCount();
    %this;
    if (%doSort) {
        %this.sortEntries("owner", 1);
        %this.sortEntries("occupancy", 0);
        %this.sortEntries("access", 1);
    }
    %nameFieldWidth = getWord(fieldWidths, 0);
    %this;
    %descriptionFieldWidth = 10000;
    %n = 0;
    if ((%count < %n)) {
        %entry = entriesSet.getObject(%n);
        %this;
        if (!(%this.includeSpaceInList(%entry, filterByText))) {
        }
        %amCurrentlyHere = (%entry SPC name $= $CSSpaceName);
        CustomSpacesSelector;
        %isFriend = %this.ownerIsFriend(owner);
        %entry;
        %floorPlanNameText = "<clip:" @ %nameFieldWidth @ ">" @ %entry @ name @ "</clip>";
        if ((%entry SPC type $= "CELEBSPACE")) {
        }
        %ownerName = owner;
        %entry;
        %ownerName = %entry @ name @ "<clip:" @ %nameFieldWidth @ ">" @ %ownerName @ "</clip>";
        if (%isFriend) {
        }
        %ownerNameText = %ownerName;
        "<spush>" @ %isFriend[$gMlStyle @ "CSProfileFriendMenuTextUnselected"] @ %ownerName @ "<spop>";
        %spaceDescTextForModel = "<clip:" @ %descriptionFieldWidth @ ">" @ %entry @ TryFixBadWords(description) @ "</clip>";
        if (%isFriend) {
        }
        %spaceDescText = %spaceDescTextForModel;
        "<spush>" @ %isFriend[$gMlStyle @ "CSProfileFriendMenuTextUnselected"] @ %spaceDescTextForModel @ "<spop>";
        %dummyText = "";
        if ((%entry == stricmp(access, "PasswordProtected"))) {
        }
        if ((%entry == stricmp(access, "Locked"))) {
        }
        %occupancyTextForModel = %entry @ occupancy;
        "---";
        if (%isFriend) {
        }
        %occupancyText = %occupancyTextForModel;
        "<just:right>" @ 0.0 @ 0.0 @ "<spush>" @ %isFriend[$gMlStyle @ "CSProfileFriendMenuTextUnselected"] @ %occupancyTextForModel @ "<spop>";
        if (%amCurrentlyHere) {
            %visitNowLinkText = "You Are Here";
        }
        if ((%entry SPC type $= "MODEL")) {
        }
        if (isFeatured) {
        }
        %visitNowLinkText = %isFriend ? "<a:gamelink go><linkcolor:aaff00ff><linkcolorHL:f279f2ff>Visit Now</a>" : "<a:gamelink go><linkcolor:ffffffff><linkcolorHL:f279f2ff>Visit Now</a>";
        "<a:gamelink go><linkcolor:f2ff16ff><linkcolorHL:f279f2ff>Visit Now</a>";
        %type = type;
        %entry;
        if (isFeatured) {
            %type = "FEATURED";
            %entry;
        }
        %lineText = "";
        %entry;
        %accessIconIndex = 1;
        "<a:gamelink go><linkcolor:aaffffff><linkcolorHL:f279f2ff>Visit Now</a>";
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
        entryName = %entry @ name @ %line;
        horizSizing = "width" @ %line;
        if ((%this != descriptionFieldNumber)) {
            horizSizing = %this @ %line.getObject(descriptionFieldNumber);
            -(1.0) @ "width";
            %i = (1.0 - %line.getCount());
            if ((descriptionFieldNumber > %i)) {
                horizSizing = %this @ "left" @ %line.getObject(%i);
                %i = (1.0 - %i);
            }
        }
        if ((-(1.0) != %accessIconIndex)) {
            %position = %line.getObject(%accessIconIndex).getPosition();
            (descriptionFieldNumber > %i);
            %position = getWord(%position, 0) @ " " @ (1.0 - getWord(%position, 1));
            %this;
            accessIcon = %entry @ %this.createAccessIcon(access, %isFriend, %position) @ %line;
            if (!(%line SPC accessIcon $= "")) {
                %line.add(accessIcon);
            }
        }
        if (!(%amCurrentlyHere)) {
            %visitNowTextBox = %line.getObject(%visitNowIndex);
            %line;
            %visitNowTextBox.setProfile(%visitNowTextBox @ profile @ "Modal");
            %visitNowTextBox.bindClassName("CSSelectorLineVisitNowMLText");
        }
        %n = (1.0 + %n);
    }
    %extentWidth = getWord(getExtent(), 0);
    CSSelectorListCtrl;
    %extentHeight = (vars + (getWord(%this.getExtent(), 1) + getWord(%this.getPosition(), 1)));
    (%count < %n) @ "textAreaPadding" @ CustomSpacesSelector;
    if (((2.0 @ "scrollBarHeight" @ CustomSpacesSelector - vars) < %extentHeight)) {
        %extentHeight = (2.0 @ "scrollBarHeight" @ CustomSpacesSelector - vars);
    }
    getWord(getPosition(), 0).resize(getWord(getPosition(), 1), %extentWidth, %extentHeight);
};
function CSSelectorCtrl::sortEntries(%this, %sortField, %increasing) {
    %set = entriesSet;
    %this;
    %num = %set.getCount();
    %sortArray = new ""();
    Array;
    %n = 0;
    0;
    if ((%num < %n)) {
        %entry = %set.getObject(%n);
        %cmd = "%key = %entry." @ %sortField @ ";";
        eval(%cmd);
        if ((%sortField $= "access")) {
            if ((strlwr(%key) $= "friendsonly")) {
                %key = %this.ownerIsFriend(owner) ? 00 : 10;
                %entry;
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
            %key = (%entry SPC owner $= $Player::Name) ? 00 : 10;
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
    entriesSet.clear(1);
    %count = %sortArray.count();
    %this;
    %i = 0;
    (%num < %n);
    if ((%count < %i)) {
        entriesSet.add(%sortArray.getValue(%i));
        %i = (1.0 + %i);
        %this;
    }
    %sortArray.delete();
};
function CSSelectorCtrl::dumpSpaces(%this) {
    %set = entriesSet;
    %this;
    %num = %set.getCount();
    %n = 0;
    if ((%num < %n)) {
        %entry = %set.getObject(%n);
        echo(%entry @ formatInt("%6d", occupancy) @ " " @ %entry);
        %n = (1.0 + %n);
        formatString("%20s", owner) @ " ";
    }
};
function CSSelectorCtrl::createAccessIcon(%this, %accessType, %isFriend, %position) {
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %position;
    extent = "16 16";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %icon = ;
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
    %entry = %this.getEntryByName(name);
    %space;
    if (!(isObject(%entry))) {
        name = new ""() @ %space @ name;
        ScriptObject;
        %entry = 0;
        if (isObject()) {
            %entry.add();
        }
        entriesSet.add(%entry);
    }
    name = %space @ name @ %entry;
    %this;
    description = %space @ description @ %entry;
    MissionCleanup;
    longDescription = %space @ longDescription @ %entry;
    MissionCleanup;
    owner = %space @ owner @ %entry;
    ownerAge = %space @ ownerAge @ %entry;
    ownerSex = %space @ ownerSex @ %entry;
    ownerLocation = %space @ ownerLocation @ %entry;
    buildingName = %space @ buildingName @ %entry;
    floorPlanName = %space @ floorPlanName @ %entry;
    floorplan = %space @ floorplan @ %entry;
    city = %space @ city @ %entry;
    occupancy = %space @ occupancy @ %entry;
    type = %space @ strupr(type) @ %entry;
    isFeatured = %space @ isFeatured @ %entry;
    access = %space @ access @ %entry;
    vurl = %space @ vurl @ %entry;
    if ((%space SPC audioStream $= "")) {
    }
    audioStream = %space @ $musicStreamIDMap.get(audioStream) @ %entry;
    "";
    videoStream = %space @ videoStream @ %entry;
    return %entry;
};
function CSSelectorCtrl::deleteEntry(%this, %name) {
    %entry = %this.getEntryByName(%name);
    entriesSet.remove(%entry);
    %entry.delete();
};
function CSSelectorCtrl::getEntryByName(%this, %name) {
    %n = (%this - entriesSet.getCount());
    1.0;
    if ((0.0 >= %n)) {
        %entry = entriesSet.getObject(%n);
        %this;
        if ((%entry SPC name $= %name)) {
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
    %newHeight = (spacing + getWord(%extent, 1));
    %this;
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
    return (BuddyHudWin SPC %spaceOwner.getFriendStatus() $= "friends");
};
function CSSelectorLine::forgetFirstClick(%this) {
    lastSelectedEntry = "" @ CustomSpacesSelector;
    cancel($gCustomSpacesDoubleClickSched);
    $gCustomSpacesDoubleClickSched = 0;
};
function CSSelectorLine::onMouseDown(%this) {
    if ((%this $= entryName)) {
    }
    if (!(%this SPC entryName $= $CSSpaceName)) {
        teleportToSelected();
        return CSSelectorListCtrl;
    }
    if ((0.0 != $gCustomSpacesDoubleClickSched)) {
        cancel($gCustomSpacesDoubleClickSched);
        $gCustomSpacesDoubleClickSched = 0;
    }
    $gCustomSpacesDoubleClickSched = %this.schedule(500);
    forgetFirstClick;
    lastSelectedEntry = %this @ entryName @ CustomSpacesSelector;
    %entry = %this.getParent().getEntryByName(entryName);
    %this;
    if (isObject(%entry)) {
        %this.doGetDescription(%entry);
        %amCurrentlyHere = (%entry SPC name $= $CSSpaceName);
        if ((%entry SPC type $= "MODEL")) {
            0.setVisible();
            0.setVisible();
            !(%amCurrentlyHere).setActive();
            1.setVisible();
        }
        if ((%entry $= owner)) {
        }
        if ($player.rolesPermissionCheckNoWarn("customspaceMaster")) {
            0.setVisible();
            0.setVisible();
            !(%amCurrentlyHere).setActive();
            1.setVisible();
        }
        if ((%entry == stricmp(access, "PasswordProtected"))) {
            0.setVisible();
            0.setVisible();
            !(%amCurrentlyHere).setActive();
            1.setVisible();
        }
        if ((%entry == stricmp(access, "Locked"))) {
            0.setVisible();
            0.setVisible();
            0.setActive();
            1.setVisible();
        }
        if ((%entry == stricmp(access, "FriendOnly"))) {
            0.setVisible();
            0.setVisible();
            if (!(%amCurrentlyHere)) {
            }
            %this.ownerIsFriend(owner).setActive();
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
    %existingDesc = vars;
    "descriptions" @ %entry @ name @ CustomSpacesSelector;
    if (!(%existingDesc $= "")) {
        %existingDesc.setTextAndUpdate();
        return CSSelectorDescriptionCtrl;
    }
    if ((%entry SPC type $= "MODEL")) {
        "(loading...)".setTextAndUpdateWithCallback(%this, "doGetModelDescription", %entry);
    }
    "(loading...)".setTextAndUpdateWithCallback(%this, "doGetNonModelDescription", %entry);
};
function CSSelectorLine::doGetModelDescription(%this, %entry) {
    %bitmapText = "<sbreak><bitmap:platform/client/ui/buildingDir_model_" @ %entry @ city @ "_" @ %entry @ floorPlanName @ "><sbreak>";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
};
function CSSelectorLine::doGetNonModelDescription(%this, %entry) {
    if ((%entry SPC type $= "RESIDENCE")) {
    }
    %spaceOwnerName = name;
    %entry;
    entry = owner @ %entry @ %this;
    %entry;
    %url = $Net::AvatarURL @ urlEncode(stripUnprintables(%spaceOwnerName)) @ "?size=M200";
    %url.applyUrl("CSSelectorLine_gotDescriptionPhoto", "CSSelectorLine_gotDescriptionPhotoFailed", %this, "");
};
function CSSelectorLine_gotDescriptionPhotoFailed(%dlItem) {
    %entry = entry;
    callbackData;
    %bitmapText = %dlItem @ %entry[$gMlStyle @ "CSProfileDescriptionTextNormal"] @ "(photo unavailable)\n";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
};
function CSSelectorLine_gotDescriptionPhoto(%dlItem, %unused) {
    %localFileName = localFilename;
    %dlItem;
    %entry = entry;
    callbackData;
    %bitmapText = %dlItem @ "<sbreak><bitmap:" @ %localFileName @ "><sbreak>";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
};
function CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText) {
    %descText = CSSelectorLine::getDescriptionText(%entry, %bitmapText);
    vars = %descText TAB "descriptions" @ %entry @ name @ CustomSpacesSelector;
    if (!(%entry $= name)) {
        return getSelectedList().getHilitedCell() SPC entryName;
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
    if ((%entry SPC type $= "MODEL")) {
        %titleLineWidth = (CustomSpacesSelector_VISITandBUYBUTTON - getWord(getPosition(), 0));
        5.0;
        %pricingText = CSSpacePurchasePriceFormatting(priceVPoints, priceVBux);
        floorplan;
        if ((%entry SPC %pricingText $= "")) {
            %pricingText = "Currently unavailable for purchase - check back soon!";
            floorplan;
        }
        %desc = %entry @ "<spush>" @ %pricingText[$gMlStyle @ "CSProfileDescriptionTitleModel"] @ %entry @ CSProfileDescriptionTitleModel @ chopTextToFitLineWidths(name, %titleLineWidth, "") @ "\n" @ %pricingText @ "\n\n" @ %pricingText[$gMlStyle @ "CSProfileDescriptionHeaderModel"] @ "Currently Playing:" @ " " @ %pricingText[$gMlStyle @ "CSProfileDescriptionHeaderModel"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ %entry @ %entry @ csGetCurrentlyPlaying(audioStream, videoStream) @ "\n\n" @ %bitmapText @ "\n" @ " " @ %bitmapText[$gMlStyle @ "CSProfileDescriptionTextNormal"] @ %entry @ TryFixBadWords(longDescription) @ "<spop>";
    }
    if (isFeatured) {
    }
    if ((%entry == stricmp(access, "PasswordProtected"))) {
    }
    if ((%entry == stricmp(access, "Locked"))) {
    }
    %titleLineWidth = (getWord(getPosition(), 0) - (CustomSpacesSelector_GOBUTTON - getWord(getPosition(), 0)));
    CustomSpacesSelector_ENTERPASSWORDBUTTON;
    if ((%entry SPC type $= "CELEBSPACE")) {
    }
    %desc = 5.0 @ %entry @ 35.0 @ 0.0 @ 0.0 @ 0.0 @ "<spush>" @ %entry @ isFeatured ? "<bitmap:platform/client/ui/buildingDir_featured_star_lg>" : "" @ %entry[$gMlStyle @ "CSProfileDescriptionTitleNormal"] @ %entry @ name @ CSProfileDescriptionTitleNormal @ chopTextToFitLineWidths(%entry @ owner @ "'s Pad", %titleLineWidth, "") @ "\n" @ %entry @ TryFixBadWords(description) @ "\n\n" @ "Currently Playing: " @ " " @ %entry @ %entry @ csGetCurrentlyPlaying(audioStream, videoStream) @ "\n\n" @ %bitmapText @ "\n" @ %bitmapText[$gMlStyle @ "CSProfileDescriptionHeaderNormal"] @ "vURL: " @ " " @ %bitmapText[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ %entry @ vurlClearResolution(vurl) @ "<a: ></a>" @ " " @ "(<a:COPY_VURL>copy</a>)" @ "\n\n" @ " " @ %entry @ TryFixBadWords(longDescription) @ "<spop>";
    return %desc;
};
function CSSelectorLine::onHilite(%this) {
    %this.setProfile(selectedProfile);
    Parent.setSelectedList();
    %doScroll = 1;
    %this;
    %scrollHeight = getWord(getExtent(), 1);
    CSSelectorScrollCtrl;
    %listHeight = getWord(getExtent(), 1);
    CSSelectorListCtrl;
    if ((%scrollHeight <= %listHeight)) {
        return CSSelectorListCtrl;
    }
    %listTop = getWord(getPosition(), 1);
    CSSelectorListCtrl;
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
    scrollToTop();
    %resizingFunction = "CSSelectorDescriptionContainer.resize(getWord(CSSelectorDescriptionContainer.getExtent(), 0), getWord(CSSelectorDescriptionCtrl.getExtent(), 1) + 4);";
    CSSelectorDescriptionScrollCtrl;
    if ((%callbackFunction $= "")) {
        %afterResizeFunction = "";
    }
    if (isObject(%callbackObject)) {
    }
    %afterResizeFunction = %callbackObject @ "." @ "" @ %callbackFunction @ "(" @ %callbackParameters @ ");";
    waitAFrameAndEval(%resizingFunction @ " " @ %afterResizeFunction);
};
function CustomSpacesSelector::doOnKeyDown(%this, %keyname) {
    %this.fromKeyStopRepetition();
    if ((%keyname $= "up")) {
        fromKeyStartMovingUp();
    }
    if ((CustomSpacesSelector SPC %keyname $= "down")) {
        fromKeyStartMovingDown();
    }
};
function CustomSpacesSelector::doOnKeyUp(%this, %keyname) {
    %this.fromKeyStopRepetition();
    if ((%keyname $= "up")) {
    }
    if ((%keyname $= "down")) {
    }
    if ((%keyname $= "enter")) {
        teleportToSelected();
    }
};
function CustomSpacesSelector::fromKeyStopRepetition(%this) {
    if ((0.0 != $gCustomSpacesKeyStrokeRepeatSched)) {
        cancel($gCustomSpacesKeyStrokeRepeatSched);
        $gCustomSpacesKeyStrokeRepeatSched = 0;
    }
};
function CustomSpacesSelector::fromKeyStartMovingUp(%this) {
    %selectedListBox = getSelectedList();
    CSSelectorListCtrl;
    if (!(isObject(%selectedListBox))) {
        return;
    }
    %indexOfHilitedCell = %selectedListBox.getObjectIndex(%selectedListBox.getHilitedCell());
    %lastIndexPossible = (1.0 - %selectedListBox.getCount());
    if ((0.0 == %indexOfHilitedCell)) {
        if ((%selectedListBox > myIndex)) {
            %nextListBox = vars;
            0.0 TAB "aptCategoryListBoxes" @ 1.0 @ (%selectedListBox - myIndex) @ CustomSpacesSelector;
            %nextListBox.getObject((1.0 - %nextListBox.getCount())).onMouseDown();
        }
        scrollToTop();
    }
    %selectedListBox.getObject((1.0 - %indexOfHilitedCell)).onMouseDown();
    $gCustomSpacesKeyStrokeRepeatSched = %this.schedule($gCustomSpacesKeyStrokeRepFrequency);
    fromKeyStartMovingUp;
};
function CustomSpacesSelector::fromKeyStartMovingDown(%this) {
    %selectedListBox = getSelectedList();
    CSSelectorListCtrl;
    if (!(isObject(%selectedListBox))) {
        if ((0.0 @ "aptCategoryCount" @ CustomSpacesSelector > vars)) {
            vars.getObject(0).onMouseDown();
        }
        return "aptCategoryListBoxes" @ 0 @ CustomSpacesSelector;
    }
    %indexOfHilitedCell = %selectedListBox.getObjectIndex(%selectedListBox.getHilitedCell());
    %lastIndexPossible = (1.0 - %selectedListBox.getCount());
    if ((%lastIndexPossible == %indexOfHilitedCell)) {
        if ((%selectedListBox < myIndex)) {
            %nextListBox = vars;
            (1.0 @ "aptCategoryCount" @ CustomSpacesSelector - vars) TAB "aptCategoryListBoxes" @ 1.0 @ (%selectedListBox + myIndex) @ CustomSpacesSelector;
            %nextListBox.getObject(0).onMouseDown();
        }
    }
    %selectedListBox.getObject((1.0 + %indexOfHilitedCell)).onMouseDown();
    $gCustomSpacesKeyStrokeRepeatSched = %this.schedule($gCustomSpacesKeyStrokeRepFrequency);
    fromKeyStartMovingDown;
};
function CSSelectorDescriptionCtrl::onURL(%this, %text) {
    if ((%text $= "COPY_VURL")) {
        %entryName = entryName;
        getSelectedList().getHilitedCell();
        %entry = getSelectedList().getEntryByName(%entryName);
        CSSelectorListCtrl;
        setClipboard(vurlClearResolution(vurl));
    }
};
function CSSelectorListCtrl::getSelectedList(%this) {
    return selectedCSSelectorCtrl;
};
function CSSelectorListCtrl::setSelectedList(%this, %listBox) {
    if (isObject(selectedCSSelectorCtrl)) {
        if ((%this == selectedCSSelectorCtrl.getId())) {
            return %listBox.getId();
        }
        selectedCSSelectorCtrl.reseatChildren();
    }
    selectedCSSelectorCtrl = %this @ %listBox @ %this;
};
function CSSelectorListCtrl::teleportToSelected(%this) {
    %selectorCtrl = %this.getSelectedList();
    if (!(isObject(%selectorCtrl))) {
        return;
    }
    %entryName = entryName;
    %selectorCtrl.getHilitedCell();
    %entry = %selectorCtrl.getEntryByName(%entryName);
    if (!(%entry $= owner)) {
    }
    if (!($player.rolesPermissionCheckNoWarn("customspaceMaster"))) {
        if ((%entry == stricmp(access, "FriendsOnly"))) {
        }
        if (!(%selectorCtrl.ownerIsFriend(owner))) {
            MessageBoxOK(%entry, 0.0, "");
            return $Player::Name;
        }
        if ((%entry == stricmp(access, "PasswordProtected"))) {
            MessageBoxTextEntryWithCancel(0.0, , "", 0);
            return CSSelectorListCtrl_tackOnPassword;
        }
        if ((%entry == stricmp(access, "Locked"))) {
            MessageBoxOK(0.0, , "");
            return;
        }
    }
    %this.teleportToSpaceName(%entryName, "");
};
function CSSelectorListCtrl_tackOnPassword(%password) {
    %entryName = entryName;
    getSelectedList().getHilitedCell();
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
    vurl.doTeleportToSpace(name, %password);
};
function CustomSpacesSelector::doTeleportToSpace(%this, %vurl, %spaceName, %password) {
    if (!(%spaceName $= "")) {
        showTransitionMessage(%spaceName, 0);
    }
    %vurlObject = vurlGetParsedVurl(%vurl);
    %vurlObject.setPassword(%password);
    cbSuccessExpected = "CustomSpacesSelector_vurlSuccessExpected" @ %vurlObject;
    cbSuccess = "CustomSpacesSelector_vurlTransitionSucceeded" @ %vurlObject;
    cbReportError = "CustomSpacesSelector_vurlTransitionFailed" @ %vurlObject;
    %vurlObject.clearResolutionAndExecute();
};
function CustomSpacesSelector_vurlSuccessExpected(%vurl) {
    close();
};
function CustomSpacesSelector_vurlTransitionSucceeded(%vurl) {
    clearHeaderAndListBoxes();
    lastSelectedEntry = CustomSpacesSelector @ "" @ CustomSpacesSelector;
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
    callback = "vurlClearResolutionAndExecute( \"" @ %vurl @ "\");" @ 0 @ %dlg;
    callback = "CustomSpacesSelector.refresh();" @ 1 @ %dlg;
    callback = "" @ 2 @ %dlg;
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
    buildingName = %building @ %this;
    %this.clearHeaderAndListBoxes();
    lastSelectedEntry = "" @ %this;
    replaceAllOthers();
    vars = buildingDirectoryMap @ "textAreaPadding" @ %this @ (vars * 2.0) @ "scrollBarWidth" @ %this @ (vars - (CSSelectorScrollCtrl - getWord(getExtent(), 0))) @ "textAreaWidth" @ %this;
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    !(CustomSpacesSelector_RETURNTOLOBBY SPC CustomSpaceClient::GetSpaceImIn() $= "").setVisible();
    1.setVisible();
    container.setVisible(1);
    container.focusAndRaise();
    if ($ETS::devMode) {
    }
    if ($gGetFakeBuildingDirectory) {
        "(loading...)".setTextAndUpdateWithCallback(%this, "getFakeBuildingDirectory", "");
    }
    "(loading...)".setTextAndUpdateWithCallback("", "getBuildingDirectory", CSSelectorDescriptionCtrl @ %building @ ", customSpaceSelGotData, customSpaceSelFailed");
    applyBaseText();
    applyBaseText();
};
function CustomSpacesSelector::close(%this) {
    buildingName = "" @ %this;
    container.setVisible(0);
    focusTopWindow();
    if (isActive()) {
        restoreAllOthers();
    }
    return 1;
};
function CustomSpacesSelectorContainer::close(%this) {
    return close();
};
function CustomSpacesSelector::addHeaderAndListBoxes(%this, %type) {
    %newHeaderAndListBoxIndex = vars;
    "aptCategoryCount" @ %this;
    horizSizing = GuiBitmapCtrl @ new ""() @ "right";
    0;
    vertSizing = "bottom";
    position = "textTitleLeft" @ %this @ vars @ " " @ "textTitleTop" @ %this @ vars;
    extent = "120 9";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    %headerText = ;
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
    horizSizing = GuiControl @ new ""() @ "right";
    0;
    vertSizing = "bottom";
    position = "textAreaPadding" @ %this @ vars @ " " @ "aptCategoryCount" @ %this @ %this.calculateHeaderTop(vars);
    extent = (3.0 + getWord(%headerText.getExtent(), 0)) @ " " @ "headerboxHeight" @ %this @ vars;
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    maxLength = 64;
    %headerBox = ;
    %headerBox.add(%headerText);
    %headerBox.add();
    vars = CSSelectorListCtrl @ %headerBox TAB "aptCategoryHeaderBoxes" @ %newHeaderAndListBoxIndex @ %this;
    %listBox = CSSelectorCtrl::makeNewListBox(%type);
    %positionLeft = (vars + getWord(%headerBox.getPosition(), 0));
    "textAreaIndentation" @ CustomSpacesSelector;
    %positionTop = (vars + (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1)));
    "spaceBetweenHeaderAndListBoxes" @ %this;
    %extentWidth = getWord(%listBox.getExtent(), 0);
    %extentHeight = getWord(%listBox.getExtent(), 1);
    %listBox.resize(%positionLeft, %positionTop, %extentWidth, %extentHeight);
    %listBox.add();
    vars = CSSelectorListCtrl @ %listBox TAB "aptCategoryListBoxes" @ %newHeaderAndListBoxIndex @ %this;
    myIndex = %newHeaderAndListBoxIndex @ %listBox;
    if (("aptCategoriesInUse" @ CustomSpacesSelector SPC vars $= "")) {
        vars = %type @ "aptCategoriesInUse" @ CustomSpacesSelector;
    }
    vars = "aptCategoriesInUse" @ CustomSpacesSelector @ vars @ "\t" @ %type @ "aptCategoriesInUse" @ CustomSpacesSelector;
    vars = %newHeaderAndListBoxIndex TAB "aptCategoryIndexes" @ %type @ %this;
    vars = (1.0 + %newHeaderAndListBoxIndex) @ "aptCategoryCount" @ %this;
    return %listBox;
};
function CustomSpacesSelector::calculateHeaderTop(%this, %ordinal) {
    if ((0.0 < %ordinal)) {
        %ordinal = 0;
    }
    if ((vars > %ordinal)) {
        %ordinal = vars;
        "aptCategoryCount" @ %this @ "aptCategoryCount" @ %this;
    }
    if ((0.0 == %ordinal)) {
        %top = vars;
        "textAreaPadding" @ %this;
    }
    %top = getWord(vars.getPosition(), 1);
    "aptCategoryListBoxes" @ (1.0 - %ordinal) @ %this;
    %top = (getWord(vars.getExtent(), 1) + %top);
    "aptCategoryListBoxes" @ (1.0 - %ordinal) @ %this;
    %top = (vars + %top);
    "textAreaPadding" @ %this;
    %top = (vars + %top);
    "textAreaInterCategoryPadding" @ %this;
    return %top;
};
function CustomSpacesSelector::clearHeaderAndListBoxes(%this) {
    clear();
    %i = 0;
    CSSelectorListCtrl;
    if ((vars < %i)) {
        vars.delete();
        vars = "aptCategoryCount" @ CustomSpacesSelector TAB "aptCategoryHeaderBoxes" @ %i @ %this @ "" TAB "aptCategoryHeaderBoxes" @ %i @ %this;
        vars.delete();
        vars = "aptCategoryListBoxes" @ %i @ %this @ "" TAB "aptCategoryListBoxes" @ %i @ %this;
        %i = (1.0 + %i);
    }
    vars = "aptCategoryCount" @ CustomSpacesSelector @ (vars < %i) @ 0 @ "aptCategoryCount" @ %this;
    vars = "" @ "aptCategoriesInUse" @ %this;
};
function CustomSpacesSelector::setTypeIndexes(%this, %typeList) {
    %currentIndex = 0;
    %i = 0;
    if ((getFieldCount(vars) < %i)) {
        %type = getField(vars, %i);
        "aptCategories" @ %this @ "aptCategories" @ %this;
        if ((0.0 >= findField(%typeList, %type))) {
            vars = %currentIndex TAB "aptCategoryIndexes" @ %type @ %this;
            %currentIndex = (1.0 + %currentIndex);
        }
        %i = (1.0 + %i);
    }
};
function CustomSpacesSelector::getTypeIndex(%this, %type) {
    %index = vars;
    "aptCategoryIndexes" @ %type @ %this;
    return %index;
};
function CustomSpacesSelector::getListBox(%this, %type) {
    %listBox = vars;
    "aptCategoryListBoxes" @ %this.getTypeIndex(%type) @ %this;
    return %listBox;
};
function CustomSpacesSelector::saveListBox(%this, %listBox, %type) {
    vars = %listBox TAB "aptCategoryListBoxes" @ %this.getTypeIndex(%type) @ %this;
};
function CustomSpacesSelector::rearrangeListBoxes(%this) {
    %typesAvailable = vars;
    "aptCategories" @ %this;
    %typesInUse = vars;
    "aptCategoriesInUse" @ %this;
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
        %tempHeader = vars;
        "aptCategoryHeaderBoxes" @ %indexOfWhereTypeShouldBe @ %this;
        %tempListBox = vars;
        "aptCategoryListBoxes" @ %indexOfWhereTypeShouldBe @ %this;
        vars = "aptCategoryHeaderBoxes" @ %indexOfCurrentType @ %this @ vars TAB "aptCategoryHeaderBoxes" @ %indexOfWhereTypeShouldBe @ %this;
        vars = "aptCategoryListBoxes" @ %indexOfCurrentType @ %this @ vars TAB "aptCategoryListBoxes" @ %indexOfWhereTypeShouldBe @ %this;
        myIndex = %indexOfWhereTypeShouldBe TAB "aptCategoryListBoxes" @ %indexOfWhereTypeShouldBe @ %this @ vars;
        vars = %tempHeader TAB "aptCategoryHeaderBoxes" @ %indexOfCurrentType @ %this;
        vars = %tempListBox TAB "aptCategoryListBoxes" @ %indexOfCurrentType @ %this;
        myIndex = %indexOfCurrentType TAB "aptCategoryListBoxes" @ %indexOfCurrentType @ %this @ vars;
        %typesInUse = setField(%typesInUse, %indexOfCurrentType, getField(%typesInUse, %indexOfWhereTypeShouldBe));
        %typesInUse = setField(%typesInUse, %indexOfWhereTypeShouldBe, %currentTypeToExamine);
        %i = (1.0 + %i);
    }
    vars = ((1.0 - %numTypesAvailable) < %i) @ %typesAvailable @ "aptCategories" @ %this;
    vars = %typesInUse @ "aptCategoriesInUse" @ %this;
    %this.setTypeIndexes(vars);
    %this.adjustListBoxPositions(1);
};
function CustomSpacesSelector::adjustListBoxPositions(%this, %doSort) {
    if ((1.0 @ "aptCategoryCount" @ %this < vars)) {
        return;
    }
    %lastListBoxBottomEdgeY = 0;
    %i = 0;
    if ((vars < %i)) {
        %headerBox = vars;
        "aptCategoryCount" @ %this TAB "aptCategoryHeaderBoxes" @ %i @ %this;
        %listBox = vars;
        "aptCategoryListBoxes" @ %i @ %this;
        %headerBox.reposition(vars, %this.calculateHeaderTop(%i));
        %listBox.refreshFromSet(%doSort);
        %positionLeft = (vars + getWord(%headerBox.getPosition(), 0));
        "textAreaPadding" @ %this @ "textAreaIndentation" @ CustomSpacesSelector;
        %positionTop = (vars + (getWord(%headerBox.getExtent(), 1) + getWord(%headerBox.getPosition(), 1)));
        "spaceBetweenHeaderAndListBoxes" @ %this;
        %extentWidth = (vars @ "textAreaWidth" @ CustomSpacesSelector - vars);
        "textAreaIndentation" @ CustomSpacesSelector;
        %extentHeight = getWord(%listBox.getExtent(), 1);
        %listBox.resize(%positionLeft, %positionTop, %extentWidth, %extentHeight);
        %lastListBoxBottomEdgeY = (getWord(%listBox.getExtent(), 1) + %positionTop);
        %i = (1.0 + %i);
    }
    %newHeight = (vars + %lastListBoxBottomEdgeY);
    (vars < %i) @ "textAreaPadding" @ %this;
    getWord(getPosition(), 0).resize(getWord(getPosition(), 1), getWord(getExtent(), 0), %newHeight);
};
function CustomSpacesSelector::getHeaderBox(%this, %type) {
    return vars;
};
function customSpaceSelGotData(%buildingInfo, %buildingDir) {
    0.setVisible();
    1.setVisible();
    if (!(%buildingInfo SPC name $= "")) {
        if ((%buildingInfo SPC Buildings::GetDescription(name) $= "")) {
        }
        %titleBarText = %buildingInfo @ name @ %buildingInfo @ Buildings::GetDescription(name) @ " " @ "building directory";
        CSLegendContainer @ %buildingInfo[$gMlStyle @ "CSProfileTitleText"];
        %titleBarText.setText();
    }
    "Building Directory".setText();
    1.setVisible();
    50.schedule(description);
    clearHeaderAndListBoxes();
    myApartment = CustomSpacesSelector @ 0 @ CustomSpacesSelector;
    %buildingInfo;
    %myPlaceIsInThisBuilding = 0;
    setTextAndUpdate;
    %spacesCount = %buildingDir.getCount();
    CSSelectorDescriptionCtrl;
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
        if ((floorPlanCount < %j)) {
        }
        if (!(%floorPlanFound)) {
            %floorPlanFound = (floorplan $= name);
            %space SPC floorPlanName @ %j @ %buildingInfo;
            %j = (1.0 + %j);
            %buildingInfo;
            if ((floorPlanCount < %j)) {
            }
        }
        if (!(%floorPlanFound)) {
            warn(CustomSpacesSelector_TITLE @ %buildingInfo @ !(%floorPlanFound) @ "customSpaceSelGotData() got listing for apartment '" @ %space @ name @ "' with owner '" @ %space @ owner @ "' that refers to non-existent floorPlan '" @ %space @ floorPlanName @ "'");
        }
        city = %buildingInfo @ city @ %space;
        CustomSpacesSelector_TITLE;
        %info = owner.get();
        %space;
        if (isObject(%info)) {
            ownerAge = %info @ StripMLControlChars(age) @ %space;
            PlayerInfoMap;
            if ((%space SPC ownerAge $= "")) {
                ownerAge = CustomSpacesSelector_TITLE @ "-" @ %space;
                CustomSpacesSelector_LOADING;
            }
            ownerAge = %space @ ownerAge @ " " @ "yrs" @ %space;
            ownerSex = %info @ gender @ %space;
            if ((%space SPC ownerSex $= "f")) {
                ownerSex = "F" @ %space;
            }
            if ((%space SPC ownerSex $= "m")) {
                ownerSex = "M" @ %space;
            }
            ownerSex = "-" @ %space;
            ownerLocation = %info @ StripMLControlChars(location) @ %space;
            if ((%space SPC ownerLocation $= "")) {
                ownerLocation = "(hidden)" @ %space;
            }
        }
        ownerAge = "-" @ %space;
        ownerSex = "-" @ %space;
        ownerLocation = "-" @ %space;
        %type = type;
        %space;
        if ((%space $= owner)) {
            %type = "MYPLACE";
            $Player::Name;
            if ((0.0 @ "aptCategoriesInUse" @ CustomSpacesSelector >= findField(vars, %type))) {
                %listBox = %type.getListBox();
                CustomSpacesSelector;
            }
            %listBox = %type.addHeaderAndListBoxes();
            CustomSpacesSelector;
            %listBox.saveListBox(%type);
            %myPlaceIsInThisBuilding = 1;
            CustomSpacesSelector;
        }
        if (!(%space $= owner)) {
        }
        if (isFeatured) {
            if (isFeatured) {
                %type = "FEATURED";
                %space;
            }
            if ((0.0 @ "aptCategoriesInUse" @ CustomSpacesSelector >= findField(vars, %type))) {
                %listBox = %type.getListBox();
                CustomSpacesSelector;
            }
            %listBox = %type.addHeaderAndListBoxes();
            CustomSpacesSelector;
            %listBox.saveListBox(%type);
        }
        %listBox.updateEntry(%space);
        vars = %space @ CustomSpacesSelector @ "" TAB "descriptions" @ %space @ name @ CustomSpacesSelector;
        $Player::Name;
        %i = (1.0 + %i);
    }
    rearrangeListBoxes();
    if (0) {
        if (!(CustomSpacesSelector SPC $Player::myPlaceVURL $= "")) {
        }
        if ((0.0 != $CSSpaceInfo)) {
        }
        %amAtHome = ($CSSpaceInfo == stricmp(owner, $player.getShapeName()));
        0.0;
        !(CustomSpacesSelector_MYPLACE_container SPC $Player::myPlaceVURL $= "").setVisible();
        %style = %amAtHome ? "CSProfileSpecialLinkDisabled" : "CSProfileSpecialLink";
        (%spacesCount < %i);
        %alpha = %amAtHome ? 85 : 255;
        %style.applyBaseTextWithStyle();
        !(%amAtHome).setActive();
        modulationColor = CustomSpacesSelector_MYPLACE @ "255 255 255" @ " " @ %alpha @ CustomSpacesSelector_MYPLACEIcon;
        CustomSpacesSelector_MYPLACE;
    }
    if ((0.0 @ "aptCategoryCount" @ CustomSpacesSelector == vars)) {
        0.setVisible();
        1.setVisible();
    }
    %buildingDir.delete();
};
function customSpaceSelFailed(%building) {
    0.setVisible();
    0.setVisible();
    if (!(CSLegendContainer SPC %building $= "")) {
        if ((CustomSpacesSelector_LOADING @ %building[$gMlStyle @ "CSProfileTitleText"] SPC Buildings::GetDescription(%building) $= "")) {
        }
        %titleBarText = %building @ Buildings::GetDescription(%building) @ " " @ "building directory";
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
        close();
    }
};
function CSSelectorLineVisitNowMLText::onURL(%this, %url) {
    if ((%url $= "gamelink go")) {
        %this.getParent().onMouseDown();
        teleportToSelected();
    }
};
function CustomSpacesSelector::refresh(%this) {
    if (isActive()) {
        restoreAllOthers();
    }
    %this.open(buildingName);
    1.setVisible();
};
function CustomSpacesSelector_FILTER::onKeyDown(%this, %unused, %unused) {
    0.setVisible();
    return 0;
};
function CustomSpacesSelector_FILTER::onKeyUp(%this, %unused, %unused) {
    %fieldIsVisible = %this.isVisible();
    (CustomSpacesSelector_FILTER_OVERLAY SPC %this.getValue() $= "").setVisible();
    %this.getValue().doFilter();
    return 0;
};
function CustomSpacesSelector::doFilter(%this, %filterByText) {
    filterByText = %filterByText @ %this;
    %this.adjustListBoxPositions(0);
};
function CSSelectorCtrl::includeSpaceInList(%this, %entry, %filterByText) {
    if (!(isObject(%entry))) {
        return 0;
    }
    if (!(%entry SPC type $= "RESIDENCE")) {
        return 1;
    }
    if (isFeatured) {
        return 1;
    }
    if ((%entry $= owner)) {
        return 1;
    }
    %filterByText = strlwr(%filterByText);
    if ((%filterByText $= "")) {
        return 1;
    }
    if ((%entry > strstr(strlwr(owner), %filterByText))) {
        return 1;
    }
    if ((%entry > strstr(strlwr(description), %filterByText))) {
        return 1;
    }
    return 0;
};
