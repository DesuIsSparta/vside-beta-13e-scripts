function Player::hasMicrophone(%this) {
    return %this.getActiveSKUs().hasSkuWithTag("microphone");
};
