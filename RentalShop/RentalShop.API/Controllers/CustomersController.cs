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
    public class CustomersController : ApiController
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

        // RETRIEVE all Customers
        // https://localhost:portNo/api/customer
        [HttpGet]
        public IHttpActionResult Get()
        {
            // Get Data from Database - Domain Model
            var customersDomain = dbContext.Customers.ToList();

            if (customersDomain == null)
            {
                return this.NotFound();
            }

            // Map Domain Model to DTO (Data Transfer Objects)
            var customersDto = new List<Customer>();
            foreach (var customerDomain in customersDomain)
            {
                customersDto.Add(new Customer()
                {
                    Id = customerDomain.Id,
                    Email = customerDomain.Email,
                    FirstName = customerDomain.FirstName,
                    LastName = customerDomain.LastName
                });
            }

            return Ok(customersDto);
        }

        // RETRIEVE single Customer (Get Customer by Id)
        // GET: http://localhost:portNo/api/customers/{id}
        [HttpGet]
        [Route("api/customers/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            // Get Customer Domain Model from Database
            var customerDomain = dbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customerDomain == null)
            {
                return NotFound();
            }

            // Map Customer Domain Model to Customer DTO
            var customerDto = new CustomerDto
            {
                Id = customerDomain.Id,
                Email = customerDomain.Email,
                FirstName = customerDomain.FirstName,
                LastName = customerDomain.LastName
            };

            return Ok(customerDto);
        }

        // CREATE new customer
        // POST: https://localhost:portNo/api/customers
        [HttpPost]
        public IHttpActionResult Create([FromBody] AddCustomerRequestDto addCustomerRequestDto)
        {
            // Convert DTO to Domain Model
            var customerDomainModel = new Customer
            {
                Email = addCustomerRequestDto.Email,
                FirstName = addCustomerRequestDto.FirstName,
                LastName = addCustomerRequestDto.LastName
            };

            // Use Domain Model to create Customer
            dbContext.Customers.Add(customerDomainModel);
            dbContext.SaveChanges();

            // Convert Domain Model back to DTO
            var customerDto = new CustomerDto
            {
                Id = customerDomainModel.Id,
                Email = customerDomainModel.Email,
                FirstName = customerDomainModel.FirstName,
                LastName = customerDomainModel.LastName
            };

            // Return post method - 201
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id.ToString()), customerDto);
        }

        // UPDATE Customer details
        // PUT: https://localhost:portNo/api/customers/{id}
        [HttpPut]
        [Route("api/customers/{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] UpdateCustomerRequestDto updateCustomerRequestDto)
        {
            // Check if the customer exists
            var customerDomainModel = dbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customerDomainModel == null)
            {
                return NotFound();
            }

            // Convert DTO to Domain Model
            customerDomainModel.Email = updateCustomerRequestDto.Email;
            customerDomainModel.FirstName = updateCustomerRequestDto.FirstName;
            customerDomainModel.LastName = updateCustomerRequestDto.LastName;
            
            dbContext.SaveChanges();

            // Convert Domain Model to DTO
            var customerDto = new CustomerDto
            {
                Id = customerDomainModel.Id,
                Email = customerDomainModel.Email,
                FirstName = customerDomainModel.FirstName,
                LastName = customerDomainModel.LastName
            };

            return Ok(customerDto);
        }

        // DELETE Customer
        // DELETE: https://localhost:portNo/api/customers/{id}
        [HttpDelete]
        [Route("api/customers/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            // Check if the customer exists from the Domain Model
            var customersDomainModel = dbContext.Customers.FirstOrDefault(x => x.Id == id);

            if (customersDomainModel == null)
            {
                return NotFound();
            }

            // Delete the genre when found
            dbContext.Customers.Remove(customersDomainModel);
            dbContext.SaveChanges();

            // Return the deleted customer back after mapping the Domain model to DTO
            var customerDto = new CustomerDto
            {
                Id = customersDomainModel.Id,
                Email = customersDomainModel.Email,
                FirstName = customersDomainModel.FirstName,
                LastName = customersDomainModel.LastName
            };

            return Ok(customerDto);
        }
    }
}
