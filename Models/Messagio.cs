using la_mia_pizzeria_static.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace la_mia_pizzeria_static.Models{

    public class Messaggio{

        [Key]
        public int Id { get; set; }

        [Required]
        public string Titolo { get; set; }


        [Column(TypeName = "Text")]
        [StringLength(300, ErrorMessage = "Il campo seguente non deve superare i 300 caratteri")]
        public string? Commento { get; set; }


        public Messaggio(){

        }
    }
}