$gAfxEffectsEnabledClient = 1;
$gAfxSelectedAvatar = -(1.0);
$gAfxSelectronStyle = 0;
$gAfxSelectronStyleCount = 1;
$gAfxTestViaBots = 0;
if (!($gAfxEffectsEnabledClient)) {
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
    %keyBinding.bindCmd(moveMap @ keyboard @ "afxRequestEffect(\"" @ %effectName @ "\");", "");
    safeEnsureScriptObject("StringMap", "afxEffectsCatalog");
    %effectName.put(%keyBinding);
};
function afxGetSelectedAvatar() {
    return $gAfxSelectedAvatar;
};
function afxGetSelectedAvatarGhost() {
    if ((-(1.0) != $gAfxSelectedAvatar)) {
    }
    return -(1.0);
};
function afxSelectAvatarByName(%name) {
    if (!($player.isDebugging())) {
        return;
    }
    %avatar = Player::findPlayerInstance(%name);
    if (!(isObject(%avatar))) {
        return;
    }
    %start_new_sele = (%avatar != $gAfxSelectedAvatar);
    if ((-(1.0) != $gAfxSelectedAvatar)) {
        sele.stopSelectron();
        $gAfxSelectedAvatar = -(1.0);
        $gAfxSelectedAvatar;
    }
    if (%start_new_sele) {
        %sele = startSelectron(%avatar, $gAfxSelectronStyle);
        if (isObject(%sele)) {
            %sele.addConstraint(%avatar, "selected");
            sele = %sele @ %avatar;
            $gAfxSelectedAvatar = %avatar;
        }
    }
};
function afxNextSelectronStyle() {
    if (!($player.isDebugging())) {
        return;
    }
    $gAfxSelectronStyle = (1.0 + $gAfxSelectronStyle);
    if (($gAfxSelectronStyleCount >= $gAfxSelectronStyle)) {
        $gAfxSelectronStyle = 0;
    }
    if ((-(1.0) == $gAfxSelectedAvatar)) {
        return;
    }
    sele.stopSelectron();
    %sele = startSelectron($gAfxSelectedAvatar, $gAfxSelectronStyle);
    $gAfxSelectedAvatar;
    if (isObject(%sele)) {
        %sele.addConstraint($gAfxSelectedAvatar, "selected");
        sele = %sele @ $gAfxSelectedAvatar;
    }
};
afxInitKeybinds();
$gAfxSelectedAvatar["AudioProfile_AFX_TeleIn" @ $gAfxClientSounds TAB "TeleIn" @ "profile"] = ;
$gAfxSelectedAvatar["AudioProfile_AFX_TeleIn" @ $gAfxClientSounds TAB "TeleIn" @ "profile"][300 @ $gAfxClientSounds TAB "TeleIn" @ "delay"] = ;
$gAfxSelectedAvatar["AudioProfile_AFX_TeleIn" @ $gAfxClientSounds TAB "TeleIn" @ "profile"][300 @ $gAfxClientSounds TAB "TeleIn" @ "delay"]["AudioProfile_AFX_TeleOut" @ $gAfxClientSounds TAB "TeleOut" @ "profile"] = ;
$gAfxSelectedAvatar["AudioProfile_AFX_TeleIn" @ $gAfxClientSounds TAB "TeleIn" @ "profile"][300 @ $gAfxClientSounds TAB "TeleIn" @ "delay"]["AudioProfile_AFX_TeleOut" @ $gAfxClientSounds TAB "TeleOut" @ "profile"][200 @ $gAfxClientSounds TAB "TeleOut" @ "delay"] = ;
function ClientCmdAfxClientSpecificSound(%soundID) {
    %soundID = detag(%soundID);
    %profile = %soundID[$gAfxClientSounds TAB %soundID @ "profile"];
    %delay = %soundID[$gAfxClientSounds TAB %soundID @ "delay"];
    if (!(isObject(%profile))) {
        error(getScopeName() @ " " @ "- could not find sound profile for" @ " " @ %soundID);
        return;
    }
    if ((0.0 > %delay)) {
        schedule(%delay, 0, "alxPlay", %profile);
    }
    alxPlay(%profile);
};
