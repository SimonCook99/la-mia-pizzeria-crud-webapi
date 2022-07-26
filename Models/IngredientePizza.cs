using la_mia_pizzeria_static.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models{

    public class IngredientePizza{

        public int IngredientsId { get; set; }
        public int PizzasId { get; set; }

        public IngredientePizza(){

        }
    }
}