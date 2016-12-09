using System.Collections.Generic;

namespace IteratorDemo
{
    class Program
    {
        static void Main()
        {
            IEnumerable<ProportionalPainter> painters = new ProportionalPainter[10];

            IPainter fastestPainter = CompositePainterFactory.CreateFastestSelector(painters);
            IPainter groupOfPainters = CompositePainterFactory.CreateGroup(painters);

        }
    }
}
