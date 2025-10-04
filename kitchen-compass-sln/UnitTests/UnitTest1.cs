using CustomerBackend;
using CustomerBackend.Menu;

namespace UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void TestMenu()
    {
        string menuType = "menutest";
        string menuId = "1";

        MenuItem testItem = new MenuItem();
        
        Menu testMenu = new Menu(menuType, menuId);
        testMenu.AddItem(testItem);

        Assert.That(testMenu.Items.Count() == 1);
    }
}
