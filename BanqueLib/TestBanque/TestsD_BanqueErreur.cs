using BanqueLib;

namespace TestBanque
{
    [TestClass]
    [TestCategory("Isaak F.")]
    public class TestsD_BanqueErreur
    {
        public enum Constructeur { Simple, Prochain, Comptes, ProchainArray, ProchainListe }

        private static Banque NewBanque(Constructeur constructeur, string nom, int? prochain = null, params Compte[] comptes) => constructeur switch
        {
            Constructeur.Simple => new Banque(nom),
            Constructeur.Comptes => new Banque(nom, comptes),
            Constructeur.Prochain => new Banque(nom, prochain ?? 1),
            Constructeur.ProchainArray => new Banque(nom, prochain ?? (comptes?.LastOrDefault()?.Numéro + 1) ?? 1, comptes!),
            Constructeur.ProchainListe => new Banque(nom, prochain ?? (comptes?.LastOrDefault()?.Numéro + 1) ?? 1, comptes?.ToList()!),
            _ => throw new ArgumentException(nom, nameof(constructeur)),
        };

        [TestMethod]
        public void T01_Compilation()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        [DataRow(Constructeur.Simple)]
        [DataRow(Constructeur.Comptes)]
        [DataRow(Constructeur.Prochain)]
        [DataRow(Constructeur.ProchainArray)]
        [DataRow(Constructeur.ProchainListe)]
        public void T02a_Construction_Erreur_NomNull(Constructeur constructeur)
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => NewBanque(constructeur, null!));
            Assert.AreEqual("Nom", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(Constructeur.Simple, "")]
        [DataRow(Constructeur.Comptes, "")]
        [DataRow(Constructeur.Prochain, "")]
        [DataRow(Constructeur.ProchainArray, "")]
        [DataRow(Constructeur.ProchainListe, "")]
        [DataRow(Constructeur.Simple, "   ")]
        [DataRow(Constructeur.Comptes, " ")]
        [DataRow(Constructeur.Prochain, "  ")]
        [DataRow(Constructeur.ProchainArray, "    ")]
        [DataRow(Constructeur.ProchainListe, "       ")]
        public void T02b_Construction_Erreur_NomBlanc(Constructeur constructeur, string nom)
        {
            var ex = Assert.ThrowsException<ArgumentException>(
                () => NewBanque(constructeur, nom));
            Assert.AreEqual("Nom", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(Constructeur.Prochain, 0)]
        [DataRow(Constructeur.ProchainArray, 0)]
        [DataRow(Constructeur.ProchainListe, 0)]
        [DataRow(Constructeur.Prochain, -1)]
        [DataRow(Constructeur.ProchainArray, -2)]
        [DataRow(Constructeur.ProchainListe, -3)]
        public void T03a_Construction_Erreur_ProchainNuméroOutOfRange(Constructeur constructeur, int prochain)
        {
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => NewBanque(constructeur, "B", prochain));
            Assert.AreEqual("ProchainNuméro", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(Constructeur.ProchainArray, 1, 1)]
        [DataRow(Constructeur.ProchainListe, 2, 2)]
        [DataRow(Constructeur.ProchainArray, 10, 5, 15)]
        [DataRow(Constructeur.ProchainListe, 11, 6, 16)]
        [DataRow(Constructeur.ProchainArray, 12, 12, 7, 2)]
        [DataRow(Constructeur.ProchainListe, 13, 6, 13, 4)]
        public void T03b_Construction_Erreur_ProchainNuméroTropPetit(Constructeur constructeur, int prochain, params int[] numéros)
        {
            var comptes = numéros.Select(n => new Compte(n, "D" + n, 2 * n)).ToArray();
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => NewBanque(constructeur, "B", prochain, comptes));
            Assert.AreEqual("ProchainNuméro", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(Constructeur.Comptes, null)]
        [DataRow(Constructeur.Comptes, 1, 2, null, 4)]
        [DataRow(Constructeur.Comptes, 10, null)]
        [DataRow(Constructeur.ProchainArray, null)]
        [DataRow(Constructeur.ProchainArray, 4, 5, 16, null, 7)]
        [DataRow(Constructeur.ProchainListe, null)]
        [DataRow(Constructeur.ProchainListe, 19, null, 11, 13)]
        public void T04a_Construction_Erreur_ComptesContientNull(Constructeur constructeur, params int?[] numéros)
        {
            var comptes = numéros.Select(n => n is null ? null : new Compte(n.Value, "D" + n, 2 * n.Value)).ToArray();
            var ex = Assert.ThrowsException<ArgumentException>(
                () => NewBanque(constructeur, "B", 50, comptes!));
            Assert.AreEqual("Comptes", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(Constructeur.Comptes, 3, 3)]
        [DataRow(Constructeur.Comptes, 1, 2, 1, 4)]
        [DataRow(Constructeur.Comptes, 10, 5, 9, 5)]
        [DataRow(Constructeur.ProchainArray, 9, 9)]
        [DataRow(Constructeur.ProchainArray, 4, 5, 16, 16, 16)]
        [DataRow(Constructeur.ProchainListe, 5, 5)]
        [DataRow(Constructeur.ProchainListe, 19, 3, 11, 13, 3)]
        public void T04c_Construction_Erreur_ComptesNumérosEnDouble(Constructeur constructeur, params int[] numéros)
        {
            var comptes = numéros.Select(n => new Compte(n, "D" + n, 2 * n)).ToArray();
            var ex = Assert.ThrowsException<ArgumentException>(
                () => NewBanque(constructeur, "B", 50, comptes));
            Assert.AreEqual("Comptes", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(Constructeur.Comptes, 3, 4)]
        [DataRow(Constructeur.Comptes, 1, 6)]
        [DataRow(Constructeur.Comptes, 5, 9)]
        [DataRow(Constructeur.ProchainArray, 2, 3)]
        [DataRow(Constructeur.ProchainArray, 3, 8)]
        [DataRow(Constructeur.ProchainListe, 4, 5)]
        [DataRow(Constructeur.ProchainListe, 0, 9)]
        public void T04d_Construction_Erreur_ComptesEnDouble(Constructeur constructeur, int i, int j)
        {
            var comptes = Enumerable.Range(1, 10).Select(n => new Compte(n, "D" + n, 2 * n)).ToArray();
            comptes[i] = comptes[j]; // Créer doublon
            var ex = Assert.ThrowsException<ArgumentException>(
                () => NewBanque(constructeur, "B", null, comptes));
            Assert.AreEqual("Comptes", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-11)]
        public void T07_ChercherCompte_Erreur_NuméroOutOfRange(int numéro)
        {
            var ban = new Banque("T07");
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => ban.ChercherCompte(numéro));
            Assert.AreEqual("numéro", ex.ParamName);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-11)]
        public void T08a_PeutSupprimerCompte_Erreur_NuméroOutOfRange(int numéro)
        {
            var ban = new Banque("T08a");
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => ban.PeutSupprimerCompte(numéro));
            Assert.AreEqual("numéro", ex.ParamName);
        }

