$SKY_OUTSIDE_MAIN_SPHERE = "projects/vside/worlds/raijuku/skies/01-02b/01-02b_0007.jpg";
$SKY_OUTSIDE_MAIN_CUBE = "projects/vside/worlds/raijuku/skies/01-02b/skycube";
$REFLECT_AMOUNT_TRANSGLASS = 0.6;
$REFLECT_AMOUNT_WOOD = 0.2;
$SOUNDID_GLASSWALK = 1;
$SOUNDID_WOODWALK = 0;
$SOUNDID_GRAVELWALK = 2;
function addMaterialMapping_GlassOutside(%matName)
{
    addMaterialMapping(%matName, "sound:" SPC $SOUNDID_GLASSWALK, "environment:" SPC $SKY_OUTSIDE_MAIN_SPHERE SPC $REFLECT_AMOUNT_TRANSGLASS, "environmentCube:" SPC $SKY_OUTSIDE_MAIN_CUBE SPC $REFLECT_AMOUNT_TRANSGLASS);
    return ;
}
function addMaterialMapping_WoodOutside(%matName)
{
    addMaterialMapping(%matName, "sound:" SPC $SOUNDID_WOODWALK);
    return ;
}
function addMaterialMapping_GlassGenericSheen(%matName)
{
    %genericSheen_SPHERE = "projects/common/worlds/gen_spheremap";
    %genericSheen_CUBE = "projects/common/cubemaps/glassSheen";
    %glossAmount = 0.6;
    addMaterialMapping(%matName, "sound:" SPC $SOUNDID_GLASSWALK, "environment:" SPC %genericSheen_SPHERE SPC %glossAmount, "environmentCube:" SPC %genericSheen_CUBE SPC %glossAmount);
    return ;
}
function addMaterialMapping_GravelOutside(%matName)
{
    addMaterialMapping(%matName, "sound:" SPC $SOUNDID_GRAVELWALK);
    return ;
}
addMaterialMapping("generic", "sound: 1");
addMaterialMapping_GravelOutside("zengarden_floor_sands6");
addMaterialMapping_GravelOutside("zengarden_floor_sands2");
addMaterialMapping_GravelOutside("zengarden_floor_sands");
addMaterialMapping_GravelOutside("w_stone_riverrocks");
addMaterialMapping_WoodOutside("env_jpeg_wood1");
addMaterialMapping_WoodOutside("rk_wood_panel1");
addMaterialMapping_WoodOutside("rk_woodfloorgrey");
addMaterialMapping_WoodOutside("sk_piertopwood");
addMaterialMapping_WoodOutside("sk_woodtable");
addMaterialMapping_WoodOutside("sk_woodwall_3857");
addMaterialMapping_GlassGenericSheen("rk_letterg");
addMaterialMapping_GlassGenericSheen("rk_iiG_glasswall");
addMaterialMapping_GlassGenericSheen("rk_glass");
addMaterialMapping_GlassGenericSheen("rk_glass-wall");
addMaterialMapping_GlassGenericSheen("rk_leaves_4");

