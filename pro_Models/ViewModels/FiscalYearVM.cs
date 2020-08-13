using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class FiscalYearVM
    {
        [ValidateComplexType]
        public FiscalYear FiscalYear { get; set; } = new FiscalYear();
        public string Exception { get; set; }
    }
}
