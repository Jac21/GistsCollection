using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using APM.WebApi.Repository;
using APM.WebAPI.Models;

namespace APM.WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ProductRepository _repository;

        public ProductsController()
        {
            _repository = new ProductRepository();
        }

        // GET: api/Products
        [EnableQuery()]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repository.Retrieve().AsQueryable());
            }
            catch (HttpResponseException ex)
            {
                return InternalServerError();
            }
        }

        // GET: api/products?search=
        public IEnumerable<Product> Get(string search)
        {
            var products = _repository.Retrieve();
            return products.Where(p => p.ProductCode.Contains(search));
        }

        // GET: api/Products/5
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Product product;

                if (id > 0)
                {
                    var products = _repository.Retrieve();
                    product = products.FirstOrDefault(p => p.ProductId == id);

                    if (product == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    product = _repository.Create();
                }

                return Ok(product);
            }
            catch (HttpResponseException ex)
            {
                return InternalServerError();
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newProduct = _repository.Save(product);

                if (newProduct == null)
                {
                    return Conflict();
                }

                return Created(Request.RequestUri + newProduct.ProductId.ToString(),
                    newProduct);
            }
            catch (HttpResponseException ex)
            {
                return InternalServerError();
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedProduct = _repository.Save(id, product);

                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (HttpResponseException ex)
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
