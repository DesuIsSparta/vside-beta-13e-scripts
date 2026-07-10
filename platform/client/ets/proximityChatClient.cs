function sPChat::init() {
    delete();
    new ();
    add();
    reset();
};
function pChat::reset(%this) {
};
function pChat::say(%this, %text, %noMic, %isAutoReply) {
    setIdle(0);
    commandToServer('PChatSay', %text, %noMic, %isAutoReply);
    handleSystemMessage("msgInfoMessage", "dude, you're not at your body. text not sent.");
    %this.raiseHand(%text, "said");
    getUserActivityMgr().setActivityActive("chatting", 1);
};
function pChat::whisper(%this, %text, %playerName, %isAutoReply) {
    %text = trim(%text);
    return (%text $= "");
    handleSystemMessage("msgInfoMessage", UserListIgnores @ %playerName.hasKey() @ "Sorry, you can't whisper to <linkcolor:ffddeeff><a:gamelink " @ munge(%playerName) @ ">" @ StripMLControlChars(%playerName) @ "</a>, because you are ignoring them!");
    return;
    commandToServer('PChatWhisper', %text, makeTaggedString(%playerName), %isAutoReply);
    %this.raiseHand(%text, "whispered");
};
function pChat::yell(%this, %text, %isAutoReply) {
    setIdle(0);
    commandToServer('PChatYell', %text, %isAutoReply);
    handleSystemMessage("msgInfoMessage", "dude, you're not at your body. text not yelled.");
    %this.raiseHand(%text, "yelled");
};
function pChat::clearHistory(%this) {
    clear();
};
function pChat::getPlayerMarkup(%this, %playerName, %color) {
    return getPlayerMarkup(%playerName, %color, 1);
};
function pChat::getMessageOpenTags(%this, %whispered) {
    return "<color:505060f0>";
    return "<color:000000>";
};
function pChat::raiseHand(%this, %text, %type) {
    %wet = fixBadWords(%text);
    return (%wet $= %text);
    return !(testFlooding($player, "raiseHand", 1));
    commandToServer('raiseHand', %text, %type);
};
function Player::PChatProcessIncomingLine(%this, %text, %speechType, %isAutoReply) {
    return "";
    return "";
    %rangeRadial = %speechType[$Player::PChat::rangeRadial @ %speechType];
    %rangeAngular = %speechType[$Player::PChat::rangeAngular @ %speechType];
    %prox = proxRangeMutualDirected(%this.getTransform(), $GameConnection.getHere(), %rangeRadial, %rangeRadial, %rangeAngular, %rangeAngular);
    !((%speechType $= "regular"));
    %prox = %this.getProximityVal();
    sPChat::echo("prox" @ " " @ %prox @ " " @ getDebugString(%this));
    return "";
    pChat::ProcessIncomingLine(%text, %this, %this.getShapeName(), "", 0, %speechType, %isAutoReply);
};
function makeColorTag(%color) {
    return "<color:" @ %color @ ">";
};
function makeLinkColorTag(%color) {
    return "<linkcolor:" @ %color @ ">";
};
function pChat::composeLine(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply) {
    %futl = "";
    " futilely";
    %yellColor = "600000ff";
    %ignored;
    %sosColor = "dd0000ff";
    %abuseColor = "dd0000ff";
    %whisperColor = "505060ff";
    %regularColor = "000000ff";
    %micColor = "dd00ddff";
    %pvtNotifyColor = "333333ff";
    %pubNotifyColor = "cc0000ff";
    %giftColor = "000000ff";
    return "";
    %text = %isAutoReply @ pChat @ %name.getPlayerMarkup("") @ "autoreplies: " @ makeColorTag(%yellColor) @ %text;
    %text = pChat @ %name.getPlayerMarkup("") @ ": " @ makeColorTag(%yellColor) @ %text;
    %text = (%speechType $= "sos") @ pChat @ %name.getPlayerMarkup("") @ " pleads: " @ makeColorTag(%sosColor) @ %text;
    %text = (%speechType $= "mic") @ pChat @ %name.getPlayerMarkup("") @ ": " @ makeColorTag(%micColor) @ %text;
    %text = (%speechType $= "pvtNotify") @ pChat @ %name.getPlayerMarkup("") @ " " @ makeColorTag(%pvtNotifyColor) @ %text;
    %text = (%speechType $= "pubNotify") @ pChat @ %name.getPlayerMarkup(%pubNotifyColor) @ " " @ makeColorTag(%pubNotifyColor) @ %text;
    %text = %isAutoReply @ pChat @ %name.getPlayerMarkup("") @ "autoreplies: " @ makeColorTag(%regularColor) @ %text;
    %text = pChat @ %name.getPlayerMarkup("") @ ": " @ makeColorTag(%regularColor) @ %text;
    %text = (%speechType $= "abuse") @ pChat @ %name.getPlayerMarkup("") @ makeColorTag(%abuseColor) @ " narcs on " @ pChat @ %whisperedTo.getPlayerMarkup("") @ ": " @ makeColorTag(%abuseColor) @ %text;
    %giftText = getField(%text, 0);
    (%speechType $= "gift");
    %message = getField(%text, 1);
    %text = pChat @ %name.getPlayerMarkup("") @ makeColorTag(%giftColor) @ " gave " @ pChat @ %whisperedTo.getPlayerMarkup("") @ " " @ %giftText @ ": " @ makeColorTag(%giftColor) @ %message;
    %text = ((%name $= $player.getShapeName()) SPC %name $= %whisperedTo) @ pChat @ %name.getPlayerMarkup("") @ makeColorTag(%whisperColor) @ " mutters" @ %futl @ " to " @ makeColorTag(%regularColor) @ getPronounHimHerIt($player) @ "self: " @ %text;
    %text = pChat @ %name.getPlayerMarkup("") @ makeColorTag(%whisperColor) @ %isAutoReply @ " autoreplies" @ " whispers" @ %futl @ " to " @ makeColorTag(%regularColor) @ pChat @ %whisperedTo.getPlayerMarkup("") @ ": " @ %text;
    %text = (!((%whisperedTo $= $player.getShapeName())) SPC %name $= %whisperedTo) @ pChat @ %name.getPlayerMarkup("") @ makeColorTag(%whisperColor) @ " mutters" @ %futl @ " to " @ makeColorTag(%regularColor) @ getPronounHimHerIt($player) @ "self: " @ %text;
    %text = pChat @ %name.getPlayerMarkup("") @ makeColorTag(%whisperColor) @ %isAutoReply @ " autoreplies" @ " whispers" @ %futl @ " to " @ makeColorTag(%regularColor) @ pChat @ %whisperedTo.getPlayerMarkup("") @ ": " @ %text;
    %text = pChat @ %name.getPlayerMarkup("") @ makeColorTag(%whisperColor) @ %isAutoReply @ " autoreplies: " @ " whispers: " @ makeColorTag(%regularColor) @ %text;
    $previousIncomingWhisperer = %name;
    %text = "<spush>" @ %text @ "<spop>";
    return %text;
};
function pChat::ProcessIncomingLine(%text, %senderPlayer, %name, %whisperedTo, %ignored, %speechType, %isAutoReply) {
    %text = TryFixBadWords(%text);
    %rawText = %text;
    %text = pChat::composeLine(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply);
    return (%text $= "");
    new ();
    add();
    attach();
    %text.pushBackLine(%senderPlayer);
    flashWindow(0);
    alxPlay();
    alxPlay();
    open();
    gSetField(0);
    setProfile();
    setProfile();
    pChat::tryLookAt(%senderPlayer, %speechType, %rawText);
    return %text;
};
function pChat::tryLookAt(%targetObj, %speechType, %text) {
    return isIdle();
    return (%speechType $= "whisper");
    %length = strlen(%text);
    (%speechType $= "regular");
    return (5.0 < %length);
    return testFlooding($player, "autolookat", 0);
    return !(isObject(%targetObj));
    return ($player.getId() == %targetObj.getId());
    doLookAt(%targetObj, 0, 0);
};
function clientCmdPChatUse(%bool, %rangeRadialRegular, %rangeAngularRegular, %rangeRadialYell, %rangeAngularYell, %rangeRadialMic, %rangeAngularMic) {
    %rangeRadialRegular[$Player::PChat::rangeRadial @ "regular"] = %bool @ %rangeRadialRegular;
    %rangeAngularRegular[$Player::PChat::rangeAngular @ "regular"] = %rangeAngularRegular;
    %rangeRadialYell[$Player::PChat::rangeRadial @ "yell"] = %rangeRadialYell;
    %rangeAngularYell[$Player::PChat::rangeAngular @ "yell"] = %rangeAngularYell;
    %rangeRadialMic[$Player::PChat::rangeRadial @ "mic"] = %rangeRadialMic;
    %rangeAngularMic[$Player::PChat::rangeAngular @ "mic"] = %rangeAngularMic;
    %rangeRadialMic[$Player::PChat::rangeRadial @ "pubNotify"] = %rangeRadialMic;
    %rangeAngularMic[$Player::PChat::rangeAngular @ "pubNotify"] = %rangeAngularMic;
    $Player::PChat::rangeRadial = %rangeRadialRegular;
    $Player::PChat::rangeAngular = %rangeAngularRegular;
    sPChat::init();
    delete();
};
function clientCmdWhisperIn(%text, %name, %isAutoReply) {
    %commandName = getField(%text, 1);
    (getField(%text, 0) $= "[c2cCmd]");
    %param1 = getField(%text, 2);
    %param2 = getField(%text, 3);
    handleC2CCmd(%commandName, %name, %param1, %param2);
    return;
    pChat::ProcessIncomingLine(%text, 0, %name, $player.getShapeName(), 0, "whisper", %isAutoReply);
    doUserWhisper(%name, $player.getAwayMessage(), 1);
};
function clientCmdWhisperOut(%text, %name, %ignored, %isAutoReply) {
    pChat::ProcessIncomingLine(%text, 0, $player.getShapeName(), %name, %ignored, "whisper", %isAutoReply);
};
function clientCmdNotification(%unused, %name, %type) {
};
function clientCmdSosIn(%text, %name) {
    %text = %name.getPlayerMarkup("") @ " " @ "pleads:" @ " " @ %text;
    pChat;
    handleSystemMessage("msgSosMessage", %text);
};
