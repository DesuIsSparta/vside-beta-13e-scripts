$Settings::VisibleDistances[0] = 200;
$Settings::VisibleDistances[1] = 300;
$Settings::VisibleDistances[2] = 1000;
$Settings::VisibleDistances[3] = $Settings::VisibleDistances[2] ;
if ($AmClient)
{
    exec("./maps/initReloadable.cs");
}
