$Whitelist_Staff = "";
$Whitelist_Staff = $Whitelist_Staff SPC "adam";
$Whitelist_Staff = $Whitelist_Staff SPC "andrew";
$Whitelist_Staff = $Whitelist_Staff SPC "brian";
$Whitelist_Staff = $Whitelist_Staff SPC "chris";
$Whitelist_Staff = $Whitelist_Staff SPC "clint";
$Whitelist_Staff = $Whitelist_Staff SPC "don";
$Whitelist_Staff = $Whitelist_Staff SPC "eric";
$Whitelist_Staff = $Whitelist_Staff SPC "erics";
$Whitelist_Staff = $Whitelist_Staff SPC "erikc";
$Whitelist_Staff = $Whitelist_Staff SPC "jimmy";
$Whitelist_Staff = $Whitelist_Staff SPC "john";
$Whitelist_Staff = $Whitelist_Staff SPC "ken";
$Whitelist_Staff = $Whitelist_Staff SPC "ling";
$Whitelist_Staff = $Whitelist_Staff SPC "matt";
$Whitelist_Staff = $Whitelist_Staff SPC "michael";
$Whitelist_Staff = $Whitelist_Staff SPC "neil";
$Whitelist_Staff = $Whitelist_Staff SPC "nikita";
$Whitelist_Staff = $Whitelist_Staff SPC "orion";
$Whitelist_Staff = $Whitelist_Staff SPC "richard";
$Whitelist_Staff = $Whitelist_Staff SPC "tara";
$Whitelist_Staff = $Whitelist_Staff SPC "tim";
$Whitelist_Staff = $Whitelist_Staff SPC "todd";
$Whitelist_Staff = $Whitelist_Staff SPC "eviljg";
$Whitelist_Staff = $Whitelist_Staff SPC "ocelot";
$Whitelist_Staff = $Whitelist_Staff SPC "ahminus";
function GameConnection::determinePermissions(%unused, %player)
{
    %name = stripUnprintables(%player.getShapeName());
    %perms = 0;
    if (!((%name $= "")) && (findWord($Whitelist_Staff, %name) >= 0))
    {
        %perms = %perms | $EtsPermissionTypes::Staff;
        echo("Staff login:" SPC %name);
    }
    if ($AmClient)
    {
        echo("Running standalone: setting staff.");
        %perms = %perms | $EtsPermissionTypes::Staff;
    }
    echo("setting permissions for" SPC getDebugString(%player) SPC "to" SPC %perms);
    %player.setEtsPermissions(%perms);
    return ;
}
