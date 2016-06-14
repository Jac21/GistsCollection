using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqTutorialSender
{
    /// <summary>
    /// Message Sender
    /// </summary>
    class Send
    {
        static void Main()
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
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "Hello, World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    // Publish message to queue
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine("[X] Sent {0} with {1} byte size", message,
                        Encoding.UTF8.GetByteCount(message));
                }

                Console.WriteLine("Press [Enter] to exit");
                Console.ReadLine();
            }
        }
    }
}
