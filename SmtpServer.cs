using System;
using System.Diagnostics.CodeAnalysis;

namespace EmailLoop
{

    /// <summary>
    /// poco
    /// </summary>
    public class SmtpServer : IEquatable<SmtpServer>

    {

        //#pragma warning disable 1591
        public string ServerName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Secret { get; set; }

        //#pragma warning restore 1591

        public static SmtpServer CreateNew()
        {
            return new SmtpServer();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] SmtpServer other)
        {
            if (other == null) return false;
            return (
                this.ServerName.Equals(other.ServerName) &&
                this.Host.Equals(other.Host) &&
                this.Port.Equals(other.Port) &&
                this.UserName.Equals(other.UserName) &&
                this.Secret.Equals(other.Secret));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ServerName.GetHashCode();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            SmtpServer svr = obj as SmtpServer;
            if (svr != null)
            {
                return Equals(svr);
            }
            else
            {
                return false;
            }
        }

    }

}