datablock PlayerData(DemoPlayer : PlayerM);
function DemoPlayer::onReachDestination(%this, %obj)
{
    if (!(%obj.Path $= ""))
    {
        if (%obj.currentNode == %obj.targetNode)
        {
            %this.onEndOfPath(%obj, %obj.Path);
        }
        else
        {
            %obj.moveToNextNode();
        }
    }
    return ;
}
function DemoPlayer::onEndOfPath(%this, %obj, %path)
{
    %obj.nextTask();
    return ;
}
function DemoPlayer::onEndSequence(%this, %obj, %slot)
{
    echo("Sequence Done!");
    %obj.stopThread(%slot);
    %obj.nextTask();
    return ;
}
function AIPlayer::spawn(%name, %spawnPoint)
{
    if (getRandom(0, 1) == 0)
    {
        %botDB = PlayerM;
    }
    else
    {
        %botDB = PlayerF;
    }
    %player = new AIPlayer()
    {
        dataBlock = %botDB;
        Path = "";
    };
    MissionCleanup.add(%player);
    %player.setShapeName(%name);
    %player.setTransform(%spawnPoint);
    %rand = getRandom(0, 2);
    if (%rand == 0)
    {
        %genre = "h";
    }
    else
    {
        if (%rand == 1)
        {
            %genre = "i";
        }
        else
        {
            if (%rand == 2)
            {
                %genre = "p";
            }
        }
    }
    %player.setGenre(%genre);
    return %player;
}
function AIPlayer::spawnOnPath(%name, %path)
{
    if (!isObject(%path))
    {
        return ;
    }
    %node = %path.getObject(0);
    %player = AIPlayer::spawn(%name, %node.getTransform());
    return %player;
}
function AIPlayer::followPath(%this, %path, %node)
{
    %this.stopThread(0);
    if (!isObject(%path))
    {
        %this.Path = "";
        return ;
    }
    if (%node > (%path.getCount() - 1))
    {
        %this.targetNode = %path.getCount() - 1;
    }
    else
    {
        %this.targetNode = %node;
    }
    if (%this.Path $= %path)
    {
        %this.moveToNode(%this.currentNode);
    }
    else
    {
        %this.Path = %path;
        %this.moveToNode(0);
    }
    return ;
}
function AIPlayer::moveToNextNode(%this)
{
    if ((%this.targetNode < 0) && (%this.currentNode < %this.targetNode))
    {
        if (%this.currentNode < (%this.Path.getCount() - 1))
        {
            %this.moveToNode(%this.currentNode + 1);
        }
        else
        {
            %this.moveToNode(0);
        }
    }
    else
    {
        if (%this.currentNode == 0)
        {
            %this.moveToNode(%this.Path.getCount() - 1);
        }
        else
        {
            %this.moveToNode(%this.currentNode - 1);
        }
    }
    return ;
}
function AIPlayer::moveToNode(%this, %index)
{
    %this.currentNode = %index;
    %node = %this.Path.getObject(%index);
    %this.setMoveDestination(%node.getTransform(), %index == %this.targetNode);
    return ;
}
function AIPlayer::pushTask(%this, %method)
{
    if (%this.taskIndex $= "")
    {
        %this.taskIndex = 0;
        %this.taskCurrent = -1;
    }
    %this.task[%this.taskIndex] = %method;
    %this.taskIndex = %this.taskIndex + 1;
    if (%this.taskCurrent == -1)
    {
        %this.executeTask(%this.taskIndex - 1);
    }
    return ;
}
function AIPlayer::clearTasks(%this)
{
    %this.taskIndex = 0;
    %this.taskCurrent = -1;
    return ;
}
function AIPlayer::nextTask(%this)
{
    if (%this.taskCurrent != -1)
    {
        if (%this.taskCurrent < (%this.taskIndex - 1))
        {
            %this.executeTask(%this.taskCurrent = %this.taskCurrent + 1);
        }
        else
        {
            %this.taskCurrent = -1;
        }
    }
    return ;
}
function AIPlayer::executeTask(%this, %index)
{
    %this.taskCurrent = %index;
    eval(%this.getId() @ "." @ %this.task[%index] @ ";");
    return ;
}
function AIPlayer::singleShot(%this)
{
    %this.setImageTrigger(0, 1);
    %this.setImageTrigger(0, 0);
    %this.Trigger = %this.schedule(%this.shootingDelay, singleShot);
    return ;
}
function AIPlayer::wait(%this, %time)
{
    %this.schedule(%time * 1000, "nextTask");
    return ;
}
function AIPlayer::done(%this, %time)
{
    %this.schedule(0, "delete");
    return ;
}
function AIPlayer::fire(%this, %bool)
{
    if (%bool)
    {
        cancel(%this.Trigger);
        %this.singleShot();
    }
    else
    {
        cancel(%this.Trigger);
    }
    %this.nextTask();
    return ;
}
function AIPlayer::aimAt(%this, %object)
{
    echo("Aim: " @ %object);
    %this.setAimObject(%object);
    %this.nextTask();
    return ;
}
function AIPlayer::animate(%this, %seq)
{
    %this.setActionThread(%seq);
    return ;
}
function AIPlayer::thinkETS(%this, %periodMS)
{
    %secondsBetweenEmotes = 20;
    %secondsBetweenMoves = 10;
    %secondsBetweenWords = 13;
    %secondsBetweenEavesdrops = 7;
    if ((%periodMS > getRandom(0, 1000 * %secondsBetweenEmotes)) && (%this.getMoveState() $= "stop"))
    {
        %this.doRandomEmote();
    }
    if (AIManager.BotsMove)
    {
        if (%periodMS > getRandom(0, 1000 * %secondsBetweenMoves))
        {
            %this.doAutoMoveEntry();
        }
    }
    if (AIManager.BotsBlahBlah)
    {
        if (%periodMS > getRandom(0, 1000 * %secondsBetweenWords))
        {
            %this.doBlahBlah(%this.botEavesdropTarget);
        }
    }
    if (AIManager.BotsEavesdrop)
    {
        if (%periodMS > getRandom(0, 1000 * %secondsBetweenEavesdrops))
        {
            %this.doEavesdropChange();
        }
    }
    return ;
}
function AIPlayer::doBlahBlah(%this, %target)
{
    if (AIManager.blahblahsNum == 0)
    {
        AIManager.blahblahsNum = 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "i am a bot !";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "do you like cheese ?";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = ".. yeah.";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "dancing is the BEST.";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "ASL ?";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "let\'s go dance.";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "cool!";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = ";)";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "this rocks.";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "where\'s the party ?";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "yawn zzzz..";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "nice outfit.";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "will you be my friend ?";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "lol !";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "rotfl !";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahs[AIManager.blahblahsNum] = "flirt";
        AIManager.blahblahsNum = AIManager.blahblahsNum + 1;
        AIManager.blahblahsNum = AIManager.blahblahsNum - 1;
    }
    %num = getRandom(1, AIManager.blahblahsNum);
    %msg = AIManager.blahblahs[%num];
    ServersideChatMessage(%this, %target, %msg);
    CONV_DEBUG("Bot" SPC %this SPC "said" SPC %msg SPC "(" SPC %num SPC "/" SPC AIManager.blahblahsNum SPC ")");
    return ;
}
function AIManager::BotsBlahBlahOneShot(%this, %player)
{
    %i = 0;
    while (%i < %this.numBots)
    {
        %this.bots[%i].doBlahBlah(%player);
        %i = %i + 1;
    }
}

