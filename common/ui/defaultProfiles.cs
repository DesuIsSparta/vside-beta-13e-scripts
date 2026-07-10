$Gui::fontCacheDirectory = ExpandFilename("./cache");
$Gui::clipboardFile = ExpandFilename("./cache/clipboard.gui");
if (!(isObject())) {
    tab = GuiDefaultProfile @ new GuiControlProfile(GuiDefaultProfile) @ 0;
    canKeyFocus = 0;
    hasBitmapArray = 0;
    mouseOverSelected = 0;
    opaque = 0;
    fillColor = ($Platform $= "macos") ? "211 211 211" : "192 192 192";
    fillColorHL = ($Platform $= "macos") ? "244 244 244" : "220 220 220";
    fillColorNA = ($Platform $= "macos") ? "244 244 244" : "220 220 220";
    border = 0;
    borderColor = "0 0 0";
    borderColorHL = "128 128 128";
    borderColorNA = "64 64 64";
    fontType = "Arial";
    fontSize = 14;
    fontColor = "0 0 0";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
    fontColorSEL = "200 200 200";
    drawShadow = 0;
    bitmap = ($Platform $= "macos") ? "./osxWindow" : "./darkWindow";
    bitmapBase = "";
    textOffset = "0 0";
    hlOffset = "0 0";
    hlInset = "2 2";
    modal = 1;
    justify = "left";
    autoSizeWidth = 0;
    autoSizeHeight = 0;
    returnTab = 0;
    numbersOnly = 0;
    cursorColor = "0 0 0 255";
    acceptMouseMove = 1;
    acceptLeftMouse = 1;
    acceptRightMouse = 1;
    acceptMouseWheel = 1;
    soundButtonDown = "";
    soundButtonOver = "";
}
if (!(isObject())) {
    tab = GuiInputCtrlProfile @ new GuiControlProfile(GuiInputCtrlProfile) @ 1;
    canKeyFocus = 1;
}
if (!(isObject())) {
    new GuiControlProfile(GuiDialogProfile);
}
if (!(isObject())) {
    opaque = GuiSolidDefaultProfile @ new GuiControlProfile(GuiSolidDefaultProfile) @ 1;
    GuiDialogProfile;
    border = 0;
    fillColor = ($Platform $= "macos") ? "211 211 211" : "192 192 192";
}
if (!(isObject())) {
    opaque = GuiWindowProfile @ new GuiControlProfile(GuiWindowProfile) @ 1;
    border = 2;
    fillColor = ($Platform $= "macos") ? "211 211 211" : "192 192 192";
    fillColorHL = ($Platform $= "macos") ? "190 255 255" : "64 150 150";
    fillColorNA = ($Platform $= "macos") ? "255 255 255" : "150 150 150";
    fontColor = ($Platform $= "macos") ? "0 0 0" : "255 255 255";
    fontColorHL = ($Platform $= "macos") ? "200 200 200" : "0 0 0";
    text = "GuiWindowCtrl test";
    bitmap = ($Platform $= "macos") ? "./osxWindow" : "./darkWindow";
    textOffset = ($Platform $= "macos") ? "5 5" : "6 6";
    hasBitmapArray = 1;
    justify = ($Platform $= "macos") ? "center" : "left";
}
if (!(isObject())) {
    opaque = GuiToolWindowProfile @ new GuiControlProfile(GuiToolWindowProfile) @ 1;
    border = 2;
    fillColor = "192 192 192";
    fillColorHL = "64 150 150";
    fillColorNA = "150 150 150";
    fontColor = "255 255 255";
    fontColorHL = "0 0 0";
    bitmap = "./torqueToolWindow";
    textOffset = "6 6";
}
if (!(isObject())) {
    opaque = EditorToolButtonProfile @ new GuiControlProfile(EditorToolButtonProfile) @ 1;
    border = 2;
}
if (!(isObject())) {
    opaque = GuiContentProfile @ new GuiControlProfile(GuiContentProfile) @ 1;
    fillColor = "255 255 255";
}
if (!(isObject())) {
    modal = GuiControlProfile @ new "GuiModelessDialogProfile"() @ 0;
    0;
    GuiModelessDialogProfile;
}
if (!(isObject())) {
    opaque = GuiButtonProfile @ new GuiControlProfile(GuiButtonProfile) @ 1;
    border = 1;
    drawShadow = 0;
    fontColor = "0 0 0";
    fontColorHL = "32 100 100";
    fixedExtent = 1;
    justify = "center";
    canKeyFocus = 0;
}
if (!(isObject())) {
    fontColorHL = GuiBorderButtonProfile @ new GuiControlProfile(GuiBorderButtonProfile) @ "0 0 0";
}
if (!(isObject())) {
    opaque = GuiMenuBarProfile @ new GuiControlProfile(GuiMenuBarProfile) @ 1;
    fillColor = ($Platform $= "macos") ? "211 211 211" : "192 192 192";
    fillColorHL = "0 0 96";
    border = 4;
    fontColor = "0 0 0";
    fontColorHL = "255 255 255";
    fontColorNA = "128 128 128";
    fixedExtent = 1;
    justify = "center";
    canKeyFocus = 0;
    mouseOverSelected = 1;
    bitmap = ($Platform $= "macos") ? "./osxMenu" : "./torqueMenu";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    fontSize = GuiButtonSmProfile @ new GuiControlProfile(GuiButtonSmProfile : GuiButtonProfile) @ 14;
}
if (!(isObject())) {
    fontSize = GuiRadioProfile @ new GuiControlProfile(GuiRadioProfile) @ 14;
    fillColor = "232 232 232";
    fontColorHL = "32 100 100";
    fixedExtent = 1;
    bitmap = ($Platform $= "macos") ? "./osxRadio" : "./torqueRadio";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    opaque = GuiScrollProfile @ new GuiControlProfile(GuiScrollProfile) @ 1;
    fillColor = "255 255 255";
    border = 3;
    borderThickness = 2;
    borderColor = "0 0 0";
    bitmap = ($Platform $= "macos") ? "./osxScroll" : "./darkScroll";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    bitmap = GuiSliderProfile @ new GuiControlProfile(GuiSliderProfile) @ "./darkSlider";
}
if (!(isObject())) {
    fontColor = GuiTextProfile @ new GuiControlProfile(GuiTextProfile) @ "0 0 0";
    fontColorLink = "255 96 96";
    fontColorLinkHL = "0 0 255";
    autoSizeWidth = 1;
    autoSizeHeight = 1;
}
if (!(isObject())) {
    fontType = EditorTextProfile @ new GuiControlProfile(EditorTextProfile) @ "Arial Bold";
    fontColor = "0 0 0";
    autoSizeWidth = 1;
    autoSizeHeight = 1;
}
if (!(isObject())) {
    fontType = EditorTextProfileWhite @ new GuiControlProfile(EditorTextProfileWhite) @ "Arial Bold";
    fontColor = "255 255 255";
    autoSizeWidth = 1;
    autoSizeHeight = 1;
}
if (!(isObject())) {
    fontSize = GuiMediumTextProfile @ new GuiControlProfile(GuiMediumTextProfile : GuiTextProfile) @ 24;
}
if (!(isObject())) {
    fontSize = GuiBigTextProfile @ new GuiControlProfile(GuiBigTextProfile : GuiTextProfile) @ 36;
}
if (!(isObject())) {
    justify = GuiCenterTextProfile @ new GuiControlProfile(GuiCenterTextProfile : GuiTextProfile) @ "center";
}
if (!(isObject())) {
    canKeyFocus = MissionEditorProfile @ new GuiControlProfile(MissionEditorProfile) @ 1;
}
if (!(isObject())) {
    opaque = EditorScrollProfile @ new GuiControlProfile(EditorScrollProfile) @ 1;
    fillColor = "192 192 192 192";
    border = 3;
    borderThickness = 2;
    borderColor = "0 0 0";
    bitmap = "./darkScroll";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    opaque = GuiTextEditProfile @ new GuiControlProfile(GuiTextEditProfile) @ 1;
    fillColor = "255 255 255";
    fillColorHL = "128 128 128";
    border = 3;
    borderThickness = 2;
    borderColor = "0 0 0";
    fontColor = "0 0 0";
    fontColorHL = "255 255 255";
    fontColorNA = "128 128 128";
    textOffset = "0 2";
    autoSizeWidth = 0;
    autoSizeHeight = 1;
    tab = 1;
    canKeyFocus = 1;
    drawShadow = 0;
}
if (!(isObject())) {
    opaque = GuiControlListPopupProfile @ new GuiControlProfile(GuiControlListPopupProfile) @ 1;
    fillColor = "255 255 255";
    fillColorHL = "128 128 128";
    border = 1;
    borderColor = "0 0 0";
    fontColor = "0 0 0";
    fontColorHL = "255 255 255";
    fontColorNA = "128 128 128";
    textOffset = "0 2";
    autoSizeWidth = 0;
    autoSizeHeight = 1;
    tab = 1;
    canKeyFocus = 1;
    bitmap = ($Platform $= "macos") ? "./osxScroll" : "./darkScroll";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    fontColorHL = GuiTextArrayProfile @ new GuiControlProfile(GuiTextArrayProfile : GuiTextProfile) @ "32 100 100";
    fillColorHL = "200 200 200";
}
if (!(isObject())) {
    new GuiControlProfile(GuiTextListProfile : GuiTextProfile);
}
if (!(isObject())) {
    fontSize = GuiTreeViewProfile @ new GuiControlProfile(GuiTreeViewProfile) @ 13;
    GuiTextListProfile;
    fontColor = "0 0 0";
    fontColorHL = "64 150 150";
    canKeyFocus = 1;
    autoSizeHeight = 1;
    fontColorSEL = "250 250 250";
    fillColorHL = "0 60 150";
    fontColorNA = "240 240 240";
    bitmap = "./shll_treeView";
}
if (!(isObject())) {
    opaque = GuiCheckBoxProfile @ new GuiControlProfile(GuiCheckBoxProfile) @ 0;
    fillColor = "232 232 232";
    border = 0;
    borderColor = "0 0 0";
    fontSize = 14;
    fontColor = "0 0 0";
    fontColorHL = "32 100 100";
    fixedExtent = 1;
    justify = "left";
    bitmap = ($Platform $= "macos") ? "./osxCheck" : "./torqueCheck";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    opaque = GuiPopUpMenuProfile @ new GuiControlProfile(GuiPopUpMenuProfile) @ 1;
    mouseOverSelected = 1;
    border = 4;
    borderThickness = 2;
    borderColor = "0 0 0";
    fontSize = 14;
    fontColor = "0 0 0";
    fontColorHL = "32 100 100";
    fontColorSEL = "32 100 100";
    fixedExtent = 1;
    justify = "center";
    bitmap = ($Platform $= "macos") ? "./osxScroll" : "./darkScroll";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    opaque = GuiEditorClassProfile @ new GuiControlProfile(GuiEditorClassProfile) @ 1;
    fillColor = "232 232 232";
    border = 1;
    borderColor = "0 0 0";
    borderColorHL = "127 127 127";
    fontColor = "0 0 0";
    fontColorHL = "32 100 100";
    fixedExtent = 1;
    justify = "center";
    bitmap = ($Platform $= "macos") ? "./osxScroll" : "./darkScroll";
    hasBitmapArray = 1;
}
if (!(isObject())) {
    fontColor = GuiControlProfile @ new "LoadTextProfile"() @ "66 219 234";
    0;
    autoSizeWidth = LoadTextProfile @ 1;
    autoSizeHeight = 1;
}
if (!(isObject())) {
    fontColorLink = GuiControlProfile @ new "GuiMLTextProfile"() @ "255  96  96";
    0;
    fontColorLinkHL = GuiMLTextProfile @ "0     0 255";
    fontColorHL = "150 200 255 200";
    fillColorHL = "255 200 230  50";
    canKeyFocus = 1;
}
canKeyFocus = new GuiControlProfile(GuiMLTextModelessProfile : GuiMLTextProfile) @ 0;
modal = 0;
if (!(isObject())) {
    fontColorLink = GuiControlProfile @ new "GuiMLTextNoSelectProfile"() @ "255 96 96";
    0;
    fontColorLinkHL = GuiMLTextNoSelectProfile @ "0 0 255";
    modal = 0;
}
if (!(isObject())) {
    fontColorLink = GuiMLTextEditProfile @ new GuiControlProfile(GuiMLTextEditProfile) @ "255 96 96";
    fontColorLinkHL = "0 0 255";
    fillColor = "255 255 255";
    fillColorHL = "128 128 128";
    fontColor = "0 0 0";
    fontColorHL = "255 255 255";
    fontColorNA = "128 128 128";
    autoSizeWidth = 1;
    autoSizeHeight = 1;
    tab = 1;
    canKeyFocus = 1;
}
if (!(isObject())) {
    fontType = 0 @ GuiControlProfile @ (new "GuiConsoleProfile"() SPC $Platform $= "macos") ? "Courier New" : "Lucida Console";
    GuiConsoleProfile;
    fontSize = ($Platform $= "macos") ? 14 : 12;
    fontColor = "0 0 0";
    fontColorHL = "130 130 130";
    fontColorNA = "255 0 0";
    fontColors = "  0   0   0" @ 5;
    fontColors = "150 150 150" @ 6;
    fontColors = "200 150  50" @ 7;
    fontColors = "255   0   0" @ 8;
    fontColors = "255   0 255" @ 9;
}
if (!(isObject())) {
    opaque = GuiControlProfile @ new "GuiProgressProfile"() @ 0;
    0;
    fillColor = GuiProgressProfile @ "44 152 162 100";
    border = 1;
    borderColor = "78 88 120";
}
if (!(isObject())) {
    fontColor = GuiControlProfile @ new "GuiProgressTextProfile"() @ "0 0 0";
    0;
    justify = GuiProgressTextProfile @ "center";
}
if (!(isObject())) {
    opaque = GuiInspectorFieldProfile @ new GuiControlProfile(GuiInspectorFieldProfile) @ 0;
    fillColor = "255 255 255";
    fillColorHL = "128 128 128";
    fillColorNA = "244 244 244";
    border = 0;
    borderColor = "190 190 190";
    borderColorHL = "156 156 156";
    borderColorNA = "64 64 64";
    bevelColorHL = "255 255 255";
    bevelColorLL = "0 0 0";
    fontType = "Arial";
    fontSize = 16;
    fontColor = "32 32 32";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
    tab = 1;
    canKeyFocus = 1;
}
if (!(isObject())) {
    border = GuiInspectorBackgroundProfile @ new GuiControlProfile(GuiInspectorBackgroundProfile : GuiInspectorFieldProfile) @ 5;
}
if (!(isObject())) {
    new GuiControlProfile(GuiInspectorDynamicFieldProfile : GuiInspectorFieldProfile);
}
if (!(isObject())) {
    opaque = GuiControlProfile @ new "GuiInspectorTextEditProfile"() @ 0;
    0;
    border = GuiInspectorDynamicFieldProfile @ GuiInspectorTextEditProfile @ 0;
    tab = 1;
    canKeyFocus = 1;
    fontType = "Arial";
    fontSize = 16;
    fontColor = "32 32 32";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
}
if (!(isObject())) {
    mouseOverSelected = InspectorTypeEnumProfile @ new GuiControlProfile(InspectorTypeEnumProfile : GuiInspectorFieldProfile) @ 1;
    bitmap = ($Platform $= "macos") ? "./osxScroll" : "./darkScroll";
    hasBitmapArray = 1;
    opaque = 1;
    border = 1;
}
if (!(isObject())) {
    bitmap = InspectorTypeCheckboxProfile @ (new GuiControlProfile(InspectorTypeCheckboxProfile : GuiInspectorFieldProfile) SPC $Platform $= "macos") ? "./osxCheck" : "./torqueCheck";
    hasBitmapArray = 1;
    opaque = 0;
    border = 0;
}
if (!(isObject())) {
    opaque = GuiInspectorTypeFileNameProfile @ new GuiControlProfile(GuiInspectorTypeFileNameProfile) @ 0;
    border = 5;
    tab = 1;
    canKeyFocus = 1;
    fontType = "Arial";
    fontSize = 16;
    justify = "center";
    fontColor = "32 32 32";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
    fillColor = "255 255 255";
    fillColorHL = "128 128 128";
    fillColorNA = "244 244 244";
    borderColor = "190 190 190";
    borderColorHL = "156 156 156";
    borderColorNA = "64 64 64";
}
if (!(isObject())) {
    bitmap = GuiMessageWindowProfile @ new GuiControlProfile(GuiMessageWindowProfile : GuiWindowProfile) @ "./msgWindow";
    fillColor = "  0   0   0 179";
    fontColor = "255 255 255 255";
    textOffset = "12 12";
    canKeyFocus = 1;
    stretchBitmaps = 0;
}
if (!(isObject())) {
    fontColor = GuiMessageTextProfile @ new GuiControlProfile(GuiMessageTextProfile : GuiTextProfile) @ "255 255 255 255";
    fontColorGL = "255 255 255 128";
    fontColorLink = "255 106 196 255";
}
if (!(isObject())) {
    bitmap = GuiVarWidthButtonProfile @ new GuiControlProfile(GuiVarWidthButtonProfile : GuiDefaultProfile) @ "./varWidthButton";
    justify = "center";
    borderColorHL = "170 170 170 255";
    hlOffset = "0 0";
    hlInset = "4 2";
    fontColor = "230 100 255 255";
    fontColors = "255 147 248 255" @ 6;
    fontColors = "147   0 137 255" @ 7;
    fontColors = "100 100 100 255" @ 8;
    drawShadow = 0;
}
if (!(isObject())) {
    canKeyFocus = GuiFocusableVWButtonProfile @ new GuiControlProfile(GuiFocusableVWButtonProfile : GuiVarWidthButtonProfile) @ 1;
    tab = 1;
}
hotSpot = new GuiCursor(DefaultCursor) @ "1 1";
bitmapName = "./CUR_3darrow";
