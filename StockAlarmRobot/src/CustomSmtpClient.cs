using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace StockAlarmRobot.src
{
    public class CustomSmtpClient
    {
  
        private static SmtpClient client;
        private static Dictionary<string, string> storedConfigs;

        public CustomSmtpClient(Dictionary<string, string> configs)
        {
            storedConfigs = configs;
            client = new SmtpClient(
               configs[Constants.CONFIG_KEYS[0]],
               Int32.Parse(configs[Constants.CONFIG_KEYS[1]])
            );
            client.EnableSsl = true;
            client.Timeout = Int32.Parse(configs[Constants.CONFIG_KEYS[6]]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(
                configs[Constants.CONFIG_KEYS[3]],
                configs[Constants.CONFIG_KEYS[4]]
            );
        }

        public void sendMessage(string subject, string msgBody)
        {
            MailMessage message = new MailMessage();
            message.To.Add(storedConfigs[Constants.CONFIG_KEYS[2]]);
            message.From = new MailAddress(
               storedConfigs[Constants.CONFIG_KEYS[3]]
            );
            message.Subject = subject;
            message.Body = msgBody;
            client.Send(message);

        }

    }
}
