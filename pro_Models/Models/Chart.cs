using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Chart
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int TypeId { get; set; } = 1;
        [Required]
        public int MenuId { get; set; } = 1;
        [Required]
        public int SideId { get; set; } = 1;
        [Required]
        public bool Deprecated { get; set; } = false;
        public string Notes { get; set; }

    }
}
