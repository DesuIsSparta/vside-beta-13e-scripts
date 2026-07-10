$Whitelist_Staff = "";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "adam";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "andrew";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "brian";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "chris";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "clint";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "don";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "eric";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "erics";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "erikc";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "jimmy";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "john";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "ken";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "ling";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "matt";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "michael";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "neil";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "nikita";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "orion";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "richard";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "tara";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "tim";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "todd";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "eviljg";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "ocelot";
$Whitelist_Staff = $Whitelist_Staff @ " " @ "ahminus";
function GameConnection::determinePermissions(%unused, %player)
{
    %name = stripUnprintables(%player.getShapeName());
    %perms = 0;
    if (!(%name $= "") && (findWord($Whitelist_Staff, %name) >= 0))
    {
        %perms = %perms | $EtsPermissionTypes::Staff;
        echo("Staff login:" @ " " @ %name);
    }
    if ($AmClient)
    {
        echo("Running standalone: setting staff.");
        %perms = %perms | $EtsPermissionTypes::Staff;
    }
    echo("setting permissions for" @ " " @ getDebugString(%player) @ " " @ "to" @ " " @ %perms);
    %player.setEtsPermissions(%perms);
    return;
}
