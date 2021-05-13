using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.BackEnd;
using Tarea_4.DataAccess;
using Tarea_4.ExceptionHandling;
using Tarea_4.Models;

namespace API_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET api/<ProductController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id < 1)
                return BadRequest($"{nameof(id)} must be at least 1.");

            // Get Product from Database
            Product dbProduct = new ProductSC().GetProductById(id);

            if (dbProduct == null)
                return NotFound();

            ProductBasicInfoDTO product = new(dbProduct);

            return Ok(product);
        }

        // GET: api/<ProductController>/page/{page}
        [HttpGet]
        [Route("pages/{requestedPage}")]
        public IActionResult GetPage(int requestedPage)
        {
            const int elementsPerPage = 10;

            if (requestedPage < 1)
                return BadRequest($"{nameof(requestedPage)} must be at least 1.");

            // Calculate Pages
            int lastPage = new ProductSC().CalculateLastPage(elementsPerPage);
            Pagination<ProductBasicInfoDTO> response = new(requestedPage, lastPage);

            // Get Selected Page
            IQueryable<Product> dbProducts = new ProductSC().GetPage(elementsPerPage, response.CurrentPage);
            List<ProductBasicInfoDTO> products = BaseSC.MaterializeIQueryable<Product, ProductBasicInfoDTO>(dbProducts);

            // Attach elements of the page to the response
            response.ResponseList = products;

            return Ok(response);
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<Product> dbProducts = new ProductSC().GetAllProducts();

            List<ProductBasicInfoDTO> products = BaseSC.MaterializeIQueryable<Product, ProductBasicInfoDTO>(dbProducts);

            return Ok(products);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductBasicInfoPostDTO newProduct)
        {
            int id;

            try
            {
                id = new ProductSC().AddNewProduct(newProduct);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex.InnerException as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return Created("GET " + Request.Path.Value + "/" + id, id);
        }

        // PUT api/<ProductController>/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductBasicInfoPutDTO modifiedProduct)
        {
            Product dataBaseProduct = new ProductSC().GetProductById(id);

            if (dataBaseProduct == null)
                return NotFound();

            try
            {
                //TODO: Check if it is possible to pass the dataBaseProduct insted of the id.
                new ProductSC().UpdateProduct(id, modifiedProduct);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex.InnerException as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return NoContent();
        }

        // DELETE api/<ProductController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product dataBaseProduct = new ProductSC().GetProductById(id);

            if (dataBaseProduct == null)
                return NotFound();

            try
            {
                new ProductSC().DeleteProduct(dataBaseProduct);
            }
            catch (Exception ex) when (ExceptionTypes.IsSqlException(ex))
            {
                string message = SqlExceptionMessages.GetCustomSqlExceptionMessage(ex.InnerException as SqlException);

                if (message != null)
                    return Conflict(message);

                throw;
            }

            return NoContent();
        }
    }
}
