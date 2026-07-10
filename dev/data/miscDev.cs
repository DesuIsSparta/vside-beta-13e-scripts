function dumpOverlap(%group) {
    %num = %group.getCount();
    %n = 0;
    while ((%n < %num)) {
        %obj1 = %n.getObject(%group);
        echo(getDebugString(%obj1) @ " " @ "overlaps:");
        %m = 0;
        while ((%m < %num)) {
            if ((%m == %n)) {
            }
            %obj2 = %m.getObject(%group);
            if (%obj2.objBoxesOverlap(%obj1)) {
                echo("   " @ getDebugString(%obj2));
            }
            %m = (%m + 1.0);
        }
        %n = (%n + 1.0);
    }
};
