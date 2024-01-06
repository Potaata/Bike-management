using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace BikeManagementSystem.Data
{
    public class ItemService
    {

        private static void SaveAll(List<ListItem> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string itemsFilePath = Utils.GetItemsFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(itemsFilePath, json);
        }

        public static List<ListItem> GetAll()
        {
            string itemsFilePath = Utils.GetItemsFilePath();
            if (!File.Exists(itemsFilePath))
            {
                return new List<ListItem>();
            }

            var json = File.ReadAllText(itemsFilePath);

            return JsonSerializer.Deserialize<List<ListItem>>(json);
        }

        public static List<ListItem> Create(Guid userId, string itemName, int quantity)
        {

            List<ListItem> items = GetAll();
            items.Add(new ListItem
            {
                ItemName = itemName,
                Quantity = quantity,
                CreatedBy = userId
            });
            SaveAll(items);
            return items;
        }

        public static List<ListItem> Delete(Guid id)
        {
            List<ListItem> items = GetAll();
            ListItem item = items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            items.Remove(item);
            SaveAll(items);
            return items;
        }

        public static void DeleteByUserId()
        {
            string itemsFilePath = Utils.GetItemsFilePath();
            if (File.Exists(itemsFilePath))
            {
                File.Delete(itemsFilePath);
            }
        }

        public static List<ListItem> Update(Guid id, string itemName, int quantity)
        {
            List<ListItem> items = GetAll();
            ListItem itemToUpdate = items.FirstOrDefault(x => x.Id == id);

            if (itemToUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemToUpdate.ItemName = itemName;
            itemToUpdate.Quantity = quantity;
            SaveAll(items);
            return items;
        }
        public static List<ListItem> WithdrawItem(Guid id, string itemName, int quantity)
        {
            List<ListItem> items = GetAll();
            ListItem itemUpdate = items.FirstOrDefault(x => x.Id == id);

            if (itemUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemUpdate.ItemName = itemName;
            itemUpdate.Quantity = itemUpdate.Quantity - quantity;
            SaveAll(items);
            return items;
        }

        public static List<ListItem> CancelWithdrawItem(Guid id, string itemName, int quantity)
        {
            List<ListItem> items = GetAll();
            ListItem itemUpdate = items.FirstOrDefault(x => x.Id == id);

            if (itemUpdate == null)
            {
                throw new Exception("Item not found.");
            }

            itemUpdate.ItemName = itemName;
            itemUpdate.Quantity = itemUpdate.Quantity + quantity;
            SaveAll(items);
            return items;
        }


    }
}
