namespace CustomerBackend.Models;

public class Menu
{
    // add id for menu objects

    public List<MenuItem> Items = new List<MenuItem>();

    public string MenuType { get; set; }
    public string Id { get; set; }
    public Menu(string menutype, string id)
    {
        MenuType = menutype;
        Id = id;
    }
    public Menu() { }

    public void AddItem(MenuItem newitem)
    {
        Items.Add(newitem);
    }
        

    public Menu CreateMenu(string menuType, string id)
    {
        return new Menu(menuType, id);
    }

    public void EditMenuItemPrice(Guid id, decimal newPrice)
    {
        MenuItem itemToEdit = Items.First(item => item.Id == id);
        itemToEdit.Price = newPrice;
    }

    public void EditMenuItemName(Guid id, string newName)
    {
        MenuItem itemToEdit = Items.First(item => item.Id == id);
        itemToEdit.Name = newName;
    }

    public void EditMenuItemDescription(Guid id, string newDescription)
    {
        MenuItem itemToEdit = Items.First(item => item.Id == id);
        itemToEdit.Description = newDescription;
    }

    public void EditMenuItemIngredients(Guid id, string newIngredients)
    {
        MenuItem itemToEdit = Items.First(item => item.Id == id);
        itemToEdit.Ingredients = newIngredients;
    }

    public void EditMenuItemCalorieCount(Guid id, int newCalorieCount)
    {
        MenuItem itemToEdit = Items.First(item => item.Id == id);
        itemToEdit.CalorieCount = newCalorieCount;
    }

    public void AddItemToMenu(MenuItem newItem, Menu menu)
    {
        foreach (MenuItem existingitem in menu.Items)
        {
            if (newItem.Id == existingitem.Id)
            {
                break;
            }
            menu.AddItem(newItem);
        }
    }

    public void RemoveMenuItem(Guid id)
    {
        MenuItem itemToRemove = Items.First(item => item.Id == id);
        Items.Remove(itemToRemove);
    }

    public Menu CopyMenu(Menu oldMenu, Menu newMenu)
    {
        if (newMenu == null)
        {
            newMenu = oldMenu;
        }

        return newMenu;
    }
}
