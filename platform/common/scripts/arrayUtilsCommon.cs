function Array::hasKey(%this, %key) {
    return (%key.getIndexFromKey(%this) >= 0.0);
};
function Array::hasValue(%this, %val) {
    return (%val.getIndexFromValue(%this) >= 0.0);
};
function Array::size(%this) {
    return %this.count();
};
function Array::get(%this, %key) {
    %ndx = %key.getIndexFromKey(%this);
    if ((%ndx < 0.0)) {
        error(getScopeName() @ " " @ "- no such key: \"" @ %key @ "\"." @ " " @ getTrace());
        return "";
    }
    return %ndx.getValue(%this);
};
function Array::put(%this, %key, %value) {
    %value.push_back(%this, %key);
};
