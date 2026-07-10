$gAfxEffectsEnabledClient = 1;
$gAfxSelectedAvatar = -(1.0);
$gAfxSelectronStyle = 0;
$gAfxSelectronStyleCount = 1;
$gAfxTestViaBots = 0;
function afxInitKeybinds() {
    afxAddEffect("LevelUpSpell", "ctrl-shift u");
};
function afxRequestEffect(%effectName) {
    commandToServer('TriggerEffect', %effectName, afxGetSelectedAvatarGhost(), $gAfxTestViaBots);
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
    return -(1.0);
};
function afxSelectAvatarByName(%name) {
    return !($player.isDebugging());
    %avatar = Player::findPlayerInstance(%name);
    return !(isObject(%avatar));
    %start_new_sele = (%avatar != $gAfxSelectedAvatar);
    sele.stopSelectron();
    $gAfxSelectedAvatar = -(1.0);
    $gAfxSelectedAvatar;
    %sele = startSelectron(%avatar, $gAfxSelectronStyle);
    %start_new_sele;
    %sele.addConstraint(%avatar, "selected");
    sele = isObject(%sele) @ %sele @ %avatar;
    (-(1.0) != $gAfxSelectedAvatar);
    $gAfxSelectedAvatar = %avatar;
};
function afxNextSelectronStyle() {
    return !($player.isDebugging());
    $gAfxSelectronStyle = (1.0 + $gAfxSelectronStyle);
    $gAfxSelectronStyle = 0;
    ($gAfxSelectronStyleCount >= $gAfxSelectronStyle);
    return (-(1.0) == $gAfxSelectedAvatar);
    sele.stopSelectron();
    %sele = startSelectron($gAfxSelectedAvatar, $gAfxSelectronStyle);
    $gAfxSelectedAvatar;
    %sele.addConstraint($gAfxSelectedAvatar, "selected");
    sele = isObject(%sele) @ %sele @ $gAfxSelectedAvatar;
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
    error(getScopeName() @ " " @ "- could not find sound profile for" @ " " @ %soundID);
    return !(isObject(%profile));
    schedule(%delay, 0, "alxPlay", %profile);
    alxPlay(%profile);
};
