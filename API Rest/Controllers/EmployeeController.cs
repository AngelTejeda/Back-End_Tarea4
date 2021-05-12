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
    public class EmployeeController : ControllerBase
    {
        // GET api/<EmployeeController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 1)
                return BadRequest($"{nameof(id)} must be at least 1.");

            // Get Employee from Database
            Employee dbEmployee = new EmployeeSC().GetEmployeeById(id);

            if (dbEmployee == null)
                return NotFound();

            EmployeePersonalInfoDTO employee = new(dbEmployee);

            return Ok(employee);
        }

        // GET: api/<EmployeeController>/page/{page}
        [HttpGet]
        [Route("pages/{requestedPage}")]
        public IActionResult GetPage(int requestedPage)
        {
            const int elementsPerPage = 10;

            if (requestedPage < 1)
                return BadRequest($"{nameof(requestedPage)} must be at least 1.");

            // Calculate Pages
            int lastPage = new EmployeeSC().CalculateLastPage(elementsPerPage);
            Pagination<EmployeePersonalInfoDTO> response = new(requestedPage, lastPage);

            // Get Selected Page
            IQueryable<Employee> dbEmployees = new EmployeeSC().GetPage(elementsPerPage, response.CurrentPage);
            List<EmployeePersonalInfoDTO> employees = EmployeeSC.MaterializeIQueryable<EmployeePersonalInfoDTO>(dbEmployees);

            // Attach elements of the page to the response
            response.ResponseList = employees;

            return Ok(response);
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<Employee> dbEmployees = new EmployeeSC().GetAllEmployees();

            List<EmployeePersonalInfoDTO> employees = EmployeeSC.MaterializeIQueryable<EmployeePersonalInfoDTO>(dbEmployees);

            return Ok(employees);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeePersonalInfoDTO newEmployee)
        {
            int id;
            
            try
            {
                id = new EmployeeSC().AddNewEmployee(newEmployee);
            }
            catch(Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return Created("GET " + Request.Path.Value + "/" + id, id);
        }

        // PUT api/<EmployeeController>/{id}
        [HttpPut("{id}")]
        [EmployeePersonalInfo_EnsureMatchingIds]
        public IActionResult Put(int id, [FromBody] EmployeePersonalInfoDTO modifiedEmployee)
        {   
            Employee dataBaseEmployee = new EmployeeSC().GetEmployeeById(id);

            if (dataBaseEmployee == null)
                return NotFound();

            try
            {
                //TODO: Check if it is possible to pass the dataBaseEmployee insted of the id.
                new EmployeeSC().UpdateEmployee(id, modifiedEmployee);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex as SqlException);

                if(message != null)
                    return Conflict(message);

                throw;
            }

            return NoContent();
        }

        // DELETE api/<EmployeeController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Employee dataBaseEmployee = new EmployeeSC().GetEmployeeById(id);

            if (dataBaseEmployee == null)
                return NotFound();

            try
            {
                new EmployeeSC().DeleteEmployee(dataBaseEmployee);
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
