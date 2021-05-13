using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public class CustomerSC : BaseSC
    {
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
            return BaseSC.CalculateLastPage(totalElements, elementsPerPage);
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
            return BaseSC.GetPage(GetAllCustomers(), elementsPerPage, page);
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
        public string AddNewCustomer(IAddible<Customer> newCustomer)
        {
            if (newCustomer == null)
                throw new ArgumentNullException(nameof(newCustomer));

            Customer dataBaseCustomer = newCustomer.GetDataBaseObject();

            dbContext.Customers.Add(dataBaseCustomer);
            dbContext.SaveChanges();

            return dataBaseCustomer.CustomerId;
        }

        /// <summary>
        /// Modifies the information of an customer in the DataBase.
        /// </summary>
        /// <param name="id">Id of the customer being modified.</param>
        /// <param name="modifiedCustomer">Model with the new information of the customer.</param>
        public void UpdateCustomer(string id, IUpdatable<Customer> modifiedCustomer)
        {
            if (modifiedCustomer == null)
                throw new ArgumentNullException(nameof(modifiedCustomer));

            Customer dataBaseCustomer = GetCustomerById(id);

            if (dataBaseCustomer == null)
                throw new KeyNotFoundException();

            modifiedCustomer.ModifyDataBaseObject(dataBaseCustomer);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Modifies the information of an customer in the DataBase.
        /// </summary>
        /// <param name="dataBaseCustomer">Customer object in the DataBase.</param>
        /// <param name="modifiedCustomer">Model with the new information of the customer.</param>
        public void UpdateCustomer(Customer dataBaseCustomer, IUpdatable<Customer> modifiedCustomer)
        {
            if (dataBaseCustomer == null)
                throw new ArgumentNullException(nameof(dataBaseCustomer));

            if (modifiedCustomer == null)
                throw new ArgumentNullException(nameof(modifiedCustomer));

            modifiedCustomer.ModifyDataBaseObject(dataBaseCustomer);

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