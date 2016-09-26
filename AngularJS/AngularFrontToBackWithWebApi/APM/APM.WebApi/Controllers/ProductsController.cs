using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
        public IQueryable<Product> Get()
        {
            return _repository.Retrieve().AsQueryable();
        }

        // GET: api/products?search=
        public IEnumerable<Product> Get(string search)
        {
            var products = _repository.Retrieve();
            return products.Where(p => p.ProductCode.Contains(search));
        }

        // GET: api/Products/5
        public Product Get(int id)
        {
            Product product;

            if (id > 0)
            {
                var products = _repository.Retrieve();
                product = products.FirstOrDefault(p => p.ProductId == id);
            }
            else
            {
                product = _repository.Create();
            }

            return product;
        }

        // POST: api/Products
        public void Post([FromBody]Product product)
        {
            var newProduct = _repository.Save(product);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]Product product)
        {
            var updatedProduct = _repository.Save(id, product);
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
