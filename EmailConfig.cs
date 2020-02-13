using System;

namespace EmailLoop
{

    /// <summary>
    /// 
    /// </summary>
    public class EmailConfig
    {



#pragma warning disable 1591
        public int Interval { get; set; } = 12; //default for 12, 5second emails
        public int Delay { get; set; } = 5000; //this one is milliseconds
        public string EmailAddress { get; set; } = "";
        public string Message { get; set; } = "I have asked multiple times to be removed from your email list....";
        public string Subject { get; set; } = "Your email list";
        public string Server { get; set; } = "";
#pragma warning restore 1591


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static EmailConfig CreateNew()
        {
            return new EmailConfig();
        }


    }
}