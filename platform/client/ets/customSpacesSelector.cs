CustomSpacesSelector.filterByText = "";
$gCustomSpacesDoubleClickSched = 0;
$gCustomSpacesKeyStrokeRepeatSched = 0;
$gCustomSpacesKeyStrokeRepFrequency = 120;
CustomSpacesSelector.vars["aptCategories"] = "MODEL" TAB "FEATURED" TAB "CELEBSPACE" TAB "MYPLACE" TAB "RESIDENCE";
CustomSpacesSelector.vars["aptCategoriesInUse"] = "";
CustomSpacesSelector.vars["aptCategoryCount"] = 0;
CustomSpacesSelector.vars["scrollBarHeight"] = 200;
CustomSpacesSelector.vars["scrollBarWidth"] = 13;
CustomSpacesSelector.vars["headerboxHeight"] = 11;
CustomSpacesSelector.vars["textTitleLeft"] = 0;
CustomSpacesSelector.vars["textTitleTop"] = 1;
CustomSpacesSelector.vars["spaceBetweenHeaderAndListBoxes"] = 0;
CustomSpacesSelector.vars["textAreaIndentation"] = 5;
CustomSpacesSelector.vars["textAreaPadding"] = 2;
CustomSpacesSelector.vars["textAreaWidth"] = 470;
CustomSpacesSelector.vars["textAreaInterCategoryPadding"] = 10;
CustomSpacesSelector.vars["spacesColumnPadding"] = 5;
CustomSpacesSelector.vars["spacesRowPadding"] = 0;
function CSSelectorCtrl::makeNewListBox(%type)
{
    %windowWidth = getWord(CustomSpacesSelector.getExtent(), 0);
    %listBox = new GuiArray2Ctrl()
    {
        profile = "CSProfileListBox";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "16 16";
        spacing = CustomSpacesSelector.vars["spacesRowPadding"];
        numRowsOrCols = 1;
        inRows = 0;
        hilited = 0;
        lastClicked = 0;
        paddingAboveText = 1;
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = (CustomSpacesSelector.vars["textAreaWidth"] + (%windowWidth - 750)) - CustomSpacesSelector.vars["textAreaIndentation"] SPC CustomSpacesSelector.vars["headerboxHeight"];
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        scroll = "CSSelectorScrollCtrl";
    };
    %listBox.bindClassName("MenuControl");
    %listBox.bindClassName("TabbedTextControl");
    %listBox.bindClassName("CSSelectorCtrl");
    %fieldWidths = "90 16" SPC 197 + (%windowWidth - 750) SPC "25 0 68";
    %listBox.descriptionFieldNumber = 2;
    if (%type $= "MODEL")
    {
        %listBox.unselectedProfile = "CSProfileModelListingUnselected";
        %listBox.selectedProfile = "CSProfileModelListingSelected";
        %listBox.menuTextProfile = "CSProfileModelListingMenuText";
        %listBox.menuTextSelectedProfile = "CSProfileModelListingMenuTextSelected";
        %fieldWidths = 111 SPC 227 + (%windowWidth - 750) SPC "0 68";
        %listBox.descriptionFieldNumber = 1;
    }
    else
    {
        if (%type $= "FEATURED")
        {
            %listBox.unselectedProfile = "CSProfileFeaturedListingUnselected";
            %listBox.selectedProfile = "CSProfileFeaturedListingSelected";
            %listBox.menuTextProfile = "CSProfileFeaturedListingMenuText";
            %listBox.menuTextSelectedProfile = "CSProfileFeaturedListingMenuTextSelected";
        }
        else
        {
            if (%type $= "CELEBSPACE")
            {
                %listBox.unselectedProfile = "CSProfileCelebListingUnselected";
                %listBox.selectedProfile = "CSProfileCelebListingSelected";
                %listBox.menuTextProfile = "CSProfileCelebListingMenuText";
                %listBox.menuTextSelectedProfile = "CSProfileCelebListingMenuTextSelected";
            }
            else
            {
                if (%type $= "MYPLACE")
                {
                    %listBox.unselectedProfile = "CSProfileNormalListingUnselected";
                    %listBox.selectedProfile = "CSProfileNormalListingSelected";
                    %listBox.menuTextProfile = "CSProfileNormalListingMenuText";
                    %listBox.menuTextSelectedProfile = "CSProfileNormalListingMenuTextSelected";
                }
                else
                {
                    if (%type $= "RESIDENCE")
                    {
                        %listBox.unselectedProfile = "CSProfileNormalListingUnselected";
                        %listBox.selectedProfile = "CSProfileNormalListingSelected";
                        %listBox.menuTextProfile = "CSProfileNormalListingMenuText";
                        %listBox.menuTextSelectedProfile = "CSProfileNormalListingMenuTextSelected";
                    }
                    else
                    {
                        %listBox.unselectedProfile = "GuiDefaultProfile";
                        %listBox.selectedProfile = "ETSSelectedMenuItemProfile";
                        %listBox.menuTextProfile = "ETSUnselectedMenuTextProfile";
                        %listBox.menuTextSelectedProfile = "ETSSelectedMenuTextProfile";
                    }
                }
            }
        }
    }
    if (!isObject(%listBox.entriesSet))
    {
        %listBox.entriesSet = new SimSet();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%listBox.entriesSet);
        }
    }
    %listBox.setFieldWidths(%fieldWidths, CustomSpacesSelector.vars["spacesColumnPadding"]);
    %listBox.clear();
    return %listBox;
}
function CSSelectorCtrl::refreshFromSet(%this, %doSort)
{
    %this.clear();
    %count = %this.entriesSet.getCount();
    if (%doSort)
    {
        %this.sortEntries("owner", 1);
        %this.sortEntries("occupancy", 0);
        %this.sortEntries("access", 1);
    }
    %nameFieldWidth = getWord(%this.fieldWidths, 0);
    %descriptionFieldWidth = 10000;
    %n = 0;
    while (%n < %count)
    {
        %entry = %this.entriesSet.getObject(%n);
        if (!%this.includeSpaceInList(%entry, CustomSpacesSelector.filterByText))
        {
            continue;
        }
        %amCurrentlyHere = %entry.name $= $CSSpaceName;
        %isFriend = %this.ownerIsFriend(%entry.owner);
        %floorPlanNameText = "<clip:" @ %nameFieldWidth @ ">" @ %entry.name @ "</clip>";
        %ownerName = %entry.type $= "CELEBSPACE" ? %entry : %entry;
        %ownerName = "<clip:" @ %nameFieldWidth @ ">" @ %ownerName @ "</clip>";
        if (%isFriend)
        {
        }
        else
        {
        }
        %ownerNameText = ;
        %spaceDescTextForModel = "<clip:" @ %descriptionFieldWidth @ ">" @ TryFixBadWords(%entry.description) @ "</clip>";
        if (%isFriend)
        {
        }
        else
        {
        }
        %spaceDescText = ;
        %dummyText = "";
        %occupancyTextForModel = "<just:right>" @ (stricmp(%entry.access, "PasswordProtected") == 0) && (stricmp(%entry.access, "Locked") == 0) ? "---" : %entry;
        if (%isFriend)
        {
        }
        else
        {
        }
        %occupancyText = ;
        if (%amCurrentlyHere)
        {
            %visitNowLinkText = "You Are Here";
        }
        else
        {
            %entry.type $= "MODEL" ? "<a:gamelink go><linkcolor:aaffffff><linkcolorHL:f279f2ff>Visit Now</a>" : %entry;
        }
        %type = %entry.type;
        if (%entry.isFeatured)
        {
            %type = "FEATURED";
        }
        %lineText = "";
        %accessIconIndex = 1;
        %visitNowIndex = 5;
        if (%type $= "MODEL")
        {
            %lineText = %floorPlanNameText TAB %spaceDescTextForModel TAB %dummyText TAB %visitNowLinkText;
            %accessIconIndex = -1;
            %visitNowIndex = 3;
        }
        else
        {
            if (%type $= "FEATURED")
            {
                %lineText = %ownerNameText TAB %dummyText TAB %spaceDescTextForModel TAB %occupancyTextForModel TAB %dummyText TAB %visitNowLinkText;
            }
            else
            {
                if (%type $= "CELEBSPACE")
                {
                    %lineText = %ownerNameText TAB %dummyText TAB %spaceDescTextForModel TAB %occupancyTextForModel TAB %dummyText TAB %visitNowLinkText;
                }
                else
                {
                    if (%type $= "MYPLACE")
                    {
                        %lineText = %ownerNameText TAB %dummyText TAB %spaceDescTextForModel TAB %occupancyTextForModel TAB %dummyText TAB %visitNowLinkText;
                    }
                    else
                    {
                        if (%type $= "RESIDENCE")
                        {
                            %lineText = %ownerNameText TAB %dummyText TAB %spaceDescTextForModel TAB %occupancyTextForModel TAB %dummyText TAB %visitNowLinkText;
                        }
                        else
                        {
                            %lineText = %ownerNameText TAB %dummyText TAB %spaceDescTextForModel TAB %occupancyTextForModel TAB %dummyText TAB %visitNowLinkText;
                        }
                    }
                }
            }
        }
        %line = %this.addLine(%lineText);
        %line.entryName = %entry.name;
        %line.horizSizing = "width";
        if (%this.descriptionFieldNumber != -1)
        {
            %line.getObject(%this.descriptionFieldNumber).horizSizing = "width";
            %i = %line.getCount() - 1;
            while (%i > %this.descriptionFieldNumber)
            {
                %line.getObject(%i).horizSizing = "left";
                %i = %i - 1;
            }
        }
        if (%accessIconIndex != -1)
        {
            %position = %line.getObject(%accessIconIndex).getPosition();
            %position = getWord(%position, 0) SPC getWord(%position, 1) - 1;
            %line.accessIcon = %this.createAccessIcon(%entry.access, %isFriend, %position);
            if (!(%line.accessIcon $= ""))
            {
                %line.add(%line.accessIcon);
            }
        }
        if (!%amCurrentlyHere)
        {
            %visitNowTextBox = %line.getObject(%visitNowIndex);
            %visitNowTextBox.setProfile(%visitNowTextBox.profile @ "Modal");
            %visitNowTextBox.bindClassName("CSSelectorLineVisitNowMLText");
        }
        %n = %n + 1;
    }
    %extentWidth = getWord(CSSelectorListCtrl.getExtent(), 0);
    %extentHeight = (getWord(%this.getPosition(), 1) + getWord(%this.getExtent(), 1)) + CustomSpacesSelector.vars["textAreaPadding"];
    if (%extentHeight < (CustomSpacesSelector.vars["scrollBarHeight"] - 2))
    {
        %extentHeight = CustomSpacesSelector.vars["scrollBarHeight"] - 2;
    }
    CSSelectorListCtrl.resize(getWord(CSSelectorListCtrl.getPosition(), 0), getWord(CSSelectorListCtrl.getPosition(), 1), %extentWidth, %extentHeight);
    return ;
}
function CSSelectorCtrl::sortEntries(%this, %sortField, %increasing)
{
    %set = %this.entriesSet;
    %num = %set.getCount();
    %sortArray = new Array();
    %n = 0;
    while (%n < %num)
    {
        %entry = %set.getObject(%n);
        %cmd = "%key = %entry." @ %sortField @ ";";
        eval(%cmd);
        if (%sortField $= "access")
        {
            if (strlwr(%key) $= "friendsonly")
            {
                %key = %this.ownerIsFriend(%entry.owner) ? 00 : 10;
            }
            else
            {
                if (strlwr(%key) $= "open")
                {
                    %key = 00;
                }
                else
                {
                    if (strlwr(%key) $= "passwordprotected")
                    {
                        %key = 08;
                    }
                    else
                    {
                        if (strlwr(%key) $= "locked")
                        {
                            %key = 10;
                        }
                    }
                }
            }
        }
        else
        {
            if (%sortField $= "owner")
            {
                %key = %key;
            }
            else
            {
                if (%sortField $= "occupancy")
                {
                    %key = formatInt("%0.8d", %key);
                }
                else
                {
                    if (%sortField $= "self")
                    {
                        %key = %entry.owner $= $Player::Name ? 00 : 10;
                    }
                    else
                    {
                        if (mFloor(%key) $= %key)
                        {
                            %key = formatInt("%0.8d", %key);
                        }
                    }
                }
            }
        }
        %sortArray.push_back(%key, %entry);
        %n = %n + 1;
    }
    if (%increasing)
    {
        %sortArray.ssortka();
    }
    else
    {
        %sortArray.ssortkd();
    }
    %this.entriesSet.clear(1);
    %count = %sortArray.count();
    %i = 0;
    while (%i < %count)
    {
        %this.entriesSet.add(%sortArray.getValue(%i));
        %i = %i + 1;
    }
    %sortArray.delete();
    return ;
}
function CSSelectorCtrl::dumpSpaces(%this)
{
    %set = %this.entriesSet;
    %num = %set.getCount();
    %n = 0;
    while (%n < %num)
    {
        %entry = %set.getObject(%n);
        echo(formatString("%20s", %entry.owner) SPC formatInt("%6d", %entry.occupancy) SPC %entry);
        %n = %n + 1;
    }
}

