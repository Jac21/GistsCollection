using System.Collections.Generic;

namespace IteratorDemo
{
    /// <summary>
    /// Constrained genertic composite structure/"Mother Class"
    /// </summary>
    class CompositePainterFactory
    {
        public static IPainter CreateCheapestSelector(IEnumerable<IPainter> painters)
        {
            return new CompositePainter<IPainter>(painters,
                (sqMeters, sequence) => new Painters(sequence).GetAvailable().GetCheapestOne(sqMeters));
        }

        public static IPainter CreateFastestSelector(IEnumerable<IPainter> painters)
        {
            return new CompositePainter<IPainter>(painters,
                (sqMeters, sequence) => new Painters(sequence).GetAvailable().GetFastestOne(sqMeters));
        }

        public static IPainter CombineProportional(IEnumerable<ProportionalPainter> painters)
        {
            return new CombiningPainter<ProportionalPainter>(painters, new ProportionalPaintingScheduler());
        }
    }
}
