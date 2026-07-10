function dumpOverlap(%group) {
    %num = %group.getCount();
    %n = 0;
    %obj1 = %group.getObject(%n);
    (%num < %n);
    echo(getDebugString(%obj1) @ " " @ "overlaps:");
    %m = 0;
    %obj2 = %group.getObject(%m);
    (%n == %m);
    echo(%obj1.objBoxesOverlap(%obj2) @ "   " @ getDebugString(%obj2));
    %m = (1.0 + %m);
    (%num < %m);
    %n = (1.0 + %n);
    (%num < %m);
};
