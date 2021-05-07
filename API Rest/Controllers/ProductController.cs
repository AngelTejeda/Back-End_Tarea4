using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.BackEnd;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace API_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IActionResult InternalServerError(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            List<ProductBasicInfoDTO> products = new();

            using (NorthwindContext dbContext = new())
            {
                IQueryable<Product> dbProducts = ProductSC.GetAllProducts(dbContext).AsNoTracking();

                foreach (Product dbProduct in dbProducts)
                {
                    products.Add(new ProductBasicInfoDTO(dbProduct));
                };
            }

            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ProductBasicInfoDTO product;

                using (NorthwindContext dbContext = new())
                {
                    Product dbProduct = ProductSC.GetProductById(dbContext, id);

                    product = new(dbProduct);
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductBasicInfoDTO newProduct)
        {
            int id;

            try
            {
                using (NorthwindContext dbContext = new())
                {
                    id = ProductSC.AddNewProduct(dbContext, newProduct);
                }

                return Ok("Registered Id: " + id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductBasicInfoDTO modifiedProduct)
        {
            try
            {
                using (NorthwindContext dbContext = new())
                {
                    ProductSC.UpdateProduct(dbContext, id, modifiedProduct);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (NorthwindContext dbContext = new())
                {
                    ProductSC.DeleteProduct(dbContext, id);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
