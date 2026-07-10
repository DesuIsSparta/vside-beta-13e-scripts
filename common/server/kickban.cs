function kick(%client)
{
    %msg = "\x03The admin has kicked" @ " " @ %client.name;
    messageAll('MsgAdminForce', %msg);
    if (!%client.isAIControlled())
    {
        BanList::add(%client.guid, %client.getAddress(), $Pref::Server::KickBanTime);
    }
    %client.delete("You have been kicked from this server");
    return;
}
function Ban(%client)
{
    %msg = "\x03The admin has banned" @ " " @ %client.name;
    messageAll('MsgAdminForce', %msg);
    if (!%client.isAIControlled())
    {
        BanList::add(%client.guid, %client.getAddress(), $Pref::Server::BanTime);
    }
    %client.delete("You have been banned from this server");
    return;
}
