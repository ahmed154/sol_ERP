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
        public string Exception { get; set; }
    }
}
