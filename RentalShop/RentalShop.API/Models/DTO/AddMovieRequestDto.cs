using RentalShop.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalShop.API.Models.DTO
{
    public class AddMovieRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Quantity { get; set; }
        public int? GenreId { get; set; }
    }
}