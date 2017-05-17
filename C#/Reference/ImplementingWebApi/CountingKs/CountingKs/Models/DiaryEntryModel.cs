using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountingKs.Models
{
    public class DiaryEntryModel
    {
        public string Url { get; set; }
        public string FoodDescription { get; set; }
        public string MeasureDescription { get; set; }
        public string MeasureUrl { get; set; }
        public double Quantity { get; set; }
    }
}
