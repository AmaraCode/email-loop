using System;
using System.Collections.Generic;
using AmaraCode;

namespace EmailLoop.Menus
{


    /// <summary>
    /// 
    /// </summary>
    public class SmtpServerMenu : IMenu
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SmtpServerMenu CreateNew()
        {
            return new SmtpServerMenu();
        }


        /// <summary>
        /// 
        /// </summary>
        public void Invoke()
        {
            //host, port, username, secret

            bool exit = false;
            while (!exit)
            {
                ShowMenu();

                var result = Statics.GetUserInput("Enter Command: ", ConsoleColor.Blue, ConsoleColor.Green);
                switch (result.ToLower())
                {
                    case "list":
                        ListServers();
                        break;
                    case "remove":
                        RemoveServer();
                        break;
                    case "add":
                        AddServer();
                        break;
                    case "":
                        //ShowMenu();
                        break;

                    case "main":
                        Console.ForegroundColor = ConsoleColor.White;
                        exit = true;
                        break;

                    case "exit":
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }


            }

            /*
            var host = Statics.GetUserInput("Enter Host Name: ");
            var port = Statics.GetUserInput("Enter Port Number:");
            var userName = Statics.GetUserInput("Enter UserName:");
            var secret = Statics.GetUserInput("Enter Password");
            */

        }


        /// <summary>
        /// 
        /// </summary>
        public void AddServer()
        {
            var svr = SmtpServer.CreateNew();
            AmaraCode.Security sec = new Security();
            try
            {
                string serverName = Statics.GetUserInput("Server Name: ", ConsoleColor.Gray, ConsoleColor.Green);
                svr.Host = Statics.GetUserInput("Enter Host: ", ConsoleColor.Gray, ConsoleColor.Green);
                svr.Port = Convert.ToInt32(Statics.GetUserInput("Enter Port: ", ConsoleColor.Gray, ConsoleColor.Green));
                svr.UserName = Statics.GetUserInput("Enter UserName: ", ConsoleColor.Gray, ConsoleColor.Green);
                svr.Secret = sec.EnryptString(Statics.GetUserInput("Enter Secret: ", ConsoleColor.Gray, ConsoleColor.Green));

                Statics.Servers.Add(serverName, svr);
                Statics.PersistSMTPServer();
                Statics.Display($"Server Added: ", false, ConsoleColor.Gray);
                Statics.Display($"{svr.Host}", true, ConsoleColor.Green);
                Statics.Display($"New SMTP Server Count: ", false, ConsoleColor.Gray);
                Statics.Display($"{Statics.Servers.Count}", true, ConsoleColor.Green);
                Statics.PressAnyKey();
            }
            catch
            {
                Statics.Display("Invalid input", true, ConsoleColor.Red);
                Statics.PressAnyKey();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public void RemoveServer()
        {
            string result = Statics.GetUserInput("Enter Host To Remove: ", ConsoleColor.Gray, ConsoleColor.Green);

            if (result != "" && Statics.Servers.ContainsKey(result))
            {
                Statics.Servers.Remove(result);
                Statics.PersistSMTPServer();
                Statics.Display($"Server Removed: ", false, ConsoleColor.Gray);
                Statics.Display($"{result}", true, ConsoleColor.Green);
                Statics.Display($"New SMTP Server Count: ", false, ConsoleColor.Gray);
                Statics.Display($"{Statics.Servers.Count}", true, ConsoleColor.Green);
                Statics.PressAnyKey();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ListServers()
        {
            Console.WriteLine("");
            var sec = new Security();


            foreach (KeyValuePair<string, SmtpServer> item in Statics.Servers)
            {

                Statics.Display("Server: ", false, ConsoleColor.Gray);
                Statics.Display(item.Key, true, ConsoleColor.Green);

                Statics.Display("Host: ", false, ConsoleColor.Gray);
                Statics.Display(item.Value.Host, true, ConsoleColor.Green);

                Statics.Display("Port: ", false, ConsoleColor.Gray);
                Statics.Display(item.Value.Port.ToString(), true, ConsoleColor.Green);

                Statics.Display("UserName: ", false, ConsoleColor.Gray);
                Statics.Display(item.Value.UserName, true, ConsoleColor.Green);

                Statics.Display("Secret: ", false, ConsoleColor.Gray);
                Statics.Display(sec.DecryptString(item.Value.Secret), true, ConsoleColor.Green);

                Statics.Display("************************************", true, ConsoleColor.White);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Statics.PressAnyKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="padded"></param>
        public void ShowMenu(bool padded = true)
        {
            if (padded)
            {
                Console.WriteLine("");
                Console.WriteLine("");
            }


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("SMTPServer Menu");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("********************************");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("List - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("List Servers \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Add - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Add Server ({Statics.Servers.Count}) \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Remove -");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Remove Server \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Edit - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Edit Server \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Main - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Return to Main Menu \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Exit - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Exit Application \n");


            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("********************************");
        }
    }

}