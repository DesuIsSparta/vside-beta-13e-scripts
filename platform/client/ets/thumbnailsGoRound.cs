function newThumbnailsGoRound_base(%name) {
    if (!(isDefined("%name"))) {
        %name = "";
    }
    %profile = ETSNonModalProfile;
    %mainContainer = new GuiControl(%name) {
        position = "0 0";
        extent = "400 100";
        profile = %profile;
        mDeetsMinWidth = 100;
        mLilThumbHeight = 40;
        mLilThumbPadding = 2;
        mTickPeriodMS = 3000;
        mPausePeriodMS = 7000;
        mClickableThumbs = 1;
    };
    %ctrl = new GuiControl("") {
        profile = %profile;
    };
    %ctrl.add(%mainContainer);
    %mainContainer.mDeetsContainer = %ctrl;
    %ctrl = new GuiControl("") {
        profile = %profile;
    };
    %ctrl.add(%mainContainer);
    %mainContainer.mBigThumbContainer = %ctrl;
    %ctrl = new GuiControl("") {
        profile = %profile;
    };
    %ctrl.add(%mainContainer);
    %mainContainer.mLilThumbsContainer = %ctrl;
    return %mainContainer;
};
function thumbnailsGoRound::rebuild(%this) {
    %totalW = getWord(%this.getExtent(), 0);
    %totalH = getWord(%this.getExtent(), 1);
    %lilThumbsNumAcross = mFloor(((((%totalW - %this.mDeetsMinWidth) - %totalH) - %this.mLilThumbPadding) / (%this.mLilThumbHeight + %this.mLilThumbPadding)));
    echoDebug(getScopeName() @ " " @ "- lilThumbsNumAcross =" @ " " @ %lilThumbsNumAcross);
    %lilThumbsWidth = ((((%this.mLilThumbHeight + %this.mLilThumbPadding) * %lilThumbsNumAcross) + %this.mLilThumbPadding) - 1.0);
    echoDebug(getScopeName() @ " " @ "- lilThumbsWidth     =" @ " " @ %lilThumbsWidth);
    %deetsWidth = ((%totalW - %totalH) - %lilThumbsWidth);
    echoDebug(getScopeName() @ " " @ "- DeetsWidth   =" @ " " @ %deetsWidth);
    %xPos = 0;
    %w = %deetsWidth;
    %totalH.resize(%this.mDeetsContainer, %xPos, 0, %w);
    %xPos = (%xPos + %w);
    %w = %totalH;
    %totalH.resize(%this.mBigThumbContainer, %xPos, 0, %w);
    %xPos = (%xPos + %w);
    %w = %lilThumbsWidth;
    %totalH.resize(%this.mLilThumbsContainer, %xPos, 0, %w);
    %xPos = (%xPos + %w);
    %this.mLilThumbsNumAcross = %lilThumbsNumAcross;
    %this.mDeetsContainer.rebuildContainer_Deets(%this);
    %this.mBigThumbContainer.rebuildContainer_BigThumb(%this);
    %this.mLilThumbsContainer.rebuildContainer_LilThumbs(%this);
    %this.onRebuilt();
};
function thumbnailsGoRound::calcMaximumThumbHeight(%this) {
    %ret = mFloor(((getWord(%this.getExtent(), 1) - %this.mLilThumbPadding) / 2.0));
    return %ret;
};
function thumbnailsGoRound::rebuildContainer_LilThumbs(%this, %container) {
    %container.deleteMembers();
    %m = 0;
    while ((%m < 2.0)) {
        if ((%m == 0.0)) {
            %dx = ((%this.mLilThumbPadding + %this.mLilThumbHeight) * -(1.0));
            %posX = ((getWord(%container.getExtent(), 0) + %dx) + 1.0);
            %posY = (getWord(%container.getExtent(), 1) - %this.mLilThumbHeight);
        }
        %dx = (%this.mLilThumbPadding + %this.mLilThumbHeight);
        %posX = %this.mLilThumbPadding;
        %posY = 0;
        %n = (%this.mLilThumbsNumAcross - 1.0);
        while ((%n >= 0.0)) {
            %ctrl = new GuiControl("") {
                position = %posX @ " " @ %posY;
                basePosition = %posX @ " " @ %posY;
                extent = %this.mLilThumbHeight @ " " @ %this.mLilThumbHeight;
                sluggishness = 0.3;
            };
            %ctrl.add(%container);
            %posX = (%posX + %dx);
            %n = (%n - 1.0);
        }
        %m = (%m + 1.0);
        (%n >= 0.0);
    }
    %num = %container.getCount();
    (%m < 2.0);
    %n = 0;
    while ((%n < %num)) {
        %ctrl = %n.getObject(%container);
        %ctrl.rebuildContainer_LilThumb(%this);
        if (%this.mClickableThumbs) {
            %ctrl.addWidget_LilThumbButton(%this);
        }
        %ctrl.mInPosition = %n;
        %n = (%n + 1.0);
    }
    %container.mOldestThumbnail = (%n < %num) @ (%container.getCount() - 1.0);
};
function thumbnailsGoRound::getThumbnailIndexInSlot(%this, %slotIndex) {
    %num = %this.mLilThumbsContainer.getCount();
    %ndx = ((%this.mLilThumbsContainer.mOldestThumbnail + 1.0) % %num);
    %n = 0;
    while ((%n < %slotIndex)) {
        %ndx = ((%ndx + 1.0) % %num);
        %n = (%n + 1.0);
    }
    return %ndx;
};
function thumbnailsGoRound::getThumbnailInSlot(%this, %slotIndex) {
    %obj = %slotIndex.getThumbnailIndexInSlot(%this).getObject(%this.mLilThumbsContainer);
    return %obj;
};
function thumbnailsGoRound::onRebuilt(%this) {
    %n = 0;
    while ((%n < (%this.mLilThumbsNumAcross * 2.0))) {
        %this.tick();
        %n = (%n + 1.0);
    }
};
function thumbnailsGoRound::tick(%this) {
    cancel(%this.tickTimerID);
    %this.tickTimerID = "";
    %this.giddap();
    %this.tickTimerID = "tick".schedule(%this, %this.mTickPeriodMS);
};
function thumbnailsGoRound::giddap(%this, %bringInNewContent) {
    if (!(%this.isVisibleRecursive())) {
        return;
    }
    if (!(isDefined("%bringInNewContent"))) {
        %bringInNewContent = 1;
    }
    %firstBasePosition = 0.getObject(%this.mLilThumbsContainer).basePosition;
    %firstInPosition = 0.getObject(%this.mLilThumbsContainer).mInPosition;
    %num = %this.mLilThumbsContainer.getCount();
    %n = 0;
    while ((%n < (%num - 1.0))) {
        %ctrlA = %n.getObject(%this.mLilThumbsContainer);
        %ctrlB = (%n + 1.0).getObject(%this.mLilThumbsContainer);
        %ctrlA.mInPosition = %ctrlB.mInPosition;
        %ctrlA.basePosition = %ctrlB.basePosition;
        %ctrlA.basePosition.setTrgPosition(%ctrlA);
        %n = (%n + 1.0);
    }
    %ctrlA = %n.getObject(%this.mLilThumbsContainer);
    (%n < (%num - 1.0));
    %ctrlA.mInPosition = %firstInPosition;
    %ctrlA.basePosition = %firstBasePosition;
    %ctrlA.basePosition.setTrgPosition(%ctrlA);
    %this.mLilThumbsContainer.mOldestThumbnail = (%this.mLilThumbsContainer.mOldestThumbnail - 1.0);
    if ((%this.mLilThumbsContainer.mOldestThumbnail < 0.0)) {
        %this.mLilThumbsContainer.mOldestThumbnail = (%this.mLilThumbsContainer.getCount() - 1.0);
    }
    if (%bringInNewContent) {
        0.getThumbnailInSlot(%this).newContentLilThumb(%this);
    }
    %this.newContentBigThumb();
};
function thumbnailsGoRound::getCurrentZoomedLilThumb(%this) {
    return %this.mLilThumbsNumAcross.getThumbnailInSlot(%this);
};
function thumbnailsGoRound::onLilThumbClick(%this, %container) {
    %d = (%this.mLilThumbsNumAcross - %container.mInPosition);
    if ((%d == 0.0)) {
        return;
    }
    if ((%d < 0.0)) {
        %d = ((%this.mLilThumbsNumAcross * 2.0) + %d);
    }
    %n = 0;
    while ((%n < %d)) {
        0.giddap(%this);
        %n = (%n + 1.0);
    }
    %this.pause();
};
function thumbnailsGoRound::pause(%this, %pausePeriodMS) {
    if (!(isDefined("%pausePeriodMS"))) {
        %pausePeriodMS = %this.mPausePeriodMS;
    }
    cancel(%this.tickTimerID);
    %this.tickTimerID = "";
    if ((%pausePeriodMS > 0.0)) {
        %this.tickTimerID = "tick".schedule(%this, %pausePeriodMS);
    }
    if ((%pausePeriodMS == 0.0)) {
        %this.tick();
    }
};
function newThumbnailsGoRound(%name) {
    %obj = newThumbnailsGoRound_base(%name);
    "thumbnailsGoRound".bindClassName(%obj);
    return %obj;
};
function thumbnailsGoRound::rebuildContainer_LilThumb(%this, %container) {
    %container.deleteMembers();
    %ctrl = new GuiBitmapCtrl("") {
        profile = ETSNonModalProfile;
        extent = %container.getExtent();
        bitmap = "platform/client/ui/white_16x16";
    };
    %ctrl.add(%container);
    %container.mBitmapCtrl = %ctrl;
    %ctrl = new GuiMLTextCtrl("") {
        profile = ETSNonModalProfile;
        extent = %container.getExtent();
        value = "<font:arial:16><color:white>lilThumb";
    };
    %ctrl.add(%container);
    %container.mTextCtrl = %ctrl;
};
function thumbnailsGoRound::addWidget_LilThumbButton(%this, %container) {
    %ctrl = new GuiBitmapButtonCtrl("") {
        extent = %container.getExtent();
        command = %this @ ".onLilThumbClick(" @ %container @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_50x50";
    };
    %ctrl.add(%container);
};
function thumbnailsGoRound::rebuildContainer_BigThumb(%this, %container) {
    %container.deleteMembers();
    %ctrl = new GuiBitmapCtrl("") {
        profile = ETSNonModalProfile;
        extent = %container.getExtent();
        bitmap = "platform/client/ui/white_16x16";
    };
    %ctrl.add(%container);
    %container.mBitmapCtrl = %ctrl;
    %ctrl = new GuiMLTextCtrl("") {
        profile = "ETSNonModalProfile";
        position = "0 0";
        extent = %container.getExtent();
        value = "<font:arial:20><color:white>bigThumb";
    };
    %ctrl.add(%container);
    %container.mTextCtrl = %ctrl;
};
function thumbnailsGoRound::rebuildContainer_Deets(%this, %container) {
    %container.deleteMembers();
    %ctrl = new GuiMLTextCtrl("") {
        position = "0 0";
        extent = %container.getExtent();
        value = "<font:arial:20><color:white>Deets";
    };
    %ctrl.add(%container);
    %container.mTextCtrl = %ctrl;
};
function thumbnailsGoRound::newContentLilThumb(%this, %container) {
    %r = getRandom(128, 255);
    %g = getRandom(128, 255);
    %b = getRandom(128, 255);
    %color1 = formatInt("%0.2X", %r) @ formatInt("%0.2X", %g) @ formatInt("%0.2X", %b);
    %color2 = formatInt("%0.2X", (%r - 128.0)) @ formatInt("%0.2X", (%g - 128.0)) @ formatInt("%0.2X", (%b - 128.0));
    %container.mContent1 = "<color:" @ %color1 @ ">" @ %color2;
    %container.mContent2 = (%r - 128.0) @ " " @ (%g - 128.0) @ " " @ (%b - 128.0) @ " " @ 255;
    %container.mContent3 = %color2;
    "<font:arial:10>" @ " " @ %container.mContent1.setText(%container.mTextCtrl);
    %container.mBitmapCtrl.modulationColor = %container.mContent2;
};
function thumbnailsGoRound::newContentBigThumb(%this) {
    %container = %this.mBigThumbContainer;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %container.mContent1 = %lilThumbContainer.mContent1;
    %container.mContent2 = %lilThumbContainer.mContent2;
    "<font:arial:16>" @ " " @ %container.mContent1.setText(%container.mTextCtrl);
    %container.mBitmapCtrl.modulationColor = %container.mContent2;
    %this.newContentDeets();
};
function thumbnailsGoRound::newContentDeets(%this) {
    %container = %this.mDeetsContainer;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %url = "http://www.w3schools.com/tags/ref_color_tryit.asp?hex=" @ %lilThumbContainer.mContent3;
    "<color:ffffffff>this is the color <a:" @ %url @ ">" @ %lilThumbContainer.mContent3 @ "</a>".setText(%container.mTextCtrl);
};
