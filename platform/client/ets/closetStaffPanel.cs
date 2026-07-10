function ClosetStaffPanel::updateSkus(%unused) {
    %skus = ClosetMainObjectView.getSkus();
    %skus.setValue(closetStaffSkusTextEdit);
    %skus.filterSkusForBody(SkuManager).setValue(closetStaffSkusBodyTextEdit);
    %skus.filterSkusForClothing(SkuManager).setValue(closetStaffSkusOutfitTextEdit);
};
