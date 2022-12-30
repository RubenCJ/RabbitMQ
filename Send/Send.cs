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
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete:false, arguments: null);
                string message = "Hola Ruben!";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.Write($"[x] Send {message}");

            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}