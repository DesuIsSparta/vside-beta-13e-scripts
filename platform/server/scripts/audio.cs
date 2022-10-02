function ServerPlay2D(%profile)
{
    %idx = 0;
    while (%idx < ClientGroup.getCount())
    {
        ClientGroup.getObject(%idx).play2D(%profile);
        %idx = %idx + 1;
    }
}

function ServerPlay3D(%profile, %transform)
{
    %idx = 0;
    while (%idx < ClientGroup.getCount())
    {
        ClientGroup.getObject(%idx).play3D(%profile, %transform);
        %idx = %idx + 1;
    }
}


