using System;
using System.CommandLine.DragonFruit;


namespace EmailLoop
{
    class Program
    {
        static void Main(string command, string server, string emailaddress, string message, string subject, int interval = 5, int delay = 1000)
        {
            try
            {

#if DEBUG
                command = "blast";
                server = "Google";
                emailaddress = "it@amaracode.com";
                message = "stop emailing me";
                subject = "eat chit";
                interval = 3;
                delay = 1000;
#endif                



                //create instance of handler
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
                    //Statics.Display(cli.ToString());

                    //check the command and do the work
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
                    Statics.Display(cli.ToString());
                    Statics.Display("Nothing to run... CLI invalid");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }
}
