$specialSKUs["f","flower"] = 27001;
$specialSKUs["m","flower"] = 17001;
$specialSKUs["f","helpmebadge"] = 30007;
$specialSKUs["m","helpmebadge"] = 30007;
$specialSKUs["n","helpmebadge"] = 30007;
$specialSKUs["f","guidebadge"] = 30003;
$specialSKUs["m","guidebadge"] = 30003;
$specialSKUs["n","guidebadge"] = 30003;
$specialSKUs["d","guidebadge"] = 30003;
$specialSKUs["f","hostbadge"] = 30013;
$specialSKUs["m","hostbadge"] = 30013;
$specialSKUs["n","hostbadge"] = 30013;
$specialSKUs["d","hostbadge"] = 30013;
$specialSKUs["f","cohostbadge"] = 30014;
$specialSKUs["m","cohostbadge"] = 30014;
$specialSKUs["n","cohostbadge"] = 30014;
$specialSKUs["d","cohostbadge"] = 30014;
$specialSKUs["f","seniorguidebadge"] = 30008;
$specialSKUs["m","seniorguidebadge"] = 30008;
$specialSKUs["n","seniorguidebadge"] = 30008;
$specialSKUs["d","seniorguidebadge"] = 30008;
$specialSKUs["f","microphone"] = 30002;
$specialSKUs["m","microphone"] = 30002;
$specialSKUs["n","microphone"] = 30002;
$specialSKUs["d","microphone"] = 30002;
$specialSKUs["f","microphoneMesh"] = 27002;
$specialSKUs["m","microphoneMesh"] = 17002;
$specialSKUs["noBackendVet"] = "";
$specialSKUs["noBackendVet"] = $specialSKUs["noBackendVet"] SPC 30007 ;
$specialSKUs["noBackendVet"] = $specialSKUs["noBackendVet"] SPC 30013 ;
$specialSKUs["noBackendVet"] = $specialSKUs["noBackendVet"] SPC 30014 ;
$specialSKUs["noBackendVet"] = $specialSKUs["noBackendVet"] SPC 30002 ;
$specialSKUs["noBackendVet"] = $specialSKUs["noBackendVet"] SPC 27002 ;
$specialSKUs["noBackendVet"] = $specialSKUs["noBackendVet"] SPC 17002 ;
$specialSKUs["noBackendVet"] = trim($specialSKUs["noBackendVet"]) ;
$specialSKUs["tickerPri0"] = 58000;
$specialSKUs["tickerPri1"] = 58001;
$specialSKUs["tickerPri2"] = 58002;
function getSpecialSKU(%player, %skuName)
{
    %gender = isObject(%player) ? %player.getGender() : "n";
    return $specialSKUs[%gender,%skuName];
}
function Player::hasSpecialSku(%this, %skuName)
{
    %sku = getSpecialSKU(%this, %skuName);
    if (%sku == 0)
    {
        return 0;
    }
    %hasIt = %this.hasActiveSKU(%sku);
    return %hasIt;
}
function getSkuShortName(%sku)
{
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        return %sku;
    }
    if (%si.descShrt $= "")
    {
        return %sku;
    }
    return %si.descShrt;
}
