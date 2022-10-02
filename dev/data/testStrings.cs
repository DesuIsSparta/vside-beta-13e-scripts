function testStringsMaster()
{
    %testCount = 0;
    %passCount = 0;
    %test[%testCount] = "testIntToChar()";
    %testCount = %testCount + 1;
    %test[%testCount] = "testUrlEncode()";
    %testCount = %testCount + 1;
    %test[%testCount] = "testUrlDecode()";
    %testCount = %testCount + 1;
    %test[%testCount] = "testCollapseWhiteSpace()";
    %testCount = %testCount + 1;
    %test[%testCount] = "testFindUnit()";
    %testCount = %testCount + 1;
    %n = 0;
    while (%n < %testCount)
    {
        eval("%succ =" SPC %test[%n] @ ";");
        if (%succ)
        {
            echo("test passed:" SPC %test[%n]);
            %passCount = %passCount + 1;
        }
        else
        {
            error("test failed:" SPC %test[%n]);
        }
        %n = %n + 1;
    }
    echo("testStringsMaster():" SPC %passCount SPC "of" SPC %testCount SPC "passed," SPC %testCount - %passCount SPC "failed.");
    return %passCount == %testCount;
}
function testIntToChar()
{
    %succ = 1;
    %n = 0;
    while (%n < 256)
    {
        %c = intToChar(%n);
        %m = charToInt(%c);
        %d = intToChar(%m);
        if (!(%c $= %d))
        {
            error("testIntToChar() failed 1 at" SPC %n);
            %succ = 0;
        }
        if (%m != %n)
        {
            error("testIntToChar() failed 2 at" SPC %n);
            %succ = 0;
        }
        %n = %n + 1;
    }
    return %succ;
}
function testUrlEncode()
{
    %input = " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþ";
    %correctOutput = "%20!%22%23%24%25%26\'()*%2B%2C-.%2F0123456789%3A%3B%3C%3D%3E%3F%40ABCDEFGHIJKLMNOPQRSTUVWXYZ%5B%5C%5D%5E_%60abcdefghijklmnopqrstuvwxyz%7B%7C%7D~%A1%A2%A3%A4%A5%A6%A7%A8%A9%AA%AB%AC%AD%AE%AF%B0%B1%B2%B3%B4%B5%B6%B7%B8%B9%BA%BB%BC%BD%BE%BF%C0%C1%C2%C3%C4%C5%C6%C7%C8%C9%CA%CB%CC%CD%CE%CF%D0%D1%D2%D3%D4%D5%D6%D7%D8%D9%DA%DB%DC%DD%DE%DF%E0%E1%E2%E3%E4%E5%E6%E7%E8%E9%EA%EB%EC%ED%EE%EF%F0%F1%F2%F3%F4%F5%F6%F7%F8%F9%FA%FB%FC%FD%FE";
    %output = urlEncode(%input);
    return %output $= %correctOutput;
}
function testUrlDecode()
{
    %correctOutput = " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþ";
    %input = "%20!%22%23%24%25%26\'()*%2B%2C-.%2F0123456789%3A%3B%3C%3D%3E%3F%40ABCDEFGHIJKLMNOPQRSTUVWXYZ%5B%5C%5D%5E_%60abcdefghijklmnopqrstuvwxyz%7B%7C%7D~%A1%A2%A3%A4%A5%A6%A7%A8%A9%AA%AB%AC%AD%AE%AF%B0%B1%B2%B3%B4%B5%B6%B7%B8%B9%BA%BB%BC%BD%BE%BF%C0%C1%C2%C3%C4%C5%C6%C7%C8%C9%CA%CB%CC%CD%CE%CF%D0%D1%D2%D3%D4%D5%D6%D7%D8%D9%DA%DB%DC%DD%DE%DF%E0%E1%E2%E3%E4%E5%E6%E7%E8%E9%EA%EB%EC%ED%EE%EF%F0%F1%F2%F3%F4%F5%F6%F7%F8%F9%FA%FB%FC%FD%FE";
    %output = urlDecode(%input);
    return %output $= %correctOutput;
}
function testCollapseWhiteSpace()
{
    %num = 0;
    %dry[%num] = "      ";
    %exp[%num] = " ";
    %num = %num + 1;
    %dry[%num] = " x  x xx     ";
    %exp[%num] = " x x xx ";
    %num = %num + 1;
    %dry[%num] = "";
    %exp[%num] = "";
    %num = %num + 1;
    %dry[%num] = "x   \t     x";
    %exp[%num] = "x x";
    %num = %num + 1;
    %dry[%num] = "x\t\t\t    x";
    %exp[%num] = "x\tx";
    %num = %num + 1;
    %dry[%num] = "          x";
    %exp[%num] = " x";
    %num = %num + 1;
    %dry[%num] = "xxx";
    %exp[%num] = "xxx";
    %num = %num + 1;
    %ret = 1;
    %n = 0;
    while (%n < %num)
    {
        %dry = %dry[%n];
        %exp = %exp[%n];
        %wet = collapseWhiteSpace(%dry[%n]);
        %succ = %wet $= %exp;
        if (!%succ)
        {
            error(getScopeName() SPC "- test failed. dry=[" @ %dry @ "] wet=[" @ %wet @ "] expected=[" @ %exp @ "]");
            %ret = 0;
        }
        %n = %n + 1;
    }
    return %ret;
}
function testFindUnit()
{
    %ret = 1;
    %ret = %ret & testFindUnitInsensitive(" ", "findWord");
    %ret = %ret & testFindUnitInsensitive("\t", "findField");
    %ret = %ret & testFindUnitInsensitive("\n", "findRecord");
    %ret = %ret & testFindUnitSensitive(" ", "findWord");
    %ret = %ret & testFindUnitSensitive("\t", "findField");
    %ret = %ret & testFindUnitSensitive("\n", "findRecord");
    return %ret;
}
function testFindUnitInsensitive(%delimiter, %findFnName)
{
    %num = 0;
    %dry[%num] = "fOo";
    %exp[%num] = 0 ;
    %num = %num + 1;
    %dry[%num] = "foOo";
    %exp[%num] = -1 ;
    %num = %num + 1;
    %dry[%num] = "fo";
    %exp[%num] = -1 ;
    %num = %num + 1;
    %dry[%num] = "foo" @ %delimiter @ "";
    %exp[%num] = 0 ;
    %num = %num + 1;
    %dry[%num] = "" @ %delimiter @ "FOO" @ %delimiter @ "";
    %exp[%num] = 1 ;
    %num = %num + 1;
    %dry[%num] = "foo" @ %delimiter @ "bar";
    %exp[%num] = 0 ;
    %num = %num + 1;
    %dry[%num] = "bar" @ %delimiter @ "fooo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %exp[%num] = 2 ;
    %num = %num + 1;
    %dry[%num] = "" @ %delimiter @ "" @ %delimiter @ "" @ %delimiter @ "foo";
    %exp[%num] = 3 ;
    %num = %num + 1;
    %dry[%num] = "bar" @ %delimiter @ "foo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %exp[%num] = 1 ;
    %num = %num + 1;
    %dry[%num] = "bar" @ %delimiter @ "foobar" @ %delimiter @ "bar" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %exp[%num] = 3 ;
    %num = %num + 1;
    %ret = 1;
    %n = 0;
    while (%n < %num)
    {
        %dry = %dry[%n];
        %exp = %exp[%n];
        %wet = call(%findFnName, %dry[%n], "foo");
        %succ = %wet $= %exp;
        if (!%succ)
        {
            error(getScopeName() SPC "- test failed. findFn =" SPC %findFnName SPC "delim =" SPC %delimiter SPC "dry=[" @ %dry @ "] wet=[" @ %wet @ "] expected=[" @ %exp @ "]");
            %ret = 0;
        }
        %n = %n + 1;
    }
    return %ret;
}
function testFindUnitSensitive(%delimiter, %findFnName)
{
    %num = 0;
    %dry[%num] = "fOo";
    %exp[%num] = -1 ;
    %num = %num + 1;
    %dry[%num] = "foOo";
    %exp[%num] = -1 ;
    %num = %num + 1;
    %dry[%num] = "fo";
    %exp[%num] = -1 ;
    %num = %num + 1;
    %dry[%num] = "foo" @ %delimiter @ "";
    %exp[%num] = 0 ;
    %num = %num + 1;
    %dry[%num] = "" @ %delimiter @ "FOO" @ %delimiter @ "";
    %exp[%num] = -1 ;
    %num = %num + 1;
    %dry[%num] = "foo" @ %delimiter @ "bar";
    %exp[%num] = 0 ;
    %num = %num + 1;
    %dry[%num] = "bar" @ %delimiter @ "fooo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %exp[%num] = 2 ;
    %num = %num + 1;
    %dry[%num] = "" @ %delimiter @ "" @ %delimiter @ "" @ %delimiter @ "foo";
    %exp[%num] = 3 ;
    %num = %num + 1;
    %dry[%num] = "bar" @ %delimiter @ "foo" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %exp[%num] = 1 ;
    %num = %num + 1;
    %dry[%num] = "bar" @ %delimiter @ "foobar" @ %delimiter @ "bar" @ %delimiter @ "foo" @ %delimiter @ "bar";
    %exp[%num] = 3 ;
    %num = %num + 1;
    %ret = 1;
    %n = 0;
    while (%n < %num)
    {
        %dry = %dry[%n];
        %exp = %exp[%n];
        %wet = call(%findFnName, %dry[%n], "foo", 1);
        %succ = %wet $= %exp;
        if (!%succ)
        {
            error(getScopeName() SPC "- test failed. findFn =" SPC %findFnName SPC "delim =" SPC %delimiter SPC "dry=[" @ %dry @ "] wet=[" @ %wet @ "] expected=[" @ %exp @ "]");
            %ret = 0;
        }
        %n = %n + 1;
    }
    return %ret;
}
