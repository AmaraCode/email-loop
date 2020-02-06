using System;
using System.CommandLine.DragonFruit;


namespace EmailLoop
{
    /// <summary>
    /// We are utilizing the CommandLine featur to add Parameters to the Main method.
    /// This can be found on GitHub @ System.CommandLine.DragonFruit
    /// </summary>
    class Program
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="server"></param>
        /// <param name="emailaddress"></param>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <param name="interval"></param>
        /// <param name="delay"></param>
        static void Main(string command, string server, string emailaddress, string message, string subject, int interval = 5, int delay = 1000)
        {
            try
            {


                //this is added to enable a test from debug mode

#if DEBUG
                command = "blast";
                server = "Google";
                emailaddress = "sucker@scammer.com";
                message = "stop emailing me";
                subject = "eat chit";
                interval = 3;
                delay = 1000;
#endif



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
                }
                else
                {
                    //display all cli info
                    Statics.Display("Command not passed or invalid command.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
