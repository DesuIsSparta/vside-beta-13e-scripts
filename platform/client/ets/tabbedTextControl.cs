function TabbedTextControl::newControlWithScroll(%name, %fieldWidths, %padding) {
    %ctrl = MenuControl::newMenuWithScroll(%name);
    if (!(getWord(%ctrl.getNamespaceList(), 0) $= "TabbedTextControl")) {
        "TabbedTextControl".bindClassName(%ctrl);
    }
    %ctrl.lastClicked = 0;
    %padding.setFieldWidths(%ctrl, %fieldWidths);
    %ctrl.scroll.hScrollBar = "dynamic";
    1.setVisible(%ctrl.scroll);
    return %ctrl;
};
function TabbedTextControl::setFieldWidths(%this, %fieldWidths, %padding) {
    %this.fieldWidths = %fieldWidths;
    %this.Padding = %padding;
    %sum = %padding;
    %numFields = getWordCount(%fieldWidths);
    %i = 0;
    while ((%i < %numFields)) {
        %sum = (%sum + (getWord(%fieldWidths, %i) + %padding));
        %i = (%i + 1.0);
    }
    getWord(%this.getExtent(), 1).resize(%this, %sum);
    %this.childrenExtent = (%i < %numFields) @ %sum @ " " @ getWord(%this.childrenExtent, 1);
};
function TabbedTextControl::addLine(%this, %fields) {
    %line = %this.addChild();
    %this.reseatChildren();
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    while ((%i < %numFields)) {
        getField(%fields, %i).setText(%i, %line.field);
        %i = (%i + 1.0);
    }
    %line.command = (%i < %numFields) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");";
    return %line;
};
function TabbedTextControl::addLineNoReseat(%this, %fields) {
    %line = %this.addChild();
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    while ((%i < %numFields)) {
        getField(%fields, %i).setText(%i, %line.field);
        %i = (%i + 1.0);
    }
    %line.command = (%i < %numFields) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");";
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
    while ((%i < %numFields)) {
        %fieldWidth = getWord(%this.fieldWidths, %i);
        %child.field = new GuiMLTextCtrl("") {
            profile = 0 @ %this.menuTextProfile;
            position = %xoffset @ " " @ %this.paddingAboveText;
            extent = %fieldWidth @ " " @ 20;
            lineSpacing = 1;
            allowColorChars = 1;
            stripTagsOnCopy = 1;
        }; @ %i
        %child.field.add(%child, %i);
        %xoffset = (%xoffset + (%fieldWidth + %this.Padding));
        %i = (%i + 1.0);
    }
    "MenuItem".bindClassName(%child);
    "TabbedTextLine".bindClassName(%child);
};
function TabbedTextControl::childSelected(%this, %child) {
};
function TabbedTextLine::onMouseEnterBounds(%this) {
};
function TabbedTextLine::onMouseDown(%this) {
    Parent::onMouseEnterBounds(%this);
};
function TabbedTextLine::onMouseUp(%this, %unused, %unused, %clickCount) {
    if ((%clickCount == 2.0)) {
    }
    if ((%this.Parent.lastClicked == %this)) {
        %this.onSelect();
    }
    %this.Parent.lastClicked = %this;
};
