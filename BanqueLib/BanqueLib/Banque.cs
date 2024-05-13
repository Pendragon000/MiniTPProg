using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanqueLib
{
    public class Banque
    {
        public string Nom { get; init; }
        public List<Compte> Comptes { get; private set; }
        public int ProchainNuméro { get; private set; }
        public int NombreDeComptes
        {
            get
            {
                if (Comptes is null)
                {
                    return 0;
                }
                else
                {
                    return Comptes.Count;
                }
            }
        }
        public decimal TotalDesDépôts
        {
            get
            {
                decimal total = 0;
                if (Comptes is not null)
                    foreach (var compte in Comptes)
                    {
                        total += compte.Solde;
                    }
                return total;
            }
        }
        public Banque(string nom, int prochainNumero = 1)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(prochainNumero);
            ArgumentException.ThrowIfNullOrWhiteSpace(nom);
            this.Nom = nom;
            ProchainNuméro = prochainNumero;
            Comptes = new List<Compte>();
        }
        public Banque(string Nom, IEnumerable<Compte> comptes)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(Nom);

            if (comptes is not null)
            {
                if (comptes.Any(c => c == null))
                {
                    throw new ArgumentException("Un compte est null", "Comptes");
                }
                if (comptes.GroupBy(c => c.Numéro).Any(g => g.Count() > 1))
                {
                    throw new ArgumentException("Un Compte est dupliqué", "Comptes");
                }
                ProchainNuméro = comptes.Max(Compte => Compte.Numéro + 1);
                foreach (Compte c in comptes)
                {
                    ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(ProchainNuméro, c.Numéro);
                }
                Comptes = comptes.ToList();
            }
            else
            {
                Comptes = new List<Compte>();
            }

            this.Nom = Nom;

        }
        public Banque(string Nom, int prochainNumero, IEnumerable<Compte> comptes) : this(Nom, prochainNumero)
        {
            if (comptes is not null)
            {
                if (comptes.GroupBy(c => c.Numéro).Any(g => g.Count() > 1))
                {
                    throw new ArgumentException("Un Compte est dupliqué", "Comptes");
                }
                foreach (Compte c in comptes)
                {
                    ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(ProchainNuméro, c.Numéro);
                }
                Comptes = comptes.ToList();
            }
            else
            {
                Comptes = new List<Compte>();
            }
        }

        public string DescriptionSommaire()
        {
            string description =
                         "[IF]============================================================================\n" +
                         "[IF]|                                                                          |\n" +
                        $"[IF]|          Banque  {Nom,-56}|\n" +
                         "[IF]|                                                                          |\n" +
                        $"[IF]|   ProchainNuméro:  {ProchainNuméro,-54}|\n" +
                        $"[IF]|   NombreDeCompte:  {NombreDeComptes,-54}|\n" +
                        $"[IF]| Total des dépots:  {TotalDesDépôts,-54:C}|\n" +
                         "[IF]|                                                                          |\n" +
                         "[IF]============================================================================\n";
            return description;
        }

        public string DescriptionDesComptes()
        {
            string description = "";
            if (Comptes != null)
            {
                //trie les comptes en ordre croissant selon le numéros de comptes
                for (int i = 0; i < Comptes.Count - 1; i++)
                {
                    for (int j = i + 1; j < Comptes.Count; j++)
                    {
                        if (Comptes[i].Numéro > Comptes[j].Numéro)
                        {
                            Compte temp = Comptes[i];
                            Comptes[i] = Comptes[j];
                            Comptes[j] = temp;
                        }
                    }
                }
                foreach (Compte c in Comptes)
                {
                    description += $"[IF]  #{c.Numéro,-8}{c.Détenteur,-30}{c.Solde,-10:C}{c.Statut}\n\n";
                }
            }
            return description;
        }
        public string DescriptionComplète() => DescriptionSommaire() + "\n\n" + DescriptionDesComptes();
        public Compte CréerCompte(string Détenteur)
        {
            if (Comptes == null)
            {
                Comptes = new List<Compte>() { };
            }
            Compte compteTemp = new Compte(ProchainNuméro, Détenteur);
            Comptes.Add(compteTemp);
            ProchainNuméro++;
            return compteTemp;
        }
        public Compte? ChercherCompte(int Num)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Num);
            if (Comptes == null)
            {
                return null;
            }
            foreach (Compte c in Comptes)
            {
                if (c.Numéro == Num)
                {
                    return c;
                }
            }
            return null;
        }
        public bool PeutSupprimerCompte(Compte compte)
        {
            ArgumentNullException.ThrowIfNull(compte);
            if(Comptes is null)
            {
                return false;
            }
            Compte? compteTrouver = Comptes.Find(comptee => comptee.Numéro == compte.Numéro);
            if (compteTrouver == null)
            {
                return false;
            }
            else if (compteTrouver.Statut == StatutCompte.Gelé)
            {
                return false;
            }
            else if (compteTrouver.Solde != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool PeutSupprimerCompte(int Num)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Num);
            if (Comptes == null)
            {
                return false;
            }
            Compte? compteTrouver = Comptes.Find(compte => compte.Numéro == Num);
            if (compteTrouver == null)
            {
                throw new ArgumentNullException();
            }
            else if (compteTrouver.Statut == StatutCompte.Gelé)
            {
                return false;
            }
            else if(compteTrouver.Solde != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void SupprimerCompte(Compte compte)
        {
            ArgumentNullException.ThrowIfNull(compte);
            if (!PeutSupprimerCompte(compte) || Comptes is null)
            {
                throw new ArgumentException();
            }
            Comptes.Remove(compte);
        }
        public void SupprimerCompte(int Num)
        {
            if (!PeutSupprimerCompte(Num) || Comptes is null)
            {
                throw new ArgumentException();
            }
            Comptes.Remove(Comptes.Find(compte => compte.Numéro == Num)!);
        }
    }
}
