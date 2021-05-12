using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public class ProductSC : BaseSC
    {
        /// <summary>
        /// Materializes a given IQueryable of products into a List.
        /// </summary>
        /// <typeparam name="T"> A Data Transfer Object Class of the products.</typeparam>
        /// <param name="dataBaseProducts">IQueryable object of the products that will be materialized.</param>
        /// <returns>A List<typeparamref name="T"/> with the materialized products.</returns>
        public static List<T> MaterializeIQueryable<T>(IQueryable<Product> dataBaseProducts) where T : ProductDTO, new()
        {
            List<T> products = new();

            dataBaseProducts = dataBaseProducts.AsNoTracking();

            foreach (Product dbProduct in dataBaseProducts)
            {
                T dtoObject = new();
                dtoObject.CopyInfoFromDataBaseProduct(dbProduct);

                products.Add(dtoObject);
            };

            return products;
        }

        /// <summary>
        /// Returns the total amount of products in the DataBase.
        /// </summary>
        /// <returns>The total amount of products in the DataBase.</returns>
        public int CountProducts()
        {
            return GetAllProducts().Count();
        }

        /// <summary>
        /// Gets the product with the specified id.
        /// </summary>
        /// <param name="id">Id of the product.</param>
        /// <returns>The product object or null if <paramref name="id"/> doesn't exist.</returns>
        public Product GetProductById(int id)
        {
            return GetAllProducts().FirstOrDefault(product => product.ProductId == id);
        }

        /// <summary>
        /// Given a certain number of elements per page, calculates the number of the last page.
        /// </summary>
        /// <param name="elementsPerPage">Maximum number of elements that contains a page.</param>
        /// <returns>The value of the last page.</returns>
        public int CalculateLastPage(int elementsPerPage)
        {
            int totalElements = CountProducts();
            int lastPage = Convert.ToInt32(Math.Ceiling((double)totalElements / elementsPerPage));

            return lastPage;
        }

        /// <summary>
        /// Returns only the specified amount of products in a certain page.
        /// The number of pages is calculated with the total amount of products and the number of products per page.
        /// The pages start at 1.
        /// </summary>
        /// <param name="elementsPerPage">The number of products in a page.</param>
        /// <param name="page">The number of the page that will be retrieved.</param>
        /// <returns>An IQueryable with the selected products.</returns>
        public IQueryable<Product> GetPage(int elementsPerPage, int page)
        {
            if (elementsPerPage <= 0)
                throw new ArgumentOutOfRangeException(nameof(elementsPerPage));

            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));

            return GetAllProducts()
                .Skip((page - 1) * elementsPerPage)
                .Take(elementsPerPage);
        }

        /// <summary>
        /// Returns an IQueryable of all the products in the DataBase.
        /// </summary>
        public IQueryable<Product> GetAllProducts()
        {
            return dbContext.Products.AsQueryable();
        }

        /// <summary>
        /// Add a new record of an product to the DataBase.
        /// </summary>
        /// <param name="newProduct">Model of the product being registered.</param>
        /// <returns>The id of the registered product.</returns>
        public int AddNewProduct(ProductDTO newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException(nameof(newProduct));

            Product dataBaseProduct = newProduct.GetDataBaseProductObject();

            dbContext.Products.Add(dataBaseProduct);
            dbContext.SaveChanges();

            return dataBaseProduct.ProductId;
        }

        /// <summary>
        /// Modifies the information of an product in the DataBase.
        /// </summary>
        /// <param name="id">Id of the product being modified.</param>
        /// <param name="modifiedProduct">Model with the new information of the product.</param>
        public void UpdateProduct(int id, ProductDTO modifiedProduct)
        {
            if (modifiedProduct == null)
                throw new ArgumentNullException(nameof(modifiedProduct));

            Product dataBaseProduct = GetProductById(id);

            if (dataBaseProduct == null)
                throw new KeyNotFoundException();

            modifiedProduct.ModifyDataBaseProduct(dataBaseProduct);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Modifies the information of an product in the DataBase.
        /// </summary>
        /// <param name="dataBaseProduct">Product object in the DataBase.</param>
        /// <param name="modifiedProduct">Model with the new information of the product.</param>
        public void UpdateProduct(Product dataBaseProduct, ProductDTO modifiedProduct)
        {
            if (dataBaseProduct == null)
                throw new ArgumentNullException(nameof(dataBaseProduct));

            if (modifiedProduct == null)
                throw new ArgumentNullException(nameof(modifiedProduct));

            modifiedProduct.ModifyDataBaseProduct(dataBaseProduct);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the record of an product in the DataBase.
        /// </summary>
        /// <param name="id">Id of the product being removed.</param>
        public void DeleteProduct(int id)
        {
            Product dataBaseProduct = GetProductById(id);

            if (dataBaseProduct == null)
                throw new KeyNotFoundException();

            dbContext.Products.Remove(dataBaseProduct);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the record of an product in the DataBase.
        /// </summary>
        /// <param name="dataBaseProduct">Product object in the DataBase.</param>
        public void DeleteProduct(Product dataBaseProduct)
        {
            if (dataBaseProduct == null)
                throw new ArgumentNullException(nameof(dataBaseProduct));

            dbContext.Products.Remove(dataBaseProduct);

            dbContext.SaveChanges();
        }
    }
}