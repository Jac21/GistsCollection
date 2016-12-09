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
            throw new NotImplementedException();
        }
    }
}
