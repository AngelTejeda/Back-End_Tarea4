using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public class EmployeeSC : BaseSC
    {
        /// <summary>
        /// Materializes a given IQueryable of employees into a List.
        /// </summary>
        /// <typeparam name="T"> A Data Transfer Object Class of the employees.</typeparam>
        /// <param name="dataBaseEmployees">IQueryable object of the employees that will be materialized.</param>
        /// <returns>A List<typeparamref name="T"/> with the materialized employees.</returns>
        public static List<T> MaterializeIQueryable<T>(IQueryable<Employee> dataBaseEmployees) where T : EmployeeDTO, new()
        {
            List<T> employees = new();

            dataBaseEmployees = dataBaseEmployees.AsNoTracking();

            foreach (Employee dbEmployee in dataBaseEmployees)
            {
                T dtoObject = new();
                dtoObject.CopyInfoFromDataBaseEmployee(dbEmployee);

                employees.Add(dtoObject);
            };

            return employees;
        }

        /// <summary>
        /// Returns the total amount of employees in the DataBase.
        /// </summary>
        /// <returns>The total amount of employees in the DataBase.</returns>
        public int CountEmployees()
        {
            return GetAllEmployees().Count();
        }

        /// <summary>
        /// Gets the employee with the specified id.
        /// </summary>
        /// <param name="id">Id of the employee.</param>
        /// <returns>The employee object or null if <paramref name="id"/> doesn't exist.</returns>
        public Employee GetEmployeeById(int id)
        {
            return GetAllEmployees().FirstOrDefault(employee => employee.EmployeeId == id);
        }

        /// <summary>
        /// Given a certain number of elements per page, calculates the number of the last page.
        /// </summary>
        /// <param name="elementsPerPage">Maximum number of elements that contains a page.</param>
        /// <returns>The value of the last page.</returns>
        public int CalculateLastPage(int elementsPerPage)
        {
            int totalElements = CountEmployees();
            int lastPage = Convert.ToInt32(Math.Ceiling((double)totalElements / elementsPerPage));

            return lastPage;
        }

        /// <summary>
        /// Returns only the specified amount of employees in a certain page.
        /// The number of pages is calculated with the total amount of employees and the number of employees per page.
        /// The pages start at 1.
        /// </summary>
        /// <param name="elementsPerPage">The number of employees in a page.</param>
        /// <param name="page">The number of the page that will be retrieved.</param>
        /// <returns>An IQueryable with the selected employees.</returns>
        public IQueryable<Employee> GetPage(int elementsPerPage, int page)
        {
            if (elementsPerPage <= 0)
                throw new ArgumentOutOfRangeException(nameof(elementsPerPage));

            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));

            return GetAllEmployees()
                .Skip((page - 1) * elementsPerPage)
                .Take(elementsPerPage);
        }

        /// <summary>
        /// Returns an IQueryable of all the employees in the DataBase.
        /// </summary>
        public IQueryable<Employee> GetAllEmployees()
        {
            return dbContext.Employees.AsQueryable();            
        }

        /// <summary>
        /// Add a new record of an employee to the DataBase.
        /// </summary>
        /// <param name="newEmployee">Model of the employee being registered.</param>
        /// <returns>The id of the registered employee.</returns>
        public int AddNewEmployee(EmployeeDTO newEmployee)
        {
            if (newEmployee == null)
                throw new ArgumentNullException(nameof(newEmployee));

            Employee dataBaseEmployee = newEmployee.GetDataBaseEmployeeObject();

            dbContext.Employees.Add(dataBaseEmployee);
            dbContext.SaveChanges();

            return dataBaseEmployee.EmployeeId;
        }

        /// <summary>
        /// Modifies the information of an employee in the DataBase.
        /// </summary>
        /// <param name="id">Id of the employee being modified.</param>
        /// <param name="modifiedEmployee">Model with the new information of the employee.</param>
        public void UpdateEmployee(int id, EmployeeDTO modifiedEmployee)
        {
            if (modifiedEmployee == null)
                throw new ArgumentNullException(nameof(modifiedEmployee));

            Employee dataBaseEmployee = GetEmployeeById(id);

            if (dataBaseEmployee == null)
                throw new KeyNotFoundException();

            modifiedEmployee.ModifyDataBaseEmployee(dataBaseEmployee);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Modifies the information of an employee in the DataBase.
        /// </summary>
        /// <param name="dataBaseEmployee">Employee object in the DataBase.</param>
        /// <param name="modifiedEmployee">Model with the new information of the employee.</param>
        public void UpdateEmployee(Employee dataBaseEmployee, EmployeeDTO modifiedEmployee)
        {
            if (dataBaseEmployee == null)
                throw new ArgumentNullException(nameof(dataBaseEmployee));

            if (modifiedEmployee == null)
                throw new ArgumentNullException(nameof(modifiedEmployee));

            modifiedEmployee.ModifyDataBaseEmployee(dataBaseEmployee);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the record of an employee in the DataBase.
        /// </summary>
        /// <param name="id">Id of the employee being removed.</param>
        public void DeleteEmployee(int id)
        {
            Employee dataBaseEmployee = GetEmployeeById(id);

            if (dataBaseEmployee == null)
                throw new KeyNotFoundException();

            dbContext.Employees.Remove(dataBaseEmployee);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the record of an employee in the DataBase.
        /// </summary>
        /// <param name="dataBaseEmployee">Employee object in the DataBase.</param>
        public void DeleteEmployee(Employee dataBaseEmployee)
        {
            if (dataBaseEmployee == null)
                throw new ArgumentNullException(nameof(dataBaseEmployee));

            dbContext.Employees.Remove(dataBaseEmployee);

            dbContext.SaveChanges();
        }
    }
}