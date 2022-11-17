using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Models.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; } 

        public CustomerEntity Customer { get; set; }

    }
}
