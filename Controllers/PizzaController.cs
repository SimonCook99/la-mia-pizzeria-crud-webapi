using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Partial;
using la_mia_pizzeria_static.Models.Repositories;
using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        private IPizzaRepository PizzaRepository;

        public PizzaController(IPizzaRepository _PizzaRepository){ //riferimento a repository nel costruttore
            this.PizzaRepository = _PizzaRepository;
        }

        public IActionResult Index(){
            List<Pizza> pizze = PizzaRepository.GetList();
            ViewData["title"] = "Menù pizze";
            return View(pizze);
        }

        public IActionResult Show(int id){
            Pizza pizzaResult = PizzaRepository.GetById(id);
            if (pizzaResult == null){
                return NotFound($"Non esiste nessuna pizza con l'id {id}");
            }
            else{
                return View(pizzaResult);

            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create(){

            using(PizzaContext context = new PizzaContext()){
                List<Category> categories = context.Categories.ToList();
                List<Ingrediente> ingredients = context.Ingredients.ToList();

                PizzaCategories model = new PizzaCategories();
                model.Categories = categories;
                model.Pizza = new Pizza();

                model.Ingredients = ingredients.Select(vm => new CheckboxItem()
                    {
                        Id = vm.Id,
                        Nome = vm.Nome,
                        IsChecked = false
                    }).ToList();


                return View("Create", model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategories model, List<CheckboxItem> ingredienti){
            using (PizzaContext context = new PizzaContext()){
                if (!ModelState.IsValid){
                    model.Categories = context.Categories.ToList();

                    model.Ingredients = context.Ingredients.ToList().Select(vm => new CheckboxItem()
                    {
                        Id = vm.Id,
                        Nome = vm.Nome,
                        IsChecked = false
                    }).ToList();

                    return View("Create", model);
                }

                model.Categories = context.Categories.Where(find => find.Id == model.Pizza.CategoryId).ToList();
                //model.Ingredients = context.Ingredients.I

                //foreach(string ingrediente in model.SelectedIngredients){

                //model.Ingredients = context.Ingredients.ToList();
                //}
                context.Pizzas.Add(model.Pizza);

                //ingredienti = (List<CheckboxItem>)ViewData["ingredienti"];

                //List<CheckboxItem> checkIngredients = new List<CheckboxItem>();
                //List<Ingrediente> Ingredients = context.Ingredients.ToList();

                ////da sistemare questo passaggio
                //foreach(Ingrediente item in Ingredients){

                //    checkIngredients.Add(new CheckboxItem() { Id = item.Id, Nome = item.Nome, IsChecked = false });
                //}

                foreach (CheckboxItem item in ingredienti){
                    if (item.IsChecked == true){
                        model.Ingredients.Add(item);
                    }
                }

                context.SaveChanges();
                return RedirectToAction("Index");
            }
                
            
        }


        [HttpGet]
        [Authorize]
        public IActionResult Update(int id){

            using (PizzaContext context = new PizzaContext()){
                Pizza pizza = PizzaRepository.GetById(id);
                List<Category> categories = context.Categories.ToList();
                List<Ingrediente> ingredients = context.Ingredients.ToList();

                PizzaCategories model = new PizzaCategories();
                model.Categories = categories;
                model.Pizza = pizza;
                //model.Ingredients = ingredients;

                //Da vedere perchè non mi passa gli ingredienti della pizza
                model.Pizza.Ingredients = pizza.Ingredients;

                if (pizza == null){
                    return NotFound();
                }

                return View("Update", model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //l'id ci serve a trovare la pizza nel db, mentre il model Pizza rappresenta la nuova 
        //entità con i dati modificati dall'utente
        public IActionResult Update(int id, PizzaCategories data){

            using (PizzaContext context = new PizzaContext()){

                if (!ModelState.IsValid){
                    data.Categories = context.Categories.ToList();

                    data.Ingredients = context.Ingredients.ToList().Select(vm => new CheckboxItem()
                    {
                        Id = vm.Id,
                        Nome = vm.Nome,
                        IsChecked = false
                    }).ToList();

                    return View("Update", data);
                }

                Pizza oldPizza = context.Pizzas.Find(id);

                if(oldPizza == null){
                    return NotFound();
                }

                oldPizza.Nome = data.Pizza.Nome;
                oldPizza.Descrizione = data.Pizza.Descrizione;
                oldPizza.Immagine = data.Pizza.Immagine;
                oldPizza.Prezzo = data.Pizza.Prezzo;
                oldPizza.Category = data.Pizza.Category;
                oldPizza.CategoryId = data.Pizza.CategoryId;

                context.Pizzas.Update(oldPizza);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id){
            using (PizzaContext context = new PizzaContext())
            {

                Pizza pizza = PizzaRepository.GetById(id);

                if (pizza == null){
                    return NotFound();
                }

                PizzaRepository.Delete(pizza);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
        }
    }
}
