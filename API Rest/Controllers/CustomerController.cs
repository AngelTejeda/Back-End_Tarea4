using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4;
using Tarea_4.BackEnd;
using Tarea_4.DataAccess;
using Tarea_4.Models;
using Tarea_4.ActionFilters;
using Tarea_4.ExceptionHandling;
using Microsoft.Data.SqlClient;

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

            // Get Selected Page
            IQueryable<Customer> dbCustomers = new CustomerSC().GetPage(elementsPerPage, response.CurrentPage);
            List<CustomerContactInfoDTO> customers = CustomerSC.MaterializeIQueryable<CustomerContactInfoDTO>(dbCustomers);

            // Attach elements of the page to the response
            response.ResponseList = customers;

            return Ok(response);
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<Customer> dbCustomers = new CustomerSC().GetAllCustomers();

            List<CustomerContactInfoDTO> customers = CustomerSC.MaterializeIQueryable<CustomerContactInfoDTO>(dbCustomers);

            return Ok(customers);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerContactInfoDTO newCustomer)
        {
            string id;

            try
            {
                id = new CustomerSC().AddNewCustomer(newCustomer);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return Created("GET " + Request.Path.Value + "/" + id, id);
        }

        // PUT api/<CustomerController>/{id}
        [HttpPut("{id}")]
        //[CustomerPersonalInfo_EnsureMatchingIds]
        public IActionResult Put(string id, [FromBody] CustomerContactInfoDTO modifiedCustomer)
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
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex as SqlException);

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
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return NoContent();
        }
    }
}
