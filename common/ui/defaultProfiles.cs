$Gui::fontCacheDirectory = ExpandFilename("./cache");
$Gui::clipboardFile = ExpandFilename("./cache/clipboard.gui");
if (!isObject(GuiDefaultProfile))
{
}
new GuiControlProfile(GuiDefaultProfile)
    {
        tab = 0;
        canKeyFocus = 0;
        hasBitmapArray = 0;
        mouseOverSelected = 0;
        opaque = 0;
        fillColor = $Platform $= "macos" ? "211 211 211" : "192 192 192";
        fillColorHL = $Platform $= "macos" ? "244 244 244" : "220 220 220";
        fillColorNA = $Platform $= "macos" ? "244 244 244" : "220 220 220";
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
        bitmap = $Platform $= "macos" ? "./osxWindow" : "./darkWindow";
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
    };
if (!isObject(GuiInputCtrlProfile))
{
}
new GuiControlProfile(GuiInputCtrlProfile)
    {
        tab = 1;
        canKeyFocus = 1;
    };
if (!isObject(GuiDialogProfile))
{
}
new GuiControlProfile(GuiDialogProfile);
if (!isObject(GuiSolidDefaultProfile))
{
}
new GuiControlProfile(GuiSolidDefaultProfile)
    {
        opaque = 1;
        border = 0;
        fillColor = $Platform $= "macos" ? "211 211 211" : "192 192 192";
    };
if (!isObject(GuiWindowProfile))
{
}
new GuiControlProfile(GuiWindowProfile)
    {
        opaque = 1;
        border = 2;
        fillColor = $Platform $= "macos" ? "211 211 211" : "192 192 192";
        fillColorHL = $Platform $= "macos" ? "190 255 255" : "64 150 150";
        fillColorNA = $Platform $= "macos" ? "255 255 255" : "150 150 150";
        fontColor = $Platform $= "macos" ? "0 0 0" : "255 255 255";
        fontColorHL = $Platform $= "macos" ? "200 200 200" : "0 0 0";
        text = "GuiWindowCtrl test";
        bitmap = $Platform $= "macos" ? "./osxWindow" : "./darkWindow";
        textOffset = $Platform $= "macos" ? "5 5" : "6 6";
        hasBitmapArray = 1;
        justify = $Platform $= "macos" ? "center" : "left";
    };
if (!isObject(GuiToolWindowProfile))
{
}
new GuiControlProfile(GuiToolWindowProfile)
    {
        opaque = 1;
        border = 2;
        fillColor = "192 192 192";
        fillColorHL = "64 150 150";
        fillColorNA = "150 150 150";
        fontColor = "255 255 255";
        fontColorHL = "0 0 0";
        bitmap = "./torqueToolWindow";
        textOffset = "6 6";
    };
if (!isObject(EditorToolButtonProfile))
{
}
new GuiControlProfile(EditorToolButtonProfile)
    {
        opaque = 1;
        border = 2;
    };
if (!isObject(GuiContentProfile))
{
}
new GuiControlProfile(GuiContentProfile)
    {
        opaque = 1;
        fillColor = "255 255 255";
    };
if (!isObject(GuiModelessDialogProfile))
{
}
new GuiControlProfile("GuiModelessDialogProfile");
if (!isObject(GuiButtonProfile))
{
}
new GuiControlProfile(GuiButtonProfile)
    {
        opaque = 1;
        border = 1;
        drawShadow = 0;
        fontColor = "0 0 0";
        fontColorHL = "32 100 100";
        fixedExtent = 1;
        justify = "center";
        canKeyFocus = 0;
    };
if (!isObject(GuiBorderButtonProfile))
{
}
new GuiControlProfile(GuiBorderButtonProfile);
if (!isObject(GuiMenuBarProfile))
{
}
new GuiControlProfile(GuiMenuBarProfile)
    {
        opaque = 1;
        fillColor = $Platform $= "macos" ? "211 211 211" : "192 192 192";
        fillColorHL = "0 0 96";
        border = 4;
        fontColor = "0 0 0";
        fontColorHL = "255 255 255";
        fontColorNA = "128 128 128";
        fixedExtent = 1;
        justify = "center";
        canKeyFocus = 0;
        mouseOverSelected = 1;
        bitmap = $Platform $= "macos" ? "./osxMenu" : "./torqueMenu";
        hasBitmapArray = 1;
    };
if (!isObject(GuiButtonSmProfile))
{
}
new GuiControlProfile(GuiButtonSmProfile : GuiButtonProfile);
if (!isObject(GuiRadioProfile))
{
}
new GuiControlProfile(GuiRadioProfile)
    {
        fontSize = 14;
        fillColor = "232 232 232";
        fontColorHL = "32 100 100";
        fixedExtent = 1;
        bitmap = $Platform $= "macos" ? "./osxRadio" : "./torqueRadio";
        hasBitmapArray = 1;
    };
if (!isObject(GuiScrollProfile))
{
}
new GuiControlProfile(GuiScrollProfile)
    {
        opaque = 1;
        fillColor = "255 255 255";
        border = 3;
        borderThickness = 2;
        borderColor = "0 0 0";
        bitmap = $Platform $= "macos" ? "./osxScroll" : "./darkScroll";
        hasBitmapArray = 1;
    };
