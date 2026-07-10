$Net::TwitterBaseInsecure = "http" @ "://twitter.com";
$Net::TwitterBaseSecure = "https" @ "://twitter.com";
function sendRequest_Twitter_verify_credentials(%user, %pass, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::TwitterBaseSecure;
    %url = %url @ "/account/verify_credentials.xml";
    %request.setURL(%url);
    %request.setUserNameAndPassword(%user @ ":" @ %pass);
    %request.setCompletedCallback(%callbackHandler);
    %request.setAutoParseResults(0);
    %request.start();
    return %request;
}
function sendRequest_Twitter_statuses_update(%user, %pass, %tweetText, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::TwitterBaseSecure;
    %url = %url @ "/statuses/update.xml";
    %request.setURL(%url);
    %request.setUserNameAndPassword(%user @ ":" @ %pass);
    %request.setCompletedCallback(%callbackHandler);
    %request.setAutoParseResults(0);
    %request.setBodyParam("status", %tweetText);
    %request.start();
    return %request;
}
$Net::BitlyAPIBaseInsecure = "http" @ "://api.bit.ly";
$Net::BitlyAPILogin = "vside";
$Net::BitlyAPIKey = "R_6758b664dc52a268180b030a0b3e88bf";
function sendRequest_Bitly_shorten(%longUrl, %callbackHandler)
{
    %request = safeNewScriptObject("URLPostObject", "", 0);
    %url = "";
    %url = %url @ $Net::BitlyAPIBaseInsecure;
    %url = %url @ "/shorten";
    %request.setURL(%url);
    %request.setUserNameAndPassword($Net::BitlyAPILogin @ ":" @ $Net::BitlyAPIKey);
    %request.setCompletedCallback(%callbackHandler);
    %request.setAutoParseResults(0);
    %request.setURLParam("longUrl", %longUrl);
    %request.setURLParam("format", "xml");
    %request.setURLParam("version", "2.0.1");
    %request.start();
    return %request;
}
