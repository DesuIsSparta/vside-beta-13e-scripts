function kick(%client) {
    %msg = %client @ name;
    "\x03The admin has kicked" @ " ";
    messageAll('MsgAdminForce', %msg);
    BanList::add(guid, %client.getAddress(), $Pref::Server::KickBanTime);
    %client.delete("You have been kicked from this server");
    return %client;
};
function Ban(%client) {
    %msg = %client @ name;
    "\x03The admin has banned" @ " ";
    messageAll('MsgAdminForce', %msg);
    BanList::add(guid, %client.getAddress(), $Pref::Server::BanTime);
    %client.delete("You have been banned from this server");
    return %client;
};
