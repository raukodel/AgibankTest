using System;
using System.Collections.Generic;
using System.IO;

namespace FileWatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher watcher;
            watcher = new FileSystemWatcher("data/in", "*.*");
            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Listening for new files!");

            Console.ReadKey();
        }

        public async static void OnCreated(object source, FileSystemEventArgs e)
        {
            DataExtractor dataExtractor = new DataExtractor();
            string fileData;
            int salesmanCount = 0;
            int clientsCount = 0;
            int saleIDHighest = 0;
            string worstSalesman = "";
            List<string> newData = new List<string>();

            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

            Console.ForegroundColor = ConsoleColor.Blue;
            try
            {
                using (StreamReader sr = new StreamReader($"data/in/{e.Name}"))
                {
                    //Read data
                    fileData = await sr.ReadToEndAsync();
                    Console.WriteLine(fileData);
                    Console.WriteLine("");

                    //Salesmans count
                    salesmanCount = dataExtractor.GetSalesmansCount(fileData);
                    newData.Add(salesmanCount.ToString());
                    Console.WriteLine($"Salesman Count: {salesmanCount}");

                    //Clients count
                    clientsCount = dataExtractor.GetClientsCount(fileData);
                    newData.Add(clientsCount.ToString());
                    Console.WriteLine($"Clients Count: {clientsCount}");

                    //Sales ID highest price
                    saleIDHighest = dataExtractor.GetIDSalesHighestPrice(fileData);
                    Console.WriteLine($"Highest Sale: {saleIDHighest}");
                    newData.Add(saleIDHighest.ToString());

                    //Worst salesman
                    worstSalesman = dataExtractor.GetWorstSalesman(fileData);
                    Console.WriteLine($"Worst Salesman: {worstSalesman}");
                    newData.Add(worstSalesman);

                    //Save data
                    System.IO.File.WriteAllLines(@"data/out/data.txt", newData);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}