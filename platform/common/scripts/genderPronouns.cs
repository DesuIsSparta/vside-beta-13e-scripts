function getGender(%obj)
{
    if (((%obj $= "f") || (%obj $= "m")) || (%obj $= "n"))
    {
        return %obj;
    }
    if (!isPlayerObject(%obj))
    {
        return "n";
    }
    return %obj.getGender();
}
$genderPronounHeSheIt["m"] = "he";
$genderPronounHeSheIt["f"] = "she";
$genderPronounHeSheIt["n"] = "it";
function getPronounHeSheIt(%obj)
{
    return $genderPronounHeSheIt[getGender(%obj)];
}
$genderPronounHeSheThey["m"] = "he";
$genderPronounHeSheThey["f"] = "she";
$genderPronounHeSheThey["n"] = "they";
function getPronounHeSheThey(%obj)
{
    return $genderPronounHeSheThey[getGender(%obj)];
}
$genderPronounHimHerIt["m"] = "him";
$genderPronounHimHerIt["f"] = "her";
$genderPronounHimHerIt["n"] = "it";
function getPronounHimHerIt(%obj)
{
    return $genderPronounHimHerIt[getGender(%obj)];
}
$genderPronounHimHerThem["m"] = "him";
$genderPronounHimHerThem["f"] = "her";
$genderPronounHimHerThem["n"] = "them";
function getPronounHimHerThem(%obj)
{
    return $genderPronounHimHerThem[getGender(%obj)];
}
$genderPronounHisHerIts["m"] = "his";
$genderPronounHisHerIts["f"] = "her";
$genderPronounHisHerIts["n"] = "its";
function getPronounHisHerIts(%obj)
{
    return $genderPronounHisHerIts[getGender(%obj)];
}
$genderPronounHisHerTheir["m"] = "his";
$genderPronounHisHerTheir["f"] = "her";
$genderPronounHisHerTheir["n"] = "their";
function getPronounHisHerTheir(%obj)
{
    return $genderPronounHisHerTheir[getGender(%obj)];
}
$genderPronounHisHerTheirCapital["m"] = "His";
$genderPronounHisHerTheirCapital["f"] = "Her";
$genderPronounHisHerTheirCapital["n"] = "Their";
function getPronounHisHerTheirCapital(%obj)
{
    return $genderPronounHisHerTheirCapital[getGender(%obj)];
}
$genderPronounHisHersIts["m"] = "his";
$genderPronounHisHersIts["f"] = "hers";
$genderPronounHisHersIts["n"] = "its";
function getPronounHisHersIts(%obj)
{
    return $genderPronounHisHersIts[getGender(%obj)];
}
$genderPronounHisHersTheirs["m"] = "his";
$genderPronounHisHersTheirs["f"] = "hers";
$genderPronounHisHersTheirs["n"] = "theirs";
function getPronounHisHersTheirs(%obj)
{
    return $genderPronounHisHersTheirs[getGender(%obj)];
}
function getPronounItThem(%quantity)
{
    %ret = %quantity == 1 ? "it" : "them";
    return %ret;
}
