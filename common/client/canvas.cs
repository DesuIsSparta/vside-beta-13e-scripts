function initCanvas(%windowName)
{
    videoSetGammaCorrection($pref::OpenGL::gammaCorrection);
    if (!createCanvas(%windowName))
    {
        echo("canvas could not be created");
        quit();
        return 0;
    }
    setOpenGLTextureCompressionHint($Pref::OpenGL::compressionHint);
    setOpenGLAnisotropy($Pref::OpenGL::anisotropy);
    setOpenGLMipReduction($Pref::OpenGL::mipReduction);
    setOpenGLInteriorMipReduction($Pref::OpenGL::interiorMipReduction);
    setOpenGLSkyMipReduction($Pref::OpenGL::skyMipReduction);
    exec("~/ui/defaultProfiles.cs");
    exec("~/ui/MessageBoxOkDlg.gui");
    exec("~/ui/MessageBoxTextEntryDlg.gui");
    exec("~/ui/MessageBoxYesNoDlg.gui");
    exec("~/ui/MessageBoxOKCancelDlg.gui");
    exec("~/ui/MessagePopupDlg.gui");
    exec("~/ui/MessageBoxTextEntryWCancelDlg.gui");
    exec("./metrics.cs");
    exec("./messageBox.cs");
    exec("./screenshot.cs");
    exec("./cursor.cs");
    return 1;
}
function resetCanvas()
{
    if (isObject(Canvas))
    {
        Canvas.repaint();
    }
    return ;
}
