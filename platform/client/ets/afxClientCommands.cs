$gAfxEffectsEnabledClient = 1;
$gAfxSelectedAvatar = -1;
$gAfxSelectronStyle = 0;
$gAfxSelectronStyleCount = 1;
$gAfxTestViaBots = 0;
if (!$gAfxEffectsEnabledClient)
{
    return ;
}
function afxInitKeybinds()
{
    afxAddEffect("LevelUpSpell", "ctrl-shift u");
    return ;
}
function afxRequestEffect(%effectName)
{
    if ($player.isDebugging())
    {
        commandToServer('TriggerEffect', %effectName, afxGetSelectedAvatarGhost(), $gAfxTestViaBots);
    }
    return ;
}
function afxAddEffect(%effectName, %keyBinding)
{
    moveMap.bindCmd(keyboard, %keyBinding, "afxRequestEffect(\"" @ %effectName @ "\");", "");
    safeEnsureScriptObject("StringMap", "afxEffectsCatalog");
    afxEffectsCatalog.put(%effectName, %keyBinding);
    return ;
}
function afxGetSelectedAvatar()
{
    return $gAfxSelectedAvatar;
}
function afxGetSelectedAvatarGhost()
{
    return $gAfxSelectedAvatar != -1 ? ServerConnection.GetGhostIndex($gAfxSelectedAvatar) : 1;
}
function afxSelectAvatarByName(%name)
{
    if (!$player.isDebugging())
    {
        return ;
    }
    %avatar = Player::findPlayerInstance(%name);
    if (!isObject(%avatar))
    {
        return ;
    }
    %start_new_sele = $gAfxSelectedAvatar != %avatar;
    if ($gAfxSelectedAvatar != -1)
    {
        $gAfxSelectedAvatar.sele.stopSelectron();
        $gAfxSelectedAvatar = -1;
    }
    if (%start_new_sele)
    {
        %sele = startSelectron(%avatar, $gAfxSelectronStyle);
        if (isObject(%sele))
        {
            %sele.addConstraint(%avatar, "selected");
            %avatar.sele = %sele;
            $gAfxSelectedAvatar = %avatar;
        }
    }
    return ;
}
function afxNextSelectronStyle()
{
    if (!$player.isDebugging())
    {
        return ;
    }
    $gAfxSelectronStyle = $gAfxSelectronStyle + 1;
    if ($gAfxSelectronStyle >= $gAfxSelectronStyleCount)
    {
        $gAfxSelectronStyle = 0;
    }
    if ($gAfxSelectedAvatar == -1)
    {
        return ;
    }
    $gAfxSelectedAvatar.sele.stopSelectron();
    %sele = startSelectron($gAfxSelectedAvatar, $gAfxSelectronStyle);
    if (isObject(%sele))
    {
        %sele.addConstraint($gAfxSelectedAvatar, "selected");
        $gAfxSelectedAvatar.sele = %sele;
    }
    return ;
}
afxInitKeybinds();
$gAfxClientSounds["TeleIn","profile"] = "AudioProfile_AFX_TeleIn";
$gAfxClientSounds["TeleIn","delay"] = 300;
$gAfxClientSounds["TeleOut","profile"] = "AudioProfile_AFX_TeleOut";
$gAfxClientSounds["TeleOut","delay"] = 200;
function ClientCmdAfxClientSpecificSound(%soundID)
{
    %soundID = detag(%soundID);
    %profile = $gAfxClientSounds[%soundID,"profile"];
    %delay = $gAfxClientSounds[%soundID,"delay"];
    if (!isObject(%profile))
    {
        error(getScopeName() SPC "- could not find sound profile for" SPC %soundID);
        return ;
    }
    if (%delay > 0)
    {
        schedule(%delay, 0, "alxPlay", %profile);
    }
    else
    {
        alxPlay(%profile);
    }
    return ;
}
