function TabbedTextControl::newControlWithScroll(%name, %fieldWidths, %padding) {
    %ctrl = MenuControl::newMenuWithScroll(%name);
    if (!(getWord(%ctrl.getNamespaceList(), 0) $= "TabbedTextControl")) {
        %ctrl.bindClassName("TabbedTextControl");
    }
    lastClicked = 0 @ %ctrl;
    %ctrl.setFieldWidths(%fieldWidths, %padding);
    hScrollBar = %ctrl @ scroll;
    "dynamic";
    scroll.setVisible(1);
    return %ctrl;
};
function TabbedTextControl::setFieldWidths(%this, %fieldWidths, %padding) {
    fieldWidths = %fieldWidths @ %this;
    Padding = %padding @ %this;
    %sum = %padding;
    %numFields = getWordCount(%fieldWidths);
    %i = 0;
    if ((%numFields < %i)) {
        %sum = ((%padding + getWord(%fieldWidths, %i)) + %sum);
        %i = (1.0 + %i);
    }
    %this.resize(%sum, getWord(%this.getExtent(), 1));
    childrenExtent = %sum @ " " @ %this @ getWord(childrenExtent, 1) @ %this;
    (%numFields < %i);
};
function TabbedTextControl::addLine(%this, %fields) {
    %line = %this.addChild();
    %this.reseatChildren();
    %numFields = getWordCount(fieldWidths);
    %this;
    %i = 0;
    if ((%numFields < %i)) {
        field.setText(getField(%fields, %i));
        %i = (1.0 + %i);
        %i @ %line;
    }
    command = (%numFields < %i) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");" @ %line;
    return %line;
};
function TabbedTextControl::addLineNoReseat(%this, %fields) {
    %line = %this.addChild();
    %numFields = getWordCount(fieldWidths);
    %this;
    %i = 0;
    if ((%numFields < %i)) {
        field.setText(getField(%fields, %i));
        %i = (1.0 + %i);
        %i @ %line;
    }
    command = (%numFields < %i) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");" @ %line;
    return %line;
};
function TabbedTextControl::onCreatedChild(%this, %child) {
    Parent = %this @ %child;
    %child.clear();
    if ((%this SPC paddingAboveText $= "")) {
        paddingAboveText = 2 @ %this;
    }
    %xoffset = Padding;
    %this;
    %numFields = getWordCount(fieldWidths);
    %this;
    %i = 0;
    if ((%numFields < %i)) {
        %fieldWidth = getWord(fieldWidths, %i);
        %this;
        profile = new ""() @ %this @ menuTextProfile;
        GuiMLTextCtrl;
        position = 0 @ %xoffset @ " " @ %this @ paddingAboveText;
        extent = %fieldWidth @ " " @ 20;
        lineSpacing = 1;
        allowColorChars = 1;
        stripTagsOnCopy = 1;
        field = %i @ %child;
        %child.add(field);
        %xoffset = ((Padding + %fieldWidth) + %xoffset);
        %this;
        %i = (1.0 + %i);
        %i @ %child;
    }
    %child.bindClassName("MenuItem");
    %child.bindClassName("TabbedTextLine");
};
function TabbedTextControl::childSelected(%this, %child) {
};
function TabbedTextLine::onMouseEnterBounds(%this) {
};
function TabbedTextLine::onMouseDown(%this) {
    Parent::onMouseEnterBounds(%this);
};
function TabbedTextLine::onMouseUp(%this, %unused, %unused, %clickCount) {
    if ((2.0 == %clickCount)) {
    }
    if ((Parent == lastClicked)) {
        %this.onSelect();
    }
    lastClicked = %this @ Parent;
    %this @ %this;
};
