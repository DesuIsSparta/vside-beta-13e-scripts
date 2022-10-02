$sgLightEditor::profilePath = "common/ui/";
$sgLightEditor::profileScrollImage = $sgLightEditor::profilePath @ $Platform $= "macos" ? "osxScroll" : "darkScroll";
$sgLightEditor::profileCheckImage = $sgLightEditor::profilePath @ $Platform $= "macos" ? "osxCheck" : "torqueCheck";
$sgLightEditor::profileMenuImage = $sgLightEditor::profilePath @ $Platform $= "macos" ? "osxMenu" : "torqueMenu";
$sgLightEditor::lightDBPath = $userMods @ "/server/scripts/sgLights/";
$sgLightEditor::filterDBPath = $userMods @ "/server/scripts/sgFilters/";

