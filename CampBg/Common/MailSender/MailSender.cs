namespace OJS.Common
{
    using System.Collections.Generic;

    public abstract class MailSender
    {
        public static MailSender Instance { get; set; }

        public abstract void SendMailAsync(
            string recipient,
            string subject,
            string messageBody,
            IEnumerable<string> bccRecipients = null);

        public abstract void SendMail(
            string recipient,
            string subject,
            string messageBody,
            IEnumerable<string> bccRecipients = null);
    }
}
