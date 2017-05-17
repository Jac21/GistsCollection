using System;

namespace IteratorDemo
{
    /// <summary>
    /// Composite Pattern (compose number of objects 
    /// together to represent a single object) usage.
    /// </summary>
    class ProportionalPainter : IPainter
    {
        public TimeSpan TimePerSqMeter { get; set; }
        public double DollarsPerHour { get; set; }

        public bool IsAvailable
        {
            get { return true; }
            set { throw new NotImplementedException(); }
        } // => true;

        public TimeSpan EstimateTimeToPaint(double sqMeters)
        {
            return TimeSpan.FromHours(this.TimePerSqMeter.TotalHours*sqMeters);
        }

        public double EstimateCompensation(double sqMeters)
        {
            return this.EstimateTimeToPaint(sqMeters).TotalHours*this.DollarsPerHour;
        }
    }
}