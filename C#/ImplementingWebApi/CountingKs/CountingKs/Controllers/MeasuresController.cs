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
    public class MeasuresController : ApiController
    {
        private ICountingKsRepository _repo;
        private ModelFactory _modelFactory;

        public MeasuresController(ICountingKsRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        public IEnumerable<MeasureModel> Get(int id)
        {
            var results = _repo.GetMeasuresForFood(id)
                .ToList()
                .Select(m => _modelFactory.Create(m));

            return results;
        }

        public MeasureModel Get(int id, int measureId)
        {
            var results = _repo.GetMeasure(measureId);

            if (results.Food.Id == id)
            {
                return _modelFactory.Create(results);
            }

            return null;
        } 
    }
}