if (!isObject(GuiSliderProfile))
{
}
new GuiControlProfile(GuiSliderProfile);
if (!isObject(GuiTextProfile))
{
}
new GuiControlProfile(GuiTextProfile)
    {
        fontColor = "0 0 0";
        fontColorLink = "255 96 96";
        fontColorLinkHL = "0 0 255";
        autoSizeWidth = 1;
        autoSizeHeight = 1;
    };
if (!isObject(EditorTextProfile))
{
}
new GuiControlProfile(EditorTextProfile)
    {
        fontType = "Arial Bold";
        fontColor = "0 0 0";
        autoSizeWidth = 1;
        autoSizeHeight = 1;
    };
if (!isObject(EditorTextProfileWhite))
{
}
new GuiControlProfile(EditorTextProfileWhite)
    {
        fontType = "Arial Bold";
        fontColor = "255 255 255";
        autoSizeWidth = 1;
        autoSizeHeight = 1;
    };
if (!isObject(GuiMediumTextProfile))
{
}
new GuiControlProfile(GuiMediumTextProfile : GuiTextProfile);
if (!isObject(GuiBigTextProfile))
{
}
new GuiControlProfile(GuiBigTextProfile : GuiTextProfile);
if (!isObject(GuiCenterTextProfile))
{
}
new GuiControlProfile(GuiCenterTextProfile : GuiTextProfile);
if (!isObject(MissionEditorProfile))
{
}
new GuiControlProfile(MissionEditorProfile);
if (!isObject(EditorScrollProfile))
{
}
new GuiControlProfile(EditorScrollProfile)
    {
        opaque = 1;
        fillColor = "192 192 192 192";
        border = 3;
        borderThickness = 2;
        borderColor = "0 0 0";
        bitmap = "./darkScroll";
        hasBitmapArray = 1;
    };
if (!isObject(GuiTextEditProfile))
{
}
new GuiControlProfile(GuiTextEditProfile)
    {
        opaque = 1;
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
    };
if (!isObject(GuiControlListPopupProfile))
{
}
new GuiControlProfile(GuiControlListPopupProfile)
    {
        opaque = 1;
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
        bitmap = $Platform $= "macos" ? "./osxScroll" : "./darkScroll";
        hasBitmapArray = 1;
    };
if (!isObject(GuiTextArrayProfile))
{
}
new GuiControlProfile(GuiTextArrayProfile : GuiTextProfile)
    {
        fontColorHL = "32 100 100";
        fillColorHL = "200 200 200";
    };
if (!isObject(GuiTextListProfile))
{
}
new GuiControlProfile(GuiTextListProfile : GuiTextProfile);
if (!isObject(GuiTreeViewProfile))
{
}
new GuiControlProfile(GuiTreeViewProfile)
    {
        fontSize = 13;
        fontColor = "0 0 0";
        fontColorHL = "64 150 150";
        canKeyFocus = 1;
        autoSizeHeight = 1;
        fontColorSEL = "250 250 250";
        fillColorHL = "0 60 150";
        fontColorNA = "240 240 240";
        bitmap = "./shll_treeView";
    };
if (!isObject(GuiCheckBoxProfile))
{
}
new GuiControlProfile(GuiCheckBoxProfile)
    {
        opaque = 0;
        fillColor = "232 232 232";
        border = 0;
        borderColor = "0 0 0";
        fontSize = 14;
        fontColor = "0 0 0";
        fontColorHL = "32 100 100";
        fixedExtent = 1;
        justify = "left";
        bitmap = $Platform $= "macos" ? "./osxCheck" : "./torqueCheck";
        hasBitmapArray = 1;
    };
if (!isObject(GuiPopUpMenuProfile))
{
}
new GuiControlProfile(GuiPopUpMenuProfile)
    {
        opaque = 1;
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
        bitmap = $Platform $= "macos" ? "./osxScroll" : "./darkScroll";
        hasBitmapArray = 1;
    };
if (!isObject(GuiEditorClassProfile))
{
}
new GuiControlProfile(GuiEditorClassProfile)
    {
        opaque = 1;
        fillColor = "232 232 232";
        border = 1;
        borderColor = "0 0 0";
        borderColorHL = "127 127 127";
        fontColor = "0 0 0";
        fontColorHL = "32 100 100";
        fixedExtent = 1;
        justify = "center";
        bitmap = $Platform $= "macos" ? "./osxScroll" : "./darkScroll";
        hasBitmapArray = 1;
    };
if (!isObject(LoadTextProfile))
{
}
new GuiControlProfile("LoadTextProfile")
    {
        fontColor = "66 219 234";
        autoSizeWidth = 1;
        autoSizeHeight = 1;
    };
if (!isObject(GuiMLTextProfile))
{
}
new GuiControlProfile("GuiMLTextProfile")
    {
        fontColorLink = "255  96  96";
        fontColorLinkHL = "0     0 255";
        fontColorHL = "150 200 255 200";
        fillColorHL = "255 200 230  50";
        canKeyFocus = 1;
    };
