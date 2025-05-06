using Demo;
using Gestion_Reservation.Services;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace Reservations.Web.Services
{
    public class GestionReservationVoiture : IData , IGestionReservation<ReservationVoiture>
    {
        private List<ReservationVoiture> ReservationsVoiture;
        public List<Voiture> Voitures;
        public string Chemin { get; }


        public GestionReservationVoiture()
        {
            Chemin = @"./reservationVoitures.txt";
            ReservationsVoiture = new List<ReservationVoiture>();
            Voitures = new List<Voiture>();
            GestionVoiture gestionVoiture = new GestionVoiture();
            Voitures = FiltrerVoiture10Ans(gestionVoiture.ObtenirTout());

            Charger();
        }



        public ReservationVoiture ObtenirParId(int? id)
        {
            return ReservationsVoiture.SingleOrDefault(p => p.Id == id);
        }
        public List<ReservationVoiture> ObtenirTout()
        {
            return ReservationsVoiture;
        }

        public bool EstReserver(ReservationVoiture reservation)
        {
            return ReservationsVoiture.Where(r => r.VoitureId == reservation.VoitureId && r.Id != reservation.Id).Any(r => r.DateDebut < reservation.DateFin && r.DateFin > reservation.DateDebut);
        }

        public void Ajouter(ReservationVoiture reservation)
        {
            ReservationsVoiture.Add(reservation);

            Enregistrer();
        }
        public void Modifier(ReservationVoiture reservationVoiture)
        {

            ReservationVoiture reservationAModifie = ReservationsVoiture.SingleOrDefault(p => p.Id == reservationVoiture.Id);

            if (reservationAModifie != null)
            {
                reservationAModifie.DateDebut = reservationVoiture.DateDebut;
                reservationAModifie.DateFin = reservationVoiture.DateFin;
                reservationAModifie.Voiture = reservationVoiture.Voiture;
                reservationAModifie.VoitureId = reservationVoiture.VoitureId;
                reservationAModifie.PrenomClient = reservationVoiture.PrenomClient;
                reservationAModifie.NomClient = reservationVoiture.NomClient;
                reservationAModifie.TelephoneClient = reservationVoiture.TelephoneClient;

                Enregistrer();
            }
        }
        public void Supprimer(int? id)
        {
            ReservationVoiture reservationASupprimer = ReservationsVoiture.SingleOrDefault(p => p.Id == id);
            if (reservationASupprimer != null)
            {
                ReservationsVoiture.Remove(reservationASupprimer);
                Enregistrer();
            }
        }

        public int GenererId()
        {
            Random random = new Random();
            int id;
            do
            {
                id = random.Next(100000, 999999);
            } while (ReservationsVoiture.Any(r => r.Id == id));
            return id;
        }


        public void Enregistrer()
        {
            using (StreamWriter sw = new StreamWriter(Chemin))
            {
                sw.WriteLine("Id;Prenom-Client;Nom-Client;Telephone-Client;Date-De-Debut;Date-De-Fin;Voiture-Id");
                foreach (ReservationVoiture reservation in ReservationsVoiture)
                {
                    sw.WriteLine(
                        reservation.Id + ";" +
                        reservation.PrenomClient + ";" +
                        reservation.NomClient + ";" +
                        reservation.TelephoneClient + ";" +
                        reservation.DateDebut + ";" +
                        reservation.DateFin + ";" +
                        reservation.VoitureId
                    );
                }
            }
        }
       
        public void Charger()
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
                        Voiture = Voitures.SingleOrDefault(v => v.Id == int.Parse(champs[6]))
                        
                    };
                    ReservationsVoiture.Add(reservation);
                }
            }
        }

        private List<Voiture> FiltrerVoiture10Ans(List<Voiture> voiture)
        {
            return voiture.Where(v => v.AnneeDeFabrication >= DateTime.Now.Year - 10).ToList();
        }


    }
}
