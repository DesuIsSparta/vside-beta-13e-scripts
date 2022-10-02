function onServerCreated()
{
    echo("in onServerCreated()");
    $Server::GameType = "Test App";
    $Server::MissionType = "Deathmatch";
    createGame();
    return ;
}
function onServerDestroyed()
{
    destroyGame();
    return ;
}
function onMissionLoaded()
{
    startGame();
    return ;
}
function onMissionEnded()
{
    endGame();
    return ;
}
function onMissionReset()
{
    return ;
}
function GameConnection::onClientEnterGame(%unused)
{
    return ;
}
function GameConnection::onClientLeaveGame(%unused)
{
    return ;
}
function createGame()
{
    return ;
}
function destroyGame()
{
    return ;
}
function startGame()
{
    return ;
}
function endGame()
{
    return ;
}
