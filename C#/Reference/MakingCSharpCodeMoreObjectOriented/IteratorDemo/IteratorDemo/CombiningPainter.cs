using System;
using System.Collections.Generic;
using System.Linq;

namespace IteratorDemo
{
    /// <summary>
    /// Strategy pattern, turning pieces of algorithms into objects,
    /// utilizing Tuples before concrete classes, then abstracting, 
    /// building template methods out of them
    /// </summary>
    /// <typeparam name="TPainter"></typeparam>
    class CombiningPainter<TPainter> : CompositePainter<TPainter>
        where TPainter : IPainter
    {

        private IPaintingScheduler<TPainter> Scheduler { get; set; }

        public CombiningPainter(IEnumerable<TPainter> painters, IPaintingScheduler<TPainter> scheduler)
            : base(painters)
        {
            Reduce = this.Combine;
            this.Scheduler = scheduler;
        }


        private IPainter Combine(double sqMeters, IEnumerable<TPainter> painters)
        {
            IEnumerable<TPainter> availablePainters = painters.Where(painter => painter.IsAvailable).ToList();

            IEnumerable<PaintingTask<TPainter>> schedule = 
                this.Scheduler.Schedule(sqMeters, availablePainters);

            TimeSpan time = schedule.Max(task => task.Painter.EstimateTimeToPaint(task.SquareMeters));

            double cost = schedule.Sum(task => task.Painter.EstimateCompensation(task.SquareMeters));

            return new ProportionalPainter()
            {
                TimePerSqMeter = TimeSpan.FromHours(time.TotalHours / sqMeters),
                DollarsPerHour = cost / time.TotalHours
            };
        }

    }
}
