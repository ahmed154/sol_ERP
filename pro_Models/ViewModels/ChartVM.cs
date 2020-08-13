using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class ChartVM
    {
        [ValidateComplexType]
        public Chart Chart { get; set; } = new Chart();
        public string Exception { get; set; }
    }
}
