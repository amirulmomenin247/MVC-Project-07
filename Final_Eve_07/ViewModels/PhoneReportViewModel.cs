using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Eve_07.ViewModels
{
    public class PhoneReportViewModel
    {
        public int PhoneId { get; set; }
      
        public string PhoneName { get; set; }
        
        public DateTime LaunchDate { get; set; }
      
        public string OperatingSystem { get; set; }
       
        public decimal PhonePrice { get; set; }
        public bool Available { get; set; }
        
        public string Picture { get; set; }
        public byte[] Image { get; set; }
    }
}