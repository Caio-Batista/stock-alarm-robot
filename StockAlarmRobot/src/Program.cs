using System;
using System.Collections.Generic;
using System.IO;
using StockAlarmRobotTests;
using StockAlarmRobot.src;

namespace StockAlarmRobot
{
    public class Program
    {
        static IFileReader fileReader;
        static CustomSmtpClient customClient;
        public Program(IFileReader reader)
        {
            fileReader = reader;
        }

        public static void Main(string[] args)
        {
            
            Console.WriteLine("Starting stock alarm robot...");


            Dictionary<string, string> configs = getRobotConfigs();
           

            if (!validateConfigs(configs))
            {
                Console.WriteLine("Error validating file");
                Environment.Exit(1);
            }

            Console.WriteLine("Creating client SMTP");

            customClient = new CustomSmtpClient(configs);

            Console.WriteLine("Created client SMTP successfuly");

            // customClient.sendMessage("Changing file name", "hey hey");


        }


        private static Dictionary<string, string> getRobotConfigs()
        {
            var fullPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.
               Parent.FullName, Constants.FILE_NAME);
            string shortPath = Constants.FILE_NAME;

            Console.WriteLine(fullPath);


            string[] configs;
            try
            {
                configs = File.ReadAllLines(shortPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "\nWarning: Error reading file, fallback to full path..."
                );

                try
                {
                    configs = File.ReadAllLines(fullPath);
                }
                catch (Exception)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(
                        "\nError could not find file, did you spell it right?"
                    );
                    configs = new string[] { };
                    Environment.Exit(1);
                }

            }

            var mapConfigs = new Dictionary<string, string>();
            foreach (var line in configs)
            {
                string[] subs = line.Split('=');
                mapConfigs[subs[0]] = subs[1];
            }


            return mapConfigs;
        }

        private static bool validateConfigs(Dictionary<string, string> mapConfigs)
        {
            
            List<string> keyList = new List<string>(mapConfigs.Keys);
            bool isValid = true;

            foreach (KeyValuePair<string, string> kvp in mapConfigs)
            {
                if (string.IsNullOrEmpty(kvp.Value) || string.IsNullOrWhiteSpace(kvp.Value))
                {
                    isValid = false;
                }
                     
            }


            foreach (string item in Constants.CONFIG_KEYS)
            {
                if (!keyList.Contains(item))
                {
                    isValid = false;
                }

            }

            return isValid;

        }

    }
}
