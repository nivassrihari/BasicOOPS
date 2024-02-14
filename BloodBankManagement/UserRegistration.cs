using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement
{
    public class UserRegistration
    {
        /*a.Donor Id (Auto Incremented which is start from UID1001)
            b.	Donor Name
            c.	Mobile Number
            d.	Blood Group
            e.	Age
            f.	LastDonationDate
        */

        //Field Declaration
        public static int s_donorID = 1000;
        //Propertie Declaration
        public string DonorId { get; set; }
        public string DonorName { get; set; }
        public string MobileNumber { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public int Age { get; set; }
        public DateTime LasetDonationDate { get; set; }

        //Constructor declaration
        public UserRegistration(string donorName,string mobileNumber,BloodGroup bloodGroup, int age, DateTime lastDonationDate)
        {
            s_donorID++;
            DonorId = "UID"+s_donorID;
            DonorName = donorName;
            MobileNumber=mobileNumber;
            BloodGroup=bloodGroup;
            Age=age;
            LasetDonationDate=lastDonationDate;
        }
    }
}