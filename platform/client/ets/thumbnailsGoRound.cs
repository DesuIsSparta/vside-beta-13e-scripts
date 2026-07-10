function newThumbnailsGoRound_base(%name) {
    if (!(isDefined("%name"))) {
        %name = "";
    }
    %name = ETSNonModalProfile;
    %mainContainer = new GuiControl(%name) {
        position = 0 @ "0 0";
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
        profile = 0 @ %profile;
    };
    %mainContainer.add(%ctrl);
    %mainContainer.mDeetsContainer = %ctrl;
    %ctrl = new GuiControl("") {
        profile = 0 @ %profile;
    };
    %mainContainer.add(%ctrl);
    %mainContainer.mBigThumbContainer = %ctrl;
    %ctrl = new GuiControl("") {
        profile = 0 @ %profile;
    };
    %mainContainer.add(%ctrl);
    %mainContainer.mLilThumbsContainer = %ctrl;
    return %mainContainer;
};
function thumbnailsGoRound::rebuild(%this) {
    %totalW = getWord(%this.getExtent(), 0);
    %totalH = getWord(%this.getExtent(), 1);
    %lilThumbsNumAcross = mFloor(((%this.mLilThumbPadding + %this.mLilThumbHeight) / (%this.mLilThumbPadding - (%totalH - (%this.mDeetsMinWidth - %totalW)))));
    echoDebug(getScopeName() @ " " @ "- lilThumbsNumAcross =" @ " " @ %lilThumbsNumAcross);
    %lilThumbsWidth = (1.0 - (%this.mLilThumbPadding + (%lilThumbsNumAcross * (%this.mLilThumbPadding + %this.mLilThumbHeight))));
    echoDebug(getScopeName() @ " " @ "- lilThumbsWidth     =" @ " " @ %lilThumbsWidth);
    %deetsWidth = (%lilThumbsWidth - (%totalH - %totalW));
    echoDebug(getScopeName() @ " " @ "- DeetsWidth   =" @ " " @ %deetsWidth);
    %xPos = 0;
    %w = %deetsWidth;
    %this.mDeetsContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = (%w + %xPos);
    %w = %totalH;
    %this.mBigThumbContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = (%w + %xPos);
    %w = %lilThumbsWidth;
    %this.mLilThumbsContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = (%w + %xPos);
    %this.mLilThumbsNumAcross = %lilThumbsNumAcross;
    %this.rebuildContainer_Deets(%this.mDeetsContainer);
    %this.rebuildContainer_BigThumb(%this.mBigThumbContainer);
    %this.rebuildContainer_LilThumbs(%this.mLilThumbsContainer);
    %this.onRebuilt();
};
function thumbnailsGoRound::calcMaximumThumbHeight(%this) {
    %ret = mFloor((2.0 / (%this.mLilThumbPadding - getWord(%this.getExtent(), 1))));
    return %ret;
};
function thumbnailsGoRound::rebuildContainer_LilThumbs(%this, %container) {
    %container.deleteMembers();
    %m = 0;
    if ((2.0 < %m)) {
        if ((0.0 == %m)) {
            %dx = (-(1.0) * (%this.mLilThumbHeight + %this.mLilThumbPadding));
            %posX = (1.0 + (%dx + getWord(%container.getExtent(), 0)));
            %posY = (%this.mLilThumbHeight - getWord(%container.getExtent(), 1));
        }
        %dx = (%this.mLilThumbHeight + %this.mLilThumbPadding);
        %posX = %this.mLilThumbPadding;
        %posY = 0;
        %n = (1.0 - %this.mLilThumbsNumAcross);
        if ((0.0 >= %n)) {
            %ctrl = new GuiControl("") {
                position = 0 @ %posX @ " " @ %posY;
                basePosition = %posX @ " " @ %posY;
                extent = %this.mLilThumbHeight @ " " @ %this.mLilThumbHeight;
                sluggishness = 0.3;
            };
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
        if (%this.mClickableThumbs) {
            %this.addWidget_LilThumbButton(%ctrl);
        }
        %ctrl.mInPosition = %n;
        %n = (1.0 + %n);
    }
    %container.mOldestThumbnail = (%num < %n) @ (1.0 - %container.getCount());
};
function thumbnailsGoRound::getThumbnailIndexInSlot(%this, %slotIndex) {
    %num = %this.mLilThumbsContainer.getCount();
    %ndx = (%num % (1.0 + %this.mLilThumbsContainer.mOldestThumbnail));
    %n = 0;
    if ((%slotIndex < %n)) {
        %ndx = (%num % (1.0 + %ndx));
        %n = (1.0 + %n);
    }
    return %ndx;
};
function thumbnailsGoRound::getThumbnailInSlot(%this, %slotIndex) {
    %obj = %this.mLilThumbsContainer.getObject(%this.getThumbnailIndexInSlot(%slotIndex));
    return %obj;
};
function thumbnailsGoRound::onRebuilt(%this) {
    %n = 0;
    if (((2.0 * %this.mLilThumbsNumAcross) < %n)) {
        %this.tick();
        %n = (1.0 + %n);
    }
};
function thumbnailsGoRound::tick(%this) {
    cancel(%this.tickTimerID);
    %this.tickTimerID = "";
    %this.giddap();
    %this.tickTimerID = %this.schedule(%this.mTickPeriodMS, "tick");
};
function thumbnailsGoRound::giddap(%this, %bringInNewContent) {
    if (!(%this.isVisibleRecursive())) {
        return;
    }
    if (!(isDefined("%bringInNewContent"))) {
        %bringInNewContent = 1;
    }
    %firstBasePosition = %this.mLilThumbsContainer.getObject(0).basePosition;
    %firstInPosition = %this.mLilThumbsContainer.getObject(0).mInPosition;
    %num = %this.mLilThumbsContainer.getCount();
    %n = 0;
    if (((1.0 - %num) < %n)) {
        %ctrlA = %this.mLilThumbsContainer.getObject(%n);
        %ctrlB = %this.mLilThumbsContainer.getObject((1.0 + %n));
        %ctrlA.mInPosition = %ctrlB.mInPosition;
        %ctrlA.basePosition = %ctrlB.basePosition;
        %ctrlA.setTrgPosition(%ctrlA.basePosition);
        %n = (1.0 + %n);
    }
    %ctrlA = %this.mLilThumbsContainer.getObject(%n);
    ((1.0 - %num) < %n);
    %ctrlA.mInPosition = %firstInPosition;
    %ctrlA.basePosition = %firstBasePosition;
    %ctrlA.setTrgPosition(%ctrlA.basePosition);
    %this.mLilThumbsContainer.mOldestThumbnail = (1.0 - %this.mLilThumbsContainer.mOldestThumbnail);
    if ((0.0 < %this.mLilThumbsContainer.mOldestThumbnail)) {
        %this.mLilThumbsContainer.mOldestThumbnail = (1.0 - %this.mLilThumbsContainer.getCount());
    }
    if (%bringInNewContent) {
        %this.newContentLilThumb(%this.getThumbnailInSlot(0));
    }
    %this.newContentBigThumb();
};
function thumbnailsGoRound::getCurrentZoomedLilThumb(%this) {
    return %this.getThumbnailInSlot(%this.mLilThumbsNumAcross);
};
function thumbnailsGoRound::onLilThumbClick(%this, %container) {
    %d = (%container.mInPosition - %this.mLilThumbsNumAcross);
    if ((0.0 == %d)) {
        return;
    }
    if ((0.0 < %d)) {
        %d = (%d + (2.0 * %this.mLilThumbsNumAcross));
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
        %pausePeriodMS = %this.mPausePeriodMS;
    }
    cancel(%this.tickTimerID);
    %this.tickTimerID = "";
    if ((0.0 > %pausePeriodMS)) {
        %this.tickTimerID = %this.schedule(%pausePeriodMS, "tick");
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
    %ctrl = new GuiBitmapCtrl("") {
        profile = 0 @ ETSNonModalProfile;
        extent = %container.getExtent();
        bitmap = "platform/client/ui/white_16x16";
    };
    %container.add(%ctrl);
    %container.mBitmapCtrl = %ctrl;
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ ETSNonModalProfile;
        extent = %container.getExtent();
        value = "<font:arial:16><color:white>lilThumb";
    };
    %container.add(%ctrl);
    %container.mTextCtrl = %ctrl;
};
function thumbnailsGoRound::addWidget_LilThumbButton(%this, %container) {
    %ctrl = new GuiBitmapButtonCtrl("") {
        extent = 0 @ %container.getExtent();
        command = %this @ ".onLilThumbClick(" @ %container @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_50x50";
    };
    %container.add(%ctrl);
};
function thumbnailsGoRound::rebuildContainer_BigThumb(%this, %container) {
    %container.deleteMembers();
    %ctrl = new GuiBitmapCtrl("") {
        profile = 0 @ ETSNonModalProfile;
        extent = %container.getExtent();
        bitmap = "platform/client/ui/white_16x16";
    };
    %container.add(%ctrl);
    %container.mBitmapCtrl = %ctrl;
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        position = "0 0";
        extent = %container.getExtent();
        value = "<font:arial:20><color:white>bigThumb";
    };
    %container.add(%ctrl);
    %container.mTextCtrl = %ctrl;
};
function thumbnailsGoRound::rebuildContainer_Deets(%this, %container) {
    %container.deleteMembers();
    %ctrl = new GuiMLTextCtrl("") {
        position = 0 @ "0 0";
        extent = %container.getExtent();
        value = "<font:arial:20><color:white>Deets";
    };
    %container.add(%ctrl);
    %container.mTextCtrl = %ctrl;
};
function thumbnailsGoRound::newContentLilThumb(%this, %container) {
    %r = getRandom(128, 255);
    %g = getRandom(128, 255);
    %b = getRandom(128, 255);
    %color1 = formatInt("%0.2X", %r) @ formatInt("%0.2X", %g) @ formatInt("%0.2X", %b);
    %color2 = formatInt("%0.2X", (128.0 - %r)) @ formatInt("%0.2X", (128.0 - %g)) @ formatInt("%0.2X", (128.0 - %b));
    %container.mContent1 = "<color:" @ %color1 @ ">" @ %color2;
    %container.mContent2 = (128.0 - %r) @ " " @ (128.0 - %g) @ " " @ (128.0 - %b) @ " " @ 255;
    %container.mContent3 = %color2;
    %container.mTextCtrl.setText("<font:arial:10>" @ " " @ %container.mContent1);
    %container.mBitmapCtrl.modulationColor = %container.mContent2;
};
function thumbnailsGoRound::newContentBigThumb(%this) {
    %container = %this.mBigThumbContainer;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %container.mContent1 = %lilThumbContainer.mContent1;
    %container.mContent2 = %lilThumbContainer.mContent2;
    %container.mTextCtrl.setText("<font:arial:16>" @ " " @ %container.mContent1);
    %container.mBitmapCtrl.modulationColor = %container.mContent2;
    %this.newContentDeets();
};
function thumbnailsGoRound::newContentDeets(%this) {
    %container = %this.mDeetsContainer;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %url = "http://www.w3schools.com/tags/ref_color_tryit.asp?hex=" @ %lilThumbContainer.mContent3;
    %container.mTextCtrl.setText("<color:ffffffff>this is the color <a:" @ %url @ ">" @ %lilThumbContainer.mContent3 @ "</a>");
};
