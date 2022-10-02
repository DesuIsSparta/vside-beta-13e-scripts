function userTips::initUserTips()
{
    $userTips::numTips = 0;
    %tipName = "AutoChangeToSwimWear";
    $userTips::tipTitle[%tipName] = "Tip - Automatically changing into swimwear.";
    $userTips::tipBody[%tipName] = "You\'ve been automatically changed into your swimming outfit.\nTo change outfits, go to the button bar and click \"Me\".";
    $userTips::tipCallbackOK[%tipName] = "";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = 300;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "DancePadUsage";
    $userTips::tipTitle[%tipName] = "Tip - Switch Dances with the Dance Pad";
    $userTips::tipBody[%tipName] = "Mouse over the 8 buttons to perform that dance.\nUse the drop down menus to choose the dance a button will do.";
    $userTips::tipCallbackOK[%tipName] = "";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = 300;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "DanceToolUsage";
    $userTips::tipTitle[%tipName] = "Tip - Dancetastìque";
    $userTips::tipBody[%tipName] = "Welcome to the preview of the Dance Tool! - This is a simple tool to let you record and play back dances, and also share them with other people via email, forums, etc.\n\nBasic Usage:\nClick \'record\' and start dancing! To play it back, click \'stop\' and then \'play\'.\n\nThe dance tool only records dance moves and animations like \'rotfl\' - it won\'t record walking, turning, or jumping.\n\nTo save a dance, \'clipboard: copy to\' will copy your dance into the computer clipboard. From there, you can paste it like normal into your favorite text editor, email, or forum page. To load a dance, you do the same thing backwards - copy the text of the dance into the clipboard, and then click \'clipboard: copy from\'.\nNote that your dance isn\'t automatically saved when you quit the " @ $ETS::AppName @ " !\n\nWe\'re going to be building a lot more stuff around this in coming releases, so don\'t worry if this one seems to be lacking.";
    $userTips::tipCallbackOK[%tipName] = "";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = 420;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "HideChat";
    $userTips::tipTitle[%tipName] = "Chat Is Hidden";
    $userTips::tipBody[%tipName] = "<just:left>You currently hiding the chat bubble.\nTo stop hiding it, press F6, choose display settings, and un-check \"Hide Chat\".";
    $userTips::tipCallbackOK[%tipName] = "";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = 300;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "SOSUsage";
    $userTips::tipTitle[%tipName] = "Help";
    $userTips::tipBody[%tipName] = "<linkcolor:e553ff><linkcolorhl:ff93f8>" NL "<spush><font:BauhausStd-Demi:24><just:center>What\'s The Problem?<spop>" NL "" NL "<just:left><font:Verdana:12>         Click the links below for Help:" NL "" NL "<font:Verdana Bold:14>       <bitmap:platform/client/ui/bullet_white> Help with <a:" @ $Net::HelpURL_Navigation @ ">moving, chatting, dancing, etc.</a>" NL "       <bitmap:platform/client/ui/bullet_white> Help finding <a:" @ $Net::HelpURL_MusicNEvents @ ">music, locations and events.</a>" NL "       <bitmap:platform/client/ui/bullet_white> Something is <a:" @ $Net::HelpURL_Support @ ">wrong with vSide.</a>" NL "       <bitmap:platform/client/ui/bullet_white> Someone is <a:" @ $Net::HelpURL_Abuse @ "?section=Safety>bothering me!</a>" NL "<spush><just:center>" NL "[ <a:" @ $Net::HelpURL_Guidelines @ ">vSide House Rules</a> | <a:" @ $Net::HelpURL_Parents @ ">Parents FAQ</a> ]<spop>" NL "";
    $userTips::tipCallbackOK[%tipName] = "cancelUserSOS();";
    $userTips::tipCallbackCnc[%tipName] = "cancelUserSOS();";
    $userTips::tipWidth[%tipName] = 360;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "GotMic";
    $userTips::tipTitle[%tipName] = "You\'ve got the Microphone!";
    $userTips::tipBody[%tipName] = "You just got a microphone!\nWhatever you type will carry extra far!  (Whispers still work normally)\nIf you don\'t want the microphone, just click \"cancel\" below.";
    $userTips::tipCallbackOK[%tipName] = "no_op();";
    $userTips::tipCallbackCnc[%tipName] = "doDropMic();";
    $userTips::tipWidth[%tipName] = 400;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "PasteSkus";
    $userTips::tipTitle[%tipName] = "Pasting SKUs";
    $userTips::tipBody[%tipName] = "This will attempt to apply whatever\'s in the system clipboard as SKUs to user <spush><b>[TARGETPLAYERNAME]<spop>.<br><br><spush><b>The SKUs will not be vetted!<spop><br><br>Is you sure ?";
    $userTips::tipCallbackOK[%tipName] = "doUserPasteSkusReally(\"[TARGETPLAYERNAME]\");";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = "";
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "BroadcastImages";
    $userTips::tipTitle[%tipName] = "Camera Tool";
    $userTips::tipBody[%tipName] = "Use this window to take snapshots of the world!";
    $userTips::tipCallbackOK[%tipName] = "";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = 300;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "SpaceOwner";
    $userTips::tipTitle[%tipName] = "Hosting Tips!";
    $userTips::tipBody[%tipName] = "<linkcolor:e553ff><linkcolorhl:ff93f8><color:ffffff>" NL "<spush><font:BauhausStd-Demi:24><just:center>Welcome to your new apartment!<spop>" NL "" NL "<just:left><font:Verdana:14>Congratulations on your new vSide apartment! Here are a few things you should know:" NL "" NL "<spush><font:Verdana Bold:14>1. <color:e553ff>Moderate:<spop> You are in charge of your space. Control who can come in and who can\'t in \"My Rules\" from the Space button on the button bar.  Block troublemakers with right-click \"Block from space\". <a:" @ absoluteURL($Net::BaseDomain, "go/moderatespace") @ ">How to moderate your apartment</a>." NL "" NL "<spush><font:Verdana Bold:14>2. <color:e553ff>Decorate:<spop> Access your furniture by clicking \"My Furnishings\" in the Space button. When you\'re ready for more, use \"Shop\". <a:" @ absoluteURL($Net::BaseDomain, "go/customizespace") @ ">More on decorating your apartment</a>." NL "" NL "<spush><font:Verdana Bold:14>3. <color:e553ff>Entertain:<spop> Play your choice of music and YouTube videos by clicking \"My Music & Videos\" in the Space button. <a:" @ absoluteURL($Net::BaseDomain, "go/apartmentmedia") @ ">More about music and video in your apartment</a>." NL "" NL "<spush><font:Verdana Bold:14>4. <color:e553ff>Party!<spop> Now that you\'ve got an awesome place, you\'ll want to throw a housewarming party! <a:" @ absoluteURL($Net::BaseDomain, "go/eventshelp") @ ">How to list your event on the vSide Event Calendar</a>." NL "";
    $userTips::tipCallbackOK[%tipName] = "";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = 600;
    %tipName = "CustomGameHost";
    $userTips::tipTitle[%tipName] = "Custom Game Introduction";
    $userTips::tipBody[%tipName] = "<linkcolor:e553ff><linkcolorhl:ff93f8><color:ffffff>" NL "<spush><font:BauhausStd-Demi:24><just:center>You created a custom game.<spop>" NL "" NL "<just:left><font:Verdana:14>This is your game, you set the goals and rules, and apply them." NL "You do this in a few ways:" NL "" NL "<bitmap:platform/client/ui/bullet_white> <spush><font:Verdana Bold:14><color:e553ff>Change score!<spop>: When your game is displayed in the hud, you can right click players on your screen to change their score. This could be flag captures, an evaluation of their dancing ability, how many quiz questions they got right - was it a 9 point dive off the skyscraper, or only a 5? You get the idea." NL "" NL "<bitmap:platform/client/ui/bullet_white> <spush><font:Verdana Bold:14><color:e553ff>Change status!<spop>: Are they IT? Are they king of the hill? Red team? The captain? Whatever they are, tell them and the other players by setting their status the same way you change their score." NL "" NL "<bitmap:platform/client/ui/bullet_white> <spush><font:Verdana Bold:14><color:e553ff>Invite!<spop>: Hey hey, this does require imagination, but you don\'t need to imagine your players too. Right click friends in person, or on your friend-list and click \"Invite to game\" and they\'ll be told about your game and have the choice to join. You must have your game inspected in the hud to do this." NL "" NL "<spush><font:Verdana:14>Just use your imagination. Play capture the flag, have a freestyle battle, run a vSide quiz show, just play." NL "";
    %tipName = "VideosDisabled";
    $userTips::tipTitle[%tipName] = "Videos are disabled";
    $userTips::tipBody[%tipName] = "<color:ffffff>" NL "You have disabled videos in your client." NL "To re-enable them just go into F6 | Audio and uncheck \"Disable Videos\".";
    $userTips::tipCallbackOK[%tipName] = "echo(\"\");";
    $userTips::tipCallbackCnc[%tipName] = "echo(\"\");";
    $userTips::tipWidth[%tipName] = 500;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "PaperDollPermute";
    $userTips::tipTitle[%tipName] = "Paper Doll Permutations";
    $userTips::tipBody[%tipName] = "Please read dev/aboutPaperDolls.txt";
    $userTips::tipCallbackOK[%tipName] = "paperDoll_Permute();";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = 300;
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "tgf_main_samples";
    $userTips::tipTitle[%tipName] = "Sample Images";
    $userTips::tipBody[%tipName] = "you need the <spush><outline><shadowcolor:ee3399><linkcolor:ffffff><a:gamelink http://svn.doppelganger.com/release/trunk/misc/>dev-mod</a><spop> to get sample images here";
    $userTips::tipCallbackOK[%tipName] = "";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = "";
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    %tipName = "closet_myshop_copyToOutfit";
    $userTips::tipTitle[%tipName] = "Copy Items to Outfit";
    $userTips::tipBody[%tipName] = "This will copy these items to your current outfit.";
    $userTips::tipCallbackOK[%tipName] = "ClosetGui_MyShop_CopySkusToOutfit();";
    $userTips::tipCallbackCnc[%tipName] = "";
    $userTips::tipWidth[%tipName] = "";
    $userTips::allTips[$userTips::numTips] = %tipName ;
    $userTips::numTips = $userTips::numTips + 1;
    userTips::resetSeenThisSession();
    return ;
}
function userTips::resetSeenThisSession()
{
    %n = 0;
    while (%n < $userTips::numTips)
    {
        $userTips::tipSeen[$userTips::allTips[%n]] = 0;
        %n = %n + 1;
    }
}

