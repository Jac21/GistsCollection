using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace RestfulJobPattern.Services
{
    public class MessageBusService
    {
        private readonly ConcurrentQueue<Message> queue = new ConcurrentQueue<Message>();

        public void SendMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            queue.Enqueue(message);
        }

        public async Task<Message> ReceiveMessage(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (queue.TryDequeue(out var message))
                    {
                        return message;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // return null on cancellation
            }

            return null;
        }
    }
}