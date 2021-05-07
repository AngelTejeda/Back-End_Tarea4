using System;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public static class ProductSC
    {
        private static readonly string InstanceName = "product";

        public static IQueryable<Product> GetAllProducts(NorthwindContext dbContext)
        {
            return dbContext.Products.AsQueryable();
        }

        public static Product GetProductById(NorthwindContext dbContext, int id)
        {
            try
            {
                return GetAllProducts(dbContext).First(product => product.ProductId == id);
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

        public static int AddNewProduct(NorthwindContext dbContext, ProductDTO newProduct)
        {
            try
            {
                Product dataBaseProduct = newProduct.GetDataBaseProductObject();

                dbContext.Products.Add(dataBaseProduct);
                dbContext.SaveChanges();

                return dataBaseProduct.ProductId;
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

        public static void UpdateProduct(NorthwindContext dbContext, int id, ProductDTO modifiedProduct)
        {
            try
            {
                Product dataBaseProduct = GetProductById(dbContext, id);

                modifiedProduct.ModifyDataBaseProduct(dataBaseProduct);

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

        public static void DeleteProduct(NorthwindContext dbContext, int id)
        {
            try
            {
                Product dataBaseProduct = GetProductById(dbContext, id);

                dbContext.Products.Remove(dataBaseProduct);

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