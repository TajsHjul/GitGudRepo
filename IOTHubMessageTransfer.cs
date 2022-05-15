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
        private static readonly string connectionString = "HostName=RasPiProject.azure-devices.net;DeviceId=RasPi;SharedAccessKey=H1pSFiQ7WHUnwZrme+nnw+hqSROzBNeyGdFwejDCsJ0=";
        private static DeviceClient deviceClient;
        public static string recievedString = "";

        public string ShowMessage()
        {
            return recievedString;
        }
        public void MessageSender()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            
            SendDeviceToCloudMessageAsync(TestDataGen.TestDataStringGen());
            Console.WriteLine("Message sent");

        }
        public void MegaMessageSender()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            for (int i = 0; i < 25; i++) 
            {
            SendDeviceToCloudMessageAsync(TestDataGen.TestDataStringGen());
            Console.WriteLine("Message sent");
            }

        }
        public void MessageReciever()
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            ReceiveCloudToDeviceMessageAsync();
            Console.WriteLine("Message sent");
        }

        private static async void SendDeviceToCloudMessageAsync(string msgstring)
        {
            var messageString = msgstring;
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
