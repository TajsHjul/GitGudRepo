using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace RasbPiProject
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            IOTHubMessageTransfer Test = new IOTHubMessageTransfer();
            Test.MegaMessageSender();
            Test.MessageReciever();
            //Console.WriteLine(Test.ShowMessage());
            Console.ReadLine();
        }
        
    }
}