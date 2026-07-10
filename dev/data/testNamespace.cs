function testNamespace() {
    new SimObject(mySimObject);
    new SimObject(mySimObject2);
    new GuiControl(myGuiControl);
    mySimObject2.allowInstanceMethods();
    mySimObject.func1();
    mySimObject2.func1();
    myGuiControl.func1();
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
    "TEST_NAMESPACE_PackageTest".addTestCase(%this);
};
function TEST_NAMESPACE_PackageTest::runTest(%this) {
    "we should be in a package if it was activated in the same script it was declared in".assertSameString(%this, "yes in a package", %this.packagePreActivatedFunc());
    "we should be not in a package when we have not activated it yet".assertSameString(%this, "not in a package", %this.packageFunc());
    activatePackage(TEST_NAMESPACE_Package);
    "we should be in a package when we activat it".assertSameString(%this, "yes in a package", %this.packageFunc());
    deactivatePackage(TEST_NAMESPACE_Package);
    "we should not be in a package when we deactivate it".assertSameString(%this, "not in a package", %this.packageFunc());
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

