using Card.Helper;
using Card.Model;
using Card.Utility.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Interface.IRepo
{
  public interface  ICardRepositoy
    {
        IEnumerable<string> ValidateCardWithStoreProcedure(string query, params object[] parameters);
     //  bool InsertUpdate(CardNumberViewModel cardNumber);

       int InsertUpdate(CardNumber cardNumber);

        //Task<ResponseModel> InsertUpdate(CardNumberViewModel cardNumber);

    }
}