new GuiControlProfile(GuiMLTextModelessProfile : GuiMLTextProfile)
{
    canKeyFocus = 0;
    modal = 0;
};
if (!isObject(GuiMLTextNoSelectProfile))
{
}
new GuiControlProfile("GuiMLTextNoSelectProfile")
    {
        fontColorLink = "255 96 96";
        fontColorLinkHL = "0 0 255";
        modal = 0;
    };
if (!isObject(GuiMLTextEditProfile))
{
}
new GuiControlProfile(GuiMLTextEditProfile)
    {
        fontColorLink = "255 96 96";
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
    };
if (!isObject(GuiConsoleProfile))
{
}
new GuiControlProfile("GuiConsoleProfile")
    {
        fontType = $Platform $= "macos" ? "Courier New" : "Lucida Console";
        fontSize = $Platform $= "macos" ? 14 : 12;
        fontColor = "0 0 0";
        fontColorHL = "130 130 130";
        fontColorNA = "255 0 0";
        fontColors[5] = "  0   0   0";
        fontColors[6] = "150 150 150";
        fontColors[7] = "200 150  50";
        fontColors[8] = "255   0   0";
        fontColors[9] = "255   0 255";
    };
if (!isObject(GuiProgressProfile))
{
}
new GuiControlProfile("GuiProgressProfile")
    {
        opaque = 0;
        fillColor = "44 152 162 100";
        border = 1;
        borderColor = "78 88 120";
    };
if (!isObject(GuiProgressTextProfile))
{
}
new GuiControlProfile("GuiProgressTextProfile")
    {
        fontColor = "0 0 0";
        justify = "center";
    };
if (!isObject(GuiInspectorFieldProfile))
{
}
new GuiControlProfile(GuiInspectorFieldProfile)
    {
        opaque = 0;
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
    };
if (!isObject(GuiInspectorBackgroundProfile))
{
}
new GuiControlProfile(GuiInspectorBackgroundProfile : GuiInspectorFieldProfile);
if (!isObject(GuiInspectorDynamicFieldProfile))
{
}
new GuiControlProfile(GuiInspectorDynamicFieldProfile : GuiInspectorFieldProfile);
if (!isObject(GuiInspectorTextEditProfile))
{
}
new GuiControlProfile("GuiInspectorTextEditProfile")
    {
        opaque = 0;
        border = 0;
        tab = 1;
        canKeyFocus = 1;
        fontType = "Arial";
        fontSize = 16;
        fontColor = "32 32 32";
        fontColorHL = "32 100 100";
        fontColorNA = "0 0 0";
    };
if (!isObject(InspectorTypeEnumProfile))
{
}
new GuiControlProfile(InspectorTypeEnumProfile : GuiInspectorFieldProfile)
    {
        mouseOverSelected = 1;
        bitmap = $Platform $= "macos" ? "./osxScroll" : "./darkScroll";
        hasBitmapArray = 1;
        opaque = 1;
        border = 1;
    };
if (!isObject(InspectorTypeCheckboxProfile))
{
}
new GuiControlProfile(InspectorTypeCheckboxProfile : GuiInspectorFieldProfile)
    {
        bitmap = $Platform $= "macos" ? "./osxCheck" : "./torqueCheck";
        hasBitmapArray = 1;
        opaque = 0;
        border = 0;
    };
if (!isObject(GuiInspectorTypeFileNameProfile))
{
}
new GuiControlProfile(GuiInspectorTypeFileNameProfile)
    {
        opaque = 0;
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
    };
if (!isObject(GuiMessageWindowProfile))
{
}
new GuiControlProfile(GuiMessageWindowProfile : GuiWindowProfile)
    {
        bitmap = "./msgWindow";
        fillColor = "  0   0   0 179";
        fontColor = "255 255 255 255";
        textOffset = "12 12";
        canKeyFocus = 1;
        stretchBitmaps = 0;
    };
if (!isObject(GuiMessageTextProfile))
{
}
new GuiControlProfile(GuiMessageTextProfile : GuiTextProfile)
    {
        fontColor = "255 255 255 255";
        fontColorGL = "255 255 255 128";
        fontColorLink = "255 106 196 255";
    };
if (!isObject(GuiVarWidthButtonProfile))
{
}
new GuiControlProfile(GuiVarWidthButtonProfile : GuiDefaultProfile)
    {
        bitmap = "./varWidthButton";
        justify = "center";
        borderColorHL = "170 170 170 255";
        hlOffset = "0 0";
        hlInset = "4 2";
        fontColor = "230 100 255 255";
        fontColors[6] = "255 147 248 255";
        fontColors[7] = "147   0 137 255";
        fontColors[8] = "100 100 100 255";
        drawShadow = 0;
    };
if (!isObject(GuiFocusableVWButtonProfile))
{
}
new GuiControlProfile(GuiFocusableVWButtonProfile : GuiVarWidthButtonProfile)
    {
        canKeyFocus = 1;
        tab = 1;
    };
new GuiCursor(DefaultCursor)
{
    hotSpot = "1 1";
    bitmapName = "./CUR_3darrow";
};

