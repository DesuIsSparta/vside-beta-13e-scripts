function compileCS() {
    %file = findFirstFile("*.cs");
    compile(%file);
    %file = findNextFile("*.cs");
    !((%file $= ""));
};
