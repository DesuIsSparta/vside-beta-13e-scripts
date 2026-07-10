function PlantDetailsGui::open(%this) {
    %this.ensureAdded();
    if (!(%this.isVisible())) {
        %this.setVisible(1);
    }
    %this.focusAndRaise();
};
function PlantDetailsGui::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function PlantDetailsGui::onClickFAQButton(%this) {
    gotoWebPage(faqURL);
};
function PlantDetailsGui::showDetails(%this, %plantSKU, %plantName, %info, %currentState, %totalStates, %faqURL) {
    %this.open();
    %plantName.setText();
    (%totalStates / %currentState).setValue();
    %info.setText();
    %bmp = PlantDetailsTitle @ PlantDetailsProgressBar @ PlantDetailsStatusText @ "projects/common/inventory/" @ %plantSKU @ "/progress" @ %plantSKU @ ".png";
    %bmp.setBitmap();
    faqURL = PlantProgressBackgroundBMP @ %faqURL @ %this;
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
    open();
    %plantSKU.showDetails(%plantName, %info, %currentState, %totalStates, %faqURL);
};
