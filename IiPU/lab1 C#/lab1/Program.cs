using System;
using System.Management;


namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementObjectSearcher searcher =                     
            new ManagementObjectSearcher("root\\CIMV2",             // Область, в которой необходимо выполнить запрос.
            "SELECT * FROM Win32_PnPEntity");                       // Задаем вызываемый запрос

            int numDevice = 0;

            foreach (ManagementObject queryObj in searcher.Get())   // Получение результирующей коллекции
            {
                var info = (string)(queryObj["DeviceID"]);
                if (info.StartsWith("PCI"))
                {
                    
                    string vendorID = info.Substring(8, 4);
                    string deviceID = info.Substring(17, 4);

                    Console.WriteLine("№{0} VendorID: {1} DeviceID: {2}", ++numDevice, vendorID, deviceID);
                    Console.WriteLine("Description: {0}", queryObj["Description"]);
                }
            }

        }
    }
}
