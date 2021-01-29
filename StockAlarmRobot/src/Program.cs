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
            
            if (args.Length < 1)
            {
                Console.WriteLine("ERROR: Missing arguments");
                Environment.Exit(1);
            }

            string path = args[0].ToString();
            Console.WriteLine(path);


            string[] configs;
            try
            {
                configs = File.ReadAllLines(path);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(
                    "\nError reading file, did you spell it correctly?"
                );
                configs = new string[] { };
                Environment.Exit(1);
            }
            
            var mapConfigs = new Dictionary<string, string>();

            foreach (var line in configs)
            {
                string[] subs = line.Split('=');
                mapConfigs[subs[0]] = subs[1];
            }

            if (!validateConfigs(mapConfigs))
            {
                Console.WriteLine("Error validating file");
                Environment.Exit(1);
            }

            Console.WriteLine("Creating client SMTP");

            customClient = new CustomSmtpClient(mapConfigs);

            Console.WriteLine("Created client SMTP successfuly");


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

        public static string getCurrentPath()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.
                Parent.FullName;
        }


    }
}
