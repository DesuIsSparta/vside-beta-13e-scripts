function connectLocal(%userName) {
    %c = new GameConnection(ServerConnection);
    $Player::Name = %userName;
    "".setCommonPreconnectClientSettings(%c);
    "localhost:" @ $Pref::Net::Port.connect(%c);
};
function GameConnection::setCommonPreconnectClientSettings(%this, %teleTarget) {
    $Player::Name.setUser(%this);
    $Token.setToken(%this);
    AssetManager::getCurrentAssetSet().setAssetSet(%this);
    outfits_getCurrentSkus().setSkus(%this);
    %teleTarget.setTeleportTarget(%this);
    log("Network", "debug", getScopeName() @ " " @ "- setUser          :" @ " " @ $Player::Name);
    log("Network", "debug", getScopeName() @ " " @ "- setToken         :" @ " " @ $Token);
    log("Network", "debug", getScopeName() @ " " @ "- setAssetSet      :" @ " " @ AssetManager::getCurrentAssetSet());
    log("Network", "debug", getScopeName() @ " " @ "- setSkus          :" @ " " @ outfits_getCurrentSkus());
    log("Network", "debug", getScopeName() @ " " @ "- setTeleportTarget:" @ " " @ %teleTarget);
};
