using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Utility.ViewModel
{
   public class CardNumberViewModel
    {
        public long id { get; set; }

        public CardTypeViewModel cardTypeModel { get; set; }
        public string cNumber { get; set; }
        public string expiry { get; set; }
        public bool active { get; set; }

        public DateTime created { get; set; }

        public long author { get; set; }

        public DateTime modified { get; set; }
        public long editor { get; set; }

    }
}
