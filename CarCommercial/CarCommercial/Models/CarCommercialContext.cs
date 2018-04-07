using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarCommercial.Models
{
    public class CarCommercialContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CarCommercialContext() : base("name=CarCommercialContext")
        {
        }

        public DbSet<Oglas> Oglas { get; set; }

        public DbSet<Automobil> Automobils { get; set; }

        public DbSet<VlasnikAuta> VlasnikAutas { get; set; }

        public DbSet<OglasivackaAgencija> OglasivackaAgencijas { get; set; }

        public System.Data.Entity.DbSet<CarCommercial.Models.Rezervacija> Rezervacijas { get; set; }
    }
}
