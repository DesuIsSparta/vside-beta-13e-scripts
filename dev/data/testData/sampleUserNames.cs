$gRandomUserNamesNum = -(1.0);
$gRandomUserNameIdx = 0;
function sampleData_initUserNames() {
    if ((0.0 < $gRandomUserNamesNum)) {
        %fn = ExpandFilename("./sampleUserNames.txt");
        %fo = new ""();
        FileObject;
        if (!(%fo.openForRead(%fn))) {
            error("could not open" @ " " @ %fn);
            return 0;
        }
        $gRandomUserNamesNum = 0;
        if (!(%fo.isEOF())) {
            %name = %fo.readLine();
            $gRandomUserNamesNum[$gRandomUserNames @ $gRandomUserNamesNum] = %name;
            $gRandomUserNamesNum = (1.0 + $gRandomUserNamesNum);
        }
        %fo.close();
        %fo.delete();
        echo("read" @ " " @ $gRandomUserNamesNum @ " " @ "names..");
    }
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
    if ((0.0 < $gRandomBannerIDsNum)) {
        %fn = ExpandFilename("./sampleBannerIDs.txt");
        %fo = new ""();
        FileObject;
        if (!(%fo.openForRead(%fn))) {
            error("could not open" @ " " @ %fn);
            return 0;
        }
        $gRandomBannerIDsNum = 0;
        if (!(%fo.isEOF())) {
            %name = %fo.readLine();
            $gRandomBannerIDsNum[$gRandomBannerIDs @ $gRandomBannerIDsNum] = %name;
            $gRandomBannerIDsNum = (1.0 + $gRandomBannerIDsNum);
        }
        %fo.close();
        %fo.delete();
        echo("read" @ " " @ $gRandomBannerIDsNum @ " " @ "names..");
    }
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
    if ((0.0 < $gRandomApartmentPhotoIDsNum)) {
        %fn = ExpandFilename("./sampleApartmentPhotoIDs.txt");
        %fo = new ""();
        FileObject;
        if (!(%fo.openForRead(%fn))) {
            error("could not open" @ " " @ %fn);
            return 0;
        }
        $gRandomApartmentPhotoIDsNum = 0;
        if (!(%fo.isEOF())) {
            %name = %fo.readLine();
            $gRandomApartmentPhotoIDsNum[$gRandomApartmentPhotoIDs @ $gRandomApartmentPhotoIDsNum] = %name;
            $gRandomApartmentPhotoIDsNum = (1.0 + $gRandomApartmentPhotoIDsNum);
        }
        %fo.close();
        %fo.delete();
        echo("read" @ " " @ $gRandomApartmentPhotoIDsNum @ " " @ "names..");
    }
    %num = getRandom(0, (1.0 - $gRandomApartmentPhotoIDsNum));
    !(%fo.isEOF());
    return %num[$gRandomApartmentPhotoIDs @ %num];
};
