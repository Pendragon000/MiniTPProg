using System.Reflection.Metadata.Ecma335;

namespace BanqueLib
{
    public class Compte
    {
       //#Region --Champs--
       private int numCompte;
       private string détenteur;
       private double solde;
       private bool compteGelé;
        //#EndRegion

        //#Region ---- initiateurs ----
        public Compte(int numéros, string détenteur, double solde = 0.00, string status = "ok") 
        {
            ArgumentOutOfRangeException.ThrowIfNegative(numéros);
            ArgumentOutOfRangeException.ThrowIfNotEqual(solde, double.Round(solde, 2));
            numCompte = numéros;
            SetDétenteur(détenteur);
            switch (status.ToLower())
            {
                case "ok":
                    compteGelé = false;
                break;
                case "gelé":
                    compteGelé = true;
                break;
            }
        }
        //#EndRegion

        //#Region ---- getters / champs calculable ----

        public int GetNumCompte() => numCompte;

        public string GetDétenteur() => détenteur;

        public double GetSolde() => solde;

        public bool GetCompteGelé() => compteGelé;
        //#EndRegion

        //#Region ---- setters ----
        public void SetDétenteur(string nom)
        {
            ArgumentException.ThrowIfNullOrEmpty(détenteur);
            détenteur = nom.Trim();
        }
        //#EndRegion

        //#Region ---- Méthodes calculantes ----

        /// <summary>
        /// permet de savoir si on peut déposer un montant quelconque
        /// </summary>
        /// <returns></returns>
        public bool PeutDéposer() => !compteGelé;

        /// <summary>
        /// permet de savoir si on peut déposer ce montant précis
        /// </summary>
        /// <param name="montant"></param>
        /// <returns></returns>
        public bool PeutDéposer(double montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, double.Round(montant, 2));
            return !compteGelé;
        }

