using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.BackEnd;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace API_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IActionResult InternalServerError(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            List<CustomerContactInfoDTO> customers = new();

            using (NorthwindContext dbContext = new())
            {
                IQueryable<Customer> dbCustomers = CustomerSC.GetAllCustomers(dbContext).AsNoTracking();

                foreach (Customer dbCustomer in dbCustomers)
                {
                    customers.Add(new CustomerContactInfoDTO(dbCustomer));
                };
            }

            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                CustomerContactInfoDTO customer;

                using (NorthwindContext dbContext = new())
                {
                    Customer dbCustomer = CustomerSC.GetCustomerById(dbContext, id);

                    customer = new(dbCustomer);
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerContactInfoDTO newCustomer)
        {
            string id;

            try
            {
                using (NorthwindContext dbContext = new())
                {
                    id = CustomerSC.AddNewCustomer(dbContext, newCustomer);
                }

                return Ok("Registered Id: " + id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CustomerContactInfoDTO modifiedCustomer)
        {
            try
            {
                using (NorthwindContext dbContext = new())
                {
                    CustomerSC.UpdateCustomer(dbContext, id, modifiedCustomer);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                using (NorthwindContext dbContext = new())
                {
                    CustomerSC.DeleteCustomer(dbContext, id);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
