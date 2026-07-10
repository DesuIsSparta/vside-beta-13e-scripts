$gClosetBrands = "";
function ClosetAddBrand(%userFacing, %codeName)
{
    if (!($gClosetBrands $= ""))
    {
        %s = "" @ "\t" @ %userFacing;
    }
    else
    {
        %s = %userFacing;
    }
    $gClosetBrands = $gClosetBrands @ %s;
    $gClosetBrandsIntrnl[%userFacing] = %codeName;
    $gClosetBrandsExtrnl[%codeName] = %userFacing;
}
