using Demo;
using Gestion_Reservation.Services;
using ProjetReservation.Models;
using ProjetReservation.Services;


namespace Reservations.Web.Services
{
    public class GestionReservationVoiture : GestionReservation<ReservationVoiture, Voiture>, IData
    {
       


        public GestionReservationVoiture() : base(@".\reservationVoitures.txt")
        {
            GestionVoiture gestionVoiture = new GestionVoiture();
            Reservables = FiltrerVoiture10Ans(gestionVoiture.ObtenirTout());
            Charger();
        }
       

        
        public bool EstReserver(ReservationVoiture reservation)
        {
            return Reservations.Any(r =>
            r.VoitureId == reservation.VoitureId &&
            r.Id != reservation.Id &&
            r.DateDebut < reservation.DateFin &&
            r.DateFin > reservation.DateDebut
        );
        }

        
        public override void Modifier(ReservationVoiture reservationVoiture)
        {

            base.Modifier(reservationVoiture);

            ReservationVoiture reservationAModifie = Reservations.SingleOrDefault(p => p.Id == reservationVoiture.Id);

            if (reservationAModifie != null)
            {
                reservationAModifie.Voiture = reservationVoiture.Voiture;
                reservationAModifie.VoitureId = reservationVoiture.VoitureId;
            }
            Enregistrer();
        }

        public override void Enregistrer()
        {
            using (StreamWriter sw = new StreamWriter(Chemin))
            {
                sw.WriteLine("Id;Prenom-Client;Nom-Client;Telephone-Client;Date-De-Debut;Date-De-Fin;Voiture-Id");
                foreach (ReservationVoiture reservation in Reservations)
                {
                    sw.WriteLine(
                        reservation.Id + ";" +
                        reservation.PrenomClient + ";" +
                        reservation.NomClient + ";" +
                        reservation.TelephoneClient + ";" +
                        reservation.DateDebut + ";" +
                        reservation.DateFin + ";" +
                        reservation.VoitureId + ";"
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
                    ReservationVoiture reservation = new ReservationVoiture
                    {
                        Id = int.Parse(champs[0]),
                        PrenomClient = champs[1],
                        NomClient = champs[2],
                        TelephoneClient = champs[3],
                        DateDebut = DateTime.Parse(champs[4]),
                        DateFin = DateTime.Parse(champs[5]),
                        VoitureId = int.Parse(champs[6]),
                        Voiture = Reservables.SingleOrDefault(v => v.Id == int.Parse(champs[6]))
                        
                    };
                    Reservations.Add(reservation);
                }
            }
        }

        private List<Voiture> FiltrerVoiture10Ans(List<Voiture> voiture)
        {
            return voiture.Where(v => v.AnneeDeFabrication >= DateTime.Now.Year - 10).ToList();
        }


    }
}
