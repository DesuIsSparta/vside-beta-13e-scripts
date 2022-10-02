function dumpOverlap(%group)
{
    %num = %group.getCount();
    %n = 0;
    while (%n < %num)
    {
        %obj1 = %group.getObject(%n);
        echo(getDebugString(%obj1) SPC "overlaps:");
        %m = 0;
        while (%m < %num)
        {
            if (%m == %n)
            {
                continue;
            }
            %obj2 = %group.getObject(%m);
            if (%obj1.objBoxesOverlap(%obj2))
            {
                echo("   " @ getDebugString(%obj2));
            }
            %m = %m + 1;
        }
        %n = %n + 1;
    }
}


