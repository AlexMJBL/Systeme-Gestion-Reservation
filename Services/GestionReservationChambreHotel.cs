using Demo;
using Gestion_Reservation.Services;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace Gestion_Reservation.Services
{
    public class GestionReservationChambreHotel : GestionReservation<ReservationChambreHotel, ChambreHotel>, IData
    {
        public GestionReservationChambreHotel() : base(@".\reservationVoitures.txt")
        {
            GestionChambreHotel gestionChambreHotel = new GestionChambreHotel();
            Reservables = gestionChambreHotel.ObtenirTout();
            Charger();
        }



        public bool EstReserver(ReservationChambreHotel reservation)
        {
            return Reservations.Any(r =>
            r.ChambreId == reservation.ChambreId &&
            r.Id != reservation.Id &&
            r.DateDebut < reservation.DateFin &&
            r.DateFin > reservation.DateDebut
        );
        }


        public override void Modifier(ReservationChambreHotel reservationChambreHotel)
        {

            base.Modifier(reservationChambreHotel);

            ReservationChambreHotel reservationAModifie = Reservations.SingleOrDefault(p => p.Id == reservationChambreHotel.Id);

            if (reservationAModifie != null)
            {
                reservationAModifie.ChambreHotel = reservationChambreHotel.ChambreHotel;
                reservationAModifie.ChambreId = reservationChambreHotel.ChambreId;
            }
            Enregistrer();
        }

        public override void Enregistrer()
        {
            using (StreamWriter sw = new StreamWriter(Chemin))
            {
                sw.WriteLine("Id;Prenom-Client;Nom-Client;Telephone-Client;Date-De-Debut;Date-De-Fin;Chambre-Id");
                foreach (ReservationChambreHotel reservation in Reservations)
                {
                    sw.WriteLine(
                        reservation.Id + ";" +
                        reservation.PrenomClient + ";" +
                        reservation.NomClient + ";" +
                        reservation.TelephoneClient + ";" +
                        reservation.DateDebut + ";" +
                        reservation.DateFin + ";" +
                        reservation.ChambreId + ";"
                    );
                }
            }
        }

        public override void Charger()
        {
            using (StreamReader sr = new StreamReader(Chemin))
            {
                sr.ReadLine();
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    string[] champs = ligne.Split(';');
                    ReservationChambreHotel reservation = new ReservationChambreHotel
                    {
                        Id = int.Parse(champs[0]),
                        PrenomClient = champs[1],
                        NomClient = champs[2],
                        TelephoneClient = champs[3],
                        DateDebut = DateTime.Parse(champs[4]),
                        DateFin = DateTime.Parse(champs[5]),
                        ChambreId = int.Parse(champs[6]),
                        ChambreHotel = Reservables.SingleOrDefault(v => v.Id == int.Parse(champs[6]))

                    };
                    Reservations.Add(reservation);
                }
            }
        }
    }

}
