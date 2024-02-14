using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EbBillCalculation
{
    public class Operation
    {
        //UserDetails List
        static List<UserDetails> usersList = new List<UserDetails>();

        //Global object
       public static UserDetails presentUser;
        public static void MainMenu()
        {

            bool exitCondition = false;
            do
            {
                Console.WriteLine("Please enter the options (1-Register, 2-Login, 3-Exit)");
                int chooseMainMenu = int.Parse(Console.ReadLine());
                switch (chooseMainMenu)
                {
                    case 1: //Registration
                        {
                            Console.WriteLine("Please enter your name");
                            string name = Console.ReadLine(); //UserName read

                            Console.WriteLine("Enter Phone number");
                            string phone = Console.ReadLine(); //Phone number read

                            Console.WriteLine("Ente email id");
                            string email = Console.ReadLine();  //email read
                            //Object creation
                            UserDetails user = new UserDetails(name, phone, email);
                            usersList.Add(user);
                            System.Console.WriteLine($"Registration successfull, MeterID is : {user.MeterID}");
                            break;
                        }
                    case 2:
                        {
                            Login();
                            break;
                        }
                    case 3:
                        {
                            System.Console.WriteLine("Thank you for visiting us.");
                            exitCondition = true;
                            break;
                        }
                }
            } while (!exitCondition);

        }
        public static void Login()
        {
            System.Console.WriteLine("------------Login page.--------------");
            System.Console.WriteLine("Please enter your meterID");
            bool invalid = false;
            do
            {
                String meterID = Console.ReadLine();
                bool checkLogin = false;
                foreach (UserDetails user in usersList)
                {
                    if (user.MeterID.Equals(meterID))
                    {
                        presentUser =user;
                        checkLogin = true;
                        invalid = true;
                        SubMenu();
                    }
                }
                if (!checkLogin)
                {
                    System.Console.WriteLine("Invalid meterID,Try again.");
                    invalid = false;

                }
            } while (!invalid);
        }
        public static void SubMenu()
        {
            System.Console.WriteLine("--------Submenu-------------");
            bool exitCondition = false;
            do
            {
                Console.WriteLine("Please enter the options (1-Calculate Amount, 2-Display user details, 3-Exit)");
                int subMenu = int.Parse(Console.ReadLine());
                switch (subMenu)
                {
                    case 1: //Calculate amount
                        {
                            System.Console.WriteLine("Pleasee enter units");
                            int units = int.Parse(Console.ReadLine());
                            System.Console.WriteLine($"BillID : {presentUser.MeterID},\nUser name : {presentUser.UserName}\nUnits : {units}\nAmount :{units*5}");
                            break;
                        }
                    case 2:
                        {
                            // Meter ID -(EB1001), Username, Phone number, Mail id
                            System.Console.WriteLine("User details");
                            System.Console.WriteLine($"Meter ID : {presentUser.MeterID}\nUser name : {presentUser.UserName}\nPhone number : {presentUser.PhoneNumber}\nEmailID : {presentUser.MailID}");
                           
                            break;
                        }
                    case 3:
                        {
                            
                            exitCondition = true;
                            break;
                        }
                }
            } while (!exitCondition);
        }
    }
}