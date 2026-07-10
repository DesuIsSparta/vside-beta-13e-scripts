function Player::onGotAnimation(%this, %key)
{
    %this.configBoneBlends();
}
function Player::onBlendComplete(%this, %key)
{
}
function Player::triggerBlendAnim(%this, %key, %doit)
{
    commandToServer('DoBoneBlendAnim', %key, %doit, 0);
}
function Player::setBoneBlendTargetPosition(%this, %idx, %targPos)
{
    commandToServer('SetBlendTargetPosition', %idx, %targPos);
}
function Player::setBoneBlendRate(%this, %val)
{
    commandToServer('SetBlendRate', %val);
}
function Player::setBoneBlendScale(%this, %val)
{
    commandToServer('SetBlendScale', %val);
}
function Player::setBoneBlendOffset(%this, %val)
{
    commandToServer('SetBlendOffset', %val);
}
function Player::setBlendPosition(%this, %idx, %val)
{
    commandToServer('SetBlendPosition', %idx, %val);
}
function Player::setBoneBlendArmActive(%this, %doit)
{
    commandToServer('SetBlendArmActive', %doit);
}
function Player::setBoneBlendRateByIndex(%this, %index, %newRate)
{
    commandToServer('setBlendRateByIndex', %index, %newRate);
}
function Player::setBoneBlendScaleByIndex(%this, %index, %newScale)
{
    commandToServer('setBlendScaleByIndex', %index, %newScale);
}
function Player::setBoneBlendOffsetByIndex(%this, %index, %newOffset)
{
    commandToServer('setBlendOffsetByIndex', %index, %newOffset);
}
