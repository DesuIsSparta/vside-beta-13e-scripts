$flowerGiftingEnabled = 0;
function PlayerContextMenu::init(%this, %playerName, %friendStatus, %isIgnore, %playerClicked) {
    if (!(isObject(%playerClicked))) {
        %playerClicked = Player::findPlayerInstance(%playerName);
    }
    %this.clear();
    %hasName = !(%playerName $= "");
    if (!(%hasName)) {
        %playerName = "no-name";
    }
    %isNPC = isNPCName(%playerName);
    %isRentabot = rentabot_isRentabotName(%playerName);
    if (isObject(%playerClicked)) {
    }
    %sameServer = (%playerName.get() SPC serverName $= $ServerName);
    UserListFriends;
    if (isObject(%playerClicked)) {
    }
    %isRealPlayer = 0;
    !(%playerClicked.isClassAIPlayer());
    if (isObject(%playerClicked)) {
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
    %schemePM = isObject() ? 0 : 1;
    pChat;
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
    %n = -(1.0);
    if (%hasName) {
        if (($player != %playerClicked)) {
            %onlineHere = %playerName.isOnlineHereOrNotFavorite();
            BuddyHudWin;
            %ignorable = 1;
            if (isObject(%playerClicked)) {
                %ignorable = !(%playerClicked.rolesPermissionCheckNoWarn("omnivocal"));
            }
            if (isObject(%playerClicked)) {
                if (!(getWearingItemWithMoreInfo(%playerClicked) $= "")) {
                    %n = (1.0 + %n);
                    %this.add("** Look At My Clothes **", , %schemeProfile);
                }
            }
            %n = (1.0 + %n);
            %this.add("View Profile", , %schemeProfile);
            if ((%friendStatus $= "friends")) {
                %n = (1.0 + %n);
                %this.add("Remove from Friends", , %schemeFavorite);
            }
            if ((%friendStatus $= "favorite")) {
                %n = (1.0 + %n);
                %this.add("Cancel Friend Request", , %schemeFavorite);
            }
            if ((%friendStatus $= "fan")) {
                %n = (1.0 + %n);
                %this.add("Accept Friend Request", , %schemeFavorite);
                %n = (1.0 + %n);
                %this.add("Decline Friend Request", , %schemeFavorite);
            }
            if ((%friendStatus $= "none")) {
                %n = (1.0 + %n);
                %this.add("Add to Friends", , %schemeFavorite);
            }
            if (%onlineHere) {
                %n = (1.0 + %n);
                %this.add("Whisper", , %schemePM);
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer) {
                    if (!(%isNPC)) {
                    }
                }
                if ($StandAlone) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange()) {
                }
                if (!(%isIdle)) {
                    %n = (1.0 + %n);
                    %this.add("Two-Player Action..", geGiftingPanel, %schemeNormal);
                }
                if (!(%playerClicked.isInRange())) {
                    %n = (1.0 + %n);
                    %this.add("Two-Player Action.. - Too Far!", geGiftingPanel, %schemeDisabled);
                }
                if (%isIdle) {
                    %n = (1.0 + %n);
                    %this.add("Two-Player Action.. - Idle!", , %schemeDisabled);
                }
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange()) {
                }
                if (!(%isIdle)) {
                    %n = (1.0 + %n);
                    %this.add("Give vCurrency", geGiftingPanel, %schemeNormal);
                }
                if (!(%playerClicked.isInRange())) {
                    %n = (1.0 + %n);
                    %this.add("Give vCurrency - Too Far!", geGiftingPanel, %schemeDisabled);
                }
                if (%isIdle) {
                    %n = (1.0 + %n);
                    %this.add("Give vCurrency - Idle!", , %schemeDisabled);
                }
            }
            if ($player.getActiveSKUs().hasSkuWithAnyTags("drink")) {
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange()) {
                }
                if (!(%isIdle)) {
                    %n = (1.0 + %n);
                    %this.add("Give Drink", geGiftingPanel, %schemeNormal);
                }
                if (!(%playerClicked.isInRange())) {
                    %n = (1.0 + %n);
                    %this.add("Give Drink - Too Far!", geGiftingPanel, %schemeDisabled);
                }
                if (%isIdle) {
                    %n = (1.0 + %n);
                    %this.add("Give Drink - Idle!", SkuManager, %schemeDisabled);
                }
            }
            %n = (1.0 + %n);
            %this.add("Give Gift", , %schemeNormal);
            if ($player.getActiveSKUs().hasSkuWithAnyTags("drinkMaker")) {
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange()) {
                }
                if (!(%isIdle)) {
                    %n = (1.0 + %n);
                    %this.add("Make Drink", geGiftingPanel, %schemeNormal);
                }
                if (!(%playerClicked.isInRange())) {
                    %n = (1.0 + %n);
                    %this.add("Make Drink - Too Far!", geGiftingPanel, %schemeDisabled);
                }
                if (%isIdle) {
                    %n = (1.0 + %n);
                    %this.add("Make Drink - Idle!", SkuManager, %schemeDisabled);
                }
            }
            if (isObject(%playerClicked)) {
            }
            if (%playerClicked.getCanHandleMusicRequest()) {
                %n = (1.0 + %n);
                %this.add("Request Music", , %schemeNormal);
            }
            if (isObject(%playerClicked)) {
                %this.add("Applaud For Me!");
            }
            if ($player.isHostOrCohost()) {
                if ($player.isHost()) {
                    if (%isRealPlayer) {
                        if (%playerClicked.isCohost()) {
                            %n = (1.0 + %n);
                            %this.add("This Space: Unmake Co-Host", , %schemeNormal);
                        }
                        %n = (1.0 + %n);
                        %this.add("This Space: Make Co-Host", , %schemeNormal);
                    }
                }
                if (%isRealPlayer) {
                }
                if (!(%playerClicked.isHost())) {
                    %n = (1.0 + %n);
                    %this.add("This Space: Kick", , %schemeNormal);
                }
                if (!(%isRentabot)) {
                }
                if ($player.isHost()) {
                    if ((-(1.0) == findField($CSBlockedList, %playerName))) {
                        %n = (1.0 + %n);
                        %this.add("This Space: Block", , %schemeNormal);
                    }
                    %n = (1.0 + %n);
                    %this.add("This Space: Unblock", , %schemeNormal);
                }
                if (isObject(%playerClicked)) {
                    if (CustomSpaceClient::isOwner()) {
                    }
                    if (%isRentabot) {
                        %n = (1.0 + %n);
                        %this.add("This Space: Customize", , %schemeNormal);
                    }
                    if ($player.isHostOrCohost()) {
                    }
                    if (!(%playerClicked.rolesPermissionCheckNoWarn("customspaceImmune"))) {
                        if ($player.isHost()) {
                            if (!(%playerClicked.isHost())) {
                            }
                        }
                    }
                    if (!(%playerClicked.isClassAIPlayer())) {
                        %n = (1.0 + %n);
                        %this.add("This Space: Summon", , %schemeNormal);
                        %n = (1.0 + %n);
                        %this.add("This Space: Respawn", , %schemeNormal);
                    }
                }
            }
            if (%onlineHere) {
            }
            if (%ignorable) {
                if (%isIgnore) {
                    %n = (1.0 + %n);
                    %this.add("Unignore", , %schemeIgnore);
                }
                %n = (1.0 + %n);
                %this.add("Ignore", , %schemeIgnore);
            }
            if ((%friendStatus $= "friends")) {
            }
            if (%isNPC) {
                if (CustomSpaceClient::isOwner()) {
                }
                if (isObject(%playerClicked)) {
                }
            }
            if (($player != %playerClicked)) {
                %n = (1.0 + %n);
                %this.add("Teleport To", , %schemeTeleport);
            }
            if (%onlineHere) {
            }
            if (!(%isRentabot)) {
                %n = (1.0 + %n);
                %this.add("Report Abuse", , %schemeProfile);
            }
            if ($flowerGiftingEnabled) {
                if (isObject(%playerClicked)) {
                }
                if (%isNPC) {
                    %skuSelf = getSpecialSKU($player, "flower");
                    %skuThem = getSpecialSKU(%playerClicked, "flower");
                    if (%playerClicked.hasActiveSKU(%skuThem)) {
                    }
                    if (!($player.hasInventorySKU(%skuSelf))) {
                        %n = (1.0 + %n);
                        %this.add("Take Flower", , %schemeGifting);
                    }
                }
                if (isObject(%playerClicked)) {
                }
                if (!(%isNPC)) {
                    %skuSelf = getSpecialSKU($player, "flower");
                    if ($player.hasInventorySKU(%skuSelf)) {
                        %n = (1.0 + %n);
                        %this.add("Give Flower", , %schemeGifting);
                    }
                }
            }
            if (isObject(%playerClicked)) {
                %helpmesku = getSpecialSKU(%playerClicked, "helpmebadge");
                if (%playerClicked.hasActiveSKU(%helpmesku)) {
                    %n = (1.0 + %n);
                    %this.addIfPermitted("answerHelpMe", "Turn off Help Request", , %schemeNormal);
                }
            }
        }
        if (isObject(%playerClicked)) {
            if (!(getWearingItemWithMoreInfo(%playerClicked) $= "")) {
                %n = (1.0 + %n);
                %this.add("** Look At My Clothes **", , 0);
            }
        }
        %n = (1.0 + %n);
        %this.add("My Profile & Account (web)", , 0);
        %n = (1.0 + %n);
        %this.add("Edit Away Message", , 0);
        if (isIdle()) {
            %n = (1.0 + %n);
            %this.add("Back From Idle", , 0);
        }
        %n = (1.0 + %n);
        %this.add("Go Idle", , 0);
        if ($UserPref::Player::TeleportBlock) {
            %n = (1.0 + %n);
            %this.add("Allow Teleports", , 0);
        }
        %n = (1.0 + %n);
        %this.add("Refuse Teleports", , 0);
        if ($UserPref::Player::WhisperBlock) {
            %n = (1.0 + %n);
            %this.add("Allow Whispers", , 0);
        }
        %n = (1.0 + %n);
        %this.add("Refuse Whispers", , 0);
        if ($UserPref::Player::YellBlock) {
            %n = (1.0 + %n);
            %this.add("Allow Yells", , 0);
        }
        %n = (1.0 + %n);
        %this.add("Refuse Yells", , 0);
        %n = (1.0 + %n);
        %this.add("Respawn Me!", , 0);
        if (%playerClicked.isCohost()) {
            %n = (1.0 + %n);
            %this.add("Stop being Co-Host", , 0);
        }
        %text = "";
        if (%playerClicked.isHost()) {
            %sku = getSpecialSKU(%playerClicked, "hostBadge");
            if (hasWord(%playerClicked.getActiveSKUs(), %sku)) {
                %text = "Hide Host Badge";
            }
            %text = "Wear Host Badge";
        }
        if (%playerClicked.isCohost()) {
            %sku = getSpecialSKU(%playerClicked, "cohostBadge");
            if (hasWord(%playerClicked.getActiveSKUs(), %sku)) {
                %text = "Hide Cohost Badge";
            }
            %text = "Wear Cohost Badge";
        }
        if (!(%text $= "")) {
            %n = (1.0 + %n);
            %this.add(%text, , 0);
        }
        if ($player.getActiveSKUs().hasSkuWithAnyTags("drinkMaker")) {
            %n = (1.0 + %n);
            %this.add("Make Drink", SkuManager, %schemeNormal);
        }
        if (%playerClicked.hasMicrophone()) {
            %n = (1.0 + %n);
            %this.add("Drop Microphone", , 0);
        }
        if (areWeInspecting()) {
            if (($player != %playerClicked)) {
                %this.add("Invite to game");
            }
            if (inCustomGame()) {
            }
            if (areWeHostOfInspectedGame()) {
                %this.add("Change score");
                %this.add("Change status");
            }
        }
        %n = (1.0 + %n);
        %this.addIfPermitted("snoop", "        --- mod/staff ---", gameMgrClient, %schemeDisabled);
        %n = (1.0 + %n);
        %this.addIfPermitted("manageUsersBasic", "Ban...", gameMgrClient, %schemeNormal);
        %n = (1.0 + %n);
        %this.addIfPermitted("manageUsersBasic", "Manage on Web", gameMgrClient, %schemeNormal);
        %n = (1.0 + %n);
        %this.addIfPermitted("track", "Fly To", , %schemeFlyTo);
        %n = (1.0 + %n);
        %this.addIfPermitted("track", "Track", , %schemeTrack);
        %n = (1.0 + %n);
        %this.addIfPermitted("manageUsersBasic", "Peek at GameState", , %schemeNormal);
        if (isObject(%playerClicked)) {
            if (%playerClicked.hasRoleString("snooped")) {
                %n = (1.0 + %n);
                %this.addIfPermitted("snoop", "unSnoop", , %schemeNormal);
            }
            %n = (1.0 + %n);
            %this.addIfPermitted("snoop", "Snoop", , %schemeNormal);
        }
        %n = (1.0 + %n);
        %this.addIfPermitted("snoop", "Snoop", , %schemeNormal);
        %n = (1.0 + %n);
        %this.addIfPermitted("snoop", "unSnoop", , %schemeNormal);
        %n = (1.0 + %n);
        %this.addIfPermitted("track", "Teleport To", , %schemeTeleport);
        %n = (1.0 + %n);
        %this.addIfPermitted("summon", "Respawn", , %schemeNormal);
        %n = (1.0 + %n);
        %this.addIfPermitted("summon", "Summon", , %schemeNormal);
        if (CustomSpaceClient::isOwner()) {
        }
        if ($player.isHostOrCohost()) {
        }
        if ($player.rolesPermissionCheckNoWarn("microphones")) {
        }
        if (isObject(%playerClicked)) {
            if (%playerClicked.hasMicrophone()) {
                %n = (1.0 + %n);
                %this.add("Revoke Microphone", , %schemeNormal);
            }
            %n = (1.0 + %n);
            %this.add("Give Microphone", , %schemeNormal);
        }
        if ($player.isDebugging()) {
            %n = (1.0 + %n);
            %this.add("        --- debug --- (" @ %playerClicked @ ")", , %schemeDisabled);
            if (%isNPC) {
            }
            if ($StandAlone) {
                %n = (1.0 + %n);
                %this.add("Set Height: really tall", , %schemeNormal);
                %n = (1.0 + %n);
                %this.add("Set Height: tall", , %schemeNormal);
                %n = (1.0 + %n);
                %this.add("Set Height: medium", , %schemeNormal);
                %n = (1.0 + %n);
                %this.add("Set Height: short", , %schemeNormal);
                %n = (1.0 + %n);
                %this.add("Set Height: really short", , %schemeNormal);
            }
            %n = (1.0 + %n);
            %this.add("Relative Transform", , %schemeSaySumpn);
            %n = (1.0 + %n);
            %this.add("Body Mod", , %schemeSaySumpn);
            %n = (1.0 + %n);
            %this.add("Puppetry: Copy Skus", , %schemeSaySumpn);
            %n = (1.0 + %n);
            %this.add("Puppetry: Paste Skus", , %schemeSaySumpn);
            if (isObject()) {
                %n = (1.0 + %n);
                %this.add("Puppetry: Speak", pChat, %schemeSaySumpn);
            }
            if (isObject()) {
                %n = (1.0 + %n);
                %this.add("Puppetry: Whisper", pChat, %schemeWhisper);
            }
            if (isObject()) {
                %n = (1.0 + %n);
                %this.add("Puppetry: Yell", pChat, %schemeSaySumpn);
            }
            if (isObject()) {
                %n = (1.0 + %n);
                %this.add("Puppetry: SOS", pChat, %schemeSaySumpn);
            }
            if ((%playerClicked.getClassName() $= "AIPlayer")) {
                %n = (1.0 + %n);
                %this.add("Puppetry: Dance", , %schemeSaySumpn);
                %n = (1.0 + %n);
                %this.add("Puppetry: Emote", , %schemeSaySumpn);
                %n = (1.0 + %n);
                %this.add("Puppetry: Be Still", , %schemeSaySumpn);
                %n = (1.0 + %n);
                %this.add("Puppetry: Puppy", , %schemeSaySumpn);
            }
            if (isObject()) {
                %n = (1.0 + %n);
                %this.add("Puppetry: Badge", pChat, %schemeSaySumpn);
            }
            %n = (1.0 + %n);
            %this.add("Puppetry: Dance With", , %schemeSaySumpn);
            %n = (1.0 + %n);
            %this.add("Puppetry: Kiss", , %schemeSaySumpn);
        }
    }
    %title = %playerName;
    if (($player == %playerClicked)) {
        %title = %title @ " " @ "(this is you)";
    }
    if (%isNPC) {
        %title = %title @ " " @ "(a bot)";
    }
    %this.setText(%title);
    gSetField(%this, "playerName", %playerName);
    gSetField(%this, "player", %playerClicked);
    gSetField(%this, "aimName", "");
};
function PlayerContextMenu::initForAIM(%this, %aimName) {
    %this.clear();
    %n = -(1.0);
    %n = (1.0 + %n);
    %this.add("Message", , 0);
    if (showInviteFriend()) {
        %n = (1.0 + %n);
        %this.add("Send Invite", , 0);
    }
    %this.setText(%aimName);
    gSetField(%this, "playerName", "");
    gSetField(%this, "player", "");
    gSetField(%this, "aimName", %aimName);
};
function PlayerContextMenu::addIfPermitted(%this, %permName, %text, %n, %scheme) {
    if ($player.rolesPermissionCheckNoWarn(%permName)) {
        if ((%permName $= "answerHelpMe")) {
            %skuGuide = getSpecialSKU($player, "guidebadge");
            %skuSGuide = getSpecialSKU($player, "seniorguidebadge");
            if (!($player.hasActiveSKU(%skuGuide))) {
            }
            if (!($player.hasActiveSKU(%skuSGuide))) {
                return;
            }
        }
        %this.add(%text, %n, %scheme);
    }
};
function PlayerContextMenu::initWithPlayerName(%this, %playerName) {
    if ((%playerName $= $player.getShapeName())) {
        %playerObj = $player;
    }
    %playerObj = 0;
    %this.init(%playerName, %playerName.getFriendStatus(), %playerName.getIgnoreStatus(), %playerObj);
};
function PlayerContextMenu::initWithPlayer(%this, %player) {
    %playerName = %player.getShapeName();
    %this.init(%playerName, %playerName.getFriendStatus(), %playerName.getIgnoreStatus(), %player);
};
function PlayerContextMenu::onSelect(%this, %unused, %text) {
    %player = gGetField(%this, "player");
    %playerName = gGetField(%this, "playerName");
    %aimName = gGetField(%this, "aimName");
    %handled = 0;
    if (!(%handled)) {
        %handled = 1;
        if ((%text $= "View Profile")) {
            doUserProfile(%playerName);
        }
        if ((%text $= "Remove from Friends")) {
            doUserFavorite(%playerName, "remove");
        }
        if ((%text $= "Cancel Friend Request")) {
            doUserFavorite(%playerName, "cancel");
        }
        if ((%text $= "Accept Friend Request")) {
            doUserFavorite(%playerName, "accept");
        }
        if ((%text $= "Decline Friend Request")) {
            doUserFavorite(%playerName, "decline");
        }
        if ((%text $= "Add to Friends")) {
            doUserFavorite(%playerName, "add");
        }
        if ((%text $= "Whisper")) {
            openUserWhisper(%playerName);
        }
        if ((%text $= "Two-Player Action..")) {
            %playerName.open();
        }
        if ((TwoPlayerEmotesPanel SPC %text $= "Give vCurrency")) {
            %playerName.open("initiate");
        }
        if ((geGiftingPanel SPC %text $= "Give Drink")) {
            drinks_confirmInitiateGift(%playerName);
        }
        if ((%text $= "Give Gift")) {
            gotoWebPage("http://www.vside.com/app/gifting/user/" @ urlEncode(%playerName) @ "/", 0);
        }
        if ((%text $= "Make Drink")) {
            $gSalonChairCurrent = "";
            ShowSalonMenu("drinks", "n", %playerName);
        }
        if ((%text $= "Request Music")) {
            gotoWebPage($Net::MusicURL);
        }
        if ((%text $= "Invite to game")) {
            %playerName.invitePlayerToInspectedGame();
        }
        if ((gameMgrClient SPC %text $= "Change score")) {
            %playerName.doHostPopupChangeScore();
        }
        if ((gameMgrClient SPC %text $= "Change status")) {
            %playerName.doHostPopupChangeStatus();
        }
        %handled = 0;
        gameMgrClient;
    }
    if (!(%handled)) {
        %handled = 1;
        if ((%text $= "This Space: Make Co-Host")) {
            CustomSpaceClient::setCoHostHood(%playerName, 1);
        }
        if ((%text $= "This Space: Unmake Co-Host")) {
            CustomSpaceClient::setCoHostHood(%playerName, 0);
        }
        if ((%text $= "Stop being Co-Host")) {
            CustomSpaceClient::setCoHostHood(%playerName, 0);
        }
        if ((%text $= "Hide Host Badge")) {
            delayedRemoveSku(getSpecialSKU($player, "hostBadge"));
        }
        if ((%text $= "Wear Host Badge")) {
            delayedWearSku(getSpecialSKU($player, "hostBadge"));
        }
        if ((%text $= "Hide Cohost Badge")) {
            delayedRemoveSku(getSpecialSKU($player, "cohostBadge"));
        }
        if ((%text $= "Wear Cohost Badge")) {
            delayedWearSku(getSpecialSKU($player, "cohostBadge"));
        }
        if ((%text $= "This Space: Kick")) {
            CustomSpaceClient::doOwnerAction("kick", %playerName);
        }
        if ((%text $= "This Space: Block")) {
            CustomSpaceClient::TryBlockUserFromSpace(%playerName, 0);
        }
        if ((%text $= "This Space: Unblock")) {
            CustomSpaceClient::TryBlockUserFromSpace(%playerName, 1);
        }
        if ((%text $= "This Space: Summon")) {
            CustomSpaceClient::doOwnerAction("summon", %playerName);
        }
        if ((%text $= "This Space: Respawn")) {
            CustomSpaceClient::doOwnerAction("respawn", %playerName);
        }
        if ((%text $= "This Space: Teleport To")) {
            CustomSpaceClient::doOwnerAction("teleport", %playerName);
        }
        if ((%text $= "This Space: Customize")) {
            rentabotClient_customizeBot(%player);
        }
        if ((%text $= "Ignore")) {
            doUserIgnore(%playerName, "add");
        }
        if ((%text $= "Unignore")) {
            doUserIgnore(%playerName, "remove");
        }
        if ((%text $= "Act Like Me")) {
            doUserMimic(%player);
        }
        if ((%text $= "Report Abuse")) {
            doUserReport(%playerName, "abuse");
        }
        if ((%text $= "My Profile & Account (web)")) {
            doEditProfile();
        }
        if ((%text $= "Edit Away Message")) {
            doEditAwayMessage();
        }
        if ((%text $= "Go Idle")) {
            setIdle(1);
        }
        if ((%text $= "Back From Idle")) {
            setIdle(0);
        }
        if ((%text $= "Allow Teleports")) {
            0.setValue();
            doTeleportBlock();
        }
        if ((TeleportBlockCheckBox SPC %text $= "Refuse Teleports")) {
            1.setValue();
            doTeleportBlock();
        }
        if ((TeleportBlockCheckBox SPC %text $= "Allow Whispers")) {
            0.setValue();
            doWhisperBlock(1);
        }
        if ((WhisperBlockCheckBox SPC %text $= "Refuse Whispers")) {
            1.setValue();
            doWhisperBlock(1);
        }
        if ((WhisperBlockCheckBox SPC %text $= "Allow Yells")) {
            0.setValue();
        }
        if ((YellBlockCheckBox SPC %text $= "Refuse Yells")) {
            1.setValue();
        }
        if ((YellBlockCheckBox SPC %text $= "Respawn Me!")) {
            doRespawnMe();
        }
        if ((%text $= "Drop Microphone")) {
            doDropMic();
        }
        if ((%text $= "** Look At My Clothes **")) {
            doLookAtMyClothes(%player);
        }
        if ((%text $= "Applaud For Me!")) {
            doCheerFor(%playerName);
        }
        %handled = 0;
    }
    if (!(%handled)) {
        %handled = 1;
        if ((%text $= "Set Height: really tall")) {
            if ($StandAlone) {
                %playerName.get().setHeight(1.15);
            }
        }
        if ((PlayerDict SPC %text $= "Set Height: tall")) {
            if ($StandAlone) {
                %playerName.get().setHeight(1.075);
            }
        }
        if ((PlayerDict SPC %text $= "Set Height: medium")) {
            if ($StandAlone) {
                %playerName.get().setHeight(1);
            }
        }
        if ((PlayerDict SPC %text $= "Set Height: short")) {
            if ($StandAlone) {
                %playerName.get().setHeight(0.93);
            }
        }
        if ((PlayerDict SPC %text $= "Set Height: really short")) {
            if ($StandAlone) {
                %playerName.get().setHeight(0.86);
            }
        }
        if ((PlayerDict SPC %text $= "Puppetry: Speak")) {
            doUserSaySomething(%playerName);
        }
        if ((%text $= "Puppetry: Whisper")) {
            doUserWhisperSomething(%playerName);
        }
        if ((%text $= "Puppetry: Yell")) {
            doUserYellSomething(%playerName);
        }
        if ((%text $= "Puppetry: SOS")) {
            doUserSosSomething(%playerName);
        }
        if ((%text $= "Puppetry: Dance")) {
            doUserAutoEmote(%playerName, "dance");
        }
        if ((%text $= "Puppetry: Emote")) {
            doUserAutoEmote(%playerName, "emote");
        }
        if ((%text $= "Puppetry: Blend Spaz")) {
            doUserAutoEmoteRate(%playerName, "blend", 300, 200);
        }
        if ((%text $= "Puppetry: Be Still")) {
            doUserAutoEmote(%playerName, "");
        }
        if ((%text $= "Puppetry: Puppy")) {
            doUserPuppy(%playerName);
        }
        if ((%text $= "Puppetry: Badge")) {
            doUserBadge(%playerName);
        }
        if ((%text $= "Puppetry: Dance With")) {
            doDanceWith(%player);
        }
        if ((%text $= "Puppetry: Kiss")) {
            doKiss(%player);
        }
        if ((%text $= "Puppetry: Copy Skus")) {
            doUserCopySkus(%playerName);
        }
        if ((%text $= "Puppetry: Paste Skus")) {
            doUserPasteSkus(%playerName);
        }
        if ((%text $= "Body Mod")) {
            doUserBodyMod(%player);
        }
        if ((%text $= "Relative Transform")) {
            doUserRelativeTransform(%player);
        }
        if ((%text $= "Teleport To")) {
            teleportOperation(%playerName);
        }
        if ((%text $= "Fly To")) {
            doUserFlyTo(%playerName);
        }
        if ((%text $= "Peek at GameState")) {
            doUserPeekAtGameState(%playerName);
        }
        if ((%text $= "Track")) {
            doUserTrack(%playerName);
        }
        if ((%text $= "Snoop")) {
            doUserSnoop(%playerName, 1);
        }
        if ((%text $= "unSnoop")) {
            doUserSnoop(%playerName, 0);
        }
        if ((%text $= "Respawn")) {
            doUserRespawn(%playerName);
        }
        if ((%text $= "Summon")) {
            doUserSummon(%playerName);
        }
        if ((%text $= "Ban...")) {
            doUserBan(%playerName);
        }
        if ((%text $= "Manage on Web")) {
            doUserManage(%playerName);
        }
        if ((%text $= "Take Flower")) {
            doTakeFlower(%playerName);
        }
        if ((%text $= "Give Flower")) {
            doGiveFlower(%playerName);
        }
        if ((%text $= "Turn off Help Request")) {
            doTurnOffHelpme(%playerName);
        }
        if ((%text $= "Revoke Microphone")) {
            doMicrophoneGiveOrRevoke(%playerName, 0);
        }
        if ((%text $= "Give Microphone")) {
            doMicrophoneGiveOrRevoke(%playerName, 1);
        }
        if ((%text $= "Message")) {
            %aimName.talkTo();
        }
        if ((AIMConvManager SPC %text $= "Send Invite")) {
            %aimName.open();
        }
        %handled = 0;
        AimInviteDialog;
    }
};
function PlayerContextMenu::showComingSoon(%this, %featureName) {
    MessageBoxOK(%featureName @ " " @ "- Coming Soon!", "The" @ " " @ %featureName @ " " @ "feature will be here soon!", "");
};
function PlayGui::onRMBPlayer(%this, %obj) {
    %obj.initWithPlayer();
    getCursorPos().showAtPoint();
};
function BuddyHudRequestsList::onRightMouseUp(%this) {
    %text = stripUnprintables(%this.getRowText(%this.getMouseOverRow()));
    if (!(%text $= "")) {
        %text.initWithPlayerName();
        getCursorPos().showAtPoint();
    }
    warn("Got empty player name from right click on FavoritesList");
};
function doUserMimic(%player) {
    commandToServer('EtsPlayerClickedSharedAnim', %player.getGhostID());
};
function doGiftInventoryItemToPlayer(%playerName, %sku) {
    commandToServer('GiftInventoryItemToPlayer', %playerName, %sku);
};
function doRespawnMe() {
    commandToServer('respawnMe');
};
function doDropMic() {
    commandToServer('dropMic');
};
function doDanceWith(%obj) {
    doLookAt(%obj, 1, 0);
};
function doKiss(%obj) {
    doLookAt(%obj, 0, 1, 0);
};
$InspectSkusMap = 0;
function getWearingItemWithMoreInfo(%player) {
    if (!(isObject($InspectSkusMap))) {
        $InspectSkusMap = new ""();
        StringMap;
        if (isObject()) {
            $InspectSkusMap.add();
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
    MissionCleanup;
    %num = getWordCount(%skusList);
    MissionCleanup;
    %inspectTextDir = "projects/common/inventoryInspect/";
    0;
    %i = 0;
    if ((%num < %i)) {
        %skunum = getWord(%skusList, %i);
        %mapped = $InspectSkusMap.get(%skunum);
        if (!(%mapped $= "")) {
            %skunum = %mapped;
        }
        %inspectFile = %inspectTextDir @ %skunum @ ".txt";
        %fo = new ""();
        FileObject;
        if (%fo.openForRead(%inspectFile)) {
            %fo.delete();
            return %inspectFile;
        }
        %fo.delete();
        %i = (1.0 + %i);
    }
    return "";
};
function doLookAtMyClothes(%player) {
    echo("TODO:  make this pick the proper thing");
    %file = getWearingItemWithMoreInfo(%player);
    if (!(%file $= "")) {
        echo("looking closer at player's clothes");
        %file.OnInspect();
    }
};
function doCheerFor(%playerName) {
    "applause".open(%playerName);
};
function doTurnOffHelpme(%playerName) {
    commandToServer('TurnOffHelpMeMode', %playerName);
};
