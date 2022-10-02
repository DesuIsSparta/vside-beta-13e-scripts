function admin::getFormattedClassName(%classname)
{
    if (%classname $= "AIPlayer")
    {
        return "npc   ";
    }
    else
    {
        if (%classname $= "Player")
        {
            return "player";
        }
        else
        {
            if (%classname $= "special")
            {
                return "*     ";
            }
            else
            {
                return %classname;
            }
        }
    }
    return ;
}
function admin::isActionable(%obj, %action)
{
    if (%action $= "Boot")
    {
        return isPlayerObject(%obj);
    }
    else
    {
        if (%action $= "BootQuiet")
        {
            return isPlayerObject(%obj);
        }
        else
        {
            if (%action $= "Ban")
            {
                if (!isPlayerObject(%obj))
                {
                    return 0;
                }
                else
                {
                    return !isAIPlayerObject(%obj);
                }
            }
            else
            {
                if (%action $= "Message")
                {
                    if (%obj == 0)
                    {
                        return 1;
                    }
                    if (!isPlayerObject(%obj))
                    {
                        return 0;
                    }
                    if (isAIPlayerObject(%obj))
                    {
                        return 0;
                    }
                    return 1;
                }
                else
                {
                    if (%action $= "Summon")
                    {
                        return isPlayerObject(%obj);
                    }
                    else
                    {
                        if (%action $= "Snoop Toggle")
                        {
                            return isPlayerObject(%obj);
                        }
                        else
                        {
                            if (%action $= "Respawn")
                            {
                                return isPlayerObject(%obj);
                            }
                            else
                            {
                                if (%action $= "Throw Voice")
                                {
                                    return isAIPlayerObject(%obj);
                                }
                                else
                                {
                                    if (%action $= "Teleport To")
                                    {
                                        return isPlayerObject(%obj);
                                    }
                                    else
                                    {
                                        if (%action $= "Fly To")
                                        {
                                            return isPlayerObject(%obj);
                                        }
                                        else
                                        {
                                            if (%action $= "Track")
                                            {
                                                return isPlayerObject(%obj);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return 0;
}
function admin::getTargetName(%shape)
{
    if (isObject(%shape))
    {
        return %shape.getShapeName();
    }
    else
    {
        return "everyone";
    }
    return ;
}
function admin::composeSystemMessage(%target, %message, %unused)
{
    %msg = "";
    %msg = %msg @ "system message to" SPC admin::getTargetName(%target) @ ":";
    %msg = %msg @ "\n";
    %msg = %msg @ %message;
    return %msg;
}
