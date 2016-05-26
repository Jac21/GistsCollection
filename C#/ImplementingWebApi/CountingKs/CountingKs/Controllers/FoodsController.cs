using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private ICountingKsRepository _repo;
        private ModelFactory _modelFactory;

        public FoodsController(ICountingKsRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        public IEnumerable<FoodModel> Get()
        {
            var repo = new CountingKsRepository(
                new CountingKsContext());

            var results = repo.GetAllFoodsWithMeasures()
                .OrderBy(f => f.Description)
                .Take(25)
                .ToList()
                .Select(f => _modelFactory.Create(f));

            return results;
        }

        public FoodModel Get(int id) {
            return _modelFactory.Create(_repo.GetFood(id));
        }
    }
}
