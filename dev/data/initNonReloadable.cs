exec("./benchmarks/" @ "initNonReloadable.cs");
exec("./admin/" @ "initNonReloadable.cs");
exec("./miscDevNonReloadable.cs");
if ($AmClient)
{
    exec("./ui/" @ "initNonReloadable.cs");
    exec("./data/" @ "initNonReloadable.cs");
}
