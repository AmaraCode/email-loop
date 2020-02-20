using System;
using System.Collections.Generic;


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
                ShowMenu(false);

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
                    case "edit":
                        EditServer();
                        break;
                    case "":
                        //ShowMenu();
                        Console.Clear();
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
        }


        /// <summary>
        /// 
        /// </summary>
        public void AddServer()
        {
            var svr = SmtpServer.CreateNew();
            AmaraCode.Security sec = new AmaraCode.Security();
            try
            {
                //call method to prompt user for input
                svr = PromptSmtpServer();

                Statics.Servers.Add(svr.ServerName, svr);
                Statics.PersistSMTPServer();
                Statics.Display($"Server Added: ", false, ConsoleColor.Gray);
                Statics.Display($"{svr.Host}", true, ConsoleColor.Green);
                Statics.Display($"New SMTP Server Count: ", false, ConsoleColor.Gray);
                Statics.Display($"{Statics.Servers.Count}", true, ConsoleColor.Green);
                System.Console.WriteLine("");
                Statics.PressAnyKey();
            }
            catch
            {
                Statics.Display("Invalid input", true, ConsoleColor.Red);
                System.Console.WriteLine("");
                Statics.PressAnyKey();
            }
        }


        private void EditServer()
        {

            Console.Clear();
            Statics.Display("Edit Smtp Server", true, ConsoleColor.Gray);
            Statics.Display(new string('-', 40), true, ConsoleColor.Gray);

            ShowServerList();

            var serverName = Statics.GetUserInput("Enter Server Name to Edit: ", ConsoleColor.Gray, ConsoleColor.Green);

            if (Statics.Servers.ContainsKey(serverName))
            {
                AmaraCode.Security sec = new AmaraCode.Security();
                SmtpServer svr = Statics.Servers[serverName];

                //prompt user for entries.
                var svrEdit = PromptSmtpServer(Statics.Servers[serverName]);

                if (svrEdit.Equals(svr))
                {
                    System.Console.WriteLine("No Changes Were Made");
                }
                else
                {
                    //remove the existing entry
                    Statics.Servers.Remove(serverName);

                    //Add new SmtpServer
                    Statics.Servers.Add(svrEdit.ServerName, svrEdit);

                    Statics.PersistSMTPServer();
                    Statics.Display($"Server Modified: ", false, ConsoleColor.Gray);
                    Statics.Display($"{svr.Host}", true, ConsoleColor.Green);
                    //Statics.Display($"New SMTP Server Count: ", false, ConsoleColor.Gray);
                    //Statics.Display($"{Statics.Servers.Count}", true, ConsoleColor.Green);
                }


                Statics.PressAnyKey();

            }
            else
            {
                Statics.Display("Cannot find that Server name.", true, ConsoleColor.Red);
                System.Console.WriteLine("");
                Statics.PressAnyKey();
            }


        }



        /// <summary>
        /// This method should handle all the prompts to get SMTPServer info from the users.
        /// If a SmtpServer object is passed in then use that to populate for Edit.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        private SmtpServer PromptSmtpServer(SmtpServer server = null)
        {
            //keep varible stating if an object  was passed in.
            bool hasServer = server == null ? false : true;
            AmaraCode.Security sec = new AmaraCode.Security();
            var newServer = SmtpServer.CreateNew();

            if (hasServer == false)
            {
                //this is genearlly used for an ADD method

                //create a new instance since not passed in.
                //server = SmtpServer.CreateNew();
                newServer.ServerName = Statics.GetUserInput("Server Name: ", ConsoleColor.Gray, ConsoleColor.Green);
                newServer.Host = Statics.GetUserInput("Enter Host: ", ConsoleColor.Gray, ConsoleColor.Green);
                newServer.Port = Statics.GetUserInput<int>("Enter Port: ", ConsoleColor.Gray, ConsoleColor.Green);
                newServer.UserName = Statics.GetUserInput("Enter UserName: ", ConsoleColor.Gray, ConsoleColor.Green);
                newServer.Secret = sec.Encrypt(Statics.GetUserInput("Enter Secret: ", ConsoleColor.Gray, ConsoleColor.Green), Statics.EncryptionKey);

            }
            else
            {

                if (Statics.GetUserSingleInput("Edit ServerName (y/n) ", ConsoleColor.Gray, ConsoleColor.Green) == 'y')
                {
                    newServer.ServerName = Statics.GetUserInput($"Server Name [{server.ServerName}]: ", ConsoleColor.Gray, ConsoleColor.Green);
                }
                else
                {
                    newServer.ServerName = server.ServerName;
                }

                if (Statics.GetUserSingleInput("Edit Host (y/n) ", ConsoleColor.Gray, ConsoleColor.Green) == 'y')
                {
                    newServer.Host = Statics.GetUserInput($"Enter Host [{server.Host}]: ", ConsoleColor.Gray, ConsoleColor.Green);
                }
                else
                {
                    newServer.Host = server.Host;
                }

                if (Statics.GetUserSingleInput("Edit Port (y/n) ", ConsoleColor.Gray, ConsoleColor.Green) == 'y')
                {
                    newServer.Port = Statics.GetUserInput<int>($"Enter Port [{server.Port.ToString()}]: ", ConsoleColor.Gray, ConsoleColor.Green);
                }
                else
                {
                    newServer.Port = server.Port;
                }

                if (Statics.GetUserSingleInput("Edit UserName (y/n) ", ConsoleColor.Gray, ConsoleColor.Green) == 'y')
                {
                    newServer.UserName = Statics.GetUserInput($"Enter UserName [{server.UserName}]: ", ConsoleColor.Gray, ConsoleColor.Green);
                }
                else
                {
                    newServer.UserName = server.UserName;
                }

                if (Statics.GetUserSingleInput("Edit Secret (y/n) ", ConsoleColor.Gray, ConsoleColor.Green) == 'y')
                {
                    newServer.Secret = sec.Encrypt(Statics.GetUserInput($"Enter Secret [{sec.Decrypt(server.Secret, Statics.EncryptionKey)}]: ", ConsoleColor.Gray, ConsoleColor.Green), Statics.EncryptionKey);
                }
                else
                {
                    newServer.Secret = server.Secret;
                }
            }

            return newServer;

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
                System.Console.WriteLine("");
                Statics.PressAnyKey();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ListServers()
        {
            ShowServerList();
            Console.WriteLine("");
            Statics.PressAnyKey();
        }



        /// <summary>
        /// This method was made because multiple other methods needed to display the list.
        /// </summary>
        private void ShowServerList()
        {

            Console.Clear();
            var sec = new AmaraCode.Security();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0,-30} {1,-30} {2, -10} {3, -30} {4, -30}", "Server", "Host", "Port", "User Name", "Secret");
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (KeyValuePair<string, SmtpServer> item in Statics.Servers)
            {
                Console.WriteLine("{0,-30} {1,-30} {2, -10} {3, -30} {4, -30}", item.Key, item.Value.Host,
                item.Value.Port.ToString(), item.Value.UserName, sec.Decrypt(item.Value.Secret, Statics.EncryptionKey));
            }
            Console.ForegroundColor = ConsoleColor.White;


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
            Console.WriteLine(new string('-', 60));

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
            Console.WriteLine(new string('-', 60));
        }
    }

}