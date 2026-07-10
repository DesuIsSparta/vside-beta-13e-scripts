function SpawnSphere::onEditorRender(%this, %editor, %unused, %unused) {
    if (%this.noShow) {
        return;
    }
    %center = %this.getWorldBoxCenter();
    %editor.consoleLineWidth = 1;
    %editor.consoleFrameColor = "255 0 0 50";
    %editor.consoleFillColor = "0 0 90 10";
    2.renderSphere(%editor, %center, %this.radius);
    %editor.consoleLineWidth = 7;
    %editor.consoleFrameColor = "255 200 0 240";
    %editor.consoleFillColor = "0 100 190 60";
    %pos = %center;
    %pos = VectorAdd(%pos, "0 0 -0.005");
    %this.radius.renderCircle(%editor, %pos, "0 0 1");
    %arrow1 = (%this.radius * 0.25) @ " " @ ((%this.radius * -(0.5)) + 1.0) @ " " @ -0.01.localToWorldPoint(%this);
    %arrow2 = (%this.radius * -(0.25)) @ " " @ ((%this.radius * -(0.5)) + 1.0) @ " " @ -0.01.localToWorldPoint(%this);
    %arrow3 = 0 @ " " @ ((%this.radius * 0.9) + 1.0) @ " " @ -0.01.localToWorldPoint(%this);
    %editor.consoleLineWidth = 3;
    %editor.consoleFrameColor = "255 200 0 180";
    %editor.consoleFillColor = "255 200 0 40";
    %arrow3.renderTriangle(%editor, %arrow1, %arrow2);
};
