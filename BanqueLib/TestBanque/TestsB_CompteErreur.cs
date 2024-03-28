using BanqueLib;
using System.Globalization;

namespace TestBanque
{
    [TestClass]
    [TestCategory("Isaak F.")]
    public class TestsB_CompteErreur
    {
        public static decimal DecimalParse(string str)
        {
            return decimal.Parse(str.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
        }

        [TestMethod]
        public void T01_Compilation()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-10)]
        public void T02a_Constructeur_Erreur_NuméroInvalide(int numéro)
        {
            var ex1 = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Compte(numéro, "T02a"));
            var ex2 = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Compte(numéro, "T02a", default));
            var ex3 = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Compte(numéro, "T02a", default, default));
            Assert.AreEqual("Numéro", ex1.ParamName, true);
            Assert.AreEqual("Numéro", ex2.ParamName, true);
            Assert.AreEqual("Numéro", ex3.ParamName, true);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void T02b_Constructeur_Erreur_DétenteurNull(int numéro)
        {
            var ex1 = Assert.ThrowsException<ArgumentNullException>(() => new Compte(numéro, null!));
            var ex2 = Assert.ThrowsException<ArgumentNullException>(() => new Compte(numéro, null!, default));
            var ex3 = Assert.ThrowsException<ArgumentNullException>(() => new Compte(numéro, null!, default, default));
            Assert.AreEqual("Détenteur", ex1.ParamName, true);
            Assert.AreEqual("Détenteur", ex2.ParamName, true);
            Assert.AreEqual("Détenteur", ex3.ParamName, true);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("     ")]
        public void T02c_Constructeur_Erreur_DétenteurBlanc(string détenteur)
        {
            var ex1 = Assert.ThrowsException<ArgumentException>(() => new Compte(1, détenteur));
            var ex2 = Assert.ThrowsException<ArgumentException>(() => new Compte(1, détenteur, default));
            var ex3 = Assert.ThrowsException<ArgumentException>(() => new Compte(1, détenteur, default, default));
            Assert.AreEqual("Détenteur", ex1.ParamName, true);
            Assert.AreEqual("Détenteur", ex2.ParamName, true);
            Assert.AreEqual("Détenteur", ex3.ParamName, true);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(-10)]
        public void T02d_Constructeur_Erreur_SoldeNégatif(int solde)
        {
            var ex2 = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Compte(1, "T02d", solde));
            var ex3 = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Compte(1, "T02d", solde, default));
            Assert.AreEqual("Solde", ex2.ParamName, true);
            Assert.AreEqual("Solde", ex3.ParamName, true);
        }

        [TestMethod]
        [DataRow("0.001")]
        [DataRow("12.345")]
        [DataRow("1.001")]
        [DataRow("0.00001")]
        [DataRow("100.00001")]
        public void T02e_Constructeur_Erreur_SoldeFractionnaire(string _solde)
        {
            decimal solde = DecimalParse(_solde);
            var ex2 = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Compte(1, "T02e", solde));
            var ex3 = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Compte(1, "T02e", solde, default));
            Assert.AreEqual("Solde", ex2.ParamName, true);
            Assert.AreEqual("Solde", ex3.ParamName, true);
        }

        [TestMethod]
        [DataRow(StatutCompte.Ok)]
        [DataRow(StatutCompte.Gelé)]
        public void T03a_SetDétenteur_Erreur_Null(StatutCompte statut)
        {
            var c = new Compte(1, "T03a", default, statut);
            var ex = Assert.ThrowsException<ArgumentNullException>(() => c.SetDétenteur(null!));
            Assert.AreEqual("Détenteur", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("     ")]
        public void T03b_SetDétenteur_Erreur_Blanc(string détenteur)
        {
            var c = new Compte(1, "T03b");
            var ex = Assert.ThrowsException<ArgumentException>(() => c.SetDétenteur(détenteur!));
            Assert.AreEqual("Détenteur", ex.ParamName, true);
        }

        [TestMethod]
        [DataRow(0, StatutCompte.Ok)]
        [DataRow(-1, StatutCompte.Ok)]
        [DataRow(-100, StatutCompte.Ok)]
        [DataRow(0, StatutCompte.Gelé)]
        public void T06a_PeutDéposer_Erreur_MontantNonPositif(int montant, StatutCompte statut)
        {
            var c = new Compte(1, "T06a", 0, statut);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.PeutDéposer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow("0.001", StatutCompte.Ok)]
        [DataRow("123.456", StatutCompte.Ok)]
        [DataRow("0.0000001", StatutCompte.Ok)]
        [DataRow("100000.00001", StatutCompte.Gelé)]
        public void T06b_PeutDéposer_Erreur_MontantFractionnaire(string _montant, StatutCompte statut)
        {
            var montant = DecimalParse(_montant);
            var c = new Compte(1, "T06b", 0, statut);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.PeutDéposer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow(0, StatutCompte.Ok)]
        [DataRow(-1, StatutCompte.Ok)]
        [DataRow(-100, StatutCompte.Ok)]
        [DataRow(0, StatutCompte.Gelé)]
        public void T07a_PeutRetirer_Erreur_MontantNonPositif(int montant, StatutCompte statut)
        {
            var c = new Compte(1, "T07a", 1000000, statut);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.PeutRetirer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow("0.001", StatutCompte.Ok)]
        [DataRow("123.456", StatutCompte.Ok)]
        [DataRow("0.0000001", StatutCompte.Ok)]
        [DataRow("100000.00001", StatutCompte.Gelé)]
        public void T07b_PeutRetirer_Erreur_MontantFractionnaire(string _montant, StatutCompte statut)
        {
            var montant = DecimalParse(_montant);
            var c = new Compte(1, "T07b", 1000000, statut);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.PeutRetirer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-100)]
        public void T08a_Déposer_Erreur_MontantNonPositif(int montant)
        {
            var c = new Compte(1, "T08a", 0);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.Déposer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow("0.001")]
        [DataRow("123.456")]
        [DataRow("0.0000001")]
        [DataRow("100000.00001")]
        public void T08b_Déposer_Erreur_MontantFractionnaire(string _montant)
        {
            var montant = DecimalParse(_montant);
            var c = new Compte(1, "T08b", 0);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.Déposer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10)]
        public void T08c_Déposer_Erreur_CompteGelé(int montant)
        {
            var c = new Compte(1, "T08c", 0, StatutCompte.Gelé);
            _ = Assert.ThrowsException<InvalidOperationException>(() => c.Déposer(montant));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-100)]
        public void T09a_Retirer_Erreur_MontantNonPositif(int montant)
        {
            var c = new Compte(1, "T09a", 1000);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.Retirer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow("0.001")]
        [DataRow("123.456")]
        [DataRow("0.0000001")]
        [DataRow("100000.00001")]
        public void T09b_Retirer_Erreur_MontantFractionnaire(string _montant)
        {
            var montant = DecimalParse(_montant);
            var c = new Compte(1, "T09b", 1000000);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => c.Retirer(montant));
            Assert.AreEqual("montant", ex.ParamName);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10)]
        public void T09c_Retirer_Erreur_CompteGelé(int montant)
        {
            var c = new Compte(1, "T09c", 100, StatutCompte.Gelé);
            _ = Assert.ThrowsException<InvalidOperationException>(() => c.Retirer(montant));
        }

        [TestMethod]
        [DataRow("0.01", "0")]
        [DataRow("100.02", "100.01")]
        [DataRow("123.45", "87.34")]
        [DataRow("123.45", "123.12")]
        public void T09d_Retirer_Erreur_SoldeInsuffisant(string _montant, string _solde)
        {
            var montant = DecimalParse(_montant);
            var solde = DecimalParse(_solde);
            var c = new Compte(1, "T09d", solde);
            _ = Assert.ThrowsException<InvalidOperationException>(() => c.Retirer(montant));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void T10a_Vider_Erreur_CompteVide(int numéro)
        {
            var c = new Compte(numéro, "T10a");
            _ = Assert.ThrowsException<InvalidOperationException>(() => c.Vider());
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(20)]
        public void T10b_Vider_Erreur_CompteGelé(int solde)
        {
            var c = new Compte(1, "T10b", solde, StatutCompte.Gelé);
            _ = Assert.ThrowsException<InvalidOperationException>(() => c.Vider());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(10)]
        public void T11_Geler_Erreur_DéjàGelé(int solde)
        {
            var c = new Compte(1, "T11", solde, StatutCompte.Gelé);
            _ = Assert.ThrowsException<InvalidOperationException>(() => c.Geler());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(10)]
        public void T12_Dégeler_Erreur_DéjàDégelé(int solde)
        {
            var c = new Compte(1, "T12", solde, StatutCompte.Ok);
            _ = Assert.ThrowsException<InvalidOperationException>(() => c.Dégeler());
        }
    }
}