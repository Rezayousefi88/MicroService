using RabbitMQ.Client;
using System.Text;

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
    if (i > 500)
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

