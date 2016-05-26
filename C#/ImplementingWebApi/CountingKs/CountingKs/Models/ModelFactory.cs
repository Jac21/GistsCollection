using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CountingKs.Data.Entities;

namespace CountingKs.Models
{
    public class ModelFactory
    {
        public FoodModel Create(Food food)
        {
            return new FoodModel()
            {
                Description = food.Description,
                Measures = food.Measures.Select(m => Create(m))
            };
        }

        public MeasureModel Create(Measure measure)
        {
            return new MeasureModel()
            {
                Description = measure.Description,
                Calories = Math.Round(measure.Calories)
            };
        }
    }
}