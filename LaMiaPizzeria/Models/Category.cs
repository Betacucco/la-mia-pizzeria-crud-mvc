using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaMiaPizzeria.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo e' obbligatorio")]
        [StringLength(60, ErrorMessage = "Il nome della categoria non deve superare 60 caratteri")]
        public string Name { get; set; }

        //Altrimenti si crea un ciclo infinito sul Json
        [JsonIgnore]
        public List<Pizza> Pizze { get; set; }

        public Category()
        {

        }
    }
}
