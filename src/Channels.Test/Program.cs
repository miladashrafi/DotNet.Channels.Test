using Channels.Test;

var channel = new AvailableChannel();

channel.BeginRead();
var index = 0;

foreach (var item in new[] {"A", "B", "C"})
{
    for (int i = 0; i < 10; i++)
    {
        index++;
        await channel.Write($"{index}-{item}-{i+1}");
    }
}

Console.WriteLine("Complete!");


Console.ReadLine();
