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
        Task<ResponseModel> SaveCardNumber(CardInputViewModel cardInputViewModel);
        Task<ResponseModel> GetAllCardNumbers();
        Task<ResponseModel> GetAllCardNumbersUsingSP();
        Task<ResponseModel> GeTCardNumberById(long Id);
        Task<ResponseModel> CheckValidateCard(CardInputViewModel cardInputViewModel);
        Task<ResponseModel> CheckValidateCardSP(CardInputViewModel cardInputViewModel);
        Task<ResponseModel> InsertUpdate(CardInputViewModel cardInputViewModel); Task<ResponseModel> GetCardTypes();
      

    }
}
