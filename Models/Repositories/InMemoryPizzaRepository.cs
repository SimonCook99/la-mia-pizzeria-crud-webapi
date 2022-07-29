using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models.Repositories{
    public class InMemoryPizzaRepository : IPizzaRepository{
        private static List<Pizza> Pizze = new List<Pizza>();

        public InMemoryPizzaRepository(){
            Pizze.Add(new Pizza("margherita", "pizza bella", "www.nonloso.it", 3.50));
            Pizze.Add(new Pizza("marinara", "pizza bella", "www.nonloso.it", 6.50));
            Pizze.Add(new Pizza("quattro stagioni", "pizza bella", "www.nonloso.it", 7.50));
            Pizze.Add(new Pizza("wrustel", "pizza bella", "www.nonloso.it", 5.30));
        }
        public void Create(Pizza pizza)
        {
            pizza.Id = Pizze.Count;
            
            pizza.Ingredients = new List<Ingrediente>();
            InMemoryPizzaRepository.Pizze.Add(pizza);
        }
        public void Delete(Pizza pizza)
        {
            int pizzaDaEliminare = -1;
            for (int i = 0; i < InMemoryPizzaRepository.Pizze.Count; i++)
            {
                Pizza pizzaToCheck = InMemoryPizzaRepository.Pizze[i];
                if (pizzaToCheck.Id == pizza.Id)
                {
                    pizzaDaEliminare = i;
                }
            }
            if (pizzaDaEliminare != -1)
            {
                InMemoryPizzaRepository.Pizze.RemoveAt(pizzaDaEliminare);
            }
        }
        public Pizza GetById(int id)
        {
            Pizza pizzaDaTrovare = null;
            for (int i = 0; i < InMemoryPizzaRepository.Pizze.Count; i++)
            {
                Pizza pizzaToCheck = InMemoryPizzaRepository.Pizze[i];
                if (pizzaToCheck.Id == id)
                {
                    pizzaDaTrovare = pizzaToCheck;
                }
            }
            return pizzaDaTrovare;
        }
        public List<Pizza> GetList()
        {
            return InMemoryPizzaRepository.Pizze;
        }
        public void Update(Pizza post)
        {
            int indicePizzaDaTrovare = -1;
            for (int i = 0; i < InMemoryPizzaRepository.Pizze.Count; i++)
            {
                Pizza postToCheck = InMemoryPizzaRepository.Pizze[i];
                if (postToCheck.Id == post.Id)
                {
                    indicePizzaDaTrovare = i;
                }
            }
            if (indicePizzaDaTrovare != -1)
            {
                InMemoryPizzaRepository.Pizze[indicePizzaDaTrovare] = post;
            }
        }

        public List<Pizza> GetListByFilter(string search){
            List<Pizza> pizzas = InMemoryPizzaRepository.Pizze;
            if (search != null)
            {
                pizzas = pizzas.Where(p => p.Nome.ToLower().Contains(search.ToLower())).ToList();
            }
            return pizzas.ToList();
        }
    }
}

