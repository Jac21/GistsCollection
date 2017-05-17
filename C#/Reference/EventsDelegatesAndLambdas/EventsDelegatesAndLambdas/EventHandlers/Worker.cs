using System;

namespace EventsDelegatesAndLambdas.EventHandlers
{
    // public delegate int WorkPerformedHandler(object sender, WorkPerformedEventArgs e);

    public class Worker
    {
        // EventHandler<T> when just using events, no need for the delegate above
        public event EventHandler<WorkPerformedEventArgs> WorkPerformed;
        public event EventHandler WorkCompleted;

        /// <summary>
        /// Simulate some work done/processing
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="workType"></param>
        public void DoWork(int hours, WorkType workType)
        {
            for (int i = 0; i < hours; i++)
            {
                // Simulate a wait/processing
                System.Threading.Thread.Sleep(500);

                // Raise event
                OnWorkPerformed(i + 1, workType);
            }

            // Raise event
            OnWorkCompleted();
        }

        /// <summary>
        /// Event
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="workType"></param>
        protected virtual void OnWorkPerformed(int hours, WorkType workType)
        {
            // init delegate, if not null, use it with EventArgs class
            var del = WorkPerformed;

            if (del != null)
            {
                del(this, new WorkPerformedEventArgs(hours, workType));
            }
        }

        /// <summary>
        /// Event
        /// </summary>
        protected virtual void OnWorkCompleted()
        {
            var del = WorkCompleted;

            if (del != null)
            {
                del(this, EventArgs.Empty);
            }
        }
    }
}
