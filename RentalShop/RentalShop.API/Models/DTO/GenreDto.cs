using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalShop.API.Models.DTO
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}