$gCreditsTickPeriod = 30;
$gCreditsTickPixels = 2;
$gCreditsTickDirection = 1;
function doCredits()
{
    %text = "<color:ccddddFF><just:center>";
    %text = %text @ "<br><br><br><br><br><br><br>";
    %text = credits_AddSection(%text, "vSide");
    %text = credits_AddName(%text, "Aaron G.");
    %text = credits_AddName(%text, "Adam B.");
    %text = credits_AddName(%text, "Alex G.");
    %text = credits_AddName(%text, "Anders H.");
    %text = credits_AddName(%text, "Andrew L.");
    %text = credits_AddName(%text, "Ashley D.");
    %text = credits_AddName(%text, "Brian E.");
    %text = credits_AddName(%text, "Chris L.");
    %text = credits_AddName(%text, "Clint B.");
    %text = credits_AddName(%text, "Daniel H.");
    %text = credits_AddName(%text, "Daniel K.");
    %text = credits_AddName(%text, "Don F.");
    %text = credits_AddName(%text, "Ed E.");
    %text = credits_AddName(%text, "Elizabeth B.");
    %text = credits_AddName(%text, "Erez M.");
    %text = credits_AddName(%text, "Eric D.");
    %text = credits_AddName(%text, "Eric H.");
    %text = credits_AddName(%text, "Eric S.");
    %text = credits_AddName(%text, "Erik C.");
    %text = credits_AddName(%text, "Evan K.");
    %text = credits_AddName(%text, "Fabien W.");
    %text = credits_AddName(%text, "Gari C.");
    %text = credits_AddName(%text, "Geoff G.");
    %text = credits_AddName(%text, "Greg L.");
    %text = credits_AddName(%text, "Ian B.");
    %text = credits_AddName(%text, "Ivy E.");
    %text = credits_AddName(%text, "James McD.");
    %text = credits_AddName(%text, "Jean D.S.");
    %text = credits_AddName(%text, "Jeff S.");
    %text = credits_AddName(%text, "Jimmy L.");
    %text = credits_AddName(%text, "John G.");
    %text = credits_AddName(%text, "John K.");
    %text = credits_AddName(%text, "Jon B.");
    %text = credits_AddName(%text, "Josh C.");
    %text = credits_AddName(%text, "Josh M.");
    %text = credits_AddName(%text, "Ken F.");
    %text = credits_AddName(%text, "Kristin Y.");
    %text = credits_AddName(%text, "Krunal P.");
    %text = credits_AddName(%text, "Lars B.");
    %text = credits_AddName(%text, "Leo R.");
    %text = credits_AddName(%text, "Ling Ling Y.");
    %text = credits_AddName(%text, "Liz D.");
    %text = credits_AddName(%text, "Loren S.");
    %text = credits_AddName(%text, "Manchiu S.");
    %text = credits_AddName(%text, "Mic M.");
    %text = credits_AddName(%text, "Mike P.");
    %text = credits_AddName(%text, "Mike J.");
    %text = credits_AddName(%text, "Neil H.");
    %text = credits_AddName(%text, "Nikita T.");
    %text = credits_AddName(%text, "Nino C.");
    %text = credits_AddName(%text, "Orion E.");
    %text = credits_AddName(%text, "Paul Z.");
    %text = credits_AddName(%text, "Randall M.");
    %text = credits_AddName(%text, "Richard K.");
    %text = credits_AddName(%text, "Ross S.");
    %text = credits_AddName(%text, "Scott S.");
    %text = credits_AddName(%text, "Shaw T.");
    %text = credits_AddName(%text, "Shun K.");
    %text = credits_AddName(%text, "Stacy S.");
    %text = credits_AddName(%text, "Steve D.");
    %text = credits_AddName(%text, "Steve R.");
    %text = credits_AddName(%text, "Tamara K.");
    %text = credits_AddName(%text, "Tara P.");
    %text = credits_AddName(%text, "Ted S.");
    %text = credits_AddName(%text, "Terrence H.");
    %text = credits_AddName(%text, "Terry R.");
    %text = credits_AddName(%text, "Tim McC.");
    %text = credits_AddName(%text, "Tim S.");
    %text = credits_AddName(%text, "Tim Sm.");
    %text = credits_AddName(%text, "Tom N.");
    %text = credits_AddName(%text, "Tracie R.");
    %text = credits_AddName(%text, "Willy B.");
    LoginCreditsText.setText(%text);
    $gCreditsTickDirection = -$gCreditsTickDirection;
    creditsTick();
    return ;
}
$gCreditsTimerID = 0;
function creditsTick()
{
    cancel($gCreditsTimerID);
    %y = getWord(LoginCreditsText.position, 1);
    %h = getWord(LoginCreditsText.extent, 1);
    if (($gCreditsTickDirection < 0) && ((%y + %h) < 0))
    {
        return ;
    }
    if (($gCreditsTickDirection > 0) && (%y > 157))
    {
        return ;
    }
    %y = %y + ($gCreditsTickPixels * $gCreditsTickDirection);
    LoginCreditsText.reposition(0, %y);
    $gCreditsTimerID = schedule($gCreditsTickPeriod, 0, "creditsTick");
    return ;
}
function credits_AddSection(%dry, %name)
{
    %wet = %dry @ "<br><br><spush><font:BauhausStd-Demi:20>" @ %name @ "<spop><br>";
    return %wet;
}
function credits_AddName(%dry, %name)
{
    %wet = %dry @ %name @ "<br>";
    return %wet;
}
function credits_AddLead(%dry, %name)
{
    return credits_AddName(%dry, "<spush><b>" @ %name @ "<spop>");
}
