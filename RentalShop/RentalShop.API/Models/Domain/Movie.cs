using RentalShop.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalShop.API.Models.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Quantity { get; set; }
        public int? GenreId { get; set; }

        // Navigation property
        public Genre Genre { get; set; }
    }
}