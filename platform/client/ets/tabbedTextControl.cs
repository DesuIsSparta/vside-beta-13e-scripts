function TabbedTextControl::newControlWithScroll(%name, %fieldWidths, %padding)
{
    %ctrl = MenuControl::newMenuWithScroll(%name);
    if (!(getWord(%ctrl.getNamespaceList(), 0) $= "TabbedTextControl"))
    {
        %ctrl.bindClassName("TabbedTextControl");
    }
    %ctrl.lastClicked = 0;
    %ctrl.setFieldWidths(%fieldWidths, %padding);
    %ctrl.scroll.hScrollBar = "dynamic";
    %ctrl.scroll.setVisible(1);
    return %ctrl;
}
function TabbedTextControl::setFieldWidths(%this, %fieldWidths, %padding)
{
    %this.fieldWidths = %fieldWidths;
    %this.Padding = %padding;
    %sum = %padding;
    %numFields = getWordCount(%fieldWidths);
    %i = 0;
    while (%i < %numFields)
    {
        %sum = %sum + (getWord(%fieldWidths, %i) + %padding);
        %i = %i + 1;
    }
    %this.resize(%sum, getWord(%this.getExtent(), 1));
    %this.childrenExtent = (%i < %numFields) @ %sum @ " " @ getWord(%this.childrenExtent, 1);
}
function TabbedTextControl::addLine(%this, %fields)
{
    %line = %this.addChild();
    %this.reseatChildren();
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    while (%i < %numFields)
    {
        %line.field[%i].setText(getField(%fields, %i));
        %i = %i + 1;
    }
    %line.command = (%i < %numFields) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");";
    return %line;
}
function TabbedTextControl::addLineNoReseat(%this, %fields)
{
    %line = %this.addChild();
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    while (%i < %numFields)
    {
        %line.field[%i].setText(getField(%fields, %i));
        %i = %i + 1;
    }
    %line.command = (%i < %numFields) @ %this.getId() @ ".childSelected(" @ %line.getId() @ ");";
    return %line;
}
function TabbedTextControl::onCreatedChild(%this, %child)
{
    %child.Parent = %this;
    %child.clear();
    if (%this.paddingAboveText $= "")
    {
        %this.paddingAboveText = 2;
    }
    %xoffset = %this.Padding;
    %numFields = getWordCount(%this.fieldWidths);
    %i = 0;
    while (%i < %numFields)
    {
        %fieldWidth = getWord(%this.fieldWidths, %i);
        %child.field[" ",%this.paddingAboveText;
            extent = %fieldWidth," ",20;
            lineSpacing = 1;
            allowColorChars = 1;
            stripTagsOnCopy = 1;
        };,%i] = new GuiMLTextCtrl("") {
            profile = %this.menuTextProfile;
            position = %xoffset;
        %child.add(%child.field[%i]);
        %xoffset = %xoffset + (%fieldWidth + %this.Padding);
        %i = %i + 1;
    }
    %child.bindClassName("MenuItem");
    %child.bindClassName("TabbedTextLine");
}
function TabbedTextControl::childSelected(%this, %child)
{
}
function TabbedTextLine::onMouseEnterBounds(%this)
{
}
function TabbedTextLine::onMouseDown(%this)
{
    Parent::onMouseEnterBounds(%this);
}
function TabbedTextLine::onMouseUp(%this, %unused, %unused, %clickCount)
{
    if (%clickCount == 2)
    {
    }
    if (%this.Parent.lastClicked == %this)
    {
        %this.onSelect();
    }
    %this.Parent.lastClicked = %this;
}
