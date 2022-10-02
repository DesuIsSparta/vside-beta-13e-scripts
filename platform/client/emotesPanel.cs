function EmoteHudWin::open(%this)
{
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    EmoteHudList.currentEmote = "";
    EmoteHudList.setEditMode(0);
    return ;
}
function EmoteHudWin::close(%this)
{
    if (!(EmoteHudList.currentEmote $= ""))
    {
        EmoteHudList.reset();
        return ;
    }
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
}
function clientCmdActivateDanceList(%listName)
{
    EmoteHudTabs.dynamicAddList(%listName);
    insertPlainToCodedListIntoMap(%listName, EmoteDict);
    return ;
}
function clientCmdDeActivateDanceList(%listName)
{
    emote("/whew");
    removePlainToCodedListFromMap(%listName, EmoteDict);
    EmoteHudTabs.dynamicRemoveList(%listName);
    return ;
}
function EmoteHudTabs::dynamicAddList(%this, %dancesList)
{
    %list = %this.getTabWithName("dances").list;
    %num = getFieldCount(%dancesList) / 2;
    %n = 0;
    while (%n < %num)
    {
        %list.addRow(%list.rowCount(), getField(%dancesList, %n * 2));
        %n = %n + 1;
    }
    EmoteHudList.reset();
    return ;
}
function EmoteHudTabs::dynamicRemoveList(%this, %dancesList)
{
    %list = %this.getTabWithName("dances").list;
    %num = getFieldCount(%dancesList) / 2;
    %n = 0;
    while (%n < %num)
    {
        %danceName = getField(%dancesList, %n * 2);
        %index = %list.findTextIndex(%danceName);
        echo("Searching for \"" @ %danceName @ "\" and got index" SPC %index);
        if (%index != -1)
        {
            %list.removeRow(%index);
        }
        %n = %n + 1;
    }
    EmoteHudList.reset();
    return ;
}
function EmoteHudTabs::wakeUp(%this)
{
    %this.setup();
    %this.selectCurrentTab();
    return ;
}
function EmoteHudWin::wakeUp(%this)
{
    EmoteHudList.setup();
    return ;
}
$gEmoteListTitles["Mood"] = "Mood";
$gEmoteListTitles["FavoriteActions"] = "Favorite Actions";
$gEmoteListTitles["Expressions"] = "Expressions";
$gEmoteListTitles["Gestures"] = "Gestures";
$gEmoteListTitles["DancesLounge"] = "Lounge Dances";
$gEmoteListTitles["DancesBreak"] = "Break Dances";
$gEmoteListTitles["DancesThrilla"] = "Thrilla Dances";
$gEmoteListTitles["DancesGoGo"] = "Go-Go Dances";
$gEmoteListTitles["DancesHipHop"] = "HipHop Dances";
$gEmoteListTitles["DancesJB"] = "JB Dances";
$gEmoteListTitles["DancesGoth"] = "Goth Dances";
function EmoteHudList::setup(%this)
{
    safeEnsureScriptObjectWithInit("StringMap", "EmoteBindingMap", "{ ignoreCase = true; }");
    EmoteBindingMap.clear();
    %genders = "f m";
    %keys = "F08 F09 F10 F11 F12 ctrl1 ctrl2 ctrl3 ctrl4 ctrl5 ctrl6 ctrl7 ctrl8 ctrl9 ctrl0";
    %n = getWordCount(%genders) - 1;
    while (%n >= 0)
    {
        %gender = getWord(%genders, %n);
        if (%gender $= $UserPref::Player::gender)
        {
            %m = getWordCount(%keys) - 1;
            while (%m >= 0)
            {
                %key = getWord(%keys, %m);
                %emote = $UserPref::emotes[%gender,normalizeKey(%key)];
                if (!(%emote $= ""))
                {
                    EmoteBindingMap.put(%emote, %key);
                }
                %m = %m - 1;
            }
        }
        %n = %n - 1;
    }
    EmoteHudList.setEditMode(0);
    return ;
}
function EmoteHudList::populateLists(%this)
{
    %this.initializeList("Mood");
    %this.initializeList("FavoriteActions");
    %this.initializeList("Expressions");
    %this.initializeList("Gestures");
    %this.initializeList("DancesLounge");
    %this.initializeList("DancesBreak");
    %this.initializeList("DancesThrilla");
    %this.initializeList("DancesGoGo");
    %this.initializeList("DancesHipHop");
    %this.initializeList("DancesJB");
    %this.initializeList("DancesGoth");
    %startingPos = %this.getPosition();
    %this.setText("");
    %list = %this.lists["Mood"];
    %numMoods = getWordCount($gMoods);
    %i = 0;
    while (%i < %numMoods)
    {
        %list.put(%i, %this.getMLDisplayForMood(getWord($gMoods, %i)));
        %i = %i + 1;
    }
    %list = %this.lists["FavoriteActions"];
    %size = EmoteBindingMap.size();
    %i = 0;
    while (%i < %size)
    {
        %key = EmoteBindingMap.getKey(%i);
        %value = EmoteBindingMap.getValue(%i);
        if (!(%key $= ""))
        {
            %list.put(%value, %this.getMLDisplayForEmote(%key));
        }
        %i = %i + 1;
    }
    %expressions = "angry" TAB "confused" TAB "cry" TAB "embarrassed" TAB "flirt" TAB "hmm" TAB "in-love" TAB "lol" TAB "rotfl" TAB "sad" TAB "scared" TAB "sleepy" TAB "smile" TAB "surprised" TAB "thinking";
    %this.populateList(%this.lists["Expressions"], %expressions);
    %gestures = "yes" TAB "no" TAB "applause" TAB "applaud for" TAB "bow" TAB "boo" TAB "busy" TAB "come-here" TAB "cool" TAB "crowd-wave" TAB "doh" TAB "hiFive-initiate" TAB "hiFive-finish" TAB "hug-initiate" TAB "hug-finish" TAB "kiss" TAB "lol" TAB "loser" TAB "not-listening" TAB "o-my-nails" TAB "point" TAB "reauxshambeaux synch" TAB "reaux" TAB "sham" TAB "beaux" TAB "rotfl" TAB "shhh" TAB "sit" TAB "shake-fist-at" TAB "shoo" TAB "shrug" TAB "sleepy" TAB "supermodel-turn" TAB "talk-to-the-hand" TAB "thumbs-up" TAB "tapglass" TAB "thumbs-down" TAB "vomit" TAB "vside" TAB "waiting" TAB "wave" TAB "whew";
    %this.populateList(%this.lists["Gestures"], %gestures);
    if ($UserPref::Player::gender $= "f")
    {
        %dances = $dancesMap_Lounge_F;
    }
    else
    {
        %dances = $dancesMap_Lounge_M;
    }
    %this.populateListWithPairs(%this.lists["DancesLounge"], %dances);
    if ($UserPref::Player::gender $= "f")
    {
        %dances = $dancesMap_Break_F;
    }
    else
    {
        %dances = $dancesMap_Break_M;
    }
    %this.populateListWithPairs(%this.lists["DancesBreak"], %dances);
    if ($UserPref::Player::gender $= "f")
    {
        %dances = $dancesMap_Goth_F;
    }
    else
    {
        %dances = $dancesMap_Goth_M;
    }
    %this.populateListWithPairs(%this.lists["DancesGoth"], %dances);
    if ($UserPref::Player::gender $= "f")
    {
        %dances = $dancesMap_GoGo_F;
    }
    else
    {
        %dances = $dancesMap_GoGo_M;
    }
    %this.populateListWithPairs(%this.lists["DancesGoGo"], %dances);
    %this.populateListWithPairs(%this.lists["DancesThrilla"], $dancesMap_Thrilla);
    %this.populateListWithPairs(%this.lists["DancesHipHop"], $dancesMap_HipHop);
    %this.populateListWithPairs(%this.lists["DancesJB"], $dancesMap_JB);
    %this.setCurListName("Mood");
    %this.putListIntoList("Mood");
    %this.setCurListName("FavoriteActions");
    %this.putListIntoList("FavoriteActions");
    %this.setCurListName("Expressions");
    %this.putListIntoList("Expressions");
    %this.setCurListName("Gestures");
    %this.putListIntoList("Gestures");
    %this.setCurListName("DancesLounge");
    %this.putListIntoList("DancesLounge");
    %this.setCurListName("DancesBreak");
    %this.putListIntoList("DancesBreak");
    %this.setCurListName("DancesThrilla");
    %this.putListIntoList("DancesThrilla");
    %this.setCurListName("DancesGoGo");
    %this.putListIntoList("DancesGoGo");
    %this.setCurListName("DancesHipHop");
    %this.putListIntoList("DancesHipHop");
    %this.setCurListName("DancesJB");
    %this.putListIntoList("DancesJB");
    %this.setCurListName("DancesGoth");
    %this.putListIntoList("DancesGoth");
    %this.scrollToPos(%startingPos);
    schedulePersist();
    return ;
}
function EmoteHudList::populateList(%this, %list, %emotes)
{
    %count = getFieldCount(%emotes);
    %i = 0;
    while (%i < %count)
    {
        %field = getField(%emotes, %i);
        %list.put(100000 + %i, %this.getMLDisplayForEmote(%field));
        %i = %i + 1;
    }
}

