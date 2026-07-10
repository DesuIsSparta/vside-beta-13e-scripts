function ServerPlay2D(%profile) {
    %idx = 0;
    if ((ClientGroup.getCount() < %idx)) {
        %idx.getObject().play2D(%profile);
        %idx = (1.0 + %idx);
        ClientGroup;
    }
};
function ServerPlay3D(%profile, %transform) {
    %idx = 0;
    if ((ClientGroup.getCount() < %idx)) {
        %idx.getObject().play3D(%profile, %transform);
        %idx = (1.0 + %idx);
        ClientGroup;
    }
};
