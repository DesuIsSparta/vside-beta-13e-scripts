function TabbedTextControl::newControlWithScroll(%name, %fieldWidths, %padding) {
    %ctrl = MenuControl::newMenuWithScroll(%name);
    if (!(getWord(%ctrl.getNamespaceList(), 0) $= "TabbedTextControl")) {
        %ctrl.bindClassName("TabbedTextControl");
    }
    %ctrl.lastClicked = 0;
    %ctrl.setFieldWidths(%fieldWidths, %padding);
    %ctrl.scroll.hScrollBar = "dynamic";
    %ctrl.scroll.setVisible(1);
    return %ctrl;
};
function TabbedTextControl::setFieldWidths(%this, %fieldWidths, %padding) {
    %this.fieldWidths = %fieldWidths;
    %this.Padding = %padding;
    %sum = %padding;
    %numFields = getWordCount(%fieldWidths);
    %i = 0;
    if ((%numFields < %i)) {
        %sum = ((%padding + getWord(%fieldWidths, %i)) + %sum);
        %i = (1.0 + %i);
    }
    %this.resize(%sum, getWord(%this.getExtent(), 1));
    %this.childrenExtent = (%numFields < %i) @ %sum @ " " @ getWord(%this.childrenExtent, 1);
};
function TabbedTextControl::addLine(%this, %fields) {
    %line = %this.addChild();
    %this.reseatChildren();
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    if ((%numFields < %i)) {
        %line.field.setText(getField(%fields, %i));
        %i = (1.0 + %i);
        %i;
    }
    %line.command = (%numFields < %i) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");";
    return %line;
};
function TabbedTextControl::addLineNoReseat(%this, %fields) {
    %line = %this.addChild();
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    if ((%numFields < %i)) {
        %line.field.setText(getField(%fields, %i));
        %i = (1.0 + %i);
        %i;
    }
    %line.command = (%numFields < %i) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");";
    return %line;
};
function TabbedTextControl::onCreatedChild(%this, %child) {
    %child.Parent = %this;
    %child.clear();
    if ((%this.paddingAboveText $= "")) {
        %this.paddingAboveText = 2;
    }
    %xoffset = %this.Padding;
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    if ((%numFields < %i)) {
        %fieldWidth = getWord(%this.fieldWidths, %i);
        0;
        %child.field = new ""() {
            profile = GuiMLTextCtrl @ %this.menuTextProfile;
            position = %xoffset @ " " @ %this.paddingAboveText;
            extent = %fieldWidth @ " " @ 20;
            lineSpacing = 1;
            allowColorChars = 1;
            stripTagsOnCopy = 1;
        }; @ %i
        %child.add(%child.field);
        %xoffset = ((%this.Padding + %fieldWidth) + %xoffset);
        %i;
        %i = (1.0 + %i);
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
    if ((%this == %this.Parent.lastClicked)) {
        %this.onSelect();
    }
    %this.Parent.lastClicked = %this;
};
