$gClosetBrands = "";
function ClosetAddBrand(%userFacing, %codeName)
{
    if (!($gClosetBrands $= ""))
    {
        %s = "" TAB %userFacing;
    }
    else
    {
        %s = %userFacing;
    }
    $gClosetBrands = $gClosetBrands @ %s;
    $gClosetBrandsIntrnl[%userFacing] = %codeName ;
    $gClosetBrandsExtrnl[%codeName] = %userFacing ;
    return ;
}
