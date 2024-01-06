using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BikeManagementSystem.Data
{
    public static class InventoryService
        {
            private static void SaveAll(List<Inventory> inventory)
            {
                string appDataDirectoryPath = Utils.GetAppDirectoryPath();
                string appInventoryFilePath = Utils.GetInventoryFilePath();

                if (!Directory.Exists(appDataDirectoryPath))
                {
                    Directory.CreateDirectory(appDataDirectoryPath);
                }

                var json = JsonSerializer.Serialize(inventory);
                File.WriteAllText(appInventoryFilePath, json);
            }


            public static List<Inventory> GetAll()
            {
                string appInventoryFilePath = Utils.GetInventoryFilePath();
                if (!File.Exists(appInventoryFilePath))
                {
                    return new List<Inventory>();
                }

                var json = File.ReadAllText(appInventoryFilePath);

                return JsonSerializer.Deserialize<List<Inventory>>(json);
            }



            public static List<Inventory> Create(string addedBy, string itemName, int quantity)
            {


                List<Inventory> inventory = GetAll();
                inventory.Add(new Inventory
                {
                    AddedBy = addedBy,
                    ItemName = itemName,
                    Quantity = quantity,
                });
                SaveAll(inventory);
                return inventory;
            }



            public static List<Inventory> Update(Guid id, string itemName, int quantity)
            {
                List<Inventory> items = GetAll();
                Inventory itemUpdate = items.FirstOrDefault(x => x.Id == id);

                if (itemUpdate == null)
                {
                    throw new Exception("Item not found.");
                }

                itemUpdate.ItemName = itemName;
                itemUpdate.Quantity = quantity;
                SaveAll(items);
                return items;
            }


            public static List<Inventory> WithdrawItem(Guid id, string itemName, int quantity)
            {
                List<Inventory> items = GetAll();
                Inventory itemUpdate = items.FirstOrDefault(x => x.Id == id);

                if (itemUpdate == null)
                {
                    throw new Exception("Item not found.");
                }

                itemUpdate.ItemName = itemName;
                itemUpdate.Quantity = itemUpdate.Quantity - quantity;
                SaveAll(items);
                return items;
            }

            public static List<Inventory> CancelWithdrawItem(Guid id, string itemName, int quantity)
            {
                List<Inventory> items = GetAll();
                Inventory itemUpdate = items.FirstOrDefault(x => x.Id == id);

                if (itemUpdate == null)
                {
                    throw new Exception("Item not found.");
                }

                itemUpdate.ItemName = itemName;
                itemUpdate.Quantity = itemUpdate.Quantity + quantity;
                SaveAll(items);
                return items;
            }


            public static List<Inventory> Delete(Guid id)
            {
                List<Inventory> items = GetAll();
                Inventory itemUpdate = items.FirstOrDefault(x => x.Id == id);

                if (itemUpdate == null)
                {
                    throw new Exception("Item not found.");
                }

                items.Remove(itemUpdate);
                SaveAll(items);
                return items;
            }
    }
}
