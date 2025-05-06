using System.ComponentModel.DataAnnotations;

namespace Gestion_Reservation.Models
{
    public abstract class Reservable
    {
        [Display(Name = "Identifiant")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Il doit avoir une description !")]
        [StringLength(100, ErrorMessage = "La description ne doit pas exceder 100 caracteres !")]
        public string Description { get; set; }

        [Display(Name = "Prix Journalier")]
        [Required(ErrorMessage = "Il doit avoir un prix journalier!")]
        [Range(1, 500, ErrorMessage = "Le prix journalier doit etre entre 1 et 500 dollars !")]
        public int PrixJournalier { get; set; }

        public string ToStringCSV()
        {
            return $"{Id};{Description};{PrixJournalier}";
        }
    }
}
