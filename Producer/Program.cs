using RabbitMQ.Client;

namespace Producer
{
	public static class Program
	{
		static Program()
		{
		}

		/// <summary>
		/// http://localhost:15672
		/// </summary>
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
						// نکته مهم
						// در صورتی که یک
						// Queue
						// قبلا با حالت
						// durable: false
						// ایجاد شده و وجود داشته باشد
						// اگر همان
						// Queue
						// را با حالت
						// durable: true
						// Declare
						// نماییم، با خطای ذیل مواجه خواهیم شد
						// The AMQP operation was interrupted: AMQP close-reason,
						// initiated by Peer, code=406, text='PRECONDITION_FAILED
						// - inequivalent arg 'durable' for queue 'TestQueue'
						// in vhost '/': received 'true' but current
						// is 'false'', classId=50, methodId=10
						channel.QueueDeclare
							(queue: queueName,
							durable: true,
							exclusive: false,
							autoDelete: false,
							arguments: null);

						string message = "Hello, World!";

						byte[] body =
							System.Text.Encoding.UTF8.GetBytes(message);

						//channel.BasicPublish
						//	(exchange: exchangeName,
						//	routingKey: queueName,
						//	mandatory: false,
						//	basicProperties: null,
						//	body: body);

						// using RabbitMQ.Client;
						channel.BasicPublish
							(exchange: exchangeName,
							routingKey: queueName,
							basicProperties: null,
							body: body);

						System.Console.WriteLine($"Message [{ message }] Sent...");
					}
				}
			}
			catch (System.Exception ex)
			{
				System.Console.WriteLine(ex.Message);
			}

			System.Console.Write("Press [ENTER] to exit the Producer (Sender) App...");
			System.Console.ReadLine();
		}
	}
}
