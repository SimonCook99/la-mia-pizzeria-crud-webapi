using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api{

    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase{

        [HttpGet]
        public IActionResult Get(){
            using(PizzaContext context = new PizzaContext()){

                IQueryable<Pizza> pizze = context.Pizzas;
                
                return(Ok(pizze.ToList()));
            }
        }
    }
}
