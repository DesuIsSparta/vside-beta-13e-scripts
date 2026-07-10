exec("./skeletonClient.cs");
function generateCacheRemote() {
    userName = new ScriptObject(skeletonClient) @ "btuser";
    password = "eviltwin";
    joinAction = "doSomething";
    quitOnError = "true";
    %cacheGenerate = ;
    $iterationsWaited = 0;
    %cacheGenerate.init();
    %cacheGenerate.doLogin("cache_host");
};
echo("LOAD: starting via generateCacheRemote()");
generateCacheRemote();
function doSomething() {
    if (isObject()) {
        echo("CACHE: We found PChat. Quitting in 5 seconds...");
        schedule(5000, 0);
    }
    if ((200.0 == $iterationsWaited)) {
        error("CACHE->ERROR : Giving up. Waited for 10 minutes and nothing happended");
    }
    echo("CACHE: Nothing yet....");
    $iterationsWaited = (1.0 + $iterationsWaited);
    quit;
    schedule(3000, 0);
};