function EmoteHudList::populateListWithPairs(%this, %list, %emotePairs)
{
    %count = getFieldCount(%emotePairs) / 2;
    %i = 0;
    while (%i < %count)
    {
        %field = getField(%emotePairs, 2 * %i);
        %list.put(100000 + %i, %this.getMLDisplayForEmote(%field));
        %i = %i + 1;
    }
}

function EmoteHudList::reset(%this)
{
    if (%this.timer != 0)
    {
        cancel(%this.timer);
        %this.timer = 0;
    }
    %this.currentEmote = "";
    %this.populateLists();
    return ;
}
function EmoteHudList::setEditMode(%this, %flag)
{
    %this.editMode = %flag;
    if (%this.editMode)
    {
        EmoteEditButton.setText("Done Editing");
    }
    else
    {
        EmoteEditButton.setText("Edit Action Hotkeys");
    }
    %this.reset();
    return ;
}
function EmoteHudList::toggleEditMode(%this)
{
    %this.setEditMode(!%this.editMode);
    return ;
}
function EmoteHudList::getMLDisplayForEmote(%this, %emote)
{
    %rightStr = "";
    %binding = EmoteBindingMap.get(%emote);
    if (%binding $= "")
    {
        if (%this.editMode)
        {
            %rightStr = "<spush><color:666666aa><linkcolor:666666aa><just:right><a:gamelink bindemote " @ %emote @ ">[bind]</a><just:left><spop>";
        }
    }
    else
    {
        if (%this.editMode)
        {
            %rightStr = "<spush><color:cccccc><linkcolor:cccccc><just:right><a:gamelink bindemote " @ %emote @ ">[" @ %binding @ "]</a><just:left><spop>";
        }
        else
        {
            %rightStr = "<spush><color:999999><linkcolor:999999><just:right>[" @ %binding @ "]<just:left><spop>";
        }
    }
    return "    <a:gamelink emote " @ convertWordToAnim(%emote) @ ">" @ %emote @ "</a>" @ %rightStr;
}
function EmoteHudList::getMLDisplayForMood(%this, %mood)
{
    %text = %mood;
    if (findWord($gMoodAbbreviations, $UserPref::Player::Genre) == findWord($gMoods, %mood))
    {
        %text = "<spush><b>-" SPC %mood SPC "-<spop>";
    }
    return "    <a:gamelink set_mood " @ %mood @ ">" @ %text @ "</a>";
}
function EmoteHudList::initializeList(%this, %listName)
{
    if (!isObject(%this.lists[%listName]))
    {
        %this.lists[%listName] = new StringMap();
    }
    %this.lists[%listName].clear();
    return ;
}
function EmoteHudList::setCurListName(%this, %listName)
{
    %this.curListName = %listName;
    %this.listAdded[%listName] = 0;
    return ;
}
function EmoteHudList::putListIntoList(%this, %srcList)
{
    %list = %this.lists[%srcList];
    if (!isObject(%list))
    {
        log(relations, error, "unknown list" SPC %srcList);
        return ;
    }
    if ((%list.size() > 0) && !(%this.listAdded[%this.curListName]))
    {
        %this.listAdded[%this.curListName] = 1;
        if ($UserPref::emotes::collapsedLists[%this.curListName])
        {
            %collapsed = "+";
        }
        else
        {
            %collapsed = "- ";
        }
        %listTitle = $gEmoteListTitles[%this.curListName];
        %titleLine = "<color:ffffff><linkcolor:ffffff><spush><linkcolor:f5b9ff><b><a:gamelink list " @ %this.curListName @ ">" @ %collapsed @ %listTitle @ "</a><spop>";
        %this.setText(%this.getText() @ %titleLine @ "<br>");
    }
    if (!$UserPref::emotes::collapsedLists[%this.curListName])
    {
        %list.forEach("addToEmotesList");
    }
    return ;
}
function StringMap::addToEmotesList(%this, %key, %value)
{
    EmoteHudList.setText(EmoteHudList.getText() @ %value @ "<br>");
    return ;
}
function EmoteHudList::scrollToPos(%this, %pos)
{
    %this.getParent().scrollTo(0, 1 - getWord(%pos, 1));
    return ;
}
function EmoteHudList::onURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %url = getWords(%url, 1);
    }
    if (getWord(%url, 0) $= "set_mood")
    {
        setMood(findWord($gMoods, getWord(%url, 1)));
        EmoteHudList.populateLists();
    }
    else
    {
        if (getWord(%url, 0) $= "emote")
        {
            sendAnimToServer(getWords(%url, 1));
        }
        else
        {
            if (getWord(%url, 0) $= "list")
            {
                %listName = getWords(%url, 1);
                $UserPref::emotes::collapsedLists[%listName] = !$UserPref::emotes::collapsedLists[%listName];
                EmoteHudList.populateLists();
            }
            else
            {
                if (getWord(%url, 0) $= "bindemote")
                {
                    %this.currentEmote = getWords(%url, 1);
                    %this.setText("<linkcolor:ffffff>" @ "Binding <spush><b><color:e553ff>" @ %this.currentEmote @ "<spop>...<br>" @ "<br>" @ "Type a hotkey below or choose one from the list.<br>" @ "<br>");
                    %bindings = "F08 F09 F10 F11 F12 ctrl1 ctrl2 ctrl3 ctrl4 ctrl5 ctrl6 ctrl7 ctrl8 ctrl9 ctrl0";
                    %count = getWordCount(%bindings);
                    %i = 0;
                    while (%i < %count)
                    {
                        %binding = getWord(%bindings, %i);
                        %emote = %this.getEmoteForBinding(%binding);
                        %rightStr = "";
                        %leftStr = "<a:gamelink bindbinding " @ %binding @ ">[" @ %binding @ "]</a>";
                        if (%emote $= %this.currentEmote)
                        {
                            %rightStr = "<just:right><spush><b><color:e553ff>" @ %emote @ "<spop><just:left>";
                            %leftStr = "[<spush><b><linkcolor:e553ff><linkcolorhl:f5b9ff><a:gamelink cancel>" @ %binding @ "</a><spop>]";
                        }
                        else
                        {
                            if (!(%emote $= ""))
                            {
                                %rightStr = "<just:right>" @ %emote @ "<just:left>";
                            }
                        }
                        %this.setText(%this.getText() @ %leftStr @ %rightStr @ "<br>");
                        %i = %i + 1;
                    }
                    %this.setText(%this.getText() @ "<br>");
                    %binding = EmoteBindingMap.get(%this.currentEmote);
                    if (!(%binding $= ""))
                    {
                        %this.setText(%this.getText() @ "<a:gamelink unbind " @ %binding @ ">[ Unbind " @ %binding @ " ]</a>  ");
                    }
                    %this.setText(%this.getText() @ "<just:right><a:gamelink cancel>[ Cancel ]</a><just:left>");
                }
                else
                {
                    if (getWord(%url, 0) $= "bindbinding")
                    {
                        if (!(%this.currentEmote $= ""))
                        {
                            %this.doFunc(getWord(%url, 1));
                        }
                        else
                        {
                            warn("Tried to bind a key without current emote defined.");
                        }
                    }
                    else
                    {
                        if (getWord(%url, 0) $= "unbind")
                        {
                            if (!(%this.currentEmote $= ""))
                            {
                                %this.rebind(getWord(%url, 1), "");
                            }
                            else
                            {
                                warn("Tried to unbind a key without current emote defined.");
                            }
                        }
                        else
                        {
                            if (getWord(%url, 0) $= "cancel")
                            {
                                %this.reset();
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function normalizeKey(%key)
{
    %key = strreplace(%key, "-", "");
    if (%key $= "F8")
    {
        %key = "F08";
    }
    else
    {
        if (%key $= "F9")
        {
            %key = "F09";
        }
    }
    return %key;
}
function EmoteHudList::getEmoteForBinding(%this, %binding)
{
    %binding = normalizeKey(%binding);
    return $UserPref::emotes[$UserPref::Player::gender,%binding];
}
function EmoteHudList::setEmoteForBinding(%this, %binding, %emote)
{
    %binding = normalizeKey(%binding);
    $UserPref::emotes[$UserPref::Player::gender,%binding] = %emote ;
    return ;
}
function EmoteHudList::rebind(%this, %binding, %emote)
{
    %binding2 = normalizeKey(%binding);
    if (%binding2 $= EmoteBindingMap.get(%emote))
    {
        %this.reset();
        return ;
    }
    %this.setEmoteForBinding(EmoteBindingMap.get(%emote), "");
    EmoteBindingMap.remove($UserPref::emotes[$UserPref::Player::gender,%binding2]);
    $UserPref::emotes[$UserPref::Player::gender,%binding2] = %emote ;
    if (!(%emote $= ""))
    {
        EmoteBindingMap.put(%emote, %binding2);
    }
    if (!(%emote $= ""))
    {
        %this.setText("<spush><b><color:e553ff>[" @ %binding @ "]<spop> now maps to <spush><b><color:e553ff>" @ %emote @ "<spop>");
    }
    else
    {
        %this.setText("Removed binding for <spush><b><color:e553ff>[" @ %binding2 @ "]<spop>");
    }
    %this.timer = %this.schedule(1500, "reset");
    return ;
}
function EmoteHudList::doFunc(%this, %func)
{
    if (!(%this.currentEmote $= ""))
    {
        %this.rebind(%func, %this.currentEmote);
    }
    else
    {
        %anim = convertWordToAnim(%this.getEmoteForBinding(%func));
        if (%anim $= "")
        {
            error(getScopeName() SPC "can\'t find anim for" SPC %func);
        }
        else
        {
            sendAnimToServer(%anim);
        }
    }
    return ;
}
$gMoods = "Confident Relaxed Upbeat Blue Fabulous";
$gMoodAbbreviations = "h i p b x";
function setMood(%mood)
{
    %moodName = getWord($gMoods, %mood);
    if (%moodName $= "")
    {
        return ;
    }
    echo("Setting mood to " @ %moodName);
    $UserPref::Player::Genre = getWord($gMoodAbbreviations, %mood);
    commandToServer('setGenre', $UserPref::Player::Genre);
    return ;
}
