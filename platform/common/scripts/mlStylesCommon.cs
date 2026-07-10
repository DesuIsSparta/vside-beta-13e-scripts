function mlStyle(%dry, %styleName) {
    if (!(isDefined("%styleName"))) {
    }
    if ((%styleName $= "")) {
        return "";
    }
    %wet = standardSubstitutions(%dry);
    %styleBody = %styleName[$gMlStyle @ %styleName];
    if ((%styleBody $= "")) {
        error(getScopeName() @ " " @ "- unknown style:" @ " " @ %styleName @ " " @ getTrace());
    }
    %wet = "<spush>" @ %styleBody @ %wet @ "<spop>";
    return %wet;
};
