using System.ComponentModel.DataAnnotations;
using Gestion_Reservation.Models;


namespace ProjetReservation.Models
{
    public class ChambreHotel : Reservable
    {
        [Required(ErrorMessage = "Vous devez entrer le type de chambre!")]
        [EnumDataType(typeof(TypeChambre))]
        [Display(Name = "Type de chambre")]
        public TypeChambre TypeChambre { get; set; }

        [Required(ErrorMessage = "Vous devez entrer le nombre de lits!")]
        [Range(1, 4, ErrorMessage = "Le nombre de lits doit etre entre 1 et 4!")]
        [Display(Name = "Nombre de lits")]
        public int NombreLits { get; set; }

        [Required(ErrorMessage = "Vous devez specifier si la chambre inclut la climatisation")]
        public bool Climatisation { get; set; }

        [Required(ErrorMessage = "Vous devez specifier la grandeur de la chambre")]
        public int GrandeurM2 { get; set; }

        [Required(ErrorMessage = "Vous devez specifier si la chambre possède un balcon")]
        public bool Balcon { get; set; }

        public string ToStringCSV()
        {
            return $"{base.ToStringCSV()};{TypeChambre};{NombreLits};{Climatisation};{GrandeurM2};{Balcon}";
        }
    }
}
