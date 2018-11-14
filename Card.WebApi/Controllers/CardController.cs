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
        [Route("GetCardTypes")]
        public async Task<IHttpActionResult> GetCardTypes()
        {
            return Ok(await cardService.GetCardTypes());
        }
    }
}
