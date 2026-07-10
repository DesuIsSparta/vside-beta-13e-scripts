function ServerPlay2D(%profile) {
    %idx = 0;
    if ((ClientGroup.getCount() < %idx)) {
        ClientGroup.getObject(%idx).play2D(%profile);
        %idx = (1.0 + %idx);
    }
};
function ServerPlay3D(%profile, %transform) {
    %idx = 0;
    if ((ClientGroup.getCount() < %idx)) {
        ClientGroup.getObject(%idx).play3D(%profile, %transform);
        %idx = (1.0 + %idx);
    }
};
