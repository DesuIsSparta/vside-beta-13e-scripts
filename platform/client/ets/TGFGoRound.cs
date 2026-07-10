function newTGFGoRound(%name) {
    %obj = newThumbnailsGoRound(%name);
    %obj.bindClassName("TGFGoRound");
    return %obj;
};
function TGFGoRound::rebuildContainer_LilThumb(%this, %container) {
    %container.deleteMembers();
    0;
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ ETSNonModalProfile;
        extent = %container.getExtent();
    };
    %container.add(%ctrl);
    %container.mBitmapCtrl = %ctrl;
    0;
    %ctrlB = new ""() {
        profile = GuiControl @ EtsDarkBorderlessBoxProfile;
        extent = %container.getExtent();
        position = 0 @ " " @ (10.0 - getWord(%container.getExtent(), 1));
    };
    %container.add(%ctrlB);
    0;
    %ctrl = new ""() {
        profile = GuiMLTextCtrl @ ETSNonModalProfile;
        extent = %ctrlB.getExtent();
        style = "tgfGoRoundLilThumb";
    };
    %ctrlB.add(%ctrl);
    %container.mTextCtrl = %ctrl;
};
function TGFGoRound::rebuildContainer_BigThumb(%this, %container) {
    %container.deleteMembers();
    0;
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        position = "0 0";
        extent = %container.getExtent();
    };
    %container.add(%ctrl);
    %container.mBitmapCtrl = %ctrl;
    0;
    %ctrlB = new ""() {
        profile = GuiControl @ EtsDarkBorderlessBoxProfile;
        extent = %container.getExtent();
        position = 0 @ " " @ (18.0 - getWord(%container.getExtent(), 1));
    };
    %container.add(%ctrlB);
    0;
    %ctrl = new ""() {
        profile = GuiMLTextCtrl @ ETSNonModalProfile;
        extent = %ctrlB.getExtent();
        style = "tgfGoRoundBigThumb";
    };
    %ctrlB.add(%ctrl);
    %container.mTextCtrl = %ctrl;
    0;
    %ctrl = new ""() {
        position = GuiBitmapButtonCtrl @ "-2 -2";
        extent = VectorAdd(%container.getExtent(), "4 4");
        command = %this @ ".onBigThumbClick(" @ %container @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
    };
    %container.add(%ctrl);
};
function TGFGoRound::rebuildContainer_Deets(%this, %container) {
    %container.deleteMembers();
};
function TGFGoRound::newContentLilThumb(%this, %container) {
    if (!(isObject(%this.mItemsList))) {
        error(getScopeName() @ " " @ "- no list" @ " " @ getTrace());
        return;
    }
    %item = %this.mItemsList.getValue(%this.mItemsList.mCurrentItem);
    if (!(isObject(%item))) {
        error(getScopeName() @ " " @ "- bad item" @ " " @ %this.mItemsList.mCurrentItem @ " " @ getTrace());
        %this.mItemsList.mCurrentItem = 0;
        return;
    }
    if ((%item.relationType $= "")) {
    }
    if (%item.userName.hasKey()) {
        %item.relationType = UserListFriends @ "friend";
    }
    %userName = %item.userName;
    %isFriend = (%item.relationType $= "friend");
    %friendColorTag = %isFriend ? "<color:00ee00ee>" : "";
    if (!(%userName $= "")) {
        %avatarPicURL = $Net::AvatarURL @ urlEncode(%userName) @ "?size=M";
        %container.mBitmapCtrl.downloadAndApplyBitmap(%avatarPicURL);
    }
    %container.mBitmapCtrl.setBitmap("platform/client/ui/tgf/tgf_profile_default");
    %container.mTextCtrl.setTextWithStyle(%friendColorTag @ %userName);
    %container.mItem = %item;
    %this.mItemsList.mCurrentItem = (%this.mItemsList.size() % (1.0 + %this.mItemsList.mCurrentItem));
};
function TGFGoRound::newContentBigThumb(%this) {
    %container = %this.mBigThumbContainer;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %item = %lilThumbContainer.mItem;
    %userName = %item.userName;
    %isFriend = (%item.relationType $= "friend");
    %friendColorTag = %isFriend ? "<color:00ee00ee>" : "";
    if (!(%userName $= "")) {
        %avatarPicURL = $Net::AvatarURL @ urlEncode(%userName) @ "?size=L";
        %container.getObject(0).downloadAndApplyBitmap(%avatarPicURL);
    }
    %container.mBitmapCtrl.setBitmap(%lilThumbContainer.mBitmapCtrl.getBitmap());
    %container.mTextCtrl.setTextWithStyle(%friendColorTag @ %userName);
    %container.mItem = %item;
    %this.newContentDeets();
};
function TGFGoRound::newContentDeets(%this) {
};
function TGFGoRound::onBigThumbClick(%this, %bigThumbContainer) {
    %this.viewItem(%bigThumbContainer.mItem);
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
    %this.mItemsList = %list;
    %list.mCurrentItem = 0;
    %n = 0;
    if (((2.0 * %this.mLilThumbsNumAcross) < %n)) {
        %lilThumbContainer = %this.mLilThumbsContainer.getObject(%n);
        %this.newContentLilThumb(%lilThumbContainer);
        %n = (1.0 + %n);
    }
    %this.newContentBigThumb();
};
