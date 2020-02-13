using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static EmailLoop.Statics;

namespace EmailLoop
{

    /// <summary>
    /// This little engine class does most of the logic work.
    /// In a real project we'd be using interfaces
    /// </summary>
    public class Engine
    {

        private EmailSender _sender;  //the class that sends the email


        /// <summary>
        /// 
        /// </summary>
        public Engine()
        {
            _sender = new EmailSender();
        }



        /// <summary>
        /// This is the command to actuatlly send emails
        /// </summary>
        public void SendEmail()
        {

            //prompt user for setup information
            EmailConfig ec = PromptUserForInput();




            SmtpServer server = Statics.Servers[ec.Server];
            if (server == null)
            {
                Display("Cannot located Server....", true, ConsoleColor.Red);
                PressAnyKey();
            }


            //ask if they really want to send
            //create random 4 digit pin code
            var rnd = new Random();
            var pin = rnd.Next(1000, 9999).ToString();
            var userPin = GetUserInput($"\nType the number {pin} to start sending.  \nAny other number will abort sending:", ConsoleColor.White, ConsoleColor.Green);
            if (pin != userPin)
            {
                Display("Send email aborted.", true, ConsoleColor.Red);
                PressAnyKey();
                return;
            }

            try
            {
                //cycle for only the intervals set
                for (int x = 1; x <= ec.Interval; x++)
                {
                    //determin if we are sending to the entire list or just one address
                    if (ec.EmailAddress != "" && ec.EmailAddress != null)
                    {
                        SendEmail(server, ec.EmailAddress, ec.Subject, ec.Message);
                    }
                    else //send to everyone
                    {
                        //cycle through the list and kindly give each person an email.
                        foreach (string email in Statics.Emails)
                        {
                            SendEmail(server, email, ec.Subject, ec.Message);
                        }
                    }

                    //make the system wait so many seconds
                    if (x != ec.Interval)
                    {
                        Thread.Sleep(ec.Delay);
                    }

                }
            }
            catch (Exception ex)
            {
                Display(ex.Message, true, ConsoleColor.Red);
                PressAnyKey();
                return;
            }


            Display("Sending email complete....", true);
            PressAnyKey();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private EmailConfig PromptUserForInput()
        {
            var ec = EmailConfig.CreateNew();
            bool success = false;
            Console.Clear();


            //valid check the servers.  if none exist then we cannot continue.
            if (Statics.Servers.Count == 0)
            {
                Display("There are no servers configured.  Cannot continue.", true, ConsoleColor.Red);
                PressAnyKey();
                return null;
            }


            //prompt for server
            while (!success)
            {

                Display("Available Servers", true);
                Display(new string('-', 30), true);
                foreach (KeyValuePair<string, SmtpServer> item in Statics.Servers)
                {
                    Display(item.Key, true, ConsoleColor.Yellow);
                }
                System.Console.WriteLine("");
                ec.Server = GetUserInput("Enter Server:", ConsoleColor.White, ConsoleColor.Green);

                if (Statics.Servers.ContainsKey(ec.Server) == true)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }

            //prompt for EmailAddress
            ec.EmailAddress = GetUserInput("Enter email adress or leave blank for all configured addresses:",
            ConsoleColor.White, ConsoleColor.Green);

            //prompt for Interval
            ec.Interval = GetUserInput<int>($"Enter Intervals [Default={ec.Interval}]:", ConsoleColor.White, ConsoleColor.Green);

            //prompt for Delay
            ec.Delay = GetUserInput<int>($"Enter Delay (milliseconds) [Default={ec.Delay}]:", ConsoleColor.White, ConsoleColor.Green);

            //prompt for Subject
            ec.Subject = GetUserInput("Enter Subject:", ConsoleColor.White, ConsoleColor.Green);

            //prompt for Message
            ec.Message = GetUserInput("Enter Meessage:", ConsoleColor.White, ConsoleColor.Green);

            return ec;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Engine CreateNew()
        {
            return new Engine();
        }




        /// <summary>
        /// The method that will actually call the method in the sender class
        /// </summary>
        /// <param name="server"></param>
        /// <param name="emailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        private void SendEmail(SmtpServer server, string emailAddress, string subject, string message)
        {
            _sender.SendEmail(server, emailAddress, subject, message);
            Statics.Display($"Email sent: {emailAddress}", true);
        }



    }

}