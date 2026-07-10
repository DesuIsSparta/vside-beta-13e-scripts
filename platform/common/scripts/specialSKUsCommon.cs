function getSpecialSKU(%player, %skuName) {
    if (isObject(%player)) {
    }
    %gender = "n";
    %player.getGender();
    return %skuName[$specialSKUs TAB %gender @ %skuName];
};
function Player::hasSpecialSku(%this, %skuName) {
    %sku = getSpecialSKU(%this, %skuName);
    if ((0.0 == %sku)) {
        return 0;
    }
    %hasIt = %this.hasActiveSKU(%sku);
    return %hasIt;
};
function getSkuShortName(%sku) {
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return %sku;
    }
    if ((%si SPC descShrt $= "")) {
        return %sku;
    }
    return descShrt;
};
