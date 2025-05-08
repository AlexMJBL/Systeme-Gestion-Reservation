using Demo;
using Gestion_Reservation.Models;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace Gestion_Reservation.Services
{
    public abstract class GestionReservation<TReservation, TReservable>
        where TReservation : Reservation
        where TReservable : Reservable
    {
        public List<TReservation> Reservations;
        public List<TReservable> Reservables;
        public string Chemin { get; }

        public GestionReservation(string chemin)
        {
            Chemin = chemin;
            Reservations = new List<TReservation>();
            Reservables = new List<TReservable>();
        }

        public TReservation ObtenirParId(int? id)
        {
            return Reservations.SingleOrDefault(p => p.Id == id);
        }
        public List<TReservation> ObtenirTout()
        {
            return Reservations;
        }

        public void Ajouter(TReservation reservation)
        {
            Reservations.Add(reservation);
            Enregistrer();
        }

        public virtual void Modifier(TReservation reservation)
        {
            TReservation reservationAModifie = Reservations.SingleOrDefault(p => p.Id == reservation.Id);
            if (reservationAModifie != null)
            {
                reservationAModifie.DateDebut = reservation.DateDebut;
                reservationAModifie.DateFin = reservation.DateFin;
                reservationAModifie.PrenomClient = reservation.PrenomClient;
                reservationAModifie.NomClient = reservation.NomClient;
                reservationAModifie.TelephoneClient = reservation.TelephoneClient;
            }
            Enregistrer();
        }

        public void Supprimer(int? id)
        {
            TReservation reservationASupprimer = Reservations.SingleOrDefault(p => p.Id == id);
            if (reservationASupprimer != null)
            {
                Reservations.Remove(reservationASupprimer);
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
            } while (Reservations.Any(r => r.Id == id));
            return id;
        }

        public virtual void Enregistrer()
        {
            using (StreamWriter sw = new StreamWriter(Chemin))
            {
                sw.WriteLine("Id;Prenom-Client;Nom-Client;Telephone-Client;Date-De-Debut;Date-De-Fin;Voiture-Id");
                foreach (TReservation reservation in Reservations)
                {
                    sw.WriteLine(
                        reservation.Id + ";" +
                        reservation.PrenomClient + ";" +
                        reservation.NomClient + ";" +
                        reservation.TelephoneClient + ";" +
                        reservation.DateDebut + ";" +
                        reservation.DateFin + ";"
                    );
                }
            }
        }

        public virtual void Charger()
        {
            using (StreamReader sr = new StreamReader(Chemin))
            {
                sr.ReadLine();
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    string[] champs = ligne.Split(';');
                    TReservation reservation = Activator.CreateInstance<TReservation>();

                    if (reservation != null)
                    {
                        reservation.Id = int.Parse(champs[0]);
                        reservation.PrenomClient = champs[1];
                        reservation.NomClient = champs[2];
                        reservation.TelephoneClient = champs[3];
                        reservation.DateDebut = DateTime.Parse(champs[4]);
                        reservation.DateFin = DateTime.Parse(champs[5]);

                        Reservations.Add(reservation);
                    }

                    Reservations.Add(reservation);
                }
            }
        }
    }
}
