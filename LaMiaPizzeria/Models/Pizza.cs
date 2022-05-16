using LaMiaPizzeria.Utilis.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaMiaPizzeria.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non può avere più di 25 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo descrizione è obbligatorio")]
        [Column(TypeName = "text")]
        [MoreThanFiveWordValidation]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo immagine è obbligatorio")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Il campo prezzo è obbligatorio")]
        [Range(3.00, 15.00, ErrorMessage = "Il prezzo non puo' essere minore di 3 o maggiore di 15 euro")]
        public double Price { get; set; }

        public Pizza()
        {

        }

        public Pizza(string name, string ingredienti, string image, double prezzo)
        {
            this.Name = name;
            this.Description = ingredienti;
            this.Image = image;
            this.Price = prezzo;
        }
    }
}
