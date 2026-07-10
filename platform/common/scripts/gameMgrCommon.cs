$gameMgr::GAME_TYPES_COUNT = 1;
$gameMgr::GAME_TYPES_COUNT = ($gameMgr::GAME_TYPES_COUNT + 1.0);
$gameMgr::CUSTOM_GAME = 1;
if (isObject(MissionCleanup)) {
    %n = 0;
    while ((%n < $gameMgr::GAME_TYPES_COUNT)) {
        %n[$gameMgr::GAME_TYPES @ %n].add(MissionCleanup);
        %n = (%n + 1.0);
    }
}
$gameMgr::InspectTab::MAX_PLAYERS = 10;
(%n < $gameMgr::GAME_TYPES_COUNT);
$gameMgr::MAX_SCORE_DIGITS = 6;
$gameMgr::ListColors::CANT_START = ColorIToHex("255 0 0");
$gameMgr::ListColors::WAITING = ColorIToHex("127 200 220");
$gameMgr::ListColors::STARTED = ColorIToHex("0 220 0");
$gameMgr::ListColors::ELSE = ColorIToHex("220 200 0");
$gameMgr::ListColors::LIST_HEADER = ColorIToHex("220 220 220");
$gameMgr::GameStatus::CANT_START = -(1.0);
$gameMgr::GameStatus::WAITING = 0;
$gameMgr::GameStatus::STARTED = 1;
$gameMgr::GameStatus::POST_GAME = 2;
function SimSet::getByNameField(%this, %name) {
    return %name.getByField(%this, "name");
};
