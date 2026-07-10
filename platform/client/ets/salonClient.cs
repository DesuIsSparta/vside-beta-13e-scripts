function SalonStyleSelector::refreshAvailableStyles(%this) {
    ShowSalonMenu(%this.lastTypeOfSalon, %this.lastClientGender);
};
function ShowSalonMenu(%typeOfSalon, %clientGender, %targetPlayerName) {
    if (!(isDefined("%targetPlayerName"))) {
        %targetPlayerName = "";
    }
    %this.lastTypeOfSalon = %typeOfSalon @ SalonStyleSelector;
    %this.lastClientGender = %clientGender @ SalonStyleSelector;
    SalonStyleSelector.open();
    if ((%targetPlayerName $= "")) {
    }
    %targetPlayer = Player::findPlayerInstance(%targetPlayerName);
    "";
    %this.targetPlayer = %targetPlayer @ SalonStyleSelector;
    %thumbsDirectory = "platform/client/ui/salon/salonthumbs_";
    %thumbsDirectory @ %typeOfSalon.setBitmap(SalonStyleSelectorChair);
    %text = "Choose" @ " " @ %typeOfSalon[$SALON_CHAIR_DEF_PROPDESC @ %typeOfSalon] @ " " @ "Prop";
    %text.setText(ShowPropsButton);
    %text = %typeOfSalon[$SALON_CHAIR_DEF_SALONMENUDESC @ %typeOfSalon];
    %text = strreplace(%text, "[TARGET]", %targetPlayerName);
    %text.setText(gePropsWindowTitle);
    %typeOfSalon[$SALON_CHAIR_DEF_CANCLOSE @ %typeOfSalon].setVisible(SalonStyleSelector, %this.closeButton);
    %propSku = $player.getActivePropSku();
    %propThumbsDir = "platform/client/ui/props/propthumbs_";
    if ((%propSku $= "")) {
    }
    %propThumbFile = %propThumbsDir @ %propSku;
    "";
    if ((%propThumbFile $= "")) {
    }
    if (!(isFile(%propThumbFile @ ".png"))) {
        "".setBitmap(SalonStyleSelectorProp);
    }
    %propThumbFile.setBitmap(SalonStyleSelectorProp);
    %list = %this.skuGuiList;
    SalonStyleSelector;
    0.setNumChildren(%list);
    %list.childrenExtent = (getWord(%list.getExtent(), 0) - (2.0 * %list.spacing)) @ " " @ 40;
    %width = getWord(%list.childrenExtent, 0);
    %i = 0;
    while ((%i < $NUM_SALON_STYLES)) {
        %skunum = %i[$SALON_STYLE_SKU @ %i];
        %grouping = %i[$SALON_STYLE_GROUPING @ %i];
        %req = %i[$SALON_STYLE_REQUIREDSKUS @ %i];
        if (!(%grouping $= %typeOfSalon)) {
        }
        if (!(%req $= "") && !(DoesPlayerHaveItemActive($player, %req))) {
        }
        %si = %skunum.findBySku(SkuManager);
        if (isObject(%si)) {
            if ((%si.gender $= %clientGender)) {
            }
            if ((%si.gender $= "n")) {
                %iconPath = %thumbsDirectory @ %skunum;
                %linkStart = "<a:gamelink chooseStyle" @ " " @ %i @ ">";
                %thumbnail = "";
                %description = %si.descShrt;
                %text = %linkStart @ %thumbnail @ " " @ "<clip:" @ %width @ ">" @ %description @ "</clip></a>";
                %text = mlStyle(%text, "salonPanel");
                %item = %list.addChild();
                %iconCtrl = new GuiBitmapCtrl("") {
                    extent = 0 @ "46 40";
                    bitmap = %iconPath;
                };
                new GuiBitmapButtonCtrl("") {
                    position = "1 1";
                    extent = "44 38";
                    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_190x109";
                    command = "SalonChooseStyle(" @ %i @ ");";
                };
                %textCtrl = new GuiMLTextCtrl("") {
                    position = 0 @ "50 13";
                    extent = "142 20";
                    bitmap = %iconPath;
                };
                %iconCtrl.add(%item);
                %textCtrl.add(%item);
                "SalonStyleSelectorRow".bindClassName(%textCtrl);
                %textCtrl.setProfile();
                %text.setText(%textCtrl);
            }
        }
        %i = (%i + 1.0);
        InfoWindowTextListProfile;
    }
    %list.reseatChildren();
    if ((%list.getNumChildren() == 0.0)) {
        %msg = ((%i < $NUM_SALON_STYLES) @ " " @ $player.getActivePropSku() $= "") ? "No styles available.\nTry choosing a prop." : "No styles available.\nChoose another prop.";
        "<just:center>" @ %msg.setText(SalonStyleSelector, noSkuGuiText);
    }
    "".setText(SalonStyleSelector, noSkuGuiText);
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
    SalonStyleSelector.close();
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
    if (!(%req $= "") && !(DoesPlayerHaveItemActive($player, %req))) {
        %reqname = getSkuShortName(%req);
        MessageBoxOK("vSalon", %reqMsg, "");
        return;
    }
    if ((SalonStyleSelector @ " " @ lastTypeOfSalon $= "drinks")) {
        drinks_confirmInitiateMake(targetPlayer.getShapeName(SalonStyleSelector), %sku);
    }
    commandToServer('EtsPlayAnimName', %anim);
    $gSalonStylistAnimSchedule = schedule(%cutTime, 0, "SalonGiveTheStyleToClient", %styleNumber);
};
$gSalonStylistAnimSchedule = 0;
$gSalonChairCurrent = 0;
