function centerPrintAll(%message, %time, %lines) {
    if ((%lines $= "")) {
        if ((3.0 > %lines)) {
        }
    }
    if ((1.0 < %lines)) {
        %lines = 1;
    }
    %count = ClientGroup.getCount();
    %i = 0;
    if ((%count < %i)) {
        %cl = ClientGroup.getObject(%i);
        if (!(%cl.isAIControlled())) {
            commandToClient(%cl, 'centerPrint', %message, %time, %lines);
        }
        %i = (1.0 + %i);
    }
};
function bottomPrintAll(%message, %time, %lines) {
    if ((%lines $= "")) {
        if ((3.0 > %lines)) {
        }
    }
    if ((1.0 < %lines)) {
        %lines = 1;
    }
    %count = ClientGroup.getCount();
    %i = 0;
    if ((%count < %i)) {
        %cl = ClientGroup.getObject(%i);
        if (!(%cl.isAIControlled())) {
            commandToClient(%cl, 'bottomPrint', %message, %time, %lines);
        }
        %i = (1.0 + %i);
    }
};
function centerPrint(%client, %message, %time, %lines) {
    if ((%lines $= "")) {
        if ((3.0 > %lines)) {
        }
    }
    if ((1.0 < %lines)) {
        %lines = 1;
    }
    commandToClient(%client, 'CenterPrint', %message, %time, %lines);
};
function bottomPrint(%client, %message, %time, %lines) {
    if ((%lines $= "")) {
        if ((3.0 > %lines)) {
        }
    }
    if ((1.0 < %lines)) {
        %lines = 1;
    }
    commandToClient(%client, 'BottomPrint', %message, %time, %lines);
};
function clearCenterPrint(%client) {
    commandToClient(%client, 'ClearCenterPrint');
};
function clearBottomPrint(%client) {
    commandToClient(%client, 'ClearBottomPrint');
};
function clearCenterPrintAll() {
    %count = ClientGroup.getCount();
    %i = 0;
    if ((%count < %i)) {
        %cl = ClientGroup.getObject(%i);
        if (!(%cl.isAIControlled())) {
            commandToClient(%cl, 'ClearCenterPrint');
        }
        %i = (1.0 + %i);
    }
};
function clearBottomPrintAll() {
    %count = ClientGroup.getCount();
    %i = 0;
    if ((%count < %i)) {
        %cl = ClientGroup.getObject(%i);
        if (!(%cl.isAIControlled())) {
            commandToClient(%cl, 'ClearBottomPrint');
        }
        %i = (1.0 + %i);
    }
};
