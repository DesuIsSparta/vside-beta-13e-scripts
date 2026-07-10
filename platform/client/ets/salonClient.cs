function SalonStyleSelector::refreshAvailableStyles(%this)
{
    ShowSalonMenu(%this.lastTypeOfSalon, %this.lastClientGender);
}
function ShowSalonMenu(%typeOfSalon, %clientGender, %targetPlayerName)
{
    if (!isDefined("%targetPlayerName"))
    {
        %targetPlayerName = "";
    }
    SalonStyleSelector.lastTypeOfSalon = %typeOfSalon;
    SalonStyleSelector.lastClientGender = %clientGender;
    SalonStyleSelector.open();
    %targetPlayer = (%targetPlayerName $= "") ? "" : Player::findPlayerInstance(%targetPlayerName);
    SalonStyleSelector.targetPlayer = %targetPlayer;
    %thumbsDirectory = "platform/client/ui/salon/salonthumbs_";
    SalonStyleSelectorChair.setBitmap(%thumbsDirectory @ %typeOfSalon);
    %text = "Choose" @ " " @ $SALON_CHAIR_DEF_PROPDESC[%typeOfSalon] @ " " @ "Prop";
    ShowPropsButton.setText(%text);
    %text = $SALON_CHAIR_DEF_SALONMENUDESC[%typeOfSalon];
    %text = strreplace(%text, "[TARGET]", %targetPlayerName);
    gePropsWindowTitle.setText(%text);
    SalonStyleSelector.closeButton.setVisible($SALON_CHAIR_DEF_CANCLOSE[%typeOfSalon]);
    %propSku = $player.getActivePropSku();
    %propThumbsDir = "platform/client/ui/props/propthumbs_";
    if (%propSku $= "")
    {
    }
    else
    {
    }
    %propThumbFile = %propThumbsDir @ %propSku;
    "";
    if ((%propThumbFile $= "") || !(isFile(%propThumbFile @ ".png")))
    {
        SalonStyleSelectorProp.setBitmap("");
    }
    else
    {
        SalonStyleSelectorProp.setBitmap(%propThumbFile);
    }
    %list = SalonStyleSelector.skuGuiList;
    %list.setNumChildren(0);
    %list.childrenExtent = (getWord(%list.getExtent(), 0) - (2 * %list.spacing)) @ " " @ 40;
    %width = getWord(%list.childrenExtent, 0);
    %i = 0;
    while (%i < $NUM_SALON_STYLES)
    {
        %skunum = $SALON_STYLE_SKU[%i];
        %grouping = $SALON_STYLE_GROUPING[%i];
        %req = $SALON_STYLE_REQUIREDSKUS[%i];
        if (!(%grouping $= %typeOfSalon))
        {
        }
        else
        {
            if (!(%req $= "") && !DoesPlayerHaveItemActive($player, %req))
            {
            }
            %si = SkuManager.findBySku(%skunum);
            if (isObject(%si) && (%si.gender $= %clientGender) || (%si.gender $= "n"))
            {
                %iconPath = %thumbsDirectory @ %skunum;
                %linkStart = "<a:gamelink chooseStyle" @ " " @ %i @ ">";
                %thumbnail = "";
                %description = %si.descShrt;
                %text = %linkStart @ %thumbnail @ " " @ "<clip:" @ %width @ ">" @ %description @ "</clip></a>";
                %text = mlStyle(%text, "salonPanel");
                %item = %list.addChild();
                %iconCtrl = new GuiBitmapCtrl("") {
                    extent = "46 40";
                    bitmap = %iconPath;
                };
                new GuiBitmapButtonCtrl("") {
                    position = "1 1";
                    extent = "44 38";
                    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_190x109";
                    command = "SalonChooseStyle(" @ %i @ ");";
                };
                %textCtrl = new GuiMLTextCtrl("") {
                    position = "50 13";
                    extent = "142 20";
                    bitmap = %iconPath;
                };
                %item.add(%iconCtrl);
                %item.add(%textCtrl);
                %textCtrl.bindClassName("SalonStyleSelectorRow");
                %textCtrl.setProfile(InfoWindowTextListProfile);
                %textCtrl.setText(%text);
            }
        }
        %i = %i + 1;
    }
    %list.reseatChildren();
    if (%list.getNumChildren() == 0)
    {
        %msg = ($player.getActivePropSku() $= "") ? "No styles available.\nTry choosing a prop." : "No styles available.\nChoose another prop.";
        SalonStyleSelector.noSkuGuiText.setText("<just:center>" @ %msg);
    }
    else
    {
        SalonStyleSelector.noSkuGuiText.setText("");
    }
}
function SalonStyleSelectorRow::onURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %url = restWords(%url);
    }
    if (firstWord(%url) $= "chooseStyle")
    {
        %styleNumber = getWord(%url, 1);
        SalonChooseStyle(%styleNumber);
    }
}
function clientCmdShowSalonMenu(%salonChair, %gender, %typeOfSalon)
{
    $gSalonChairCurrent = %salonChair;
    ShowSalonMenu(%typeOfSalon, %gender);
}
function clientCmdHideSalonMenu(%salonChair)
{
    if (!($gSalonChairCurrent $= %salonChair))
    {
        return;
    }
    cancel($gSalonStylistAnimSchedule);
    $gSalonStylistAnimSchedule = 0;
    $gSalonChairCurrent = 0;
    SalonStyleSelector.close();
}
function SalonGiveTheStyleToClient(%styleNumber)
{
    cancel($gSalonStylistAnimSchedule);
    $gSalonStylistAnimSchedule = 0;
    commandToServer('SalonChooseStyle', $gSalonChairCurrent, %styleNumber);
}
function SalonChooseStyle(%styleNumber)
{
    cancel($gSalonStylistAnimSchedule);
    $gSalonStylistAnimSchedule = 0;
    %sku = $SALON_STYLE_SKU[%styleNumber];
    %req = $SALON_STYLE_REQUIREDSKUS[%styleNumber];
    %reqMsg = $SALON_STYLE_REQUREDSKUSMESSAGE[%styleNumber];
    %anim = $SALON_STYLE_ANIMATION[%styleNumber];
    %cutTime = $SALON_STYLE_CUTTIME[%styleNumber];
    if (!(%req $= "") && !DoesPlayerHaveItemActive($player, %req))
    {
        %reqname = getSkuShortName(%req);
        MessageBoxOK("vSalon", %reqMsg, "");
        return;
    }
    if (SalonStyleSelector.lastTypeOfSalon $= "drinks")
    {
        drinks_confirmInitiateMake(SalonStyleSelector.targetPlayer.getShapeName(), %sku);
    }
    else
    {
        commandToServer('EtsPlayAnimName', %anim);
        $gSalonStylistAnimSchedule = schedule(%cutTime, 0, "SalonGiveTheStyleToClient", %styleNumber);
    }
}
$gSalonStylistAnimSchedule = 0;
$gSalonChairCurrent = 0;
