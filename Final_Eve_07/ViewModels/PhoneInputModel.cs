using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Final_Eve_07.Models;

namespace Final_Eve_07.ViewModels
{
    public class PhoneInputModel
    {
        [Key]
        public int PhoneId { get; set; }
        [Required, StringLength(100)]
        public string PhoneName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime LaunchDate { get; set; }
        [Required, StringLength(30)]
        public string OperatingSystem { get; set; }
        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal PhonePrice { get; set; }
        public bool Available { get; set; }
        [Required, StringLength(30)]
        public string Picture { get; set; }
        public virtual List<SpeciFication> SpeciFications { get; set; } = new List<SpeciFication>();

    }
}