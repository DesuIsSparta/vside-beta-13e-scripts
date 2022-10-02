function newThumbnailsGoRound_base(%name)
{
    if (!isDefined("%name"))
    {
        %name = "";
    }
    %profile = ETSNonModalProfile;
    %mainContainer = new GuiControl(%name)
    {
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
    %ctrl = new GuiControl();
    %mainContainer.add(%ctrl);
    %mainContainer.mDeetsContainer = %ctrl;
    %ctrl = new GuiControl();
    %mainContainer.add(%ctrl);
    %mainContainer.mBigThumbContainer = %ctrl;
    %ctrl = new GuiControl();
    %mainContainer.add(%ctrl);
    %mainContainer.mLilThumbsContainer = %ctrl;
    return %mainContainer;
}
function thumbnailsGoRound::rebuild(%this)
{
    %totalW = getWord(%this.getExtent(), 0);
    %totalH = getWord(%this.getExtent(), 1);
    %lilThumbsNumAcross = mFloor((((%totalW - %this.mDeetsMinWidth) - %totalH) - %this.mLilThumbPadding) / (%this.mLilThumbHeight + %this.mLilThumbPadding));
    echoDebug(getScopeName() SPC "- lilThumbsNumAcross =" SPC %lilThumbsNumAcross);
    %lilThumbsWidth = (((%this.mLilThumbHeight + %this.mLilThumbPadding) * %lilThumbsNumAcross) + %this.mLilThumbPadding) - 1;
    echoDebug(getScopeName() SPC "- lilThumbsWidth     =" SPC %lilThumbsWidth);
    %deetsWidth = (%totalW - %totalH) - %lilThumbsWidth;
    echoDebug(getScopeName() SPC "- DeetsWidth   =" SPC %deetsWidth);
    %xPos = 0;
    %w = %deetsWidth;
    %this.mDeetsContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = %xPos + %w;
    %w = %totalH;
    %this.mBigThumbContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = %xPos + %w;
    %w = %lilThumbsWidth;
    %this.mLilThumbsContainer.resize(%xPos, 0, %w, %totalH);
    %xPos = %xPos + %w;
    %this.mLilThumbsNumAcross = %lilThumbsNumAcross;
    %this.rebuildContainer_Deets(%this.mDeetsContainer);
    %this.rebuildContainer_BigThumb(%this.mBigThumbContainer);
    %this.rebuildContainer_LilThumbs(%this.mLilThumbsContainer);
    %this.onRebuilt();
    return ;
}
function thumbnailsGoRound::calcMaximumThumbHeight(%this)
{
    %ret = mFloor((getWord(%this.getExtent(), 1) - %this.mLilThumbPadding) / 2);
    return %ret;
}
function thumbnailsGoRound::rebuildContainer_LilThumbs(%this, %container)
{
    %container.deleteMembers();
    %m = 0;
    while (%m < 2)
    {
        if (%m == 0)
        {
            %dx = (%this.mLilThumbPadding + %this.mLilThumbHeight) * -1;
            %posX = (getWord(%container.getExtent(), 0) + %dx) + 1;
            %posY = getWord(%container.getExtent(), 1) - %this.mLilThumbHeight;
        }
        else
        {
            %dx = %this.mLilThumbPadding + %this.mLilThumbHeight;
            %posX = %this.mLilThumbPadding;
            %posY = 0;
        }
        %n = %this.mLilThumbsNumAcross - 1;
        while (%n >= 0)
        {
            %ctrl = new GuiControl()
            {
                position = %posX SPC %posY;
                basePosition = %posX SPC %posY;
                extent = %this.mLilThumbHeight SPC %this.mLilThumbHeight;
                sluggishness = 0.3;
            };
            %container.add(%ctrl);
            %posX = %posX + %dx;
            %n = %n - 1;
        }
        %m = %m + 1;
    }
    %num = %container.getCount();
    %n = 0;
    while (%n < %num)
    {
        %ctrl = %container.getObject(%n);
        %this.rebuildContainer_LilThumb(%ctrl);
        if (%this.mClickableThumbs)
        {
            %this.addWidget_LilThumbButton(%ctrl);
        }
        %ctrl.mInPosition = %n;
        %n = %n + 1;
    }
    %container.mOldestThumbnail = %container.getCount() - 1;
    return ;
}
function thumbnailsGoRound::getThumbnailIndexInSlot(%this, %slotIndex)
{
    %num = %this.mLilThumbsContainer.getCount();
    %ndx = (%this.mLilThumbsContainer.mOldestThumbnail + 1) % %num;
    %n = 0;
    while (%n < %slotIndex)
    {
        %ndx = (%ndx + 1) % %num;
        %n = %n + 1;
    }
    return %ndx;
}
function thumbnailsGoRound::getThumbnailInSlot(%this, %slotIndex)
{
    %obj = %this.mLilThumbsContainer.getObject(%this.getThumbnailIndexInSlot(%slotIndex));
    return %obj;
}
function thumbnailsGoRound::onRebuilt(%this)
{
    %n = 0;
    while (%n < (%this.mLilThumbsNumAcross * 2))
    {
        %this.tick();
        %n = %n + 1;
    }
}

function thumbnailsGoRound::tick(%this)
{
    cancel(%this.tickTimerID);
    %this.tickTimerID = "";
    %this.giddap();
    %this.tickTimerID = %this.schedule(%this.mTickPeriodMS, "tick");
    return ;
}
function thumbnailsGoRound::giddap(%this, %bringInNewContent)
{
    if (!%this.isVisibleRecursive())
    {
        return ;
    }
    if (!isDefined("%bringInNewContent"))
    {
        %bringInNewContent = 1;
    }
    %firstBasePosition = %this.mLilThumbsContainer.getObject(0).basePosition;
    %firstInPosition = %this.mLilThumbsContainer.getObject(0).mInPosition;
    %num = %this.mLilThumbsContainer.getCount();
    %n = 0;
    while (%n < (%num - 1))
    {
        %ctrlA = %this.mLilThumbsContainer.getObject(%n);
        %ctrlB = %this.mLilThumbsContainer.getObject(%n + 1);
        %ctrlA.mInPosition = %ctrlB.mInPosition;
        %ctrlA.basePosition = %ctrlB.basePosition;
        %ctrlA.setTrgPosition(%ctrlA.basePosition);
        %n = %n + 1;
    }
    %ctrlA = %this.mLilThumbsContainer.getObject(%n);
    %ctrlA.mInPosition = %firstInPosition;
    %ctrlA.basePosition = %firstBasePosition;
    %ctrlA.setTrgPosition(%ctrlA.basePosition);
    %this.mLilThumbsContainer.mOldestThumbnail = %this.mLilThumbsContainer.mOldestThumbnail - 1;
    if (%this.mLilThumbsContainer.mOldestThumbnail < 0)
    {
        %this.mLilThumbsContainer.mOldestThumbnail = %this.mLilThumbsContainer.getCount() - 1;
    }
    if (%bringInNewContent)
    {
        %this.newContentLilThumb(%this.getThumbnailInSlot(0));
    }
    %this.newContentBigThumb();
    return ;
}
function thumbnailsGoRound::getCurrentZoomedLilThumb(%this)
{
    return %this.getThumbnailInSlot(%this.mLilThumbsNumAcross);
}
function thumbnailsGoRound::onLilThumbClick(%this, %container)
{
    %d = %this.mLilThumbsNumAcross - %container.mInPosition;
    if (%d == 0)
    {
        return ;
    }
    if (%d < 0)
    {
        %d = (%this.mLilThumbsNumAcross * 2) + %d;
    }
    %n = 0;
    while (%n < %d)
    {
        %this.giddap(0);
        %n = %n + 1;
    }
    %this.pause();
    return ;
}
function thumbnailsGoRound::pause(%this, %pausePeriodMS)
{
    if (!isDefined("%pausePeriodMS"))
    {
        %pausePeriodMS = %this.mPausePeriodMS;
    }
    cancel(%this.tickTimerID);
    %this.tickTimerID = "";
    if (%pausePeriodMS > 0)
    {
        %this.tickTimerID = %this.schedule(%pausePeriodMS, "tick");
    }
    else
    {
        if (%pausePeriodMS == 0)
        {
            %this.tick();
        }
    }
    return ;
}
function newThumbnailsGoRound(%name)
{
    %obj = newThumbnailsGoRound_base(%name);
    %obj.bindClassName("thumbnailsGoRound");
    return %obj;
}
function thumbnailsGoRound::rebuildContainer_LilThumb(%this, %container)
{
    %container.deleteMembers();
    %ctrl = new GuiBitmapCtrl()
    {
        profile = ETSNonModalProfile;
        extent = %container.getExtent();
        bitmap = "platform/client/ui/white_16x16";
    };
    %container.add(%ctrl);
    %container.mBitmapCtrl = %ctrl;
    %ctrl = new GuiMLTextCtrl()
    {
        profile = ETSNonModalProfile;
        extent = %container.getExtent();
        value = "<font:arial:16><color:white>lilThumb";
    };
    %container.add(%ctrl);
    %container.mTextCtrl = %ctrl;
    return ;
}
function thumbnailsGoRound::addWidget_LilThumbButton(%this, %container)
{
    %ctrl = new GuiBitmapButtonCtrl()
    {
        extent = %container.getExtent();
        command = %this @ ".onLilThumbClick(" @ %container @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_50x50";
    };
    %container.add(%ctrl);
    return ;
}
function thumbnailsGoRound::rebuildContainer_BigThumb(%this, %container)
{
    %container.deleteMembers();
    %ctrl = new GuiBitmapCtrl()
    {
        profile = ETSNonModalProfile;
        extent = %container.getExtent();
        bitmap = "platform/client/ui/white_16x16";
    };
    %container.add(%ctrl);
    %container.mBitmapCtrl = %ctrl;
    %ctrl = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        position = "0 0";
        extent = %container.getExtent();
        value = "<font:arial:20><color:white>bigThumb";
    };
    %container.add(%ctrl);
    %container.mTextCtrl = %ctrl;
    return ;
}
function thumbnailsGoRound::rebuildContainer_Deets(%this, %container)
{
    %container.deleteMembers();
    %ctrl = new GuiMLTextCtrl()
    {
        position = "0 0";
        extent = %container.getExtent();
        value = "<font:arial:20><color:white>Deets";
    };
    %container.add(%ctrl);
    %container.mTextCtrl = %ctrl;
    return ;
}
function thumbnailsGoRound::newContentLilThumb(%this, %container)
{
    %r = getRandom(128, 255);
    %g = getRandom(128, 255);
    %b = getRandom(128, 255);
    %color1 = formatInt("%0.2X", %r) @ formatInt("%0.2X", %g) @ formatInt("%0.2X", %b);
    %color2 = formatInt("%0.2X", %r - 128) @ formatInt("%0.2X", %g - 128) @ formatInt("%0.2X", %b - 128);
    %container.mContent1 = "<color:" @ %color1 @ ">" @ %color2;
    %container.mContent2 = %r - 128 SPC %g - 128 SPC %b - 128 SPC 255;
    %container.mContent3 = %color2;
    %container.mTextCtrl.setText("<font:arial:10>" SPC %container.mContent1);
    %container.mBitmapCtrl.modulationColor = %container.mContent2;
    return ;
}
function thumbnailsGoRound::newContentBigThumb(%this)
{
    %container = %this.mBigThumbContainer;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %container.mContent1 = %lilThumbContainer.mContent1;
    %container.mContent2 = %lilThumbContainer.mContent2;
    %container.mTextCtrl.setText("<font:arial:16>" SPC %container.mContent1);
    %container.mBitmapCtrl.modulationColor = %container.mContent2;
    %this.newContentDeets();
    return ;
}
function thumbnailsGoRound::newContentDeets(%this)
{
    %container = %this.mDeetsContainer;
    %lilThumbContainer = %this.getCurrentZoomedLilThumb();
    %url = "http://www.w3schools.com/tags/ref_color_tryit.asp?hex=" @ %lilThumbContainer.mContent3;
    %container.mTextCtrl.setText("<color:ffffffff>this is the color <a:" @ %url @ ">" @ %lilThumbContainer.mContent3 @ "</a>");
    return ;
}
