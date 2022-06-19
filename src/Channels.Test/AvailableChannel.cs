using System.Threading.Channels;

namespace Channels.Test
{
    public interface IAvailableChannel
    {
        public Task Write(string value);
        public void BeginRead();
    }

    public sealed class AvailableChannel : IAvailableChannel
    {
        private readonly Channel<string> _channel = Channel.CreateUnbounded<string>();
        public void BeginRead()
        {
            _ = Task.Factory.StartNew(async () =>
            {
                while (await _channel.Reader.WaitToReadAsync())
                {
                    if (_channel.Reader.TryRead(out var msg))
                        Console.WriteLine(msg);

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            });
        }

        public async Task Write(string value)
        {
            await _channel.Writer.WriteAsync(value);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

    }
}
