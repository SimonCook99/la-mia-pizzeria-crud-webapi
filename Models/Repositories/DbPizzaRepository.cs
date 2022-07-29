using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models.Repositories{
    public class DbPizzaRepository : IPizzaRepository{
        public void Create(Pizza pizza){
            using (PizzaContext context = new PizzaContext()){
                pizza.Ingredients = new List<Ingrediente>();
               
                context.Pizzas.Add(pizza);
                context.SaveChanges();
            }
        }

        public void Delete(Pizza pizza){
            using (PizzaContext context = new PizzaContext()){
                context.Pizzas.Remove(pizza);
                context.SaveChanges();
            }
        }

        public Pizza GetById(int id){
            using (PizzaContext context = new PizzaContext())
            {
                Pizza FoundPizza = context.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();
                return FoundPizza;
            }
        }

        public List<Pizza> GetList(){
            using (PizzaContext context = new PizzaContext()){
                IQueryable<Pizza> pizzas = context.Pizzas;
                return pizzas.ToList();
            }
        }


        public void Update(Pizza pizza){
            using (PizzaContext context = new PizzaContext()){

                pizza.Category = context.Categories.Where(c => c.Id == pizza.CategoryId).FirstOrDefault();
                context.Attach(pizza);
                
                pizza.Ingredients.Clear();

                context.Update(pizza);
                context.SaveChanges();
            }
        }

        public List<Pizza> GetListByFilter(string search){
            using (PizzaContext context = new PizzaContext())
            {
                IQueryable<Pizza> pizzas = context.Pizzas;
                if (search != null){
                    pizzas = pizzas.Where(p => p.Nome.ToLower().Contains(search.ToLower()));
                }
                return pizzas.ToList();
            }
        }
    }
}

