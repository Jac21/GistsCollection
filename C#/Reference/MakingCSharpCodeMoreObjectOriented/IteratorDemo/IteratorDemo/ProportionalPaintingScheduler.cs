using System;
using System.Collections.Generic;
using System.Linq;

namespace IteratorDemo
{
    class ProportionalPaintingScheduler : IPaintingScheduler<ProportionalPainter>
    {
        public IEnumerable<PaintingTask<ProportionalPainter>> Schedule(double sqMeters,
            IEnumerable<ProportionalPainter> painters)
        {
            IEnumerable<Tuple<ProportionalPainter, double>> velocities =
                painters
                    .Select(painter =>
                            Tuple.Create(painter, sqMeters/painter.EstimateTimeToPaint(sqMeters).TotalHours))
                    .ToList();

            double totalVelocity = velocities.Sum(tuple => tuple.Item2);

            IEnumerable<PaintingTask<ProportionalPainter>> schedule =
                velocities
                    .Select(tuple =>
                        new PaintingTask<ProportionalPainter>(
                            tuple.Item1,
                            sqMeters*tuple.Item2/totalVelocity))
                    .ToList();

            return schedule;
        }
    }
}