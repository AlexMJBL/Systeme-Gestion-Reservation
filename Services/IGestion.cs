using Gestion_Reservation.Models;
namespace Gestion_Reservation.Services
{
    public interface IGestion<TReservable> where TReservable : Reservable
    {
        List<TReservable> ObtenirTout();
        TReservable? ObtenirParId(int? id);  
        int ObtenirProchainId();
        void Ajouter(TReservable item); 
        void Modifier(TReservable item);  
        void Supprimer(int? id);
    }
}
