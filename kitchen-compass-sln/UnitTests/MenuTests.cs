using CustomerBackend;

namespace UnitTests;

public class MenuTests
{
    string itemName;
    float itemPrice;
    string menuType;
    string menuId;
    MenuItem testItem;
    Menu? testMenu;

    [SetUp]
    public void Setup()
    {
        itemName = "item1";
        itemPrice = 2;
        menuType = "menutest";
        menuId = "1";

        testItem = new MenuItem(itemName, itemPrice);
        testMenu = new Menu(menuType, menuId);
    }

    [Test]
    public void AddItemToMenuTest()
    {
        MenuItem testItem = new MenuItem("TestItem", 0);
        
        Menu testMenu = new Menu(menuType, menuId);
        testMenu.AddItemToMenu(testItem, testMenu);

        Assert.That(testMenu.Items.Count() == 1);
    }

    [Test]
    public void CreateMenuTest()
    {
        string menuType = "menutest";
        string menuId = "1";

        Menu testNewMenu = new Menu(menuType, menuId);

        Assert.That(testNewMenu, Is.Not.Null);
    }

    [Test]
    public void EditMenuItemPriceTest()
    {
        testMenu.AddItemToMenu(testItem, testMenu);
        testMenu.EditMenuItemPrice(testItem.Id, 3);

        Assert.That(testMenu.Items[0].Price, Is.EqualTo(3));
    }

    [Test]
    public void EditMenuItemNameTest()
    {
        testMenu.AddItemToMenu(testItem, testMenu);
        testMenu.EditMenuItemName(testItem.Id, "item2");

        Assert.That(testMenu.Items[0].Name, Is.EqualTo("item2"));
    }

    [Test]
    public void EditMenuItemDescriptionTest()
    {
        testMenu.AddItemToMenu(testItem, testMenu);
        testMenu.EditMenuItemDescription(testItem.Id, "this is item 2");

        Assert.That(testMenu.Items[0].Description, Is.EqualTo("this is item 2"));
    }

    [Test]
    public void EditMenuItemIngredientsTest()
    {
        testMenu.AddItemToMenu(testItem, testMenu);
        testMenu.EditMenuItemIngredients(testItem.Id, "cheese");

        Assert.That(testMenu.Items[0].Ingredients, Is.EqualTo("cheese"));
    }

    [Test]
    public void EditMenuItemCalorieCountTest()
    {
        testMenu.AddItemToMenu(testItem, testMenu);
        testMenu.EditMenuItemCalorieCount(testItem.Id, 30);

        Assert.That(testMenu.Items[0].CalorieCount, Is.EqualTo(30));
    }

    [Test]
    public void RemoveMenuItemTest()
    {
        testMenu.AddItemToMenu(testItem, testMenu);
        testMenu.RemoveMenuItem(testItem.Id);

        Assert.That(testMenu.Items, Is.Empty);
    }

    [Test]
    public void CopyMenuTest()
    {
        Menu NewMenu = new Menu("menu2", "id2");
        testMenu.AddItemToMenu(testItem, testMenu);

        NewMenu.CopyMenu(testMenu, NewMenu);

        Assert.That(NewMenu, Is.EqualTo(testMenu));
    }
}
