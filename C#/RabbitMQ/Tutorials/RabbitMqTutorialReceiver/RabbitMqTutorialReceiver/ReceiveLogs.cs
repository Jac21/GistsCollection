using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqTutorialReceiver
{
    class ReceiveLogs
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
                    // Declare an exchange, receives messages from producers and 
                    // pushes them to queues, knows what to do with messages received
                    channel.ExchangeDeclare("logs", "fanout"); // just broadcasts all the messages it receives to all the queues it knows.

                    // random queue name
                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue:queueName,
                                      exchange:"logs",
                                      routingKey:"");

                    Console.WriteLine("[*] Waiting for logs...");

                    // Push messages from the queue asynchronously
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[X] Received {0}", message);
                    };

                    channel.BasicConsume(queue: queueName,
                        noAck: true,
                        consumer: consumer);

                    Console.WriteLine("Press [Enter] to exit");
                    Console.ReadLine();
                }

                /*
                 * If you want to save logs to a file, just open a console and type:
                 * $ ReceiveLogs.exe > logs_from_rabbit.log
                 * */
            }
        }
    }
}
