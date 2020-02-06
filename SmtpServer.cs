using System;

namespace EmailLoop
{

    /// <summary>
    /// poco
    /// </summary>
    public class SmtpServer
    {

#pragma warning disable 1591

        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Secret { get; set; }

#pragma warning restore 1591        

    }

}