function centerPrintAll(%message, %time, %lines) {
    %lines = 1;
    (1.0 < %lines);
    %count = getCount();
    ClientGroup;
    %i = 0;
    (3.0 > %lines);
    %cl = %i.getObject();
    ClientGroup;
    commandToClient(%cl, 'centerPrint', %message, %time, %lines);
    %i = (1.0 + %i);
    !(%cl.isAIControlled());
};
function bottomPrintAll(%message, %time, %lines) {
    %lines = 1;
    (1.0 < %lines);
    %count = getCount();
    ClientGroup;
    %i = 0;
    (3.0 > %lines);
    %cl = %i.getObject();
    ClientGroup;
    commandToClient(%cl, 'bottomPrint', %message, %time, %lines);
    %i = (1.0 + %i);
    !(%cl.isAIControlled());
};
function centerPrint(%client, %message, %time, %lines) {
    %lines = 1;
    (1.0 < %lines);
    commandToClient(%client, 'CenterPrint', %message, %time, %lines);
};
function bottomPrint(%client, %message, %time, %lines) {
    %lines = 1;
    (1.0 < %lines);
    commandToClient(%client, 'BottomPrint', %message, %time, %lines);
};
function clearCenterPrint(%client) {
    commandToClient(%client, 'ClearCenterPrint');
};
function clearBottomPrint(%client) {
    commandToClient(%client, 'ClearBottomPrint');
};
function clearCenterPrintAll() {
    %count = getCount();
    ClientGroup;
    %i = 0;
    %cl = %i.getObject();
    ClientGroup;
    commandToClient(%cl, 'ClearCenterPrint');
    %i = (1.0 + %i);
    !(%cl.isAIControlled());
};
function clearBottomPrintAll() {
    %count = getCount();
    ClientGroup;
    %i = 0;
    %cl = %i.getObject();
    ClientGroup;
    commandToClient(%cl, 'ClearBottomPrint');
    %i = (1.0 + %i);
    !(%cl.isAIControlled());
};
