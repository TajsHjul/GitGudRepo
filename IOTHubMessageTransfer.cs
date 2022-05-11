using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace RasbPiProject
{
    class IOTHubMessageTransfer
    {
        private static readonly string connectionString = "HostName=Blazer.azure-devices.net;DeviceId=RaspberryPi;SharedAccessKey=3Og2iWWINkWSgIG8nQTi5msnA4jP1vFs+nVW/WChvIs=";
        private static DeviceClient deviceClient;

        static void MessageSender()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            ReceiveCloudToDeviceMessageAsync();
            SendDeviceToCloudMessageAsync();


        }

        private static async void SendDeviceToCloudMessageAsync()
        {
            var messageString = "Random test message";
            Message message = new Message(Encoding.ASCII.GetBytes(messageString));            

            await deviceClient.SendEventAsync(message);
            Console.WriteLine("Sending Message {0}", messageString);

        }

        private static async void ReceiveCloudToDeviceMessageAsync()
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
                }

            }
        }
    }
}
