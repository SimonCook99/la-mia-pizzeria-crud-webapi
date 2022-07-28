using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase{

        [HttpPost]
        public IActionResult Post(Messaggio model){

            if (!ModelState.IsValid){
                return UnprocessableEntity(ModelState);
            }

            using (PizzaContext context = new PizzaContext()){
                context.Messages.Add(model);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
