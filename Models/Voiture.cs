using System.ComponentModel.DataAnnotations;
using Gestion_Reservation.Models;
using Reservations.Web.Models;

namespace ProjetReservation.Models
{
    public class Voiture : Reservable
    {
        [Required(ErrorMessage = "Vous devez entrer la marque de la voiture!")]
        [EnumDataType(typeof(Marques))]
        public Marques Marque { get; set; }

        [Display(Name = "Modèle")]
        [Required(ErrorMessage = "Vous devez entrer le modèle de la voiture!")]
        public string Modele { get; set; }

        [Display(Name = "Année de fabrication")]
        [Required(ErrorMessage = "Il doit avoir une année de fabrication")]
        public int AnneeDeFabrication { get; set; }

        [Display(Name = "Nombre de portes")]
        [Required(ErrorMessage = "Il doit avoir un nombre de portes")]
        [Range(2, 5, ErrorMessage = "Le nombre de portes doit etre entre 2 et 5")]
        public int NombreDePortes { get; set; }

        [Display(Name = "Nombre de places")]
        [Required(ErrorMessage = "Il doit avoir un nombre de places")]
        [Range(2, 7, ErrorMessage = "Le nombre de places doit etre entre 2 et 7")]
        public int NombreDePlaces { get; set; }

        public string ToStringCSV()
        { 
            return $"{base.ToStringCSV()};{Marque};{Modele};{AnneeDeFabrication};{NombreDePortes};{NombreDePlaces}";
        }
    }
}
