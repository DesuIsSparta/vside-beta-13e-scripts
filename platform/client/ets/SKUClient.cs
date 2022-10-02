function Player::onGotSKUs(%this)
{
    %this.currentBaseActiveSkus = %this.getActiveSKUs();
    if (%this.hasMicrophone())
    {
        %this.setBlendTargetValue($BB_UPPR_MICROPHONE, 0.2);
        %this.triggerBoneBlendAnimation($BB_UPPR_MICROPHONE, 1, 0);
    }
    else
    {
        %this.setBlendTargetValue($BB_UPPR_MICROPHONE, 0);
        %this.triggerBoneBlendAnimation($BB_UPPR_MICROPHONE, 0, 0);
    }
    if (!(%this.getShapeName() $= $Player::Name))
    {
        return ;
    }
    %skus = %this.getActiveSKUs();
    %gender = %this.getGender();
    %outfitName = $gOutfits.get("currentOutfit");
    $gOutfits.put(%gender @ "Body", SkuManager.filterSkusForBody(%skus));
    $gOutfits.put(%gender @ %outfitName, SkuManager.filterSkusForClothing(%skus));
    $Player::IsInHelpMeMode = %this.isInHelpMeMode();
    if (isObject(SalonStyleSelector) && SalonStyleSelector.isVisible())
    {
        SalonStyleSelector.refreshAvailableStyles();
    }
    %instrumentGenre = "";
    if (isObject(ApplauseMeterGui))
    {
        %usingInstrument = 0;
        %i = InstrumentRegistryClient.getInstrumentCount() - 1;
        while (!%usingInstrument)
        {
            %instrument = InstrumentRegistryClient.getInstrumentByIndex(%i);
            if (hasWord(%skus, %instrument.skus[%gender]))
            {
                ApplauseMeterGui.open("instrument", %instrument.name);
                %instrumentGenre = %instrument.genre;
                %usingInstrument = 1;
            }
            %i = %i - 1;
        }
        if (!%usingInstrument && (ApplauseMeterGui.applauseMeterUse $= "instrument"))
        {
            ApplauseMeterGui.closingFromServer = 1;
            ApplauseMeterGui.close();
        }
    }
    %propSku = %this.getActivePropSku();
    %currentGenre = %this.getGenre();
    if (%propSku $= "")
    {
        if (isPropGenre(%currentGenre))
        {
            %propGenre = %currentGenre;
        }
        else
        {
            %propGenre = "y";
        }
    }
    else
    {
        %propGenre = PropGenreMap.get(%propSku);
        if (%propGenre $= "")
        {
            error(getScopeName() SPC "- could not find genre for sku" SPC %propSku SPC "using y." SPC getTrace());
            %propGenre = "y";
        }
    }
    %currentGenreIsInstrumentGenre = InstrumentRegistryClient.isInstrumentGenre(%currentGenre);
    %currentGenreIsPropGenre = isPropGenre(%currentGenre);
    if (!((%instrumentGenre $= "")) && !((%currentGenre $= %instrumentGenre)))
    {
        commandToServer('EnterSpecialGenre', %instrumentGenre);
    }
    else
    {
        if ((%propSku $= "") && %currentGenreIsInstrumentGenre)
        {
            commandToServer('ExitSpecialGenre', %currentGenre);
        }
        else
        {
            if ((!((%propSku $= "")) && (%instrumentGenre $= "")) && !%currentGenreIsPropGenre)
            {
                commandToServer('EnterSpecialGenre', %propGenre);
            }
            else
            {
                if ((%propSku $= "") && %currentGenreIsPropGenre)
                {
                    commandToServer('ExitSpecialGenre', %propGenre);
                }
                else
                {
                    if ((!((%propSku $= "")) && %currentGenreIsPropGenre) && !((%currentGenre $= %propGenre)))
                    {
                        commandToServer('SwitchSpecialGenre', %currentGenre, %propGenre);
                    }
                }
            }
        }
    }
    if (((((%propSku $= "") && %currentGenreIsPropGenre) || %currentGenreIsInstrumentGenre) || !((%instrumentGenre $= ""))) && !((%currentGenre $= $UserPref::Player::Genre)))
    {
        commandToServer('setGenre', $UserPref::Player::Genre);
    }
    updateHelpMeModeMenu();
    MessageHud.updateModeIcon();
    %this.resetSkuEffectsClient();
    %n = getWordCount(%skus) - 1;
    while (%n >= 0)
    {
        %sku = getWord(%skus, %n);
        if (!hasWord(%this.prevActiveSkus, %sku))
        {
            trySkuNotification(%sku);
        }
        %this.trySkuEffectsClient(%sku);
        %n = %n - 1;
    }
    %this.prevActiveSkus = %this.getActiveSKUs();
    return %i;
}
function Player::applySkuBadge(%this, %skunum)
{
    %prevSkuBadge = gGetField(%this, prevSkuBadge);
    if (%prevSkuBadge == %skunum)
    {
        return ;
    }
    gSetField(%this, prevSkuBadge, %skunum);
    %this.updateMapIcon();
    %hudCtrl = %this.hudCtrl;
    if (!isObject(%hudCtrl))
    {
        return ;
    }
    if (%this == $player)
    {
        trySkuNotification(%skunum);
    }
    if ((%skunum == 0) && %this.rolesPermissionCheckNoWarn("hideBadges"))
    {
        if (isObject(%hudCtrl.badge))
        {
            %hudCtrl.badge.setVisible(0);
        }
        return ;
    }
    %si = SkuManager.findBySku(%skunum);
    if (!isObject(%si))
    {
        return ;
    }
    if (!isObject(%hudCtrl.badge))
    {
        %ctrl = new GuiBitmapCtrl()
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "64 64";
            minExtent = "64 64";
            sluggishness = -1;
            visible = 1;
        };
        %hudCtrl.add(%ctrl);
        %hudCtrl.badge = %ctrl;
    }
    %bitmap = getBitmapFilename("badge", getWord(%si.getTxtrNames(), 0));
    %hudCtrl.badge.setBitmap(%bitmap);
    %hudCtrl.badge.setVisible(1);
    return ;
}
$gSkuNotificationsMap = 0;
function initSkuNotificationsMap()
{
    if (isObject($gSkuNotificationsMap))
    {
        return ;
    }
    $gSkuNotificationsMap = safeNewScriptObject("StringMap", "", 1);
    $gSkuNotificationsMap.put(17002, "displayMicrophoneHelp();");
    $gSkuNotificationsMap.put(27002, "displayMicrophoneHelp();");
    return ;
}
function trySkuNotification(%skunum)
{
    initSkuNotificationsMap();
    %cmd = $gSkuNotificationsMap.get(%skunum);
    if (!(%cmd $= ""))
    {
        eval(%cmd);
    }
    return ;
}
function Player::resetSkuEffectsClient(%this)
{
    %this.staggerSetAmount(0);
    return ;
}
function Player::trySkuEffectsClient(%this, %sku)
{
    %si = SkuManager.findBySku(%sku);
    if (0)
    {
    }
    else
    {
        if (hasWord(%si.tags, "stagger3"))
        {
            %this.staggerSetAmount(0.1);
        }
        else
        {
            if (hasWord(%si.tags, "stagger2"))
            {
                %this.staggerSetAmount(0.05);
            }
            else
            {
                if (hasWord(%si.tags, "stagger1"))
                {
                    %this.staggerSetAmount(0.01);
                }
            }
        }
    }
    return ;
}
function SkuItem::getBitmapPath(%this)
{
    if (%this.skuType $= "swatch")
    {
        %ret = getBitmapFilename(%this.skuType, getWord(%this.getTxtrNames(), 1));
    }
    else
    {
        %ret = getBitmapFilename(%this.skuType, getWord(%this.getTxtrNames(), 0));
    }
    return ;
}
