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
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="lineFeed"></param>
        /// <param name="color"></param>
        public static void Display(string info, bool lineFeed = false, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            if (lineFeed)
            {
                System.Console.WriteLine($"{info}");
            }
            else
            {
                System.Console.Write($"{info}");
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageToDisplay"></param>
        /// <param name="messageColor"></param>
        /// <param name="promptColor"></param>
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
        public static void PersistEmail()
        {
            var path = Environment.CurrentDirectory;
            var fileio = new AmaraCode.FileIO();
            string file = path + "\\emails.json";
            System.Console.WriteLine(file);
            //persist the email list
            fileio.SaveCollection<List<string>>(Statics.Emails, file);
        }



        /// <summary>
        /// 
        /// </summary>
        public static void PersistSMTPServer()
        {
            var path = Environment.CurrentDirectory.ToString();
            var fileio = new AmaraCode.FileIO();
            string file = path + "\\smtpserver.json";
            //persist the email list
            fileio.SaveCollection<Dictionary<string, SmtpServer>>(Statics.Servers, file);
        }


        /// <summary>
        /// 
        /// </summary>
        public static void PressAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }


    }
}