function CSSelectorCtrl::createAccessIcon(%this, %accessType, %isFriend, %position)
{
    %icon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %position;
        extent = "16 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    if ((stricmp(%accessType, "PasswordProtected") == 0) && (stricmp(%accessType, "Locked") == 0))
    {
        if (%isFriend)
        {
            %icon.setBitmap("platform/client/ui/buildingDir_key_green");
        }
        else
        {
            %icon.setBitmap("platform/client/ui/buildingDir_key_white");
        }
    }
    else
    {
        if (stricmp(%accessType, "FriendsOnly") == 0)
        {
            if (%isFriend)
            {
                %icon.setBitmap("platform/client/ui/buildingDir_heart_green");
            }
            else
            {
                %icon.setBitmap("platform/client/ui/buildingDir_heart_white");
            }
        }
        else
        {
            %icon.delete();
            %icon = "";
        }
    }
    return %icon;
}
function CSSelectorCtrl::updateEntry(%this, %space)
{
    %entry = %this.getEntryByName(%space.name);
    if (!isObject(%entry))
    {
        %entry = new ScriptObject();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%entry);
        }
        %this.entriesSet.add(%entry);
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
    %entry.audioStream = %space.audioStream $= "" ? "" : $musicStreamIDMap.get(%space.audioStream);
    %entry.videoStream = %space.videoStream;
    return %entry;
}
function CSSelectorCtrl::deleteEntry(%this, %name)
{
    %entry = %this.getEntryByName(%name);
    %this.entriesSet.remove(%entry);
    %entry.delete();
    return ;
}
function CSSelectorCtrl::getEntryByName(%this, %name)
{
    %n = %this.entriesSet.getCount() - 1;
    while (%n >= 0)
    {
        %entry = %this.entriesSet.getObject(%n);
        if (%entry.name $= %name)
        {
            return %entry;
        }
        %n = %n - 1;
    }
    return -1;
}
function CSSelectorCtrl::childSelected(%this, %child)
{
    return ;
}
function CSSelectorCtrl::onCreatedChild(%this, %child)
{
    Parent::onCreatedChild(%this, %child);
    %position = %child.getPosition();
    %extent = %child.getExtent();
    %newHeight = getWord(%extent, 1) + %this.spacing;
    %child.resize(getWord(%position, 0), getWord(%position, 1), getWord(%extent, 0), %newHeight);
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSSelectorLine"))
    {
        %child.bindClassName("CSSelectorLine");
    }
    return ;
}
function CSSelectorCtrl::ownerIsFriend(%this, %spaceOwner)
{
    if (($ETS::devMode && $gGetFakeBuildingDirectory) && (%spaceOwner $= "DDDD"))
    {
        return 1;
    }
    return BuddyHudWin.getFriendStatus(%spaceOwner) $= "friends";
}
function CSSelectorLine::forgetFirstClick(%this)
{
    CustomSpacesSelector.lastSelectedEntry = "";
    cancel($gCustomSpacesDoubleClickSched);
    $gCustomSpacesDoubleClickSched = 0;
    return ;
}
function CSSelectorLine::onMouseDown(%this)
{
    if ((CustomSpacesSelector.lastSelectedEntry $= %this.entryName) && !((%this.entryName $= $CSSpaceName)))
    {
        CSSelectorListCtrl.teleportToSelected();
        return ;
    }
    if ($gCustomSpacesDoubleClickSched != 0)
    {
        cancel($gCustomSpacesDoubleClickSched);
        $gCustomSpacesDoubleClickSched = 0;
    }
    $gCustomSpacesDoubleClickSched = %this.schedule(500, forgetFirstClick);
    CustomSpacesSelector.lastSelectedEntry = %this.entryName;
    %entry = %this.getParent().getEntryByName(%this.entryName);
    if (isObject(%entry))
    {
        %this.doGetDescription(%entry);
        %amCurrentlyHere = %entry.name $= $CSSpaceName;
        if (%entry.type $= "MODEL")
        {
            CustomSpacesSelector_GOBUTTON.setVisible(0);
            CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(0);
            CustomSpacesSelector_VISITandBUYBUTTON.setActive(!%amCurrentlyHere);
            CustomSpacesSelector_VISITandBUYBUTTON.setVisible(1);
        }
        else
        {
            if (($Player::Name $= %entry.owner) && $player.rolesPermissionCheckNoWarn("customspaceMaster"))
            {
                CustomSpacesSelector_VISITandBUYBUTTON.setVisible(0);
                CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(0);
                CustomSpacesSelector_GOBUTTON.setActive(!%amCurrentlyHere);
                CustomSpacesSelector_GOBUTTON.setVisible(1);
            }
            else
            {
                if (stricmp(%entry.access, "PasswordProtected") == 0)
                {
                    CustomSpacesSelector_VISITandBUYBUTTON.setVisible(0);
                    CustomSpacesSelector_GOBUTTON.setVisible(0);
                    CustomSpacesSelector_ENTERPASSWORDBUTTON.setActive(!%amCurrentlyHere);
                    CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(1);
                }
                else
                {
                    if (stricmp(%entry.access, "Locked") == 0)
                    {
                        CustomSpacesSelector_VISITandBUYBUTTON.setVisible(0);
                        CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(0);
                        CustomSpacesSelector_GOBUTTON.setActive(0);
                        CustomSpacesSelector_GOBUTTON.setVisible(1);
                    }
                    else
                    {
                        if (stricmp(%entry.access, "FriendOnly") == 0)
                        {
                            CustomSpacesSelector_VISITandBUYBUTTON.setVisible(0);
                            CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(0);
                            CustomSpacesSelector_GOBUTTON.setActive(!%amCurrentlyHere && %this.ownerIsFriend(%entry.owner));
                            CustomSpacesSelector_GOBUTTON.setVisible(1);
                        }
                        else
                        {
                            CustomSpacesSelector_VISITandBUYBUTTON.setVisible(0);
                            CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(0);
                            CustomSpacesSelector_GOBUTTON.setActive(!%amCurrentlyHere);
                            CustomSpacesSelector_GOBUTTON.setVisible(1);
                        }
                    }
                }
            }
        }
    }
    else
    {
        CustomSpacesSelector_VISITandBUYBUTTON.setVisible(0);
        CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(0);
        CustomSpacesSelector_GOBUTTON.setVisible(0);
        CSSelectorDescriptionCtrl.setTextAndUpdate("");
    }
    Parent::onMouseDown(%this);
    return ;
}
function CSSelectorLine::doGetDescription(%this, %entry)
{
    %existingDesc = CustomSpacesSelector.vars[("descriptions",%entry.name)];
    if (!(%existingDesc $= ""))
    {
        CSSelectorDescriptionCtrl.setTextAndUpdate(%existingDesc);
        return ;
    }
    if (%entry.type $= "MODEL")
    {
        CSSelectorDescriptionCtrl.setTextAndUpdateWithCallback("(loading...)", %this, "doGetModelDescription", %entry);
    }
    else
    {
        CSSelectorDescriptionCtrl.setTextAndUpdateWithCallback("(loading...)", %this, "doGetNonModelDescription", %entry);
    }
    return ;
}
function CSSelectorLine::doGetModelDescription(%this, %entry)
{
    %bitmapText = "<sbreak><bitmap:platform/client/ui/buildingDir_model_" @ %entry.city @ "_" @ %entry.floorPlanName @ "><sbreak>";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
    return ;
}
function CSSelectorLine::doGetNonModelDescription(%this, %entry)
{
    %spaceOwnerName = %entry.type $= "RESIDENCE" ? %entry : %entry;
    %this.entry = %entry;
    %url = $Net::AvatarURL @ urlEncode(stripUnprintables(%spaceOwnerName)) @ "?size=M200";
    dlMgr.applyUrl(%url, "CSSelectorLine_gotDescriptionPhoto", "CSSelectorLine_gotDescriptionPhotoFailed", %this, "");
    return ;
}
function CSSelectorLine_gotDescriptionPhotoFailed(%dlItem)
{
    %entry = %dlItem.callbackData.entry;
    %bitmapText = $gMlStyle["CSProfileDescriptionTextNormal"] @ "(photo unavailable)\n";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
    return ;
}
function CSSelectorLine_gotDescriptionPhoto(%dlItem, %unused)
{
    %localFileName = %dlItem.localFilename;
    %entry = %dlItem.callbackData.entry;
    %bitmapText = "<sbreak><bitmap:" @ %localFileName @ "><sbreak>";
    CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText);
    return ;
}
function CSSelectorLine_gotDescriptionPhotoFinale(%entry, %bitmapText)
{
    %descText = CSSelectorLine::getDescriptionText(%entry, %bitmapText);
    CustomSpacesSelector.vars["descriptions",%entry.name] = %descText;
    if (!(CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName $= %entry.name))
    {
        return ;
    }
    CSSelectorDescriptionCtrl.setTextAndUpdate(%descText);
    return ;
}
function csGetCurrentlyPlaying(%audioStream, %videoStream)
{
    if (!((%videoStream $= "")) && !((%videoStream $= "no-video")))
    {
        return "YouTube Videos!";
    }
    if (!((%audioStream $= "")) && !((%audioStream $= "- none -")))
    {
        return %audioStream;
    }
    return "(nothing)";
}
function CSSelectorLine::getDescriptionText(%entry, %bitmapText)
{
    %desc = "";
    if (%entry.type $= "MODEL")
    {
        %titleLineWidth = getWord(CustomSpacesSelector_VISITandBUYBUTTON.getPosition(), 0) - 5;
        %pricingText = CSSpacePurchasePriceFormatting(%entry.floorplan.priceVPoints, %entry.floorplan.priceVBux);
        if (%pricingText $= "")
        {
            %pricingText = "Currently unavailable for purchase - check back soon!";
        }
        %desc = "<spush>" @ $gMlStyle["CSProfileDescriptionTitleModel"] @ chopTextToFitLineWidths(%entry.name, CSProfileDescriptionTitleModel, %titleLineWidth, "") @ "\n" @ $gMlStyle["CSProfileDescriptionTextModel"] @ %pricingText @ "\n\n" @ $gMlStyle["CSProfileDescriptionHeaderModel"] @ "Currently Playing:" SPC $gMlStyle["CSProfileDescriptionTextNormal"] @ csGetCurrentlyPlaying(%entry.audioStream, %entry.videoStream) @ "\n\n" @ %bitmapText @ "\n" SPC $gMlStyle["CSProfileDescriptionTextNormal"] @ TryFixBadWords(%entry.longDescription) @ "<spop>";
    }
    else
    {
        %titleLineWidth = ((stricmp(%entry.access, "PasswordProtected") == 0) && (stricmp(%entry.access, "Locked") == 0) ? getWord(CustomSpacesSelector_ENTERPASSWORDBUTTON.getPosition(), 0) : getWord(CustomSpacesSelector_GOBUTTON.getPosition(), 0) - %entry.isFeatured ? 35 : 0) - 5;
        %desc = "<spush>" @ %entry.isFeatured ? "<bitmap:platform/client/ui/buildingDir_featured_star_lg>" : "" @ $gMlStyle["CSProfileDescriptionTitleNormal"] @ chopTextToFitLineWidths(%entry.type $= "CELEBSPACE" ? %entry : %entry @ "\'s Pad", CSProfileDescriptionTitleNormal, %titleLineWidth, "") @ "\n" @ $gMlStyle["CSProfileDescriptionTextNormal"] @ TryFixBadWords(%entry.description) @ "\n\n" @ $gMlStyle["CSProfileDescriptionHeaderNormal"] @ "Currently Playing: " SPC $gMlStyle["CSProfileDescriptionTextNormal"] @ csGetCurrentlyPlaying(%entry.audioStream, %entry.videoStream) @ "\n\n" @ %bitmapText @ "\n" @ $gMlStyle["CSProfileDescriptionHeaderNormal"] @ "vURL: " SPC $gMlStyle["CSProfileDescriptionTextNormal"] @ vurlClearResolution(%entry.vurl) @ "<a: ></a>" SPC "(<a:COPY_VURL>copy</a>)" @ "\n\n" SPC $gMlStyle["CSProfileDescriptionTextNormal"] @ TryFixBadWords(%entry.longDescription) @ "<spop>";
    }
    return %desc;
}
function CSSelectorLine::onHilite(%this)
{
    %this.setProfile(%this.Parent.selectedProfile);
    CSSelectorListCtrl.setSelectedList(%this.Parent);
    %doScroll = 1;
    %scrollHeight = getWord(CSSelectorScrollCtrl.getExtent(), 1);
    %listHeight = getWord(CSSelectorListCtrl.getExtent(), 1);
    if (%listHeight <= %scrollHeight)
    {
        return ;
    }
    %listTop = getWord(CSSelectorListCtrl.getPosition(), 1);
    %extremeEdge = (getWord(%this.getParent().getPosition(), 1) + getWord(%this.getPosition(), 1)) - 2;
    if ((%listTop + %extremeEdge) >= 0)
    {
        %extremeEdge = %extremeEdge + ((getWord(%this.getExtent(), 1) - %scrollHeight) + 4);
        if ((%listTop + %extremeEdge) <= 0)
        {
            %doScroll = 0;
        }
    }
    if (%doScroll)
    {
        CSSelectorScrollCtrl.scrollTo(0, %extremeEdge);
    }
    return ;
}
function CSSelectorLine::onUnhilite(%this)
{
    Parent::onUnhilite(%this);
    return ;
}
function CSSelectorDescriptionCtrl::setTextAndUpdate(%this, %text)
{
    %this.setTextAndUpdateWithCallback(%text, 0, "", "");
    return ;
}
function CSSelectorDescriptionCtrl::setTextAndUpdateWithCallback(%this, %text, %callbackObject, %callbackFunction, %callbackParameters)
{
    %this.setText(%text);
    CSSelectorDescriptionScrollCtrl.scrollToTop();
    %resizingFunction = "CSSelectorDescriptionContainer.resize(getWord(CSSelectorDescriptionContainer.getExtent(), 0), getWord(CSSelectorDescriptionCtrl.getExtent(), 1) + 4);";
    if (%callbackFunction $= "")
    {
        %afterResizeFunction = "";
    }
    else
    {
        %afterResizeFunction = isObject(%callbackObject) ? %callbackObject : "" @ %callbackFunction @ "(" @ %callbackParameters @ ");";
    }
    waitAFrameAndEval(%resizingFunction SPC %afterResizeFunction);
    return ;
}
function CustomSpacesSelector::doOnKeyDown(%this, %keyname)
{
    %this.fromKeyStopRepetition();
    if (%keyname $= "up")
    {
        CustomSpacesSelector.fromKeyStartMovingUp();
    }
    else
    {
        if (%keyname $= "down")
        {
            CustomSpacesSelector.fromKeyStartMovingDown();
        }
        else
        {
            if (%keyname $= "enter")
            {
            }
        }
    }
    return ;
}
function CustomSpacesSelector::doOnKeyUp(%this, %keyname)
{
    %this.fromKeyStopRepetition();
    if (%keyname $= "up")
    {
    }
    else
    {
        if (%keyname $= "down")
        {
        }
        else
        {
            if (%keyname $= "enter")
            {
                CSSelectorListCtrl.teleportToSelected();
            }
        }
    }
    return ;
}
function CustomSpacesSelector::fromKeyStopRepetition(%this)
{
    if ($gCustomSpacesKeyStrokeRepeatSched != 0)
    {
        cancel($gCustomSpacesKeyStrokeRepeatSched);
        $gCustomSpacesKeyStrokeRepeatSched = 0;
    }
    return ;
}
function CustomSpacesSelector::fromKeyStartMovingUp(%this)
{
    %selectedListBox = CSSelectorListCtrl.getSelectedList();
    if (!isObject(%selectedListBox))
    {
        return ;
    }
    %indexOfHilitedCell = %selectedListBox.getObjectIndex(%selectedListBox.getHilitedCell());
    %lastIndexPossible = %selectedListBox.getCount() - 1;
    if (%indexOfHilitedCell == 0)
    {
        if (%selectedListBox.myIndex > 0)
        {
            %nextListBox = CustomSpacesSelector.vars[("aptCategoryListBoxes",%selectedListBox.myIndex - 1)];
            %nextListBox.getObject(%nextListBox.getCount() - 1).onMouseDown();
        }
        else
        {
            CSSelectorScrollCtrl.scrollToTop();
        }
    }
    else
    {
        %selectedListBox.getObject(%indexOfHilitedCell - 1).onMouseDown();
    }
    $gCustomSpacesKeyStrokeRepeatSched = %this.schedule($gCustomSpacesKeyStrokeRepFrequency, fromKeyStartMovingUp);
    return ;
}
function CustomSpacesSelector::fromKeyStartMovingDown(%this)
{
    %selectedListBox = CSSelectorListCtrl.getSelectedList();
    if (!isObject(%selectedListBox))
    {
        if (CustomSpacesSelector.vars["aptCategoryCount"] > 0)
        {
            CustomSpacesSelector.vars[("aptCategoryListBoxes",0)].getObject(0).onMouseDown();
        }
        return ;
    }
    %indexOfHilitedCell = %selectedListBox.getObjectIndex(%selectedListBox.getHilitedCell());
    %lastIndexPossible = %selectedListBox.getCount() - 1;
    if (%indexOfHilitedCell == %lastIndexPossible)
    {
        if (%selectedListBox.myIndex < (CustomSpacesSelector.vars["aptCategoryCount"] - 1))
        {
            %nextListBox = CustomSpacesSelector.vars[("aptCategoryListBoxes",%selectedListBox.myIndex + 1)];
            %nextListBox.getObject(0).onMouseDown();
        }
    }
    else
    {
        %selectedListBox.getObject(%indexOfHilitedCell + 1).onMouseDown();
    }
    $gCustomSpacesKeyStrokeRepeatSched = %this.schedule($gCustomSpacesKeyStrokeRepFrequency, fromKeyStartMovingDown);
    return ;
}
function CSSelectorDescriptionCtrl::onURL(%this, %text)
{
    if (%text $= "COPY_VURL")
    {
        %entryName = CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName;
        %entry = CSSelectorListCtrl.getSelectedList().getEntryByName(%entryName);
        setClipboard(vurlClearResolution(%entry.vurl));
    }
    return ;
}
function CSSelectorListCtrl::getSelectedList(%this)
{
    return %this.selectedCSSelectorCtrl;
}
function CSSelectorListCtrl::setSelectedList(%this, %listBox)
{
    if (isObject(%this.selectedCSSelectorCtrl))
    {
        if (%this.selectedCSSelectorCtrl.getId() == %listBox.getId())
        {
            return ;
        }
        %this.selectedCSSelectorCtrl.reseatChildren();
    }
    %this.selectedCSSelectorCtrl = %listBox;
    return ;
}
function CSSelectorListCtrl::teleportToSelected(%this)
{
    %selectorCtrl = %this.getSelectedList();
    if (!isObject(%selectorCtrl))
    {
        return ;
    }
    %entryName = %selectorCtrl.getHilitedCell().entryName;
    %entry = %selectorCtrl.getEntryByName(%entryName);
    if (!(($Player::Name $= %entry.owner)) && !$player.rolesPermissionCheckNoWarn("customspaceMaster"))
    {
        if ((stricmp(%entry.access, "FriendsOnly") == 0) && !%selectorCtrl.ownerIsFriend(%entry.owner))
        {
            MessageBoxOK($MsgCat::custSpacSel["APT-IS-FRIENDSONLY-TITLE"], $MsgCat::custSpacSel["APT-IS-FRIENDSONLY-TEXT"], "");
            return ;
        }
        if (stricmp(%entry.access, "PasswordProtected") == 0)
        {
            MessageBoxTextEntryWithCancel($MsgCat::custSpacSel["APT-REQUIRES-PASSWORD-TITLE"], $MsgCat::custSpacSel["APT-REQUIRES-PASSWORD-TEXT"], CSSelectorListCtrl_tackOnPassword, "", 0);
            return ;
        }
        if (stricmp(%entry.access, "Locked") == 0)
        {
            MessageBoxOK($MsgCat::custSpacSel["APT-IS-LOCKED-TITLE"], $MsgCat::custSpacSel["APT-IS-LOCKED-TEXT"], "");
            return ;
        }
    }
    %this.teleportToSpaceName(%entryName, "");
    return ;
}
function CSSelectorListCtrl_tackOnPassword(%password)
{
    %entryName = CSSelectorListCtrl.getSelectedList().getHilitedCell().entryName;
    CSSelectorListCtrl.teleportToSpaceName(%entryName, %password);
    return ;
}
function CustomSpacesSelector::doTeleportToMyApartment(%this)
{
    if (!($Player::myPlaceVURL $= ""))
    {
        %this.doTeleportToSpace($Player::myPlaceVURL, "", "");
    }
    return ;
}
function CSSelectorListCtrl::teleportToSpaceName(%this, %spaceName, %password)
{
    %selectedCSSelectorCtrl = %this.getSelectedList();
    if (!isObject(%selectedCSSelectorCtrl))
    {
        error("error in retrieving list of space entries <- " @ getScopeName());
        return ;
    }
    %space = %selectedCSSelectorCtrl.getEntryByName(%spaceName);
    if (!isObject(%space))
    {
        error("error in retrieving object record of space entry in list <- " @ getScopeName());
        return ;
    }
    CustomSpacesSelector.doTeleportToSpace(%space.vurl, %space.name, %password);
    return ;
}
function CustomSpacesSelector::doTeleportToSpace(%this, %vurl, %spaceName, %password)
{
    if (!(%spaceName $= ""))
    {
        showTransitionMessage(%spaceName, 0);
    }
    %vurlObject = vurlGetParsedVurl(%vurl);
    %vurlObject.setPassword(%password);
    %vurlObject.cbSuccessExpected = "CustomSpacesSelector_vurlSuccessExpected";
    %vurlObject.cbSuccess = "CustomSpacesSelector_vurlTransitionSucceeded";
    %vurlObject.cbReportError = "CustomSpacesSelector_vurlTransitionFailed";
    %vurlObject.clearResolutionAndExecute();
    return ;
}
function CustomSpacesSelector_vurlSuccessExpected(%vurl)
{
    CustomSpacesSelector.close();
    return ;
}
function CustomSpacesSelector_vurlTransitionSucceeded(%vurl)
{
    CustomSpacesSelector.clearHeaderAndListBoxes();
    CustomSpacesSelector.lastSelectedEntry = "";
    return ;
}
function CustomSpacesSelector_vurlTransitionFailed(%vurl, %errorCode, %unused)
{
    if ((((stricmp(%errorCode, "missingdoorcode") == 0) || (stricmp(%errorCode, "missingpassword") == 0)) || (stricmp(%errorCode, "incorrectdoorcode") == 0)) || (stricmp(%errorCode, "incorrectpassword") == 0))
    {
        MessageBoxTextEntryWithCancel($MsgCat::custSpacSel["APT-BAD-PASSWORD-TITLE"], $MsgCat::custSpacSel["APT-REQUIRES-PASSWORD-TEXT"], CSSelectorListCtrl_tackOnPassword, "", 0);
        return 1;
    }
    else
    {
        %title = "Teleport Failed";
        %text = "Sorry, couldn\'t get into this apartment at this time. Please try again later.";
        %buttons = "Try Again" TAB "Go to Another Apartment" TAB "Cancel";
        %dlg = MessageBoxCustom(%title, %text, %buttons);
        %dlg.callback[0] = "vurlClearResolutionAndExecute( \"" @ %vurl @ "\");";
        %dlg.callback[1] = "CustomSpacesSelector.refresh();";
        %dlg.callback[2] = "";
        return 1;
    }
    return 0;
}
function CustomSpacesSelector::open(%this, %building)
{
    %building = trim(%building);
    if ((((%building $= "") || (%building $= 0)) && $ETS::devMode) && !$gGetFakeBuildingDirectory)
    {
        warn(getScopeName() SPC "- trying to open building directory with building name \'" @ %building @ "\'");
        return ;
    }
    %this.buildingName = %building;
    %this.clearHeaderAndListBoxes();
    %this.lastSelectedEntry = "";
    buildingDirectoryMap.replaceAllOthers();
    %this.vars["textAreaWidth"] = (getWord(CSSelectorScrollCtrl.getExtent(), 0) - %this.vars["scrollBarWidth"]) - (2 * %this.vars["textAreaPadding"]);
    CustomSpacesSelector_TITLE.setVisible(0);
    CustomSpacesSelector_NO_APARTMENTS.setVisible(0);
    CustomSpacesSelector_VISITandBUYBUTTON.setVisible(0);
    CustomSpacesSelector_GOBUTTON.setVisible(0);
    CustomSpacesSelector_ENTERPASSWORDBUTTON.setVisible(0);
    CustomSpacesSelector_RETURNTOLOBBY.setVisible(!(CustomSpaceClient::GetSpaceImIn() $= ""));
    CustomSpacesSelector_LOADING.setVisible(1);
    %this.container.setVisible(1);
    PlayGui.focusAndRaise(%this.container);
    if ($ETS::devMode && $gGetFakeBuildingDirectory)
    {
        CSSelectorDescriptionCtrl.setTextAndUpdateWithCallback("(loading...)", %this, "getFakeBuildingDirectory", "");
    }
    else
    {
        CSSelectorDescriptionCtrl.setTextAndUpdateWithCallback("(loading...)", "", "getBuildingDirectory", %building @ ", customSpaceSelGotData, customSpaceSelFailed");
    }
    CustomSpacesSelector_MYPLACE.applyBaseText();
    CustomSpacesSelector_RETURNTOLOBBY.applyBaseText();
    return ;
}
function CustomSpacesSelector::close(%this)
{
    %this.buildingName = "";
    %this.container.setVisible(0);
    PlayGui.focusTopWindow();
    if (buildingDirectoryMap.isActive())
    {
        buildingDirectoryMap.restoreAllOthers();
    }
    return 1;
}
function CustomSpacesSelectorContainer::close(%this)
{
    return CustomSpacesSelector.close();
}
function CustomSpacesSelector::addHeaderAndListBoxes(%this, %type)
{
    %newHeaderAndListBoxIndex = %this.vars["aptCategoryCount"];
    %headerText = new GuiBitmapCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = %this.vars["textTitleLeft"] SPC %this.vars["textTitleTop"];
        extent = "120 9";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    if (%type $= "MODEL")
    {
        %headerText.setBitmap("platform/client/ui/buildingDir_check_out_the_models");
    }
    else
    {
        if (%type $= "FEATURED")
        {
            %headerText.setBitmap("platform/client/ui/buildingDir_featured_apartments");
        }
        else
        {
            if (%type $= "CELEBSPACE")
            {
                %headerText.setBitmap("platform/client/ui/buildingDir_celebrity_apartments");
            }
            else
            {
                if (%type $= "MYPLACE")
                {
                    %headerText.setBitmap("platform/client/ui/buildingDir_my_apartment");
                }
                else
                {
                    if (%type $= "RESIDENCE")
                    {
                        %headerText.setBitmap("platform/client/ui/buildingDir_resident_apartments");
                    }
                    else
                    {
                        %headerText.setBitmap("");
                    }
                }
            }
        }
    }
    %headerBox = new GuiControl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = %this.vars["textAreaPadding"] SPC %this.calculateHeaderTop(%this.vars["aptCategoryCount"]);
        extent = getWord(%headerText.getExtent(), 0) + 3 SPC %this.vars["headerboxHeight"];
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 64;
    };
    %headerBox.add(%headerText);
    CSSelectorListCtrl.add(%headerBox);
    %this.vars["aptCategoryHeaderBoxes",%newHeaderAndListBoxIndex] = %headerBox;
    %listBox = CSSelectorCtrl::makeNewListBox(%type);
    %positionLeft = getWord(%headerBox.getPosition(), 0) + CustomSpacesSelector.vars["textAreaIndentation"];
    %positionTop = (getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1)) + %this.vars["spaceBetweenHeaderAndListBoxes"];
    %extentWidth = getWord(%listBox.getExtent(), 0);
    %extentHeight = getWord(%listBox.getExtent(), 1);
    %listBox.resize(%positionLeft, %positionTop, %extentWidth, %extentHeight);
    CSSelectorListCtrl.add(%listBox);
    %this.vars["aptCategoryListBoxes",%newHeaderAndListBoxIndex] = %listBox;
    %listBox.myIndex = %newHeaderAndListBoxIndex;
    if (CustomSpacesSelector.vars["aptCategoriesInUse"] $= "")
    {
        CustomSpacesSelector.vars["aptCategoriesInUse"] = %type;
    }
    else
    {
        CustomSpacesSelector.vars["aptCategoriesInUse"] = CustomSpacesSelector.vars["aptCategoriesInUse"] TAB %type;
    }
    %this.vars["aptCategoryIndexes",%type] = %newHeaderAndListBoxIndex;
    %this.vars["aptCategoryCount"] = %newHeaderAndListBoxIndex + 1;
    return %listBox;
}
function CustomSpacesSelector::calculateHeaderTop(%this, %ordinal)
{
    if (%ordinal < 0)
    {
        %ordinal = 0;
    }
    if (%ordinal > %this.vars["aptCategoryCount"])
    {
        %ordinal = %this.vars["aptCategoryCount"];
    }
    if (%ordinal == 0)
    {
        %top = %this.vars["textAreaPadding"];
    }
    else
    {
        %top = getWord(%this.vars[("aptCategoryListBoxes",%ordinal - 1)].getPosition(), 1);
        %top = %top + getWord(%this.vars[("aptCategoryListBoxes",%ordinal - 1)].getExtent(), 1);
        %top = %top + %this.vars["textAreaPadding"];
        %top = %top + %this.vars["textAreaInterCategoryPadding"];
    }
    return %top;
}
function CustomSpacesSelector::clearHeaderAndListBoxes(%this)
{
    CSSelectorListCtrl.clear();
    %i = 0;
    while (%i < CustomSpacesSelector.vars["aptCategoryCount"])
    {
        %this.vars[("aptCategoryHeaderBoxes",%i)].delete();
        %this.vars["aptCategoryHeaderBoxes",%i] = "";
        %this.vars[("aptCategoryListBoxes",%i)].delete();
        %this.vars["aptCategoryListBoxes",%i] = "";
        %i = %i + 1;
    }
    %this.vars["aptCategoryCount"] = 0;
    %this.vars["aptCategoriesInUse"] = "";
    return ;
}
function CustomSpacesSelector::setTypeIndexes(%this, %typeList)
{
    %currentIndex = 0;
    %i = 0;
    while (%i < getFieldCount(%this.vars["aptCategories"]))
    {
        %type = getField(%this.vars["aptCategories"], %i);
        if (findField(%typeList, %type) >= 0)
        {
            %this.vars["aptCategoryIndexes",%type] = %currentIndex;
            %currentIndex = %currentIndex + 1;
        }
        %i = %i + 1;
    }
}

