using ProjetReservation.Models;

namespace Gestion_Reservation.Services
{
    public interface IGestionReservation<TReservation> where TReservation : Reservation
    {
       TReservation ObtenirParId(int? id);
        List<TReservation> ObtenirTout();
        bool EstReserver(TReservation reservation);
        void Ajouter(TReservation reservation);
        void Modifier(TReservation reservation);
        void Supprimer(int? id);

        int GenererId();
        


    }
}
