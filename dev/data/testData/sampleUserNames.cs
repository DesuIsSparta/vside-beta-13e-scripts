$gRandomUserNamesNum = -1;
$gRandomUserNameIdx = 0;
function sampleData_initUserNames()
{
    if ($gRandomUserNamesNum < 0)
    {
        %fn = ExpandFilename("./sampleUserNames.txt");
        %fo = new FileObject();
        if (!%fo.openForRead(%fn))
        {
            error("could not open" SPC %fn);
            return ;
        }
        $gRandomUserNamesNum = 0;
        while (!%fo.isEOF())
        {
            %name = %fo.readLine();
            $gRandomUserNames[$gRandomUserNamesNum] = %name ;
            $gRandomUserNamesNum = $gRandomUserNamesNum + 1;
        }
        %fo.close();
        %fo.delete();
        echo("read" SPC $gRandomUserNamesNum SPC "names..");
    }
    return ;
}
function getRandomUserName()
{
    sampleData_initUserNames();
    %num = getRandom(0, $gRandomUserNamesNum - 1);
    return $gRandomUserNames[%num];
}
function getSequentialUserName()
{
    sampleData_initUserNames();
    %ret = $gRandomUserNames[$gRandomUserNameIdx];
    $gRandomUserNameIdx = ($gRandomUserNameIdx + 1) % $gRandomUserNamesNum;
    return %ret;
}
$gRandomBannerIDsNum = -1;
$gSequentialBannerID = 0;
function sampleData_initBannerIDs()
{
    if ($gRandomBannerIDsNum < 0)
    {
        %fn = ExpandFilename("./sampleBannerIDs.txt");
        %fo = new FileObject();
        if (!%fo.openForRead(%fn))
        {
            error("could not open" SPC %fn);
            return ;
        }
        $gRandomBannerIDsNum = 0;
        while (!%fo.isEOF())
        {
            %name = %fo.readLine();
            $gRandomBannerIDs[$gRandomBannerIDsNum] = %name ;
            $gRandomBannerIDsNum = $gRandomBannerIDsNum + 1;
        }
        %fo.close();
        %fo.delete();
        echo("read" SPC $gRandomBannerIDsNum SPC "names..");
    }
    return ;
}
function getRandomBannerID()
{
    sampleData_initBannerIDs();
    %num = getRandom(0, $gRandomBannerIDsNum - 1);
    return $gRandomBannerIDs[%num];
}
function getSequentialBannerID()
{
    sampleData_initBannerIDs();
    %ret = $gRandomBannerIDs[$gSequentialBannerID];
    $gSequentialBannerID = ($gSequentialBannerID + 1) % $gRandomBannerIDsNum;
    return %ret;
}
$gRandomApartmentPhotoIDsNum = -1;
function getRandomApartmentPhotoID()
{
    if ($gRandomApartmentPhotoIDsNum < 0)
    {
        %fn = ExpandFilename("./sampleApartmentPhotoIDs.txt");
        %fo = new FileObject();
        if (!%fo.openForRead(%fn))
        {
            error("could not open" SPC %fn);
            return ;
        }
        $gRandomApartmentPhotoIDsNum = 0;
        while (!%fo.isEOF())
        {
            %name = %fo.readLine();
            $gRandomApartmentPhotoIDs[$gRandomApartmentPhotoIDsNum] = %name ;
            $gRandomApartmentPhotoIDsNum = $gRandomApartmentPhotoIDsNum + 1;
        }
        %fo.close();
        %fo.delete();
        echo("read" SPC $gRandomApartmentPhotoIDsNum SPC "names..");
    }
    %num = getRandom(0, $gRandomApartmentPhotoIDsNum - 1);
    return $gRandomApartmentPhotoIDs[%num];
}
