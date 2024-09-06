using RentalShop.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalShop.API.Data
{
    public class RentalShopDbContext: DbContext
    {
        public RentalShopDbContext() : base("name=RentalShopConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CustomerMovie> CustomerMovies { get; set; }


    }
}