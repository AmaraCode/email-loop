using System;

namespace EmailLoop.Menus
{

    /// <summary>
    /// 
    /// </summary>
    public class MainMenu : IMenu
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static MainMenu CreateNew()
        {
            return new MainMenu();
        }



        /// <summary>
        /// 
        /// </summary>
        public void Invoke()
        {
            bool exit = false;
            //bool firstRun = true;
            bool padMenu = false;

            while (!exit)
            {
                ShowMenu(padMenu);  //temporary
                padMenu = true;

                var result = Statics.GetUserInput("Enter Command: ", ConsoleColor.Blue, ConsoleColor.Green);
                switch (result.ToLower())
                {

                    case "":
                        Console.Clear();
                        padMenu = false;
                        //ShowMenu();
                        break;

                    case "send":
                        SendEmails();
                        break;

                    case "list":
                        ListEmails();
                        break;

                    case "add":
                        AddEmail();
                        break;

                    case "remove":
                        RemoveEmail();
                        break;

                    case "x":
                        //reset the console color
                        Console.ForegroundColor = ConsoleColor.White;
                        exit = true;
                        break;

                    case "exit":
                        //reset the console color
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                        break;

                    case "wipe":
                        //reset the console color
                        WipeAllEmails();
                        break;


                    case "smtp":
                        var item = SmtpServerMenu.CreateNew();
                        item.Invoke();
                        break;

                    default:
                        System.Console.WriteLine("Invalid Option.");
                        //ShowMenu();
                        break;
                }
            }

        }



        /// <summary>
        /// 
        /// </summary>
        public void ShowMenu(bool padded = true)
        {
            if (padded)
            {
                Console.WriteLine("");
                Console.WriteLine("");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Main Menu");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new string('-', 60));



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Send - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Send Blast \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("List - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"List Emails ({Statics.Emails.Count}) \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Add - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Add Email Address \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Remove - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Remove Email Address \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SMTP - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("SMTP Server Menu \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Wipe - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Wipe All Emails \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Exit - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Exit Application \n");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(new string('-', 60));
        }


        /// <summary>
        /// 
        /// </summary>
        public void SendEmails()
        {
            var eng = Engine.CreateNew();
            eng.SendEmail();
        }


        /// <summary>
        /// Simple method that displays the emails in our blast list
        /// </summary>
        public void ListEmails()
        {
            Console.Clear();
            Statics.Display("Email Addresses", true, ConsoleColor.White);
            Console.WriteLine(new string('-', Console.WindowWidth));

            foreach (string email in Statics.Emails)
            {
                Statics.Display(email, true, ConsoleColor.Yellow);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Statics.PressAnyKey();
        }


        /// <summary>
        /// 
        /// </summary>
        public void WipeAllEmails()
        {

            string response = Statics.GetUserInput("Enter yes to confirm: ", ConsoleColor.Red, ConsoleColor.White);

            if (response == "yes")
            {
                Statics.Emails.Clear();
                Statics.PersistEmail();
                Statics.Display("All emails deleted.", true, ConsoleColor.Red);
                Statics.Display("New Emails Count: ", false, ConsoleColor.White);
                Statics.Display($"{Statics.Emails.Count}", true, ConsoleColor.Green);
                Statics.PressAnyKey();
            }
            else
            {
                Statics.Display("Wipe Aborted", true, ConsoleColor.Red);
                Statics.PressAnyKey();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public void RemoveEmail()
        {
            string email = Statics.GetUserInput("Enter Email Address To Remove: ", ConsoleColor.Gray, ConsoleColor.Green);

            if (email != "")
            {
                Statics.Emails.Remove(email);
                Statics.PersistEmail();
                Statics.Display($"Email Removed: ", false, ConsoleColor.Gray);
                Statics.Display($"{email}", true, ConsoleColor.Green);
                Statics.Display($"New Emails Count: ", false, ConsoleColor.Gray);
                Statics.Display($"{Statics.Emails.Count}", true, ConsoleColor.Green);
                Statics.PressAnyKey();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public void AddEmail()
        {
            string email = Statics.GetUserInput("Enter New Email Address: ", ConsoleColor.Gray, ConsoleColor.Green);

            if (email != "")
            {
                if (!Statics.Emails.Contains(email))
                {
                    Statics.Emails.Add(email);
                    Statics.PersistEmail();
                    Statics.Display($"Email Added: ", false, ConsoleColor.Gray);
                    Statics.Display($"{email}", true, ConsoleColor.Green);
                    Statics.Display($"New Emails Count: ", false, ConsoleColor.Gray);
                    Statics.Display($"{Statics.Emails.Count}", true, ConsoleColor.Green);
                    Statics.PressAnyKey();
                }
                else
                {
                    Statics.Display($"Email already exists.", true, ConsoleColor.Red);
                    Statics.PressAnyKey();
                }

            }
        }


    }

}