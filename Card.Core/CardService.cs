using Card.Core.Common;
using Card.Helper;
using Card.Helper.ExceptionLog;
using Card.Interface.IRepo;
using Card.Interface.IServices;
using Card.Mapper;
using Card.Model;
using Card.Utility.Enum;
using Card.Utility.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Core
{
    public class CardService : ICardService
    {
        private IRepository<CardType> cardTypeRepo;
        private IRepository<CardNumber> cardNumberRepo;
        private ICardRepositoy cardRepo;
        public CardService(IRepository<CardType> cardTypeRepo, IRepository<CardNumber> cardNumberRepo, ICardRepositoy cardRepo)
        {
            this.cardNumberRepo = cardNumberRepo;
            this.cardTypeRepo = cardTypeRepo;
            this.cardRepo = cardRepo;
        }
    
        public async Task<ResponseModel> GetCardTypes()
        {
            try
            {
                List<CardTypeViewModel> enums = ((CardTypeEnum[])Enum.GetValues(typeof(CardTypeEnum))).Select(c => new CardTypeViewModel() { id = (int)c, name = c.ToString() }).ToList();
                return HelperClass.Response(true
                                    , GlobalDecleration._successAction
                                    , enums
                                );

            }
            catch (Exception ex)
            {

                ExceptionHandle.PrintException(ex);
                return HelperClass.Response(false,
                    GlobalDecleration._internalServerError,
                    null
                    );
            }
        }
 

    }
}
