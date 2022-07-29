using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers.Api{

    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase{

        private IPizzaRepository PizzaRepository;

        public PizzasController(IPizzaRepository _pizzaRepository){
            this.PizzaRepository = _pizzaRepository;
        }


        [HttpGet] //get per la ricerca (tramite input in frontend)
        public IActionResult Get(string? search){
            using (PizzaContext context = new PizzaContext()) {

                List<Pizza> pizze = this.PizzaRepository.GetListByFilter(search);
                return Ok(pizze.ToList());
            }
        }

        [HttpGet("{id}")] //Show della singola pizza
        public IActionResult Get(int id){
            using (PizzaContext context = new PizzaContext()){
                Pizza pizza = this.PizzaRepository.GetById(id);

                return Ok(pizza);
            }
     
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Pizza model){

            if (!ModelState.IsValid){
                return UnprocessableEntity(ModelState);
            }

            using (PizzaContext context = new PizzaContext()){
                Pizza pizza = context.Pizzas.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
            
                if(pizza == null){
                    return NotFound();
                }
                else{
                    pizza.Nome = model.Nome;
                    pizza.Prezzo = model.Prezzo;
                    pizza.Descrizione = model.Descrizione;
                    pizza.Category = model.Category;
                    pizza.CategoryId = model.CategoryId;
                    pizza.Immagine = model.Immagine;

                    context.Pizzas.Update(pizza);
                    context.SaveChanges();
                    return Ok(pizza);
                }
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            using (PizzaContext context = new PizzaContext()){
                Pizza PizzaDaRimuovere = context.Pizzas.Where(p => p.Id == id).FirstOrDefault();
                if (PizzaDaRimuovere != null){
                    context.Pizzas.Remove(PizzaDaRimuovere);
                    context.SaveChanges();
                    return Ok();
                }
                else{
                    return NotFound();
                }
            }
        }
    }
}
