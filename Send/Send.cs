using System.Text;
using RabbitMQ.Client;

public class Program
{
    private static void Main(string[] args)
    {
        var facory = new ConnectionFactory() { HostName = "localhost" };

        using(var connection = facory.CreateConnection())
        {
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange:"logs", type: ExchangeType.Fanout);
                string message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);
                Console.Write($"[x] Send {message}");

            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }

    private static string GetMessage(string[] args)
    {
        return args.Length > 0 ? string.Join("", args) : "info: Hola Mundo II";

    }
}