        /// <summary>
        /// Permet de savoir si on peut retirer n'importe quel somme
        /// d'argent
        /// </summary>
        /// <returns></returns>
        public bool PeutRetirer()
        {
            if (solde < 0 && !compteGelé)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// permet de savoir si on peut retirer ce montant précis
        /// </summary>
        /// <param name="montant"></param>
        /// <returns></returns>
        public bool PeutRetirer(double montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, double.Round(montant, 2));
            if (solde < montant && !compteGelé)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Retourne une string qui affiche tous les informations du compte
        /// </summary>
        /// <returns></returns>
        public string Description()
        {
            string description = "";
            int maxLongueurMot = GetLengthDescription();
            int longueurTableau = maxLongueurMot + 20;
            string initial = "[IF]";
            string border = "*";
            string vide = " ";
            string noCompte = Convert.ToString(numCompte);
            string stringSolde = Convert.ToString(solde);
            for (int i = 0; i < 8; i++)
            {
                if (i == 0 || i == 7)
                {
                    description += initial;
                    description += new string('*', longueurTableau);
                    description += "/n";
                }
                if(i == 1 || i == 6)
                {
                    description += border;
                    description += new string(' ', longueurTableau - 2);
                    description += $"{border}/n";
                }
                else
                {
                    for (int j = 0; j < longueurTableau + 4; j++)
                    {
                        switch (i)
                        {
                            case 3:
                                switch (j)
                                {
                                    case 0:
                                        description = initial;
                                    break;
                                    case 1:
                                        description += border;
                                    break;
                                    case 2:
                                        description += new string(' ', 4);
                                    break;
                                    case 3:
                                        description += "COMPTE";
                                        break;
                                    case 4:
                                        description += vide;
                                        break;
                                    case 5:
                                        description += noCompte;
                                        break;
                                    case 6:
                                        int longueur = longueurTableau - noCompte.Length -1 - 12;
                                        description += new string(' ', longueur);
                                        break;
                                    case 7:
                                        description += $"{border}/n";
                                        break;
                                }
                                break;
                            case 4:
                                switch (j)
                                {
                                    case 0:
                                        description = initial;
                                        break;
                                    case 1:
                                        description += border;
                                        break;
                                    case 2:
                                        description += new string(' ', 7);
                                        break;
                                    case 3:
                                        description += "De:";
                                        break;
                                    case 4:
                                        description += vide;
                                        break;
                                    case 5:
                                        description += détenteur;
                                        break;
                                    case 6:
                                        int longueur = longueurTableau - détenteur.Length - 1 - 12;
                                        description += new string(' ', longueur);
                                        break;
                                    case 7:
                                        description += $"{border}/n";
                                        break;
                                }
                                break;
                            case 5:
                                switch (j)
                                {
                                    case 0:
                                        description = initial;
                                        break;
                                    case 1:
                                        description += border;
                                        break;
                                    case 2:
                                        description += new string(' ', 4);
                                        break;
                                    case 3:
                                        description += "Solde:";
                                        break;
                                    case 4:
                                        description += vide;
                                        break;
                                    case 5:
                                        description += stringSolde;
                                        break;
                                    case 6:
                                        int longueur = longueurTableau - stringSolde.Length - 1 - 12;
                                        description += new string(' ', longueur);
                                        break;
                                    case 7:
                                        description += $"{border}/n";
                                        break;
                                }
                            break;
                            case 6:
                                switch (j)
                                {
                                    case 0:
                                        description = initial;
                                        break;
                                    case 1:
                                        description += border;
                                        break;
                                    case 2:
                                        description += new string(' ', 3);
                                        break;
                                    case 3:
                                        description += "Status";
                                        break;
                                    case 4:
                                        description += vide;
                                        break;
                                    case 5:
                                        description += GetCompteGeléString();
                                        break;
                                    case 6:
                                        int longueur = longueurTableau - GetCompteGeléString().Length - 1 - 12;
                                        description += new string(' ', longueur);
                                        break;
                                    case 7:
                                        description += $"{border}/n";
                                        break;
                                }
                            break;
                        }
                    }
                }
            }
            return description;
        }

        /// <summary>
        /// Détermine la longeur que le tableau de description doit avoir
        /// </summary>
        /// <returns></returns>
        private int GetLengthDescription()
        {
            int longeurDescription = 0;
            //besion de string pour déterminer la longeur de chaque variable
            string noCompte = Convert.ToString(numCompte);
            string stringSolde = Convert.ToString(solde);
            int[] longeur = {noCompte.Length, stringSolde.Length, GetCompteGeléString().Length,détenteur.Length};
            foreach(int i in longeur)
            {
                if(i > longeurDescription)
                {
                    longeurDescription = i;
                }
            }
            return longeurDescription + 13 + 4;
        }

        /// <summary>
        /// obtient le status en string de la variable compteGelé
        /// </summary>
        /// <returns></returns>
        private string GetCompteGeléString()
        {
            if (compteGelé)
            {
                return "gelé";
            }
            else
            {
                return "ok";
            }
        }

        //#EndRegion

        //#Region ---- Méthodes modifiantes ----

        /// <summary>
        /// Permet de déposer une somme d'argent sur le compte
        /// </summary>
        /// <param name="montant"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Déposer(double montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, double.Round(montant, 2));
            if (!PeutDéposer(montant))
            {
                throw new InvalidOperationException();
            }
            else
            {
                solde += montant;
            }
        }

        /// <summary>
        /// Permet de retirer une somme d'argent sur le compte
        /// </summary>
        /// <param name="montant"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Retirer(double montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, double.Round(montant, 2));
            if (!PeutRetirer(montant))
            {
                throw new InvalidOperationException();
            }
            else
            {
                solde += montant;
            }
        }

        /// <summary>
        /// Vide le compte entièrement et retourn le montant retirer
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public double Vider()
        {
            if(solde == 0) throw new InvalidOperationException();
            if(compteGelé) throw new InvalidOperationException();
            double numRetirer = solde;
            solde = 0;
            return numRetirer;
        }

        /// <summary>
        /// Gèle le compte
        /// </summary>
        public void Geler()
        {
            compteGelé = true;
        }

        /// <summary>
        /// Dégèle le compte
        /// </summary>
        public void DéGeler()
        {
            compteGelé = false;
        }

        //#EndRegions
    }
}
