using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using log4net;
using log4net.Config;

namespace SumApp
{
    public class Program
    {
        private static readonly string _fileName = "Numbers.txt";
        private static readonly string _resultsFileName = "Results.txt";
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            // Load configuration
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("Log4net.config"));

            // Run program
            var numbers = ReadFile(Path.Combine(Environment.CurrentDirectory, _fileName));

            if(numbers?.Count > 0)
            {
                var sum = SumNumbers(numbers);
                _log.Info($"Sum of numbers: {sum}");

                PrintResults(Path.Combine(Environment.CurrentDirectory, _resultsFileName), numbers, sum);
            }
            Console.ReadKey();
        }
        public static int SumNumbers(List<int> numbers)
        {
            return numbers.Sum();
        }
        public static void PrintResults(string path, List<int> numbers, int result)
        {
            using (var writer = File.AppendText(path))
            {
                writer.WriteLine($"Sum of({string.Join(", ", numbers)}): {result}");
            }
        }
        private static List<int> ReadFile(string path)
        {
            var numbers = new List<int>();

            try
            {
                if (!File.Exists(path))
                    throw new Exception("File does not exist");

                using (var reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(',');

                        foreach (var value in values)
                        {
                            var number = 0;

                            if (int.TryParse(value.Trim(), out number))
                            {
                                numbers.Add(number);
                            }
                            else
                            {
                                var message = $"\"{value}\" is not a number and was ignored";
                                _log.Error(message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }

            return numbers;
        }
    }
}
