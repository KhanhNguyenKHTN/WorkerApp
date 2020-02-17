using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WorkerModel.Order;
using WorkerApp.Global;
using Xamarin.Forms;

namespace WorkerApp.Services
{
    public class RabbitConnect
    {
        public event EventHandler<EventArgs> ReceiverOderDetail;
        public void ReceiveNotifyRabbitMQ()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.UserName = GlobalInfo.UserName;
                factory.Password = GlobalInfo.Password;
                factory.VirtualHost = GlobalInfo.VirtualHost;
                factory.HostName = GlobalInfo.HostName;
                IConnection conn = factory.CreateConnection();

                var channel = conn.CreateModel();

                var consumer = new EventingBasicConsumer(channel);

                Console.WriteLine("connecting to listen");
                consumer.Received += (model, ea) =>
                {
                    var tag = ea.RoutingKey;
                    if(tag == GlobalInfo.UserLogin)
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        if (message != null)
                        {
                            var customer = JsonConvert.DeserializeObject<OrderDetail>(message);

                            ReceiverOderDetail?.Invoke(customer, null);
                        }
                    }
                };
                channel.BasicConsume(queue: "cook01",
                                     autoAck: true,
                                     consumer: consumer);
                channel.BasicConsume(queue: "cook02",
                                     autoAck: true,
                                     consumer: consumer);
                channel.BasicConsume(queue: "cook03",
                                     autoAck: true,
                                     consumer: consumer);

            }
            catch
            {
                Console.WriteLine("Loi roai");
            }
        }
    }
}