function AIPlayer::doEavesdropChange(%this)
{
    %newEavesdropTarget = AIManager.bots[getRandom(0, AIManager.numBots - 1)];
    if (%newEavesdropTarget == %this)
    {
        %newEavesdropTarget = 0;
    }
    CONV_DEBUG("Bot" SPC %this SPC "Switching eavesdrop from" SPC %this.botEavesdropTarget SPC "to" SPC %newEavesdropTarget);
    serverSideEavesdrop(%this, %this.botEavesdropTarget, %newEavesdropTarget);
    CONV_DEBUG("Bot" SPC %this SPC "-Switched eavesdrop from" SPC %this.botEavesdropTarget SPC "to" SPC %newEavesdropTarget);
    %this.botEavesdropTarget = %newEavesdropTarget;
    return ;
}
function AIPlayer::doRandomEmote(%this)
{
    playRandomEmote(%this);
    return ;
}
function AIPlayer::doAutoMove(%this)
{
    %amount = 700;
    if (%this.autoMoveList[0] == 0)
    {
        %this.autoMoveList[0] = %amount SPC "0 0";
        %this.autoMoveList[1] = 0 SPC %amount SPC 0;
        %this.autoMoveList[2] = -%amount SPC "0 0";
        %this.autoMoveList[3] = 0 SPC -%amount SPC 0;
        %this.autoMoveNum = 0;
    }
    %move = %this.autoMoveList[%this.autoMoveNum];
    %this.applyImpulse("0 0 0", %move);
    %this.autoMoveNum = %this.autoMoveNum + 1;
    if (%this.autoMoveNum > 3)
    {
        %this.autoMoveNum = 0;
    }
    return ;
}
function AIManager::doBotsSurfing(%this, %periodMS)
{
    %secondsBetweenLeaveOrEntry = 3;
    %minBots = 20;
    %maxBots = 70;
    if (%periodMS < getRandom(0, 1000 * %secondsBetweenLeaveOrEntry))
    {
        return ;
    }
    %add = 1;
    if (%this.numBots <= %minBots)
    {
        %add = 1;
    }
    else
    {
        if (%this.numBots >= %maxBots)
        {
            %add = 0;
        }
        else
        {
            %add = getRandom(0, 1);
        }
    }
    if (%add == 1)
    {
        %this.addOneBot();
    }
    else
    {
        %this.delOneBot();
    }
    echo("NumBots is now" SPC %this.numBots);
    return ;
}
function AIManager::addOneBot(%this)
{
    echo("adding one bot...");
    EntrySpawn.spawnBots(1, 1);
    return ;
}
function AIManager::delOneBot(%this)
{
    if (%this.numBots <= 0)
    {
        return ;
    }
    echo("removing one bot...");
    %this.numBots = %this.numBots - 1;
    %this.bots[%this.numBots].delete();
    return ;
}
function AIManager::think(%this)
{
    %period = 500;
    %i = 0;
    while (%i < %this.numBots)
    {
        %this.bots[%i].thinkETS(%period);
        %i = %i + 1;
    }
    if (%this.BotsSurfing)
    {
        %this.doBotsSurfing(%period);
    }
    %this.schedule(%period, think);
    return ;
}
function AIManager::spawn(%this)
{
    %BotRows = 10;
    %BotCols = 5;
    %XPosition = -288;
    %YPosition = -410;
    %j = 0;
    while (%j < %BotRows)
    {
        %i = 0;
        while (%i < %BotCols)
        {
            %this.numBots = %this.numBots + 1;
            %player = AIPlayer::spawn("hi!" SPC %this.numBots, %XPosition SPC %YPosition SPC "216 0 0 1 3.14");
            %XPosition = %XPosition + 1;
            %i = %i + 1;
        }
        %YPosition = %YPosition + 1;
        %XPosition = %XPosition - (1 * %BotCols);
        if ((%YPosition % 2) == 1)
        {
            %XPosition = %XPosition - 0.5;
        }
        else
        {
            %XPosition = %XPosition + 0.5;
        }
        %j = %j + 1;
    }
    return %player;
}
function AIManager::SpawnETS(%this, %transform)
{
    if (!%this.numBots)
    {
        %this.numBots = 0;
    }
    %name = "";
    %name = "bot" @ %this.numBots + 1;
    %player = AIPlayer::spawn(%name, %transform);
    %this.bots[%this.numBots] = %player;
    %this.numBots = %this.numBots + 1;
    %player.gender = getSubStr(%player.getDataBlock().possibleGenders, 0, 1);
    %player.randomizeOutfit();
    %rand = getRandom(0, 2);
    %player.setGenre(getSubStr(%player.getDataBlock().possibleGenres, %rand, 1));
    %player.botChatTarget = 0;
    %player.setAwayMessage(getRandomAwayMessage());
    PlayerDict.put(%name, %player);
    %player.MeshOff(%player.gender @ ".headphones.dj");
    echo("bot entered:   \c2" SPC getDebugString(%player));
    return %player;
}
function AIManager::SpawnArmyETS(%this, %transform)
{
    %delt = 1.5;
    %BotRows = 3;
    %BotCols = 3;
    %posX = getWord(%transform, 0) - (%delt * mFloor(0.5 * %BotCols));
    %posY = getWord(%transform, 1) - (%delt * mFloor(0.5 * %BotRows));
    %posZ = getWord(%transform, 2) + 3;
    %j = 0;
    while (%j < %BotRows)
    {
        %i = 0;
        while (%i < %BotCols)
        {
            %trans = %posX SPC %posY SPC %posZ SPC getWords(%transform, 3, 6);
            if ((%i != mFloor(%BotCols / 2)) && (%j != mFloor(%BotRows / 2)))
            {
                %this.SpawnETS(%trans);
            }
            %posX = %posX + %delt;
            %i = %i + 1;
        }
        %posY = %posY + %delt;
        %posX = %posX - (%delt * %BotCols);
        %j = %j + 1;
    }
}

