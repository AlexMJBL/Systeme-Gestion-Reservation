using Demo;
using Gestion_Reservation.Models;
using Gestion_Reservation.Services;
using ProjetReservation.Models;


namespace ProjetReservation.Services
{
    public class GestionChambreHotel : IGestion<ChambreHotel>, IData
    {
        private List<ChambreHotel> _listeChambre;
        public string Chemin { get; }

        public GestionChambreHotel()
        {
            Chemin = @".\listeChambre.txt";
            _listeChambre = new List<ChambreHotel>();
            Charger();
        }

        public List<ChambreHotel> ObtenirTout()
        {
            return _listeChambre;
        }

        public ChambreHotel? ObtenirParId(int? id)
        {
            return _listeChambre.FirstOrDefault(c => c.Id == id);
        }

        public int ObtenirProchainId()
        {
            if (_listeChambre.Any())
            {
                return _listeChambre.Max(c => c.Id) + 1;
            }
            else
            {
                return 1;
            }
        }

        public void Ajouter(ChambreHotel chambre)
        {
            _listeChambre.Add(chambre);
            Enregistrer();
        }

        public void Modifier(ChambreHotel chambre)
        {
            ChambreHotel? chambreAModifier = _listeChambre.FirstOrDefault(c => c.Id == chambre.Id);
            if (chambreAModifier != null)
            {
                chambreAModifier.Description = chambre.Description;
                chambreAModifier.PrixJournalier = chambre.PrixJournalier;
                chambreAModifier.TypeChambre = chambre.TypeChambre;
                chambreAModifier.NombreLits = chambre.NombreLits;
                chambreAModifier.Climatisation = chambre.Climatisation;
                chambreAModifier.GrandeurM2 = chambre.GrandeurM2;
                chambreAModifier.Balcon = chambre.Balcon;

            }
            Enregistrer();
        }

        public void Supprimer(int? id)
        {
            ChambreHotel? chambreASupprimer = _listeChambre.FirstOrDefault(c => c.Id == id);
            if (chambreASupprimer != null)
            {
                _listeChambre.Remove(chambreASupprimer);
            }
            Enregistrer();
        }

        
        public void Charger()
        {
            if (!File.Exists(Chemin))
            {
                File.Create(Chemin).Close();
                return;
            }
            using (StreamReader sr = new StreamReader(Chemin))
            {
                sr.ReadLine();
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    string[] champs = ligne.Split(";");
                    ChambreHotel chambreHotel = new ChambreHotel();
                    chambreHotel.Id = int.Parse(champs[0]);
                    chambreHotel.Description = champs[1];
                    chambreHotel.PrixJournalier = int.Parse(champs[2]);
                    chambreHotel.TypeChambre = (TypeChambre)Enum.Parse(typeof(TypeChambre), champs[3]);
                    chambreHotel.NombreLits = int.Parse(champs[4]);
                    chambreHotel.Climatisation = bool.Parse(champs[5]);
                    chambreHotel.GrandeurM2 = int.Parse(champs[6]);
                    chambreHotel.Balcon = bool.Parse(champs[7]);

                    _listeChambre.Add(chambreHotel);
                }

            }
        }

        public void Enregistrer()
        {
            using (StreamWriter sw = new StreamWriter(Chemin))
            {
                sw.WriteLine("Id;Desription;Prix-Journalier;TypeChambre;NombreLits;Climatisation;GrandeurM2;Balcon");
                foreach (ChambreHotel chambre in _listeChambre)
                {
                    sw.WriteLine(chambre.ToStringCSV());
                }
            }
        }

    }
}


