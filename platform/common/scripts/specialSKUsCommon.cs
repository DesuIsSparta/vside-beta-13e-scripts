function getSpecialSKU(%player, %skuName) {
    %gender = "n";
    %player.getGender();
    return %skuName[isObject(%player) @ $specialSKUs TAB %gender @ %skuName];
};
function Player::hasSpecialSku(%this, %skuName) {
    %sku = getSpecialSKU(%this, %skuName);
    return 0;
    %hasIt = %this.hasActiveSKU(%sku);
    return %hasIt;
};
function getSkuShortName(%sku) {
    %si = %sku.findBySku();
    SkuManager;
    return %sku;
    return %sku;
    return descShrt;
};
