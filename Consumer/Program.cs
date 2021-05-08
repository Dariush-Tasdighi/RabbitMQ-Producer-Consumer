using RabbitMQ.Client;

namespace Consumer
{
	public static class Program
	{
		static Program()
		{
		}

		public static void Main()
		{
			try
			{
				var factory =
					new RabbitMQ.Client.ConnectionFactory
					{
						Port = 5672,
						UserName = "guest",
						Password = "guest",
						HostName = "localhost",
					};

				//var factory =
				//	new RabbitMQ.Client.ConnectionFactory
				//	{
				//		Uri = new System.Uri(uriString: "amqp://guest:guest@localhost:5672"),
				//	};

				string queueName = "TestQueue";
				string exchangeName = string.Empty;

				using (var connection = factory.CreateConnection())
				{
					using (var channel = connection.CreateModel())
					{
						channel.QueueDeclare
							(queue: queueName,
							durable: true,
							exclusive: false,
							autoDelete: false,
							arguments: null);

						var consumer =
							new RabbitMQ.Client.Events.EventingBasicConsumer(model: channel);

						consumer.Received += (sender, e) =>
						{
							byte[] body = e.Body.ToArray();

							string message =
								System.Text.Encoding.UTF8.GetString(body);

							System.Console.WriteLine($"Message [{ message }] Received...");
						};

						//channel.BasicConsume
						//	(queue: queueName,
						//	autoAck: true,
						//	consumerTag: string.Empty, // null -> Runtime Error!
						//	noLocal: false,
						//	exclusive: false,
						//	arguments: null,
						//	consumer: consumer);

						// using RabbitMQ.Client;
						channel.BasicConsume
							(queue: queueName,
							autoAck: true,
							consumer: consumer);

						// به محل قرارگیری دو دستور ذیل، بر خلاف پروژه قبلی توجه نمایید
						System.Console.WriteLine("Press [ENTER] to exit the Consumer (Receiver) App...");
						System.Console.ReadLine();
					}
				}
			}
			catch (System.Exception ex)
			{
				System.Console.WriteLine(ex.Message);

				System.Console.Write("Press [ENTER] to exit the Consumer (Receiver) App...");
				System.Console.ReadLine();
			}
		}
	}
}

