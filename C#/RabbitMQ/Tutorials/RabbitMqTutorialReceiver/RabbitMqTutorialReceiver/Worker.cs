using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqTutorialReceiver
{
    class Worker
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
                    channel.QueueDeclare(queue: "task_queue",
                        durable: true,  // making sure the queue isn't lost if RabbitMQ dies
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    // Tells RabbitMQ not to give more than one message to a worker at a time,
                    // i.e., don't dispatch new message to a worker until it has processed
                    // and acked the previous one
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    // Push messages from the queue asynchronously
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[X] Received {0}", message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [X] Done");

                        // Acking so message is never lost
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple:false);
                    };

                    channel.BasicConsume(queue: "task_queue",
                        noAck: false,
                        consumer: consumer);

                    Console.WriteLine("Press [Enter] to exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
