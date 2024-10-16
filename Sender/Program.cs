﻿using RabbitMQ.Client;
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
string exchangeName = "Order";
channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false);

string message = $"this is a test meaasge from my sender at{DateTime.Now}";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchangeName, "", null, body);

Console.ReadLine();



