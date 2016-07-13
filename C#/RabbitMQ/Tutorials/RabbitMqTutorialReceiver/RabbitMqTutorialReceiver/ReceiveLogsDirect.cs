using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqTutorialReceiver
{
    class ReceiveLogsDirect
    {
        static void Main(string[] args)
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
                    channel.ExchangeDeclare("direct_logs", "direct");

                    // random queue name
                    var queueName = channel.QueueDeclare().QueueName;

                    if (args.Length < 1)
                    {
                        Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                            Environment.GetCommandLineArgs()[0]);
                        Console.WriteLine("Press [enter] to exit");
                        Console.ReadLine();
                        Environment.ExitCode = 1;
                        return;
                    }

                    foreach (var severity in args)
                    {
                        channel.QueueBind(queue: queueName,
                            exchange: "direct_logs",
                            routingKey: severity);
                    }

                    Console.WriteLine("[*] Waiting for messages...");

                    // Push messages from the queue asynchronously
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var routingKey = ea.RoutingKey;
                        Console.WriteLine("[X] Received '{0}' : '{1}'", routingKey, message);
                    };

                    channel.BasicConsume(queue: queueName,
                        noAck: true,
                        consumer: consumer);

                    Console.WriteLine("Press [enter] to exit");
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
