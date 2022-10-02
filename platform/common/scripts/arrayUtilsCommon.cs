function Array::hasKey(%this, %key)
{
    return %this.getIndexFromKey(%key) >= 0;
}
function Array::hasValue(%this, %val)
{
    return %this.getIndexFromValue(%val) >= 0;
}
function Array::size(%this)
{
    return %this.count();
}
function Array::get(%this, %key)
{
    %ndx = %this.getIndexFromKey(%key);
    if (%ndx < 0)
    {
        error(getScopeName() SPC "- no such key: \"" @ %key @ "\"." SPC getTrace());
        return "";
    }
    return %this.getValue(%ndx);
}
function Array::put(%this, %key, %value)
{
    %this.push_back(%key, %value);
    return ;
}
