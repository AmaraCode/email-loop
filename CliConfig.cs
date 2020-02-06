using System;

namespace EmailLoop
{

    /// <summary>
    /// 
    /// </summary>
    public class CliConfig
    {



#pragma warning disable 1591
        public int Interval { get; set; } //60 seconds
        public int Delay { get; set; } //this one is milliseconds
        public string EmailAddress { get; set; } = "";
        public string Command { get; set; } = "loop"; //loop, Add, Remove, Blast
        public string Message { get; set; } = "Please stop emailing this address";
        public string Subject { get; set; } = "your request";
        public string Server { get; set; } = "AmaraCode";
#pragma warning restore 1591


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static CliConfig CreateNew()
        {
            return new CliConfig();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $" Command: {Command} \n Intervals: {Interval} \n Delay: {Delay} \n Server: {Server} \n EmailAddress: {EmailAddress} \n Subject: {Subject} \n Message: {Message}";
        }

    }

}