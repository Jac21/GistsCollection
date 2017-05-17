using System;

namespace EventsDelegatesAndLambdas.EventHandlers
{
    /// <summary>
    /// Custom EventArgs class for WorkPerformed
    /// </summary>
    public class WorkPerformedEventArgs : EventArgs
    {
        public int Hours { get; set; }
        public WorkType WorkType { get; set; }

        public WorkPerformedEventArgs(int hours, WorkType workType)
        {
            this.Hours = hours;
            this.WorkType = workType;
        }
    }
}
