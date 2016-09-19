using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using APM.WebApi.Repository;
using APM.WebAPI.Models;

namespace APM.WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductRepository _repository;

        public ProductsController()
        {
            _repository = new ProductRepository();
        }

        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            return _repository.Retrieve();
        }

        // GET: api/products?search=
        public IEnumerable<Product> Get(string search)
        {
            var products = _repository.Retrieve();
            return products.Where(p => p.ProductCode.Contains(search));
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
