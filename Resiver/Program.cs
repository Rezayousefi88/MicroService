using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };

//Create connection
var connection = factory.CreateConnection();

//Create channel
var channel = connection.CreateModel();
var queueName = "MyQueue01";
channel.QueueDeclare(queueName, true, false, false, null);

//create consumer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, eventArg) =>
{
    var body = eventArg.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Resived message => {message}");
    Thread.Sleep(1);
    channel.BasicAck(eventArg.DeliveryTag, true );
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
