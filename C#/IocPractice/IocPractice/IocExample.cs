using System;

namespace IocPractice
{
    /// <summary>
    /// Dependency injenction (by method) example class, 
    /// </summary>
    class IocExample
    {
        /// <summary>
        /// Contract interface for the below class' actions
        /// </summary>
        public interface INotificationAction
        {
            void ActOnNotification(string message);
        }

        /// <summary>
        /// Method instantiation class
        /// </summary>
        class EventLogWriter : INotificationAction
        {
            public void ActOnNotification(string message)
            {
                // write to event log
                Console.WriteLine("Written to event log...");
            }
        }

        /// <summary>
        /// Higher-level module for class abstraction use
        /// </summary>
        class AppPoolWatcher
        {
            // handle to EventLog writer to write to the logs
            private readonly INotificationAction _action;

            public AppPoolWatcher(INotificationAction concreteImplementation)
            {
                _action = concreteImplementation;
            }

            public void Notify(string message)
            {
                _action.ActOnNotification(message);
            }
        }

        /// <summary>
        /// Email sender class, utilizes NotificationAction interface
        /// </summary>
        class EmailSender : INotificationAction
        {
            public void ActOnNotification(string message)
            {
                //send email from here
                Console.WriteLine(message);
            }
        }

        /// <summary>
        /// SMS sender class, utilizes NotificationAction interface
        /// </summary>
        class SmsSender : INotificationAction
        {
            public void ActOnNotification(string message)
            {
                //send sms from here
                Console.WriteLine(message);
            }
        }

        static void Main()
        {
            // initialize event log extension class and app pool module
            EventLogWriter writer = new EventLogWriter();
            AppPoolWatcher watcher = new AppPoolWatcher(writer);

            watcher.Notify("Sample message to log");

            // sender class initializers
            EmailSender emailSender = new EmailSender();
            SmsSender smsSender = new SmsSender();

            // inject classes into the AppPoolWatcher
            AppPoolWatcher emailWatcher = new AppPoolWatcher(emailSender);
            AppPoolWatcher smsWatcher = new AppPoolWatcher(smsSender);

            // call base methods
            emailWatcher.Notify("Sample email sent..");
            smsWatcher.Notify("Sample SMS sent..");
        }
    }
}
