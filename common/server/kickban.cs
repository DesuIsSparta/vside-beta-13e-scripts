function kick(%client)
{
    %msg = "\c2The admin has kicked" SPC %client.name;
    messageAll('MsgAdminForce', %msg);
    if (!%client.isAIControlled())
    {
        BanList::add(%client.guid, %client.getAddress(), $Pref::Server::KickBanTime);
    }
    %client.delete("You have been kicked from this server");
    return ;
}
function Ban(%client)
{
    %msg = "\c2The admin has banned" SPC %client.name;
    messageAll('MsgAdminForce', %msg);
    if (!%client.isAIControlled())
    {
        BanList::add(%client.guid, %client.getAddress(), $Pref::Server::BanTime);
    }
    %client.delete("You have been banned from this server");
    return ;
}
