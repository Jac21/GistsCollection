using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqTutorialReceiver
{
    class Receive
    {
        static void Main()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Declare queue here as well, start receiver before the sender
                    // to make sure queue exists before trying to consume messages
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    // Push messages from the queue asynchronously
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[X] Received {0}", message);
                    };

                    channel.BasicConsume(queue: "hello",
                        noAck: true,
                        consumer: consumer);

                    Console.WriteLine("Press [Enter] to exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
