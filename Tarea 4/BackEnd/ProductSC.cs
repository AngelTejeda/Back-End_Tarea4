using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public class ProductSC : BaseSC
    {
        public int CountProducts()
        {
            return GetAllProducts().Count();
        }

        public Product GetProductById(int id)
        {
            return GetAllProducts().FirstOrDefault(product => product.ProductId == id);
        }

        public int CalculateLastPage(int elementsPerPage)
        {
            int totalElements = CountProducts();
            return BaseSC.CalculateLastPage(totalElements, elementsPerPage);
        }

        public IQueryable<Product> GetPage(int elementsPerPage, int page)
        {
            return BaseSC.GetPage(GetAllProducts(), elementsPerPage, page);
        }

        public IQueryable<Product> GetAllProducts()
        {
            return dbContext.Products.AsQueryable();
        }

        public int AddNewProduct(IAddible<Product> newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException(nameof(newProduct));

            Product dataBaseProduct = newProduct.GetDataBaseObject();

            dbContext.Products.Add(dataBaseProduct);
            dbContext.SaveChanges();

            return dataBaseProduct.ProductId;
        }

        public void UpdateProduct(int id, IUpdatable<Product> modifiedProduct)
        {
            if (modifiedProduct == null)
                throw new ArgumentNullException(nameof(modifiedProduct));

            Product dataBaseProduct = GetProductById(id);

            if (dataBaseProduct == null)
                throw new KeyNotFoundException();

            modifiedProduct.ModifyDataBaseObject(dataBaseProduct);

            dbContext.SaveChanges();
        }

        public void UpdateProduct(Product dataBaseProduct, IUpdatable<Product> modifiedProduct)
        {
            if (dataBaseProduct == null)
                throw new ArgumentNullException(nameof(dataBaseProduct));

            if (modifiedProduct == null)
                throw new ArgumentNullException(nameof(modifiedProduct));

            modifiedProduct.ModifyDataBaseObject(dataBaseProduct);

            dbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            Product dataBaseProduct = GetProductById(id);

            if (dataBaseProduct == null)
                throw new KeyNotFoundException();

            dbContext.Products.Remove(dataBaseProduct);

            dbContext.SaveChanges();
        }

        public void DeleteProduct(Product dataBaseProduct)
        {
            if (dataBaseProduct == null)
                throw new ArgumentNullException(nameof(dataBaseProduct));

            dbContext.Products.Remove(dataBaseProduct);

            dbContext.SaveChanges();
        }
    }
}