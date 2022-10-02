GuiButtonProfile.soundButtonOver = "AudioButtonOver";
GuiTextProfile.fontColorLink = "255   0 153 255";
GuiTextProfile.fontColorLinkHL = "255   0 153 255";
$RegistrationTextColor = " 30  30  30 255";
$RegistrationErrorColor = "250 215  20 255";
$ClosetHiliteLt = "102 102 102 255";
$ClosetHiliteDk = " 51  51  51 255";
$ClosetHiliteBackgroundLt = "  0   0   0   0";
$ClosetHiliteBackgroundDk = "  0   0   0   0";
$WindowBackgroundDk = "  0   0   0 163";
$WindowBorderColor = " 91  91  91 255";
$VPointsFontColor = " 21 159 231";
$VPointsFontColorLink = " 21 159 231";
$VPointsFontColorLinkHL = "118 199 241";
$VBuxFontColor = " 19 185  60";
$VBuxFontColorLink = " 19 185  60";
$VBuxFontColorLinkHL = "117 214 141";
$Fuschia = "229  83 255 255";
$FuschiaLt = "255 147 248 255";
$FuschiaDk = "147   0 137 255";
$FuschiaAlpha = "229  83 255 80";
$SeaFoam = "170 200 180 255";
$SeaFoamLt = "188 201 190 255";
$SeaFoamDk = "113 120 154 255";
$SeaFoamIn = "200 205 205 255";
$Algae = "170 250  50 255";
$AlgaeLt = "218 253 167 255";
$AlgaeDk = " 93 157   3 255";
$DarkSea = "  0  50  80 255";
$DarkSeaLt = "  0 152 240 255";
$DarkSeaDk = "  0  25  40 255";
$HighlightColor = $Fuschia;
$HighlightColorLt = $FuschiaLt;
$HighlightColorDk = $FuschiaDk;
$HighlightColorLight = $SeaFoam;
$HighlightColorLightLt = "100 255 200 255";
$HighlightColorLightDk = $SeaFoamDk;
$HighlightColorLightIn = $SeaFoamIn;
$InfoTextColor = "230 144   0 255";
$NameColorStaff = "255   0   0 255";
$NameColorFriendStaff = "255   0 153 255";
$NameColorNormal = "255 255 255 255";
$NameColorFriend = "102 255  26 255";
$NameColorElsewhere = "155 137  56 255";
$NameColorOffline = "150 150 150 255";
$NameColorStaffF = ColorIToColorF($NameColorStaff);
$NameColorFriendStaffF = ColorIToColorF($NameColorFriendStaff);
$NameColorNormalF = ColorIToColorF($NameColorNormal);
$NameColorFriendF = ColorIToColorF($NameColorFriend);
$NameColorElsewhereF = ColorIToColorF($NameColorElsewhere);
$NameColorOfflineF = ColorIToColorF($NameColorOffline);
$NameColorHilite = "255   0 153 255";
$NameColorHiliteF = ColorIToColorF($NameColorHilite);
$NameColorIdleModulationF = "0.5 0.5 0.5 1.0";
$gMlStyle["UserName_Normal"] = "<color:" @ ColorIToHex($NameColorNormal) @ ">";
$gMlStyle["UserName_Friend"] = "<color:" @ ColorIToHex($NameColorFriend) @ ">";
new GuiControlProfile(ETSNonModalProfile : GuiDefaultProfile);
new GuiControlProfile(HUDDarkProfile : ETSNonModalProfile)
{
    fillColor = $WindowBackgroundDk;
    opaque = 1;
};
new GuiControlProfile(SwatchBrushProfile : ETSNonModalProfile)
{
    border = 1;
    borderColor = "200 255 40 255";
};
new GuiControlProfile(PortraitProfile : ETSNonModalProfile)
{
    border = 1;
    borderColor = "128 128 128 128";
};
new GuiControlProfile(PaperDollBackdropProfile : ETSNonModalProfile)
{
    fillColor = "255 255 255 255";
    opaque = 1;
};
new GuiControlProfile(ETSDroppableProfile);
new GuiControlProfile(TransitionMessageProfile : GuiTextProfile)
{
    fontType = "SF Cartoonist Hand Bold";
    fontSize = 40;
    opaque = 0;
    border = 0;
    drawShadow = 1;
    borderColor = "250  10   0 255";
    fillColor = "255   0 153";
    fillColorHL = "  0   0   0 120";
    fontColor = "255 255 255";
    fontColorHL = " 90  90  90";
    justify = "center";
    hasBitmapArray = 0;
    autoSizeWidth = 0;
    autoSizeHeight = 0;
    acceptMouseMove = 0;
    acceptLeftMouse = 0;
    acceptRightMouse = 0;
    acceptMouseWheel = 0;
};
new GuiControlProfile(SelfView : GuiTextEditProfile)
{
    fontType = "SF Cartoonist Hand Bold";
    fontSize = 25;
    opaque = 0;
    border = 0;
    drawShadow = 1;
    borderColor = "120 120 120 255";
    fillColor = "120 120 120 255";
    fillColorHL = "120 120 120 255";
    fontColor = "255 255 255";
    fontColorHL = " 90  90  90";
    justify = "center";
    hasBitmapArray = 0;
    autoSizeWidth = 0;
    autoSizeHeight = 0;
    halobitmap = "projects/common/characters/textures/reflect";
};
new GuiControlProfile(FocusableDefaultProfile : GuiDefaultProfile);
new GuiControlProfile(ChatHudTextProfile)
{
    opaque = 0;
    fillColor = "255 255 255";
    fillColorHL = "128 128 128";
    border = 0;
    borderThickness = 0;
    borderColor = "40 231 240";
    fontColor = "40 231 240";
    fontColorHL = "40 231 240";
    fontColorNA = "128 128 128";
    textOffset = "0 0";
    autoSizeWidth = 1;
    autoSizeHeight = 1;
    tab = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(ChatHudMessageProfile)
{
    fontType = "Arial";
    fontSize = 16;
    fontColor = "44 172 181";
    fontColors[1] = "4 235 105";
    fontColors[2] = "219 200 128";
    fontColors[3] = "77 253 95";
    fontColors[4] = "40 231 240";
    fontColors[5] = "200 200 50 200";
    autoSizeWidth = 1;
    autoSizeHeight = 1;
};
new GuiControlProfile(ChatHudScrollProfile)
{
    opaque = 0;
    border = 0;
    borderColor = "0 255 0";
    bitmap = "common/ui/darkScroll";
    hasBitmapArray = 1;
};
new GuiControlProfile(HudScrollProfile)
{
    opaque = 0;
    border = 1;
    borderColor = "0 255 0";
    bitmap = "common/ui/darkScroll";
    hasBitmapArray = 1;
};
new GuiControlProfile(HudTextProfile)
{
    opaque = 0;
    border = 1;
    borderColor = "0 255 0";
    fillColor = "128 128 128";
    fontColor = "0 255 0";
};
new GuiControlProfile(HudBorderProfile)
{
    opaque = 0;
    border = 0;
};
new GuiControlProfile(ToolTipProfile)
{
    opaque = 1;
    border = 1;
    borderColor = "255 255 255  90";
    fillColor = "  0   0   0 210";
    modal = 0;
};
new GuiControlProfile(DragAndDropProfile : ToolTipProfile)
{
    borderColor = "255 255 255  45";
    fillColor = "  0   0   0 120";
};
new GuiControlProfile(ToolTipTextProfile : GuiTextProfile)
{
    fontColor = "255 255 255 255";
    modal = 0;
};
new GuiControlProfile(ETSWhiteProfile)
{
    opaque = 1;
    fillColor = "255 255 255 255";
};
new GuiControlProfile(ETSTranslucentProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = "255 255 255 160";
};
new GuiControlProfile(BlankProfile : GuiDefaultProfile)
{
    opaque = 0;
    fillColor = "0 0 0 0";
};
new GuiControlProfile(ETSRespektLevelPBProfile : GuiDefaultProfile)
{
    opaque = 0;
    border = 1;
    borderColor = "200 200 200";
    fillColor = "0 0 0 0";
};
new GuiControlProfile(EtsThumbCoverProfile : ETSTranslucentProfile)
{
    modal = 0;
    fillColor = "0 40 0 30";
};
new GuiControlProfile(ETSTextProfile : GuiTextProfile)
{
    fontColor = "255 255 255";
    fontColorLink = "255 255 255";
    fontColorLinkHL = "255   0 153";
};
new GuiControlProfile(ETSBoldTextProfile : ETSTextProfile);
new GuiControlProfile(ETSHiBoldTextProfile : ETSBoldTextProfile);
new GuiControlProfile(ETSTextListProfile)
{
    fontType = "Arial";
    fontSize = 15;
    opaque = 1;
    fillColor = "  0   0   0   0";
    fillColorHL = "  0   0   0   0";
    fillColorNA = " 50  50  50";
    fontColor = "200 200 200";
    fontColorHL = "255   0 131";
    fontColorLinkHL = $HighlightColor;
    drawShadow = 1;
    mouseOverSelected = 1;
};
new GuiControlProfile(ETSSmallTextListProfile : ETSTextListProfile);
new GuiControlProfile(ETSSmallTextNonModalListProfile : ETSTextListProfile);
new GuiControlProfile(ETSTinyTextListProfile : ETSSmallTextListProfile);
new GuiControlProfile(InfoWindowTextListProfile)
{
    fontType = "Arial";
    fontSize = 14;
    opaque = 1;
    fillColor = "  0   0   0   0";
    fillColorHL = "  0   0   0   0";
    fillColorNA = " 50  50  50";
    fontColor = "255 255 255";
    fontColorLink = "255 255 255";
    fontColorHL = "255   0 153";
    fontColors[6] = "255   0 153";
    drawShadow = 0;
    mouseOverSelected = 0;
};
new GuiControlProfile(AIMTextListProfile : ETSTextListProfile)
{
    fontColors[5] = "100 100 100 160";
    fontColors[6] = "102 153 255 204";
    fontColors[7] = "255 255 255 160";
    fontColors[8] = "255 255 255 160";
};
new GuiControlProfile(ConvBubProfile)
{
    opaque = 1;
    border = 1;
    borderColor = "  0   0   0 255";
    fillColor = "255 255 255 180";
    borderColorNA = "  0   0   0 255";
    hasBitmapArray = 0;
    tailAgeMin = 0;
    tailAgeHld = 14;
    tailAgeMax = 25;
    tailLenMin = -1;
    tailLenHld = 18;
    tailLenMax = 100;
};
new GuiControlProfile(ConvBubSpookyProfile : ConvBubProfile)
{
    borderColor = "150   0   0 255";
    borderColorNA = "255   0   0 255";
};
new GuiControlProfile(ConvBubFadedProfile : ConvBubProfile)
{
    borderColor = "  0   0   0 200";
    fillColor = "220 255 200  90";
    borderColorNA = "  0   0   0 200";
    tailLenMin = 5;
    tailLenHld = 5;
    tailLenMax = 50;
};
new GuiControlProfile(EavesdropBubProfile : ConvBubProfile)
{
    borderColor = "200 200 200 150";
    fillColor = "220 170 170 140";
    borderColorNA = "250 230 230  60";
    tailLenMin = 5;
    tailLenHld = 5;
    tailLenMax = 40;
};
new GuiControlProfile(ConvBubGreenProfile : ConvBubProfile);
new GuiControlProfile(ConvBubRedOutlineProfile : ConvBubProfile)
{
    borderColor = "250  10   0 255";
    fillColor = "  0   0   0   0";
};
new GuiControlProfile(SystemMessageDialogProfile : ConvBubProfile)
{
    borderColor = "200   0   0 255";
    fillColor = "  0   0   0 230";
    fontColorLink = "  0   0 255";
    fontColorLinkHL = "255   0 153";
};
new GuiControlProfile(SystemMessageTextProfile)
{
    fontColor = "200 255 150";
    fontColorLink = "200 200 255";
    fontColorLinkHL = " 50  50 255";
    canKeyFocus = 1;
    drawShadow = 1;
};
new GuiControlProfile(InfoMessageDialogProfile : ConvBubProfile)
{
    borderColor = "  0   0   0 255";
    fillColor = "255 255 255 230";
    borderColorNA = "230 240 255 255";
};
new GuiControlProfile(InfoMessageTextProfile : SystemMessageTextProfile)
{
    fontColor = "255 255 255";
    fontColorLink = "  0   0  70";
    fontColorLinkHL = "  0  60  90";
    fontColorLink = "  0   0 255";
    fontColorLinkHL = "255   0 153";
};
new GuiControlProfile(SnoopMessageTextProfile : SystemMessageTextProfile);
new GuiControlProfile(MessageHudEditProfile : GuiTextEditProfile)
{
    fontType = "SF Cartoonist Hand Bold";
    fontSize = 22;
    opaque = 0;
    border = 0;
    borderColor = "120 120 120 200";
    borderColorHL = "255 255 255 200";
    fillColor = "210 220 220 180";
    hasBitmapArray = 0;
};
new GuiControlProfile(ConvBubMessageProfile)
{
    fontType = "SF Cartoonist Hand Bold";
    fontSize = 22;
    fontColor = "0 0 0 255";
    fontColors[1] = "4 235 105";
    fontColors[2] = "219 200 128";
    fontColors[3] = "77 253 95";
    fontColors[4] = "40 231 240";
    fontColors[5] = "200 200 50 200";
    fontColorLink = "  0   0 255";
    fontColorLinkHL = "255   0 153";
    fontColorHL = "  4 235 105";
    fillColorHL = "100 100 100 255";
    fontColors[6] = " 40   0 230 255";
    fontColors[7] = " 70   0 160 255";
    fontColors[8] = "  0   0   0 128";
    autoSizeWidth = 1;
    autoSizeHeight = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(FocusableWindowProfile : GuiWindowProfile);
new GuiControlProfile(OpaqueFocusableWindowProfile : FocusableWindowProfile);
new GuiControlProfile(LessOpaqueFocusableWindowProfile : FocusableWindowProfile);
new GuiControlProfile(TransparentFocusableWindowProfile : FocusableWindowProfile)
{
    fillColor = "0 0 0 0";
    opaque = 0;
};
new GuiControlProfile(HiliteFrameProfile : GuiWindowProfile)
{
    fillColor = "0 0 0 0";
    bitmap = "./ui/hiliteFrame";
    modal = 0;
};
new GuiControlProfile(ETSWindowProfile : GuiWindowProfile)
{
    opaque = 0;
    border = 2;
    fillColor = "  0   0   0   0";
    fontColor = "200 200 200";
    fontColorHL = "120 120 120";
    text = "";
    bitmap = "./ui/etsWindow";
    textOffset = "2 1";
    hasBitmapArray = 1;
    justify = "left";
    canKeyFocus = 1;
};
new GuiControlProfile(ETSDarkWindowProfile : ETSWindowProfile)
{
    bitmap = "./ui/etsDarkWindow";
    opaque = 1;
    fillColor = $WindowBackgroundDk;
    stretchBitmaps = 0;
    textOffset = "2 2";
};
new GuiControlProfile(ETSLightHighlightProfile : ETSDarkWindowProfile)
{
    fillColor = "100 255 150 40";
    modal = 0;
    border = 0;
};
new GuiControlProfile(ETSScrollProfile);
new GuiControlProfile(ETSInviteMessageScrollProfile : ETSScrollProfile)
{
    border = 1;
    borderThickness = 1;
    borderColor = "200 200 200";
    borderColorHL = "255 255 255";
};
new GuiControlProfile(ETSAimMessageScrollProfile : ETSScrollProfile);
new GuiControlProfile(ETSAimConvContainerProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = $WindowBackgroundDk;
};
new GuiControlProfile(ETSHiScrollProfile);
new GuiControlProfile(ETSScrollSmallProfile);
new GuiControlProfile(ETSScrollDarkProfile);
new GuiControlProfile(ETSScrollDimProfile);
new GuiControlProfile(ETSBorderedScrollProfile : ETSScrollProfile)
{
    opaque = 1;
    border = 1;
    borderColor = "255 255 255 255";
    fillColor = "238 238 238 160";
};
new GuiControlProfile(ETSTabProfile)
{
    opaque = 1;
    border = 0;
    borderColor = "255 255 255 255";
    fillColor = "  0   0   0   0";
};
new GuiControlProfile(ETSAIMTabProfile : ETSTabProfile)
{
    border = 0;
    opaque = 1;
    fillColor = "255 255 255 50";
};
new GuiControlProfile(ETSCheckBoxProfile : GuiCheckBoxProfile)
{
    fontColor = "255 255 255 255";
    fontColorHL = $HighlightColor;
    fontColorNA = " 90  90  90 255";
    bitmap = "./ui/etsCheck";
    drawShadow = 1;
};
new GuiControlProfile(ETSCheckBoxProfile2 : GuiCheckBoxProfile)
{
    fontColor = "0 0 0 255";
    fontColorHL = $HighlightColor;
    fontColorNA = " 90  90  90 255";
    bitmap = "./ui/etsCheck";
};
new GuiControlProfile(ETSSliderProfile : GuiSliderProfile)
{
    fillColor = "200 200 200 120";
    bitmap = "./ui/etsScroll";
};
new GuiControlProfile(AIMTextEditProfile)
{
    opaque = 0;
    border = 0;
    fillColor = "  0   0   0   0";
    fillColorHL = "  0   0   0 120";
    fontColor = "190 190 238";
    fontColorHL = "255 255 255";
    fontColorNA = "128 128 128";
    fontType = "Lucida Console";
    fontSize = 12;
    cursorColor = "190 190 238";
    drawShadow = 1;
    returnTab = 0;
    textOffset = "0 2";
    autoSizeWidth = 0;
    autoSizeHeight = 1;
    tab = 1;
    canKeyFocus = 1;
    text = "";
};
new GuiControlProfile(ETSPopUpMenuProfile : AIMTextEditProfile)
{
    opaque = 1;
    border = 4;
    borderThickness = 1;
    textOffset = "2 0";
    justify = "left";
    fontColor = "100 0 0 255";
    fontColorHL = "255 255 255";
    fontColorSEL = "255 255 255";
    fontColorNA = "128 128 128";
    fillColor = "200 200 200 200";
    drawShadow = 0;
    bitmap = "./ui/dottedScroll";
    tab = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(ETSDarkTextEditProfile : AIMTextEditProfile)
{
    border = 1;
    borderThickness = 1;
    borderColor = "200 200 200";
    borderColorHL = "255 255 255";
    fillColor = "0 0 0 0";
    fillColorHL = "80 128 128 150";
    fontColor = "200 200 200 255";
    fontColorHL = $HighlightColor;
    drawShadow = 1;
};
new GuiControlProfile(ETSDarkReadonlyTextEditProfile : ETSDarkTextEditProfile)
{
    borderColor = "100 100 100";
    fillColor = "80 80 80 150";
    fillColorHL = "80 128 128 150";
    fontColor = "160 160 160 255";
};
new GuiControlProfile(ETSDarkTabbableTextEditProfile : ETSDarkTextEditProfile);
new GuiControlProfile(ETSDarkBorderlessTextEditProfile : ETSDarkTextEditProfile);
new GuiControlProfile(ETSDarkModelessTextProfile : ETSTextProfile)
{
    fontType = "Lucida Console";
    fontSize = 12;
    fillColor = "0 0 0 0";
    fontColor = "200 200 200 255";
    fontColorHL = $HighlightColor;
    modal = 0;
};
new GuiControlProfile(ETSDarkPopUpMenuProfile : ETSDarkTextEditProfile)
{
    opaque = 1;
    border = 4;
    borderThickness = 1;
    textOffset = "0 0";
    justify = "left";
    fillColor = "  0   0   0 180";
    fontSize = 14;
    fontType = "Arial";
    fontColorSEL = "255   0 153 255";
    bitmap = "./ui/etsScroll";
    tab = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(ETSAIMMessageProfile)
{
    fontType = "Lucida Console";
    fontSize = 12;
    fontColor = "200 200 200";
    fontColors[1] = "  4 235 105";
    fontColors[2] = "219 200 128";
    fontColors[3] = " 77 253  95";
    fontColors[4] = " 40 231 240";
    fontColors[5] = "200 200  50 200";
    fontColorLink = "  0   0 255";
    fontColorLinkHL = "255   0 153";
    fontColorHL = "  4 235 105";
    fillColorHL = "  0   0   0 120";
    fontColors[6] = "238 143 238";
    fontColors[7] = "190 190 238";
    drawShadow = 1;
    autoSizeWidth = 1;
    autoSizeHeight = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(GUIWhatsThisMenuProfile)
{
    opaque = 1;
    border = 2;
    borderWidth = 1;
    borderColor = "204 204 204 223";
    borderColorHL = "153 153 153 223";
    borderColorNA = "204 204 204 223";
    fillColor = "228 228 228 223";
    fontColor = "  0   0   0 255";
    fontColorHL = "255   0 153 255";
    fontColorSEL = "255   0 153 255";
    textOffset = "6 6";
    justify = "center";
    bitmap = "./ui/etsScroll";
    tab = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(ETSLoginEditProfile : GuiTextEditProfile)
{
    fontType = "Arial";
    fontSize = 15;
    fontColor = "255 255 255";
    fillColor = "255 255 255   0";
    fillColorHL = "100 150 200 200";
    cursorColor = "255 255 255 128";
    border = 0;
};
new GuiControlProfile(ETSLoginNoEditProfile : ETSLoginEditProfile)
{
    fontColor = "204 204 204";
    canKeyFocus = 0;
};
new GuiControlProfile(ETSLoginSmallEditProfile : ETSLoginEditProfile);
new GuiControlProfile(ETSLoginSmallNoEditProfile : ETSLoginSmallEditProfile)
{
    fontColor = "204 204 204";
    canKeyFocus = 0;
};
new GuiControlProfile(ETSLoginSeparatorProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = "150 200 220";
};
new GuiControlProfile(ETSLoginTextProfile : GuiTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 15;
    fontColor = "200 200 200 255";
};
new GuiControlProfile(ETSLoginSmallTextProfile : ETSLoginTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
};
new GuiControlProfile(ETSLoginSmallGrayTextProfile : ETSLoginSmallTextProfile);
new GuiControlProfile(ETSLoginSmallCheckBoxProfile : GuiCheckBoxProfile)
{
    fontColor = "200 200 200";
    fontColorHL = $HighlightColorLt;
    fontColorNA = "204 204 204";
    bitmap = "./ui/dottedCheckLight";
};
new GuiControlProfile(ETSLoginMLTextProfile : GuiMLTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = $HighlightColorLight;
    fontColorLink = $HighlightColorLight;
    fontColorLinkHL = $HighlightColorLightDk;
};
new GuiControlProfile(ETSVenueNameMLTextProfile : GuiMLTextProfile)
{
    modal = 0;
    fontType = "Verdana Bold";
    fontSize = 16;
    fontColor = "255 255 255 255";
    fontColorLink = " 91  91  91 255";
    fontColorLinkHL = $HighlightColorDk;
    justify = "center";
};
new GuiControlProfile(ETSTextEditProfile : GuiTextProfile)
{
    border = 1;
    borderWidth = 1;
    fillColor = "210 220 220 180";
};
new GuiControlProfile(ETSMLTextProfile : GuiMLTextProfile)
{
    fontColorLink = " 20  10  60";
    fontColorLinkHL = "120 110 255";
};
new GuiControlProfile(ETSShadowTextProfile : ETSTextProfile);
new GuiControlProfile(ETSShadowTextNonModalProfile : ETSShadowTextProfile)
{
    canKeyFocus = 0;
    modal = 0;
};
new GuiControlProfile(ETSRightJustifiedShadowTextProfile : ETSShadowTextProfile);
new GuiControlProfile(ThumbnailTextProfile : ETSTextProfile)
{
    fontColor = "255 255 255";
    drawShadow = 1;
    modal = 0;
};
new GuiControlProfile(ThumbnailSelectedProfile : GuiDefaultProfile)
{
    modal = 0;
    border = 4;
    borderColor = "255  0 0 150";
};
new GuiControlProfile(ETSAIMTextProfile : GuiTextProfile);
new GuiControlProfile(ETSAIMSelectedProfile : GuiTextProfile)
{
    fontColor = "255 255 255 255";
    fontColors[6] = "180 180 180 255";
    fontColors[7] = "255 255 255 255";
    fontColors[8] = " 90  90  90 255";
    fontType = "Lucida Console";
    fontSize = 12;
    drawShadow = 1;
};
new GuiControlProfile(ETSAIMDeselectedProfile : GuiTextProfile)
{
    fontColor = "255 255 255 255";
    fontColors[6] = "180 180 180 255";
    fontColors[7] = "255 255 255 255";
    fontColors[8] = " 90  90  90 255";
    fontType = "Lucida Console";
    fontSize = 10;
    drawShadow = 1;
};
new GuiControlProfile(H1Profile : GuiMLTextProfile)
{
    fontColor = "255 255 255";
    fontColorLink = "255 255 255";
    fontColorLinkHL = "255   0 153";
    fontSize = 20;
    fontType = "Trebuchet MS Bold";
    drawShadow = 1;
    modal = 0;
};
new GuiControlProfile(H2Profile : H1Profile);
new GuiControlProfile(H3Profile : H1Profile);
new GuiControlProfile(InfoTextProfile : GuiMLTextProfile)
{
    fontColor = $InfoTextColor;
    fontColorLink = $HighlightColor;
    fontColorLinkHL = $HighlightColorDk;
};
new GuiControlProfile(InfoTextProfileNonModal : InfoTextProfile)
{
    modal = 0;
    canKeyFocus = 0;
    canHilite = 0;
};
new GuiControlProfile(InfoTextSmallProfile : InfoTextProfile);
new GuiControlProfile(MusicMLTextProfile : H1Profile)
{
    lineSpacing = -2;
    modal = 1;
};
new GuiControlProfile(MusicMLTextProfileMedium : MusicMLTextProfile)
{
    fontSize = 18;
    lineSpacing = -1;
};
new GuiControlProfile(MusicMLTextProfileSmall : MusicMLTextProfile)
{
    fontSize = 16;
    lineSpacing = 0;
};
new GuiControlProfile(MusicRatingTextProfile : ETSShadowTextProfile);
new GuiControlProfile(ClosetTabButtonProfile : GuiButtonProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = " 50  50  50 255";
    fontColors[6] = $HighlightColorLt;
    fontColors[7] = $HighlightColorDk;
    fontColors[8] = $HighlightColor;
};
new GuiControlProfile(ClosetPriceLabelProfile : GuiTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = $ClosetHiliteDk;
};
new GuiControlProfile(ClosetPriceLabelSelectedProfile : ClosetPriceLabelProfile);
new GuiControlProfile(ClosetTitleProfile : GuiTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $ClosetHiliteDk;
};
new GuiControlProfile(ClosetLargeTitleProfile : ClosetTitleProfile)
{
    fontSize = 18;
    fontColor = $ClosetHiliteDk;
};
new GuiControlProfile(ClosetLeftInfoProfile : GuiTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = $ClosetHiliteDk;
};
new GuiControlProfile(ClosetRightInfoProfile : ClosetLeftInfoProfile);
new GuiControlProfile(ClosetSmallInfoProfile : ClosetLeftInfoProfile)
{
    fontType = "Arial Bold";
    fontSize = 13;
    modal = 0;
};
new GuiControlProfile(ClosetHighlightProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = "178 178 178 255";
};
new GuiControlProfile(ClosetHangerButtonProfile : GuiButtonProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = "102 102 102 255";
    fontColors[6] = "  0   0   0 255";
    fontColors[7] = "  0   0   0 255";
    fontColors[8] = "  0   0   0 255";
    textOffset = "10 -9";
    justify = "center";
    acceptMouseDragAndDrop = 1;
};
new GuiControlProfile(ClosetFrameButtonProfile : GuiButtonProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = $ClosetHiliteDk;
    fontColors[6] = $ClosetHiliteDk;
    fontColors[7] = "255 255 255 255";
    fontColors[8] = "153 153 153 255";
    textOffset = "-12 41";
    justify = "right";
};
new GuiControlProfile(ClosetFrameSelectedButtonProfile : ClosetFrameButtonProfile)
{
    fontColor = "255 255 255 255";
    fontColors[6] = "255 255 255 255";
    fontColors[7] = $ClosetHiliteDk;
    fontColors[8] = "153 153 153 255";
};
new GuiControlProfile(ClosetBuxProfile : ETSMLTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $VBuxFontColor;
    fontColorLink = $VBuxFontColorLink;
    fontColorLinkHL = $VBuxFontColorLinkHL;
};
new GuiControlProfile(ClosetPointsProfile : ETSMLTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $VPointsFontColor;
    fontColorLink = $VPointsFontColorLink;
    fontColorLinkHL = $VPointsFontColorLinkHL;
};
new GuiControlProfile(ClosetAvailabilityProfile : ETSMLTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = $ClosetHiliteDk;
    modal = 0;
};
new GuiControlProfile(ClosetInStockProfile : ETSMLTextProfile)
{
    fontType = "Arial";
    fontSize = 12;
    fontColor = $ClosetHiliteDk;
    modal = 0;
};
new GuiControlProfile(ClosetMediumLinkProfile : ETSMLTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $ClosetHiliteDk;
    fontColorLink = $ClosetHiliteDk;
    fontColorLinkHL = $HighlightColorLt;
};
new GuiControlProfile(ClosetLargeLinkProfile : ClosetMediumLinkProfile);
new GuiControlProfile(ClosetSmallLinkProfile : ClosetMediumLinkProfile);
new GuiControlProfile(ClosetLtBackgroundProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = $ClosetHiliteBackgroundLt;
    border = 1;
    borderColor = "  0   0   0  30";
};
new GuiControlProfile(ClosetDkBackgroundProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = $ClosetHiliteBackgroundDk;
    border = 1;
    borderColor = "  0   0   0  30";
};
new GuiControlProfile(ClosetHiliteProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = " 90  90  90  60";
};
new GuiControlProfile(ClosetTinyLinkProfile : ClosetMediumLinkProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = $ClosetHiliteDk;
    fontColors[6] = $ClosetHiliteLt;
    fontColors[7] = $ClosetHiliteDk;
    fontColors[8] = "153 153 153 255";
    textOffset = "1 0";
};
new GuiControlProfile(ClosetPopupProfile : GuiDefaultProfile)
{
    opaque = 1;
    border = 0;
    borderColorHL = "170 170 170 255";
    fillColor = "228 228 228 223";
    fontColor = "  0   0   0 255";
    fontColorHL = $HighlightColorDk;
    fontColorSEL = $HighlightColor;
    fontColors[6] = "  0   0   0 255";
    fontType = "Arial";
    fontSize = 14;
    textOffset = "7 0";
    hlOffset = "0 0";
    hlInset = "1 1";
    justify = "left";
    bitmap = "./ui/dropdown";
    drawShadow = 0;
    tab = 0;
    canKeyFocus = 0;
};
new GuiControlProfile(InfoWindowPopupProfile : ClosetPopupProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255";
    fontColors[6] = "255 0 153";
    fillColor = "0 0 0 0";
    bitmap = "./ui/infowindowdropdown";
};
new GuiControlProfile(ClosetPopupWindowProfile : ETSWindowProfile)
{
    fillColor = "255 255 255 255";
    bitmap = "./ui/dropdown_win";
};
new GuiControlProfile(InfoWindowPopupWindowProfile : ClosetPopupWindowProfile)
{
    fillColor = "0 0 0 160";
    fontColor = "255 255 255";
    fontColorHL = "255 0 153";
    bitmap = "./ui/infowindowdropdown_win";
};
new GuiControlProfile(ETSRightClickProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = $WindowBackgroundDk;
    fontColor = "255 255 255 255";
    fontColorHL = $HighlightColor;
    bitmap = "./ui/right_click";
    textOffset = "7 0";
};
new GuiControlProfile(ETSRightClickWindowProfile : ETSWindowProfile)
{
    opaque = 1;
    fillColor = $WindowBackgroundDk;
    bitmap = "./ui/right_click_win";
};
new GuiControlProfile(ClosetScrollProfile : ETSScrollProfile);
new GuiControlProfile(ClosetBorderedProfile : GuiDefaultProfile)
{
    border = 1;
    borderColor = "204 204 204";
};
new GuiControlProfile(ETSProgressProfile : GuiProgressProfile)
{
    fillColor = "204 204 204";
    borderColor = "102 102 102";
};
new GuiControlProfile(ETSPlantProgressProfile : GuiProgressProfile)
{
    opaque = 0;
    fillColor = "80 255 80 60";
    border = 0;
};
new GuiControlProfile(ETSMapHudTextProfile : ETSTextProfile)
{
    fontColor = "102 102 102 255";
    fontColors[1] = "255   0 153 255";
};
new GuiControlProfile(ETSProgressTextProfile : GuiProgressTextProfile);
new GuiControlProfile(ETSBigProgressTextProfile : ETSProgressTextProfile)
{
    fontColor = "255 255 255 255";
    fontSize = 18;
    justify = "left";
};
new GuiControlProfile(WardrobeBoxProfile : GuiDefaultProfile)
{
    opaque = 1;
    border = 1;
    borderColor = "204 204 204";
    fillColor = "255 255 255";
    borderColorNA = "230 240 255 255";
    hasBitmapArray = 0;
};
new GuiControlProfile(WardrobeDarkBoxProfile : WardrobeBoxProfile)
{
    border = 0;
    fillColor = "153 153 153";
};
new GuiControlProfile(WardrobeWhiteTextProfile : ETSTextProfile);
new GuiControlProfile(WardrobeWhiteBoldTextProfile : ETSTextProfile)
{
    fontType = "Arial Bold";
    fontColor = "255 255 255";
};
new GuiControlProfile(WardrobeLtGrayTextProfile : ETSTextProfile);
new GuiControlProfile(WardrobeDkGrayTextProfile : ETSTextProfile);
new GuiControlProfile(WardrobeLinkProfile : ETSMLTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = "153 153 153";
    fontColorLink = "153 153 153";
    fontColorLinkHL = "255   0 153";
};
new GuiControlProfile(WardrobePopUpProfile)
{
    opaque = 1;
    border = 1;
    borderColor = "153 153 153";
    borderThickness = 1;
    textOffset = "0 0";
    justify = "center";
    fillColor = "255   0 153";
    fontColor = "255 255 255";
    fontColorHL = " 90  90  90";
    fontType = "Arial";
    bitmap = "./ui/etsScroll";
    tab = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(WardrobeSmallPopUpProfile : WardrobePopUpProfile)
{
    border = 0;
    borderColor = "255 255 255 0";
    borderColorHL = "255 255 255 0";
    borderThickness = 0;
    textOffset = "0 0";
    justify = "center";
    fillColor = "255 255 255";
    fontColor = "153 153 153";
    fontColorHL = "255   0 153";
    fontSize = 13;
    tab = 0;
};
new GuiControlProfile(ETSSnapshotBackgroundProfile : GuiDefaultProfile)
{
    opaque = 1;
    border = 0;
    fillColor = "255 255 255";
    hasBitmapArray = 0;
};
new GuiControlProfile(ThumbnailBoxProfile0 : WardrobeBoxProfile);
new GuiControlProfile(ThumbnailBoxProfile1 : WardrobeBoxProfile);
new GuiControlProfile(ThumbnailBoxProfile2 : WardrobeBoxProfile);
new GuiControlProfile(ThumbnailBoxProfile3 : WardrobeBoxProfile);
new GuiControlProfile(ShoppingBagItemProfile : GuiTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = $ClosetHiliteDk;
    fontColorLink = $ClosetHiliteDk;
    fontColorLinkHL = $ClosetHiliteLt;
};
new GuiControlProfile(ShoppingBagPriceProfile : ShoppingBagItemProfile);
new GuiControlProfile(ShoppingBagCheckBoxProfile : GuiCheckBoxProfile);
new GuiControlProfile(RegistrationBackgroundProfile : ETSTranslucentProfile);
new GuiControlProfile(RegistrationTextProfile : GuiTextProfile);
new GuiControlProfile(RegistrationMLTextProfile : RegistrationTextProfile)
{
    fontColor = $RegistrationTextColor;
    fontColorLink = $DarkSeaDk;
    fontColorLinkHL = $HighlightColorLightLt;
};
new GuiControlProfile(RegistrationErrorTextProfile : RegistrationTextProfile)
{
    fontType = "Arial Bold";
    fontColor = $RegistrationErrorColor;
};
new GuiControlProfile(RegistrationTextEditProfile : GuiTextEditProfile)
{
    border = 1;
    borderColor = "180 180 180 255";
    borderColorHL = "  0   0   0 255";
};
new GuiControlProfile(RegistrationMenuProfile : GUIWhatsThisMenuProfile)
{
    border = 3;
    borderColor = "  0   0   0 255";
    borderColorHL = "170 170 170 255";
};
new GuiControlProfile(RegistrationBorderedProfile : GuiDefaultProfile)
{
    border = 1;
    borderColor = "180 180 180 255";
};
new GuiControlProfile(RegistrationCheckboxProfile : GuiCheckBoxProfile)
{
    fontColor = $RegistrationTextColor;
    borderColorHL = "170 170 170 255";
    canKeyFocus = 1;
    tab = 1;
    hlOffset = "0 0";
    hlInset = "0 0";
};
new GuiControlProfile(RegistrationRadioProfile : GuiRadioProfile)
{
    borderColorHL = "170 170 170 255";
    canKeyFocus = 1;
    tab = 1;
    hlOffset = "0 0";
    hlInset = "0 0";
};
new GuiControlProfile(RegistrationTitleProfile)
{
    fontSize = 18;
    fontType = "BauhausStd-Demi";
    fontColor = $RegistrationTextColor;
};
new GuiControlProfile(RegistrationLargeTitleProfile : RegistrationTitleProfile);
new GuiControlProfile(InfoWindowRadioButtonProfile : GuiRadioProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255";
    fontColorHL = "255   0 153";
    fontColorLink = "255 255 255";
    fontColorLinkHL = "255   0 153";
    bitmap = "./ui/etsCheck";
    drawShadow = 1;
};
new GuiControlProfile(RegistrationPopupProfile : ClosetPopupProfile)
{
    tab = 1;
    canKeyFocus = 1;
};
new GuiControlProfile(ViewfinderWindowProfile : GuiWindowProfile)
{
    bitmap = "./ui/viewfinder.png";
    fillColor = "255 255 255   0";
    resizeLeftWidth = 7;
    resizeRightWidth = 7;
    resizeTopWidth = 7;
    resizeBottomWidth = 7;
    acceptRightMouse = 0;
    acceptMouseWheel = 0;
};
new GuiControlProfile(BroadcastImageViewProfile : GuiWindowProfile)
{
    bitmap = "./ui/viewfinder.png";
    fillColor = "255 255 255   0";
    modal = 0;
    acceptRightMouse = 0;
    acceptMouseWheel = 0;
};
new GuiControlProfile(BroadcastWindowProfile : ETSWindowProfile)
{
    bitmap = "./ui/camera_frame";
    opaque = 1;
    fillColor = "255 255 255   0";
    acceptRightMouse = 0;
    acceptMouseWheel = 0;
    stretchBitmaps = 0;
};
new GuiControlProfile(MLScrollInspectWindowProfile : BroadcastWindowProfile)
{
    bitmap = "./ui/darkBoxWindow";
    fillColor = $WindowBackgroundDk;
};
new GuiControlProfile(InfoWindowProfile : FocusableWindowProfile)
{
    bitmap = "./ui/darkBoxWindow";
    fillColor = $WindowBackgroundDk;
};
new GuiControlProfile(SnoopWindowProfile : FocusableWindowProfile);
new GuiControlProfile(ETSDarkBoxProfile : FocusableWindowProfile)
{
    bitmap = "./ui/darkBoxWindow";
    fillColor = $WindowBackgroundDk;
    border = 1;
    borderColor = "255 255 255 90";
};
new GuiControlProfile(ETSNotSoDarkBoxProfile : ETSDarkBoxProfile)
{
    fillColor = "  0   0   0 20";
    border = 1;
    borderColor = "255 255 255 90";
};
new GuiControlProfile(EtsDarkBorderlessBoxProfile : ETSDarkBoxProfile);
new GuiControlProfile(EtsNotQuiteSoDarkBorderlessBoxProfile : EtsDarkBorderlessBoxProfile);
new GuiControlProfile(ETSNotSoDarkNonModalBoxProfile : ETSDarkBoxProfile)
{
    fillColor = "  0   0   0 20";
    border = 1;
    borderColor = "255 255 255 90";
    modal = 0;
};
new GuiControlProfile(ETSDarkBoxNoFocusProfile : ETSDarkBoxProfile)
{
    canKeyFocus = 0;
    tab = 0;
    border = 1;
    borderColor = "255 255 255 90";
};
new GuiControlProfile(ETSDarkBoxNonModalProfile : ETSDarkBoxProfile);
new GuiControlProfile(ETSLightBoxProfile : ETSDarkBoxProfile);
new GuiControlProfile(InfoWindowTextProfile : GuiMLTextProfile)
{
    fontType = "Arial";
    fontSize = 16;
    fontColor = "255 255 255";
    fontColorLink = "255 255 255";
    fontColorLinkHL = "255   0 153";
};
new GuiControlProfile(InfoWindowNonModalTextProfile : InfoWindowTextProfile);
new GuiControlProfile(InfoWindowTextEditProfile : ETSDarkTextEditProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255";
    fontColorLink = "255 255 255";
    fontColorLinkHL = "255   0 153";
};
new GuiControlProfile(InfoWindowTextEditInactiveProfile : InfoWindowTextEditProfile)
{
    borderColor = "127 127 127 255";
    borderColorHL = "200 200 200 255";
    canKeyFocus = 0;
};
new GuiControlProfile(InfoWindowTextEditInvisibleProfile : InfoWindowTextEditProfile)
{
    opaque = 0;
    fontColor = "255 255 255 180";
    fontColorNA = " 79  91 133 150";
    fontColorHL = "255 255 255 255";
    fillColorHL = " 50  50 100 200";
    drawShadow = 0;
    cursorColor = "255 255 255 128";
    fontType = "Arial";
    fontSize = 16;
    border = 0;
};
new GuiControlProfile(InfoWindowTextEditInvisibleOnWhiteProfile : InfoWindowTextEditInvisibleProfile)
{
    fontColor = "  0   0   0 180";
    fontColorNA = " 90   0  90 150";
    fontColorHL = " 80   0   0 255";
    fillColorHL = " 50  50 100  80";
    cursorColor = "  0   0   0 180";
};
new GuiControlProfile(HudScoresLabelTextProfile : InfoWindowTextProfile)
{
    fontColorHL = "255 255 255";
    fillColorHL = "  0   0   0   0";
};
new GuiControlProfile(ETSHudHeadingProfile : GuiTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 24;
    fontColor = "255 255 255 255";
    modal = 0;
};
new GuiControlProfile(ETSMenuProfile : FocusableDefaultProfile)
{
    opaque = 1;
    border = 1;
    fillColor = "  0   0   0 156";
    borderColor = "255 255 255  84";
};
new GuiControlProfile(ETSClearMenuProfile : ETSMenuProfile)
{
    opaque = 0;
    border = 0;
};
new GuiControlProfile(ETSMenuNonModalProfile : ETSMenuProfile)
{
    modal = 0;
    canKeyFocus = 0;
    canHilite = 0;
};
new GuiControlProfile(ETSButtonProfile : GuiButtonProfile)
{
    fontColor = "255 255 255 255";
    fontColors[6] = $HighlightColorLt;
    fontColors[7] = $HighlightColorDk;
    fontColors[8] = "255 255 255  90";
    fillColor = "  0   0   0 156";
    borderColor = "255 255 255  84";
    fillColorHL = "127 127 127 180";
    borderColorHL = "255 255 255  84";
};
new GuiControlProfile(ETSVerticalButtonProfile : ETSButtonProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = $HighlightColor;
    fontColors[6] = $HighlightColorLt;
    fontColors[7] = $HighlightColorDk;
    fontColors[8] = "255 255 255 220";
    textRotation = 90;
    textOffset = "0 4";
    justify = "right";
};
new GuiControlProfile(ETSShopVerticalButtonProfile : ETSVerticalButtonProfile)
{
    fontColor = $VBuxFontColor;
    fontColors[6] = $VBuxFontColorLinkHL;
    fontColors[8] = $VBuxFontColor;
    fontColorLink = $VBuxFontColorLink;
    fontColorLinkHL = $VBuxFontColorLinkHL;
};
new GuiControlProfile(ETSSelectedMenuItemProfile : GuiDefaultProfile)
{
    opaque = 1;
    border = 1;
    fillColor = "127 127 127 180";
    borderColor = "255 255 255  84";
};
new GuiControlProfile(ETSSelectedMenuItemNoBorderProfile : ETSSelectedMenuItemProfile);
new GuiControlProfile(ETSSelectedMenuTextProfile : ETSTextProfile)
{
    modal = 0;
    fontColor = "  0   0   0 255";
};
new GuiControlProfile(ETSUnselectedMenuTextProfile : ETSTextProfile)
{
    modal = 0;
    fontColor = "255 255 255 255";
};
new GuiControlProfile(ETSWhite16TextProfile : ETSMLTextProfile)
{
    fontType = "Arial";
    fontSize = 16;
    fontColor = "255 255 255 255";
    fontColorLink = "255 255 255 255";
    fontColorLinkHL = "120 110 255 255";
};
new GuiControlProfile(ETSWhite14TextProfile : ETSMLTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255 255";
    fontColorLink = "255 255 255 255";
    fontColorLinkHL = "120 110 255 255";
};
new GuiControlProfile(ETSScrollBigThumbProfile : ETSScrollProfile);
new GuiControlProfile(DottedScrollProfile : ETSScrollProfile);
new GuiControlProfile(DottedScrollDarkProfile : DottedScrollProfile);
new GuiControlProfile(ETSServerListScrollProfile : DottedScrollProfile)
{
    opaque = 1;
    fillColor = "100   0  50 100";
};
new GuiControlProfile(DottedSliderProfile : ETSScrollProfile);
new GuiControlProfile(DottedWindowProfile : ETSWindowProfile)
{
    bitmap = "./ui/dottedWindow";
    stretchBitmaps = 0;
};
new GuiControlProfile(NonModalDottedWindowProfile : DottedWindowProfile);
new GuiControlProfile(DottedWindowLtProfile : ETSWindowProfile)
{
    opaque = 1;
    fillColor = "255 255 255 125";
    bitmap = "./ui/dottedWindowLt";
    stretchBitmaps = 0;
};
new GuiControlProfile(DottedWindowDkProfile : ETSWindowProfile)
{
    opaque = 1;
    fillColor = $WindowBackgroundDk;
    bitmap = "./ui/dottedWindowDk";
    stretchBitmaps = 0;
};
new GuiControlProfile(DottedWindowDkNonFocusProfile : DottedWindowDkProfile);
new GuiControlProfile(CornersWindowProfile : ETSWindowProfile)
{
    bitmap = "./ui/cornersWindow";
    canKeyFocus = 0;
};
new GuiControlProfile(BracketButton19Profile : GuiFocusableVWButtonProfile)
{
    bitmap = "./ui/bracketButton19";
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $HighlightColor;
    fontColors[6] = $HighlightColorLt;
    fontColors[7] = $HighlightColorDk;
    fontColors[8] = "0 0 0 128";
    hlInset = "2 2";
};
new GuiControlProfile(BracketButton19TealProfile : BracketButton19Profile);
new GuiControlProfile(BracketButton19NonDefaultProfile : BracketButton19Profile);
new GuiControlProfile(BracketButton19NonFocusProfile : BracketButton19NonDefaultProfile);
new GuiControlProfile(BracketButton17Profile : BracketButton19Profile)
{
    bitmap = "./ui/bracketButton17";
    fontSize = 15;
};
new GuiControlProfile(BracketButton17NonDefaultProfile : BracketButton17Profile);
new GuiControlProfile(BracketButton17InertProfile : BracketButton17Profile)
{
    modal = 0;
    canKeyFocus = 0;
    tab = 0;
};
new GuiControlProfile(BracketButton15Profile : BracketButton19Profile)
{
    bitmap = "./ui/bracketButton15";
    fontSize = 14;
};
new GuiControlProfile(BracketButton15RedProfile : BracketButton15Profile);
new GuiControlProfile(BracketButton18TGFProfile : BracketButton19Profile)
{
    fontType = "Arial Bold";
    fontSize = 18;
    fontColor = "238 238 238 255";
    fontColors[6] = "255 147 248 255";
    fontColors[7] = "255 147 248 255";
    fontColors[8] = $HighlightColorLightIn;
};
new GuiControlProfile(BracketButton16TGFProfile : BracketButton18TGFProfile);
new GuiControlProfile(BracketButton14TGFProfile : BracketButton18TGFProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "238 238 238 170";
};
new GuiControlProfile(BracketButtonLt19Profile : BracketButton19Profile)
{
    fontColor = $HighlightColorLight;
    fontColors[6] = $HighlightColorLightLt;
    fontColors[7] = $Algae;
    fontColors[8] = $HighlightColorLightIn;
};
new GuiControlProfile(BracketButtonLt19NonDefaultProfile : BracketButtonLt19Profile);
new GuiControlProfile(BracketButtonLt15Profile : BracketButtonLt19Profile)
{
    bitmap = "./ui/bracketButton15";
    fontSize = 14;
};
new GuiControlProfile(HiddenBracketButton15Profile : BracketButton15Profile)
{
    bitmap = "./ui/hiddenBracketButton15";
    canKeyFocus = 0;
    tab = 0;
};
new GuiControlProfile(BracketButton15NonDefaultProfile : BracketButton15Profile);
new GuiControlProfile(BracketButtonLt15NonDefaultProfile : BracketButtonLt15Profile);
new GuiControlProfile(BracketButton15NonFocusProfile : BracketButton15Profile);
new GuiControlProfile(StoreItemButtonProfile : HiddenBracketButton15Profile)
{
    fontColor = "  0   0   0 255";
    fontColors[6] = $HighlightColorLt;
    fontColors[7] = $HighlightColorDk;
    fontColors[8] = "  0   0   0 128";
    justify = "left";
    textOffset = "3 0";
};
new GuiControlProfile(StoreHiliteFrameProfile : GuiWindowProfile)
{
    opaque = 0;
    fillColor = "255 255 255   0";
    bitmap = "./ui/store_hilite_frame";
    modal = 0;
};
new GuiControlProfile(BracketButton15InertProfile : BracketButton15Profile)
{
    modal = 0;
    canKeyFocus = 0;
    tab = 0;
};
new GuiControlProfile(BracketButtonLt15InertProfile : BracketButtonLt15Profile)
{
    modal = 0;
    canKeyFocus = 0;
    tab = 0;
};
new GuiControlProfile(BracketButton13Profile : BracketButton19Profile)
{
    bitmap = "./ui/bracketButton13";
    fontSize = 13;
};
new GuiControlProfile(ETSMessageTextProfile : GuiMessageTextProfile)
{
    fontColorLink = BracketButton19Profile.fontColor;
    fontColorLinkHL = BracketButton19Profile.fontColors[6];
};
new GuiControlProfile(MapLargeLabelProfile : GuiTextProfile)
{
    fontType = "Arial";
    fontSize = 22;
    fontColor = "255 255 255 200";
    drawShadow = 1;
};
new GuiControlProfile(MapPopupProfile : ClosetPopupProfile)
{
    bitmap = "./ui/dottedDropdown";
    fontColor = "255 255 255 255";
    fontColors[1] = "255 255 255 180";
    fontColors[6] = "255 255 255 180";
};
new GuiControlProfile(MapPopupWindowProfile : ETSWindowProfile)
{
    opaque = 1;
    bitmap = "./ui/dottedDropdownWin";
    fillColor = "  0   0   0 120";
    fontColor = "255 255 255 255";
};
new GuiControlProfile(MapScrollProfile : ETSScrollProfile)
{
    opaque = 0;
    bitmap = "./ui/dottedScroll";
    fontColor = "255 255 255 255";
};
new GuiControlProfile(SnoopButtonProfile : BracketButton15Profile)
{
    fontColor = "200 200 200 255";
    fontColorHL = "255   0 153 255";
    fontColorNA = " 90  90  90 255";
    drawShadow = 1;
};
new GuiControlProfile(LoginPopupProfile : ClosetPopupProfile);
new GuiControlProfile(LoginPopupWindowProfile : ETSWindowProfile)
{
    opaque = 1;
    bitmap = "./ui/dottedDropdownWinLt";
    fillColor = "255 255 255 211";
    fontColor = "  0   0   0 255";
    stretchBitmaps = 0;
};
new GuiControlProfile(LoginScrollProfile : ETSScrollProfile)
{
    opaque = 0;
    bitmap = "./ui/dottedScroll";
    fontColor = "  0   0   0 255";
};
new GuiControlProfile(VPointsButtonProfile : GuiButtonProfile)
{
    bitmap = "./ui/vpoints_frame";
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $HighlightColor;
    fontColors[6] = $HighlightColorLt;
    fontColors[7] = $HighlightColorDk;
    fontColors[8] = "  0   0   0 128";
    hlInset = "2 2";
};
new GuiControlProfile(VPointsTextProfile : ETSTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $VPointsFontColor;
    fontColorLink = $VPointsFontColorLink;
    fontColorLinkHL = $VPointsFontColorLinkHL;
    justify = "right";
    modal = 0;
};
new GuiControlProfile(VBuxButtonProfile : VPointsButtonProfile)
{
    bitmap = "./ui/vbux_frame";
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $HighlightColor;
    fontColors[6] = $HighlightColorLt;
    fontColors[7] = $HighlightColorDk;
    fontColors[8] = "  0   0   0 128";
    hlInset = "2 2";
};
new GuiControlProfile(VBuxTextProfile : ETSTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = $VBuxFontColor;
    fontColorLink = $VBuxFontColorLink;
    fontColorLinkHL = $VBuxFontColorLinkHL;
    justify = "right";
    modal = 0;
};
new GuiControlProfile(GuiDragNZoomProfile : GuiDefaultProfile);
new GuiControlProfile(TGFBigWindowProfile : ETSWindowProfile)
{
    opaque = 1;
    fillColor = "0 0 0 0";
    canKeyFocus = 0;
    bitmap = "./ui/dottedDropdownWin";
};
new GuiControlProfile(TGFSmallWindowProfile : TGFBigWindowProfile)
{
    bitmap = "./ui/right_click_win";
    canKeyFocus = 1;
};
new GuiControlProfile(GuiTableProfile : GuiDefaultProfile)
{
    modal = 1;
    canKeyFocus = 1;
    tab = 1;
    opaque = 0;
    border = 0;
};
new GuiControlProfile(GuiTableHeaderRowProfile : ETSNonModalProfile)
{
    opaque = 0;
    border = 0;
};
new GuiControlProfile(GuiTableHeaderCell_N_Profile : ETSNonModalProfile)
{
    opaque = 0;
    border = 0;
};
new GuiControlProfile(GuiTableHeaderCell_H_Profile : GuiTableHeaderCell_N_Profile)
{
    opaque = 1;
    border = 1;
    borderColor = "200 200 255 150";
    fillColor = "230 230 255  80";
};
new GuiControlProfile(GuiTableHeaderCell_D_Profile : GuiTableHeaderCell_N_Profile)
{
    opaque = 1;
    border = 1;
    opaque = 1;
    border = 1;
    borderColor = "200 150 255 150";
    fillColor = "230 180 255  80";
};
new GuiControlProfile(GuiTableHeaderCellMLTextProfile : GuiMLTextModelessProfile)
{
    opaque = 0;
    fillColor = "0 0 0 0";
    border = 0;
    fontType = "Arial Bold";
    fontSize = 20;
    fontColor = "0 185 255 255";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
    fontColorSEL = "200 200 200";
    drawShadow = 0;
};
new GuiControlProfile(GuiTableHeaderCellButtonProfile : GuiButtonProfile)
{
    opaque = 0;
    fillColor = "0 0 0 0";
    border = 0;
};
new GuiControlProfile(GuiTableBodyRowHilitedProfile : GuiDefaultProfile)
{
    opaque = 1;
    fillColor = "188 33 168 80";
    border = 0;
};
new GuiControlProfile(GuiTableBodyRowUnhilitedProfile : GuiDefaultProfile)
{
    opaque = 0;
    border = 0;
};
new GuiControlProfile(GuiTableBodyRowHoverHilitedProfile : GuiTableBodyRowHilitedProfile)
{
    border = 1;
    borderColor = "200 200 255 80";
};
new GuiControlProfile(GuiTableBodyRowHoverUnhilitedProfile : GuiTableBodyRowUnhilitedProfile)
{
    border = 1;
    borderColor = "200 200 255 80";
};
new GuiControlProfile(GuiTableBodyCellProfile : ETSNonModalProfile)
{
    opaque = 0;
    border = 0;
};
new GuiControlProfile(GuiTableBodyCellMLTextProfile : GuiMLTextModelessProfile)
{
    opaque = 0;
    border = 0;
    fontType = "Arial";
    fontSize = 16;
    fontColor = "255 255 255 255";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
    fontColorSEL = "200 200 200";
    drawShadow = 0;
};
new GuiControlProfile(GuiTableBodyCellBitmapProfile : ETSNonModalProfile);
new GuiControlProfile(GuiTableScrollProfile : DottedScrollProfile);
if (!isObject(ClipboardProfile))
{
}
new GuiControlProfile(ClipboardProfile : GuiDefaultProfile)
    {
        opaque = 1;
        border = 1;
        fillColor = "  2  12  18 255";
        borderColor = $WindowBorderColor;
    };
if (!isObject(ClipboardTabButtonProfile))
{
}
new GuiControlProfile(ClipboardTabButtonProfile : ClosetTabButtonProfile)
    {
        fontType = "Arial";
        fontSize = 15;
        fontColor = "255 255 255 120";
        fontColors[6] = $HighlightColorLt;
        fontColors[7] = $HighlightColorDk;
        fontColors[8] = $HighlightColor;
    };
if (!isObject(ClipboardHeaderCellProfile))
{
}
new GuiControlProfile(ClipboardHeaderCellProfile : GuiTableHeaderCell_N_Profile);
if (!isObject(ClipboardHeaderCellButtonProfile))
{
}
new GuiControlProfile(ClipboardHeaderCellButtonProfile : GuiTableHeaderCellButtonProfile)
    {
        border = 1;
        borderColor = "255 255 255  90";
    };
if (!isObject(ClipboardHeaderMLTextProfile))
{
}
new GuiControlProfile(ClipboardHeaderMLTextProfile : GuiTableHeaderCellMLTextProfile)
    {
        fontType = "Arial";
        fontSize = 14;
        fontColor = "255 255 255 120";
        fontColors[6] = $HighlightColorLt;
        fontColors[7] = $HighlightColorDk;
        fontColors[8] = $HighlightColor;
    };
if (!isObject(ClipboardTextProfile))
{
}
new GuiControlProfile(ClipboardTextProfile : ETSTextProfile)
    {
        fontType = "Arial";
        fontSize = 14;
    };
new GuiControlProfile(CSProfileModelListingsHeaderBox : ETSMenuProfile)
{
    opaque = 1;
    border = 0;
    fillColor = "170 255 255 255";
};
new GuiControlProfile(CSProfileFeaturedListingsHeaderBox : ETSMenuProfile)
{
    opaque = 1;
    border = 0;
    fillColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileCelebListingsHeaderBox : ETSMenuProfile)
{
    opaque = 1;
    border = 0;
    fillColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileNormalListingsHeaderBox : ETSMenuProfile)
{
    opaque = 1;
    border = 0;
    fillColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileListBox : ETSMenuProfile)
{
    border = 0;
    canKeyFocus = 0;
    modal = 0;
    opaque = 0;
};
new GuiControlProfile(CSProfileModelListingUnselected : GuiDefaultProfile);
new GuiControlProfile(CSProfileFeaturedListingUnselected : GuiDefaultProfile);
new GuiControlProfile(CSProfileCelebListingUnselected : GuiDefaultProfile);
new GuiControlProfile(CSProfileNormalListingUnselected : GuiDefaultProfile);
new GuiControlProfile(CSProfileFriendListingUnselected : GuiDefaultProfile);
new GuiControlProfile(CSProfileModelListingSelected : ETSSelectedMenuItemProfile);
new GuiControlProfile(CSProfileFeaturedListingSelected : ETSSelectedMenuItemProfile);
new GuiControlProfile(CSProfileCelebListingSelected : ETSSelectedMenuItemProfile);
new GuiControlProfile(CSProfileNormalListingSelected : ETSSelectedMenuItemProfile);
new GuiControlProfile(CSProfileFriendListingSelected : ETSSelectedMenuItemProfile);
new GuiControlProfile(CSProfileModelListingMenuText : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "170 255 255 255";
};
new GuiControlProfile(CSProfileModelListingMenuTextModal : CSProfileModelListingMenuText);
new GuiControlProfile(CSProfileFeaturedListingMenuText : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "242 255  22 255";
};
new GuiControlProfile(CSProfileFeaturedListingMenuTextModal : CSProfileFeaturedListingMenuText);
new GuiControlProfile(CSProfileCelebListingMenuText : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileCelebListingMenuTextModal : CSProfileCelebListingMenuText);
new GuiControlProfile(CSProfileNormalListingMenuText : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileNormalListingMenuTextModal : CSProfileNormalListingMenuText);
new GuiControlProfile(CSProfileFriendListingMenuText : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "128 255   0 255";
};
new GuiControlProfile(CSProfileFriendListingMenuTextModal : CSProfileFriendListingMenuText);
new GuiControlProfile(CSProfileModelListingMenuTextSelected : ETSSelectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "170 255 255 255";
};
new GuiControlProfile(CSProfileFeaturedListingMenuTextSelected : ETSSelectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "242 255  22 255";
};
new GuiControlProfile(CSProfileCelebListingMenuTextSelected : ETSSelectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileNormalListingMenuTextSelected : ETSSelectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileFriendListingMenuTextSelected : ETSSelectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "128 255   0 255";
};
new GuiControlProfile(CSProfileDescriptionTitleModel : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = "170 255 255 255";
};
new GuiControlProfile(CSProfileDescriptionTitleNormal : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 16;
    fontColor = "242 121 242 255";
};
new GuiControlProfile(CSProfileDescriptionHeaderModel : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = "170 255 255 255";
};
new GuiControlProfile(CSProfileDescriptionHeaderNormal : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial Bold";
    fontSize = 14;
    fontColor = "242 121 242 255";
};
new GuiControlProfile(CSProfileDescriptionTextModel : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255 255";
};
new GuiControlProfile(CSProfileDescriptionTextNormal : ETSUnselectedMenuTextProfile)
{
    fontType = "Arial";
    fontSize = 14;
    fontColor = "255 255 255 255";
};
new GuiControlProfile(Profile_MyShop_TextField : GuiTextEditProfile)
{
    border = 0;
    borderColor = "0 0 0 80";
    fontColors[1] = "0 0 0 255";
    fontColors[6] = "0 0 0 255";
    fillColorHL = "0 200 250 100";
};
new GuiControlProfile(Profile_MyShop_TextField_Bold : Profile_MyShop_TextField);
new GuiControlProfile(Profile_MyShop_SettingsField : Profile_MyShop_TextField);
new GuiControlProfile(Profile_Plain_White)
{
    border = 0;
    fillColor = "255 255 255 255";
    opaque = 1;
    modal = 0;
};

