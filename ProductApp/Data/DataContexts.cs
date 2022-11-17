using Microsoft.EntityFrameworkCore;
using ProductApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Data
{
    public class DataContexts : DbContext
    {
        public DataContexts(DbContextOptions<DataContexts> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }  
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public  DbSet<OrderRowEntity> OrderRows { get; set; }

    }
}
