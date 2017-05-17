using System.Collections.Generic;
using System.Linq;

namespace IteratorDemo
{
    /// <summary>
    /// Abstracted composite structure
    /// </summary>
    class Painters
    {
        private IEnumerable<IPainter> ContainedPainters { get; set; }

        public Painters(IEnumerable<IPainter> painters)
        {
            this.ContainedPainters = painters.ToList();
        }

        public Painters GetAvailable()
        {
            /*
             * e.g., Let's say all the painters are available, 
             * just return the structure
             * for some optimization
            if (this.ContainedPainters.All(painter => painter.IsAvailable))
            {
                return this;
            }
             */

            return new Painters(this.ContainedPainters.Where(painter => painter.IsAvailable));
        }

        public IPainter GetCheapestOne(double sqMeters)
        {
            return this.ContainedPainters.WithMinimum(painter => painter.EstimateCompensation(sqMeters));
        }

        public IPainter GetFastestOne(double sqMeters)
        {
            return this.ContainedPainters.WithMinimum(painter => painter.EstimateTimeToPaint(sqMeters));
        }
    }
}
