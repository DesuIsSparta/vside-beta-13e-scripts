function PlantDetailsGui::open(%this)
{
    PlayGui.ensureAdded(%this);
    if (!%this.isVisible())
    {
        %this.setVisible(1);
    }
    PlayGui.focusAndRaise(%this);
    return ;
}
function PlantDetailsGui::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function PlantDetailsGui::onClickFAQButton(%this)
{
    gotoWebPage(%this.faqURL);
    return ;
}
function PlantDetailsGui::showDetails(%this, %plantSKU, %plantName, %info, %currentState, %totalStates, %faqURL)
{
    %this.open();
    PlantDetailsTitle.setText(%plantName);
    PlantDetailsProgressBar.setValue(%currentState / %totalStates);
    PlantDetailsStatusText.setText(%info);
    %bmp = "projects/common/inventory/" @ %plantSKU @ "/progress" @ %plantSKU @ ".png";
    PlantProgressBackgroundBMP.setBitmap(%bmp);
    %this.faqURL = %faqURL;
    return ;
}
function ClientCmdShowPlantDetails(%plantSKU, %plantName, %totalStates, %currentState, %status, %faqURL)
{
    if (%status $= "HAPPY")
    {
        %info = $MsgCat::plant["GENERIC-DetailsInfoPlantIsHappy"];
        if (%currentState == %totalStates)
        {
            %info = $MsgCat::plant["GENERIC-DetailsInfoPlantIsFullyGrown"];
        }
    }
    else
    {
        if (%status $= "DRY")
        {
            %info = $MsgCat::plant["GENERIC-DetailsInfoPlantIsDry"];
        }
        else
        {
            if (%status $= "DEAD")
            {
                %info = $MsgCat::plant["GENERIC-DetailsInfoPlantIsDead"];
            }
        }
    }
    %info = strreplace(%info, "[PLANTNAME_OR_YOURPLANT]", %plantName);
    PlantDetailsGui.open();
    PlantDetailsGui.showDetails(%plantSKU, %plantName, %info, %currentState, %totalStates, %faqURL);
    return ;
}
