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
    public class FoodsController : BaseApiController
    {
        public FoodsController(ICountingKsRepository repo) : base(repo)
        {
        }

        public IEnumerable<FoodModel> Get(bool includeMeasures = true)
        {
            IQueryable<Food> query;

            if (includeMeasures)
            {
                query = TheRepository.GetAllFoodsWithMeasures();
            } 
            else
            {
                query = TheRepository.GetAllFoods();
            }

            var results = query
                    .OrderBy(f => f.Description)
                    .Take(25)
                    .ToList()
                    .Select(f => TheModelFactory.Create(f));

            return results;
        }

        public FoodModel Get(int id) {
            return TheModelFactory.Create(TheRepository.GetFood(id));
        }
    }
}
