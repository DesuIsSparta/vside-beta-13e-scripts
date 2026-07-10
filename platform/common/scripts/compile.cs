function compileCS() {
    %file = findFirstFile("*.cs");
    if (!(%file $= "")) {
        compile(%file);
        %file = findNextFile("*.cs");
    }
};
