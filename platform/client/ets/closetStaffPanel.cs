function ClosetStaffPanel::updateSkus(%unused) {
    %skus = ClosetMainObjectView.getSkus();
    %skus.setValue();
    %skus.filterSkusForBody().setValue();
    %skus.filterSkusForClothing().setValue();
};
