using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountingKs.Data.Entities
{
  public class Measure
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public double Calories { get; set; }
    public double TotalFat { get; set; }
    public double SaturatedFat { get; set; }
    public double Protein { get; set; }
    public double Carbohydrates { get; set; }
    public double Fiber { get; set; }
    public double Sugar { get; set; }
    public double Sodium { get; set; }
    public double Iron { get; set; }
    public double Cholestrol { get; set; }

    public virtual Food Food { get; set; }
  }
}
