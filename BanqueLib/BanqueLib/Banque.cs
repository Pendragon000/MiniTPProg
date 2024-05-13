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
        private List<Compte> _comptes;
        public IReadOnlyList<Compte> Comptes { get => _comptes.OrderBy(c => c.Numéro).ToList();}
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
        public Banque(string nom, int prochainNuméro = 1)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(prochainNuméro);
            ArgumentException.ThrowIfNullOrWhiteSpace(nom);
            this.Nom = nom;
            ProchainNuméro = prochainNuméro;
            _comptes = new List<Compte>();
        }
        public Banque(string Nom, IEnumerable<Compte> comptes)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(Nom);

            if (comptes is not null)
            {
                _comptes = comptes.ToList();
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
            }
            else
            {
                ProchainNuméro = 1;
                _comptes = new List<Compte>();
            }

            this.Nom = Nom;

        }
        public Banque(string Nom, int prochainNuméro, IEnumerable<Compte> Comptes) : this(Nom, prochainNuméro)
        {
            if (Comptes is not null)
            {
                if (Comptes.Any(c=> c == null))
                {
                    throw new ArgumentException(); 
                }
                foreach (Compte c in Comptes)
                {
                    ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(this.ProchainNuméro, c.Numéro);
                }
                if (Comptes.GroupBy(c => c.Numéro).Any(g => g.Count() > 1))
                {
                    throw new ArgumentException("Un Compte est dupliqué", "Comptes");
                }
                _comptes = Comptes.ToList();
            }
            else
            {
                _comptes = new List<Compte>();
            }
        }

        public string DescriptionSommaire()
        {
            string description =
                         "[IF]=============================================================================\n" +
                         "[IF]|                                                                           |\n" +
                        $"[IF]|           Banque  {Nom,-56}|\n" +
                         "[IF]|                                                                           |\n" +
                        $"[IF]|   Prochain numéro:  {ProchainNuméro,-54}|\n" +
                        $"[IF]| Nombre de comptes:  {NombreDeComptes,-54}|\n" +
                        $"[IF]|  Total des dépôts:  {TotalDesDépôts,-54:C}|\n" +
                         "[IF]|                                                                           |\n" +
                         "[IF]=============================================================================\n";
            return description;
        }

        public string DescriptionDesComptes()
        {
            string description = "";
            if (Comptes != null)
            {
                foreach (Compte c in Comptes)
                {
                    description += $"[IF]  # "+$"{c.Numéro,-8}{c.Détenteur,-30}{c.Solde,-10:C}{c.Statut}\n\n";
                }
            }
            return description;
        }
        public string DescriptionComplète() => DescriptionSommaire() + "\n\n" + DescriptionDesComptes();
        public Compte CréerCompte(string Détenteur)
        {
            if (Comptes == null)
            {
                _comptes = new List<Compte>() { };
            }
            Compte compteTemp = new Compte(ProchainNuméro, Détenteur);
            _comptes.Add(compteTemp);
            ProchainNuméro++;
            return compteTemp;
        }
        public Compte? ChercherCompte(int numéro)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numéro);
            if (Comptes == null)
            {
                return null;
            }
            foreach (Compte c in Comptes)
            {
                if (c.Numéro == numéro)
                {
                    return c;
                }
            }
            return null;
        }
        public bool PeutSupprimerCompte(Compte compte)
        {
            ArgumentNullException.ThrowIfNull(compte);
            Compte? compteTrouver = null;
            foreach (Compte c in Comptes)
            {
                if (c == compte)
                    compteTrouver = c;
            }
            if (compteTrouver == null)
                return false;
            else if (compteTrouver.Statut == StatutCompte.Gelé)
                return false;
            else if (compteTrouver.Solde != 0)
                return false;
            else
                return true;
        }
        public bool PeutSupprimerCompte(int numéro)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numéro);
            if (Comptes == null)
            {
                return false;
            }
            Compte? compteTrouver = _comptes.Find(compte => compte.Numéro == numéro);
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
        public void SupprimerCompte(Compte compte)
        {
            ArgumentNullException.ThrowIfNull(compte);
            if (!PeutSupprimerCompte(compte) || Comptes is null)
            {
                throw new ArgumentException();
            }
            _comptes.Remove(compte);
        }
        public void SupprimerCompte(int numéro)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numéro);
            if (Comptes == null)
            {
                throw new ArgumentException();
            }
            if (!PeutSupprimerCompte(numéro))
            {
                throw new ArgumentException();
            }
            _comptes.Remove(_comptes.Find(compte => compte.Numéro == numéro)!);
        }
    }
}
