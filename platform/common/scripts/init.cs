exec("./commonCommands.cs");
exec("./scriptProfiler.cs");
exec("./rolesCommon.cs");
exec("./compile.cs");
exec("./gameConnection.cs");
exec("./adminCommon.cs");
exec("./genderPronouns.cs");
exec("./systemCommon.cs");
exec("./playerCommon.cs");
exec("./proximityChatCommon.cs");
exec("./skuManagerCommon.cs");
if (!exec("./skusInitGenerated_" @ $ETS::ProjectName @ ".cs", 0))
{
    if (!exec("./skusInitGenerated_vside.cs", 0))
    {
        exec("./skusInitGenerated.cs");
    }
}
if (!exec("./skusInitFurnishingsGenerated_" @ $ETS::ProjectName @ ".cs", 0))
{
    if (!exec("./skusInitFurnishingsGenerated_vside.cs", 0))
    {
        exec("./skusInitFurnishingsGenerated.cs");
    }
}
exec("./skusInitCommon.cs");
exec("./emote.cs");
exec("./blendConfig.cs");
exec("./dynamicContentCommon.cs");
exec("./floodFilterCommon.cs");
exec("./specialSKUsCommon.cs");
exec("./permissionsCommon.cs");
exec("./gameMgrCommon.cs");
exec("./assetManagerCommon.cs");
exec("./initReloadable.cs");

