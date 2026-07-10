function ServerPlay2D(%profile) {
    %idx = 0;
    while ((%idx < ClientGroup.getCount())) {
        %profile.play2D(%idx.getObject(ClientGroup));
        %idx = (%idx + 1.0);
    }
};
function ServerPlay3D(%profile, %transform) {
    %idx = 0;
    while ((%idx < ClientGroup.getCount())) {
        %transform.play3D(%idx.getObject(ClientGroup), %profile);
        %idx = (%idx + 1.0);
    }
};
