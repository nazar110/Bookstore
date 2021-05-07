using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage
using Newtonsoft.Json.Linq;

namespace Bookstore.Core.Helpers
{
    public class QueueStorageHelper
    {
        private string connectionString = (string)JObject.Parse("appsetting.json")["ConnectionStrings"]["StorageConnectionString"];
        public void DequeueMessages(string queueName, int lastNMessages)
        {
            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Receive and process 20 messages
                QueueMessage[] receivedMessages = queueClient.ReceiveMessages(lastNMessages, TimeSpan.FromMinutes(5));

                foreach (QueueMessage message in receivedMessages)
                {
                    // Delete the message
                    queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
                }
            }
        }
        public string PeekLastMessage(string queueName)
        {
            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Peek at the next message
                PeekedMessage[] peekedMessages = queueClient.PeekMessages();

                // Display the message
                Console.WriteLine($"Peeked message: '{peekedMessages[0].MessageText}'");
                return peekedMessages.Last().MessageText;
            }
            return null;
        }
        public void InsertMessage(string queueName, string message)
        {
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                queueClient.SendMessage(message);
            }
        }
    }
}
