using System;

namespace IteratorDemo
{
    public interface IPainter
    {
        bool IsAvailable { get; set; }
        TimeSpan EstimateTimeToPaint(double sqMeters);
        double EstimateCompensation(double sqMeters);
    }
}
