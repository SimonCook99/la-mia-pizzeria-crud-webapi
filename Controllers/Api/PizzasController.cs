using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers.Api{

    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase{

        [HttpGet]
        public IActionResult Search(string? search){
            using (PizzaContext context = new PizzaContext()) {

                IQueryable<Pizza> pizze;

                if (search == null){
                    pizze = context.Pizzas.Include(p => p.Category);
                }
                else{
                    pizze = context.Pizzas.Where(p => p.Nome.Contains(search)).Include(p => p.Category);

                }
                return Ok(pizze.ToList());
            }
        }
    }
}
