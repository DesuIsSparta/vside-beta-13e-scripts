function connectLocal(%userName)
{
    %c = new GameConnection(ServerConnection);
    $Player::Name = %userName;
    %c.setCommonPreconnectClientSettings("");
    %c.connect("localhost:" @ $Pref::Net::Port);
    return ;
}
function GameConnection::setCommonPreconnectClientSettings(%this, %teleTarget)
{
    %this.setUser($Player::Name);
    %this.setToken($Token);
    %this.setAssetSet(AssetManager::getCurrentAssetSet());
    %this.setSkus(outfits_getCurrentSkus());
    %this.setTeleportTarget(%teleTarget);
    log("Network", "debug", getScopeName() SPC "- setUser          :" SPC $Player::Name);
    log("Network", "debug", getScopeName() SPC "- setToken         :" SPC $Token);
    log("Network", "debug", getScopeName() SPC "- setAssetSet      :" SPC AssetManager::getCurrentAssetSet());
    log("Network", "debug", getScopeName() SPC "- setSkus          :" SPC outfits_getCurrentSkus());
    log("Network", "debug", getScopeName() SPC "- setTeleportTarget:" SPC %teleTarget);
    return ;
}
