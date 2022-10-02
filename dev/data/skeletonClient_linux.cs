echo("LOAD: Hello skeletonClient_linux.cs!");
$UserPref::Audio::mute = 1;
$UserPref::Audio::keepMusicHudOpen = 0;
$UserPref::ETS::Debugging = 0;
$UserPref::slideshow::random = 0;
$UserPref::AIM::SavePassword = 0;
$UserPref::ETS::HudTabs::AutoHide = 1;
$UserPref::AIM::AutoSignin = 0;
$UserPref::UI::Radar::AutoOpen = 1;
$System::ID = "fakeMacAddress.com";
$System::ID1 = "fakeMacAddress.com";
$System::ID2 = "fakeMacAddress.com";
$System::ID3 = "fakeMacAddress.com";
$System::ID4 = "fakeMacAddress.com";
function initAVPlayer()
{
    echo("Fake initAVPlayer()");
    return ;
}
function getCurrentMemoryUsage()
{
    echo("Fake getCurrentMemoryUsage()");
    return ;
}
function getAllAVPlayerNames()
{
    echo("Fake getAllAVPlayerNames()");
    return ;
}
function ffmpegSetMasterVolume()
{
    echo("Fake ffmpegSetMasterVolume()");
    return ;
}
