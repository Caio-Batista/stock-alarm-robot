using System;
namespace StockAlarmRobot.src
{
    public class Constants
    {
        public static string[] CONFIG_KEYS = {
            "SMTP_ENDPOINT",
            "SMPT_CODE",
            "RECEIVER_EMAIL",
            "SENDER_EMAIL",
            "SENDER_PASS",
            "RECEIVER_NAME",
            "CLIENT_TIMEOUT"
        };
        public static string FILE_NAME = "default.robot.config";
    }
}
