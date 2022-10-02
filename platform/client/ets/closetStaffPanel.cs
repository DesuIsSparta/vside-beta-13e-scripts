function ClosetStaffPanel::updateSkus(%unused)
{
    %skus = ClosetMainObjectView.getSkus();
    closetStaffSkusTextEdit.setValue(%skus);
    closetStaffSkusBodyTextEdit.setValue(SkuManager.filterSkusForBody(%skus));
    closetStaffSkusOutfitTextEdit.setValue(SkuManager.filterSkusForClothing(%skus));
    return ;
}
