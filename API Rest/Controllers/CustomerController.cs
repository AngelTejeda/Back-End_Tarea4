using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.BackEnd;
using Tarea_4.DataAccess;
using Tarea_4.ExceptionHandling;
using Tarea_4.Models;

namespace API_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // GET api/<CustomerController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            // Get Customer from Database
            Customer dbCustomer = new CustomerSC().GetCustomerById(id);

            if (dbCustomer == null)
                return NotFound();

            CustomerContactInfoDTO customer = new(dbCustomer);

            return Ok(customer);
        }

        // GET: api/<CustomerController>/page/{page}
        [HttpGet]
        [Route("pages/{requestedPage}")]
        public IActionResult GetPage(int requestedPage)
        {
            const int elementsPerPage = 10;

            if (requestedPage < 1)
                return BadRequest($"{nameof(requestedPage)} must be at least 1.");

            // Calculate Pages
            int lastPage = new CustomerSC().CalculateLastPage(elementsPerPage);
            Pagination<CustomerContactInfoDTO> response = new(requestedPage, lastPage);

            if (lastPage == 0)
                return Ok(response);

            // Get Selected Page
            IQueryable<Customer> dbCustomers = new CustomerSC().GetPage(elementsPerPage, (int)response.CurrentPage);
            List<CustomerContactInfoDTO> customers = BaseSC.MaterializeIQueryable<Customer, CustomerContactInfoDTO>(dbCustomers);

            // Attach elements of the page to the response
            response.ResponseList = customers;

            return Ok(response);
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<Customer> dbCustomers = new CustomerSC().GetAllCustomers();

            List<CustomerContactInfoDTO> customers = BaseSC.MaterializeIQueryable<Customer, CustomerContactInfoDTO>(dbCustomers);

            return Ok(customers);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerContactInfoPostDTO newCustomer)
        {
            string id;

            try
            {
                id = new CustomerSC().AddNewCustomer(newCustomer);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex.InnerException as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return Created("GET " + Request.Path.Value + "/" + id, id);
        }

        // PUT api/<CustomerController>/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] CustomerContactInfoPutDTO modifiedCustomer)
        {
            Customer dataBaseCustomer = new CustomerSC().GetCustomerById(id);

            if (dataBaseCustomer == null)
                return NotFound();

            try
            {
                //TODO: Check if it is possible to pass the dataBaseCustomer insted of the id.
                new CustomerSC().UpdateCustomer(id, modifiedCustomer);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex.InnerException as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return NoContent();
        }

        // DELETE api/<CustomerController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Customer dataBaseCustomer = new CustomerSC().GetCustomerById(id);

            if (dataBaseCustomer == null)
                return NotFound();

            try
            {
                new CustomerSC().DeleteCustomer(dataBaseCustomer);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex.InnerException as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return NoContent();
        }
    }
}
