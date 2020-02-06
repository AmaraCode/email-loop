using System;
using System.Collections.Generic;

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
    }

}