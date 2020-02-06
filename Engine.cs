using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace EmailLoop
{

    /// <summary>
    /// This little engine class does most of the logic work.
    /// In a real project we'd be using interfaces
    /// </summary>
    public class Engine
    {

        private EmailSender _sender;  //the class that sends the email
        private FileIO<List<string>, Engine> _file;
        private FileIO<Dictionary<string, SmtpServer>, SmtpServer> _servers;
        private CliConfig _cli;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cli"></param>
        public Engine(CliConfig cli)
        {
            _cli = cli;
            _file = new FileIO<List<string>, Engine>(AppDomain.CurrentDomain.BaseDirectory);
            _servers = new FileIO<Dictionary<string, SmtpServer>, SmtpServer>(AppDomain.CurrentDomain.BaseDirectory);
            _sender = new EmailSender();
            LoadData();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        public void AddEmail(string email)
        {
            if (email != "")
            {
                if (!Statics.Emails.Contains(email))
                {
                    Statics.Emails.Add(email);
                    PersistData();
                    Statics.Display($"Email Added: {email}");
                    Statics.Display($"New Emails Count: {Statics.Emails.Count}");
                }
                else
                {
                    Statics.Display($"Email already exists.");

                }

            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        public void RemoveEmail(string email)
        {
            if (email != "")
            {
                Statics.Emails.Remove(email);
                PersistData();
                Statics.Display($"Email Removed: {email}");
                Statics.Display($"New Emails Count: {Statics.Emails.Count}");

            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void PersistData()
        {
            _file.SaveData(Statics.Emails);
        }



        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            Statics.Emails.Clear();
            PersistData();
            Statics.Display("All emails deleted.");
            Statics.Display($"New Emails Count: {Statics.Emails.Count}");
        }



        /// <summary>
        /// Load up the two needed collections
        /// </summary>
        private void LoadData()
        {
            Statics.Emails = _file.GetData();
            Statics.Servers = _servers.GetData();
        }



        /// <summary>
        /// This is the command to actuatlly send emails
        /// </summary>
        public void Blast()
        {
            SmtpServer server;


            //Ensure we have the server config in the collection 
            if (Statics.Servers.ContainsKey(_cli.Server))
            {
                server = Statics.Servers[_cli.Server];
            }
            else
            {
                Statics.Display("Cannot locate server.. check SmtpServer.json");
                return;
            }


            //cycle for only the intervals set
            for (int x = 1; x <= _cli.Interval; x++)
            {
                //determin if we are sending to the entire list or just one address
                if (_cli.EmailAddress != "" && _cli.EmailAddress != null)
                {
                    SendEmail(server, _cli.EmailAddress, _cli.Subject, _cli.Message);
                }
                else //send to everyone
                {
                    //cycle through the list and kindly give each person an email.
                    foreach (string email in Statics.Emails)
                    {
                        SendEmail(server, email, _cli.Subject, _cli.Message);
                    }
                }

                //make the system wait so many seconds
                Thread.Sleep(_cli.Delay);
            }
        }



        /// <summary>
        /// Simple factory method for convienence
        /// </summary>
        /// <param name="cliConfig"></param>
        /// <returns></returns>
        public static Engine CreateNew(CliConfig cliConfig)
        {
            return new Engine(cliConfig);
        }




        /// <summary>
        /// The method that will actually call the method in the sender class
        /// </summary>
        /// <param name="server"></param>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public void SendEmail(SmtpServer server, string emailAddress, string subject, string message)
        {
            _sender.SendEmail(server, emailAddress, subject, message);
            Statics.Display($"Email sent: {emailAddress}");
        }


        /// <summary>
        /// Simple method that displays the emails in our blast list
        /// </summary>
        public void DisplayList()
        {
            foreach (string email in Statics.Emails)
            {
                System.Console.WriteLine(email);
            }
        }

    }

}