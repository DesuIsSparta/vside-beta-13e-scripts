function PlantDetailsGui::open(%this) {
    %this.ensureAdded(PlayGui);
    if (!(%this.isVisible())) {
        1.setVisible(%this);
    }
    %this.focusAndRaise(PlayGui);
};
function PlantDetailsGui::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function PlantDetailsGui::onClickFAQButton(%this) {
    gotoWebPage(%this.faqURL);
};
function PlantDetailsGui::showDetails(%this, %plantSKU, %plantName, %info, %currentState, %totalStates, %faqURL) {
    %this.open();
    %plantName.setText(PlantDetailsTitle);
    (%currentState / %totalStates).setValue(PlantDetailsProgressBar);
    %info.setText(PlantDetailsStatusText);
    %bmp = "projects/common/inventory/" @ %plantSKU @ "/progress" @ %plantSKU @ ".png";
    %bmp.setBitmap(PlantProgressBackgroundBMP);
    %this.faqURL = %faqURL;
};
function ClientCmdShowPlantDetails(%plantSKU, %plantName, %totalStates, %currentState, %status, %faqURL) {
    if ((%status $= "HAPPY")) {
        %info = %status[$MsgCat::plant @ "GENERIC-DetailsInfoPlantIsHappy"];
        if ((%currentState == %totalStates)) {
            %info = %currentState[$MsgCat::plant @ "GENERIC-DetailsInfoPlantIsFullyGrown"];
        }
    }
    if ((%status $= "DRY")) {
        %info = %status[$MsgCat::plant @ "GENERIC-DetailsInfoPlantIsDry"];
    }
    if ((%status $= "DEAD")) {
        %info = %status[$MsgCat::plant @ "GENERIC-DetailsInfoPlantIsDead"];
    }
    %info = strreplace(%info, "[PLANTNAME_OR_YOURPLANT]", %plantName);
    PlantDetailsGui.open();
    %faqURL.showDetails(PlantDetailsGui, %plantSKU, %plantName, %info, %currentState, %totalStates);
};
