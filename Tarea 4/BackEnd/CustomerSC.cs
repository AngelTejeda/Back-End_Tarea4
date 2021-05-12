using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public class CustomerSC : BaseSC
    {
        /// <summary>
        /// Materializes a given IQueryable of customers into a List.
        /// </summary>
        /// <typeparam name="T"> A Data Transfer Object Class of the customers.</typeparam>
        /// <param name="dataBaseCustomers">IQueryable object of the customers that will be materialized.</param>
        /// <returns>A List<typeparamref name="T"/> with the materialized customers.</returns>
        public static List<T> MaterializeIQueryable<T>(IQueryable<Customer> dataBaseCustomers) where T : CustomerDTO, new()
        {
            List<T> customers = new();

            dataBaseCustomers = dataBaseCustomers.AsNoTracking();

            foreach (Customer dbCustomer in dataBaseCustomers)
            {
                T dtoObject = new();
                dtoObject.CopyInfoFromDataBaseCustomer(dbCustomer);

                customers.Add(dtoObject);
            };

            return customers;
        }

        /// <summary>
        /// Returns the total amount of customers in the DataBase.
        /// </summary>
        /// <returns>The total amount of customers in the DataBase.</returns>
        public int CountCustomers()
        {
            return GetAllCustomers().Count();
        }

        /// <summary>
        /// Gets the customer with the specified id.
        /// </summary>
        /// <param name="id">Id of the customer.</param>
        /// <returns>The customer object or null if <paramref name="id"/> doesn't exist.</returns>
        public Customer GetCustomerById(string id)
        {
            return GetAllCustomers().FirstOrDefault(customer => customer.CustomerId == id);
        }

        /// <summary>
        /// Given a certain number of elements per page, calculates the number of the last page.
        /// </summary>
        /// <param name="elementsPerPage">Maximum number of elements that contains a page.</param>
        /// <returns>The value of the last page.</returns>
        public int CalculateLastPage(int elementsPerPage)
        {
            int totalElements = CountCustomers();
            int lastPage = Convert.ToInt32(Math.Ceiling((double)totalElements / elementsPerPage));

            return lastPage;
        }

        /// <summary>
        /// Returns only the specified amount of customers in a certain page.
        /// The number of pages is calculated with the total amount of customers and the number of customers per page.
        /// The pages start at 1.
        /// </summary>
        /// <param name="elementsPerPage">The number of customers in a page.</param>
        /// <param name="page">The number of the page that will be retrieved.</param>
        /// <returns>An IQueryable with the selected customers.</returns>
        public IQueryable<Customer> GetPage(int elementsPerPage, int page)
        {
            if (elementsPerPage <= 0)
                throw new ArgumentOutOfRangeException(nameof(elementsPerPage));

            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));

            return GetAllCustomers()
                .Skip((page - 1) * elementsPerPage)
                .Take(elementsPerPage);
        }

        /// <summary>
        /// Returns an IQueryable of all the customers in the DataBase.
        /// </summary>
        public IQueryable<Customer> GetAllCustomers()
        {
            return dbContext.Customers.AsQueryable();
        }

        /// <summary>
        /// Add a new record of an customer to the DataBase.
        /// </summary>
        /// <param name="newCustomer">Model of the customer being registered.</param>
        /// <returns>The id of the registered customer.</returns>
        public string AddNewCustomer(CustomerDTO newCustomer)
        {
            if (newCustomer == null)
                throw new ArgumentNullException(nameof(newCustomer));

            Customer dataBaseCustomer = newCustomer.GetDataBaseCustomerObject();

            dbContext.Customers.Add(dataBaseCustomer);
            dbContext.SaveChanges();

            return dataBaseCustomer.CustomerId;
        }

        /// <summary>
        /// Modifies the information of an customer in the DataBase.
        /// </summary>
        /// <param name="id">Id of the customer being modified.</param>
        /// <param name="modifiedCustomer">Model with the new information of the customer.</param>
        public void UpdateCustomer(string id, CustomerDTO modifiedCustomer)
        {
            if (modifiedCustomer == null)
                throw new ArgumentNullException(nameof(modifiedCustomer));

            Customer dataBaseCustomer = GetCustomerById(id);

            if (dataBaseCustomer == null)
                throw new KeyNotFoundException();

            modifiedCustomer.ModifyDataBaseCustomer(dataBaseCustomer);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Modifies the information of an customer in the DataBase.
        /// </summary>
        /// <param name="dataBaseCustomer">Customer object in the DataBase.</param>
        /// <param name="modifiedCustomer">Model with the new information of the customer.</param>
        public void UpdateCustomer(Customer dataBaseCustomer, CustomerDTO modifiedCustomer)
        {
            if (dataBaseCustomer == null)
                throw new ArgumentNullException(nameof(dataBaseCustomer));

            if (modifiedCustomer == null)
                throw new ArgumentNullException(nameof(modifiedCustomer));

            modifiedCustomer.ModifyDataBaseCustomer(dataBaseCustomer);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the record of an customer in the DataBase.
        /// </summary>
        /// <param name="id">Id of the customer being removed.</param>
        public void DeleteCustomer(string id)
        {
            Customer dataBaseCustomer = GetCustomerById(id);

            if (dataBaseCustomer == null)
                throw new KeyNotFoundException();

            dbContext.Customers.Remove(dataBaseCustomer);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the record of an customer in the DataBase.
        /// </summary>
        /// <param name="dataBaseCustomer">Customer object in the DataBase.</param>
        public void DeleteCustomer(Customer dataBaseCustomer)
        {
            if (dataBaseCustomer == null)
                throw new ArgumentNullException(nameof(dataBaseCustomer));

            dbContext.Customers.Remove(dataBaseCustomer);

            dbContext.SaveChanges();
        }
    }
}