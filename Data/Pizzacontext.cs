using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data{
    public class PizzaContext : IdentityDbContext<IdentityUser>{

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingrediente> Ingredients { get; set; }
        public DbSet<Messaggio> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizzeria;Integrated Security=True");

        }

        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options){

        }

        public PizzaContext(){

        }
    }
}
