using System.Reflection.Metadata.Ecma335;

namespace BanqueLib
{
    public enum StatutCompte {Ok,Gelé }
    public class Compte
    {
        //#Region --Champs--
        private int numéro;
        public string détenteur;
        public decimal solde;
        public StatutCompte statut;
        public bool estGelé;
        //#EndRegion

        //#Region ---- initiateurs ----
        public Compte(int numéros, string détendeur, decimal solde = 0.00m, StatutCompte status = StatutCompte.Ok, 
            bool estGelé = false)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numéros);
            ArgumentOutOfRangeException.ThrowIfNegative(solde);
            ArgumentOutOfRangeException.ThrowIfNotEqual(solde, decimal.Round(solde, 2));
            this.numéro = numéros;
            SetDétenteur(détendeur);
            this.statut = status;
            this.estGelé = estGelé;
            this.solde = decimal.Round(solde,2);
        }
        //#EndRegion

        //#Region ---- getters / champs calculable ----

        public int Numéro
        {
            get { return numéro; }
        }

        public string Détenteur
        {
            get { return détenteur; }
        }

        public decimal Solde
        {
            get { return solde; }
        }

        public StatutCompte Statut
        {
            get { return statut; }
        }

        public bool EstGelé
        {
            get { return estGelé; }
        }
        //#EndRegion

        //#Region ---- Setters ----
        public void SetDétenteur(string nom)
        {
            ArgumentException.ThrowIfNullOrEmpty(nom);
            ArgumentException.ThrowIfNullOrWhiteSpace(nom);
             détenteur = nom.Trim();
        }
        //#Region ---- Méthodes calculantes ----

        /// <summary>
        /// permet de savoir si on peut déposer un montant quelconque
        /// </summary>
        /// <returns></returns>
        public bool PeutDéposer()
        {
            if(statut == StatutCompte.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// permet de savoir si on peut déposer ce montant précis
        /// </summary>
        /// <param name="montant"></param>
        /// <returns></returns>
        public bool PeutDéposer(decimal montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, decimal.Round(montant, 2));
            if (statut == StatutCompte.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Permet de savoir si on peut retirer n'importe quel somme
        /// d'argent
        /// </summary>
        /// <returns></returns>
        public bool PeutRetirer()
        {
            if (Solde > 0 && statut == StatutCompte.Ok)
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
        public bool PeutRetirer(decimal montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, decimal.Round(montant, 2));
            if (Solde > montant && statut == StatutCompte.Ok)
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
            string noCompte = Convert.ToString(Numéro);
            string stringSolde = Convert.ToString(Solde);
            string stringStatus = Convert.ToString(statut);
            for (int i = 0; i < 9; i++)
            {
                if (i == 0 || i == 8)
                {
                    description += initial;
                    description += new string('*', longueurTableau);
                    description += "\n";
                }
                if(i == 1 || i == 7)
                {
                    description += initial;
                    description += border;
                    description += new string(' ', longueurTableau - 2);
                    description += $"{border}\n";
                }
                else
                {
                    for (int j = 0; j < 8; j++)
                    {
                        switch (i)
                        {
                            case 3:
                                switch (j)
                                {
                                    case 0:
                                        description += initial;
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
                                        description += $"{border}\n";
                                        break;
                                }
                                break;
                            case 4:
                                switch (j)
                                {
                                    case 0:
                                        description += initial;
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
                                        description += Détenteur;
                                        break;
                                    case 6:
                                        int longueur = longueurTableau - Détenteur.Length - 1 - 12;
                                        description += new string(' ', longueur);
                                        break;
                                    case 7:
                                        description += $"{border}\n";
                                        break;
                                }
                                break;
                            case 5:
                                switch (j)
                                {
                                    case 0:
                                        description += initial;
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
                                        description += $"{Solde.ToString("0.00")} $";
                                        break;
                                    case 6:
                                        int longueur = longueurTableau - stringSolde.Length - 6 - 12;
                                        description += new string(' ', longueur);
                                        break;
                                    case 7:
                                        description += $"{border}\n";
                                        break;
                                }
                            break;
                            case 6:
                                switch (j)
                                {
                                    case 0:
                                        description += initial;
                                        break;
                                    case 1:
                                        description += border;
                                        break;
                                    case 2:
                                        description += new string(' ', 3);
                                        break;
                                    case 3:
                                        description += "Statut:";
                                        break;
                                    case 4:
                                        description += vide;
                                        break;
                                    case 5:
                                        description +=statut;
                                        break;
                                    case 6:
                                        int longueur = longueurTableau - stringStatus.Length - 1 - 12;
                                        description += new string(' ', longueur);
                                        break;
                                    case 7:
                                        description += $"{border}\n";
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
            string noCompte = Convert.ToString(Numéro);
            string stringSolde = Convert.ToString(Solde);
            string stringStatus = Convert.ToString(statut);
            int[] longeur = {noCompte.Length, stringSolde.Length, stringStatus.Length,Détenteur.Length};
            foreach(int i in longeur)
            {
                if(i > longeurDescription)
                {
                    longeurDescription = i;
                }
            }
            return longeurDescription + 13 + 4;
        }
        //#EndRegion

        //#Region ---- Méthodes modifiantes ----

        /// <summary>
        /// Permet de déposer une somme d'argent sur le compte
        /// </summary>
        /// <param name="montant"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Déposer(decimal montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, decimal.Round(montant, 2));
            if (!PeutDéposer(montant))
            {
                throw new InvalidOperationException();
            }
            else
            {
                solde += decimal.Round(montant, 2);
            }
        }

        /// <summary>
        /// Permet de retirer une somme d'argent sur le compte
        /// </summary>
        /// <param name="montant"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Retirer(decimal montant)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(montant);
            ArgumentOutOfRangeException.ThrowIfNotEqual(montant, decimal.Round(montant, 2));
            if (!PeutRetirer(montant))
            {
                throw new InvalidOperationException();
            }
            else
            {
                solde -= decimal.Round(montant, 2);
            }
        }

        /// <summary>
        /// Vide le compte entièrement et retourn le montant retirer
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public decimal Vider()
        {
            if(Solde == 0) throw new InvalidOperationException("Vider");
            if(statut == StatutCompte.Gelé) throw new InvalidOperationException("Vider");
            decimal numRetirer = Solde;
            solde = 0.00m;
            return numRetirer;
        }

        /// <summary>
        /// Gèle le compte
        /// </summary>
        public void Geler()
        {
            if(statut == StatutCompte.Gelé)throw new InvalidOperationException("Geler");   
            statut = StatutCompte.Gelé;
            estGelé = true;
        }

        /// <summary>
        /// Dégèle le compte
        /// </summary>
        public void Dégeler()
        {
            if(statut == StatutCompte.Ok)throw new InvalidOperationException("Dégeler");
            statut = StatutCompte.Ok;
            estGelé = false;
        }

        //#EndRegions
    }
}
