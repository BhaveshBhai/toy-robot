using System;
using System.IO;
using System.Net;
using toy_robot.Model;

namespace toy_robot
{
    class Program
    {
        static void Main(string[] args)
        {

            DisplayWelcome();

        }
        private static void DisplayWelcome()
        {
            var driver = new RobotDriver(new Robot());

            Console.WriteLine("\n Toy Robot");
            Console.WriteLine("---------");
            Console.WriteLine("");

            Console.Write("\nHere are the options :\n");
            Console.Write("1-Using File.\n2-Standard Input.\n3-Exit.\n");
            Console.Write("\nInput your choice :");
            var opt = Convert.ToInt32(Console.ReadLine());
            string command;
            switch (opt)
            {
                case 1:
                   
                    Console.WriteLine("\nPlease enter the fully qualified path of the file to be uploaded to the URI");
                    
                    using (var file = new StreamReader(Console.ReadLine()))
                    {
                        
                        while ((command = file.ReadLine()) != null)
                        {
                            Console.WriteLine(driver.Command(command));
                        }
                    }
                    break;

                case 2:
                    while (true)
                    {
                        command = PromptForCommand();

                        if (command.ToUpper() == "REPORT")
                        {
                            Console.WriteLine(driver.Command(command)); 
                            DisplayWelcome();
                        }
                        else { Console.WriteLine(driver.Command(command)); }
                    }
                    
                case 3:
                    break;

                default:
                    Console.Write("Input correct option\n");
                    break;
            }
        }

        private static string PromptForCommand()
        {
            Console.WriteLine(" Please enter next move # : ");
            return Console.ReadLine();
        }
    }
}
