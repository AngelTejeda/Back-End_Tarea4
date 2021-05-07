using System;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public static class CustomerSC
    {
        private static readonly string InstanceName = "customer";

        public static IQueryable<Customer> GetAllCustomers(NorthwindContext dbContext)
        {
            return dbContext.Customers.AsQueryable();
        }

        public static Customer GetCustomerById(NorthwindContext dbContext, string id)
        {
            try
            {
                return GetAllCustomers(dbContext).First(customer => customer.CustomerId == id);
            }
            catch (ArgumentNullException ex)
            {
                ex.SetMessage(DbExceptionMessages.FieldIsRequired("id"));
                throw;
            }
            catch (InvalidOperationException ex)
            {
                ex.SetMessage(DbExceptionMessages.InstanceNotFound(InstanceName, id));
                throw;
            }
        }

        public static string AddNewCustomer(NorthwindContext dbContext, CustomerDTO newCustomer)
        {
            try
            {
                Customer dataBaseCustomer = newCustomer.GetDataBaseCustomerObject();

                dbContext.Customers.Add(dataBaseCustomer);
                dbContext.SaveChanges();

                return dataBaseCustomer.CustomerId;
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                ex.SetMessage(DbExceptionMessages.FailedToAdd(InstanceName, ex.InnerException));
                throw;
            }
            catch (Exception ex) when (ExceptionTypes.IsDbException(ex))
            {
                ex.SetMessage(DbExceptionMessages.UnexpectedFailure(ex));
                throw;
            }

        }

        public static void UpdateCustomer(NorthwindContext dbContext, string id, CustomerDTO modifiedCustomer)
        {
            try
            {
                Customer dataBaseCustomer = GetCustomerById(dbContext, id);

                modifiedCustomer.ModifyDataBaseCustomer(dataBaseCustomer);

                dbContext.SaveChanges();
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                ex.SetMessage(DbExceptionMessages.FailedToUpdate(InstanceName, id, ex.InnerException));
                throw;
            }
            catch (Exception ex) when (ExceptionTypes.IsDbException(ex))
            {
                ex.SetMessage(DbExceptionMessages.UnexpectedFailure(ex));
                throw;
            }
        }

        public static void DeleteCustomer(NorthwindContext dbContext, string id)
        {
            try
            {
                Customer dataBaseCustomer = GetCustomerById(dbContext, id);

                dbContext.Customers.Remove(dataBaseCustomer);

                dbContext.SaveChanges();
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                ex.SetMessage(DbExceptionMessages.FailedToDelete(InstanceName, id, ex.InnerException));
                throw;
            }
            catch (Exception ex) when (ExceptionTypes.IsDbException(ex))
            {
                ex.SetMessage(DbExceptionMessages.UnexpectedFailure(ex));
                throw;
            }
        }
    }
}