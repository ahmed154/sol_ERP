using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class CostCenterVM
    {
        [ValidateComplexType]
        public CostCenter CostCenter { get; set; } = new CostCenter();
        public string Exception { get; set; }
    }
}
