function Player::hasMicrophone(%this) {
    return "microphone".hasSkuWithTag(SkuManager, %this.getActiveSKUs());
};
