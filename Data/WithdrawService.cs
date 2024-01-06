using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BikeManagementSystem.Data
{
    public static class WithdrawlService
    {
        private static void SaveAll(List<WithdrawItems> withdrawitems)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string withdrawlFilePath = Utils.GetWithdrawlFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(withdrawitems);
            File.WriteAllText(withdrawlFilePath, json);
        }



        public static List<WithdrawItems> GetAll()
        {
            string appInventoryFilePath = Utils.GetWithdrawlFilePath();
            if (!File.Exists(appInventoryFilePath))
            {
                return new List<WithdrawItems>();
            }

            var json = File.ReadAllText(appInventoryFilePath);

            return JsonSerializer.Deserialize<List<WithdrawItems>>(json);
        }



        public static List<WithdrawItems> Create(Guid userId, Guid itemId, string itemName, int quantity, string takerName)
        {


            List<WithdrawItems> withdraw = GetAll();
            withdraw.Add(new WithdrawItems
            {
                Quantity = quantity,
                TakenBy = userId,
                TakerName = takerName,
                ItemId = itemId,
                IsApproved = false,
                ItemName = itemName,


            });
            SaveAll(withdraw);
            return withdraw;
        }



        public static List<WithdrawItems> Delete(Guid id)
        {
            List<WithdrawItems> withdraw = GetAll();
            WithdrawItems itemDelete = withdraw.FirstOrDefault(x => x.Id == id);

            if (itemDelete == null)
            {
                throw new Exception("Item not found.");
            }

            withdraw.Remove(itemDelete);
            SaveAll(withdraw);
            return withdraw;
        }
    }
}
