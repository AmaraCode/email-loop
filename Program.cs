﻿using System;
//using System.CommandLine.DragonFruit;
using EmailLoop.Commands;
using AmaraCode;
using System.Collections.Generic;

namespace EmailLoop
{
    /// <summary>
    /// We are utilizing the CommandLine featur to add Parameters to the Main method.
    /// This can be found on GitHub @ System.CommandLine.DragonFruit
    /// </summary>
    class Program
    {


        static void Main(string[] args)
        {
            //string command, string server, string emailaddress, string message, string subject, int interval = 5, int delay = 1000
            try
            {

                bool exit = false;
                LoadData();

                Statics.ShowMainMenu();
                while (!exit)
                {

                    var result = Statics.GetUserInput("Enter Command: ", ConsoleColor.Blue, ConsoleColor.Green);
                    switch (result)
                    {

                        case "clear":
                            Console.Clear();
                            //Statics.ShowMainMenu();
                            break;

                        case "":
                            Statics.ShowMainMenu();
                            break;

                        case "2":
                            var eng = Engine.CreateNew();
                            eng.DisplayList();
                            break;

                        case "exit":
                            //reset the console color
                            Console.ForegroundColor = ConsoleColor.White;
                            return;
                        case "5":
                            var item = new SmtpServerCommand();
                            item.Invoke();
                            break;
                        default:
                            System.Console.WriteLine("Invalid Option.");
                            Statics.ShowMainMenu();
                            break;
                    }
                }



                /*
                //create instance of handler and sets all the properties from the commandline
                var cli = CliConfig.CreateNew();
                cli.Command = command;
                cli.EmailAddress = emailaddress;
                cli.Interval = interval;
                cli.Delay = delay;
                cli.Subject = subject;
                cli.Message = message;
                cli.Server = server;

                if (command != null)
                {
                    //display all cli info
                    System.Console.WriteLine("*********** EmailLoop - Configuration ************");
                    Statics.Display(cli.ToString());


                    //Create instance of the Engine to call based on the command
                    var eng = Engine.CreateNew(cli);

                    switch (cli.Command.ToLower())
                    {
                        case "blast":
                            eng.Blast();
                            break;
                        case "add":
                            eng.AddEmail(cli.EmailAddress);
                            break;
                        case "remove":
                            eng.RemoveEmail(cli.EmailAddress);
                            break;
                        case "list":
                            eng.DisplayList();
                            break;
                        case "clear":
                            eng.Clear();
                            break;
                        default:
                            Statics.Display("Command Not Found");
                            break;
                    }
                    return 0;
                }
                else
                {
                    //display all cli info
                    //Statics.Display("Command not passed or invalid command.");
                    return 1;

                }
                */
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());

            }

        }



        /// <summary>
        /// 
        /// </summary>
        private static void LoadData()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            var io = new FileIO();

            Statics.Emails = io.GetCollection<List<string>>(path + "emails.json");
            Statics.Servers = io.GetCollection<Dictionary<string, SmtpServer>>(path + "SmtpServer.json");
        }
    }
}
