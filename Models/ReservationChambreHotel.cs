using Gestion_Reservation.Models;

namespace ProjetReservation.Models
{

    public class ReservationChambreHotel : Reservation
    {
        public ChambreHotel? ChambreHotel { get; set; }
        public int ChambreId { get; set; }
        public double Prix { get { return ChambreHotel != null ? ChambreHotel.PrixJournalier * (Math.Abs((DateFin - DateDebut).Days)) : 0; } }
    }
}

