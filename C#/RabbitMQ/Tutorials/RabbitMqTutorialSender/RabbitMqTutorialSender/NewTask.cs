using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqTutorialSender
{
    /// <summary>
    /// Message Sender
    /// </summary>
    class NewTask
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
                    channel.QueueDeclare(queue: "task_queue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);

                    // marking messages as persistent for durability
                    var properties = channel.CreateBasicProperties();
                    properties.SetPersistent(true);

                    // Publish message to queue
                    channel.BasicPublish(exchange: "",
                                         routingKey: "task_queue",
                                         basicProperties: properties,
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
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello, World!");
        }
    }
}
