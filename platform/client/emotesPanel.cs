function EmoteHudWin::open(%this) {
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
    currentEmote = "" @ EmoteHudList;
    0.setEditMode(EmoteHudList);
};
function EmoteHudWin::close(%this) {
    if (!(EmoteHudList @ " " @ currentEmote $= "")) {
        EmoteHudList.reset();
        return;
    }
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function clientCmdActivateDanceList(%listName) {
    %listName.dynamicAddList(EmoteHudTabs);
    insertPlainToCodedListIntoMap(%listName);
};
function clientCmdDeActivateDanceList(%listName) {
    emote("/whew");
    removePlainToCodedListFromMap(%listName);
    %listName.dynamicRemoveList(EmoteHudTabs);
};
function EmoteHudTabs::dynamicAddList(%this, %dancesList) {
    %list = "dances".getTabWithName(%this).list;
    %num = (getFieldCount(%dancesList) / 2.0);
    %n = 0;
    while ((%n < %num)) {
        getField(%dancesList, (%n * 2.0)).addRow(%list, %list.rowCount());
        %n = (%n + 1.0);
    }
    EmoteHudList.reset();
};
function EmoteHudTabs::dynamicRemoveList(%this, %dancesList) {
    %list = "dances".getTabWithName(%this).list;
    %num = (getFieldCount(%dancesList) / 2.0);
    %n = 0;
    while ((%n < %num)) {
        %danceName = getField(%dancesList, (%n * 2.0));
        %index = %danceName.findTextIndex(%list);
        echo("Searching for \"" @ %danceName @ "\" and got index" @ " " @ %index);
        if ((%index != -(1.0))) {
            %index.removeRow(%list);
        }
        %n = (%n + 1.0);
    }
    EmoteHudList.reset();
};
function EmoteHudTabs::wakeUp(%this) {
    %this.setup();
    %this.selectCurrentTab();
};
function EmoteHudWin::wakeUp(%this) {
    EmoteHudList.setup();
};
function EmoteHudList::setup(%this) {
    safeEnsureScriptObjectWithInit("StringMap", "EmoteBindingMap", "{ ignoreCase = true; }");
    EmoteBindingMap.clear();
    %genders = "f m";
    %keys = "F08 F09 F10 F11 F12 ctrl1 ctrl2 ctrl3 ctrl4 ctrl5 ctrl6 ctrl7 ctrl8 ctrl9 ctrl0";
    %n = (getWordCount(%genders) - 1.0);
    while ((%n >= 0.0)) {
        %gender = getWord(%genders, %n);
        if ((%gender $= $UserPref::Player::gender)) {
            %m = (getWordCount(%keys) - 1.0);
            while ((%m >= 0.0)) {
                %key = getWord(%keys, %m);
                %emote = ;
                if (!(%emote $= "")) {
                    %key.put(EmoteBindingMap, %emote);
                }
                %m = (%m - 1.0);
            }
        }
        %n = (%n - 1.0);
        (%m >= 0.0);
    }
    0.setEditMode(EmoteHudList);
};
function EmoteHudList::populateLists(%this) {
    "Mood".initializeList(%this);
    "FavoriteActions".initializeList(%this);
    "Expressions".initializeList(%this);
    "Gestures".initializeList(%this);
    "DancesLounge".initializeList(%this);
    "DancesBreak".initializeList(%this);
    "DancesThrilla".initializeList(%this);
    "DancesGoGo".initializeList(%this);
    "DancesHipHop".initializeList(%this);
    "DancesJB".initializeList(%this);
    "DancesGoth".initializeList(%this);
    %startingPos = %this.getPosition();
    "".setText(%this);
    %list = %this.lists;
    "Mood";
    %numMoods = getWordCount($gMoods);
    %i = 0;
    while ((%i < %numMoods)) {
        getWord($gMoods, %i).getMLDisplayForMood(%this).put(%list, %i);
        %i = (%i + 1.0);
    }
    %list = %this.lists;
    (%i < %numMoods) @ "FavoriteActions";
    %size = EmoteBindingMap.size();
    %i = 0;
    while ((%i < %size)) {
        %key = %i.getKey(EmoteBindingMap);
        %value = %i.getValue(EmoteBindingMap);
        if (!(%key $= "")) {
            %key.getMLDisplayForEmote(%this).put(%list, %value);
        }
        %i = (%i + 1.0);
    }
    %expressions = "angry" @ "\t" @ "confused" @ "\t" @ "cry" @ "\t" @ "embarrassed" @ "\t" @ "flirt" @ "\t" @ "hmm" @ "\t" @ "in-love" @ "\t" @ "lol" @ "\t" @ "rotfl" @ "\t" @ "sad" @ "\t" @ "scared" @ "\t" @ "sleepy" @ "\t" @ "smile" @ "\t" @ "surprised" @ "\t" @ "thinking";
    (%i < %size);
    %expressions.populateList(%this, "Expressions", %this.lists);
    %gestures = "yes" @ "\t" @ "no" @ "\t" @ "applause" @ "\t" @ "applaud for" @ "\t" @ "bow" @ "\t" @ "boo" @ "\t" @ "busy" @ "\t" @ "come-here" @ "\t" @ "cool" @ "\t" @ "crowd-wave" @ "\t" @ "doh" @ "\t" @ "hiFive-initiate" @ "\t" @ "hiFive-finish" @ "\t" @ "hug-initiate" @ "\t" @ "hug-finish" @ "\t" @ "kiss" @ "\t" @ "lol" @ "\t" @ "loser" @ "\t" @ "not-listening" @ "\t" @ "o-my-nails" @ "\t" @ "point" @ "\t" @ "reauxshambeaux synch" @ "\t" @ "reaux" @ "\t" @ "sham" @ "\t" @ "beaux" @ "\t" @ "rotfl" @ "\t" @ "shhh" @ "\t" @ "sit" @ "\t" @ "shake-fist-at" @ "\t" @ "shoo" @ "\t" @ "shrug" @ "\t" @ "sleepy" @ "\t" @ "supermodel-turn" @ "\t" @ "talk-to-the-hand" @ "\t" @ "thumbs-up" @ "\t" @ "tapglass" @ "\t" @ "thumbs-down" @ "\t" @ "vomit" @ "\t" @ "vside" @ "\t" @ "waiting" @ "\t" @ "wave" @ "\t" @ "whew";
    %gestures.populateList(%this, "Gestures", %this.lists);
    if (($UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_Lounge_F;
    }
    %dances = $dancesMap_Lounge_M;
    %dances.populateListWithPairs(%this, "DancesLounge", %this.lists);
    if (($UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_Break_F;
    }
    %dances = $dancesMap_Break_M;
    %dances.populateListWithPairs(%this, "DancesBreak", %this.lists);
    if (($UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_Goth_F;
    }
    %dances = $dancesMap_Goth_M;
    %dances.populateListWithPairs(%this, "DancesGoth", %this.lists);
    if (($UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_GoGo_F;
    }
    %dances = $dancesMap_GoGo_M;
    %dances.populateListWithPairs(%this, "DancesGoGo", %this.lists);
    $dancesMap_Thrilla.populateListWithPairs(%this, "DancesThrilla", %this.lists);
    $dancesMap_HipHop.populateListWithPairs(%this, "DancesHipHop", %this.lists);
    $dancesMap_JB.populateListWithPairs(%this, "DancesJB", %this.lists);
    "Mood".setCurListName(%this);
    "Mood".putListIntoList(%this);
    "FavoriteActions".setCurListName(%this);
    "FavoriteActions".putListIntoList(%this);
    "Expressions".setCurListName(%this);
    "Expressions".putListIntoList(%this);
    "Gestures".setCurListName(%this);
    "Gestures".putListIntoList(%this);
    "DancesLounge".setCurListName(%this);
    "DancesLounge".putListIntoList(%this);
    "DancesBreak".setCurListName(%this);
    "DancesBreak".putListIntoList(%this);
    "DancesThrilla".setCurListName(%this);
    "DancesThrilla".putListIntoList(%this);
    "DancesGoGo".setCurListName(%this);
    "DancesGoGo".putListIntoList(%this);
    "DancesHipHop".setCurListName(%this);
    "DancesHipHop".putListIntoList(%this);
    "DancesJB".setCurListName(%this);
    "DancesJB".putListIntoList(%this);
    "DancesGoth".setCurListName(%this);
    "DancesGoth".putListIntoList(%this);
    %startingPos.scrollToPos(%this);
    schedulePersist();
};
function EmoteHudList::populateList(%this, %list, %emotes) {
    %count = getFieldCount(%emotes);
    %i = 0;
    while ((%i < %count)) {
        %field = getField(%emotes, %i);
        %field.getMLDisplayForEmote(%this).put(%list, (100000.0 + %i));
        %i = (%i + 1.0);
    }
};
function EmoteHudList::populateListWithPairs(%this, %list, %emotePairs) {
    %count = (getFieldCount(%emotePairs) / 2.0);
    %i = 0;
    while ((%i < %count)) {
        %field = getField(%emotePairs, (2.0 * %i));
        %field.getMLDisplayForEmote(%this).put(%list, (100000.0 + %i));
        %i = (%i + 1.0);
    }
};
function EmoteHudList::reset(%this) {
    if ((%this.timer != 0.0)) {
        cancel(%this.timer);
        %this.timer = 0;
    }
    %this.currentEmote = "";
    %this.populateLists();
};
function EmoteHudList::setEditMode(%this, %flag) {
    %this.editMode = %flag;
    if (%this.editMode) {
        "Done Editing".setText(EmoteEditButton);
    }
    "Edit Action Hotkeys".setText(EmoteEditButton);
    %this.reset();
};
function EmoteHudList::toggleEditMode(%this) {
    !(%this.editMode).setEditMode(%this);
};
function EmoteHudList::getMLDisplayForEmote(%this, %emote) {
    %rightStr = "";
    %binding = %emote.get(EmoteBindingMap);
    if ((%binding $= "")) {
        if (%this.editMode) {
            %rightStr = "<spush><color:666666aa><linkcolor:666666aa><just:right><a:gamelink bindemote " @ %emote @ ">[bind]</a><just:left><spop>";
        }
    }
    if (%this.editMode) {
        %rightStr = "<spush><color:cccccc><linkcolor:cccccc><just:right><a:gamelink bindemote " @ %emote @ ">[" @ %binding @ "]</a><just:left><spop>";
    }
    %rightStr = "<spush><color:999999><linkcolor:999999><just:right>[" @ %binding @ "]<just:left><spop>";
    return "    <a:gamelink emote " @ convertWordToAnim(%emote) @ ">" @ %emote @ "</a>" @ %rightStr;
};
function EmoteHudList::getMLDisplayForMood(%this, %mood) {
    %text = %mood;
    if ((findWord($gMoodAbbreviations, $UserPref::Player::Genre) == findWord($gMoods, %mood))) {
        %text = "<spush><b>-" @ " " @ %mood @ " " @ "-<spop>";
    }
    return "    <a:gamelink set_mood " @ %mood @ ">" @ %text @ "</a>";
};
function EmoteHudList::initializeList(%this, %listName) {
    if (!(isObject(%listName, %this.lists))) {
        %this.lists = new StringMap("") {
            ignoreCase = 0 @ 1;
        }; @ %listName
    }
    %this.lists.clear(%listName);
};
function EmoteHudList::setCurListName(%this, %listName) {
    %this.curListName = %listName;
    %this.listAdded = 0 @ %listName;
};
function EmoteHudList::putListIntoList(%this, %srcList) {
    %list = %this.lists;
    %srcList;
    if (!(isObject(%list))) {
        log(relations, error, "unknown list" @ " " @ %srcList);
        return;
    }
    if ((%list.size() > 0.0)) {
    }
    if (!(%this.listAdded)) {
        %this.listAdded = %this.curListName @ 1 @ %this.curListName;
        if (%this[$UserPref::emotes::collapsedLists @ %this.curListName]) {
            %collapsed = "+";
        }
        %collapsed = "- ";
        %listTitle = %this[$gEmoteListTitles @ %this.curListName];
        %titleLine = "<color:ffffff><linkcolor:ffffff><spush><linkcolor:f5b9ff><b><a:gamelink list " @ %this.curListName @ ">" @ %collapsed @ %listTitle @ "</a><spop>";
        %this.getText() @ %titleLine @ "<br>".setText(%this);
    }
    if (!(%this[$UserPref::emotes::collapsedLists @ %this.curListName])) {
        "addToEmotesList".forEach(%list);
    }
};
function StringMap::addToEmotesList(%this, %key, %value) {
    EmoteHudList.getText() @ %value @ "<br>".setText(EmoteHudList);
};
function EmoteHudList::scrollToPos(%this, %pos) {
    (1.0 - getWord(%pos, 1)).scrollTo(%this.getParent(), 0);
};
function EmoteHudList::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "set_mood")) {
        setMood(findWord($gMoods, getWord(%url, 1)));
        EmoteHudList.populateLists();
    }
    if ((getWord(%url, 0) $= "emote")) {
        sendAnimToServer(getWords(%url, 1));
    }
    if ((getWord(%url, 0) $= "list")) {
        %listName = getWords(%url, 1);
        %listName[$UserPref::emotes::collapsedLists @ %listName] = !(%listName[$UserPref::emotes::collapsedLists @ %listName]);
        EmoteHudList.populateLists();
    }
    if ((getWord(%url, 0) $= "bindemote")) {
        %this.currentEmote = getWords(%url, 1);
        "<linkcolor:ffffff>" @ "Binding <spush><b><color:e553ff>" @ %this.currentEmote @ "<spop>...<br>" @ "<br>" @ "Type a hotkey below or choose one from the list.<br>" @ "<br>".setText(%this);
        %bindings = "F08 F09 F10 F11 F12 ctrl1 ctrl2 ctrl3 ctrl4 ctrl5 ctrl6 ctrl7 ctrl8 ctrl9 ctrl0";
        %count = getWordCount(%bindings);
        %i = 0;
        while ((%i < %count)) {
            %binding = getWord(%bindings, %i);
            %emote = %binding.getEmoteForBinding(%this);
            %rightStr = "";
            %leftStr = "<a:gamelink bindbinding " @ %binding @ ">[" @ %binding @ "]</a>";
            if ((%emote $= %this.currentEmote)) {
                %rightStr = "<just:right><spush><b><color:e553ff>" @ %emote @ "<spop><just:left>";
                %leftStr = "[<spush><b><linkcolor:e553ff><linkcolorhl:f5b9ff><a:gamelink cancel>" @ %binding @ "</a><spop>]";
            }
            if (!(%emote $= "")) {
                %rightStr = "<just:right>" @ %emote @ "<just:left>";
            }
            %this.getText() @ %leftStr @ %rightStr @ "<br>".setText(%this);
            %i = (%i + 1.0);
        }
        %this.getText() @ "<br>".setText(%this);
        %binding = %this.currentEmote.get(EmoteBindingMap);
        (%i < %count);
        if (!(%binding $= "")) {
            %this.getText() @ "<a:gamelink unbind " @ %binding @ ">[ Unbind " @ %binding @ " ]</a>  ".setText(%this);
        }
        %this.getText() @ "<just:right><a:gamelink cancel>[ Cancel ]</a><just:left>".setText(%this);
    }
    if ((getWord(%url, 0) $= "bindbinding")) {
        if (!(%this.currentEmote $= "")) {
            getWord(%url, 1).doFunc(%this);
        }
        warn("Tried to bind a key without current emote defined.");
    }
    if ((getWord(%url, 0) $= "unbind")) {
        if (!(%this.currentEmote $= "")) {
            "".rebind(%this, getWord(%url, 1));
        }
        warn("Tried to unbind a key without current emote defined.");
    }
    if ((getWord(%url, 0) $= "cancel")) {
        %this.reset();
    }
};
function normalizeKey(%key) {
    %key = strreplace(%key, "-", "");
    if ((%key $= "F8")) {
        %key = "F08";
    }
    if ((%key $= "F9")) {
        %key = "F09";
    }
    return %key;
};
function EmoteHudList::getEmoteForBinding(%this, %binding) {
    %binding = normalizeKey(%binding);
    return %binding[$UserPref::emotes TAB $UserPref::Player::gender @ %binding];
};
function EmoteHudList::setEmoteForBinding(%this, %binding, %emote) {
    %binding = normalizeKey(%binding);
    %binding[%emote @ $UserPref::emotes TAB $UserPref::Player::gender @ %binding] = ;
};
function EmoteHudList::rebind(%this, %binding, %emote) {
    %binding2 = normalizeKey(%binding);
    if ((%binding2 $= %emote.get(EmoteBindingMap))) {
        %this.reset();
        return;
    }
    "".setEmoteForBinding(%this, %emote.get(EmoteBindingMap));
    %binding2[EmoteBindingMap @ $UserPref::emotes TAB $UserPref::Player::gender @ %binding2].remove();
    %binding2[%emote @ $UserPref::emotes TAB $UserPref::Player::gender @ %binding2] = ;
    if (!(%emote $= "")) {
        %binding2.put(EmoteBindingMap, %emote);
    }
    if (!(%emote $= "")) {
        "<spush><b><color:e553ff>[" @ %binding @ "]<spop> now maps to <spush><b><color:e553ff>" @ %emote @ "<spop>".setText(%this);
    }
    "Removed binding for <spush><b><color:e553ff>[" @ %binding2 @ "]<spop>".setText(%this);
    %this.timer = "reset".schedule(%this, 1500);
};
function EmoteHudList::doFunc(%this, %func) {
    if (!(%this.currentEmote $= "")) {
        %this.currentEmote.rebind(%this, %func);
    }
    %anim = convertWordToAnim(%func.getEmoteForBinding(%this));
    if ((%anim $= "")) {
        error(getScopeName() @ " " @ "can't find anim for" @ " " @ %func);
    }
    sendAnimToServer(%anim);
};
$gMoods = "Confident Relaxed Upbeat Blue Fabulous";
$gMoodAbbreviations = "h i p b x";
function setMood(%mood) {
    %moodName = getWord($gMoods, %mood);
    if ((%moodName $= "")) {
        return;
    }
    echo("Setting mood to " @ %moodName);
    $UserPref::Player::Genre = getWord($gMoodAbbreviations, %mood);
    commandToServer('setGenre', $UserPref::Player::Genre);
};
