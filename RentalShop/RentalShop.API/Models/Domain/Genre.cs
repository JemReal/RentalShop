using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalShop.API.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}