function Player::onGotAnimation(%this, %key)
{
    %this.configBoneBlends();
    return ;
}
function Player::onBlendComplete(%this, %key)
{
    return ;
}
function Player::triggerBlendAnim(%this, %key, %doit)
{
    commandToServer('DoBoneBlendAnim', %key, %doit, 0);
    return ;
}
function Player::setBoneBlendTargetPosition(%this, %idx, %targPos)
{
    commandToServer('SetBlendTargetPosition', %idx, %targPos);
    return ;
}
function Player::setBoneBlendRate(%this, %val)
{
    commandToServer('SetBlendRate', %val);
    return ;
}
function Player::setBoneBlendScale(%this, %val)
{
    commandToServer('SetBlendScale', %val);
    return ;
}
function Player::setBoneBlendOffset(%this, %val)
{
    commandToServer('SetBlendOffset', %val);
    return ;
}
function Player::setBlendPosition(%this, %idx, %val)
{
    commandToServer('SetBlendPosition', %idx, %val);
    return ;
}
function Player::setBoneBlendArmActive(%this, %doit)
{
    commandToServer('SetBlendArmActive', %doit);
    return ;
}
function Player::setBoneBlendRateByIndex(%this, %index, %newRate)
{
    commandToServer('setBlendRateByIndex', %index, %newRate);
    return ;
}
function Player::setBoneBlendScaleByIndex(%this, %index, %newScale)
{
    commandToServer('setBlendScaleByIndex', %index, %newScale);
    return ;
}
function Player::setBoneBlendOffsetByIndex(%this, %index, %newOffset)
{
    commandToServer('setBlendOffsetByIndex', %index, %newOffset);
    return ;
}
