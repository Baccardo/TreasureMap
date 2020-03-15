using System;
using System.IO;
using TreasureMap.Business;
using TreasureMap.Data;

namespace TreasureMap.Application
{
    class Program
    {
        static void Main()
        {
            string mapFilePath = string.Empty;
            while (!File.Exists(mapFilePath))
            {
                Console.WriteLine(Messages.EnterMapFilePath);
                mapFilePath = Console.ReadLine();
            }
            (bool dataAreValid, string msg) = FileManagement.DataLinesAreValid(mapFilePath);
            if (dataAreValid)
            {
                msg = MapManagement.Process(mapFilePath);
            }
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }
}
