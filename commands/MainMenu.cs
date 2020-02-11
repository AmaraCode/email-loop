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
            bool firstRun = true;

            while (!exit)
            {
                if (firstRun)
                {
                    ShowMenu(false);
                    firstRun = false;
                }
                else
                {
                    ShowMenu();
                }


                var result = Statics.GetUserInput("Enter Command: ", ConsoleColor.Blue, ConsoleColor.Green);
                switch (result)
                {

                    case "":
                        Console.Clear();
                        ShowMenu();
                        break;

                    case "1":
                        System.Console.WriteLine("Coming soon");
                        break;

                    case "2":
                        DisplayList();
                        break;

                    case "3":
                        AddEmail();
                        break;

                    case "4":
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


                    case "5":
                        var item = SmtpServerMenu.CreateNew();
                        item.Invoke();
                        break;

                    default:
                        System.Console.WriteLine("Invalid Option.");
                        ShowMenu();
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
            Console.WriteLine("Welcome to the email blast app.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("********************************");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("1. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Send Blast \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("2. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"List Emails ({Statics.Emails.Count}) \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("3. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Add Email Address \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("4. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Remove Email Address \n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("5. ");
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
            Console.WriteLine("********************************");
        }



        /// <summary>
        /// Simple method that displays the emails in our blast list
        /// </summary>
        public void DisplayList()
        {
            foreach (string email in Statics.Emails)
            {
                System.Console.WriteLine(email);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }


        /// <summary>
        /// 
        /// </summary>
        public void WipeAllEmails()
        {
            Statics.Emails.Clear();
            Statics.PersistEmail();
            Statics.Display("All emails deleted.");
            Statics.Display($"New Emails Count: {Statics.Emails.Count}");
        }


        public void RemoveEmail()
        {
            string email = Statics.GetUserInput("Enter EmailAddress: ", ConsoleColor.Blue, ConsoleColor.White);

            if (email != "")
            {
                Statics.Emails.Remove(email);
                Statics.PersistEmail();
                Statics.Display($"Email Removed: {email}");
                Statics.Display($"New Emails Count: {Statics.Emails.Count}");

            }
        }


        public void AddEmail()
        {
            string email = Statics.GetUserInput("Enter EmailAddress: ", ConsoleColor.Blue, ConsoleColor.White);

            if (email != "")
            {
                if (!Statics.Emails.Contains(email))
                {
                    Statics.Emails.Add(email);
                    Statics.PersistEmail();
                    Statics.Display($"Email Added: {email}");
                    Statics.Display($"New Emails Count: {Statics.Emails.Count}");
                }
                else
                {
                    Statics.Display($"Email already exists.");

                }

            }
        }


    }

}