using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            ManagementObject[] objects = searcher.Get().Cast<ManagementObject>().ToArray();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            Console.WriteLine("Диск SSD");
            Console.WriteLine("Модель: " + objects[0]["Model"]);
            Console.WriteLine("Изготовитель: " + objects[0]["Manufacturer"]);
            Console.WriteLine("Серийный номер: " + objects[0]["SerialNumber"].ToString().Replace(" ", ""));
            Console.WriteLine("Версия прошивки: " + objects[0]["FirmwareRevision"]);
            Console.WriteLine("Свободно: " + (allDrives[0].AvailableFreeSpace + allDrives[0].AvailableFreeSpace) / (1024 * 1024 * 1024) + " ГБ");
            Console.WriteLine("Занято: " + (allDrives[0].TotalSize + allDrives[0].TotalSize - allDrives[0].AvailableFreeSpace - allDrives[0].AvailableFreeSpace) / (1024 * 1024 * 1024) + " ГБ");
            Console.WriteLine("Всего: " + (allDrives[0].TotalSize + allDrives[0].TotalSize) / (1024 * 1024 * 1024) + " ГБ");
            Console.WriteLine("Тип интерфейса: " + objects[0]["InterfaceType"]);
            //Console.WriteLine("Возможности: " 
            //+ (entity["CapabilityDescriptions"] as IEnumerable<string>).Aggregate((l, r) => $"{l.ToString()}, {r.ToString()}"));

             Console.ReadKey();
        }
    }
}
