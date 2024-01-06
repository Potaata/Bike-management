using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeManagementSystem.Data
{
    public class Inventory
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide the task name.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Please provide a Quantity.")]
        public int Quantity { get; set; }

        public string AddedBy { get; set; }
    }
}
