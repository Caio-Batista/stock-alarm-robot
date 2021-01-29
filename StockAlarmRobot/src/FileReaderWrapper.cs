using System.IO;
using StockAlarmRobotTests;

namespace StockAlarmRobot.src
{
    public class FileReaderWrapper : IFileReader
    {
        public FileReaderWrapper()
        {
        }

        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
