using Card.Helper;
using Card.Utility.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Interface.IServices
{
   public interface ICardService
    {
      
        Task<ResponseModel> GetCardTypes();
      

    }
}
