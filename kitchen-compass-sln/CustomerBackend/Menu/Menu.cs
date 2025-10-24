using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerBackend
{
    public class Menu
    {
        // add id for menu objects

        public List<MenuItem> Items = new List<MenuItem>();

        public string MenuType { get; set; }
        public string Id { get; set; }
        public Menu(string menuType, string id)
        {
            MenuType = menuType;
            Id = id;
        }
        private void AddItem(MenuItem newItem)
        {
            Items.Add(newItem);
        }

        public void DisplayMenu()
        {
            if (Items.Count != 0)
            {
                foreach (MenuItem item in Items)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("no menu items");
            }
        }

        public Menu CreateMenu(string menuType, string id)
        {
            return new Menu(menuType, id);
        }

        public void EditMenuItemPrice(Guid id, float newPrice)
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
}
