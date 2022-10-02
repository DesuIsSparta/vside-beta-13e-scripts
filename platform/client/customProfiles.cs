new GuiControlProfile(GuiDefaultProfile)
{
    tab = 0;
    canKeyFocus = 0;
    hasBitmapArray = 0;
    mouseOverSelected = 0;
    opaque = 0;
    fillColor = "201 182 153";
    fillColorHL = "221 202 173";
    fillColorNA = "221 202 173";
    border = 0;
    borderColor = "0 0 0";
    borderColorHL = "179 134 94";
    borderColorNA = "126 79 37";
    fontType = "Arial";
    fontSize = 14;
    fontColor = "0 0 0";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
    fontColorSEL = "200 200 200";
    drawShadow = 0;
    bitmap = "./demoWindow";
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
new GuiControlProfile(GuiSelectableProfile : GuiDefaultProfile);
new GuiControlProfile(LargeShapeNameHudProfile : GuiSelectableProfile)
{
    fontType = "Arial";
    fontSize = 18;
};
new GuiControlProfile(MediumShapeNameHudProfile : LargeShapeNameHudProfile)
{
    fontType = "Arial";
    fontSize = 16;
};
new GuiControlProfile(SmallShapeNameHudProfile : LargeShapeNameHudProfile)
{
    fontType = "Arial";
    fontSize = 14;
};
new GuiControlProfile(BoldLargeShapeNameHudProfile : LargeShapeNameHudProfile);
new GuiControlProfile(BoldMediumShapeNameHudProfile : MediumShapeNameHudProfile);
new GuiControlProfile(BoldSmallShapeNameHudProfile : SmallShapeNameHudProfile);
new GuiControlProfile(GuiWindowProfile)
{
    opaque = 1;
    border = 2;
    borderColor = "200 200 200 200";
    borderColorHL = "255 255 255 200";
    borderColorNA = "180 180 180 200";
    fillColor = "255 255 255 200";
    fillColorHL = "200 200 200";
    fillColorNA = "200 200 200";
    fontColor = "255 255 255";
    fontColorHL = "255 255 255";
    text = "GuiWindowCtrl test";
    bitmap = "./demoWindow";
    textOffset = "6 6";
    hasBitmapArray = 1;
    justify = "center";
};
new GuiControlProfile(GuiTranslucentProfile)
{
    opaque = 1;
    border = 0;
    fillColor = "255 255 255 150";
    fillColorHL = "200 200 200";
    fillColorNA = "200 200 200";
    fontColor = "255 255 255";
    fontColorHL = "255 255 255";
    text = "GuiTranslucedntCtrl test";
    textOffset = "6 6";
    justify = "center";
};
new GuiControlProfile(GuiScrollProfile)
{
    opaque = 1;
    fillColor = "255 255 255";
    border = 3;
    borderThickness = 2;
    borderColor = "0 0 0";
    bitmap = "./demoScroll";
    hasBitmapArray = 1;
};
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
    bitmap = "./demoCheck";
    hasBitmapArray = 1;
};
new GuiControlProfile(GuiRadioProfile)
{
    fontSize = 14;
    fillColor = "232 232 232";
    fontColorHL = "32 100 100";
    fixedExtent = 1;
    bitmap = "./demoRadio";
    hasBitmapArray = 1;
};
if (!isObject(GuiClickLabelProfile))
{
}
new GuiControlProfile(GuiClickLabelProfile)
    {
        opaque = 1;
        fontColor = "100 100 100";
        fontColorHL = "0 0 0";
        fontColorNA = "30 30 30";
        fillColor = "232 232 232 0";
        fixedExtent = 1;
        justify = "center";
        canKeyFocus = 0;
        borderThickness = 0;
        border = 1;
        borderColor = "256 256 256 200";
        borderColorNA = "128 128 128 200";
        acceptMouseDragAndDrop = 1;
    };
if (!isObject(GuiClickLabelProfileBold))
{
}
new GuiControlProfile(GuiClickLabelProfileBold : GuiClickLabelProfile);

