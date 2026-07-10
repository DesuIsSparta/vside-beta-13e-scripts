$gRandomUserNamesNum = -(1.0);
$gRandomUserNameIdx = 0;
function sampleData_initUserNames() {
    %fn = ExpandFilename("./sampleUserNames.txt");
    (0.0 < $gRandomUserNamesNum);
    %fo = new ""();
    FileObject;
    error("could not open" @ " " @ %fn);
    return !(%fo.openForRead(%fn));
    $gRandomUserNamesNum = 0;
    %name = %fo.readLine();
    !(%fo.isEOF());
    $gRandomUserNamesNum[$gRandomUserNames @ $gRandomUserNamesNum] = %name;
    $gRandomUserNamesNum = (1.0 + $gRandomUserNamesNum);
    %fo.close();
    %fo.delete();
    echo("read" @ " " @ $gRandomUserNamesNum @ " " @ "names..");
};
function getRandomUserName() {
    sampleData_initUserNames();
    %num = getRandom(0, (1.0 - $gRandomUserNamesNum));
    return %num[$gRandomUserNames @ %num];
};
function getSequentialUserName() {
    sampleData_initUserNames();
    %ret = $gRandomUserNameIdx[$gRandomUserNames @ $gRandomUserNameIdx];
    $gRandomUserNameIdx = ($gRandomUserNamesNum % (1.0 + $gRandomUserNameIdx));
    return %ret;
};
$gRandomBannerIDsNum = -(1.0);
$gSequentialBannerID = 0;
function sampleData_initBannerIDs() {
    %fn = ExpandFilename("./sampleBannerIDs.txt");
    (0.0 < $gRandomBannerIDsNum);
    %fo = new ""();
    FileObject;
    error("could not open" @ " " @ %fn);
    return !(%fo.openForRead(%fn));
    $gRandomBannerIDsNum = 0;
    %name = %fo.readLine();
    !(%fo.isEOF());
    $gRandomBannerIDsNum[$gRandomBannerIDs @ $gRandomBannerIDsNum] = %name;
    $gRandomBannerIDsNum = (1.0 + $gRandomBannerIDsNum);
    %fo.close();
    %fo.delete();
    echo("read" @ " " @ $gRandomBannerIDsNum @ " " @ "names..");
};
function getRandomBannerID() {
    sampleData_initBannerIDs();
    %num = getRandom(0, (1.0 - $gRandomBannerIDsNum));
    return %num[$gRandomBannerIDs @ %num];
};
function getSequentialBannerID() {
    sampleData_initBannerIDs();
    %ret = $gSequentialBannerID[$gRandomBannerIDs @ $gSequentialBannerID];
    $gSequentialBannerID = ($gRandomBannerIDsNum % (1.0 + $gSequentialBannerID));
    return %ret;
};
$gRandomApartmentPhotoIDsNum = -(1.0);
function getRandomApartmentPhotoID() {
    %fn = ExpandFilename("./sampleApartmentPhotoIDs.txt");
    (0.0 < $gRandomApartmentPhotoIDsNum);
    %fo = new ""();
    FileObject;
    error("could not open" @ " " @ %fn);
    return !(%fo.openForRead(%fn));
    $gRandomApartmentPhotoIDsNum = 0;
    %name = %fo.readLine();
    !(%fo.isEOF());
    $gRandomApartmentPhotoIDsNum[$gRandomApartmentPhotoIDs @ $gRandomApartmentPhotoIDsNum] = %name;
    $gRandomApartmentPhotoIDsNum = (1.0 + $gRandomApartmentPhotoIDsNum);
    %fo.close();
    %fo.delete();
    echo("read" @ " " @ $gRandomApartmentPhotoIDsNum @ " " @ "names..");
    %num = getRandom(0, (1.0 - $gRandomApartmentPhotoIDsNum));
    !(%fo.isEOF());
    return %num[$gRandomApartmentPhotoIDs @ %num];
};
