using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace RasbPiProject
{
    static class TestDataGen
    {
        public static string TestDataStringGen()
        {
            var rand = new Random();
            int[] testdata = new int[3];
            string datastring;
            for (int ctr = 0; ctr < 3; ctr++)
                testdata[ctr] = rand.Next(999);
            datastring = "Flow:" + testdata[0].ToString()+"m3/hr,"+
                           "Frequency:" + testdata[1].ToString() + "hertz," +
                              "Wattage:" + testdata[2].ToString() + "watts";
            return datastring;
        }
        /*
        public static string FlowTestDataGen()
        {
            

        }
        public static string FreqTestDataGen()
        {
            

        }
        public static string WattageTestDataGen()
        {
            

        }
       */
    }
}
