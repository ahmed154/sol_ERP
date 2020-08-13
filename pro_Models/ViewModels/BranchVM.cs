using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class BranchVM
    {
        [ValidateComplexType]
        public Branch Branch { get; set; } = new Branch();
        public string Exception { get; set; }
    }
}
