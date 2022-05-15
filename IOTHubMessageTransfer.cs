using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace RasbPiProject
{
    class IOTHubMessageTransfer
    {
        private static readonly string connectionString = "HostName=RasPiProject.azure-devices.net;DeviceId=RasPi;SharedAccessKey=H1pSFiQ7WHUnwZrme+nnw+hqSROzBNeyGdFwejDCsJ0=";
        private static DeviceClient deviceClient;
        public static string recievedString = "";
        private static int _messageId = 1;
        private static readonly Random Rand = new Random();
        public string ShowMessage()
        {
            return recievedString;
        }
        public void MessageSender()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            
            SendDeviceToCloudMessageAsync();
            Console.WriteLine("Message sent");

        }
        public void MegaMessageSender()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            for (int i = 0; i < 25; i++) 
            {
            SendDeviceToCloudMessageAsync();
            Console.WriteLine("Message sent");
            Thread.Sleep(10000);
            }

        }
        public void MessageReciever()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            ReceiveCloudToDeviceMessageAsync();
            Console.WriteLine("Message sent");
        }

        private static async void SendDeviceToCloudMessageAsync()
        {
            var currentFrequency = Rand.NextDouble() * 15;
            var currentWattage = Rand.NextDouble() * 20;
            var currentFlow = Rand.NextDouble() * 20;
            var telemetryDataPoint = new
                {
                    messageId = _messageId++,
                    deviceId = "PumpItUp",
                    frequency = currentFrequency,
                    wattage = currentWattage,
                    flow = currentFlow
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            
            Message message = new Message(Encoding.ASCII.GetBytes(messageString));            
            
            await deviceClient.SendEventAsync(message);
            Console.WriteLine("Sending Message {0}", messageString);

        }

        public static async void ReceiveCloudToDeviceMessageAsync()
        {
            Console.WriteLine("Receiving Cloud to Device messages from IoT Hub");
            
            while (true)
            {
                Message receivedMessage = await deviceClient.ReceiveAsync();

                if (receivedMessage != null)
                {
                    string receivedMessageString = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    Console.WriteLine("Received message: {0}", receivedMessageString);
                    await deviceClient.CompleteAsync(receivedMessage);
                    recievedString = receivedMessageString;
                }
                
            }

        }
    }
}
