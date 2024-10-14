using RabbitMQ.Client;
using System.Text;

/*
===> Direct => ارسال به صف بطور مستقیم
Fanout => پیام به همه ارسال میشه
Topic => بر اساس الگو تصمیم میگیره به کدام الگو پیام ارسال کنه
Headers => براساس پارامتر هایی که در هدر و کیو قرار میدیم مشخص میکنیم پیام به کجا ارسال بشه   
*/
var factory = new ConnectionFactory { HostName = "localhost" };

var connection = factory.CreateConnection();

var channel = connection.CreateModel();
var queueName = "Create.Order";
channel.QueueDeclare(queueName, false, false, false, null);
string message = $"this is a test meaasge from my sender at{DateTime.Now}";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("", queueName, null, body);
 

/*
////before 41
 //2024-08-21
//access to rabbitMQ
//var factory = new ConnectionFactory();
//factory.Uri = new Uri("amqp://guest:guest@localhost:15672");
var factory = new ConnectionFactory { HostName = "localhost" };

//Create connection
var connection = factory.CreateConnection();

//Create channel
var channel = connection.CreateModel();
//crete Queue
var queueName = "MyQueue01";
//<param name="durable">Should this queue will survive a broker restart?</param>
channel.QueueDeclare(queueName, true, false, false, null);

int i = 1;
var action = true;
while (action)
{
    string message = $"this is a test meaasge{i++} from my sender at{DateTime.Now}";
    var body = Encoding.UTF8.GetBytes(message);
    //save messages in disc
    var properties = channel.CreateBasicProperties();
    properties.Persistent = true;

    channel.BasicPublish("", queueName, properties, body);
    if (i > 100)
    {
        action = false;
    }
}
//for (int i = 0; i < 20; i++)
//{
//    string message = $"this is a test meaasge{i} from my sender at{DateTime.Now}";
//    var body = Encoding.UTF8.GetBytes(message);
//    //save messages in disc
//    var properties = channel.CreateBasicProperties();
//    properties.Persistent = true;

//    channel.BasicPublish("", queueName, properties, body);
//}
channel.Close();
connection.Close();
 */


/*
////before 38
//access to rabbitMQ
//var factory = new ConnectionFactory();
//factory.Uri = new Uri("amqp://guest:guest@localhost:15672");
var factory = new ConnectionFactory { HostName = "localhost" };

//Create connection
var connection = factory.CreateConnection();

//Create channel
var channel = connection.CreateModel();
//crete Queue
var queueName = "MyQueue01";
//<param name="durable">Should this queue will survive a broker restart?</param>
channel.QueueDeclare(queueName, true,false,false, null);
for (int i = 0; i < 20; i++)
{
    string message = $"this is a test meaasge{i} from my sender at{DateTime.Now}";
    var body = Encoding.UTF8.GetBytes(message);
    //save messages in disc
    var properties = channel.CreateBasicProperties();
    properties.Persistent = true;

    channel.BasicPublish("", queueName, properties, body);
}
channel.Close();
connection.Close();
 */

