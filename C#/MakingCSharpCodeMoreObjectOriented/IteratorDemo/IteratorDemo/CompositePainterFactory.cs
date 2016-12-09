using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IPainter CreateGroup(IEnumerable<ProportionalPainter> painters)
        {
            return new CompositePainter<ProportionalPainter>(painters, (sqMeters, sequence) =>
            {
                TimeSpan time =
                    TimeSpan.FromHours(
                        sequence
                            .Where(painter => painter.IsAvailable)
                            .Select(painter => 1/painter.EstimateTimeToPaint(sqMeters).TotalHours)
                            .Sum());

                double cost =
                    sequence
                        .Where(painter => painter.IsAvailable)
                        .Select(painter =>
                            painter.EstimateCompensation(sqMeters)/
                            painter.EstimateTimeToPaint(sqMeters).TotalHours*time.TotalHours)
                        .Sum();

                return new ProportionalPainter()
                {
                    TimePerSqMeter = TimeSpan.FromHours(time.TotalHours/sqMeters),
                    DollarsPerHour = cost/time.TotalHours
                };
            });
        }
    }
}
