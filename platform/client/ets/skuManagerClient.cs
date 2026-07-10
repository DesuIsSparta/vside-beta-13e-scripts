function SkuManager::getRandomSkusForLocalPlayer(%this, %drawersList) {
    %skulist = $Player::inventory;
    %skulist = $player.getGender().filterSkusGender(%this, %skulist);
    return %drawersList.getRandomSkusFromList(%this, %skulist);
};
