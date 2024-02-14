using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement
{
    //Emun declaration for blood group
    public enum BloodGroup {select,A_Positive,B_Positive,O_Positive,AB_Positive}
    public class Donation
    {
        /*
        •	Donation ID (Auto increment - DID1001)
        •	Donor Id
        •	Donation Date
        •	Weight
        •	Blood Pressure
        •	Hemoglobin Count (above 13.5)
        •	Blood Group – (Enum – A_Positive, B_Positive, O_Positive, AB_Positive)
        */

        //Field
        public static int s_DonationID = 1000;

        //Properties

        public string DonationID { get; set; }
        public string DonorID { get; set; }
        public DateTime DonationDate { get; set; }
        public int Weight{get;set;}
        public int BloodPressure{get;set;}
        public double HemoglobinCount{get;set;}
        public BloodGroup BloodGroup {get;set;}

        //Constructore

        public Donation(string donorID,DateTime donationDate,int weight,int bloodPressure,double hemoglobinCount,BloodGroup bloodGroup)
        {
            s_DonationID++;
            DonationID = "DID"+s_DonationID;
            DonorID = donorID;
            DonationDate=donationDate;
            Weight =weight;
            BloodPressure=bloodPressure;
            HemoglobinCount=hemoglobinCount;
            BloodGroup=bloodGroup; 
        }
    }
}