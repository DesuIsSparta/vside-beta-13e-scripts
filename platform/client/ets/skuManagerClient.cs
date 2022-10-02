function SkuManager::getRandomSkusForLocalPlayer(%this, %drawersList)
{
    %skulist = $Player::inventory;
    %skulist = %this.filterSkusGender(%skulist, $player.getGender());
    return %this.getRandomSkusFromList(%skulist, %drawersList);
}
