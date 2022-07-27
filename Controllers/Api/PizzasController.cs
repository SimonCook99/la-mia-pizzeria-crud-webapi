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
        public IActionResult Get(string? search){
            using (PizzaContext context = new PizzaContext()) {

                IQueryable<Pizza> pizze;

                if (search == null || search == ""){
                    pizze = context.Pizzas.Include(p => p.Category);
                }
                else{
                    pizze = context.Pizzas.Where(p => p.Nome.Contains(search)).Include(p => p.Category);

                }
                return Ok(pizze.ToList());
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id){
            using (PizzaContext context = new PizzaContext()){
                Pizza pizza = context.Pizzas.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();

                if (pizza == null){
                    return NotFound();
                }

                return Ok(pizza);
            }

                
        }
    }
}
