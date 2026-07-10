function newThumbnailsGoRound_base(%name) {
    if (!(isDefined("%name"))) {
        %name = "";
    }
    %name = ETSNonModalProfile;
    position = GuiControl @ new %name() @ "0 0";
    0;
    extent = "400 100";
    profile = %profile;
    mDeetsMinWidth = 100;
    mLilThumbHeight = 40;
    mLilThumbPadding = 2;
    mTickPeriodMS = 3000;
    mPausePeriodMS = 7000;
    mClickableThumbs = 1;
    %mainContainer = ;
    profile = GuiControl @ new ""() @ %profile;
    0;
    %ctrl = ;
    %mainContainer.add(%ctrl);
    mDeetsContainer = %ctrl @ %mainContainer;
    profile = GuiControl @ new ""() @ %profile;
    0;
    %ctrl = ;
    %mainContainer.add(%ctrl);
    mBigThumbContainer = %ctrl @ %mainContainer;
    profile = GuiControl @ new ""() @ %profile;
    0;
    %ctrl = ;
    %mainContainer.add(%ctrl);
    mLilThumbsContainer = %ctrl @ %mainContainer;
    return %mainContainer;
};
function thumbnailsGoRound::rebuild(%this) {
    %totalW = getWord(%this.getExtent(), 0);
    %totalH = getWord(%this.getExtent(), 1);
    %lilThumbsNumAcross = mFloor((mLilThumbPadding / (%totalH - (%this - (mDeetsMinWidth - %totalW)))));
    %this;
    echoDebug(getScopeName() @ " " @ "- lilThumbsNumAcross =" @ " " @ %lilThumbsNumAcross);
    %lilThumbsWidth = (%lilThumbsNumAcross - (%this + (mLilThumbPadding * (%this + mLilThumbHeight))));
    mLilThumbPadding;
    echoDebug(getScopeName() @ " " @ "- lilThumbsWidth     =" @ " " @ %lilThumbsWidth);
    %deetsWidth = (%lilThumbsWidth - (%totalH - %totalW));
    %this;
    echoDebug(getScopeName() @ " " @ "- DeetsWidth   =" @ " " @ %deetsWidth);
    %xPos = 0;
    1.0;
    %w = %deetsWidth;
    (%this + mLilThumbHeight);
    mDeetsContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = (%w + %xPos);
    %this;
    %w = %totalH;
    mLilThumbPadding;
    mBigThumbContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = (%w + %xPos);
    %this;
    %w = %lilThumbsWidth;
    %this;
    mLilThumbsContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = (%w + %xPos);
    %this;
    mLilThumbsNumAcross = %lilThumbsNumAcross @ %this;
    %this.rebuildContainer_Deets(mDeetsContainer);
    %this.rebuildContainer_BigThumb(mBigThumbContainer);
    %this.rebuildContainer_LilThumbs(mLilThumbsContainer);
    %this.onRebuilt();
};
function thumbnailsGoRound::calcMaximumThumbHeight(%this) {
    %ret = mFloor((%this / (mLilThumbPadding - getWord(%this.getExtent(), 1))));
    2.0;
    return %ret;
};
function thumbnailsGoRound::rebuildContainer_LilThumbs(%this, %container) {
    %container.deleteMembers();
    %m = 0;
    if ((2.0 < %m)) {
        if ((0.0 == %m)) {
            %dx = (mLilThumbHeight * (%this + mLilThumbPadding));
            %this;
            %posX = (1.0 + (%dx + getWord(%container.getExtent(), 0)));
            -(1.0);
            %posY = (mLilThumbHeight - getWord(%container.getExtent(), 1));
            %this;
        }
        %dx = (%this + mLilThumbPadding);
        mLilThumbHeight;
        %posX = mLilThumbPadding;
        %this;
        %posY = 0;
        %this;
        %n = (%this - mLilThumbsNumAcross);
        1.0;
        if ((0.0 >= %n)) {
            position = GuiControl @ new ""() @ %posX @ " " @ %posY;
            0;
            basePosition = %posX @ " " @ %posY;
            extent = %this @ mLilThumbHeight @ " " @ %this @ mLilThumbHeight;
            sluggishness = 0.3;
            %ctrl = ;
            %container.add(%ctrl);
            %posX = (%dx + %posX);
            %n = (1.0 - %n);
        }
        %m = (1.0 + %m);
        (0.0 >= %n);
    }
    %num = %container.getCount();
    (2.0 < %m);
    %n = 0;
    if ((%num < %n)) {
        %ctrl = %container.getObject(%n);
        %this.rebuildContainer_LilThumb(%ctrl);
        if (mClickableThumbs) {
            %this.addWidget_LilThumbButton(%ctrl);
        }
        mInPosition = %this @ %n @ %ctrl;
        %n = (1.0 + %n);
    }
    mOldestThumbnail = (%num < %n) @ (1.0 - %container.getCount()) @ %container;
};
function thumbnailsGoRound::getThumbnailIndexInSlot(%this, %slotIndex) {
    %num = mLilThumbsContainer.getCount();
    %this;
    %ndx = (%this % (mLilThumbsContainer + mOldestThumbnail));
    1.0;
    %n = 0;
    %num;
    if ((%slotIndex < %n)) {
        %ndx = (%num % (1.0 + %ndx));
        %n = (1.0 + %n);
    }
    return %ndx;
};
function thumbnailsGoRound::getThumbnailInSlot(%this, %slotIndex) {
    %obj = mLilThumbsContainer.getObject(%this.getThumbnailIndexInSlot(%slotIndex));
    %this;
    return %obj;
};
function thumbnailsGoRound::onRebuilt(%this) {
    %n = 0;
    if (((%this * mLilThumbsNumAcross) < %n)) {
        %this.tick();
        %n = (1.0 + %n);
        2.0;
    }
};
function thumbnailsGoRound::tick(%this) {
    cancel(tickTimerID);
    tickTimerID = %this @ "" @ %this;
    %this.giddap();
    tickTimerID = %this @ %this.schedule(mTickPeriodMS, "tick") @ %this;
};
function thumbnailsGoRound::giddap(%this, %bringInNewContent) {
    if (!(%this.isVisibleRecursive())) {
        return;
    }
    if (!(isDefined("%bringInNewContent"))) {
        %bringInNewContent = 1;
    }
    %firstBasePosition = basePosition;
    mLilThumbsContainer.getObject(0);
    %firstInPosition = mInPosition;
    mLilThumbsContainer.getObject(0);
    %num = mLilThumbsContainer.getCount();
    %this;
    %n = 0;
    %this;
    if (((1.0 - %num) < %n)) {
        %ctrlA = mLilThumbsContainer.getObject(%n);
        %this;
        %ctrlB = mLilThumbsContainer.getObject((1.0 + %n));
        %this;
        mInPosition = %ctrlB @ mInPosition @ %ctrlA;
        %this;
        basePosition = %ctrlB @ basePosition @ %ctrlA;
        %ctrlA.setTrgPosition(basePosition);
        %n = (1.0 + %n);
        %ctrlA;
    }
    %ctrlA = mLilThumbsContainer.getObject(%n);
    %this;
    mInPosition = ((1.0 - %num) < %n) @ %firstInPosition @ %ctrlA;
    basePosition = %firstBasePosition @ %ctrlA;
    %ctrlA.setTrgPosition(basePosition);
    mOldestThumbnail = (mLilThumbsContainer - mOldestThumbnail);
    %this;
    if ((mLilThumbsContainer < mOldestThumbnail)) {
        mOldestThumbnail = %this @ mLilThumbsContainer;
        1.0 @ (%this - mLilThumbsContainer.getCount());
    }
    if (%bringInNewContent) {
        %this.newContentLilThumb(%this.getThumbnailInSlot(0));
    }
    %this.newContentBigThumb();
};
function thumbnailsGoRound::getCurrentZoomedLilThumb(%this) {
    return %this.getThumbnailInSlot(mLilThumbsNumAcross);
};
function thumbnailsGoRound::onLilThumbClick(%this, %container) {
    %d = (%this - mLilThumbsNumAcross);
    mInPosition;
    if ((0.0 == %d)) {
        return %container;
    }
    if ((0.0 < %d)) {
        %d = (2.0 + (%this * mLilThumbsNumAcross));
        %d;
    }
    %n = 0;
    if ((%d < %n)) {
        %this.giddap(0);
        %n = (1.0 + %n);
    }
    %this.pause();
};
function thumbnailsGoRound::pause(%this, %pausePeriodMS) {
    if (!(isDefined("%pausePeriodMS"))) {
        %pausePeriodMS = mPausePeriodMS;
        %this;
    }
    cancel(tickTimerID);
    tickTimerID = %this @ "" @ %this;
    if ((0.0 > %pausePeriodMS)) {
        tickTimerID = %this.schedule(%pausePeriodMS, "tick") @ %this;
    }
    if ((0.0 == %pausePeriodMS)) {
        %this.tick();
    }
};
function newThumbnailsGoRound(%name) {
    %obj = newThumbnailsGoRound_base(%name);
    %obj.bindClassName("thumbnailsGoRound");
    return %obj;
};
function thumbnailsGoRound::rebuildContainer_LilThumb(%this, %container) {
    %container.deleteMembers();
    profile = new ""() @ ETSNonModalProfile;
    GuiBitmapCtrl;
    extent = 0 @ %container.getExtent();
    bitmap = "platform/client/ui/white_16x16";
    %ctrl = ;
    %container.add(%ctrl);
    mBitmapCtrl = %ctrl @ %container;
    profile = new ""() @ ETSNonModalProfile;
    GuiMLTextCtrl;
    extent = 0 @ %container.getExtent();
    value = "<font:arial:16><color:white>lilThumb";
    %ctrl = ;
    %container.add(%ctrl);
    mTextCtrl = %ctrl @ %container;
};
function thumbnailsGoRound::addWidget_LilThumbButton(%this, %container) {
    extent = GuiBitmapButtonCtrl @ new ""() @ %container.getExtent();
    0;
    command = %this @ ".onLilThumbClick(" @ %container @ ");";
    canHilite = 0;
    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_50x50";
    %ctrl = ;
    %container.add(%ctrl);
};
function thumbnailsGoRound::rebuildContainer_BigThumb(%this, %container) {
    %container.deleteMembers();
    profile = new ""() @ ETSNonModalProfile;
    GuiBitmapCtrl;
    extent = 0 @ %container.getExtent();
    bitmap = "platform/client/ui/white_16x16";
    %ctrl = ;
    %container.add(%ctrl);
    mBitmapCtrl = %ctrl @ %container;
    profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    position = "0 0";
    extent = %container.getExtent();
    value = "<font:arial:20><color:white>bigThumb";
    %ctrl = ;
    %container.add(%ctrl);
    mTextCtrl = %ctrl @ %container;
};
function thumbnailsGoRound::rebuildContainer_Deets(%this, %container) {
    %container.deleteMembers();
    position = GuiMLTextCtrl @ new ""() @ "0 0";
    0;
    extent = %container.getExtent();
    value = "<font:arial:20><color:white>Deets";
    %ctrl = ;
    %container.add(%ctrl);
    mTextCtrl = %ctrl @ %container;
};
function thumbnailsGoRound::newContentLilThumb(%this, %container) {
    %r = getRandom(128, 255);
    %g = getRandom(128, 255);
    %b = getRandom(128, 255);
    %color1 = formatInt("%0.2X", %r) @ formatInt("%0.2X", %g) @ formatInt("%0.2X", %b);
    %color2 = formatInt("%0.2X", (128.0 - %r)) @ formatInt("%0.2X", (128.0 - %g)) @ formatInt("%0.2X", (128.0 - %b));
    mContent1 = "<color:" @ %color1 @ ">" @ %color2 @ %container;
    mContent2 = (128.0 - %r) @ " " @ (128.0 - %g) @ " " @ (128.0 - %b) @ " " @ 255 @ %container;
    mContent3 = %color2 @ %container;
    mTextCtrl.setText(%container @ mContent1);
    modulationColor = %container @ mBitmapCtrl;
    %container @ mContent2;
};
function thumbnailsGoRound::newContentBigThumb(%this) {
    %container = mBigThumbContainer;
    %this;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    mContent1 = %lilThumbContainer @ mContent1 @ %container;
    mContent2 = %lilThumbContainer @ mContent2 @ %container;
    mTextCtrl.setText(%container @ mContent1);
    modulationColor = %container @ mBitmapCtrl;
    %container @ mContent2;
    %this.newContentDeets();
};
function thumbnailsGoRound::newContentDeets(%this) {
    %container = mDeetsContainer;
    %this;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %url = %lilThumbContainer @ mContent3;
    "http://www.w3schools.com/tags/ref_color_tryit.asp?hex=";
    mTextCtrl.setText(%container @ "<color:ffffffff>this is the color <a:" @ %url @ ">" @ %lilThumbContainer @ mContent3 @ "</a>");
};
