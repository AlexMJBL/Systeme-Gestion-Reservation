using Demo;
using Gestion_Reservation.Services;
using ProjetReservation.Models;
using ProjetReservation.Services;

namespace Gestion_Reservation.Services
{
    public class GestionReservationChambreHotel : IGestionReservation<ReservationChambreHotel>, IData
    {
        private List<ReservationChambreHotel> ReservationsChambreHotel;
        public List<ChambreHotel> ChambresHotel;
        public string Chemin { get; }

        public GestionReservationChambreHotel()
        {
            Chemin = @"./reservationChambresHotels.txt";
            ReservationsChambreHotel = new List<ReservationChambreHotel>();
            ChambresHotel = new List<ChambreHotel>();
            GestionChambreHotel gestionChambreHotel = new GestionChambreHotel();
            ChambresHotel = gestionChambreHotel.ObtenirTout();
            Charger();
        }

        public ReservationChambreHotel ObtenirParId(int? id)
        {
            return ReservationsChambreHotel.SingleOrDefault(p => p.Id == id);
        }

        public List<ReservationChambreHotel> ObtenirTout()
        {
            return ReservationsChambreHotel;
        }

        public bool EstReserver(ReservationChambreHotel reservation)
        {
            return ReservationsChambreHotel.Where(r => r.ChambreId == reservation.ChambreId && r.Id != reservation.Id).Any(r => r.DateDebut < reservation.DateFin && r.DateFin > reservation.DateDebut);
        }
        public void Ajouter(ReservationChambreHotel reservation)
        {
            ReservationsChambreHotel.Add(reservation);
            Enregistrer();
        }

        public void Modifier(ReservationChambreHotel reservation)
        {
            ReservationChambreHotel reservationAModifie = ReservationsChambreHotel.SingleOrDefault(p => p.Id == reservation.Id);
            if (reservationAModifie != null)
            {
                reservationAModifie.DateDebut = reservation.DateDebut;
                reservationAModifie.DateFin = reservation.DateFin;
                reservationAModifie.ChambreId = reservation.ChambreId;
                reservationAModifie.ChambreHotel = reservation.ChambreHotel;    
                reservationAModifie.PrenomClient = reservation.PrenomClient;
                reservationAModifie.NomClient = reservation.NomClient;
                reservationAModifie.TelephoneClient = reservation.TelephoneClient;
                

                Enregistrer();
            }

        }

        public void Supprimer(int? id)
        {
            ReservationChambreHotel reservationASupprimer = ReservationsChambreHotel.SingleOrDefault(p => p.Id == id);
            if (reservationASupprimer != null)
            {
                ReservationsChambreHotel.Remove(reservationASupprimer);
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
            } while (ReservationsChambreHotel.Any(r => r.Id == id));
            return id;
        }

        public void Enregistrer()
        {
            using (StreamWriter sw = new StreamWriter(Chemin))
            {
                sw.WriteLine("Id;Prenom-Client;Nom-Client;Telephone-Client;Date-De-Debut;Date-De-Fin;Chambre-Id");
                foreach (ReservationChambreHotel reservation in ReservationsChambreHotel)
                {
                        sw.WriteLine(
                            reservation.Id + ";" +
                            reservation.PrenomClient + ";" +
                            reservation.NomClient + ";" +
                            reservation.TelephoneClient + ";" +
                            reservation.DateDebut + ";" +
                            reservation.DateFin + ";" + 
                            reservation.ChambreId
                        );
                }
            }
        }

        public void Charger()
        {
            if (File.Exists(Chemin))
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
                            ChambreHotel = ChambresHotel.FirstOrDefault(c => c.Id == int.Parse(champs[6])),
                            ChambreId = int.Parse(champs[6])
                        };
                        ReservationsChambreHotel.Add(reservation);
                    }
                }
            }
        }
    }

}
