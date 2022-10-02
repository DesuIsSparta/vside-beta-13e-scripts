function testCommandToServer_DumpVar(%val, %name)
{
    echo(%name SPC "    =" SPC %val);
    echo(%name SPC "len =" SPC strlen(%val));
    return ;
}
function testCommandToServer()
{
    echo("testCommandToServer().");
    echo("this function tests the transmission of several long params to server and back via commandToBlah.");
    %var1 = " 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef";
    %var2 = " 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef";
    %var3 = " 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef";
    %var4 = " 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef 123456789abcdef";
    testCommandToServer_DumpVar(%var1, "var1");
    testCommandToServer_DumpVar(%var2, "var2");
    testCommandToServer_DumpVar(%var3, "var3");
    testCommandToServer_DumpVar(%var4, "var4");
    echo("sending vars to server..");
    commandToServer('testCommandToServer', %var1, %var2, %var3, %var4);
    return ;
}
function serverCmdTestCommandToServer(%client, %var1, %var2, %var3, %var4)
{
    echo("testCommandToServer: got vars from client");
    echo("client =" SPC getDebugString(%client));
    testCommandToServer_DumpVar(%var1, "var1");
    testCommandToServer_DumpVar(%var2, "var2");
    testCommandToServer_DumpVar(%var3, "var3");
    testCommandToServer_DumpVar(%var4, "var4");
    echo("sending vars to client..");
    commandToClient(%client, 'testCommandToClient', %var1, %var2, %var3, %var4);
    return ;
}
function clientCmdTestCommandToClient(%var1, %var2, %var3, %var4)
{
    echo("testCommandToClient: got vars from server");
    testCommandToServer_DumpVar(%var1, "var1");
    testCommandToServer_DumpVar(%var2, "var2");
    testCommandToServer_DumpVar(%var3, "var3");
    testCommandToServer_DumpVar(%var4, "var4");
    return ;
}
