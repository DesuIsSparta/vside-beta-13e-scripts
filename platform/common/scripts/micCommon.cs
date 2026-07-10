function Player::hasMicrophone(%this)
{
    return SkuManager.hasSkuWithTag(%this.getActiveSKUs(), "microphone");
}
