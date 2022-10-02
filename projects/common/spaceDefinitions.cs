function spaceDefs_Init()
{
    %space = spaces_GetSpaceDef("starStyle", 1);
    %space.shortName = "StarStyle";
    %space.onEntryText = "SPACE Welcome to StarStyle - Rock Pop & Urban are in the front, Latin & Country are in the back! Press F5 to shop the styles in the videos. Check back often, more products are on the way!";
    %space.accessRoles = "staff";
    %space.accessLevels = "";
    %space.audioStreamID = "StarStyle";
    %space.audioStreamVolume = 0.6;
    %space.visitID = "edocBot_secretStoreRare2";
    %space = spaces_GetSpaceDef("starStyleSub1", 1);
    %space.storeID = "starstyle";
    %space.shoppingUIText = "Welcome to StarStyle - Rock Pop & Urban!";
    %space = spaces_GetSpaceDef("starStyleSub2", 1);
    %space.storeID = "starstyle2";
    %space.shoppingUIText = "Welcome to StarStyle - Larin & Country!";
    %space = spaces_GetSpaceDef("blueSpace", 1);
    %space = spaces_GetSpaceDef("greenSpace", 1);
    %space = spaces_GetSpaceDef("maroonSpace", 1);
    %space = spaces_GetSpaceDef("yellowSpace", 1);
    %space = spaces_GetSpaceDef("triggerWonderlandSpace", 1);
    return ;
}
