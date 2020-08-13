using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class CurrencyVM
    {
        [ValidateComplexType]
        public Currency Currency { get; set; } = new Currency();
        public string Exception { get; set; }
    }
}
