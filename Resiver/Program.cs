using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//2024-10-14 exchange
/*
Direct => ارسال به صف بطور مستقیم
Fanout => پیام به همه ارسال میشه
Topic => بر اساس الگو تصمیم میگیره به کدام الگو پیام ارسال کنه
Headers => براساس پارامتر هایی که در هدر و کیو قرار میدیم مشخص میکنیم پیام به کجا ارسال بشه   
*/


//2024-08-21
var factory = new ConnectionFactory { HostName = "localhost" };

//Create connection
var connection = factory.CreateConnection();

//Create channel
var channel = connection.CreateModel();
var queueName = "MyQueue01";
//02 create queue
channel.QueueDeclare(queueName, true, false, false, null);
//03 => how read message from queue
channel.BasicQos(0, 1, false);
//create consumer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, eventArg) =>
{
    var body = eventArg.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    //01
    var random = new Random();
    int sleep = random.Next(0, 3) * 1000;
    Console.WriteLine($"sleep => {sleep} || DeliveryTag => {eventArg.DeliveryTag}");
    Thread.Sleep(sleep);

    Console.WriteLine($"Resived message => {message}");

    channel.BasicAck(eventArg.DeliveryTag, true);
};

channel.BasicConsume(queueName, false, consumer);


// See https://aka.ms/new-console-template for more information
Console.WriteLine("-----------------");
Console.ReadKey();
/*
////Before 38
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };

//Create connection
var connection = factory.CreateConnection();

//Create channel
var channel = connection.CreateModel();
var queueName = "MyQueue01";
channel.QueueDeclare(queueName, false, false, false, null);

//create consumer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArg) =>
{
    var body = eventArg.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Resived message => {message}");
    Thread.Sleep(1000);
};

channel.BasicConsume(queueName, true, consumer);


// See https://aka.ms/new-console-template for more information
Console.WriteLine("-----------------");
Console.ReadKey();

*/
