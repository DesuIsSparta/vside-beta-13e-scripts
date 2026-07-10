function ClosetStaffPanel::updateSkus(%unused) {
    %skus = getSkus();
    ClosetMainObjectView;
    %skus.setValue();
    %skus.filterSkusForBody().setValue();
    %skus.filterSkusForClothing().setValue();
};
