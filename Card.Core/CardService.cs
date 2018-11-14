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
        public async Task<ResponseModel> SaveCardNumber(CardInputViewModel cardInputViewModel)
        {
            try
            {
                var validateCard = ValidateCard(cardInputViewModel.cardNumber, cardInputViewModel.expiryDate);
                if (validateCard.result == "Valid")
                {
                    var cardType = validateCard.CardType;
                    var cardNum = await cardNumberRepo.Table.Where(x => x.CNumber == cardInputViewModel.cardNumber).SingleOrDefaultAsync();
                    if (cardNum == null)
                    {
                        CardTypeEnum cardTypeEnum = validateCard.CardType.ToEnum<CardTypeEnum>();
                        CardNumber cardNumber = new CardNumber
                        {
                            Id = 0,
                            CNumber = cardInputViewModel.cardNumber,
                            CardTypeId = (long)cardTypeEnum,
                            Active = true,
                            Created = DateTime.Now,
                            Modified = DateTime.Now,
                            Author = 1,
                            Editor = 1
                        };
                        await cardNumberRepo.Insert(cardNumber);
                    }
                    else
                    {
                        CardTypeEnum cardTypeEnum = cardInputViewModel.cardNumber.ToEnum<CardTypeEnum>();
                        CardNumber cardNumber = new CardNumber
                        {
                            Id = cardNum.Id,
                            CNumber = cardInputViewModel.cardNumber,
                            CardTypeId = (long)cardTypeEnum,
                            Active = true,
                            Created = cardNum.Created,
                            Modified = DateTime.Now,
                            Author = cardNum.Author,
                            Editor = 1
                        };
                        await cardNumberRepo.Update(cardNumber);
                    }


                }
                return HelperClass.Response(true
                                  , GlobalDecleration._successAction
                                  , null
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
        public async Task<ResponseModel> CheckValidateCard(CardInputViewModel cardInputViewModel)
        {
            try
            {
                var result = ValidateCard(cardInputViewModel.cardNumber, cardInputViewModel.expiryDate);
                if (result.result == "Valid")
                {
                    var cardNumber = await cardNumberRepo.Table.Where(x => x.CNumber == cardInputViewModel.cardNumber).SingleOrDefaultAsync();
                    if (cardNumber == null)
                    {
                        result.result = "Does not exist";
                    }
                }
                return HelperClass.Response(true
                                    , GlobalDecleration._successAction
                                    , result
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
        public async Task<ResponseModel> CheckValidateCardSP(CardInputViewModel cardInputViewModel)
        {
            try
            {
                IEnumerable<string> cardTypes =
            cardRepo.ValidateCardWithStoreProcedure(
            "EXEC [dbo].[validateCardType] @CNumber",
            new SqlParameter("@CNumber", SqlDbType.NVarChar) { Value = cardInputViewModel.cardNumber }
            );
               
                var cardType = "";
                if (cardTypes != null)
                {
                    cardType = cardTypes.Take(1).FirstOrDefault();
                }


                CardResponse cardResponse = new CardResponse
                {
                    result = "",
                    CardType = cardType

                };

                return HelperClass.Response(true
                                    , GlobalDecleration._successAction
                                    , cardResponse
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

        public async Task<ResponseModel> InsertUpdate(CardInputViewModel cardInputViewModel)
        {
            try
            {
                var validateCard = ValidateCard(cardInputViewModel.cardNumber, cardInputViewModel.expiryDate);
                if (validateCard.result == "Valid")
                {
                    CardNumber cardNumber = new CardNumber();
                    var cardType = validateCard.CardType;
                    CardTypeEnum cardTypeEnum = cardType.ToEnum<CardTypeEnum>();
                    var cardNum = await cardNumberRepo.Table.Where(x => x.CNumber == cardInputViewModel.cardNumber).SingleOrDefaultAsync();

                    if (cardNum == null)
                    {
                        cardNumber.Id = 0;
                        cardNumber.CNumber = cardInputViewModel.cardNumber;
                        cardNumber.CardTypeId = (long)cardTypeEnum;
                        cardNumber.Expiry = cardInputViewModel.expiryDate;
                        cardNumber.Active = true;
                        cardNumber.Created = DateTime.Now;
                        cardNumber.Modified = DateTime.Now;
                        cardNumber.Author = 1;
                        cardNumber.Editor = 1;
                    }

                    else
                    {
                        cardNumber.Id = cardNum.Id;
                        cardNumber.CNumber = cardInputViewModel.cardNumber;
                        cardNumber.CardTypeId = (long)cardTypeEnum;
                        cardNumber.Expiry = cardInputViewModel.expiryDate;
                        cardNumber.Active = true;
                        cardNumber.Created = cardNum.Created;
                        cardNumber.Modified = DateTime.Now;
                        cardNumber.Author = cardNum.Author;
                        cardNumber.Editor = 1;

                    }
                    var result = cardRepo.InsertUpdate(cardNumber);
                    return HelperClass.Response(true
                                                 , GlobalDecleration._savedSuccesfully
                                                 , result
                                             );
                }
                else
                {
                    return HelperClass.Response(false
                                                , GlobalDecleration._internalServerError
                                                , "Invalid Card"
                                            );
                }
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
     
        public async Task<ResponseModel> GetAllCardNumbers()
        {
            try
            {
                var results = await cardNumberRepo.Table.ToListAsync();
                return HelperClass.Response(true
                                    , GlobalDecleration._successAction
                                    , results
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
        public async Task<ResponseModel> GetAllCardNumbersUsingSP()
        {
            try
            {

                var cardNumbers = cardNumberRepo.ExecWithStoreProcedure("EXEC GetAllCardNumber").ToList();
                //var results = await cardNumberRepo.Table.ToListAsync();
                return HelperClass.Response(true
                                    , GlobalDecleration._successAction
                                    , cardNumbers
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
        public async Task<ResponseModel> GetAllCardTypes()
        {
            try
            {
                var results = await cardTypeRepo.Table.ToListAsync();
                var cardTypes = results.ConvertToViews();

                return HelperClass.Response(true
                                    , GlobalDecleration._successAction
                                    , cardTypes
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
        public async Task<ResponseModel> GeTCardNumberById(long Id)
        {
            try
            {
                var result = await cardNumberRepo.Get(Id);
                var cardType = result.CardType.ConvertToView();
                var cardNumber = result.ConvertToView();
                cardNumber.cardTypeModel = cardType;
                return HelperClass.Response(true
                                    , GlobalDecleration._successAction
                                    , cardNumber
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


        public CardResponse ValidateCard(string cardNumber, string expiry)
        {
            CardResponse cardResponse = new CardResponse();
            try
            {
                var number = Convert.ToInt64(cardNumber);
                if (cardNumber.Length == 16)
                {
                    if (cardNumber.Substring(0, 1) == "4")
                    {
                        var year = expiry.Substring(2, 4);
                        if (HelperClass.IsLoapYear(Convert.ToInt16(year)))
                        {
                            cardResponse = new CardResponse
                            {
                                result = "Valid",
                                CardType = CardTypeEnum.Visa.ToString()

                            };
                        }
                        else
                        {
                            cardResponse = new CardResponse
                            {
                                result = "InValid",
                                CardType = CardTypeEnum.Visa.ToString()

                            };
                        }
                        return cardResponse;
                    }
                    else if (cardNumber.Substring(0, 1) == "5")
                    {
                        var year = expiry.Substring(2, 4);
                        if (HelperClass.IsPrime(Convert.ToInt16(year)))
                        {
                            cardResponse = new CardResponse
                            {
                                result = "Valid",
                                CardType = CardTypeEnum.Master.ToString()

                            };
                        }
                        else
                        {
                            cardResponse = new CardResponse
                            {
                                result = "InValid",
                                CardType = CardTypeEnum.Master.ToString()

                            };
                        }
                        return cardResponse;
                    }
                    else if (cardNumber.Substring(0, 8) == "35283589")
                    {
                        cardResponse = new CardResponse
                        {
                            result = "Valid",
                            CardType = CardTypeEnum.JCB.ToString()

                        };
                        return cardResponse;
                    }
                    else
                    {
                        cardResponse = new CardResponse
                        {
                            result = "Valid",
                            CardType = CardTypeEnum.Unknown.ToString()

                        };
                        return cardResponse;
                    }
                }
                else if (cardNumber.Length == 15)
                {
                    if (cardNumber.Substring(0, 2) == "34" || cardNumber.Substring(0, 2) == "37")
                    {
                        cardResponse = new CardResponse
                        {
                            result = "Valid",
                            CardType = CardTypeEnum.Amex.ToString()

                        };
                    }
                    return cardResponse;
                }
                else
                {
                    cardResponse = new CardResponse
                    {
                        result = "Invalid",
                        CardType = null

                    };
                    return cardResponse;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                cardResponse = new CardResponse
                {
                    result = "Invalid",
                    CardType = null

                };
                return cardResponse;
            }
        }

    }
}
