using System;
using System.Collections.Generic;
using System.IO;
using StockAlarmRobotTests;

namespace StockAlarmRobot
{
    public class Program
    {
        static IFileReader fileReader;
        public Program(IFileReader reader)
        {
            fileReader = reader;
        }

        public static void Main(string[] args)
        {
            
            Console.WriteLine("Starting stock alarm robot...");

            if (args.Length < 1)
            {
                Environment.Exit(1);
            }

            string path =
                Path.Combine(getCurrentPath(), args[0].ToString());


            string[] configs;
            try
            {
                configs = File.ReadAllLines(path);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                configs = new string[] { };
            }
            
            var mapConfigs = new Dictionary<string, string>();
            foreach (var line in configs)
            {
                string[] subs = line.Split('=');
                mapConfigs[subs[0]] = subs[1];
            }

            if (!validateConfigs(mapConfigs))
            {
                Console.WriteLine("Error validating configs");
                Environment.Exit(1);
            }


            foreach (KeyValuePair<string, string> kvp in mapConfigs)
            { 
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }


        }

        private static bool validateConfigs(Dictionary<string, string> mapConfigs)
        {
            string[] fields = {
                "SMTP_ENDPOINT",
                "SMPT_CODE",
                "RECEIVER_EMAIL",
                "SENDER_EMAIL",
                "SENDER_PASS",
                "SENDER_NAME"
            };
            List<string> keyList = new List<string>(mapConfigs.Keys);
            bool isValid = true;

            foreach (KeyValuePair<string, string> kvp in mapConfigs)
            {
                if (string.IsNullOrEmpty(kvp.Value) || string.IsNullOrWhiteSpace(kvp.Value))
                {
                    isValid = false;
                }
                     
            }


            foreach (var item  in fields)
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