function userTips::resetSeenEver()
{
    %n = 0;
    while (%n < $userTips::numTips)
    {
        $UserPref::userTips::tipSeen[$userTips::allTips[%n]] = 0;
        %n = %n + 1;
    }
}

function userTips::resetSeenAll()
{
    userTips::resetSeenThisSession();
    userTips::resetSeenEver();
    return ;
}
function userTips::showNow(%tipName)
{
    $userTips::tipSeen[%tipName] = 1;
    %title = standardSubstitutions($userTips::tipTitle[%tipName]);
    %body = standardSubstitutions($userTips::tipBody[%tipName]);
    %cbOk = standardSubstitutions($userTips::tipCallbackOK[%tipName]);
    %cbCnc = standardSubstitutions($userTips::tipCallbackCnc[%tipName]);
    %width = $userTips::tipWidth[%tipName];
    %width = %width $= "" ? 300 : %width;
    %dialog = 0;
    if (!(%cbOk $= ""))
    {
        %dialog = MessageBoxOkCancel(%title, %body, %cbOk, %cbCnc);
    }
    else
    {
        %dialog = MessageBoxOK(%title, %body, "");
    }
    %dialog.setWindowWidth(%width);
    return %dialog;
}
function userTips::showOnceThisSession(%tipName)
{
    if ($userTips::tipSeen[%tipName] != 0)
    {
        if (!($userTips::tipCallbackOK[%tipName] $= ""))
        {
            eval($userTips::tipCallbackOK[%tipName]);
        }
    }
    else
    {
        userTips::showNow(%tipName);
    }
    return ;
}
function userTips::showOnceEver(%tipName)
{
    if ($UserPref::userTips::tipSeen[%tipName] == 1)
    {
        if (!($userTips::tipCallbackOK[%tipName] $= ""))
        {
            eval($userTips::tipCallbackOK[%tipName]);
        }
    }
    else
    {
        $UserPref::userTips::tipSeen[%tipName] = 1;
        userTips::showNow(%tipName);
    }
    return ;
}
function no_op()
{
    return ;
}
userTips::initUserTips();

