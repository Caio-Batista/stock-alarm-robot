using System;
namespace StockAlarmRobotTests
{
    public interface IFileReader
    {
        string[] ReadAllLines(string path);
    }
}
