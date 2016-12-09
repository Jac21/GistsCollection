using System;
using System.Collections.Generic;
using System.Linq;

namespace IteratorDemo
{
    /// <summary>
    /// Composite pattern, applies composition function to a set of
    /// objects. CompositePainter is a "kind of" IPainter, can apply concept
    /// of ProportionalPainter to ensure validity of implemented methods
    /// </summary>
    class CompositePainter<TPainter> : IPainter
        where TPainter : IPainter
    {

        public bool IsAvailable
        {
            get { return this.Painters.Any(p => p.IsAvailable); } 
            set { throw new NotImplementedException(); }
        }

        private IEnumerable<TPainter> Painters { get; set; }

        private Func<double, IEnumerable<TPainter>, IPainter> Reduce { get; set; }

        public CompositePainter(IEnumerable<TPainter> painters,
            Func<double, IEnumerable<TPainter>, IPainter> reduce)
        {
            this.Painters = painters.ToList();
            this.Reduce = reduce;
        }

        public TimeSpan EstimateTimeToPaint(double sqMeters)
        {
            return this.Reduce(sqMeters, this.Painters).EstimateTimeToPaint(sqMeters);
        }

        public double EstimateCompensation(double sqMeters)
        {
            return this.Reduce(sqMeters, this.Painters).EstimateCompensation(sqMeters);
        }
    }
}
