using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Final_Eve_07.Models
{
    public class Phone
    {
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
    public class SpeciFication
    {
        public int SpeciFicationId { get; set; }
        [Required, StringLength(50)]
        public string CompanyName { get; set; }
        [Required]
        public string Camera { get; set; }
        [Required, StringLength(50)]
        public string Display { get; set; }
        [Required, StringLength(20)]
        public string RAM { get; set; }
        [ForeignKey("Phone")]
        public int PhoneId { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual List<AdditionalFeature> AdditionalFeatures { get; set; } = new List<AdditionalFeature>();
    }
    public class AdditionalFeature
    {
        public int AdditionalFeatureId { get; set; }
        [Required, StringLength(200)]
        public string Sensors { get; set; }
        [Required, StringLength(100)]
        public string Battery { get; set; }
        [Required, StringLength(50)]
        public string VideoSupport { get; set; }
        [ForeignKey("SpeciFication")]
        public int SpeciFicationId { get; set; }
        public virtual SpeciFication SpeciFication { get; set; }
        public virtual List<Platform> Platforms { get; set; } = new List<Platform>();
    }
    public class Platform
    {
        public int PlatformId { get; set; }
        [Required, StringLength(50)]
        public string Chipset { get; set; }
        [Required, StringLength(50)]
        public string CPU { get; set; }
        [Required, StringLength(50)]
        public string GPU { get; set; }
        [ForeignKey("AdditionalFeature")]
        public int AdditionalFeatureId { get; set; }
        public virtual AdditionalFeature AdditionalFeature { get; set; }

    }
    public class PhoneDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<SpeciFication> SpeciFications { get; set; }
        public DbSet<AdditionalFeature> AdditionalFeatures { get; set; }
        public DbSet<Platform> Platforms { get; set; }
    }
}