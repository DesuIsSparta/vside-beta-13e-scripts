function giftingItems_getRegistryClient()
{
    return safeEnsureScriptObject("StringMap", "gPendingItemTransactionsClient");
}
function giftingItems_getRegistryServer()
{
    return safeEnsureScriptObject("StringMap", "gPendingItemTransactionsServer");
}
function giftingItems_registerPendingTransactionGiver(%transactionID, %otherPlayerName, %skus, %dlg, %making)
{
    return giftingItems_registerPendingTransaction(giftingItems_getRegistryClient(), %transactionID, $Player::Name, %otherPlayerName, %skus, %dlg, 0, %making);
}
function giftingItems_registerPendingTransactionRecipient(%transactionID, %otherPlayerName, %skus, %autoAccepted, %making)
{
    return giftingItems_registerPendingTransaction(giftingItems_getRegistryClient(), %transactionID, %otherPlayerName, $Player::Name, %skus, 0, %autoAccepted, %making);
}
function giftingItems_registerPendingTransactionServer(%transactionID, %sourcePlayerName, %targetPlayerName, %skus, %making)
{
    return giftingItems_registerPendingTransaction(giftingItems_getRegistryServer(), %transactionID, %sourcePlayerName, %targetPlayerName, %skus, "", 0, %making);
}
function giftingItems_registerPendingTransaction(%registry, %transactionID, %sourcePlayerName, %targetPlayerName, %skus, %dlg, %autoAccepted, %making)
{
    if (!(%registry.get(%transactionID) $= ""))
    {
        error(getScopeName() SPC "- transaction already exists!" SPC %transactionID SPC getTrace());
        return ;
    }
    %pendingTransactionRecord = safeNewScriptObject("ScriptObject", "", 0);
    %pendingTransactionRecord.transactionID = %transactionID;
    %pendingTransactionRecord.skus = %skus;
    %pendingTransactionRecord.sourcePlayerName = %sourcePlayerName;
    %pendingTransactionRecord.targetPlayerName = %targetPlayerName;
    %pendingTransactionRecord.dlg = %dlg;
    %pendingTransactionRecord.autoAccepted = %autoAccepted;
    %pendingTransactionRecord.making = %making;
    %registry.put(%transactionID, %pendingTransactionRecord);
    %line = getScopeName() SPC "- Initiated:" SPC %transactionID SPC %skus SPC %sourcePlayerName SPC "->" SPC %targetPlayerName;
    if ($AmServer)
    {
        appendLogLine("gifting", %line);
    }
    echo(%line);
    return ;
}
function giftingItems_getPendingTransactionClient(%transactionID)
{
    return giftingItems_getPendingTransaction(giftingItems_getRegistryClient(), %transactionID);
}
function giftingItems_getPendingTransactionServer(%transactionID)
{
    return giftingItems_getPendingTransaction(giftingItems_getRegistryServer(), %transactionID);
}
function giftingItems_getPendingTransaction(%registry, %transactionID)
{
    %pendingTransactionRecord = %registry.get(%transactionID);
    if (%pendingTransactionRecord $= "")
    {
        error(getScopeName() SPC "- no such transaction:" SPC %transactionID SPC getTrace());
        return "";
    }
    return %pendingTransactionRecord;
}
function giftingItems_deletePendingTransactionClient(%transactionID)
{
    return giftingItems_deletePendingTransaction(giftingItems_getRegistryClient(), %transactionID);
}
function giftingItems_deletePendingTransactionServer(%transactionID)
{
    return giftingItems_deletePendingTransaction(giftingItems_getRegistryServer(), %transactionID);
}
function giftingItems_deletePendingTransaction(%registry, %transactionID)
{
    %pendingTransactionRecord = %registry.get(%transactionID);
    if (%pendingTransactionRecord $= "")
    {
        error(getScopeName() SPC "- no such transaction:" SPC %transactionID SPC getTrace());
        return "";
    }
    %pendingTransactionRecord.delete();
    %registry.remove(%transactionID);
    return ;
}
function giftingItems_dumpPendingTransactionsClient()
{
    return giftingItems_dumpPendingTransaction(giftingItems_getRegistryClient());
}
function giftingItems_dumpPendingTransactionsServer(%transactionID)
{
    return giftingItems_dumpPendingTransactions(giftingItems_getRegistryServer());
}
function giftingItems_dumpPendingTransaction(%registry)
{
    %registry.dumpValues();
    return ;
}
