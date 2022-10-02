function SpawnSphere::onEditorRender(%this, %editor, %unused, %unused)
{
    if (%this.noShow)
    {
        return ;
    }
    %center = %this.getWorldBoxCenter();
    %editor.consoleLineWidth = 1;
    %editor.consoleFrameColor = "255 0 0 50";
    %editor.consoleFillColor = "0 0 90 10";
    %editor.renderSphere(%center, %this.radius, 2);
    %editor.consoleLineWidth = 7;
    %editor.consoleFrameColor = "255 200 0 240";
    %editor.consoleFillColor = "0 100 190 60";
    %pos = %center;
    %pos = VectorAdd(%pos, "0 0 -0.005");
    %editor.renderCircle(%pos, "0 0 1", %this.radius);
    %arrow1 = %this.localToWorldPoint(%this.radius * 0.25 SPC (%this.radius * -0.5) + 1 SPC -0.01);
    %arrow2 = %this.localToWorldPoint(%this.radius * -0.25 SPC (%this.radius * -0.5) + 1 SPC -0.01);
    %arrow3 = %this.localToWorldPoint(0 SPC (%this.radius * 0.9) + 1 SPC -0.01);
    %editor.consoleLineWidth = 3;
    %editor.consoleFrameColor = "255 200 0 180";
    %editor.consoleFillColor = "255 200 0 40";
    %editor.renderTriangle(%arrow1, %arrow2, %arrow3);
    return ;
}
