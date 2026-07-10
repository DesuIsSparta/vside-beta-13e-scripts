function newTGFGoRound(%name) {
    %obj = newThumbnailsGoRound(%name);
    %obj.bindClassName("TGFGoRound");
    return %obj;
};
function TGFGoRound::rebuildContainer_LilThumb(%this, %container) {
    %container.deleteMembers();
    profile = new ""() @ ETSNonModalProfile;
    GuiBitmapCtrl;
    extent = 0 @ %container.getExtent();
    %ctrl = ;
    %container.add(%ctrl);
    mBitmapCtrl = %ctrl @ %container;
    profile = new ""() @ EtsDarkBorderlessBoxProfile;
    GuiControl;
    extent = 0 @ %container.getExtent();
    position = 0 @ " " @ (10.0 - getWord(%container.getExtent(), 1));
    %ctrlB = ;
    %container.add(%ctrlB);
    profile = new ""() @ ETSNonModalProfile;
    GuiMLTextCtrl;
    extent = 0 @ %ctrlB.getExtent();
    style = "tgfGoRoundLilThumb";
    %ctrl = ;
    %ctrlB.add(%ctrl);
    mTextCtrl = %ctrl @ %container;
};
function TGFGoRound::rebuildContainer_BigThumb(%this, %container) {
    %container.deleteMembers();
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    position = "0 0";
    extent = %container.getExtent();
    %ctrl = ;
    %container.add(%ctrl);
    mBitmapCtrl = %ctrl @ %container;
    profile = new ""() @ EtsDarkBorderlessBoxProfile;
    GuiControl;
    extent = 0 @ %container.getExtent();
    position = 0 @ " " @ (18.0 - getWord(%container.getExtent(), 1));
    %ctrlB = ;
    %container.add(%ctrlB);
    profile = new ""() @ ETSNonModalProfile;
    GuiMLTextCtrl;
    extent = 0 @ %ctrlB.getExtent();
    style = "tgfGoRoundBigThumb";
    %ctrl = ;
    %ctrlB.add(%ctrl);
    mTextCtrl = %ctrl @ %container;
    position = GuiBitmapButtonCtrl @ new ""() @ "-2 -2";
    0;
    extent = VectorAdd(%container.getExtent(), "4 4");
    command = %this @ ".onBigThumbClick(" @ %container @ ");";
    canHilite = 0;
    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
    %ctrl = ;
    %container.add(%ctrl);
};
function TGFGoRound::rebuildContainer_Deets(%this, %container) {
    %container.deleteMembers();
};
function TGFGoRound::newContentLilThumb(%this, %container) {
    if (!(isObject(mItemsList))) {
        error(getScopeName() @ " " @ "- no list" @ " " @ getTrace());
        return %this;
    }
    %item = mItemsList.getValue(mCurrentItem);
    mItemsList;
    if (!(isObject(%item))) {
        error(mItemsList @ mCurrentItem @ " " @ getTrace());
        mCurrentItem = %this @ mItemsList;
        %this @ 0;
        return getScopeName() @ " " @ "- bad item" @ " ";
    }
    if ((%item SPC relationType $= "")) {
    }
    if (userName.hasKey()) {
        relationType = %item @ "friend" @ %item;
        UserListFriends;
    }
    %userName = userName;
    %item;
    %isFriend = (%item SPC relationType $= "friend");
    %friendColorTag = %isFriend ? "<color:00ee00ee>" : "";
    if (!(%userName $= "")) {
        %avatarPicURL = $Net::AvatarURL @ urlEncode(%userName) @ "?size=M";
        mBitmapCtrl.downloadAndApplyBitmap(%avatarPicURL);
    }
    mBitmapCtrl.setBitmap("platform/client/ui/tgf/tgf_profile_default");
    mTextCtrl.setTextWithStyle(%container @ %friendColorTag @ %userName);
    mItem = %container @ %item @ %container;
    %container;
    mCurrentItem = %this @ mItemsList;
    1.0 @ (%this % (mItemsList + mCurrentItem));
};
function TGFGoRound::newContentBigThumb(%this) {
    %container = mBigThumbContainer;
    %this;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %item = mItem;
    %lilThumbContainer;
    %userName = userName;
    %item;
    %isFriend = (%item SPC relationType $= "friend");
    %friendColorTag = %isFriend ? "<color:00ee00ee>" : "";
    if (!(%userName $= "")) {
        %avatarPicURL = $Net::AvatarURL @ urlEncode(%userName) @ "?size=L";
        %container.getObject(0).downloadAndApplyBitmap(%avatarPicURL);
    }
    mBitmapCtrl.setBitmap(mBitmapCtrl.getBitmap());
    mTextCtrl.setTextWithStyle(%container @ %friendColorTag @ %userName);
    mItem = %lilThumbContainer @ %item @ %container;
    %container;
    %this.newContentDeets();
};
function TGFGoRound::newContentDeets(%this) {
};
function TGFGoRound::onBigThumbClick(%this, %bigThumbContainer) {
    %this.viewItem(mItem);
};
function TGFGoRound::viewItem(%this, %item) {
    %this.pause();
    "main".DoDetails(%item);
};
function geTGFGoRound_DeetsMLText::onURL(%this, %url) {
    %type = firstWord(%url);
    if (!(%type $= "PROFILE")) {
        error(getScopeName() @ " " @ "- unknown type" @ " " @ %type @ " " @ getTrace());
        return;
    }
    %userName = restWords(%url);
    %userName.viewProfile();
};
function TGFGoRound::setItemList(%this, %list) {
    mItemsList = %list @ %this;
    mCurrentItem = 0 @ %list;
    %n = 0;
    if (((%this * mLilThumbsNumAcross) < %n)) {
        %lilThumbContainer = mLilThumbsContainer.getObject(%n);
        %this;
        %this.newContentLilThumb(%lilThumbContainer);
        %n = (1.0 + %n);
        2.0;
    }
    %this.newContentBigThumb();
};
