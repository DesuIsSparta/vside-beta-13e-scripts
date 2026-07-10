$gAfxEffectsEnabledClient = 1;
$gAfxSelectedAvatar = -(1.0);
$gAfxSelectronStyle = 0;
$gAfxSelectronStyleCount = 1;
$gAfxTestViaBots = 0;
if (!$gAfxEffectsEnabledClient) {
}
function afxInitKeybinds() {
    afxAddEffect("LevelUpSpell", "ctrl-shift u");
};
function afxRequestEffect(%effectName) {
    if ($player.isDebugging()) {
        commandToServer('TriggerEffect', %effectName, afxGetSelectedAvatarGhost(), $gAfxTestViaBots);
    }
};
function afxAddEffect(%effectName, %keyBinding) {
    moveMap.bindCmd(keyboard, %keyBinding, "afxRequestEffect(\"" @ %effectName @ "\");", "");
    safeEnsureScriptObject("StringMap", "afxEffectsCatalog");
    afxEffectsCatalog.put(%effectName, %keyBinding);
};
function afxGetSelectedAvatar() {
    return $gAfxSelectedAvatar;
};
function afxGetSelectedAvatarGhost() {
    if (($gAfxSelectedAvatar != -(1.0))) {
    }
    return -(1.0);
};
function afxSelectAvatarByName(%name) {
    if (!$player.isDebugging()) {
        return;
    }
    %avatar = Player::findPlayerInstance(%name);
    if (!isObject(%avatar)) {
        return;
    }
    %start_new_sele = ($gAfxSelectedAvatar != %avatar);
    if (($gAfxSelectedAvatar != -(1.0))) {
        $gAfxSelectedAvatar.sele.stopSelectron();
        $gAfxSelectedAvatar = -(1.0);
    }
    if (%start_new_sele) {
        %sele = startSelectron(%avatar, $gAfxSelectronStyle);
        if (isObject(%sele)) {
            %sele.addConstraint(%avatar, "selected");
            %avatar.sele = %sele;
            $gAfxSelectedAvatar = %avatar;
        }
    }
};
function afxNextSelectronStyle() {
    if (!$player.isDebugging()) {
        return;
    }
    $gAfxSelectronStyle = ($gAfxSelectronStyle + 1.0);
    if (($gAfxSelectronStyle >= $gAfxSelectronStyleCount)) {
        $gAfxSelectronStyle = 0;
    }
    if (($gAfxSelectedAvatar == -(1.0))) {
        return;
    }
    $gAfxSelectedAvatar.sele.stopSelectron();
    %sele = startSelectron($gAfxSelectedAvatar, $gAfxSelectronStyle);
    if (isObject(%sele)) {
        %sele.addConstraint($gAfxSelectedAvatar, "selected");
        $gAfxSelectedAvatar.sele = %sele;
    }
};
afxInitKeybinds();
$gAfxSelectedAvatar["AudioProfile_AFX_TeleIn" @ $gAfxClientSounds TAB "TeleIn" @ "profile"] = ;
$gAfxClientSounds["TeleIn","delay"] = 300;
$gAfxClientSounds["TeleOut","profile"] = "AudioProfile_AFX_TeleOut";
$gAfxClientSounds["TeleOut","delay"] = 200;
function ClientCmdAfxClientSpecificSound(%soundID) {
    %soundID = detag(%soundID);
    %profile = %soundID[$gAfxClientSounds TAB %soundID @ "profile"];
    %delay = %soundID[$gAfxClientSounds TAB %soundID @ "delay"];
    if (!isObject(%profile)) {
        error(getScopeName() @ " " @ "- could not find sound profile for" @ " " @ %soundID);
        return;
    }
    if ((%delay > 0.0)) {
        schedule(%delay, 0, "alxPlay", %profile);
    }
    alxPlay(%profile);
};
