function testNamespace() {
    new SimObject(mySimObject);
    new SimObject(mySimObject2);
    new GuiControl(myGuiControl);
    myGuiControl.allowInstanceMethods(mySimObject2);
    myGuiControl.func1(mySimObject);
    myGuiControl.func1(mySimObject2);
    myGuiControl.func1(myGuiControl);
};
function mySimObject::func1(%this) {
    echo("mySimObject func1()");
};
function mySimObject2::func1(%this) {
    echo("mySimObject2 func1()");
};
function myGuiControl::func1(%this) {
    echo("myGuiControl func1()");
};
DeclareTestSuite("TestSuite_NAMESPACE");
function TestSuite_NAMESPACE::setup(%this) {
    %this.addTestCase("TEST_NAMESPACE_PackageTest");
};
function TEST_NAMESPACE_PackageTest::runTest(%this) {
    %this.assertSameString("yes in a package", myGuiControl.packagePreActivatedFunc(%this), "we should be in a package if it was activated in the same script it was declared in");
    %this.assertSameString("not in a package", myGuiControl.packageFunc(%this), "we should be not in a package when we have not activated it yet");
    activatePackage(TEST_NAMESPACE_Package);
    %this.assertSameString("yes in a package", myGuiControl.packageFunc(%this), "we should be in a package when we activat it");
    deactivatePackage(TEST_NAMESPACE_Package);
    %this.assertSameString("not in a package", myGuiControl.packageFunc(%this), "we should not be in a package when we deactivate it");
};
function TEST_NAMESPACE_PackageTest::packageFunc(%this) {
    return "not in a package";
};
package TEST_NAMESPACE_Package {
    function TEST_NAMESPACE_PackageTest::packageFunc(%this) {
        return "yes in a package";
    };
};

function TEST_NAMESPACE_PackageTest::packagePreActivatedFunc(%this) {
    return "not in a package";
};
package TEST_NAMESPACE_Package_PreActivated {
    function TEST_NAMESPACE_PackageTest::packagePreActivatedFunc(%this) {
        return "yes in a package";
    };
    activatePackage(TEST_NAMESPACE_Package_PreActivated);
};

