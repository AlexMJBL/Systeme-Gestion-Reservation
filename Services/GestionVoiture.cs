
using Demo;
using Gestion_Reservation.Services;
using ProjetReservation.Models;
using Reservations.Web.Models;

namespace ProjetReservation.Services
{


    public class GestionVoiture : IGestion<Voiture>, IData
    {
        private List<Voiture> _listeVoitures;
        public string Chemin { get; }

        public GestionVoiture()
        {
            Chemin = @".\listeVoiture.txt";
            _listeVoitures = new List<Voiture>();
            Charger();
        }

        public List<Voiture> ObtenirTout()
        {
            return _listeVoitures;
        }

        public Voiture? ObtenirParId(int? id)
        {
            return _listeVoitures.SingleOrDefault(c => c.Id == id);
        }

        public int ObtenirProchainId()
        {

            if (_listeVoitures.Any())
            {
                return _listeVoitures.Max(c => c.Id) + 1;
            }
            else
            {
                return 1;
            }
        }

        public void Ajouter(Voiture voiture)
        {
            _listeVoitures.Add(voiture);
            Enregistrer();
        }

        public void Modifier(Voiture voiture)
        {
            Voiture? voitureAModifier = _listeVoitures.SingleOrDefault(c => c.Id == voiture.Id);
            if (voitureAModifier != null)
            {
                voitureAModifier.Description = voiture.Description;
                voitureAModifier.PrixJournalier = voiture.PrixJournalier;
                voitureAModifier.Marque = voiture.Marque;
                voitureAModifier.Modele = voiture.Modele;
                voitureAModifier.AnneeDeFabrication = voiture.AnneeDeFabrication;
                voitureAModifier.NombreDePortes = voiture.NombreDePortes;
                voitureAModifier.NombreDePlaces = voiture.NombreDePlaces;

            }
            Enregistrer();
        }

        public void Supprimer(int? id)
        {
            Voiture? voitureASupprimer = _listeVoitures.SingleOrDefault(c => c.Id == id);
            if (voitureASupprimer != null)
            {
                _listeVoitures.Remove(voitureASupprimer);
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
                    Voiture voiture = new Voiture();
                    voiture.Id = int.Parse(champs[0]);
                    voiture.Description = champs[1];
                    voiture.PrixJournalier = int.Parse(champs[2]);
                    voiture.Marque = (Marques)Enum.Parse(typeof(Marques), champs[3]);
                    voiture.Modele = champs[4];
                    voiture.AnneeDeFabrication = int.Parse(champs[5]);
                    voiture.NombreDePortes = int.Parse(champs[6]);
                    voiture.NombreDePlaces = int.Parse(champs[7]);

                    _listeVoitures.Add(voiture);
                }

            }
        }

        public void Enregistrer()
        {

            using (StreamWriter sw = new StreamWriter(Chemin))
            {
                sw.WriteLine("Id;Desription;Prix-Journalier;Marque;Modele;Annee-De-Fabrication;Nombre-De-Portes;Nombre-De-Places");
                foreach (Voiture voiture in _listeVoitures)
                {
                    sw.WriteLine(voiture.ToStringCSV());
                }
            }
        }

    }

}
