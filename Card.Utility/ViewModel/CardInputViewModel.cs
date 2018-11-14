using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Utility.ViewModel
{
   public class CardInputViewModel
    {
        [Required]
        [MaxLength(16)]
        [MinLength(15)]
        [RegularExpression("[^0-9]", ErrorMessage = "Card must be numeric")]
        public string cardNumber { get; set; }
        [Required]
        [StringLength(6)]
        [RegularExpression("[^0-9]", ErrorMessage = "Expiry Date must be numeric")]
        public string expiryDate { get; set; }

        
    }
}
