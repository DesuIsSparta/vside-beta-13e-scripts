function SalonStyleSelector::refreshAvailableStyles(%this) {
    ShowSalonMenu(lastTypeOfSalon, lastClientGender);
};
function ShowSalonMenu(%typeOfSalon, %clientGender, %targetPlayerName) {
    if (!(isDefined("%targetPlayerName"))) {
        %targetPlayerName = "";
    }
    lastTypeOfSalon = %typeOfSalon @ SalonStyleSelector;
    lastClientGender = %clientGender @ SalonStyleSelector;
    open();
    if ((SalonStyleSelector SPC %targetPlayerName $= "")) {
    }
    %targetPlayer = Player::findPlayerInstance(%targetPlayerName);
    "";
    targetPlayer = %targetPlayer @ SalonStyleSelector;
    %thumbsDirectory = "platform/client/ui/salon/salonthumbs_";
    SalonStyleSelectorChair @ %thumbsDirectory @ %typeOfSalon.setBitmap();
    %text = "Choose" @ " " @ %typeOfSalon[$SALON_CHAIR_DEF_PROPDESC @ %typeOfSalon] @ " " @ "Prop";
    %text.setText();
    %text = %typeOfSalon[$SALON_CHAIR_DEF_SALONMENUDESC @ %typeOfSalon];
    ShowPropsButton;
    %text = strreplace(%text, "[TARGET]", %targetPlayerName);
    %text.setText();
    closeButton.setVisible(%typeOfSalon[$SALON_CHAIR_DEF_CANCLOSE @ %typeOfSalon]);
    %propSku = $player.getActivePropSku();
    SalonStyleSelector;
    %propThumbsDir = "platform/client/ui/props/propthumbs_";
    gePropsWindowTitle;
    if ((%propSku $= "")) {
    }
    %propThumbFile = "" @ %propThumbsDir @ %propSku;
    if ((%propThumbFile $= "")) {
    }
    if (!(isFile(%propThumbFile @ ".png"))) {
        "".setBitmap();
    }
    %propThumbFile.setBitmap();
    %list = skuGuiList;
    SalonStyleSelector;
    %list.setNumChildren(0);
    childrenExtent = %list @ ((spacing * 2.0) - getWord(%list.getExtent(), 0)) @ " " @ 40 @ %list;
    SalonStyleSelectorProp;
    %width = getWord(childrenExtent, 0);
    %list;
    %i = 0;
    SalonStyleSelectorProp;
    if (($NUM_SALON_STYLES < %i)) {
        %skunum = %i[$SALON_STYLE_SKU @ %i];
        %grouping = %i[$SALON_STYLE_GROUPING @ %i];
        %req = %i[$SALON_STYLE_REQUIREDSKUS @ %i];
        if (!(%grouping $= %typeOfSalon)) {
        }
        if (!(%req $= "")) {
            if (!(DoesPlayerHaveItemActive($player, %req))) {
            }
        }
        %si = %skunum.findBySku();
        SkuManager;
        if (isObject(%si)) {
            if ((%si SPC gender $= %clientGender)) {
            }
            if ((%si SPC gender $= "n")) {
                %iconPath = %thumbsDirectory @ %skunum;
                %linkStart = "<a:gamelink chooseStyle" @ " " @ %i @ ">";
                %thumbnail = "";
                %description = descShrt;
                %si;
                %text = %linkStart @ %thumbnail @ " " @ "<clip:" @ %width @ ">" @ %description @ "</clip></a>";
                %text = mlStyle(%text, "salonPanel");
                %item = %list.addChild();
                extent = GuiBitmapCtrl @ new ""() @ "46 40";
                0;
                bitmap = %iconPath;
                position = GuiBitmapButtonCtrl @ new ""() @ "1 1";
                extent = "44 38";
                bitmap = "platform/client/buttons/tgf/tgf_buttonframe_190x109";
                command = "SalonChooseStyle(" @ %i @ ");";
                %iconCtrl = ;
                position = GuiMLTextCtrl @ new ""() @ "50 13";
                0;
                extent = "142 20";
                bitmap = %iconPath;
                %textCtrl = ;
                %item.add(%iconCtrl);
                %item.add(%textCtrl);
                %textCtrl.bindClassName("SalonStyleSelectorRow");
                %textCtrl.setProfile();
                %textCtrl.setText(%text);
            }
        }
        %i = (1.0 + %i);
        InfoWindowTextListProfile;
    }
    %list.reseatChildren();
    if ((0.0 == %list.getNumChildren())) {
        %msg = (($NUM_SALON_STYLES < %i) SPC $player.getActivePropSku() $= "") ? "No styles available.\nTry choosing a prop." : "No styles available.\nChoose another prop.";
        noSkuGuiText.setText(SalonStyleSelector @ "<just:center>" @ %msg);
    }
    noSkuGuiText.setText("");
};
function SalonStyleSelectorRow::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    if ((firstWord(%url) $= "chooseStyle")) {
        %styleNumber = getWord(%url, 1);
        SalonChooseStyle(%styleNumber);
    }
};
function clientCmdShowSalonMenu(%salonChair, %gender, %typeOfSalon) {
    $gSalonChairCurrent = %salonChair;
    ShowSalonMenu(%typeOfSalon, %gender);
};
function clientCmdHideSalonMenu(%salonChair) {
    if (!($gSalonChairCurrent $= %salonChair)) {
        return;
    }
    cancel($gSalonStylistAnimSchedule);
    $gSalonStylistAnimSchedule = 0;
    $gSalonChairCurrent = 0;
    close();
};
function SalonGiveTheStyleToClient(%styleNumber) {
    cancel($gSalonStylistAnimSchedule);
    $gSalonStylistAnimSchedule = 0;
    commandToServer('SalonChooseStyle', $gSalonChairCurrent, %styleNumber);
};
function SalonChooseStyle(%styleNumber) {
    cancel($gSalonStylistAnimSchedule);
    $gSalonStylistAnimSchedule = 0;
    %sku = %styleNumber[$SALON_STYLE_SKU @ %styleNumber];
    %req = %styleNumber[$SALON_STYLE_REQUIREDSKUS @ %styleNumber];
    %reqMsg = %styleNumber[$SALON_STYLE_REQUREDSKUSMESSAGE @ %styleNumber];
    %anim = %styleNumber[$SALON_STYLE_ANIMATION @ %styleNumber];
    %cutTime = %styleNumber[$SALON_STYLE_CUTTIME @ %styleNumber];
    if (!(%req $= "")) {
        if (!(DoesPlayerHaveItemActive($player, %req))) {
            %reqname = getSkuShortName(%req);
            MessageBoxOK("vSalon", %reqMsg, "");
            return;
        }
    }
    if ((SalonStyleSelector SPC lastTypeOfSalon $= "drinks")) {
        drinks_confirmInitiateMake(targetPlayer.getShapeName(), %sku);
    }
    commandToServer('EtsPlayAnimName', %anim);
    $gSalonStylistAnimSchedule = schedule(%cutTime, 0, "SalonGiveTheStyleToClient", %styleNumber);
    SalonStyleSelector;
};
$gSalonStylistAnimSchedule = 0;
$gSalonChairCurrent = 0;
