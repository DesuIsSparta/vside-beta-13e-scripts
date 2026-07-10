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
    %sameServer = (%playerName.get(UserListFriends).serverName $= $ServerName);
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
    %grey.addScheme(%this, 1, %grey, %grey);
    %white.addScheme(%this, 2, %white, %white);
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
    %n = -(1.0);
    if (%hasName) {
        if ((%playerClicked != $player)) {
            %onlineHere = %playerName.isOnlineHereOrNotFavorite(BuddyHudWin);
            %ignorable = 1;
            if (isObject(%playerClicked)) {
                %ignorable = !("omnivocal".rolesPermissionCheckNoWarn(%playerClicked));
            }
            if (isObject(%playerClicked) && !(getWearingItemWithMoreInfo(%playerClicked) $= "")) {
                %schemeProfile.add(%this, "** Look At My Clothes **", %n = (%n + 1.0));
            }
            %schemeProfile.add(%this, "View Profile", %n = (%n + 1.0));
            if ((%friendStatus $= "friends")) {
                %schemeFavorite.add(%this, "Remove from Friends", %n = (%n + 1.0));
            }
            if ((%friendStatus $= "favorite")) {
                %schemeFavorite.add(%this, "Cancel Friend Request", %n = (%n + 1.0));
            }
            if ((%friendStatus $= "fan")) {
                %schemeFavorite.add(%this, "Accept Friend Request", %n = (%n + 1.0));
                %schemeFavorite.add(%this, "Decline Friend Request", %n = (%n + 1.0));
            }
            if ((%friendStatus $= "none")) {
                %schemeFavorite.add(%this, "Add to Friends", %n = (%n + 1.0));
            }
            if (%onlineHere) {
                %schemePM.add(%this, "Whisper", %n = (%n + 1.0));
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer && !(%isNPC)) {
                }
                if ($StandAlone) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange(geGiftingPanel)) {
                }
                if (!(%isIdle)) {
                    %schemeNormal.add(%this, "Two-Player Action..", %n = (%n + 1.0));
                }
                if (!(%playerClicked.isInRange(geGiftingPanel))) {
                    %schemeDisabled.add(%this, "Two-Player Action.. - Too Far!", %n = (%n + 1.0));
                }
                if (%isIdle) {
                    %schemeDisabled.add(%this, "Two-Player Action.. - Idle!", %n = (%n + 1.0));
                }
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange(geGiftingPanel)) {
                }
                if (!(%isIdle)) {
                    %schemeNormal.add(%this, "Give vCurrency", %n = (%n + 1.0));
                }
                if (!(%playerClicked.isInRange(geGiftingPanel))) {
                    %schemeDisabled.add(%this, "Give vCurrency - Too Far!", %n = (%n + 1.0));
                }
                if (%isIdle) {
                    %schemeDisabled.add(%this, "Give vCurrency - Idle!", %n = (%n + 1.0));
                }
            }
            if ("drink".hasSkuWithAnyTags(SkuManager, $player.getActiveSKUs())) {
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange(geGiftingPanel)) {
                }
                if (!(%isIdle)) {
                    %schemeNormal.add(%this, "Give Drink", %n = (%n + 1.0));
                }
                if (!(%playerClicked.isInRange(geGiftingPanel))) {
                    %schemeDisabled.add(%this, "Give Drink - Too Far!", %n = (%n + 1.0));
                }
                if (%isIdle) {
                    %schemeDisabled.add(%this, "Give Drink - Idle!", %n = (%n + 1.0));
                }
            }
            %schemeNormal.add(%this, "Give Gift", %n = (%n + 1.0));
            if ("drinkMaker".hasSkuWithAnyTags(SkuManager, $player.getActiveSKUs())) {
            }
            if (!(rentabot_isRentabotName(%playerName))) {
                if (%sameServer) {
                }
                if (%onlineHere) {
                }
                if (!(%isIgnore)) {
                }
                if (%playerClicked.isInRange(geGiftingPanel)) {
                }
                if (!(%isIdle)) {
                    %schemeNormal.add(%this, "Make Drink", %n = (%n + 1.0));
                }
                if (!(%playerClicked.isInRange(geGiftingPanel))) {
                    %schemeDisabled.add(%this, "Make Drink - Too Far!", %n = (%n + 1.0));
                }
                if (%isIdle) {
                    %schemeDisabled.add(%this, "Make Drink - Idle!", %n = (%n + 1.0));
                }
            }
            if (isObject(%playerClicked)) {
            }
            if (%playerClicked.getCanHandleMusicRequest()) {
                %schemeNormal.add(%this, "Request Music", %n = (%n + 1.0));
            }
            if (isObject(%playerClicked)) {
                "Applaud For Me!".add(%this);
            }
            if ($player.isHostOrCohost()) {
                if ($player.isHost() && %isRealPlayer) {
                    if (%playerClicked.isCohost()) {
                        %schemeNormal.add(%this, "This Space: Unmake Co-Host", %n = (%n + 1.0));
                    }
                    %schemeNormal.add(%this, "This Space: Make Co-Host", %n = (%n + 1.0));
                }
                if (%isRealPlayer) {
                }
                if (!(%playerClicked.isHost())) {
                    %schemeNormal.add(%this, "This Space: Kick", %n = (%n + 1.0));
                }
                if (!(%isRentabot)) {
                }
                if ($player.isHost()) {
                    if ((findField($CSBlockedList, %playerName) == -(1.0))) {
                        %schemeNormal.add(%this, "This Space: Block", %n = (%n + 1.0));
                    }
                    %schemeNormal.add(%this, "This Space: Unblock", %n = (%n + 1.0));
                }
                if (isObject(%playerClicked)) {
                    if (CustomSpaceClient::isOwner()) {
                    }
                    if (%isRentabot) {
                        %schemeNormal.add(%this, "This Space: Customize", %n = (%n + 1.0));
                    }
                    if ($player.isHostOrCohost()) {
                    }
                    if (!("customspaceImmune".rolesPermissionCheckNoWarn(%playerClicked)) && $player.isHost() && !(%playerClicked.isHost())) {
                    }
                    if (!(%playerClicked.isClassAIPlayer())) {
                        %schemeNormal.add(%this, "This Space: Summon", %n = (%n + 1.0));
                        %schemeNormal.add(%this, "This Space: Respawn", %n = (%n + 1.0));
                    }
                }
            }
            if (%onlineHere) {
            }
            if (%ignorable) {
                if (%isIgnore) {
                    %schemeIgnore.add(%this, "Unignore", %n = (%n + 1.0));
                }
                %schemeIgnore.add(%this, "Ignore", %n = (%n + 1.0));
            }
            if ((%friendStatus $= "friends")) {
            }
            if (%isNPC) {
                if (CustomSpaceClient::isOwner()) {
                }
                if (isObject(%playerClicked)) {
                }
            }
            if ((%playerClicked != $player)) {
                %schemeTeleport.add(%this, "Teleport To", %n = (%n + 1.0));
            }
            if (%onlineHere) {
            }
            if (!(%isRentabot)) {
                %schemeProfile.add(%this, "Report Abuse", %n = (%n + 1.0));
            }
            if ($flowerGiftingEnabled) {
                if (isObject(%playerClicked)) {
                }
                if (%isNPC) {
                    %skuSelf = getSpecialSKU($player, "flower");
                    %skuThem = getSpecialSKU(%playerClicked, "flower");
                    if (%skuThem.hasActiveSKU(%playerClicked)) {
                    }
                    if (!(%skuSelf.hasInventorySKU($player))) {
                        %schemeGifting.add(%this, "Take Flower", %n = (%n + 1.0));
                    }
                }
                if (isObject(%playerClicked)) {
                }
                if (!(%isNPC)) {
                    %skuSelf = getSpecialSKU($player, "flower");
                    if (%skuSelf.hasInventorySKU($player)) {
                        %schemeGifting.add(%this, "Give Flower", %n = (%n + 1.0));
                    }
                }
            }
            if (isObject(%playerClicked)) {
                %helpmesku = getSpecialSKU(%playerClicked, "helpmebadge");
                if (%helpmesku.hasActiveSKU(%playerClicked)) {
                    %schemeNormal.addIfPermitted(%this, "answerHelpMe", "Turn off Help Request", %n = (%n + 1.0));
                }
            }
        }
        if (isObject(%playerClicked) && !(getWearingItemWithMoreInfo(%playerClicked) $= "")) {
            0.add(%this, "** Look At My Clothes **", %n = (%n + 1.0));
        }
        0.add(%this, "My Profile & Account (web)", %n = (%n + 1.0));
        0.add(%this, "Edit Away Message", %n = (%n + 1.0));
        if (isIdle()) {
            0.add(%this, "Back From Idle", %n = (%n + 1.0));
        }
        0.add(%this, "Go Idle", %n = (%n + 1.0));
        if ($UserPref::Player::TeleportBlock) {
            0.add(%this, "Allow Teleports", %n = (%n + 1.0));
        }
        0.add(%this, "Refuse Teleports", %n = (%n + 1.0));
        if ($UserPref::Player::WhisperBlock) {
            0.add(%this, "Allow Whispers", %n = (%n + 1.0));
        }
        0.add(%this, "Refuse Whispers", %n = (%n + 1.0));
        if ($UserPref::Player::YellBlock) {
            0.add(%this, "Allow Yells", %n = (%n + 1.0));
        }
        0.add(%this, "Refuse Yells", %n = (%n + 1.0));
        0.add(%this, "Respawn Me!", %n = (%n + 1.0));
        if (%playerClicked.isCohost()) {
            0.add(%this, "Stop being Co-Host", %n = (%n + 1.0));
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
            0.add(%this, %text, %n = (%n + 1.0));
        }
        if ("drinkMaker".hasSkuWithAnyTags(SkuManager, $player.getActiveSKUs())) {
            %schemeNormal.add(%this, "Make Drink", %n = (%n + 1.0));
        }
        if (%playerClicked.hasMicrophone()) {
            0.add(%this, "Drop Microphone", %n = (%n + 1.0));
        }
        if (gameMgrClient.areWeInspecting()) {
            if ((%playerClicked != $player)) {
                "Invite to game".add(%this);
            }
            if (gameMgrClient.inCustomGame()) {
            }
            if (gameMgrClient.areWeHostOfInspectedGame()) {
                "Change score".add(%this);
                "Change status".add(%this);
            }
        }
        %schemeDisabled.addIfPermitted(%this, "snoop", "        --- mod/staff ---", %n = (%n + 1.0));
        %schemeNormal.addIfPermitted(%this, "manageUsersBasic", "Ban...", %n = (%n + 1.0));
        %schemeNormal.addIfPermitted(%this, "manageUsersBasic", "Manage on Web", %n = (%n + 1.0));
        %schemeFlyTo.addIfPermitted(%this, "track", "Fly To", %n = (%n + 1.0));
        %schemeTrack.addIfPermitted(%this, "track", "Track", %n = (%n + 1.0));
        %schemeNormal.addIfPermitted(%this, "manageUsersBasic", "Peek at GameState", %n = (%n + 1.0));
        if (isObject(%playerClicked)) {
            if ("snooped".hasRoleString(%playerClicked)) {
                %schemeNormal.addIfPermitted(%this, "snoop", "unSnoop", %n = (%n + 1.0));
            }
            %schemeNormal.addIfPermitted(%this, "snoop", "Snoop", %n = (%n + 1.0));
        }
        %schemeNormal.addIfPermitted(%this, "snoop", "Snoop", %n = (%n + 1.0));
        %schemeNormal.addIfPermitted(%this, "snoop", "unSnoop", %n = (%n + 1.0));
        %schemeTeleport.addIfPermitted(%this, "track", "Teleport To", %n = (%n + 1.0));
        %schemeNormal.addIfPermitted(%this, "summon", "Respawn", %n = (%n + 1.0));
        %schemeNormal.addIfPermitted(%this, "summon", "Summon", %n = (%n + 1.0));
        if (CustomSpaceClient::isOwner()) {
        }
        if ($player.isHostOrCohost()) {
        }
        if ("microphones".rolesPermissionCheckNoWarn($player)) {
        }
        if (isObject(%playerClicked)) {
            if (%playerClicked.hasMicrophone()) {
                %schemeNormal.add(%this, "Revoke Microphone", %n = (%n + 1.0));
            }
            %schemeNormal.add(%this, "Give Microphone", %n = (%n + 1.0));
        }
        if ($player.isDebugging()) {
            %schemeDisabled.add(%this, "        --- debug --- (" @ %playerClicked @ ")", %n = (%n + 1.0));
            if (%isNPC) {
            }
            if ($StandAlone) {
                %schemeNormal.add(%this, "Set Height: really tall", %n = (%n + 1.0));
                %schemeNormal.add(%this, "Set Height: tall", %n = (%n + 1.0));
                %schemeNormal.add(%this, "Set Height: medium", %n = (%n + 1.0));
                %schemeNormal.add(%this, "Set Height: short", %n = (%n + 1.0));
                %schemeNormal.add(%this, "Set Height: really short", %n = (%n + 1.0));
            }
            %schemeSaySumpn.add(%this, "Relative Transform", %n = (%n + 1.0));
            %schemeSaySumpn.add(%this, "Body Mod", %n = (%n + 1.0));
            %schemeSaySumpn.add(%this, "Puppetry: Copy Skus", %n = (%n + 1.0));
            %schemeSaySumpn.add(%this, "Puppetry: Paste Skus", %n = (%n + 1.0));
            if (isObject(pChat)) {
                %schemeSaySumpn.add(%this, "Puppetry: Speak", %n = (%n + 1.0));
            }
            if (isObject(pChat)) {
                %schemeWhisper.add(%this, "Puppetry: Whisper", %n = (%n + 1.0));
            }
            if (isObject(pChat)) {
                %schemeSaySumpn.add(%this, "Puppetry: Yell", %n = (%n + 1.0));
            }
            if (isObject(pChat)) {
                %schemeSaySumpn.add(%this, "Puppetry: SOS", %n = (%n + 1.0));
            }
            if ((%playerClicked.getClassName() $= "AIPlayer")) {
                %schemeSaySumpn.add(%this, "Puppetry: Dance", %n = (%n + 1.0));
                %schemeSaySumpn.add(%this, "Puppetry: Emote", %n = (%n + 1.0));
                %schemeSaySumpn.add(%this, "Puppetry: Be Still", %n = (%n + 1.0));
                %schemeSaySumpn.add(%this, "Puppetry: Puppy", %n = (%n + 1.0));
            }
            if (isObject(pChat)) {
                %schemeSaySumpn.add(%this, "Puppetry: Badge", %n = (%n + 1.0));
            }
            %schemeSaySumpn.add(%this, "Puppetry: Dance With", %n = (%n + 1.0));
            %schemeSaySumpn.add(%this, "Puppetry: Kiss", %n = (%n + 1.0));
        }
    }
    %title = %playerName;
    if ((%playerClicked == $player)) {
        %title = %title @ " " @ "(this is you)";
    }
    if (%isNPC) {
        %title = %title @ " " @ "(a bot)";
    }
    %title.setText(%this);
    gSetField(%this, "playerName", %playerName);
    gSetField(%this, "player", %playerClicked);
    gSetField(%this, "aimName", "");
};
function PlayerContextMenu::initForAIM(%this, %aimName) {
    %this.clear();
    %n = -(1.0);
    0.add(%this, "Message", %n = (%n + 1.0));
    if (showInviteFriend()) {
        0.add(%this, "Send Invite", %n = (%n + 1.0));
    }
    %aimName.setText(%this);
    gSetField(%this, "playerName", "");
    gSetField(%this, "player", "");
    gSetField(%this, "aimName", %aimName);
};
function PlayerContextMenu::addIfPermitted(%this, %permName, %text, %n, %scheme) {
    if (%permName.rolesPermissionCheckNoWarn($player)) {
        if ((%permName $= "answerHelpMe")) {
            %skuGuide = getSpecialSKU($player, "guidebadge");
            %skuSGuide = getSpecialSKU($player, "seniorguidebadge");
            if (!(%skuGuide.hasActiveSKU($player))) {
            }
            if (!(%skuSGuide.hasActiveSKU($player))) {
                return;
            }
        }
        %scheme.add(%this, %text, %n);
    }
};
function PlayerContextMenu::initWithPlayerName(%this, %playerName) {
    if ((%playerName $= $player.getShapeName())) {
        %playerObj = $player;
    }
    %playerObj = 0;
    %playerObj.init(%this, %playerName, %playerName.getFriendStatus(BuddyHudWin), %playerName.getIgnoreStatus(BuddyHudWin));
};
function PlayerContextMenu::initWithPlayer(%this, %player) {
    %playerName = %player.getShapeName();
    %player.init(%this, %playerName, %playerName.getFriendStatus(BuddyHudWin), %playerName.getIgnoreStatus(BuddyHudWin));
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
            %playerName.open(TwoPlayerEmotesPanel);
        }
        if ((%text $= "Give vCurrency")) {
            "initiate".open(geGiftingPanel, %playerName);
        }
        if ((%text $= "Give Drink")) {
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
            %playerName.invitePlayerToInspectedGame(gameMgrClient);
        }
        if ((%text $= "Change score")) {
            %playerName.doHostPopupChangeScore(gameMgrClient);
        }
        if ((%text $= "Change status")) {
            %playerName.doHostPopupChangeStatus(gameMgrClient);
        }
        %handled = 0;
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
            0.setValue(TeleportBlockCheckBox);
            doTeleportBlock();
        }
        if ((%text $= "Refuse Teleports")) {
            1.setValue(TeleportBlockCheckBox);
            doTeleportBlock();
        }
        if ((%text $= "Allow Whispers")) {
            0.setValue(WhisperBlockCheckBox);
            doWhisperBlock(1);
        }
        if ((%text $= "Refuse Whispers")) {
            1.setValue(WhisperBlockCheckBox);
            doWhisperBlock(1);
        }
        if ((%text $= "Allow Yells")) {
            0.setValue(YellBlockCheckBox);
        }
        if ((%text $= "Refuse Yells")) {
            1.setValue(YellBlockCheckBox);
        }
        if ((%text $= "Respawn Me!")) {
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
                1.15.setHeight(%playerName.get(PlayerDict));
            }
        }
        if ((%text $= "Set Height: tall")) {
            if ($StandAlone) {
                1.075.setHeight(%playerName.get(PlayerDict));
            }
        }
        if ((%text $= "Set Height: medium")) {
            if ($StandAlone) {
                1.setHeight(%playerName.get(PlayerDict));
            }
        }
        if ((%text $= "Set Height: short")) {
            if ($StandAlone) {
                0.93.setHeight(%playerName.get(PlayerDict));
            }
        }
        if ((%text $= "Set Height: really short")) {
            if ($StandAlone) {
                0.86.setHeight(%playerName.get(PlayerDict));
            }
        }
        if ((%text $= "Puppetry: Speak")) {
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
            %aimName.talkTo(AIMConvManager);
        }
        if ((%text $= "Send Invite")) {
            %aimName.open(AimInviteDialog);
        }
        %handled = 0;
    }
};
function PlayerContextMenu::showComingSoon(%this, %featureName) {
    MessageBoxOK(%featureName @ " " @ "- Coming Soon!", "The" @ " " @ %featureName @ " " @ "feature will be here soon!", "");
};
function PlayGui::onRMBPlayer(%this, %obj) {
    %obj.initWithPlayer(PlayerContextMenu);
    Canvas.getCursorPos().showAtPoint(PlayerContextMenu);
};
function BuddyHudRequestsList::onRightMouseUp(%this) {
    %text = stripUnprintables(%this.getMouseOverRow().getRowText(%this));
    if (!(%text $= "")) {
        %text.initWithPlayerName(PlayerContextMenu);
        Canvas.getCursorPos().showAtPoint(PlayerContextMenu);
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
        $InspectSkusMap = new StringMap("");
        if (isObject(MissionCleanup)) {
            $InspectSkusMap.add(MissionCleanup);
        }
        "neptune".put($InspectSkusMap, 22228);
        "neptune".put($InspectSkusMap, 22229);
        "rtv".put($InspectSkusMap, 32261);
        "rtv".put($InspectSkusMap, 32262);
        "masquerade".put($InspectSkusMap, 22212);
        "masquerade".put($InspectSkusMap, 22213);
        "viper".put($InspectSkusMap, 32259);
        "viper".put($InspectSkusMap, 32260);
        "wireless".put($InspectSkusMap, 22210);
        "wireless".put($InspectSkusMap, 22211);
        "visionvamp".put($InspectSkusMap, 32257);
        "visionvamp".put($InspectSkusMap, 32258);
        "crown".put($InspectSkusMap, 22230);
        "crown".put($InspectSkusMap, 22231);
        "cosmicyoga".put($InspectSkusMap, 32274);
        "cosmicyoga".put($InspectSkusMap, 32275);
        "royalflush".put($InspectSkusMap, 22214);
        "royalflush".put($InspectSkusMap, 22215);
        "warrior".put($InspectSkusMap, 32255);
        "warrior".put($InspectSkusMap, 32256);
    }
    %skusList = %player.getActiveSKUs();
    %num = getWordCount(%skusList);
    %inspectTextDir = "projects/common/inventoryInspect/";
    %i = 0;
    while ((%i < %num)) {
        %skunum = getWord(%skusList, %i);
        %mapped = %skunum.get($InspectSkusMap);
        if (!(%mapped $= "")) {
            %skunum = %mapped;
        }
        %inspectFile = %inspectTextDir @ %skunum @ ".txt";
        %fo = new FileObject("");
        if (%inspectFile.openForRead(%fo)) {
            %fo.delete();
            return %inspectFile;
        }
        %fo.delete();
        %i = (%i + 1.0);
    }
    return "";
};
function doLookAtMyClothes(%player) {
    echo("TODO:  make this pick the proper thing");
    %file = getWearingItemWithMoreInfo(%player);
    if (!(%file $= "")) {
        echo("looking closer at player's clothes");
        %file.OnInspect(MLScrollInspectPanel);
    }
};
function doCheerFor(%playerName) {
    %playerName.open(ApplauseMeterGui, "applause");
};
function doTurnOffHelpme(%playerName) {
    commandToServer('TurnOffHelpMeMode', %playerName);
};
