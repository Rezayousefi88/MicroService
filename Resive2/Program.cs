using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

/*
Direct => ارسال به صف بطور مستقیم
===> Fanout => پیام به همه ارسال میشه
Topic => بر اساس الگو تصمیم میگیره به کدام الگو پیام ارسال کنه
Headers => براساس پارامتر هایی که در هدر و کیو قرار میدیم مشخص میکنیم پیام به کجا ارسال بشه   
*/
var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
var queueName = "fanout.Test2";
string exchangeName = "Order";
channel.QueueDeclare(queueName, false, true, false);
channel.QueueBind(queueName, exchangeName, "");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, eventArg) =>
{
    var body = eventArg.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Resived message => {message}");
};

channel.BasicConsume(queueName, true, consumer);
Console.ReadLine();


