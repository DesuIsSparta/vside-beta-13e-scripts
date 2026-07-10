function connectLocal(%userName) {
    %c = new GameConnection(ServerConnection);;
    $Player::Name = %userName;
    %c.setCommonPreconnectClientSettings("");
    %c.connect("localhost:" @ $Pref::Net::Port);
};
function GameConnection::setCommonPreconnectClientSettings(%this, %teleTarget) {
    %this.setUser($Player::Name);
    %this.setToken($Token);
    %this.setAssetSet(AssetManager::getCurrentAssetSet());
    %this.setSkus(outfits_getCurrentSkus());
    %this.setTeleportTarget(%teleTarget);
    log("Network", "debug", getScopeName() @ " " @ "- setUser          :" @ " " @ $Player::Name);
    log("Network", "debug", getScopeName() @ " " @ "- setToken         :" @ " " @ $Token);
    log("Network", "debug", getScopeName() @ " " @ "- setAssetSet      :" @ " " @ AssetManager::getCurrentAssetSet());
    log("Network", "debug", getScopeName() @ " " @ "- setSkus          :" @ " " @ outfits_getCurrentSkus());
    log("Network", "debug", getScopeName() @ " " @ "- setTeleportTarget:" @ " " @ %teleTarget);
};
