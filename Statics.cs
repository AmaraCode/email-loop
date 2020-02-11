using System;
using System.Collections.Generic;
using AmaraCode;


namespace EmailLoop
{

    /// <summary>
    /// Static class to hole random stuff since this is a small app
    /// </summary>
    public static class Statics
    {

#pragma warning disable 1591
        public static bool Cancelled { get; set; } = false;
        public static List<string> Emails { get; set; }
        public static Dictionary<string, SmtpServer> Servers { get; set; }
#pragma warning restore 1591


        /// <summary>
        /// static constructor
        /// </summary>
        static Statics()
        {
            Emails = new List<string>();
            Servers = new Dictionary<string, SmtpServer>();
        }


        /// <summary>
        /// A single display method for writing to console
        /// </summary>
        /// <param name="info"></param>
        /// <param name="showDate"></param>
        public static void Display(string info, bool showDate = false)
        {
            if (showDate)
            {
                System.Console.WriteLine($"{DateTime.Now} : {info}");
            }
            else
            {
                System.Console.WriteLine($"{info}");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageToDisplay"></param>
        /// <returns></returns>
        public static string GetUserInput(string messageToDisplay, ConsoleColor messageColor = ConsoleColor.White, ConsoleColor promptColor = ConsoleColor.White)
        {
            Console.ForegroundColor = messageColor;
            Console.Write(messageToDisplay + " ");
            Console.ForegroundColor = promptColor;

            var result = Console.ReadLine();
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        public static void ShowMainMenu()
        {
            //Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to the email blast app.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("********************************");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Send Blast \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"List Emails ({Statics.Emails.Count}) \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("3. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Add Email Address \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("4. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Remove Email Address \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("5. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("SMTP Server Menu \n");


            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Type");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" exit");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" to quit application. \n");



            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("********************************");
        }



        public static void PersistData()
        {

            //TODO implement methods

        }

    }
}