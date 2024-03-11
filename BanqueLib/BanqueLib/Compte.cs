using System.Reflection.Metadata.Ecma335;

namespace BanqueLib
{
    public class Compte
    {
       private int numCompte;
       private string détenteur;
       private double solde;
       private bool compteGelée;

        public Compte(int numéros, string détenteur, double solde = 0.00, string status = "ok") 
        {
            ArgumentOutOfRangeException.ThrowIfNegative(numéros);
            ArgumentOutOfRangeException.ThrowIfNotEqual(solde, double.Round(solde, 2));
            numCompte = numéros;
            SetDétenteur(détenteur);
            switch (status.ToLower())
            {
                case "ok":
                    compteGelée = false;
                break;
                case "gelé":
                    compteGelée = true;
                break;
            }
        }

        public int GetNumCompte() => numCompte;

        public void SetDétenteur(string nom)
        {
            ArgumentException.ThrowIfNullOrEmpty(détenteur);
            détenteur = nom;
        }
        public string GetDétenteur() => détenteur;

        public double GetSolde() => solde;

        public bool GetCompteGelée() => compteGelée;

        public string description()
        {
            string description;
            int maxlongeur;
            return description;
        }
    }
}
