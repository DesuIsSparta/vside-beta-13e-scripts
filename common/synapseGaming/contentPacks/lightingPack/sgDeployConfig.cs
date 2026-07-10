$sgLightEditor::profilePath = "common/ui/";
$sgLightEditor::profileScrollImage = ($sgLightEditor::profilePath SPC $Platform $= "macos") ? "osxScroll" : "darkScroll";
$sgLightEditor::profileCheckImage = ($sgLightEditor::profilePath SPC $Platform $= "macos") ? "osxCheck" : "torqueCheck";
$sgLightEditor::profileMenuImage = ($sgLightEditor::profilePath SPC $Platform $= "macos") ? "osxMenu" : "torqueMenu";
$sgLightEditor::lightDBPath = $userMods @ "/server/scripts/sgLights/";
$sgLightEditor::filterDBPath = $userMods @ "/server/scripts/sgFilters/";
