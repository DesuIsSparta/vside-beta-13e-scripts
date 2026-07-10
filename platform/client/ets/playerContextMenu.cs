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
    if (isObject(%playerClicked))
    {
    }
    else
    {
    }
    %isRealPlayer = 0;
    !%playerClicked.isClassAIPlayer();
    if (isObject(%playerClicked))
    {
    }
    else
    {
    }
    %isIdle = 0;
    %playerClicked.getAFK();
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
    %n = -(1);
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
            if (isObject(%playerClicked) && !(getWearingItemWithMoreInfo(%playerClicked) $= ""))
            {
                %this.add("** Look At My Clothes **", %n = %n + 1, %schemeProfile);
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
                if (%friendStatus $= "fan")
                {
                    %this.add("Accept Friend Request", %n = %n + 1, %schemeFavorite);
                    %this.add("Decline Friend Request", %n = %n + 1, %schemeFavorite);
                }
                if (%friendStatus $= "none")
                {
                    %this.add("Add to Friends", %n = %n + 1, %schemeFavorite);
                }
            }
            if (%onlineHere)
            {
                %this.add("Whisper", %n = %n + 1, %schemePM);
            }
            if (!rentabot_isRentabotName(%playerName))
            {
                if (%sameServer)
                {
                }
                if (!%isNPC || $StandAlone)
                {
                }
                if (%onlineHere)
                {
                }
                if (!%isIgnore)
                {
                }
                if (geGiftingPanel.isInRange(%playerClicked))
                {
                }
                if (!%isIdle)
                {
                    %this.add("Two-Player Action..", %n = %n + 1, %schemeNormal);
                }
                if (!geGiftingPanel.isInRange(%playerClicked))
                {
                    %this.add("Two-Player Action.. - Too Far!", %n = %n + 1, %schemeDisabled);
                }
                if (%isIdle)
                {
                    %this.add("Two-Player Action.. - Idle!", %n = %n + 1, %schemeDisabled);
                }
            }
            if (!rentabot_isRentabotName(%playerName))
            {
                if (%sameServer)
                {
                }
                if (%onlineHere)
                {
                }
                if (!%isIgnore)
                {
                }
                if (geGiftingPanel.isInRange(%playerClicked))
                {
                }
                if (!%isIdle)
                {
                    %this.add("Give vCurrency", %n = %n + 1, %schemeNormal);
                }
                if (!geGiftingPanel.isInRange(%playerClicked))
                {
                    %this.add("Give vCurrency - Too Far!", %n = %n + 1, %schemeDisabled);
                }
                if (%isIdle)
                {
                    %this.add("Give vCurrency - Idle!", %n = %n + 1, %schemeDisabled);
                }
            }
            if (SkuManager.hasSkuWithAnyTags($player.getActiveSKUs(), "drink"))
            {
            }
            if (!rentabot_isRentabotName(%playerName))
            {
                if (%sameServer)
                {
                }
                if (%onlineHere)
                {
                }
                if (!%isIgnore)
                {
                }
                if (geGiftingPanel.isInRange(%playerClicked))
                {
                }
                if (!%isIdle)
                {
                    %this.add("Give Drink", %n = %n + 1, %schemeNormal);
                }
                if (!geGiftingPanel.isInRange(%playerClicked))
                {
                    %this.add("Give Drink - Too Far!", %n = %n + 1, %schemeDisabled);
                }
                if (%isIdle)
                {
                    %this.add("Give Drink - Idle!", %n = %n + 1, %schemeDisabled);
                }
            }
            %this.add("Give Gift", %n = %n + 1, %schemeNormal);
            if (SkuManager.hasSkuWithAnyTags($player.getActiveSKUs(), "drinkMaker"))
            {
            }
            if (!rentabot_isRentabotName(%playerName))
            {
                if (%sameServer)
                {
                }
                if (%onlineHere)
                {
                }
                if (!%isIgnore)
                {
                }
                if (geGiftingPanel.isInRange(%playerClicked))
                {
                }
                if (!%isIdle)
                {
                    %this.add("Make Drink", %n = %n + 1, %schemeNormal);
                }
                if (!geGiftingPanel.isInRange(%playerClicked))
                {
                    %this.add("Make Drink - Too Far!", %n = %n + 1, %schemeDisabled);
                }
                if (%isIdle)
                {
                    %this.add("Make Drink - Idle!", %n = %n + 1, %schemeDisabled);
                }
            }
            if (isObject(%playerClicked))
            {
            }
            if (%playerClicked.getCanHandleMusicRequest())
            {
                %this.add("Request Music", %n = %n + 1, %schemeNormal);
            }
            if (isObject(%playerClicked))
            {
                %this.add("Applaud For Me!");
            }
            if ($player.isHostOrCohost())
            {
                if ($player.isHost() && %isRealPlayer)
                {
                    if (%playerClicked.isCohost())
                    {
                        %this.add("This Space: Unmake Co-Host", %n = %n + 1, %schemeNormal);
                    }
                    %this.add("This Space: Make Co-Host", %n = %n + 1, %schemeNormal);
                }
                if (%isRealPlayer)
                {
                }
                if (!%playerClicked.isHost())
                {
                    %this.add("This Space: Kick", %n = %n + 1, %schemeNormal);
                }
                if (!%isRentabot)
                {
                }
                if ($player.isHost())
                {
                    if (findField($CSBlockedList, %playerName) == -(1))
                    {
                        %this.add("This Space: Block", %n = %n + 1, %schemeNormal);
                    }
                    %this.add("This Space: Unblock", %n = %n + 1, %schemeNormal);
                }
                if (isObject(%playerClicked))
                {
                    if (CustomSpaceClient::isOwner())
                    {
                    }
                    if (%isRentabot)
                    {
                        %this.add("This Space: Customize", %n = %n + 1, %schemeNormal);
                    }
                    if ($player.isHostOrCohost())
                    {
                    }
                    if (!%playerClicked.rolesPermissionCheckNoWarn("customspaceImmune") && !%playerClicked.isHost())
                    {
                    }
                    if ($player.isHost() || !%playerClicked.isClassAIPlayer())
                    {
                        %this.add("This Space: Summon", %n = %n + 1, %schemeNormal);
                        %this.add("This Space: Respawn", %n = %n + 1, %schemeNormal);
                    }
                }
            }
            if (%onlineHere)
            {
            }
            if (%ignorable)
            {
                if (%isIgnore)
                {
                    %this.add("Unignore", %n = %n + 1, %schemeIgnore);
                }
                %this.add("Ignore", %n = %n + 1, %schemeIgnore);
            }
            if (CustomSpaceClient::isOwner())
            {
            }
            if (isObject(%playerClicked))
            {
            }
            if ((%friendStatus $= "friends") || %isNPC || (%playerClicked != $player))
            {
                %this.add("Teleport To", %n = %n + 1, %schemeTeleport);
            }
            if (%onlineHere)
            {
            }
            if (!%isRentabot)
            {
                %this.add("Report Abuse", %n = %n + 1, %schemeProfile);
            }
            if ($flowerGiftingEnabled)
            {
                if (isObject(%playerClicked))
                {
                }
                if (%isNPC)
                {
                    %skuSelf = getSpecialSKU($player, "flower");
                    %skuThem = getSpecialSKU(%playerClicked, "flower");
                    if (%playerClicked.hasActiveSKU(%skuThem))
                    {
                    }
                    if (!$player.hasInventorySKU(%skuSelf))
                    {
                        %this.add("Take Flower", %n = %n + 1, %schemeGifting);
                    }
                }
                if (isObject(%playerClicked))
                {
                }
                if (!%isNPC)
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
            if (isObject(%playerClicked) && !(getWearingItemWithMoreInfo(%playerClicked) $= ""))
            {
                %this.add("** Look At My Clothes **", %n = %n + 1, 0);
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
                    %text = "Wear Cohost Badge";
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
            if (gameMgrClient.inCustomGame())
            {
            }
            if (gameMgrClient.areWeHostOfInspectedGame())
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
        if (CustomSpaceClient::isOwner() || $player.isHostOrCohost() || $player.rolesPermissionCheckNoWarn("microphones"))
        {
        }
        if (isObject(%playerClicked))
        {
            if (%playerClicked.hasMicrophone())
            {
                %this.add("Revoke Microphone", %n = %n + 1, %schemeNormal);
            }
            %this.add("Give Microphone", %n = %n + 1, %schemeNormal);
        }
        if ($player.isDebugging())
        {
            %this.add("        --- debug --- (" @ %playerClicked @ ")", %n = %n + 1, %schemeDisabled);
            if (%isNPC)
            {
            }
            if ($StandAlone)
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
        %title = %title @ " " @ "(this is you)";
    }
    if (%isNPC)
    {
        %title = %title @ " " @ "(a bot)";
    }
    %this.setText(%title);
    gSetField(%this, "playerName", %playerName);
    gSetField(%this, "player", %playerClicked);
    gSetField(%this, "aimName", "");
}
function PlayerContextMenu::initForAIM(%this, %aimName)
{
    %this.clear();
    %n = -(1);
    %this.add("Message", %n = %n + 1, 0);
    if (showInviteFriend())
    {
        %this.add("Send Invite", %n = %n + 1, 0);
    }
    %this.setText(%aimName);
    gSetField(%this, "playerName", "");
    gSetField(%this, "player", "");
    gSetField(%this, "aimName", %aimName);
}
function PlayerContextMenu::addIfPermitted(%this, %permName, %text, %n, %scheme)
{
    if ($player.rolesPermissionCheckNoWarn(%permName))
    {
        if (%permName $= "answerHelpMe")
        {
            %skuGuide = getSpecialSKU($player, "guidebadge");
            %skuSGuide = getSpecialSKU($player, "seniorguidebadge");
            if (!$player.hasActiveSKU(%skuGuide))
            {
            }
            if (!$player.hasActiveSKU(%skuSGuide))
            {
                return;
            }
        }
        %this.add(%text, %n, %scheme);
    }
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
}
function PlayerContextMenu::initWithPlayer(%this, %player)
{
    %playerName = %player.getShapeName();
    %this.init(%playerName, BuddyHudWin.getFriendStatus(%playerName), BuddyHudWin.getIgnoreStatus(%playerName), %player);
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
        if (%text $= "Remove from Friends")
        {
            doUserFavorite(%playerName, "remove");
        }
        if (%text $= "Cancel Friend Request")
        {
            doUserFavorite(%playerName, "cancel");
        }
        if (%text $= "Accept Friend Request")
        {
            doUserFavorite(%playerName, "accept");
        }
        if (%text $= "Decline Friend Request")
        {
            doUserFavorite(%playerName, "decline");
        }
        if (%text $= "Add to Friends")
        {
            doUserFavorite(%playerName, "add");
        }
        if (%text $= "Whisper")
        {
            openUserWhisper(%playerName);
        }
        if (%text $= "Two-Player Action..")
        {
            TwoPlayerEmotesPanel.open(%playerName);
        }
        if (%text $= "Give vCurrency")
        {
            geGiftingPanel.open(%playerName, "initiate");
        }
        if (%text $= "Give Drink")
        {
            drinks_confirmInitiateGift(%playerName);
        }
        if (%text $= "Give Gift")
        {
            gotoWebPage("http://www.vside.com/app/gifting/user/" @ urlEncode(%playerName) @ "/", 0);
        }
        if (%text $= "Make Drink")
        {
            $gSalonChairCurrent = "";
            ShowSalonMenu("drinks", "n", %playerName);
        }
        if (%text $= "Request Music")
        {
            gotoWebPage($Net::MusicURL);
        }
        if (%text $= "Invite to game")
        {
            gameMgrClient.invitePlayerToInspectedGame(%playerName);
        }
        if (%text $= "Change score")
        {
            gameMgrClient.doHostPopupChangeScore(%playerName);
        }
        if (%text $= "Change status")
        {
            gameMgrClient.doHostPopupChangeStatus(%playerName);
        }
        %handled = 0;
    }
    if (!%handled)
    {
        %handled = 1;
        if (%text $= "This Space: Make Co-Host")
        {
            CustomSpaceClient::setCoHostHood(%playerName, 1);
        }
        if (%text $= "This Space: Unmake Co-Host")
        {
            CustomSpaceClient::setCoHostHood(%playerName, 0);
        }
        if (%text $= "Stop being Co-Host")
        {
            CustomSpaceClient::setCoHostHood(%playerName, 0);
        }
        if (%text $= "Hide Host Badge")
        {
            delayedRemoveSku(getSpecialSKU($player, "hostBadge"));
        }
        if (%text $= "Wear Host Badge")
        {
            delayedWearSku(getSpecialSKU($player, "hostBadge"));
        }
        if (%text $= "Hide Cohost Badge")
        {
            delayedRemoveSku(getSpecialSKU($player, "cohostBadge"));
        }
        if (%text $= "Wear Cohost Badge")
        {
            delayedWearSku(getSpecialSKU($player, "cohostBadge"));
        }
        if (%text $= "This Space: Kick")
        {
            CustomSpaceClient::doOwnerAction("kick", %playerName);
        }
        if (%text $= "This Space: Block")
        {
            CustomSpaceClient::TryBlockUserFromSpace(%playerName, 0);
        }
        if (%text $= "This Space: Unblock")
        {
            CustomSpaceClient::TryBlockUserFromSpace(%playerName, 1);
        }
        if (%text $= "This Space: Summon")
        {
            CustomSpaceClient::doOwnerAction("summon", %playerName);
        }
        if (%text $= "This Space: Respawn")
        {
            CustomSpaceClient::doOwnerAction("respawn", %playerName);
        }
        if (%text $= "This Space: Teleport To")
        {
            CustomSpaceClient::doOwnerAction("teleport", %playerName);
        }
        if (%text $= "This Space: Customize")
        {
            rentabotClient_customizeBot(%player);
        }
        if (%text $= "Ignore")
        {
            doUserIgnore(%playerName, "add");
        }
        if (%text $= "Unignore")
        {
            doUserIgnore(%playerName, "remove");
        }
        if (%text $= "Act Like Me")
        {
            doUserMimic(%player);
        }
        if (%text $= "Report Abuse")
        {
            doUserReport(%playerName, "abuse");
        }
        if (%text $= "My Profile & Account (web)")
        {
            doEditProfile();
        }
        if (%text $= "Edit Away Message")
        {
            doEditAwayMessage();
        }
        if (%text $= "Go Idle")
        {
            setIdle(1);
        }
        if (%text $= "Back From Idle")
        {
            setIdle(0);
        }
        if (%text $= "Allow Teleports")
        {
            TeleportBlockCheckBox.setValue(0);
            doTeleportBlock();
        }
        if (%text $= "Refuse Teleports")
        {
            TeleportBlockCheckBox.setValue(1);
            doTeleportBlock();
        }
        if (%text $= "Allow Whispers")
        {
            WhisperBlockCheckBox.setValue(0);
            doWhisperBlock(1);
        }
        if (%text $= "Refuse Whispers")
        {
            WhisperBlockCheckBox.setValue(1);
            doWhisperBlock(1);
        }
        if (%text $= "Allow Yells")
        {
            YellBlockCheckBox.setValue(0);
        }
        if (%text $= "Refuse Yells")
        {
            YellBlockCheckBox.setValue(1);
        }
        if (%text $= "Respawn Me!")
        {
            doRespawnMe();
        }
        if (%text $= "Drop Microphone")
        {
            doDropMic();
        }
        if (%text $= "** Look At My Clothes **")
        {
            doLookAtMyClothes(%player);
        }
        if (%text $= "Applaud For Me!")
        {
            doCheerFor(%playerName);
        }
        %handled = 0;
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
        if (%text $= "Set Height: tall")
        {
            if ($StandAlone)
            {
                PlayerDict.get(%playerName).setHeight(1.075);
            }
        }
        if (%text $= "Set Height: medium")
        {
            if ($StandAlone)
            {
                PlayerDict.get(%playerName).setHeight(1);
            }
        }
        if (%text $= "Set Height: short")
        {
            if ($StandAlone)
            {
                PlayerDict.get(%playerName).setHeight(0.93);
            }
        }
        if (%text $= "Set Height: really short")
        {
            if ($StandAlone)
            {
                PlayerDict.get(%playerName).setHeight(0.86);
            }
        }
        if (%text $= "Puppetry: Speak")
        {
            doUserSaySomething(%playerName);
        }
        if (%text $= "Puppetry: Whisper")
        {
            doUserWhisperSomething(%playerName);
        }
        if (%text $= "Puppetry: Yell")
        {
            doUserYellSomething(%playerName);
        }
        if (%text $= "Puppetry: SOS")
        {
            doUserSosSomething(%playerName);
        }
        if (%text $= "Puppetry: Dance")
        {
            doUserAutoEmote(%playerName, "dance");
        }
        if (%text $= "Puppetry: Emote")
        {
            doUserAutoEmote(%playerName, "emote");
        }
        if (%text $= "Puppetry: Blend Spaz")
        {
            doUserAutoEmoteRate(%playerName, "blend", 300, 200);
        }
        if (%text $= "Puppetry: Be Still")
        {
            doUserAutoEmote(%playerName, "");
        }
        if (%text $= "Puppetry: Puppy")
        {
            doUserPuppy(%playerName);
        }
        if (%text $= "Puppetry: Badge")
        {
            doUserBadge(%playerName);
        }
        if (%text $= "Puppetry: Dance With")
        {
            doDanceWith(%player);
        }
        if (%text $= "Puppetry: Kiss")
        {
            doKiss(%player);
        }
        if (%text $= "Puppetry: Copy Skus")
        {
            doUserCopySkus(%playerName);
        }
        if (%text $= "Puppetry: Paste Skus")
        {
            doUserPasteSkus(%playerName);
        }
        if (%text $= "Body Mod")
        {
            doUserBodyMod(%player);
        }
        if (%text $= "Relative Transform")
        {
            doUserRelativeTransform(%player);
        }
        if (%text $= "Teleport To")
        {
            teleportOperation(%playerName);
        }
        if (%text $= "Fly To")
        {
            doUserFlyTo(%playerName);
        }
        if (%text $= "Peek at GameState")
        {
            doUserPeekAtGameState(%playerName);
        }
        if (%text $= "Track")
        {
            doUserTrack(%playerName);
        }
        if (%text $= "Snoop")
        {
            doUserSnoop(%playerName, 1);
        }
        if (%text $= "unSnoop")
        {
            doUserSnoop(%playerName, 0);
        }
        if (%text $= "Respawn")
        {
            doUserRespawn(%playerName);
        }
        if (%text $= "Summon")
        {
            doUserSummon(%playerName);
        }
        if (%text $= "Ban...")
        {
            doUserBan(%playerName);
        }
        if (%text $= "Manage on Web")
        {
            doUserManage(%playerName);
        }
        if (%text $= "Take Flower")
        {
            doTakeFlower(%playerName);
        }
        if (%text $= "Give Flower")
        {
            doGiveFlower(%playerName);
        }
        if (%text $= "Turn off Help Request")
        {
            doTurnOffHelpme(%playerName);
        }
        if (%text $= "Revoke Microphone")
        {
            doMicrophoneGiveOrRevoke(%playerName, 0);
        }
        if (%text $= "Give Microphone")
        {
            doMicrophoneGiveOrRevoke(%playerName, 1);
        }
        if (%text $= "Message")
        {
            AIMConvManager.talkTo(%aimName);
        }
        if (%text $= "Send Invite")
        {
            AimInviteDialog.open(%aimName);
        }
        %handled = 0;
    }
}
function PlayerContextMenu::showComingSoon(%this, %featureName)
{
    MessageBoxOK(%featureName @ " " @ "- Coming Soon!", "The" @ " " @ %featureName @ " " @ "feature will be here soon!", "");
}
function PlayGui::onRMBPlayer(%this, %obj)
{
    PlayerContextMenu.initWithPlayer(%obj);
    PlayerContextMenu.showAtPoint(Canvas.getCursorPos());
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
}
function doUserMimic(%player)
{
    commandToServer('EtsPlayerClickedSharedAnim', %player.getGhostID());
}
function doGiftInventoryItemToPlayer(%playerName, %sku)
{
    commandToServer('GiftInventoryItemToPlayer', %playerName, %sku);
}
function doRespawnMe()
{
    commandToServer('respawnMe');
}
function doDropMic()
{
    commandToServer('dropMic');
}
function doDanceWith(%obj)
{
    doLookAt(%obj, 1, 0);
}
function doKiss(%obj)
{
    doLookAt(%obj, 0, 1, 0);
}
$InspectSkusMap = 0;
function getWearingItemWithMoreInfo(%player)
{
    if (!isObject($InspectSkusMap))
    {
        $InspectSkusMap = new StringMap("");
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
        %fo = new FileObject("");
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
        echo("looking closer at player's clothes");
        MLScrollInspectPanel.OnInspect(%file);
    }
}
function doCheerFor(%playerName)
{
    ApplauseMeterGui.open("applause", %playerName);
}
function doTurnOffHelpme(%playerName)
{
    commandToServer('TurnOffHelpMeMode', %playerName);
}
