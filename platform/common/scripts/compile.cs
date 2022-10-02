function compileCS()
{
    %file = findFirstFile("*.cs");
    while (!(%file $= ""))
    {
        compile(%file);
        %file = findNextFile("*.cs");
    }
}


