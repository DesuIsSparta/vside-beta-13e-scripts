function getSpecialSKU(%player, %skuName) {
    if (isObject(%player)) {
    }
    %gender = "n";
    %player.getGender();
    return %skuName[$specialSKUs TAB %gender @ %skuName];
};
function Player::hasSpecialSku(%this, %skuName) {
    %sku = getSpecialSKU(%this, %skuName);
    if ((%sku == 0.0)) {
        return 0;
    }
    %hasIt = %sku.hasActiveSKU(%this);
    return %hasIt;
};
function getSkuShortName(%sku) {
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return %sku;
    }
    if ((%si.descShrt $= "")) {
        return %sku;
    }
    return %si.descShrt;
};