        [TestMethod]
        [DataRow("B1")]
        [DataRow("B2")]
        public void T08b_PeutSupprimerCompte_Erreur_RéférenceNull(string nom)
        {
            var ban = new Banque(nom);
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => ban.PeutSupprimerCompte(null!));
            Assert.AreEqual("compte", ex.ParamName);
        }

        [TestMethod]
        [DataRow("B1")]
        [DataRow("B2")]
        public void T09a_CréerCompte_Erreur_DétenteurNull(string nom)
        {
            var ban = new Banque(nom);
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => ban.CréerCompte(null!));
            Assert.AreEqual("Détenteur", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("     ")]
        public void T09b_CréerCompte_Erreur_DétenteurBlanc(string détenteur)
        {
            var ban = new Banque("T09b");
            var ex = Assert.ThrowsException<ArgumentException>(
                () => ban.CréerCompte(détenteur));
            Assert.AreEqual("Détenteur", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-11)]
        public void T10a_SupprimerCompte_Erreur_NuméroOutOfRange(int numéro)
        {
            var ban = new Banque("T10a", [new Compte(1, "D1")]);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => ban.SupprimerCompte(numéro));
            Assert.AreEqual("numéro", ex.ParamName);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        public void T10b_SupprimerCompte_Erreur_NuméroInvalide(int numéro)
        {
            var ban = new Banque("T10b", 
                        [new (2, "D2", 2), 
                        new(4, "D4", 0, StatutCompte.Gelé), 
                        new(6, "D6", 6, StatutCompte.Gelé)]);
            var ex = Assert.ThrowsException<ArgumentException>(
                () => ban.SupprimerCompte(numéro));
            Assert.AreEqual("numéro", ex.ParamName);
        }

        [TestMethod]
        [DataRow("B1")]
        [DataRow("B2")]
        public void T10c_SupprimerCompte_Erreur_CompteNull(string nom)
        {
            var ban = new Banque(nom, [ new(2, "D2", 2), 
                                        new(4, "D4", 0, StatutCompte.Gelé), 
                                        new(6, "D6")]);
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => ban.SupprimerCompte(null!));
            Assert.AreEqual("compte", ex.ParamName);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(null)]
        public void T10d_SupprimerCompte_Erreur_CompteInvalide(int? indice)
        {
            var ban = new Banque("T10d", [ 
                        new(2, "D2", 2), 
                        new(4, "D4", 0, StatutCompte.Gelé), 
                        new(6, "D6", 6, StatutCompte.Gelé)]);
            var compte = indice.HasValue ? ban.Comptes[indice.Value] : new Compte(2, "D2");
            var ex = Assert.ThrowsException<ArgumentException>(
                () => ban.SupprimerCompte(compte));
            Assert.AreEqual("compte", ex.ParamName);
        }
    }
}