using System;

namespace BasicAutofac
{
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