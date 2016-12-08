using System;

namespace IteratorDemo
{
    class Painter : IPainter
    {
        public bool IsAvailable { get; set; }
        public TimeSpan EstimateTimeToPaint(double sqMeters)
        {
            throw new NotImplementedException();
        }

        public double EstimateCompensation(double sqMeters)
        {
            // dummy data
            double compensation = sqMeters*5.0;
            return compensation;
        }
    }
}
