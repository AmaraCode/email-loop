using System;
using AmaraCode;

namespace EmailLoop.Commands
{


    /// <summary>
    /// 
    /// </summary>
    public class SmtpServerCommand : ICommand
    {


        /// <summary>
        /// 
        /// </summary>
        public void Invoke()
        {
            //host, port, username, secret

            var response = Statics.GetUserInput("SMTP Server: (A)dd / (E)dit / (D)elete / e(X)it");



            var host = Statics.GetUserInput("Enter Host Name: ");
            var port = Statics.GetUserInput("Enter Port Number:");
            var userName = Statics.GetUserInput("Enter UserName:");
            var secret = Statics.GetUserInput("Enter Password");


        }
    }

}