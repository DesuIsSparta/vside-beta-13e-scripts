// $gameMgr::GAME_TYPES_COUNT = 1;
// new ScriptObject()
// {
//     title = "the lounge race";
//     INST_TITLE = "a lounge race";
//     description = "A checkpoint race against your friends! First to get all the checkpoints in order wins!";
//     USER_CREATE = 0;
//     $gameMgr::GAME_TYPES;
// }
// $gameMgr::GAME_TYPES_COUNT = $gameMgr::GAME_TYPES_COUNT + 1;
// new ScriptObject()
// {
//     title = "custom game";
//     INST_TITLE = "a custom game";
//     description = "Whatever game you want to play. The host and players pick the goal, the rules, everything, and the host acts as referee, enforcing the rules, assigning points/player status, and deciding when the game is over. Use your imagination!";
//     USER_CREATE = 1;
//     $gameMgr::GAME_TYPES;
// }
// $gameMgr::CUSTOM_GAME = 1;
// if (isObject(MissionCleanup))
// {
//     %n = 0;
//     while (%n < $gameMgr::GAME_TYPES_COUNT)
//     {
//         MissionCleanup.add($gameMgr::GAME_TYPES[%n]);
//         %n = %n + 1;
//     }
// }
// $gameMgr::InspectTab::MAX_PLAYERS = 10;
// $gameMgr::MAX_SCORE_DIGITS = 6;
// $gameMgr::ListColors::CANT_START = ColorIToHex("255 0 0");
// $gameMgr::ListColors::WAITING = ColorIToHex("127 200 220");
// $gameMgr::ListColors::STARTED = ColorIToHex("0 220 0");
// $gameMgr::ListColors::ELSE = ColorIToHex("220 200 0");
// $gameMgr::ListColors::LIST_HEADER = ColorIToHex("220 220 220");
// $gameMgr::GameStatus::CANT_START = -1;
// $gameMgr::GameStatus::WAITING = 0;
// $gameMgr::GameStatus::STARTED = 1;
// $gameMgr::GameStatus::POST_GAME = 2;
// function SimSet::getByNameField(%this, %name)
// {
//     return %this.getByField("name", %name);
// }
