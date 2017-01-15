using System.Linq.Expressions;

namespace OJS.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;

    using RestSharp;

    public sealed class MailgunMailSender : MailSender
    {
        private const string SendFrom = "campbgjusttest@gmail.com";
        private const string Domain = "sandbox3436a65b89634d5588e1e0ac91349df4.mailgun.org";
        private const string SendFromName = "Camp.bg";

        private const string BaseUrl = "https://api.mailgun.net/v2";
        private const string Password = "56Bu]u7[3295#yn";

        private const string ServerAddress = "smtp.gmail.com";
        private const int ServerPort = 587;

        private static readonly object SyncRoot = new object();

        private static MailSender instance;
        private readonly SmtpClient mailClient;

        private IRestClient client;

        private MailgunMailSender()
        {
            this.client = new RestClient
            {
                BaseUrl = BaseUrl,
                Authenticator =
                    new HttpBasicAuthenticator("api", "key-6b5d66574d1364ef9c5e09be6f993394")
            };
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
                            instance = new MailgunMailSender();
                        }
                    }
                }

                return instance;
            }
        }

        public override void SendMailAsync(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {
            var request = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);

            var res = this.client.ExecuteAsync(request, response => { });
            var webrequestresponse = res.WebRequest.GetResponse();
        }

        public override void SendMail(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients = null)
        {

            var request = this.PrepareMessage(recipient, subject, messageBody, bccRecipients);

            var response = this.client.Execute(request);

            return;
        }

        private IRestRequest PrepareMessage(string recipient, string subject, string messageBody, IEnumerable<string> bccRecipients)
        {
            var request = new RestRequest();
            request.AddParameter("domain", Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", string.Format("Camp.bg<admin@{0}>", Domain));
            request.AddParameter("to", recipient);
            if (bccRecipients != null)
            {
                request.AddParameter("bcc", string.Join(",", bccRecipients));
            }

            request.AddParameter("subject", subject);
            request.AddParameter("html", messageBody);
            request.Method = Method.POST;
            return request;
        }
    }
}