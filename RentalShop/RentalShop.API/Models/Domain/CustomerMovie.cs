using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalShop.API.Models.Domain
{
    public class CustomerMovie
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

        // Navigation Properties
        public Movie Movies { get; set; }
        public Customer Customers { get; set; }
    }
}