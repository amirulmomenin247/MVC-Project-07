using Final_Eve_07.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Eve_07.ViewModels
{
    public class SpeciFicationViewModel
    {
        public int SpeciFicationId { get; set; }

        public string CompanyName { get; set; }

        public string Camera { get; set; }

        public string Display { get; set; }

        public string RAM { get; set; }

        public int PhoneId { get; set; }
        public string PhoneName { get; set; }
    }
}