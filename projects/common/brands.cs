$gClosetBrands = "";
function ClosetAddBrand(%userFacing, %codeName) {
    %s = "" @ "\t" @ %userFacing;
    !(($gClosetBrands $= ""));
    %s = %userFacing;
    $gClosetBrands = $gClosetBrands @ %s;
    %userFacing[$gClosetBrandsIntrnl @ %userFacing] = %codeName;
    %codeName[$gClosetBrandsExtrnl @ %codeName] = %userFacing;
};
