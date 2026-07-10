function Array::hasKey(%this, %key) {
    return (0.0 >= %this.getIndexFromKey(%key));
};
function Array::hasValue(%this, %val) {
    return (0.0 >= %this.getIndexFromValue(%val));
};
function Array::size(%this) {
    return %this.count();
};
function Array::get(%this, %key) {
    %ndx = %this.getIndexFromKey(%key);
    if ((0.0 < %ndx)) {
        error(getScopeName() @ " " @ "- no such key: \"" @ %key @ "\"." @ " " @ getTrace());
        return "";
    }
    return %this.getValue(%ndx);
};
function Array::put(%this, %key, %value) {
    %this.push_back(%key, %value);
};
