using System.ComponentModel.DataAnnotations;

namespace ProjetReservation.Models
{
    public abstract class Reservation
    {

        [Display(Name = "Identifiant")]
        public int Id { get; set; }

        [Display(Name = "Prénom du client")]
        [Required(ErrorMessage = "Il doit avoir un prénom !")]
        [StringLength(50, ErrorMessage = "Le prénom ne doit pas exceder 50 caracteres !")]
        public string PrenomClient { get; set; }

        [Display(Name = "Nom du client")]
        [Required(ErrorMessage = "Il doit avoir un nom !")]
        [StringLength(50, ErrorMessage = "Le nom ne doit pas exceder 50 caracteres !")]
        public string NomClient { get; set; }

        [Display(Name = "Numéro de téléphone")]
        [Required(ErrorMessage = "Il doit avoir un numéro de téléphone !")]
        [StringLength(10, ErrorMessage = "Le numéro de téléphone ne doit pas exceder 10 chiffres !")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Le numéro de téléphone doit contenir 10 chiffres !")]
        public string TelephoneClient { get; set; }

        [Display(Name = "Date de début")]
        public DateTime DateDebut { get; set; }
        [Display(Name = "Date de fin")]
        public DateTime DateFin { get; set; }

        

    }
}