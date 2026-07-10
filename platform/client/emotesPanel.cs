function EmoteHudWin::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
    currentEmote = WindowManager @ "" @ EmoteHudList;
    PlayGui;
    0.setEditMode();
};
function EmoteHudWin::close(%this) {
    if (!(EmoteHudList SPC currentEmote $= "")) {
        reset();
        return EmoteHudList;
    }
    %this.setVisible(0);
    focusTopWindow();
    update();
    return 1;
};
function clientCmdActivateDanceList(%listName) {
    %listName.dynamicAddList();
    insertPlainToCodedListIntoMap(%listName);
};
function clientCmdDeActivateDanceList(%listName) {
    emote("/whew");
    removePlainToCodedListFromMap(%listName);
    %listName.dynamicRemoveList();
};
function EmoteHudTabs::dynamicAddList(%this, %dancesList) {
    %list = list;
    %this.getTabWithName("dances");
    %num = (2.0 / getFieldCount(%dancesList));
    %n = 0;
    if ((%num < %n)) {
        %list.addRow(%list.rowCount(), getField(%dancesList, (2.0 * %n)));
        %n = (1.0 + %n);
    }
    reset();
};
function EmoteHudTabs::dynamicRemoveList(%this, %dancesList) {
    %list = list;
    %this.getTabWithName("dances");
    %num = (2.0 / getFieldCount(%dancesList));
    %n = 0;
    if ((%num < %n)) {
        %danceName = getField(%dancesList, (2.0 * %n));
        %index = %list.findTextIndex(%danceName);
        echo("Searching for \"" @ %danceName @ "\" and got index" @ " " @ %index);
        if ((-(1.0) != %index)) {
            %list.removeRow(%index);
        }
        %n = (1.0 + %n);
    }
    reset();
};
function EmoteHudTabs::wakeUp(%this) {
    %this.setup();
    %this.selectCurrentTab();
};
function EmoteHudWin::wakeUp(%this) {
    setup();
};
function EmoteHudList::setup(%this) {
    safeEnsureScriptObjectWithInit("StringMap", "EmoteBindingMap", "{ ignoreCase = true; }");
    clear();
    %genders = "f m";
    EmoteBindingMap;
    %keys = "F08 F09 F10 F11 F12 ctrl1 ctrl2 ctrl3 ctrl4 ctrl5 ctrl6 ctrl7 ctrl8 ctrl9 ctrl0";
    %n = (1.0 - getWordCount(%genders));
    if ((0.0 >= %n)) {
        %gender = getWord(%genders, %n);
        if ((%gender $= $UserPref::Player::gender)) {
            %m = (1.0 - getWordCount(%keys));
            if ((0.0 >= %m)) {
                %key = getWord(%keys, %m);
                %emote = ;
                if (!(%emote $= "")) {
                    %emote.put(%key);
                }
                %m = (1.0 - %m);
                EmoteBindingMap;
            }
        }
        %n = (1.0 - %n);
        (0.0 >= %m);
    }
    0.setEditMode();
};
function EmoteHudList::populateLists(%this) {
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
    %list = lists;
    "Mood" @ %this;
    %numMoods = getWordCount($gMoods);
    %i = 0;
    if ((%numMoods < %i)) {
        %list.put(%i, %this.getMLDisplayForMood(getWord($gMoods, %i)));
        %i = (1.0 + %i);
    }
    %list = lists;
    (%numMoods < %i) @ "FavoriteActions" @ %this;
    %size = size();
    EmoteBindingMap;
    %i = 0;
    if ((%size < %i)) {
        %key = %i.getKey();
        EmoteBindingMap;
        %value = %i.getValue();
        EmoteBindingMap;
        if (!(%key $= "")) {
            %list.put(%value, %this.getMLDisplayForEmote(%key));
        }
        %i = (1.0 + %i);
    }
    %expressions = "angry" @ "\t" @ "confused" @ "\t" @ "cry" @ "\t" @ "embarrassed" @ "\t" @ "flirt" @ "\t" @ "hmm" @ "\t" @ "in-love" @ "\t" @ "lol" @ "\t" @ "rotfl" @ "\t" @ "sad" @ "\t" @ "scared" @ "\t" @ "sleepy" @ "\t" @ "smile" @ "\t" @ "surprised" @ "\t" @ "thinking";
    (%size < %i);
    %this.populateList(lists, %expressions);
    %gestures = "yes" @ "\t" @ "no" @ "\t" @ "applause" @ "\t" @ "applaud for" @ "\t" @ "bow" @ "\t" @ "boo" @ "\t" @ "busy" @ "\t" @ "come-here" @ "\t" @ "cool" @ "\t" @ "crowd-wave" @ "\t" @ "doh" @ "\t" @ "hiFive-initiate" @ "\t" @ "hiFive-finish" @ "\t" @ "hug-initiate" @ "\t" @ "hug-finish" @ "\t" @ "kiss" @ "\t" @ "lol" @ "\t" @ "loser" @ "\t" @ "not-listening" @ "\t" @ "o-my-nails" @ "\t" @ "point" @ "\t" @ "reauxshambeaux synch" @ "\t" @ "reaux" @ "\t" @ "sham" @ "\t" @ "beaux" @ "\t" @ "rotfl" @ "\t" @ "shhh" @ "\t" @ "sit" @ "\t" @ "shake-fist-at" @ "\t" @ "shoo" @ "\t" @ "shrug" @ "\t" @ "sleepy" @ "\t" @ "supermodel-turn" @ "\t" @ "talk-to-the-hand" @ "\t" @ "thumbs-up" @ "\t" @ "tapglass" @ "\t" @ "thumbs-down" @ "\t" @ "vomit" @ "\t" @ "vside" @ "\t" @ "waiting" @ "\t" @ "wave" @ "\t" @ "whew";
    "Expressions" @ %this;
    %this.populateList(lists, %gestures);
    if (("Gestures" @ %this SPC $UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_Lounge_F;
    }
    %dances = $dancesMap_Lounge_M;
    %this.populateListWithPairs(lists, %dances);
    if (("DancesLounge" @ %this SPC $UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_Break_F;
    }
    %dances = $dancesMap_Break_M;
    %this.populateListWithPairs(lists, %dances);
    if (("DancesBreak" @ %this SPC $UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_Goth_F;
    }
    %dances = $dancesMap_Goth_M;
    %this.populateListWithPairs(lists, %dances);
    if (("DancesGoth" @ %this SPC $UserPref::Player::gender $= "f")) {
        %dances = $dancesMap_GoGo_F;
    }
    %dances = $dancesMap_GoGo_M;
    %this.populateListWithPairs(lists, %dances);
    %this.populateListWithPairs(lists, $dancesMap_Thrilla);
    %this.populateListWithPairs(lists, $dancesMap_HipHop);
    %this.populateListWithPairs(lists, $dancesMap_JB);
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
};
function EmoteHudList::populateList(%this, %list, %emotes) {
    %count = getFieldCount(%emotes);
    %i = 0;
    if ((%count < %i)) {
        %field = getField(%emotes, %i);
        %list.put((%i + 100000.0), %this.getMLDisplayForEmote(%field));
        %i = (1.0 + %i);
    }
};
function EmoteHudList::populateListWithPairs(%this, %list, %emotePairs) {
    %count = (2.0 / getFieldCount(%emotePairs));
    %i = 0;
    if ((%count < %i)) {
        %field = getField(%emotePairs, (%i * 2.0));
        %list.put((%i + 100000.0), %this.getMLDisplayForEmote(%field));
        %i = (1.0 + %i);
    }
};
function EmoteHudList::reset(%this) {
    if ((%this != timer)) {
        cancel(timer);
        timer = %this @ 0 @ %this;
        0.0;
    }
    currentEmote = "" @ %this;
    %this.populateLists();
};
function EmoteHudList::setEditMode(%this, %flag) {
    editMode = %flag @ %this;
    if (editMode) {
        "Done Editing".setText();
    }
    "Edit Action Hotkeys".setText();
    %this.reset();
};
function EmoteHudList::toggleEditMode(%this) {
    %this.setEditMode(!(editMode));
};
function EmoteHudList::getMLDisplayForEmote(%this, %emote) {
    %rightStr = "";
    %binding = %emote.get();
    EmoteBindingMap;
    if ((%binding $= "")) {
        if (editMode) {
            %rightStr = %this @ "<spush><color:666666aa><linkcolor:666666aa><just:right><a:gamelink bindemote " @ %emote @ ">[bind]</a><just:left><spop>";
        }
    }
    if (editMode) {
        %rightStr = %this @ "<spush><color:cccccc><linkcolor:cccccc><just:right><a:gamelink bindemote " @ %emote @ ">[" @ %binding @ "]</a><just:left><spop>";
    }
    %rightStr = "<spush><color:999999><linkcolor:999999><just:right>[" @ %binding @ "]<just:left><spop>";
    return "    <a:gamelink emote " @ convertWordToAnim(%emote) @ ">" @ %emote @ "</a>" @ %rightStr;
};
function EmoteHudList::getMLDisplayForMood(%this, %mood) {
    %text = %mood;
    if ((findWord($gMoods, %mood) == findWord($gMoodAbbreviations, $UserPref::Player::Genre))) {
        %text = "<spush><b>-" @ " " @ %mood @ " " @ "-<spop>";
    }
    return "    <a:gamelink set_mood " @ %mood @ ">" @ %text @ "</a>";
};
function EmoteHudList::initializeList(%this, %listName) {
    if (!(isObject(lists))) {
        ignoreCase = StringMap @ new ""() @ 1;
        0;
        lists = %listName @ %this @ %listName @ %this;
    }
    lists.clear();
};
function EmoteHudList::setCurListName(%this, %listName) {
    curListName = %listName @ %this;
    listAdded = 0 @ %listName @ %this;
};
function EmoteHudList::putListIntoList(%this, %srcList) {
    %list = lists;
    %srcList @ %this;
    if (!(isObject(%list))) {
        log("unknown list" @ " " @ %srcList);
        return error;
    }
    if ((0.0 > %list.size())) {
    }
    if (!(listAdded)) {
        listAdded = %this @ curListName @ %this @ 1 @ %this @ curListName @ %this;
        if (%this[%this @ curListName]) {
            %collapsed = "+";
            $UserPref::emotes::collapsedLists;
        }
        %collapsed = "- ";
        %listTitle = %this[%this @ curListName];
        $gEmoteListTitles;
        %titleLine = "<color:ffffff><linkcolor:ffffff><spush><linkcolor:f5b9ff><b><a:gamelink list " @ %this @ curListName @ ">" @ %collapsed @ %listTitle @ "</a><spop>";
        %this.setText(%this.getText() @ %titleLine @ "<br>");
    }
    if (!(%this[%this @ curListName])) {
        %list.forEach("addToEmotesList");
    }
};
function StringMap::addToEmotesList(%this, %key, %value) {
    EmoteHudList @ EmoteHudList @ getText() @ %value @ "<br>".setText();
};
function EmoteHudList::scrollToPos(%this, %pos) {
    %this.getParent().scrollTo(0, (getWord(%pos, 1) - 1.0));
};
function EmoteHudList::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((getWord(%url, 0) $= "set_mood")) {
        setMood(findWord($gMoods, getWord(%url, 1)));
        populateLists();
    }
    if ((EmoteHudList SPC getWord(%url, 0) $= "emote")) {
        sendAnimToServer(getWords(%url, 1));
    }
    if ((getWord(%url, 0) $= "list")) {
        %listName = getWords(%url, 1);
        %listName[$UserPref::emotes::collapsedLists @ %listName] = !(%listName[$UserPref::emotes::collapsedLists @ %listName]);
        populateLists();
    }
    if ((EmoteHudList SPC getWord(%url, 0) $= "bindemote")) {
        currentEmote = getWords(%url, 1) @ %this;
        %this.setText("<linkcolor:ffffff>" @ "Binding <spush><b><color:e553ff>" @ %this @ currentEmote @ "<spop>...<br>" @ "<br>" @ "Type a hotkey below or choose one from the list.<br>" @ "<br>");
        %bindings = "F08 F09 F10 F11 F12 ctrl1 ctrl2 ctrl3 ctrl4 ctrl5 ctrl6 ctrl7 ctrl8 ctrl9 ctrl0";
        %count = getWordCount(%bindings);
        %i = 0;
        if ((%count < %i)) {
            %binding = getWord(%bindings, %i);
            %emote = %this.getEmoteForBinding(%binding);
            %rightStr = "";
            %leftStr = "<a:gamelink bindbinding " @ %binding @ ">[" @ %binding @ "]</a>";
            if ((%this $= currentEmote)) {
                %rightStr = %emote @ "<just:right><spush><b><color:e553ff>" @ %emote @ "<spop><just:left>";
                %leftStr = "[<spush><b><linkcolor:e553ff><linkcolorhl:f5b9ff><a:gamelink cancel>" @ %binding @ "</a><spop>]";
            }
            if (!(%emote $= "")) {
                %rightStr = "<just:right>" @ %emote @ "<just:left>";
            }
            %this.setText(%this.getText() @ %leftStr @ %rightStr @ "<br>");
            %i = (1.0 + %i);
        }
        %this.setText((%count < %i) @ %this.getText() @ "<br>");
        %binding = currentEmote.get();
        %this;
        if (!(EmoteBindingMap SPC %binding $= "")) {
            %this.setText(%this.getText() @ "<a:gamelink unbind " @ %binding @ ">[ Unbind " @ %binding @ " ]</a>  ");
        }
        %this.setText(%this.getText() @ "<just:right><a:gamelink cancel>[ Cancel ]</a><just:left>");
    }
    if ((getWord(%url, 0) $= "bindbinding")) {
        if (!(%this SPC currentEmote $= "")) {
            %this.doFunc(getWord(%url, 1));
        }
        warn("Tried to bind a key without current emote defined.");
    }
    if ((getWord(%url, 0) $= "unbind")) {
        if (!(%this SPC currentEmote $= "")) {
            %this.rebind(getWord(%url, 1), "");
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
    if ((EmoteBindingMap $= %emote.get())) {
        %this.reset();
        return %binding2;
    }
    %this.setEmoteForBinding(%emote.get(), "");
    %binding2[EmoteBindingMap @ $UserPref::emotes TAB $UserPref::Player::gender @ %binding2].remove();
    %binding2[EmoteBindingMap @ %emote @ $UserPref::emotes TAB $UserPref::Player::gender @ %binding2] = ;
    if (!(%emote $= "")) {
        %emote.put(%binding2);
    }
    if (!(EmoteBindingMap SPC %emote $= "")) {
        %this.setText("<spush><b><color:e553ff>[" @ %binding @ "]<spop> now maps to <spush><b><color:e553ff>" @ %emote @ "<spop>");
    }
    %this.setText("Removed binding for <spush><b><color:e553ff>[" @ %binding2 @ "]<spop>");
    timer = %this.schedule(1500, "reset") @ %this;
};
function EmoteHudList::doFunc(%this, %func) {
    if (!(%this SPC currentEmote $= "")) {
        %this.rebind(%func, currentEmote);
    }
    %anim = convertWordToAnim(%this.getEmoteForBinding(%func));
    %this;
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