function CustomSpacesSelector::getTypeIndex(%this, %type)
{
    %index = %this.vars[("aptCategoryIndexes",%type)];
    return %index;
}
function CustomSpacesSelector::getListBox(%this, %type)
{
    %listBox = %this.vars[("aptCategoryListBoxes",%this.getTypeIndex(%type))];
    return %listBox;
}
function CustomSpacesSelector::saveListBox(%this, %listBox, %type)
{
    %this.vars["aptCategoryListBoxes",%this.getTypeIndex(%type)] = %listBox;
    return ;
}
function CustomSpacesSelector::rearrangeListBoxes(%this)
{
    %typesAvailable = %this.vars["aptCategories"];
    %typesInUse = %this.vars["aptCategoriesInUse"];
    %numTypesAvailable = getFieldCount(%typesAvailable);
    %typesSkipped = 0;
    %i = 0;
    while (%i < (%numTypesAvailable - 1))
    {
        %currentTypeToExamine = getField(%typesAvailable, %i);
        %indexOfCurrentType = findField(%typesInUse, %currentTypeToExamine);
        if (%indexOfCurrentType == (%i - %typesSkipped))
        {
            continue;
        }
        if (%indexOfCurrentType == -1)
        {
            %typesSkipped = %typesSkipped + 1;
        }
        else
        {
            %indexOfWhereTypeShouldBe = findField(%typesAvailable, %currentTypeToExamine) - %typesSkipped;
            %tempHeader = %this.vars[("aptCategoryHeaderBoxes",%indexOfWhereTypeShouldBe)];
            %tempListBox = %this.vars[("aptCategoryListBoxes",%indexOfWhereTypeShouldBe)];
            %this.vars["aptCategoryHeaderBoxes",%indexOfWhereTypeShouldBe] = %this.vars[("aptCategoryHeaderBoxes",%indexOfCurrentType)];
            %this.vars["aptCategoryListBoxes",%indexOfWhereTypeShouldBe] = %this.vars[("aptCategoryListBoxes",%indexOfCurrentType)];
            %this.vars[("aptCategoryListBoxes",%indexOfWhereTypeShouldBe)].myIndex = %indexOfWhereTypeShouldBe;
            %this.vars["aptCategoryHeaderBoxes",%indexOfCurrentType] = %tempHeader;
            %this.vars["aptCategoryListBoxes",%indexOfCurrentType] = %tempListBox;
            %this.vars[("aptCategoryListBoxes",%indexOfCurrentType)].myIndex = %indexOfCurrentType;
            %typesInUse = setField(%typesInUse, %indexOfCurrentType, getField(%typesInUse, %indexOfWhereTypeShouldBe));
            %typesInUse = setField(%typesInUse, %indexOfWhereTypeShouldBe, %currentTypeToExamine);
        }
        %i = %i + 1;
    }
    %this.vars["aptCategories"] = %typesAvailable;
    %this.vars["aptCategoriesInUse"] = %typesInUse;
    %this.setTypeIndexes(%this.vars["aptCategoriesInUse"]);
    %this.adjustListBoxPositions(1);
    return ;
}
function CustomSpacesSelector::adjustListBoxPositions(%this, %doSort)
{
    if (%this.vars["aptCategoryCount"] < 1)
    {
        return ;
    }
    %lastListBoxBottomEdgeY = 0;
    %i = 0;
    while (%i < %this.vars["aptCategoryCount"])
    {
        %headerBox = %this.vars[("aptCategoryHeaderBoxes",%i)];
        %listBox = %this.vars[("aptCategoryListBoxes",%i)];
        %headerBox.reposition(%this.vars["textAreaPadding"], %this.calculateHeaderTop(%i));
        %listBox.refreshFromSet(%doSort);
        %positionLeft = getWord(%headerBox.getPosition(), 0) + CustomSpacesSelector.vars["textAreaIndentation"];
        %positionTop = (getWord(%headerBox.getPosition(), 1) + getWord(%headerBox.getExtent(), 1)) + %this.vars["spaceBetweenHeaderAndListBoxes"];
        %extentWidth = CustomSpacesSelector.vars["textAreaWidth"] - CustomSpacesSelector.vars["textAreaIndentation"];
        %extentHeight = getWord(%listBox.getExtent(), 1);
        %listBox.resize(%positionLeft, %positionTop, %extentWidth, %extentHeight);
        %lastListBoxBottomEdgeY = %positionTop + getWord(%listBox.getExtent(), 1);
        %i = %i + 1;
    }
    %newHeight = %lastListBoxBottomEdgeY + %this.vars["textAreaPadding"];
    CSSelectorListCtrl.resize(getWord(CSSelectorListCtrl.getPosition(), 0), getWord(CSSelectorListCtrl.getPosition(), 1), getWord(CSSelectorListCtrl.getExtent(), 0), %newHeight);
    return ;
}
function CustomSpacesSelector::getHeaderBox(%this, %type)
{
    return %this.vars[("aptCategoryHeaderBoxes",%this.getTypeIndex(%type))];
}
function customSpaceSelGotData(%buildingInfo, %buildingDir)
{
    CustomSpacesSelector_LOADING.setVisible(0);
    CSLegendContainer.setVisible(1);
    if (!(%buildingInfo.name $= ""))
    {
        %titleBarText = $gMlStyle["CSProfileTitleText"] @ Buildings::GetDescription(%buildingInfo.name) $= "" ? %buildingInfo : Buildings::GetDescription(%buildingInfo.name) SPC $gMlStyle["CSProfileSubTitleText"] @ "building directory";
        CustomSpacesSelector_TITLE.setText(%titleBarText);
    }
    else
    {
        CustomSpacesSelector_TITLE.setText("Building Directory");
    }
    CustomSpacesSelector_TITLE.setVisible(1);
    CSSelectorDescriptionCtrl.schedule(50, setTextAndUpdate, %buildingInfo.description);
    CustomSpacesSelector.clearHeaderAndListBoxes();
    CustomSpacesSelector.myApartment = 0;
    %myPlaceIsInThisBuilding = 0;
    %spacesCount = %buildingDir.getCount();
    if (%spacesCount == 0)
    {
        CSSelectorListCtrl.setVisible(0);
        CustomSpacesSelector_NO_APARTMENTS.setVisible(1);
    }
    else
    {
        CustomSpacesSelector_NO_APARTMENTS.setVisible(0);
        CSSelectorListCtrl.setVisible(1);
        %i = 0;
        while (%i < %spacesCount)
        {
            %space = %buildingDir.getObject(%i);
            %floorPlanFound = 0;
            %j = 0;
            while (!%floorPlanFound)
            {
                %floorPlanFound = %space.floorPlanName $= %buildingInfo.floorplan[%j].name;
                %j = %j + 1;
            }
            if (!%floorPlanFound)
            {
                warn("customSpaceSelGotData() got listing for apartment \'" @ %space.name @ "\' with owner \'" @ %space.owner @ "\' that refers to non-existent floorPlan \'" @ %space.floorPlanName @ "\'");
            }
            else
            {
                %space.city = %buildingInfo.city;
                %info = PlayerInfoMap.get(%space.owner);
                if (isObject(%info))
                {
                    %space.ownerAge = StripMLControlChars(%info.age);
                    if (%space.ownerAge $= "")
                    {
                        %space.ownerAge = "-";
                    }
                    else
                    {
                        %space.ownerAge = %space.ownerAge SPC "yrs";
                    }
                    %space.ownerSex = %info.gender;
                    if (%space.ownerSex $= "f")
                    {
                        %space.ownerSex = "F";
                    }
                    else
                    {
                        if (%space.ownerSex $= "m")
                        {
                            %space.ownerSex = "M";
                        }
                        else
                        {
                            %space.ownerSex = "-";
                        }
                    }
                    %space.ownerLocation = StripMLControlChars(%info.location);
                    if (%space.ownerLocation $= "")
                    {
                        %space.ownerLocation = "(hidden)";
                    }
                }
                else
                {
                    %space.ownerAge = "-";
                    %space.ownerSex = "-";
                    %space.ownerLocation = "-";
                }
                %type = %space.type;
                if ($Player::Name $= %space.owner)
                {
                    %type = "MYPLACE";
                    if (findField(CustomSpacesSelector.vars["aptCategoriesInUse"], %type) >= 0)
                    {
                        %listBox = CustomSpacesSelector.getListBox(%type);
                    }
                    else
                    {
                        %listBox = CustomSpacesSelector.addHeaderAndListBoxes(%type);
                        CustomSpacesSelector.saveListBox(%listBox, %type);
                    }
                    %myPlaceIsInThisBuilding = 1;
                }
                if (!(($Player::Name $= %space.owner)) && %space.isFeatured)
                {
                    if (%space.isFeatured)
                    {
                        %type = "FEATURED";
                    }
                    if (findField(CustomSpacesSelector.vars["aptCategoriesInUse"], %type) >= 0)
                    {
                        %listBox = CustomSpacesSelector.getListBox(%type);
                    }
                    else
                    {
                        %listBox = CustomSpacesSelector.addHeaderAndListBoxes(%type);
                        CustomSpacesSelector.saveListBox(%listBox, %type);
                    }
                }
                %listBox.updateEntry(%space);
                CustomSpacesSelector.vars["descriptions",%space.name] = "";
            }
            %i = %i + 1;
        }
        CustomSpacesSelector.rearrangeListBoxes();
    }
    if (0)
    {
        %amAtHome = (!(($Player::myPlaceVURL $= "")) && ($CSSpaceInfo != 0)) && (stricmp($CSSpaceInfo.owner, $player.getShapeName()) == 0);
        CustomSpacesSelector_MYPLACE_container.setVisible(!($Player::myPlaceVURL $= ""));
        %style = %amAtHome ? "CSProfileSpecialLinkDisabled" : "CSProfileSpecialLink";
        %alpha = %amAtHome ? 85 : 255;
        CustomSpacesSelector_MYPLACE.applyBaseTextWithStyle(%style);
        CustomSpacesSelector_MYPLACE.setActive(!%amAtHome);
        CustomSpacesSelector_MYPLACEIcon.modulationColor = "255 255 255" SPC %alpha;
    }
    if (CustomSpacesSelector.vars["aptCategoryCount"] == 0)
    {
        CSSelectorListCtrl.setVisible(0);
        CustomSpacesSelector_NO_APARTMENTS.setVisible(1);
    }
    %buildingDir.delete();
    return ;
}
function customSpaceSelFailed(%building)
{
    CustomSpacesSelector_LOADING.setVisible(0);
    CSLegendContainer.setVisible(0);
    if (!(%building $= ""))
    {
        %titleBarText = $gMlStyle["CSProfileTitleText"] @ Buildings::GetDescription(%building) $= "" ? %building : Buildings::GetDescription(%building) SPC $gMlStyle["CSProfileSubTitleText"] @ "building directory";
        CustomSpacesSelector_TITLE.setText(%titleBarText);
    }
    else
    {
        CustomSpacesSelector_TITLE.setText("Building Directory");
    }
    CustomSpacesSelector_TITLE.setVisible(1);
    CSSelectorDescriptionCtrl.setTextAndUpdate("Could not connect! Please try back later.");
    return ;
}
function CustomSpacesSelector_MYPLACE::onURL(%this, %url)
{
    %url = getWords(%url, 1);
    if (%url $= "MY_PLACE")
    {
        geTGF.openToTabName("myplace");
    }
    return ;
}
function CustomSpacesSelector_RETURNTOLOBBY::onURL(%this, %url)
{
    %url = getWords(%url, 1);
    if (%url $= "EXIT_TO_LOBBY")
    {
        MessageBoxYesNo($MsgCat::custSpacSel["RETURN-TO-LOBBY-TITLE"], $MsgCat::custSpacSel["RETURN-TO-LOBBY-TEXT"], "CustomSpacesSelector.doReturnToLobby();", "");
    }
    return ;
}
function CustomSpacesSelector::doReturnToLobby(%this)
{
    %this.doTeleportToSpace(Buildings::getReturnVURL($CSBuildingName), "Lobby", "");
    return ;
}
function CustomSpacesSelector_CLOSEWINDOW::onURL(%this, %url)
{
    %url = getWords(%url, 1);
    if (%url $= "CLOSE_WINDOW")
    {
        CustomSpacesSelector.close();
    }
    return ;
}
function CSSelectorLineVisitNowMLText::onURL(%this, %url)
{
    if (%url $= "gamelink go")
    {
        %this.getParent().onMouseDown();
        CSSelectorListCtrl.teleportToSelected();
    }
    return ;
}
function CustomSpacesSelector::refresh(%this)
{
    if (buildingDirectoryMap.isActive())
    {
        buildingDirectoryMap.restoreAllOthers();
    }
    %this.open(%this.buildingName);
    CustomSpacesSelector_TITLE.setVisible(1);
    return ;
}
function CustomSpacesSelector_FILTER::onKeyDown(%this, %unused, %unused)
{
    CustomSpacesSelector_FILTER_OVERLAY.setVisible(0);
    return 0;
}
function CustomSpacesSelector_FILTER::onKeyUp(%this, %unused, %unused)
{
    %fieldIsVisible = %this.isVisible();
    CustomSpacesSelector_FILTER_OVERLAY.setVisible(%this.getValue() $= "");
    CustomSpacesSelector.doFilter(%this.getValue());
    return 0;
}
function CustomSpacesSelector::doFilter(%this, %filterByText)
{
    %this.filterByText = %filterByText;
    %this.adjustListBoxPositions(0);
    return ;
}
function CSSelectorCtrl::includeSpaceInList(%this, %entry, %filterByText)
{
    if (!isObject(%entry))
    {
        return 0;
    }
    if (!(%entry.type $= "RESIDENCE"))
    {
        return 1;
    }
    if (%entry.isFeatured)
    {
        return 1;
    }
    if ($Player::Name $= %entry.owner)
    {
        return 1;
    }
    %filterByText = strlwr(%filterByText);
    if (%filterByText $= "")
    {
        return 1;
    }
    if (strstr(strlwr(%entry.owner), %filterByText) > -1)
    {
        return 1;
    }
    if (strstr(strlwr(%entry.description), %filterByText) > -1)
    {
        return 1;
    }
    return 0;
}
