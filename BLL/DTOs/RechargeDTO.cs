using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class RechargeDTO
    {
        [Required]
        [Range(100, 100000)]
        public decimal Amount { get; set; }
    }
}



//ErrorMessage = "Minimum Recharge Amount is 100.00 & Maximum is 100000.00"