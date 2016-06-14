using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqTutorialSender
{
    /// <summary>
    /// Message Sender
    /// </summary>
    class EmitLog
    {
        static void Main(string[] args)
        {
            // Create a connection to the server, broker on the local machine
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                // Declare a channel and queues for publishing
                using (var channel = connection.CreateModel())
                {
                    // Declare an exchange, receives messages from producers and 
                    // pushes them to queues, knows what to do with messages received
                    channel.ExchangeDeclare("logs", "fanout"); // just broadcasts all the messages it receives to all the queues it knows.

                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);

                    // Publish message to queue
                    channel.BasicPublish(exchange: "logs",
                                         routingKey: "",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine("[X] Sent {0} with {1} byte size", message,
                        Encoding.UTF8.GetByteCount(message));
                }

                Console.WriteLine("Press [Enter] to exit");
                Console.ReadLine();
            }
        }

        // get message from the command line argument
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) 
                ? string.Join(" ", args) 
                : "Hello, World!");
        }
    }
}
