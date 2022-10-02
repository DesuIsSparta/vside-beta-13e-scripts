safeEnsureScriptObject("StringMap", "gStompedObjectNames");
function onObjectNameStomped(%name, %stompeeID, %likeleyStomperID)
{
    gStompedObjectNames.put(gStompedObjectNames.size() SPC %name, %stompeeID SPC %likeleyStomperID);
    return ;
}
function displayStompedObjectNameErrors()
{
    if (!$ETS::devMode && (gStompedObjectNames.size() == 0))
    {
        return ;
    }
    schedule(0, 0, "displayStompedObjectNameErrorsReally");
    return ;
}
function displayStompedObjectNameErrorsReally()
{
    %title = "STOMPED OBJECT NAMES";
    %body = "the following critical object names were stomped.";
    %body = %body NL "THIS IS A CRITICAL PROBLEM, DO NOT CHECK IN YOUR CHANGES.";
    %body = %body NL "If you can\'t figure out the problem from this message and the console.log,";
    %body = %body NL "please ask one of the game engine engineers to take a look.";
    %count = gStompedObjectNames.size();
    %realCount = 0;
    %n = 0;
    while (%n < %count)
    {
        %name = getWord(gStompedObjectNames.getKey(%n), 1);
        %skip = 0;
        if (%name $= "ClientSeatDisplayData")
        {
            %skip = 1;
        }
        else
        {
            if (%name $= "ClientSeatListeningDisplayData")
            {
                %skip = 1;
            }
        }
        if (!%skip)
        {
            %realCount = %realCount + 1;
            %body = %body NL "";
            %body = %body @ %n;
            %body = %body SPC "\"" @ %name @ "\"";
            %body = %body SPC "-" SPC getDebugString(getWord(gStompedObjectNames.getValue(%n), 1));
            %body = %body SPC "stomped" SPC getDebugString(getWord(gStompedObjectNames.getValue(%n), 0));
        }
        %n = %n + 1;
    }
    if (%realCount > 0)
    {
        %mb = MessageBoxOK(%title, %body, "").window;
        %mb.resize(800, 200);
        %mb.resizeWidth = 1;
        %mb.resizeHeight = 1;
    }
    return ;
}
