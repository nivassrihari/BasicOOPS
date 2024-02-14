using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace BloodBankManagement
{
    public class Operations
    {
        //UsersList
        static List<UserRegistration> usersList = new List<UserRegistration>();
        // DonationList
        static List<Donation> donationsList = new();

        //Global object
        static UserRegistration currentLoggedInUser;

        public static void MainMenu()
        {
            Console.WriteLine("****************Bload Bank*******************");
            Console.WriteLine("MainMenu \n 1.User registration \n 2.User login \n 3.Fetch donor details \n 4.Exit.");
            //Get mainmenu option from user
            System.Console.WriteLine("Choose a option.");
            int mainMenu = int.Parse(Console.ReadLine());
            switch (mainMenu)
            {
                case 1:
                    {
                        Console.Clear();
                        System.Console.WriteLine("Registration page..");
                        Registration();
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        System.Console.WriteLine("Login page...");
                        Login();
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        FetchDonorsDetails();
                        break;
                    }
                case 4:
                    {
                        System.Console.WriteLine("exit");
                        break;
                    }
            }
        }

        //Registratioin page starts
        public static void Registration()
        {
            //Get data for Registration
            Console.WriteLine("Please enter your name.");
            string name = Console.ReadLine();

            Console.WriteLine("Please enter your mobile number.");
            string mobileNumber = Console.ReadLine();

            System.Console.WriteLine("Please choose your Blood group \n 1.A_Positive \n 2.B_Positive \n 3.O_Positive \n 4.AB_Positive.");
            BloodGroup bloodGroup = Enum.Parse<BloodGroup>(Console.ReadLine());

            System.Console.WriteLine("Please enter your age.");
            int age;
            bool ageFlag;
            //Check age is valid
            do
            {
                ageFlag = int.TryParse(Console.ReadLine(), out age);
                if (!ageFlag)
                {
                    System.Console.WriteLine("Invalid age");
                }
            } while (!ageFlag && age < 0);

            Console.WriteLine("Please enter your last donation date (Example : dd/MM/yyyy).");
            DateTime lastDonationDate;
            bool date;
            //Check last donation date is valid
            do
            {
                date = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out lastDonationDate);
                if (!date)
                {
                    System.Console.WriteLine("Invalid date Try again.");
                }
            }
            while (!date);
            //User object creation
            UserRegistration user = new UserRegistration(name, mobileNumber, bloodGroup, age, lastDonationDate);
            //Add object to usersList
            usersList.Add(user);

            //Show User id
            System.Console.WriteLine("Registerd successfully");
            Console.WriteLine($"Your donor id is {user.DonorId}");
            MainMenu();

        }

        //Login page
        public static void Login()
        {
            System.Console.WriteLine(("Please enter your Donor id"));
            string donorID = Console.ReadLine();
            bool loginCheck = true;
            foreach (UserRegistration user in usersList)
            {
                if (user.DonorId.Equals(donorID))
                {
                    loginCheck = false;
                    currentLoggedInUser = user;
                    Console.Clear();
                    SubMenu();
                    break;
                }
            }
            if (loginCheck)
            {
                Console.WriteLine("Invalid user id");
            }

        }

        //Fetch donors details
        public static void FetchDonorsDetails()
        {
            System.Console.WriteLine("Donor details.");
            System.Console.WriteLine("Choose blood group \n1.A_Postive\n2.B_Positive\n3.O_Positive\n4.AB_Positve");
            BloodGroup bloodGroup = Enum.Parse<BloodGroup>(Console.ReadLine());
            foreach (UserRegistration user in usersList)
            {
                if (user.BloodGroup.Equals(bloodGroup))
                {
                    System.Console.WriteLine($"Donor name:{user.DonorName} | Donor Mobile : {user.MobileNumber} | Blood group :{user.BloodGroup}");
                }
            }
        }

        public static void SubMenu()
        {
            Console.WriteLine("SubMenu..");
            Console.WriteLine("1.Donate Blood \n2.Donation History\n3.Next Eligible Date\n4.Exit");
            System.Console.WriteLine("Choose a one option from above options.");
            int SubMenu = int.Parse(Console.ReadLine());
            switch (SubMenu)
            {
                case 1:
                    {
                        System.Console.WriteLine("Donate Blood page..");
                        DonateBlood();
                        break;
                    }
                case 2:
                    {

                        DonationHistory();
                        break;

                    }
                case 3:
                    {
                        EligibilityCheck();
                        break;

                    }
                case 4:
                    {
                        MainMenu();
                        break;
                    }
            }
        }
        //DonateBlood methos
        public static void DonateBlood()
        {
            Console.Clear();
            System.Console.WriteLine("Please enter your wight.");
            int weight = int.Parse(Console.ReadLine());
            System.Console.WriteLine("Please enter your Blood pressure.");
            int bloodPressure = int.Parse(Console.ReadLine());
            System.Console.WriteLine("Please enter your hemoglobin count.");
            double hemoglobinCount = double.Parse(Console.ReadLine());
            DateTime Today = DateTime.Now;


            if (weight > 50 && bloodPressure < 130 && hemoglobinCount > 13)
            {
                if (Today.AddMonths(-6) >= currentLoggedInUser.LasetDonationDate)
                {
                    //Object creation 
                    Donation donated = new(currentLoggedInUser.DonorId, DateTime.Now, weight, bloodPressure, hemoglobinCount, currentLoggedInUser.BloodGroup);
                    //Add to list
                    donationsList.Add(donated);
                    System.Console.WriteLine("Donated successfuly");
                    System.Console.WriteLine($"DonationID is {donated.DonationID}");
                    System.Console.WriteLine($"Next eligible date of donatioin: {Today.AddMonths(6).ToString("dd/MM/yyyy")}");
                    SubMenu();
                }

            }
            else
            {
                System.Console.WriteLine("You'r not eligibel to donate,Thank you :)");
            }
        }

        //Donation history
        public static void DonationHistory()
        {
            System.Console.WriteLine("Donation History page..");
            bool historyFlag = false;
            foreach (Donation donat in donationsList)
            {
                if (currentLoggedInUser.DonorId.Equals(donat.DonorID))
                {
                    historyFlag = true;
                    Console.WriteLine($"Donation ID : {donat.DonationID} | Donation date : {donat.DonationDate.ToString("dd/MM/yyyy")} | Weight : {donat.Weight} | Blood pressure : {donat.BloodPressure} | HB count : {donat.HemoglobinCount}");
                    SubMenu();
                }
            }
            if (!historyFlag)
            {
                System.Console.WriteLine("No match found.");
                SubMenu();
            }

        }

        //Eligibility chick
        public static void EligibilityCheck()
        {
            System.Console.WriteLine("Next Eligible Date..");
            ArrayList dates = new ArrayList();
            bool eligibelFlag = false;
            foreach (Donation donation in donationsList)
            {
                if (currentLoggedInUser.DonorId.Equals(donation.DonorID))
                {
                    eligibelFlag = true;
                    dates.Add(donation.DonationDate);
                }
            }
            if (dates.Count == 1)
            {
                foreach (DateTime date in dates)
                {
                    System.Console.WriteLine(date.AddMonths(6).ToString("dd/MM/yyyy"));
                }
            }
            else if (dates.Count > 1)
            {
                DateTime max = DateTime.MinValue;
                foreach (DateTime date in dates)
                {
                    if (DateTime.Compare(date, max) == 1)
                    {
                        max = date;
                    }
                }
                System.Console.WriteLine(max.ToString("dd/MM/yyyy"));
            }

            //eligible check if condition not to pass
            if (!eligibelFlag)
            {
                Console.WriteLine("You are elibile Now");
            }
            SubMenu();
        }
        //Default data Add
        public static void DefaultData()
        {
            /*
            UID1001	Ravichandran	8484848	O_Positive	30	25/08/2022
            UID1002	Baskaran	4747447	AB_Positive	30	30/09/2022
            */
            //UserObject
            UserRegistration userOne = new UserRegistration("Ravichandran", "8484848", BloodGroup.O_Positive, 30, DateTime.ParseExact("25/08/2022", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture));
            UserRegistration userTwo = new UserRegistration("Baskaran", "4747447", BloodGroup.AB_Positive, 30, DateTime.ParseExact("30/09/2022", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture));
            // Add user object list
            usersList.AddRange(new List<UserRegistration> { userOne, userTwo });

            /*
            DID1001	UID1001	10/06/2022	73	120	14	O_Positive
            DID1002	UID1001	10/10/2022	74	120	14	O_Positive
            DID1003	UID1002	11/07/2022	74	120	13.6	AB_Positive
            */

            //Donation Object
            Donation donationOne = new Donation("UID1001", DateTime.ParseExact("10/06/2022", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), 73, 120, 14, BloodGroup.O_Positive);
            Donation donationTwo = new Donation("UID1002", DateTime.ParseExact("10/10/2022", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), 74, 120, 14, BloodGroup.O_Positive);
            Donation donationThree = new Donation("UID1003", DateTime.ParseExact("11/07/2022", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), 74, 120, 13.6, BloodGroup.O_Positive);
            //Add Donation data in donationsList
            donationsList.AddRange(new List<Donation> { donationOne, donationTwo, donationThree });

        }
    }
}