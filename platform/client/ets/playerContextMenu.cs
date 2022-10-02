$flowerGiftingEnabled = 0;
function PlayerContextMenu::init(%this, %playerName, %friendStatus, %isIgnore, %playerClicked)
{
    if (!isObject(%playerClicked))
    {
        %playerClicked = Player::findPlayerInstance(%playerName);
    }
    %this.clear();
    %hasName = !(%playerName $= "");
    if (!%hasName)
    {
        %playerName = "no-name";
    }
    %isNPC = isNPCName(%playerName);
    %isRentabot = rentabot_isRentabotName(%playerName);
    %sameServer = isObject(%playerClicked) || (UserListFriends.get(%playerName).serverName $= $ServerName);
    %isRealPlayer = isObject(%playerClicked) ? %playerClicked.isClassAIPlayer() : 0;
    %isIdle = isObject(%playerClicked) ? %playerClicked.getAFK() : 0;
    %grey = "255 255 255 128";
    %white = "255 255 255 255";
    %this.addScheme(1, %grey, %grey, %grey);
    %this.addScheme(2, %white, %white, %white);
    %schemeNormal = 0;
    %schemeDisabled = 1;
    %schemeProfile = 0;
    %schemeFavorite = 0;
    %schemePM = isObject(pChat) ? 0 : 1;
    %schemeIgnore = 0;
    %schemeReport = 0;
    %schemeTeleport = 0;
    %schemeMimic = 0;
    %schemeFlyTo = 0;
    %schemeTrack = 0;
    %schemeSaySumpn = 0;
    %schemeWhisper = %schemePM;
    %schemeGifting = 0;
    %schemeDanceWith = 0;
    %schemeKiss = 0;
    %schemeInfo = 2;
    %n = -1;
    if (%hasName)
    {
        if (%playerClicked != $player)
        {
            %onlineHere = BuddyHudWin.isOnlineHereOrNotFavorite(%playerName);
            %ignorable = 1;
            if (isObject(%playerClicked))
            {
                %ignorable = !%playerClicked.rolesPermissionCheckNoWarn("omnivocal");
            }
            if (isObject(%playerClicked))
            {
                if (!(getWearingItemWithMoreInfo(%playerClicked) $= ""))
                {
                    %this.add("** Look At My Clothes **", %n = %n + 1, %schemeProfile);
                }
            }
            %this.add("View Profile", %n = %n + 1, %schemeProfile);
            if (%friendStatus $= "friends")
            {
                %this.add("Remove from Friends", %n = %n + 1, %schemeFavorite);
            }
            else
            {
                if (%friendStatus $= "favorite")
                {
                    %this.add("Cancel Friend Request", %n = %n + 1, %schemeFavorite);
                }
                else
                {
                    if (%friendStatus $= "fan")
                    {
                        %this.add("Accept Friend Request", %n = %n + 1, %schemeFavorite);
                        %this.add("Decline Friend Request", %n = %n + 1, %schemeFavorite);
                    }
                    else
                    {
                        if (%friendStatus $= "none")
                        {
                            %this.add("Add to Friends", %n = %n + 1, %schemeFavorite);
                        }
                    }
                }
            }
            if (%onlineHere)
            {
                %this.add("Whisper", %n = %n + 1, %schemePM);
            }
            if (!rentabot_isRentabotName(%playerName))
            {
                if ((((((%sameServer && !%isNPC) || $StandAlone) && %onlineHere) && !%isIgnore) && geGiftingPanel.isInRange(%playerClicked)) && !%isIdle)
                {
                    %this.add("Two-Player Action..", %n = %n + 1, %schemeNormal);
                }
                else
                {
                    if (!geGiftingPanel.isInRange(%playerClicked))
                    {
                        %this.add("Two-Player Action.. - Too Far!", %n = %n + 1, %schemeDisabled);
                    }
                    else
                    {
                        if (%isIdle)
                        {
                            %this.add("Two-Player Action.. - Idle!", %n = %n + 1, %schemeDisabled);
                        }
                    }
                }
            }
            if (!rentabot_isRentabotName(%playerName))
            {
                if ((((%sameServer && %onlineHere) && !%isIgnore) && geGiftingPanel.isInRange(%playerClicked)) && !%isIdle)
                {
                    %this.add("Give vCurrency", %n = %n + 1, %schemeNormal);
                }
                else
                {
                    if (!geGiftingPanel.isInRange(%playerClicked))
                    {
                        %this.add("Give vCurrency - Too Far!", %n = %n + 1, %schemeDisabled);
                    }
                    else
                    {
                        if (%isIdle)
                        {
                            %this.add("Give vCurrency - Idle!", %n = %n + 1, %schemeDisabled);
                        }
                    }
                }
            }
            if (SkuManager.hasSkuWithAnyTags($player.getActiveSKUs(), "drink") && !rentabot_isRentabotName(%playerName))
            {
                if ((((%sameServer && %onlineHere) && !%isIgnore) && geGiftingPanel.isInRange(%playerClicked)) && !%isIdle)
                {
                    %this.add("Give Drink", %n = %n + 1, %schemeNormal);
                }
                else
                {
                    if (!geGiftingPanel.isInRange(%playerClicked))
                    {
                        %this.add("Give Drink - Too Far!", %n = %n + 1, %schemeDisabled);
                    }
                    else
                    {
                        if (%isIdle)
                        {
                            %this.add("Give Drink - Idle!", %n = %n + 1, %schemeDisabled);
                        }
                    }
                }
            }
            %this.add("Give Gift", %n = %n + 1, %schemeNormal);
            if (SkuManager.hasSkuWithAnyTags($player.getActiveSKUs(), "drinkMaker") && !rentabot_isRentabotName(%playerName))
            {
                if ((((%sameServer && %onlineHere) && !%isIgnore) && geGiftingPanel.isInRange(%playerClicked)) && !%isIdle)
                {
                    %this.add("Make Drink", %n = %n + 1, %schemeNormal);
                }
                else
                {
                    if (!geGiftingPanel.isInRange(%playerClicked))
                    {
                        %this.add("Make Drink - Too Far!", %n = %n + 1, %schemeDisabled);
                    }
                    else
                    {
                        if (%isIdle)
                        {
                            %this.add("Make Drink - Idle!", %n = %n + 1, %schemeDisabled);
                        }
                    }
                }
            }
            if (isObject(%playerClicked) && %playerClicked.getCanHandleMusicRequest())
            {
                %this.add("Request Music", %n = %n + 1, %schemeNormal);
            }
            if (isObject(%playerClicked))
            {
                %this.add("Applaud For Me!");
            }
            if ($player.isHostOrCohost())
            {
                if ($player.isHost())
                {
                    if (%isRealPlayer)
                    {
                        if (%playerClicked.isCohost())
                        {
                            %this.add("This Space: Unmake Co-Host", %n = %n + 1, %schemeNormal);
                        }
                        else
                        {
                            %this.add("This Space: Make Co-Host", %n = %n + 1, %schemeNormal);
                        }
                    }
                }
                if (%isRealPlayer && !%playerClicked.isHost())
                {
                    %this.add("This Space: Kick", %n = %n + 1, %schemeNormal);
                }
                if (!%isRentabot && $player.isHost())
                {
                    if (findField($CSBlockedList, %playerName) == -1)
                    {
                        %this.add("This Space: Block", %n = %n + 1, %schemeNormal);
                    }
                    else
                    {
                        %this.add("This Space: Unblock", %n = %n + 1, %schemeNormal);
                    }
                }
                if (isObject(%playerClicked))
                {
                    if (CustomSpaceClient::isOwner() && %isRentabot)
                    {
                        %this.add("This Space: Customize", %n = %n + 1, %schemeNormal);
                    }
                    if (((($player.isHostOrCohost() && !%playerClicked.rolesPermissionCheckNoWarn("customspaceImmune")) && $player.isHost()) || !%playerClicked.isHost()) && !%playerClicked.isClassAIPlayer())
                    {
                        %this.add("This Space: Summon", %n = %n + 1, %schemeNormal);
                        %this.add("This Space: Respawn", %n = %n + 1, %schemeNormal);
                    }
                }
            }
            if (%onlineHere && %ignorable)
            {
                if (%isIgnore)
                {
                    %this.add("Unignore", %n = %n + 1, %schemeIgnore);
                }
                else
                {
                    %this.add("Ignore", %n = %n + 1, %schemeIgnore);
                }
            }
            if (((((%friendStatus $= "friends") || %isNPC) || CustomSpaceClient::isOwner()) && isObject(%playerClicked)) && (%playerClicked != $player))
            {
                %this.add("Teleport To", %n = %n + 1, %schemeTeleport);
            }
            if (%onlineHere && !%isRentabot)
            {
                %this.add("Report Abuse", %n = %n + 1, %schemeProfile);
            }
            if ($flowerGiftingEnabled)
            {
                if (isObject(%playerClicked) && %isNPC)
                {
                    %skuSelf = getSpecialSKU($player, "flower");
                    %skuThem = getSpecialSKU(%playerClicked, "flower");
                    if (%playerClicked.hasActiveSKU(%skuThem) && !$player.hasInventorySKU(%skuSelf))
                    {
                        %this.add("Take Flower", %n = %n + 1, %schemeGifting);
                    }
                }
                if (isObject(%playerClicked) && !%isNPC)
                {
                    %skuSelf = getSpecialSKU($player, "flower");
                    if ($player.hasInventorySKU(%skuSelf))
                    {
                        %this.add("Give Flower", %n = %n + 1, %schemeGifting);
                    }
                }
            }
            if (isObject(%playerClicked))
            {
                %helpmesku = getSpecialSKU(%playerClicked, "helpmebadge");
                if (%playerClicked.hasActiveSKU(%helpmesku))
                {
                    %this.addIfPermitted("answerHelpMe", "Turn off Help Request", %n = %n + 1, %schemeNormal);
                }
            }
        }
        else
        {
            if (isObject(%playerClicked))
            {
                if (!(getWearingItemWithMoreInfo(%playerClicked) $= ""))
                {
                    %this.add("** Look At My Clothes **", %n = %n + 1, 0);
                }
            }
            %this.add("My Profile & Account (web)", %n = %n + 1, 0);
            %this.add("Edit Away Message", %n = %n + 1, 0);
            if (isIdle())
            {
                %this.add("Back From Idle", %n = %n + 1, 0);
            }
            else
            {
                %this.add("Go Idle", %n = %n + 1, 0);
            }
            if ($UserPref::Player::TeleportBlock)
            {
                %this.add("Allow Teleports", %n = %n + 1, 0);
            }
            else
            {
                %this.add("Refuse Teleports", %n = %n + 1, 0);
            }
            if ($UserPref::Player::WhisperBlock)
            {
                %this.add("Allow Whispers", %n = %n + 1, 0);
            }
            else
            {
                %this.add("Refuse Whispers", %n = %n + 1, 0);
            }
            if ($UserPref::Player::YellBlock)
            {
                %this.add("Allow Yells", %n = %n + 1, 0);
            }
            else
            {
                %this.add("Refuse Yells", %n = %n + 1, 0);
            }
            %this.add("Respawn Me!", %n = %n + 1, 0);
            if (%playerClicked.isCohost())
            {
                %this.add("Stop being Co-Host", %n = %n + 1, 0);
            }
            %text = "";
            if (%playerClicked.isHost())
            {
                %sku = getSpecialSKU(%playerClicked, "hostBadge");
                if (hasWord(%playerClicked.getActiveSKUs(), %sku))
                {
                    %text = "Hide Host Badge";
                }
                else
                {
                    %text = "Wear Host Badge";
                }
            }
            else
            {
                if (%playerClicked.isCohost())
                {
                    %sku = getSpecialSKU(%playerClicked, "cohostBadge");
                    if (hasWord(%playerClicked.getActiveSKUs(), %sku))
                    {
                        %text = "Hide Cohost Badge";
                    }
                    else
                    {
                        %text = "Wear Cohost Badge";
                    }
                }
            }
            if (!(%text $= ""))
            {
                %this.add(%text, %n = %n + 1, 0);
            }
            if (SkuManager.hasSkuWithAnyTags($player.getActiveSKUs(), "drinkMaker"))
            {
                %this.add("Make Drink", %n = %n + 1, %schemeNormal);
            }
            if (%playerClicked.hasMicrophone())
            {
                %this.add("Drop Microphone", %n = %n + 1, 0);
            }
        }
        if (gameMgrClient.areWeInspecting())
        {
            if (%playerClicked != $player)
            {
                %this.add("Invite to game");
            }
            if (gameMgrClient.inCustomGame() && gameMgrClient.areWeHostOfInspectedGame())
            {
                %this.add("Change score");
                %this.add("Change status");
            }
        }
        %this.addIfPermitted("snoop", "        --- mod/staff ---", %n = %n + 1, %schemeDisabled);
        %this.addIfPermitted("manageUsersBasic", "Ban...", %n = %n + 1, %schemeNormal);
        %this.addIfPermitted("manageUsersBasic", "Manage on Web", %n = %n + 1, %schemeNormal);
        %this.addIfPermitted("track", "Fly To", %n = %n + 1, %schemeFlyTo);
        %this.addIfPermitted("track", "Track", %n = %n + 1, %schemeTrack);
        %this.addIfPermitted("manageUsersBasic", "Peek at GameState", %n = %n + 1, %schemeNormal);
        if (isObject(%playerClicked))
        {
            if (%playerClicked.hasRoleString("snooped"))
            {
                %this.addIfPermitted("snoop", "unSnoop", %n = %n + 1, %schemeNormal);
            }
            else
            {
                %this.addIfPermitted("snoop", "Snoop", %n = %n + 1, %schemeNormal);
            }
        }
        else
        {
            %this.addIfPermitted("snoop", "Snoop", %n = %n + 1, %schemeNormal);
            %this.addIfPermitted("snoop", "unSnoop", %n = %n + 1, %schemeNormal);
        }
        %this.addIfPermitted("track", "Teleport To", %n = %n + 1, %schemeTeleport);
        %this.addIfPermitted("summon", "Respawn", %n = %n + 1, %schemeNormal);
        %this.addIfPermitted("summon", "Summon", %n = %n + 1, %schemeNormal);
        if (((CustomSpaceClient::isOwner() || $player.isHostOrCohost()) || $player.rolesPermissionCheckNoWarn("microphones")) && isObject(%playerClicked))
        {
            if (%playerClicked.hasMicrophone())
            {
                %this.add("Revoke Microphone", %n = %n + 1, %schemeNormal);
            }
            else
            {
                %this.add("Give Microphone", %n = %n + 1, %schemeNormal);
            }
        }
        if ($player.isDebugging())
        {
            %this.add("        --- debug --- (" @ %playerClicked @ ")", %n = %n + 1, %schemeDisabled);
            if (%isNPC && $StandAlone)
            {
                %this.add("Set Height: really tall", %n = %n + 1, %schemeNormal);
                %this.add("Set Height: tall", %n = %n + 1, %schemeNormal);
                %this.add("Set Height: medium", %n = %n + 1, %schemeNormal);
                %this.add("Set Height: short", %n = %n + 1, %schemeNormal);
                %this.add("Set Height: really short", %n = %n + 1, %schemeNormal);
            }
            %this.add("Relative Transform", %n = %n + 1, %schemeSaySumpn);
            %this.add("Body Mod", %n = %n + 1, %schemeSaySumpn);
            %this.add("Puppetry: Copy Skus", %n = %n + 1, %schemeSaySumpn);
            %this.add("Puppetry: Paste Skus", %n = %n + 1, %schemeSaySumpn);
            if (isObject(pChat))
            {
                %this.add("Puppetry: Speak", %n = %n + 1, %schemeSaySumpn);
            }
            if (isObject(pChat))
            {
                %this.add("Puppetry: Whisper", %n = %n + 1, %schemeWhisper);
            }
            if (isObject(pChat))
            {
                %this.add("Puppetry: Yell", %n = %n + 1, %schemeSaySumpn);
            }
            if (isObject(pChat))
            {
                %this.add("Puppetry: SOS", %n = %n + 1, %schemeSaySumpn);
            }
            if (%playerClicked.getClassName() $= "AIPlayer")
            {
                %this.add("Puppetry: Dance", %n = %n + 1, %schemeSaySumpn);
                %this.add("Puppetry: Emote", %n = %n + 1, %schemeSaySumpn);
                %this.add("Puppetry: Be Still", %n = %n + 1, %schemeSaySumpn);
                %this.add("Puppetry: Puppy", %n = %n + 1, %schemeSaySumpn);
            }
            if (isObject(pChat))
            {
                %this.add("Puppetry: Badge", %n = %n + 1, %schemeSaySumpn);
            }
            %this.add("Puppetry: Dance With", %n = %n + 1, %schemeSaySumpn);
            %this.add("Puppetry: Kiss", %n = %n + 1, %schemeSaySumpn);
        }
    }
    %title = %playerName;
    if (%playerClicked == $player)
    {
        %title = %title SPC "(this is you)";
    }
    if (%isNPC)
    {
        %title = %title SPC "(a bot)";
    }
    %this.setText(%title);
    gSetField(%this, "playerName", %playerName);
    gSetField(%this, "player", %playerClicked);
    gSetField(%this, "aimName", "");
    return ;
}
function PlayerContextMenu::initForAIM(%this, %aimName)
{
    %this.clear();
    %n = -1;
    %this.add("Message", %n = %n + 1, 0);
    if (showInviteFriend())
    {
        %this.add("Send Invite", %n = %n + 1, 0);
    }
    %this.setText(%aimName);
    gSetField(%this, "playerName", "");
    gSetField(%this, "player", "");
    gSetField(%this, "aimName", %aimName);
    return ;
}
function PlayerContextMenu::addIfPermitted(%this, %permName, %text, %n, %scheme)
{
    if ($player.rolesPermissionCheckNoWarn(%permName))
    {
        if (%permName $= "answerHelpMe")
        {
            %skuGuide = getSpecialSKU($player, "guidebadge");
            %skuSGuide = getSpecialSKU($player, "seniorguidebadge");
            if (!$player.hasActiveSKU(%skuGuide) && !$player.hasActiveSKU(%skuSGuide))
            {
                return ;
            }
        }
        %this.add(%text, %n, %scheme);
    }
    return ;
}
function PlayerContextMenu::initWithPlayerName(%this, %playerName)
{
    if (%playerName $= $player.getShapeName())
    {
        %playerObj = $player;
    }
    else
    {
        %playerObj = 0;
    }
    %this.init(%playerName, BuddyHudWin.getFriendStatus(%playerName), BuddyHudWin.getIgnoreStatus(%playerName), %playerObj);
    return ;
}
function PlayerContextMenu::initWithPlayer(%this, %player)
{
    %playerName = %player.getShapeName();
    %this.init(%playerName, BuddyHudWin.getFriendStatus(%playerName), BuddyHudWin.getIgnoreStatus(%playerName), %player);
    return ;
}
function PlayerContextMenu::onSelect(%this, %unused, %text)
{
    %player = gGetField(%this, "player");
    %playerName = gGetField(%this, "playerName");
    %aimName = gGetField(%this, "aimName");
    %handled = 0;
    if (!%handled)
    {
        %handled = 1;
        if (%text $= "View Profile")
        {
            doUserProfile(%playerName);
        }
        else
        {
            if (%text $= "Remove from Friends")
            {
                doUserFavorite(%playerName, "remove");
            }
            else
            {
                if (%text $= "Cancel Friend Request")
                {
                    doUserFavorite(%playerName, "cancel");
                }
                else
                {
                    if (%text $= "Accept Friend Request")
                    {
                        doUserFavorite(%playerName, "accept");
                    }
                    else
                    {
                        if (%text $= "Decline Friend Request")
                        {
                            doUserFavorite(%playerName, "decline");
                        }
                        else
                        {
                            if (%text $= "Add to Friends")
                            {
                                doUserFavorite(%playerName, "add");
                            }
                            else
                            {
                                if (%text $= "Whisper")
                                {
                                    openUserWhisper(%playerName);
                                }
                                else
                                {
                                    if (%text $= "Two-Player Action..")
                                    {
                                        TwoPlayerEmotesPanel.open(%playerName);
                                    }
                                    else
                                    {
                                        if (%text $= "Give vCurrency")
                                        {
                                            geGiftingPanel.open(%playerName, "initiate");
                                        }
                                        else
                                        {
                                            if (%text $= "Give Drink")
                                            {
                                                drinks_confirmInitiateGift(%playerName);
                                            }
                                            else
                                            {
                                                if (%text $= "Give Gift")
                                                {
                                                    gotoWebPage("http://www.vside.com/app/gifting/user/" @ urlEncode(%playerName) @ "/", 0);
                                                }
                                                else
                                                {
                                                    if (%text $= "Make Drink")
                                                    {
                                                        $gSalonChairCurrent = "";
                                                        ShowSalonMenu("drinks", "n", %playerName);
                                                    }
                                                    else
                                                    {
                                                        if (%text $= "Request Music")
                                                        {
                                                            gotoWebPage($Net::MusicURL);
                                                        }
                                                        else
                                                        {
                                                            if (%text $= "Invite to game")
                                                            {
                                                                gameMgrClient.invitePlayerToInspectedGame(%playerName);
                                                            }
                                                            else
                                                            {
                                                                if (%text $= "Change score")
                                                                {
                                                                    gameMgrClient.doHostPopupChangeScore(%playerName);
                                                                }
                                                                else
                                                                {
                                                                    if (%text $= "Change status")
                                                                    {
                                                                        gameMgrClient.doHostPopupChangeStatus(%playerName);
                                                                    }
                                                                    else
                                                                    {
                                                                        %handled = 0;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    if (!%handled)
    {
        %handled = 1;
        if (%text $= "This Space: Make Co-Host")
        {
            CustomSpaceClient::setCoHostHood(%playerName, 1);
        }
        else
        {
            if (%text $= "This Space: Unmake Co-Host")
            {
                CustomSpaceClient::setCoHostHood(%playerName, 0);
            }
            else
            {
                if (%text $= "Stop being Co-Host")
                {
                    CustomSpaceClient::setCoHostHood(%playerName, 0);
                }
                else
                {
                    if (%text $= "Hide Host Badge")
                    {
                        delayedRemoveSku(getSpecialSKU($player, "hostBadge"));
                    }
                    else
                    {
                        if (%text $= "Wear Host Badge")
                        {
                            delayedWearSku(getSpecialSKU($player, "hostBadge"));
                        }
                        else
                        {
                            if (%text $= "Hide Cohost Badge")
                            {
                                delayedRemoveSku(getSpecialSKU($player, "cohostBadge"));
                            }
                            else
                            {
                                if (%text $= "Wear Cohost Badge")
                                {
                                    delayedWearSku(getSpecialSKU($player, "cohostBadge"));
                                }
                                else
                                {
                                    if (%text $= "This Space: Kick")
                                    {
                                        CustomSpaceClient::doOwnerAction("kick", %playerName);
                                    }
                                    else
                                    {
                                        if (%text $= "This Space: Block")
                                        {
                                            CustomSpaceClient::TryBlockUserFromSpace(%playerName, 0);
                                        }
                                        else
                                        {
                                            if (%text $= "This Space: Unblock")
                                            {
                                                CustomSpaceClient::TryBlockUserFromSpace(%playerName, 1);
                                            }
                                            else
                                            {
                                                if (%text $= "This Space: Summon")
                                                {
                                                    CustomSpaceClient::doOwnerAction("summon", %playerName);
                                                }
                                                else
                                                {
                                                    if (%text $= "This Space: Respawn")
                                                    {
                                                        CustomSpaceClient::doOwnerAction("respawn", %playerName);
                                                    }
                                                    else
                                                    {
                                                        if (%text $= "This Space: Teleport To")
                                                        {
                                                            CustomSpaceClient::doOwnerAction("teleport", %playerName);
                                                        }
                                                        else
                                                        {
                                                            if (%text $= "This Space: Customize")
                                                            {
                                                                rentabotClient_customizeBot(%player);
                                                            }
                                                            else
                                                            {
                                                                if (%text $= "Ignore")
                                                                {
                                                                    doUserIgnore(%playerName, "add");
                                                                }
                                                                else
                                                                {
                                                                    if (%text $= "Unignore")
                                                                    {
                                                                        doUserIgnore(%playerName, "remove");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (%text $= "Act Like Me")
                                                                        {
                                                                            doUserMimic(%player);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (%text $= "Report Abuse")
                                                                            {
                                                                                doUserReport(%playerName, "abuse");
                                                                            }
                                                                            else
                                                                            {
                                                                                if (%text $= "My Profile & Account (web)")
                                                                                {
                                                                                    doEditProfile();
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (%text $= "Edit Away Message")
                                                                                    {
                                                                                        doEditAwayMessage();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (%text $= "Go Idle")
                                                                                        {
                                                                                            setIdle(1);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (%text $= "Back From Idle")
                                                                                            {
                                                                                                setIdle(0);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (%text $= "Allow Teleports")
                                                                                                {
                                                                                                    TeleportBlockCheckBox.setValue(0);
                                                                                                    doTeleportBlock();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (%text $= "Refuse Teleports")
                                                                                                    {
                                                                                                        TeleportBlockCheckBox.setValue(1);
                                                                                                        doTeleportBlock();
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (%text $= "Allow Whispers")
                                                                                                        {
                                                                                                            WhisperBlockCheckBox.setValue(0);
                                                                                                            doWhisperBlock(1);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (%text $= "Refuse Whispers")
                                                                                                            {
                                                                                                                WhisperBlockCheckBox.setValue(1);
                                                                                                                doWhisperBlock(1);
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (%text $= "Allow Yells")
                                                                                                                {
                                                                                                                    YellBlockCheckBox.setValue(0);
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (%text $= "Refuse Yells")
                                                                                                                    {
                                                                                                                        YellBlockCheckBox.setValue(1);
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (%text $= "Respawn Me!")
                                                                                                                        {
                                                                                                                            doRespawnMe();
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (%text $= "Drop Microphone")
                                                                                                                            {
                                                                                                                                doDropMic();
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (%text $= "** Look At My Clothes **")
                                                                                                                                {
                                                                                                                                    doLookAtMyClothes(%player);
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (%text $= "Applaud For Me!")
                                                                                                                                    {
                                                                                                                                        doCheerFor(%playerName);
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        %handled = 0;
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    if (!%handled)
    {
        %handled = 1;
        if (%text $= "Set Height: really tall")
        {
            if ($StandAlone)
            {
                PlayerDict.get(%playerName).setHeight(1.15);
            }
        }
        else
        {
            if (%text $= "Set Height: tall")
            {
                if ($StandAlone)
                {
                    PlayerDict.get(%playerName).setHeight(1.075);
                }
            }
            else
            {
                if (%text $= "Set Height: medium")
                {
                    if ($StandAlone)
                    {
                        PlayerDict.get(%playerName).setHeight(1);
                    }
                }
                else
                {
                    if (%text $= "Set Height: short")
                    {
                        if ($StandAlone)
                        {
                            PlayerDict.get(%playerName).setHeight(0.93);
                        }
                    }
                    else
                    {
                        if (%text $= "Set Height: really short")
                        {
                            if ($StandAlone)
                            {
                                PlayerDict.get(%playerName).setHeight(0.86);
                            }
                        }
                        else
                        {
                            if (%text $= "Puppetry: Speak")
                            {
                                doUserSaySomething(%playerName);
                            }
                            else
                            {
                                if (%text $= "Puppetry: Whisper")
                                {
                                    doUserWhisperSomething(%playerName);
                                }
                                else
                                {
                                    if (%text $= "Puppetry: Yell")
                                    {
                                        doUserYellSomething(%playerName);
                                    }
                                    else
                                    {
                                        if (%text $= "Puppetry: SOS")
                                        {
                                            doUserSosSomething(%playerName);
                                        }
                                        else
                                        {
                                            if (%text $= "Puppetry: Dance")
                                            {
                                                doUserAutoEmote(%playerName, "dance");
                                            }
                                            else
                                            {
                                                if (%text $= "Puppetry: Emote")
                                                {
                                                    doUserAutoEmote(%playerName, "emote");
                                                }
                                                else
                                                {
                                                    if (%text $= "Puppetry: Blend Spaz")
                                                    {
                                                        doUserAutoEmoteRate(%playerName, "blend", 300, 200);
                                                    }
                                                    else
                                                    {
                                                        if (%text $= "Puppetry: Be Still")
                                                        {
                                                            doUserAutoEmote(%playerName, "");
                                                        }
                                                        else
                                                        {
                                                            if (%text $= "Puppetry: Puppy")
                                                            {
                                                                doUserPuppy(%playerName);
                                                            }
                                                            else
                                                            {
                                                                if (%text $= "Puppetry: Badge")
                                                                {
                                                                    doUserBadge(%playerName);
                                                                }
                                                                else
                                                                {
                                                                    if (%text $= "Puppetry: Dance With")
                                                                    {
                                                                        doDanceWith(%player);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (%text $= "Puppetry: Kiss")
                                                                        {
                                                                            doKiss(%player);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (%text $= "Puppetry: Copy Skus")
                                                                            {
                                                                                doUserCopySkus(%playerName);
                                                                            }
                                                                            else
                                                                            {
                                                                                if (%text $= "Puppetry: Paste Skus")
                                                                                {
                                                                                    doUserPasteSkus(%playerName);
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (%text $= "Body Mod")
                                                                                    {
                                                                                        doUserBodyMod(%player);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (%text $= "Relative Transform")
                                                                                        {
                                                                                            doUserRelativeTransform(%player);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (%text $= "Teleport To")
                                                                                            {
                                                                                                teleportOperation(%playerName);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (%text $= "Fly To")
                                                                                                {
                                                                                                    doUserFlyTo(%playerName);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (%text $= "Peek at GameState")
                                                                                                    {
                                                                                                        doUserPeekAtGameState(%playerName);
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (%text $= "Track")
                                                                                                        {
                                                                                                            doUserTrack(%playerName);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (%text $= "Snoop")
                                                                                                            {
                                                                                                                doUserSnoop(%playerName, 1);
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (%text $= "unSnoop")
                                                                                                                {
                                                                                                                    doUserSnoop(%playerName, 0);
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (%text $= "Respawn")
                                                                                                                    {
                                                                                                                        doUserRespawn(%playerName);
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (%text $= "Summon")
                                                                                                                        {
                                                                                                                            doUserSummon(%playerName);
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (%text $= "Ban...")
                                                                                                                            {
                                                                                                                                doUserBan(%playerName);
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (%text $= "Manage on Web")
                                                                                                                                {
                                                                                                                                    doUserManage(%playerName);
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (%text $= "Take Flower")
                                                                                                                                    {
                                                                                                                                        doTakeFlower(%playerName);
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (%text $= "Give Flower")
                                                                                                                                        {
                                                                                                                                            doGiveFlower(%playerName);
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (%text $= "Turn off Help Request")
                                                                                                                                            {
                                                                                                                                                doTurnOffHelpme(%playerName);
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (%text $= "Revoke Microphone")
                                                                                                                                                {
                                                                                                                                                    doMicrophoneGiveOrRevoke(%playerName, 0);
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if (%text $= "Give Microphone")
                                                                                                                                                    {
                                                                                                                                                        doMicrophoneGiveOrRevoke(%playerName, 1);
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (%text $= "Message")
                                                                                                                                                        {
                                                                                                                                                            AIMConvManager.talkTo(%aimName);
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (%text $= "Send Invite")
                                                                                                                                                            {
                                                                                                                                                                AimInviteDialog.open(%aimName);
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                %handled = 0;
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function PlayerContextMenu::showComingSoon(%this, %featureName)
{
    MessageBoxOK(%featureName SPC "- Coming Soon!", "The" SPC %featureName SPC "feature will be here soon!", "");
    return ;
}
function PlayGui::onRMBPlayer(%this, %obj)
{
    PlayerContextMenu.initWithPlayer(%obj);
    PlayerContextMenu.showAtPoint(Canvas.getCursorPos());
    return ;
}
function BuddyHudRequestsList::onRightMouseUp(%this)
{
    %text = stripUnprintables(%this.getRowText(%this.getMouseOverRow()));
    if (!(%text $= ""))
    {
        PlayerContextMenu.initWithPlayerName(%text);
        PlayerContextMenu.showAtPoint(Canvas.getCursorPos());
    }
    else
    {
        warn("Got empty player name from right click on FavoritesList");
    }
    return ;
}
function doUserMimic(%player)
{
    commandToServer('EtsPlayerClickedSharedAnim', %player.getGhostID());
    return ;
}
function doGiftInventoryItemToPlayer(%playerName, %sku)
{
    commandToServer('GiftInventoryItemToPlayer', %playerName, %sku);
    return ;
}
function doRespawnMe()
{
    commandToServer('respawnMe');
    return ;
}
function doDropMic()
{
    commandToServer('dropMic');
    return ;
}
function doDanceWith(%obj)
{
    doLookAt(%obj, 1, 0);
    return ;
}
function doKiss(%obj)
{
    doLookAt(%obj, 0, 1, 0);
    return ;
}
$InspectSkusMap = 0;
function getWearingItemWithMoreInfo(%player)
{
    if (!isObject($InspectSkusMap))
    {
        $InspectSkusMap = new StringMap();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add($InspectSkusMap);
        }
        $InspectSkusMap.put(22228, "neptune");
        $InspectSkusMap.put(22229, "neptune");
        $InspectSkusMap.put(32261, "rtv");
        $InspectSkusMap.put(32262, "rtv");
        $InspectSkusMap.put(22212, "masquerade");
        $InspectSkusMap.put(22213, "masquerade");
        $InspectSkusMap.put(32259, "viper");
        $InspectSkusMap.put(32260, "viper");
        $InspectSkusMap.put(22210, "wireless");
        $InspectSkusMap.put(22211, "wireless");
        $InspectSkusMap.put(32257, "visionvamp");
        $InspectSkusMap.put(32258, "visionvamp");
        $InspectSkusMap.put(22230, "crown");
        $InspectSkusMap.put(22231, "crown");
        $InspectSkusMap.put(32274, "cosmicyoga");
        $InspectSkusMap.put(32275, "cosmicyoga");
        $InspectSkusMap.put(22214, "royalflush");
        $InspectSkusMap.put(22215, "royalflush");
        $InspectSkusMap.put(32255, "warrior");
        $InspectSkusMap.put(32256, "warrior");
    }
    %skusList = %player.getActiveSKUs();
    %num = getWordCount(%skusList);
    %inspectTextDir = "projects/common/inventoryInspect/";
    %i = 0;
    while (%i < %num)
    {
        %skunum = getWord(%skusList, %i);
        %mapped = $InspectSkusMap.get(%skunum);
        if (!(%mapped $= ""))
        {
            %skunum = %mapped;
        }
        %inspectFile = %inspectTextDir @ %skunum @ ".txt";
        %fo = new FileObject();
        if (%fo.openForRead(%inspectFile))
        {
            %fo.delete();
            return %inspectFile;
        }
        else
        {
            %fo.delete();
        }
        %i = %i + 1;
    }
    return "";
}
function doLookAtMyClothes(%player)
{
    echo("TODO:  make this pick the proper thing");
    %file = getWearingItemWithMoreInfo(%player);
    if (!(%file $= ""))
    {
        echo("looking closer at player\'s clothes");
        MLScrollInspectPanel.OnInspect(%file);
    }
    return ;
}
function doCheerFor(%playerName)
{
    ApplauseMeterGui.open("applause", %playerName);
    return ;
}
function doTurnOffHelpme(%playerName)
{
    commandToServer('TurnOffHelpMeMode', %playerName);
    return ;
}
