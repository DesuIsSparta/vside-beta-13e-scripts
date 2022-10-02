function writeOutFunctions()
{
    new ConsoleLogger(Logger, "scriptFunctions.txt", 0);
    dumpConsoleFunctions();
    Logger.delete();
    return ;
}
function writeOutClasses()
{
    new ConsoleLogger(Logger, "scriptClasses.txt", 0);
    dumpConsoleClasses();
    Logger.delete();
    return ;
}
