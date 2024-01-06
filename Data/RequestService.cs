
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BikeManagementSystem.Data
{
    public class RequestService
    {

        private static void SaveAll(List<RequestLog> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string requestFilePath = Utils.GetRequestFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(requestFilePath, json);
        }

        public static List<RequestLog> GetAll()
        {
            string requestFilePath = Utils.GetRequestFilePath();
            if (!File.Exists(requestFilePath))
            {
                return new List<RequestLog>();
            }

            var json = File.ReadAllText(requestFilePath);

            return JsonSerializer.Deserialize<List<RequestLog>>(json);
        }

        public static List<RequestLog> Create(string itemName, int quantity, Guid approveBy, string takenBy)
        {

            List<RequestLog> items = GetAll();
            items.Add(new RequestLog
            {
                ItemName = itemName,
                Quantity = quantity,
                ApproveBy = approveBy,
                TakenBy = takenBy,
            });
            SaveAll(items);
            return items;
        }

        public static List<RequestLog> Delete(Guid id)
        {
            List<RequestLog> items = GetAll();
            RequestLog item = items.FirstOrDefault(x => x.Id == id);

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
            string itemsFilePath = Utils.GetRequestFilePath();
            if (File.Exists(itemsFilePath))
            {
                File.Delete(itemsFilePath);
            }
        }


    }
}
       
