$testOutfits::timeout = 1000;
$testOutfits::dataOutfit["f","stock"] = "5400 5527 5600 5702 5850 5875 5900 5950 5980";
$testOutfits::dataOutfit["m","stock"] = "400 554 600 701 850 875 900 950";
$testOutfits::dataBody["f","stock"] = "5100 5200 5303 5801";
$testOutfits::dataBody["m","stock"] = "121 200 303 801";
$testOutfits::dataOutfit["f","nonStock"] = "5401 5523 5650 5716 5851 5876 5901 5951 5980";
$testOutfits::dataOutfit["m","nonStock"] = "401 555 633 710 853 877 911 952";
$testOutfits::dataBody["f","nonStock"] = "5133 5203 5302 5803";
$testOutfits::dataBody["m","nonStock"] = "122 201 302 803";
$testOutfits::dataOutfit["f","santaItem"] = "5401 5585 5616 5716 5851 5876 5901 5951 5980";
$testOutfits::dataOutfit["m","santaItem"] = "401 1205 633 710 853 877 911 952";
$testOutfits::dataBody["f","santaItem"] = "5195 5203 5302 5803";
$testOutfits::dataBody["m","santaItem"] = "176 201 302 803";
$testOutfits::dataOutfit["f","otherGender"] = "401 555 633 710 853 877 911 952";
$testOutfits::dataOutfit["m","otherGender"] = "5401 5523 5616 5716 5851 5876 5901 5951 5980";
$testOutfits::dataBody["f","otherGender"] = "122 201 302 803";
$testOutfits::dataBody["m","otherGender"] = "5133 5203 5302 5803";
$testOutfits::dataOutfit["f","staff"] = "5401 5557 5654 5716 5851 5876 5901 5951 5980";
$testOutfits::dataOutfit["m","staff"] = "401 570 633 710 853 877 911 952 981";
$testOutfits::dataBody["f","staff"] = "5133 5203 5303 5805";
$testOutfits::dataBody["m","staff"] = "122 201 302 806";
$testOutfits::dataOutfit["f","stock"][$testOutfits::dataOutfit["f","stock"] @ " " @ 27002 @ $testOutfits::dataOutfit TAB "f" @ "microphone"] = ;
$testOutfits::dataOutfit["m","stock"][$testOutfits::dataOutfit["m","stock"] @ " " @ 30002 @ $testOutfits::dataOutfit TAB "m" @ "microphone"] = ;
$testOutfits::dataBody["f","stock"][$testOutfits::dataBody["f","stock"] @ $testOutfits::dataBody TAB "f" @ "microphone"] = ;
$testOutfits::dataBody["m","stock"][$testOutfits::dataBody["m","stock"] @ $testOutfits::dataBody TAB "m" @ "microphone"] = ;
$testOutfits::dataOutfit["f","owned"] = "5400 5570 5600 5702 5850 5875 5900 5950 5980";
$testOutfits::dataOutfit["m","owned"] = "400 575 600 701 850 875 900 950";
$testOutfits::dataBody["f","stock"][$testOutfits::dataBody["f","stock"] @ $testOutfits::dataBody TAB "f" @ "owned"] = ;
$testOutfits::dataBody["m","stock"][$testOutfits::dataBody["m","stock"] @ $testOutfits::dataBody TAB "m" @ "owned"] = ;
$testOutfits::timer = 0;
$testOutfits::badSkus = 12345;
$testOutfits::quitWhenDone = 1;
function testOutfits_MasterSetQuit(%quitWhenDone) {
    $testOutfits::quitWhenDone = %quitWhenDone;
};
function testOutfits_MasterNoQuit() {
    testOutfits_MasterSetQuit(0);
    testOutfits_Master();
};
function testOutfits_Master() {
    if (($Token $= "")) {
        error(getScopeName() @ " " @ "- must be logged into envManager. aborting tests.");
        return;
    }
    $testOutfits_testCount = 0;
    $testOutfits_passCount = 0;
    $testOutfits_nextTest = 0;
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManagerOwned";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvServerMicrophone";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManagerMicrophone";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvServerSantaItem";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManagerSantaItem";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvServerOtherGender";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManagerOtherGender";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvServerAlmostEmpty";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManagerAlmostEmpty";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvServerEmpty";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManagerEmpty";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvServerStaff";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManagerStaff";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvServer";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    $testOutfits_test[$testOutfits_testCount] = "testOutfits_UpToEnvManager";
    $testOutfits_testCount = ($testOutfits_testCount + 1.0);
    %n = 0;
    while ((%n < $testOutfits_testCount)) {
        $testOutfits_result[%n] = "NA  ";
        %n = (%n + 1.0);
    }
    testOutfits_MasterDoNext();
};
function testOutfits::getBodyAndOutfitSkus(%setName) {
    %gender = $player.getGender();
    %skus = $testOutfits::dataOutfit[%gender,%setName];
    %skus = %skus[" ",$testOutfits::dataBody,%gender,%setName];
    return %skus;
};
function testOutfits::skuListsAreEqual(%skusA, %skusB) {
    %skusA = SortNumbers(%skusA);
    %skusB = SortNumbers(%skusB);
    return (%skusA $= %skusB);
};
function testOutfits::skuListHasDuplicates(%skus) {
    %skus = SortNumbers(%skus);
    %n = (getWordCount(%skus) - 2.0);
    while ((%n >= 0.0)) {
        if ((getWord(%skus, %n) $= getWord(%skus, (%n + 1.0)))) {
            return 1;
        }
        %n = (%n - 1.0);
    }
    return 0;
};
function testOutfits_MasterDoNext() {
    cancel($testOutfits::timer);
    $testOutfits::timer = 0;
    if (($testOutfits_nextTest >= $testOutfits_testCount)) {
        echo("testOutfitsMaster() complete. User=" @ $player.getShapeName() @ " " @ "Gender=" @ $player.getGender() @ " " @ "Roles=" @ roles::getRoleStrings($player.getRolesMask()));
        %n = 0;
        while ((%n < $testOutfits_testCount)) {
            %level = ($testOutfits_result[%n] $= "pass") ? "info" : "error";
            log("network", %level, "testOutfitsMaster()" @ " " @ $testOutfits_result[%n] @ ":" @ " " @ $testOutfits_test[%n]);
            %n = (%n + 1.0);
        }
        %level = ($testOutfits_testCount == $testOutfits_passCount) ? "info" : "warn";
        (%n < $testOutfits_testCount);
        log("network", %level, "testOutfitsMaster() results:" @ " " @ $testOutfits_passCount @ " " @ "of" @ " " @ $testOutfits_testCount @ " " @ "passed," @ " " @ ($testOutfits_testCount - $testOutfits_passCount) @ " " @ "failed.");
        if ($testOutfits::quitWhenDone) {
            quit();
        }
    } else {
        call($testOutfits_test[$testOutfits_nextTest]);
        $testOutfits_nextTest = ($testOutfits_nextTest + 1.0);
    }
};
function testOutfits_TestCatch(%testname, %skusSent, %skusExpected, %timeout) {
    %skusGot = $player.getActiveSKUs();
    %skusGot = SortNumbers(%skusGot);
    %skusSent = SortNumbers(%skusSent);
    %skusExpected = SortNumbers(%skusExpected);
    %succ = testOutfits::skuListsAreEqual(%skusGot, %skusExpected);
    if ((%skusGot $= $testOutfits::badSkus)) {
        %noChange = "(no change)";
    } else {
        %noChange = "";
    }
    if (%succ) {
        log("network", "info", "outfit test succeeded after" @ " " @ %timeout @ "ms:" @ " " @ %testname @ ".");
        $testOutfits_passCount = ($testOutfits_passCount + 1.0);
        $testOutfits_nextTest[$testOutfits_result @ ($testOutfits_nextTest - 1.0)] = "pass";
    } else {
        log("network", "warn", "outfit test failed    after" @ " " @ %timeout @ "ms:" @ " " @ %testname @ "." @ " " @ %noChange);
        log("network", "warn", "sent    " @ " " @ %skusSent);
        log("network", "warn", "got     " @ " " @ %skusGot);
        log("network", "warn", "expected" @ " " @ %skusExpected);
        $testOutfits_passCount = ($testOutfits_passCount + 0.0);
        $testOutfits_nextTest[$testOutfits_result @ ($testOutfits_nextTest - 1.0)] = "fail";
    }
    testOutfits_MasterDoNext();
};
function testOutfits_FireEnvServerTest(%skusDry, %skusWet) {
    $player.setActiveSKUs($testOutfits::badSkus);
    commandToServer('SetActiveSkus', %skusDry);
    $testOutfits::timer = schedule($testOutfits::timeout, 0, "testOutfits_TestCatch", $testOutfits_test[$testOutfits_nextTest], %skusDry, %skusWet, $testOutfits::timeout);
};
function testOutfits_FireEnvManagerTest(%skusDry, %skusWet) {
    $player.setActiveSKUs($testOutfits::badSkus);
    SaveOutfitAndBodySkusAsCurrent(%skusDry);
    $testOutfits::timer = schedule($testOutfits::timeout, 0, "testOutfits_TestCatch", $testOutfits_test[$testOutfits_nextTest], %skusDry, %skusWet, $testOutfits::timeout);
};
function testOutfits_UpToEnvServer() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("nonStock");
    %skusWet = %skusDry;
    testOutfits_FireEnvServerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManager() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("nonStock");
    %skusWet = %skusDry;
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvServerStaff() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("staff");
    %skusWet = SkuManager.filterSkusRoles(%skusDry, $player.getRolesMask());
    %skusWet = SkuManager.overlaySkus(testOutfits::getBodyAndOutfitSkus("stock"), %skusWet);
    testOutfits_FireEnvServerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManagerStaff() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("staff");
    %skusWet = SkuManager.filterSkusRoles(%skusDry, $player.getRolesMask());
    %skusWet = SkuManager.overlaySkus(testOutfits::getBodyAndOutfitSkus("stock"), %skusWet);
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvServerEmpty() {
    %skusDry = "";
    %skusWet = testOutfits::getBodyAndOutfitSkus("stock");
    testOutfits_FireEnvServerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManagerEmpty() {
    %skusDry = "";
    %skusWet = testOutfits::getBodyAndOutfitSkus("stock");
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvServerAlmostEmpty() {
    %skusDry = getWord(testOutfits::getBodyAndOutfitSkus("stock"), 0);
    %skusWet = testOutfits::getBodyAndOutfitSkus("stock");
    testOutfits_FireEnvServerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManagerAlmostEmpty() {
    %skusDry = getWord(testOutfits::getBodyAndOutfitSkus("stock"), 0);
    %skusWet = testOutfits::getBodyAndOutfitSkus("stock");
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvServerOtherGender() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("otherGender");
    %skusWet = testOutfits::getBodyAndOutfitSkus("stock");
    testOutfits_FireEnvServerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManagerOtherGender() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("otherGender");
    %skusWet = testOutfits::getBodyAndOutfitSkus("stock");
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvServerSantaItem() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("santaItem");
    %skusWet = SkuManager.filterSkusRoles(%skusDry, $player.getRolesMask());
    %skusWet = SkuManager.overlaySkus(testOutfits::getBodyAndOutfitSkus("stock"), %skusWet);
    testOutfits_FireEnvServerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManagerSantaItem() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("santaItem");
    %skusWet = SkuManager.filterSkusRoles(%skusDry, $player.getRolesMask());
    %skusWet = SkuManager.overlaySkus(testOutfits::getBodyAndOutfitSkus("stock"), %skusWet);
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvServerMicrophone() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("microphone");
    %skusWet = SkuManager.filterSkusRoles(%skusDry, $player.getRolesMask());
    %skusWet = SkuManager.overlaySkus(testOutfits::getBodyAndOutfitSkus("stock"), %skusWet);
    testOutfits_FireEnvServerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManagerMicrophone() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("microphone");
    %skusWet = SkuManager.filterSkusRoles(%skusDry, $player.getRolesMask());
    %skusWet = SkuManager.overlaySkus(testOutfits::getBodyAndOutfitSkus("stock"), %skusWet);
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_UpToEnvManagerOwned() {
    %skusDry = testOutfits::getBodyAndOutfitSkus("owned");
    %skusWet = SkuManager.filterSkusInList(%skusDry, $Player::inventory);
    %skusWet = SkuManager.overlaySkus(testOutfits::getBodyAndOutfitSkus("stock"), %skusWet);
    testOutfits_FireEnvManagerTest(%skusDry, %skusWet);
};
function testOutfits_dumpSkusInteresting() {
    %skus = SkuManager.getSkus();
    testOutfits_dumpSkusRoles(%skus, "");
    testOutfits_dumpSkusRoles(%skus, "staff");
    testOutfits_dumpSkusRoles(%skus, "moderator");
};
function testOutfits_dumpSkusRoles(%skus, %roleStrings) {
    %indnt = "                    ";
    %delim = ", ";
    %roles = roles::getRolesMaskFromStrings(%roleStrings);
    %skus = SkuManager.filterSkusRoles(%skus, %roles, 1);
    %skus = SkuManager.filterSkusBornWith(%skus, 1);
    %skus = putInSets(%skus, %delim, %indnt, 10);
    echo("\n" @ %indnt @ "//Role Skus: (ie born with = 1, roles = " @ %roleStrings @ ")\n" @ %skus);
};
function putInSets(%nums, %delim, %indent, %setSize) {
    %nums = SortNumbers(%nums);
    %out = %indent;
    %d = "";
    %inum = getWordCount(%nums);
    %prevNum = getWord(%nums, 0);
    %i = 0;
    while ((%i < %inum)) {
        %num = getWord(%nums, %i);
        %dif = (%num - %prevNum);
        if ((%dif >= %setSize)) {
            %out = %out @ %delim @ "\n";
            %prevNum = %num;
            %d = %indent;
        }
        %out = %out @ %d @ %num;
        %d = %delim;
        %i = (%i + 1.0);
    }
    return %out;
};
function testOutfits_dumpSkusStaff() {
};
function testOutfits_dumpSkusMod() {
};
