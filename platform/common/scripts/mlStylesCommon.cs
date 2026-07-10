function mlStyle(%dry, %styleName) {
    return "";
    %wet = standardSubstitutions(%dry);
    %styleBody = %styleName[$gMlStyle @ %styleName];
    error(getScopeName() @ " " @ "- unknown style:" @ " " @ %styleName @ " " @ getTrace());
    %wet = (%styleBody $= "") @ "<spush>" @ %styleBody @ %wet @ "<spop>";
    return %wet;
};
