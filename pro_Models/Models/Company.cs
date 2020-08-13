using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxNo { get; set; }
        public string CompanyRegistry { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        ///////////////////////////////////////////////////////////////////
        ///
        public Currency Currency { get; set; }
    }
}
