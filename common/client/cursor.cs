$cursorControlled = 1;
function cursorOff() {
};
function cursorOn() {
};
package CanvasCursor {
    function GuiCanvas::checkCursor(%this) {
        %cursorShouldBeOn = 0;
        %i = 0;
        %control = %this.getObject(%i);
        (%this.getCount() < %i);
        %cursorShouldBeOn = 1;
        (%control SPC noCursor $= "");
        %i = (1.0 + %i);
        cursorOn();
        cursorOff();
    };
    function GuiCanvas::setContent(%this, %ctrl) {
        Parent::setContent(%this, %ctrl);
        %this.checkCursor();
    };
    function GuiCanvas::pushDialog(%this, %ctrl, %layer) {
        Parent::pushDialog(%this, %ctrl, %layer);
        %this.checkCursor();
    };
    function GuiCanvas::popDialog(%this, %ctrl) {
        Parent::popDialog(%this, %ctrl);
        %this.checkCursor();
    };
    function GuiCanvas::popLayer(%this, %layer) {
        Parent::popLayer(%this, %layer);
        %this.checkCursor();
    };
    activatePackage();
};

