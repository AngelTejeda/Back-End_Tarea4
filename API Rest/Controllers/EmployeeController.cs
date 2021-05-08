using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4;
using Tarea_4.BackEnd;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace API_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IActionResult InternalServerError(string errorMessage = "")
        {
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }

        private readonly string InstanceName = "employee";

        // GET api/<EmployeeController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 1)
                return BadRequest($"{nameof(id)} must be at least 1.");

            Employee dbEmployee = new EmployeeSC().GetEmployeeById(id);

            if (dbEmployee == null)
                return NotFound(DbExceptionMessages.InstanceNotFound(InstanceName, id));

            EmployeePersonalInfoDTO employee = new(dbEmployee);

            return Ok(employee);
        }

        // GET: api/<EmployeeController>/page/{page}
        [HttpGet]
        [Route("page/{page}")]
        public IActionResult GetPage(int page)
        {
            const int elementsPerPage = 10;

            if (page < 1)
                return BadRequest($"{nameof(page)} must be at least 1.");

            int lastPage = new EmployeeSC().CalculateLastPage(elementsPerPage);
            Pagination<EmployeePersonalInfoDTO> response = new(page, lastPage);

            IQueryable<Employee> dbEmployees = new EmployeeSC().GetPage(elementsPerPage, response.CurrentPage);
            List<EmployeePersonalInfoDTO> employees = EmployeeSC.MaterializeIQueryable<EmployeePersonalInfoDTO>(dbEmployees);

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
                return Conflict(ex.InnerException.Message);
            }
            catch(Exception ex) when (ExceptionTypes.IsDbException(ex))
            {
                return InternalServerError();
            }

            return Ok(id);
        }

        // PUT api/<EmployeeController>/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EmployeePersonalInfoDTO modifiedEmployee)
        {
            if (modifiedEmployee.Id != id)
                return BadRequest($"{nameof(id)} and {nameof(modifiedEmployee.Id)} must have the same value.");
            
            Employee dataBaseEmployee = new EmployeeSC().GetEmployeeById(id);

            if (dataBaseEmployee == null)
                return NotFound(DbExceptionMessages.InstanceNotFound(InstanceName, id));

            try
            {
                //TODO: Check if it is possible to pass the dataBaseEmployee insted of the id.
                new EmployeeSC().UpdateEmployee(id, modifiedEmployee);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                return Conflict(ex.InnerException.Message);
            }
            catch (Exception ex) when (ExceptionTypes.IsDbException(ex))
            {
                return InternalServerError();
            }

            return NoContent();
        }

        // DELETE api/<EmployeeController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Employee dataBaseEmployee = new EmployeeSC().GetEmployeeById(id);

            if (dataBaseEmployee == null)
                return NotFound(DbExceptionMessages.InstanceNotFound(InstanceName, id));

            try
            {
                new EmployeeSC().DeleteEmployee(dataBaseEmployee);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                return Conflict(ex.InnerException.Message);
            }
            catch (Exception ex) when (ExceptionTypes.IsDbException(ex))
            {
                return InternalServerError();
            }

            return NoContent();
        }
    }
}
