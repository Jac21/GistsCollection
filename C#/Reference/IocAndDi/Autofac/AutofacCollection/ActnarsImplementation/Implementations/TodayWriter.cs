using System;
using ActnarsImplementation.Interfaces;

namespace ActnarsImplementation.Implementations
{
    // This TodayWriter is where it all comes together.
    // Notice it takes a constructor parameter of type
    // IOutput - that lets the writer write to anywhere
    // based on the implementation. Further, it implements
    // WriteDate such that today's date is written out;
    // you could have one that writes in a different format
    // or a different date.
    public class TodayWriter : IDateWriter
    {
        private readonly IOutput output;

        public TodayWriter(IOutput output)
        {
            this.output = output;
        }

        public void WriteDate()
        {
            output.Write(DateTime.Today.ToShortDateString());
        }
    }
}