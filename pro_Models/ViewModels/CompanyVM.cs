using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class CompanyVM
    {
        [ValidateComplexType]
        public Company Company { get; set; } = new Company();
        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public string Exception { get; set; }
    }
}