function AIManager::RandomizeBots(%this)
{
    %i = 0;
    while (%i < %this.numBots)
    {
        %this.bots[%i].randomizeOutfit();
        %rand = getRandom(0, 2);
        %this.bots[%i].setGenre(getSubStr(%this.bots[%i].getDataBlock().possibleGenres, %rand, 1));
        %this.bots[%i].setAwayMessage(getRandomAwayMessage());
        %i = %i + 1;
    }
}

function ServerCmdNextToonModeBots(%client)
{
    %i = 0;
    while (%i < AIManager.numBots)
    {
        %clientBotID = %client.getGhostID(AIManager.bots[%i]);
        commandToClient(%client, 'matchToonModeToPlayer', %clientBotID);
        %i = %i + 1;
    }
}

function AIManager::BotsStress(%this, %val)
{
    %this.BotsMove = %val;
    %this.BotsBlahBlah = %val;
    %this.BotsEavesdrop = %val;
    %this.BotsSurfing = %val;
    return ;
}
$randomAwayMessagesNum = 0;
function getRandomAwayMessage()
{
    if ($randomAwayMessagesNum == 0)
    {
        $randomAwayMessage[$randomAwayMessagesNum] = "I\'m cooking dinner.";
        $randomAwayMessagesNum = $randomAwayMessagesNum + 1;
        $randomAwayMessage[$randomAwayMessagesNum] = "I\'m on a date.";
        $randomAwayMessagesNum = $randomAwayMessagesNum + 1;
        $randomAwayMessage[$randomAwayMessagesNum] = "away.";
        $randomAwayMessagesNum = $randomAwayMessagesNum + 1;
        $randomAwayMessage[$randomAwayMessagesNum] = "lost.";
        $randomAwayMessagesNum = $randomAwayMessagesNum + 1;
        $randomAwayMessage[$randomAwayMessagesNum] = $Pref::Player::defaultAwayMessage ;
        $randomAwayMessagesNum = $randomAwayMessagesNum + 1;
    }
    return $randomAwayMessage[getRandom(0, $randomAwayMessagesNum - 1)];
}
