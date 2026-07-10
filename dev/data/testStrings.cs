function testStringsMaster() {
    %testCount = 0;
    %passCount = 0;
    %testCount[%test @ %testCount] = "testIntToChar()";
    %testCount = (%testCount + 1.0);
    %testCount[%test @ %testCount] = "testUrlEncode()";
    %testCount = (%testCount + 1.0);
    %testCount[%test @ %testCount] = "testUrlDecode()";
    %testCount = (%testCount + 1.0);
    %testCount[%test @ %testCount] = "testCollapseWhiteSpace()";
    %testCount = (%testCount + 1.0);
    %testCount[%test @ %testCount] = "testFindUnit()";
    %testCount = (%testCount + 1.0);
    %n = 0;
    while ((%n < %testCount)) {
        eval("%succ =" @ " " @ %n[%test @ %n] @ ";");
        if (%succ) {
            echo("test passed:" @ " " @ %n[%test @ %n]);
            %passCount = (%passCount + 1.0);
        }
        error("test failed:" @ " " @ %n[%test @ %n]);
        %n = (%n + 1.0);
    }
    echo("testStringsMaster():" @ " " @ %passCount @ " " @ "of" @ " " @ %testCount @ " " @ "passed," @ " " @ (%testCount - %passCount) @ " " @ "failed.");
    return (%passCount == %testCount);
};
function testIntToChar() {
    %succ = 1;
    %n = 0;
    while ((%n < 256.0)) {
        %c = intToChar(%n);
        %m = charToInt(%c);
        %d = intToChar(%m);
        if (!(%c $= %d)) {
            error("testIntToChar() failed 1 at" @ " " @ %n);
            %succ = 0;
        }
        if ((%m != %n)) {
            error("testIntToChar() failed 2 at" @ " " @ %n);
            %succ = 0;
        }
        %n = (%n + 1.0);
    }
    return %succ;
};
function testUrlEncode() {
    %input = "";
    %correctOutput = "%20!%22%23%24%25%26'()*%2B%2C-.%2F0123456789%3A%3B%3C%3D%3E%3F%40ABCDEFGHIJKLMNOPQRSTUVWXYZ%5B%5C%5D%5E_%60abcdefghijklmnopqrstuvwxyz%7B%7C%7D~%A1%A2%A3%A4%A5%A6%A7%A8%A9%AA%AB%AC%AD%AE%AF%B0%B1%B2%B3%B4%B5%B6%B7%B8%B9%BA%BB%BC%BD%BE%BF%C0%C1%C2%C3%C4%C5%C6%C7%C8%C9%CA%CB%CC%CD%CE%CF%D0%D1%D2%D3%D4%D5%D6%D7%D8%D9%DA%DB%DC%DD%DE%DF%E0%E1%E2%E3%E4%E5%E6%E7%E8%E9%EA%EB%EC%ED%EE%EF%F0%F1%F2%F3%F4%F5%F6%F7%F8%F9%FA%FB%FC%FD%FE";
    %output = urlEncode(%input);
    return (%output $= %correctOutput);
};
function testUrlDecode() {
    %correctOutput = "";
    %input = "%20!%22%23%24%25%26'()*%2B%2C-.%2F0123456789%3A%3B%3C%3D%3E%3F%40ABCDEFGHIJKLMNOPQRSTUVWXYZ%5B%5C%5D%5E_%60abcdefghijklmnopqrstuvwxyz%7B%7C%7D~%A1%A2%A3%A4%A5%A6%A7%A8%A9%AA%AB%AC%AD%AE%AF%B0%B1%B2%B3%B4%B5%B6%B7%B8%B9%BA%BB%BC%BD%BE%BF%C0%C1%C2%C3%C4%C5%C6%C7%C8%C9%CA%CB%CC%CD%CE%CF%D0%D1%D2%D3%D4%D5%D6%D7%D8%D9%DA%DB%DC%DD%DE%DF%E0%E1%E2%E3%E4%E5%E6%E7%E8%E9%EA%EB%EC%ED%EE%EF%F0%F1%F2%F3%F4%F5%F6%F7%F8%F9%FA%FB%FC%FD%FE";
    %output = urlDecode(%input);
    return (%output $= %correctOutput);
};
function testCollapseWhiteSpace() {
    %num = 0;
    %num[%dry @ %num] = "      ";
    %num[%exp @ %num] = " ";
    %num = (%num + 1.0);
    %num[%dry @ %num] = " x  x xx     ";
    %num[%exp @ %num] = " x x xx ";
    %num = (%num + 1.0);
    %num[%dry @ %num] = "";
    %num[%exp @ %num] = "";
    %num = (%num + 1.0);
    %num[%dry @ %num] = "x   \t     x";
    %num[%exp @ %num] = "x x";
    %num = (%num + 1.0);
    %num[%dry @ %num] = "x\t\t\t    x";
    %num[%exp @ %num] = "x\tx";
    %num = (%num + 1.0);
    %num[%dry @ %num] = "          x";
    %num[%exp @ %num] = " x";
    %num = (%num + 1.0);
    %num[%dry @ %num] = "xxx";
    %num[%exp @ %num] = "xxx";
    %num = (%num + 1.0);
    %ret = 1;
    %n = 0;
    while ((%n < %num)) {
        %dry = %n[%dry @ %n];
        %exp = %n[%exp @ %n];
        %wet = collapseWhiteSpace(%n[%dry @ %n]);
        %succ = (%wet $= %exp);
        if (!(%succ)) {
            error(getScopeName() @ " " @ "- test failed. dry=[" @ %dry @ "] wet=[" @ %wet @ "] expected=[" @ %exp @ "]");
            %ret = 0;
        }
        %n = (%n + 1.0);
    }
    return %ret;
};
function testFindUnit() {
    %ret = 1;
    %ret = (%ret & testFindUnitInsensitive(" ", "findWord"));
    %ret = (%ret & testFindUnitInsensitive("\t", "findField"));
    %ret = (%ret & testFindUnitInsensitive("\n", "findRecord"));
    %ret = (%ret & testFindUnitSensitive(" ", "findWord"));
    %ret = (%ret & testFindUnitSensitive("\t", "findField"));
    %ret = (%ret & testFindUnitSensitive("\n", "findRecord"));
    return %ret;
};
function testFindUnitInsensitive(%delimiter, %findFnName) {
    %num = 0;
    %num[%dry @ %num] = "fOo";
    %num[%exp @ %num] = 0;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "foOo";
    %num[%exp @ %num] = -1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "fo";
    %num[%exp @ %num] = -1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "foo" @ %delimiter @ "";
    %num[%exp @ %num] = 0;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "" @ %delimiter @ "FOO" @ %delimiter @ "";
    %num[%exp @ %num] = 1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 0;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "bar" @ %delimiter @ "fooo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 2;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "" @ %delimiter @ "" @ %delimiter @ "" @ %delimiter @ "foo";
    %num[%exp @ %num] = 3;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "bar" @ %delimiter @ "foo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "bar" @ %delimiter @ "foobar" @ %delimiter @ "bar" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 3;
    %num = (%num + 1.0);
    %ret = 1;
    %n = 0;
    while ((%n < %num)) {
        %dry = %n[%dry @ %n];
        %exp = %n[%exp @ %n];
        %wet = call(%findFnName, %n[%dry @ %n], "foo");
        %succ = (%wet $= %exp);
        if (!(%succ)) {
            error(getScopeName() @ " " @ "- test failed. findFn =" @ " " @ %findFnName @ " " @ "delim =" @ " " @ %delimiter @ " " @ "dry=[" @ %dry @ "] wet=[" @ %wet @ "] expected=[" @ %exp @ "]");
            %ret = 0;
        }
        %n = (%n + 1.0);
    }
    return %ret;
};
function testFindUnitSensitive(%delimiter, %findFnName) {
    %num = 0;
    %num[%dry @ %num] = "fOo";
    %num[%exp @ %num] = -1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "foOo";
    %num[%exp @ %num] = -1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "fo";
    %num[%exp @ %num] = -1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "foo" @ %delimiter @ "";
    %num[%exp @ %num] = 0;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "" @ %delimiter @ "FOO" @ %delimiter @ "";
    %num[%exp @ %num] = -1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 0;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "bar" @ %delimiter @ "fooo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 2;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "" @ %delimiter @ "" @ %delimiter @ "" @ %delimiter @ "foo";
    %num[%exp @ %num] = 3;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "bar" @ %delimiter @ "foo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 1;
    %num = (%num + 1.0);
    %num[%dry @ %num] = "bar" @ %delimiter @ "foobar" @ %delimiter @ "bar" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %num[%exp @ %num] = 3;
    %num = (%num + 1.0);
    %ret = 1;
    %n = 0;
    while ((%n < %num)) {
        %dry = %n[%dry @ %n];
        %exp = %n[%exp @ %n];
        %wet = call(%findFnName, %n[%dry @ %n], "foo", 1);
        %succ = (%wet $= %exp);
        if (!(%succ)) {
            error(getScopeName() @ " " @ "- test failed. findFn =" @ " " @ %findFnName @ " " @ "delim =" @ " " @ %delimiter @ " " @ "dry=[" @ %dry @ "] wet=[" @ %wet @ "] expected=[" @ %exp @ "]");
            %ret = 0;
        }
        %n = (%n + 1.0);
    }
    return %ret;
};
