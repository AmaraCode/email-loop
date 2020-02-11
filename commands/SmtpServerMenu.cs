using System;
using AmaraCode;

namespace EmailLoop.Menus
{


    /// <summary>
    /// 
    /// </summary>
    public class SmtpServerMenu : IMenu
    {


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

            ShowMenu();


            bool exit = false;
            while (!exit)
            {
                var result = Statics.GetUserInput("Enter Command: ", ConsoleColor.Blue, ConsoleColor.Green);
                switch (result.ToLower())
                {
                    case "":
                        ShowMenu();
                        break;

                    case "x":
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
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("List Servers \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Add Server ({Statics.Emails.Count}) \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("3. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Remove Server \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("4. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Edit Server \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("X. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Exit to Main Menu \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Exit - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Exit Application \n");


            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("********************************");
        }
    }

}