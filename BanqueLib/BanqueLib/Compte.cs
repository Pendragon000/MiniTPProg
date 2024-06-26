﻿using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace BanqueLib
{
    public enum StatutCompte {Ok,Gelé }
    public class Compte
    {
        //#Region --Champs--
        private readonly int _numéro;
        private string _détenteur;
        private decimal _solde;
        private StatutCompte _statut;
        private bool _estGelé;
        //#EndRegion

        //#Region ---- initiateurs ----
        
        public Compte(int Numéro, string Détenteur, decimal solde = 0.00m, StatutCompte status = StatutCompte.Ok)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Numéro);
            ArgumentOutOfRangeException.ThrowIfNegative(solde);
            ArgumentOutOfRangeException.ThrowIfNotEqual(solde, decimal.Round(solde, 2));
            //ArgumentException.ThrowIfNullOrEmpty(Détenteur);
            //ArgumentException.ThrowIfNullOrWhiteSpace(Détenteur);
            _numéro = Numéro;
            
            //_détenteur = Détenteur.Trim();
            _statut = status;
            if (status == StatutCompte.Gelé) 
            {
                _estGelé = true;

            }
            _solde = decimal.Round(solde,2);

            SetDétenteur(Détenteur);
        }
        //#EndRegion

        //#Region ---- getters / champs calculable ----

        public int Numéro
        {
            get { return _numéro; }
        }

        public string Détenteur
        {
            get { return _détenteur; }
        }

        public decimal Solde
        {
            get { return _solde; }
        }

        public StatutCompte Statut
        {
            get { return _statut; }
        }

        public bool EstGelé
        {
            get { return _estGelé; }
        }
        //#EndRegion

        //#Region ---- Setters ----
        [MemberNotNull(nameof(_détenteur))]
        public void SetDétenteur(string Détenteur)
        {
            ArgumentException.ThrowIfNullOrEmpty(Détenteur);
            ArgumentException.ThrowIfNullOrWhiteSpace(Détenteur);
            _détenteur = Détenteur.Trim();
        }
        //#Region ---- Méthodes calculantes ----

        /// <summary>
        /// permet de savoir si on peut déposer un montant quelconque
        /// </summary>
        /// <returns></returns>
        public bool PeutDéposer()
        {
            if(_statut == StatutCompte.Ok)
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
            if (_statut == StatutCompte.Ok)
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
            if (Solde > 0 && _statut == StatutCompte.Ok)
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
            if (Solde >= montant && _statut == StatutCompte.Ok)
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
           string description = "[IF]**********************************************\n" +
                          "[IF]*                                            *\n" +
                         $"[IF]*    COMPTE  {_numéro,-33}*\n" +
                         $"[IF]*       De:  {_détenteur,-33}*\n" +
                         $"[IF]*    Solde:  {_solde ,-33:C}*\n" +
                         $"[IF]*   Statut:  {_statut,-33}*\n" +
                          "[IF]*                                            *\n" +
                          "[IF]**********************************************\n";
            return description;
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
                _solde += decimal.Round(montant, 2);
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
                _solde -= decimal.Round(montant, 2);
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
            if(_statut == StatutCompte.Gelé) throw new InvalidOperationException("Vider");
            decimal numRetirer = Solde;
            _solde = 0.00m;
            return numRetirer;
        }

        /// <summary>
        /// Gèle le compte
        /// </summary>
        public void Geler()
        {
            if(_statut == StatutCompte.Gelé)throw new InvalidOperationException("Geler");
            _statut = StatutCompte.Gelé;
            _estGelé = true;
        }

        /// <summary>
        /// Dégèle le compte
        /// </summary>
        public void Dégeler()
        {
            if(_statut == StatutCompte.Ok)throw new InvalidOperationException("Dégeler");
            _statut = StatutCompte.Ok;
            _estGelé = false;
        }

        //#EndRegions

        // -----Sérialisation-----
        public void SérialiserCompte()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText("compte.json", json);
        }

        public Compte DésérialiserCompte()
        {
            string Ejson = File.ReadAllText("compte.json");
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var tempObject = JsonSerializer.Deserialize<JsonElement>(Ejson);
            int Numéro = tempObject.GetProperty("Numéro").GetInt32();
            string Détenteur = tempObject.GetProperty("Détenteur").GetString()!;
            decimal Solde = tempObject.GetProperty("Solde").GetDecimal();
            int Statut = tempObject.GetProperty("Statut").GetInt32();
            return new Compte(Numéro, Détenteur, Solde, (StatutCompte)Statut);
        }
    }
}
