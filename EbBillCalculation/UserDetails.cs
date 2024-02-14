using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;

namespace EbBillCalculation
{
    // Meter ID -(EB1001), Username, Phone number, Mail id, Units Used =0
    public class UserDetails
    {
        //Field
        public static int s_meterID=1000;
        //Properties
        public string MeterID { get; set; }
        public String UserName { get; set; }
        public string PhoneNumber { get; set; }
        public String MailID { get; set; }
        public int UnitsUsed {get; set;}
        

        public UserDetails(string userName,string phoneNumber,string mailID)
        {
            s_meterID++;
            MeterID ="EB"+s_meterID;
            UserName=userName;
            PhoneNumber=phoneNumber;
            MailID=mailID;
        } 
        public void  CalculateAmount(int unitsUsed)
        {
            
        }
    }
}