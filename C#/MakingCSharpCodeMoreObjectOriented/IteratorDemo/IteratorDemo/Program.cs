using System.Collections.Generic;
using System.Linq;

namespace IteratorDemo
{
    class Program
    {
        private static IPainter FindCheapestPainter(double sqMeters, 
                                                    IEnumerable<IPainter> painters)
        {
            return
                painters
                    .Where(painter => painter.IsAvailable)
                    .WithMinimum(painter => painter.EstimateCompensation(sqMeters));

        }

        static void Main()
        {
            List<Painter> painters = new List<Painter>();
            
            Painter painterOne = new Painter()
            {
                IsAvailable = true
            };

            Painter painterTwo = new Painter()
            {
                IsAvailable = false
            };

            painters.Add(painterOne);
            painters.Add(painterTwo);

            FindCheapestPainter(100, painters);
        }
    }
}
