namespace la_mia_pizzeria_static.Models.Repositories.Interfaces{
    public interface IPizzaRepository{
        public List<Pizza> GetList();
        public Pizza GetById(int id);
        public void Create(Pizza post);
        public void Update(Pizza post);
        public void Delete(Pizza post);


        public List<Pizza> GetListByFilter(string search); //ricerca per l'APIcontroller
    }
}
