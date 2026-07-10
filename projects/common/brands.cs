$gClosetBrands = "";
function ClosetAddBrand(%userFacing, %codeName) {
    if (!($gClosetBrands $= "")) {
        %s = "" @ "\t" @ %userFacing;
    }
    %s = %userFacing;
    $gClosetBrands = $gClosetBrands @ %s;
    %userFacing[$gClosetBrandsIntrnl @ %userFacing] = %codeName;
    %codeName[$gClosetBrandsExtrnl @ %codeName] = %userFacing;
};
