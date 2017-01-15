using System.Configuration;

namespace OJS.Common
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public sealed class SMTPMailSender : MailSender
    {
        private static readonly object SyncRoot = new object();

        private static MailSender instance;
        private readonly SmtpClient mailClient;

        private SMTPMailSender()
        {
            var sendFrom = ConfigurationManager.AppSettings["sendFrom"];
            var password = ConfigurationManager.AppSettings["password"];
            var serverPort = int.Parse(ConfigurationManager.AppSettings["serverPort"]);
            var host = ConfigurationManager.AppSettings["serverAddress"];

            this.mailClient = new SmtpClient
            {
                Credentials = new NetworkCredential(sendFrom, password),
                Port = serverPort,
                Host = host,
                EnableSsl = false,
            };

            // this.mailClient = new SmtpClient { DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis };
        }

        public static MailSender Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SMTPMailSender();
                        }
                    }
                }

                return instance;
            }
        }

        public override void SendMailAsync(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
            this.mailClient.SendAsync(message, null);
        }

        public override void SendMail(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            try
            {
                var message = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);
                this.mailClient.Send(message);
            }
            catch (Exception ex)
            {

            }
        }

        private MailMessage PrepareMessage(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients)
        {
            var sendFrom = ConfigurationManager.AppSettings["sendFrom"];
            var sendFromName = ConfigurationManager.AppSettings["sendFromName"];
            var mailTo = new MailAddress(recipient);
            var mailFrom = new MailAddress(sendFrom, sendFromName);
            var message = new MailMessage(mailFrom, mailTo)
            {
                Body = messageBody,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
            };

            if (bccRecipients != null)
            {
                foreach (var bccRecipient in bccRecipients)
                {
                    message.Bcc.Add(bccRecipient);
                }
            }

            return message;
        }
    }
}