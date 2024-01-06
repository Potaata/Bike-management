using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeManagementSystem.Data
{
    public class RequestLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Please provide the task name.")]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public Guid ApproveBy { get; set; }
        public string TakenBy { get; set; }
        public DateTime TakenOutDate { get; set; } = DateTime.Now;
    }
}
