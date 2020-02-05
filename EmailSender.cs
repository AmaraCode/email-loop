using System;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Collections.Generic;




namespace EmailLoop
{

    /// <summary>
    /// 
    /// </summary>
    public class EmailSender
    {

        /// <summary>
        /// 
        /// </summary>
        public EmailSender()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public void SendEmail(SmtpServer server, string emailAddress, string subject, string message)
        {

            var mimeMessage = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            // from
            mimeMessage.From.Add(new MailboxAddress("Admin", server.UserName));
            // to
            mimeMessage.To.Add(new MailboxAddress(emailAddress, emailAddress));
            // reply to
            //message.ReplyTo.Add(new MailboxAddress("reply_name", "reply_email@example.com"));

            mimeMessage.Subject = subject;
            bodyBuilder.HtmlBody = message;
            bodyBuilder.TextBody = message;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.Connect(server.Host, server.Port, SecureSocketOptions.Auto);
            client.Authenticate(server.UserName, server.Secret);
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}