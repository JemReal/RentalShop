using RentalShop.API.Data;
using RentalShop.API.Models.Domain;
using RentalShop.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RentalShop.API.Controllers
{
    [EnableCors(origins: "http://localhost:1841", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private RentalShopDbContext dbContext = new RentalShopDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dbContext.Dispose();
            }

            base.Dispose(disposing);
        }

        // RETRIEVE all Movies
        // https://localhost:portNo/api/movies
        [HttpGet]
        public IHttpActionResult Get()
        {
            // Get Data from Database - Domain Model
            // var moviesDomain = dbContext.Movies.ToList();
            var moviesDomain = dbContext.Movies.Include("Genre").AsQueryable();

            if (moviesDomain == null)
            {
                return this.NotFound();
            }

            // Map Domain Model to DTO (Data Transfer Objects)
            var moviesDto = new List<Movie>();
            foreach (var movieDomain in moviesDomain)
            {
                moviesDto.Add(new Movie()
                {
                    Id = movieDomain.Id,
                    Title = movieDomain.Title,
                    ReleaseDate = movieDomain.ReleaseDate,
                    Quantity = movieDomain.Quantity,
                    GenreId = movieDomain.GenreId,
                    Genre = movieDomain.Genre
                });
            }

            return Ok(moviesDto);
        }

        // RETRIEVE single Movie (Get Movie by Id)
        // GET: http://localhost:portNo/api/movies/{id}
        [HttpGet]
        [Route("api/movies/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            // Get Movie Domain Model from Database
            var movieDomain = dbContext.Movies.Include("Genre").FirstOrDefault(x => x.Id == id);

            if (movieDomain == null)
            {
                return NotFound();
            }

            // Map Movie Domain Model to Movie DTO
            var movieDto = new Movie
            {
                Id= movieDomain.Id,
                Title = movieDomain.Title,
                ReleaseDate = movieDomain.ReleaseDate,
                Quantity = movieDomain.Quantity,
                GenreId = movieDomain.GenreId,
                Genre = movieDomain.Genre
            };

            return Ok(movieDto);
        }

        // CREATE new movie
        // POST: https://localhost:portNo/api/movies
        [HttpPost]
        public IHttpActionResult Create([FromBody] AddMovieRequestDto addMovieRequestDto)
        {
            // Convert from DTO to Domain Model
            var movieDomainModel = new Movie
            {
                Title = addMovieRequestDto.Title,
                ReleaseDate = addMovieRequestDto.ReleaseDate,
                Quantity = addMovieRequestDto.Quantity,
                GenreId = addMovieRequestDto.GenreId
            };

            // Use Domain Model to create Customer
            dbContext.Movies.Add(movieDomainModel);
            dbContext.SaveChanges();

            // Convert Domain Model back to DTO
            var movieDto = new MovieDto
            {
                Id = movieDomainModel.Id,
                Title = movieDomainModel.Title,
                ReleaseDate = movieDomainModel.ReleaseDate,
                Quantity = movieDomainModel.Quantity,
                GenreId= movieDomainModel.GenreId
            };

            // Return post method - 201
            return Created(new Uri(Request.RequestUri + movieDto.Id.ToString()), movieDto);
        }

        // UPDATE Movie details
        // PUT: https://localhost:portNo/api/movies/{id}
        [HttpPut]
        [Route("api/movies/{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] UpdateMovieRequestDto updateMovieRequestDto)
        {
            // Check if the movie exists
            var movieDomainModel = dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movieDomainModel == null)
            {
                return NotFound();
            }

            // Convert DTO to Domain Model
            movieDomainModel.Title = updateMovieRequestDto.Title;
            movieDomainModel.ReleaseDate = updateMovieRequestDto.ReleaseDate;
            movieDomainModel.Quantity = updateMovieRequestDto.Quantity;
            movieDomainModel.GenreId = updateMovieRequestDto.GenreId;

            dbContext.SaveChanges();

            // Convert Domain Model to DTO
            var movieDto = new MovieDto
            {
                Id = movieDomainModel.Id,
                Title = movieDomainModel.Title,
                ReleaseDate = movieDomainModel.ReleaseDate,
                Quantity = movieDomainModel.Quantity,
                GenreId = movieDomainModel.GenreId
            };

            return Ok(movieDto);
        }

        // DELETE Movie
        // DELETE: https://localhost:portNo/api/movies/{id}
        [HttpDelete]
        [Route("api/movies/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            // Check if the genre exists from the Domain Model
            var moviesDomainModel = dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (moviesDomainModel == null)
            {
                return NotFound();
            }

            // Delete the genre when found
            dbContext.Movies.Remove(moviesDomainModel);
            dbContext.SaveChanges();

            // Return the deleted movie back after mapping the Domain Model to DTO
            var movieDto = new MovieDto
            {
                Id = moviesDomainModel.Id,
                Title = moviesDomainModel.Title,
                ReleaseDate = moviesDomainModel.ReleaseDate,
                Quantity = moviesDomainModel.Quantity,
                GenreId = moviesDomainModel.GenreId
            };

            return Ok(movieDto);
        }
    }
}
