using System;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;
using RabbitMQ.Client;

namespace RabbitMqTutorialSender
{
    /// <summary>
    /// Message Sender
    /// </summary>
    class EmitLogDirect
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
                    channel.ExchangeDeclare(exchange:"direct_logs", type:"direct"); // use a routing key to direct messages

                    var severity = (args.Length > 0) ? args[0] : "info";
                    var message = (args.Length > 1)
                        ? string.Join(" ", args.Skip(1).ToArray())
                        : "Hello, World!";

                    var body = Encoding.UTF8.GetBytes(message);


                    // Publish message to queue
                    channel.BasicPublish(exchange: "direct_logs",
                                         routingKey: severity,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine("[X] Sent '{0}' : '{1}' with '{2}' byte size", message, severity,
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
