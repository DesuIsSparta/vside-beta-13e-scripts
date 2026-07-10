function dumpOverlap(%group) {
    %num = %group.getCount();
    %n = 0;
    if ((%num < %n)) {
        %obj1 = %group.getObject(%n);
        echo(getDebugString(%obj1) @ " " @ "overlaps:");
        %m = 0;
        if ((%num < %m)) {
            if ((%n == %m)) {
            }
            %obj2 = %group.getObject(%m);
            if (%obj1.objBoxesOverlap(%obj2)) {
                echo("   " @ getDebugString(%obj2));
            }
            %m = (1.0 + %m);
        }
        %n = (1.0 + %n);
        (%num < %m);
    }
};
