function PlantDetailsGui::open(%this) {
    %this.ensureAdded();
    if (!(%this.isVisible())) {
        %this.setVisible(1);
    }
    %this.focusAndRaise();
};
function PlantDetailsGui::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function PlantDetailsGui::onClickFAQButton(%this) {
    gotoWebPage(%this.faqURL);
};
function PlantDetailsGui::showDetails(%this, %plantSKU, %plantName, %info, %currentState, %totalStates, %faqURL) {
    %this.open();
    %plantName.setText();
    (%totalStates / %currentState).setValue();
    %info.setText();
    %bmp = "projects/common/inventory/" @ %plantSKU @ "/progress" @ %plantSKU @ ".png";
    PlantDetailsStatusText;
    %bmp.setBitmap();
    %this.faqURL = PlantProgressBackgroundBMP @ %faqURL;
    PlantDetailsProgressBar;
};
function ClientCmdShowPlantDetails(%plantSKU, %plantName, %totalStates, %currentState, %status, %faqURL) {
    if ((%status $= "HAPPY")) {
        %info = %status[$MsgCat::plant @ "GENERIC-DetailsInfoPlantIsHappy"];
        if ((%totalStates == %currentState)) {
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
    %plantSKU.showDetails(%plantName, %info, %currentState, %totalStates, %faqURL);
};
