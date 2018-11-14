using Card.Interface.IRepo;
using Card.Interface.IServices;
using Card.Utility.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Card.WebApi.Controllers
{
    [RoutePrefix("api/Card")]
    public class CardController : ApiController
    {
        private ICardService cardService;
        public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }
        [HttpGet]
        [Route("GetAllCardNumbers")]
        public async Task<IHttpActionResult> GetAllCardNumbers()
        {
            return Ok(await cardService.GetAllCardNumbers());
        }
        [HttpGet]
        [Route("GetAllCardNumbersUsingSP")]
        public async Task<IHttpActionResult> GetAllCardNumbersUsingSP()
        {
            return Ok(await cardService.GetAllCardNumbersUsingSP());
        }
        [HttpPost]
        [Route("SaveCardNumberBySP")]
        public async Task<IHttpActionResult> SaveCardNumberBySP(CardInputViewModel cardInputViewModel)
        {
            return Ok(await cardService.InsertUpdate(cardInputViewModel));
        }

        [Route("SaveCardNumber")]
        public async Task<IHttpActionResult> SaveCardNumber(CardInputViewModel cardInputViewModel)
        {
            return Ok(await cardService.SaveCardNumber(cardInputViewModel));
        }
        [HttpPost]
        [Route("CheckCard")]
        public async Task<IHttpActionResult> CheckCard(CardInputViewModel cardInputViewModel)
        {
            return Ok(await cardService.CheckValidateCard(cardInputViewModel));
        }
        [HttpPost]
        [Route("CheckCardUsingSP")]
        public async Task<IHttpActionResult> CheckCardUsingSP(CardInputViewModel cardInputViewModel)
        {
            return Ok(await cardService.CheckValidateCardSP(cardInputViewModel));
        }
        [HttpGet]
        [Route("GetCardNumberById")]
        public async Task<IHttpActionResult> GetCardNumberById(long Id)
        {
            return Ok(await cardService.GeTCardNumberById(Id));
        }
        [HttpGet]
        [Route("GetCardTypes")]
        public async Task<IHttpActionResult> GetCardTypes()
        {
            return Ok(await cardService.GetCardTypes());
        }

    }
}
