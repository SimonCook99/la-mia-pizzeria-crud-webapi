using la_mia_pizzeria_static.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace la_mia_pizzeria_static.Models{

    public class Category{

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [JsonIgnore]
        public List<Pizza> Pizzas { get; set; }

        public Category(string nome){
            Nome = nome;
        }

        public Category(){

        }
    }
}