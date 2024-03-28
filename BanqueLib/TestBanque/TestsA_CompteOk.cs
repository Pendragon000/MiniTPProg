using BanqueLib;
using System.Globalization;

namespace TestBanque
{
    [TestClass]
    [TestCategory("Isaak F.")]
    public class TestsA_CompteOk
    {
        public static decimal DecimalParse(string str) 
        { 
            return decimal.Parse(str.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)); 
        }

        [TestMethod]
        public void T01a_Compilation()
        {
            Assert.IsTrue(true);
            Assert.AreEqual(default, StatutCompte.Ok);
        }

        [TestMethod]
        public void T01b_Encapsulation()
        {
            // Aucun champ public svp
            Assert.AreEqual(0, typeof(Compte).GetFields().Count(f => f.IsPublic));
        }

        [TestMethod]
        [DataRow(1, "D", "0", StatutCompte.Ok)]
        [DataRow(201, "Han", "12.3", StatutCompte.Ok)]
        [DataRow(30001, "Yoda", "123.45", StatutCompte.Gelé)]
        public void T02_Constructeurs_et_Getters(int numéro, string détenteur, string _solde, StatutCompte statut)
        {
            decimal solde = DecimalParse(_solde);
            var c1 = new Compte(numéro, détenteur);
            var c2 = new Compte(numéro, détenteur, solde);
            var c3 = new Compte(numéro, détenteur, solde, statut);
            Assert.AreEqual(numéro, c1.Numéro);
            Assert.AreEqual(numéro, c2.Numéro);
            Assert.AreEqual(numéro, c3.Numéro);
            Assert.AreEqual(détenteur, c1.Détenteur);
            Assert.AreEqual(détenteur, c2.Détenteur);
            Assert.AreEqual(détenteur, c3.Détenteur);
            Assert.AreEqual(0m, c1.Solde);
            Assert.AreEqual(solde, c2.Solde);
            Assert.AreEqual(solde, c3.Solde);
            Assert.AreEqual(default, c1.Statut);
            Assert.AreEqual(default, c2.Statut);
            Assert.AreEqual(statut, c3.Statut);
            Assert.AreEqual(false, c1.EstGelé);
            Assert.AreEqual(false, c2.EstGelé);
            Assert.AreEqual(statut == StatutCompte.Gelé, c3.EstGelé);
        }

        [TestMethod]
        [DataRow("Han", StatutCompte.Ok)]
        [DataRow("Yoda", StatutCompte.Ok)]
        [DataRow("Vador", StatutCompte.Gelé)]
        [DataRow("Palpatine", StatutCompte.Gelé)]
        public void T03_SetDétenteur(string détenteur, StatutCompte statut)
        {
            var c = new Compte(1, "D", default, statut);
            c.SetDétenteur(détenteur);
            Assert.AreEqual(détenteur, c.Détenteur);
        }

        [TestMethod]
        [DataRow("   D   ")]
        [DataRow("     Han     ")]
        [DataRow("     Yoda     ")]
        public void T04a_Trimage_SetDétenteur(string détenteur)
        {
            var c = new Compte(1, "H");
            c.SetDétenteur(détenteur);
            Assert.AreEqual(détenteur.Trim(), c.Détenteur);
        }

        [TestMethod]
        [DataRow("   D   ")]
        [DataRow("     Han     ")]
        [DataRow("     Yoda     ")]
        public void T04b_Trimage_Constructeur(string détenteur)
        {
            var c = new Compte(1, détenteur);
            Assert.AreEqual(détenteur.Trim(), c.Détenteur);
        }

        [TestMethod]
        [DataRow(1, "D", "0", StatutCompte.Ok)]
        [DataRow(201, "Han", "12.3", StatutCompte.Ok)]
        [DataRow(30001, "Yoda", "123.45", StatutCompte.Gelé)]
        public void T05_Description(int numéro, string détenteur, string _solde, StatutCompte statut)
        {
            decimal solde = DecimalParse(_solde);
            var d = new Compte(numéro, détenteur, solde, statut).Description();
            StringAssert.Contains(d, $"COMPTE  {numéro}");
            StringAssert.Contains(d, $"De:  {détenteur}");
            StringAssert.Contains(d, $"Solde:  {solde:C}");
            StringAssert.Contains(d, $"Statut:  {statut}");
        }

        [TestMethod]
        [DataRow("0.01", 0, StatutCompte.Ok, true)]
        [DataRow("0.01", 234, StatutCompte.Ok, true)]
        [DataRow("100", 0, StatutCompte.Ok, true)]
        [DataRow("0.01", 0, StatutCompte.Gelé, false)]
        [DataRow("100", 0, StatutCompte.Gelé, false)]
        [DataRow("100", 123, StatutCompte.Gelé, false)]
        public void T06_PeutDéposer(string _montant, int solde, StatutCompte statut, bool réponseAttendue)
        {
            var montant = DecimalParse(_montant);
            var c1 = new Compte(1, "D", solde, statut);
            Assert.AreEqual(réponseAttendue, c1.PeutDéposer());
            Assert.AreEqual(réponseAttendue, c1.PeutDéposer(montant));
        }

        [TestMethod]
        [DataRow("0.01", 0, StatutCompte.Ok, false, false)]
        [DataRow("0.01", 234, StatutCompte.Ok, true, true)]
        [DataRow("100", 10, StatutCompte.Ok, true, false)]
        [DataRow("100", 100, StatutCompte.Ok, true, true)]
        [DataRow("100", 1000, StatutCompte.Ok, true, true)]
        [DataRow("0.01", 0, StatutCompte.Gelé, false, false)]
        [DataRow("0.01", 10, StatutCompte.Gelé, false, false)]
        [DataRow("100", 1000, StatutCompte.Gelé, false, false)]
        [DataRow("100", 123, StatutCompte.Gelé, false, false)]
        public void T07_PeutRetirer(string _montant, int solde, StatutCompte statut, bool réponse1, bool réponse2)
        {
            var montant = DecimalParse(_montant);
            var c = new Compte(1, "D", solde, statut);
            Assert.AreEqual(réponse1, c.PeutRetirer());
            Assert.AreEqual(réponse2, c.PeutRetirer(montant));
        }

        [TestMethod]
        [DataRow("0.01", "0", "0.01")]
        [DataRow("123.45", "0", "123.45")]
        [DataRow("0.01", "123.45", "123.46")]
        [DataRow("1111.11", "123.45", "1234.56")]
        public void T08_Déposer(string _montant, string _soldeAvant, string _soldeAprès)
        {
            var montant = DecimalParse(_montant);
            var soldeAvant = DecimalParse(_soldeAvant);
            var soldeAprès = DecimalParse(_soldeAprès);
            var c = new Compte(1, "D", soldeAvant);
            c.Déposer(montant);
            Assert.AreEqual(soldeAprès, c.Solde);
        }

        [TestMethod]
        [DataRow("0.01", "100", "99.99")]
        [DataRow("0.01", "0.01", "0")]
        [DataRow("123.45", "1000", "876.55")]
        public void T09_Retirer(string _montant, string _soldeAvant, string _soldeAprès)
        {
            var montant = DecimalParse(_montant);
            var soldeAvant = DecimalParse(_soldeAvant);
            var soldeAprès = DecimalParse(_soldeAprès);
            var c = new Compte(1, "D", soldeAvant);
            c.Retirer(montant);
            Assert.AreEqual(soldeAprès, c.Solde);
        }

        [TestMethod]
        [DataRow("0.01")]
        [DataRow("100.02")]
        [DataRow("123.45")]
        [DataRow("12356.67")]
        public void T10_Vider(string _solde)
        {
            var solde = DecimalParse(_solde);
            var c = new Compte(1, "D", solde);
            var montant = c.Vider();
            Assert.AreEqual(0, c.Solde);
            Assert.AreEqual(montant, solde);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(10, 10)]
        public void T11_Geler(int numéro, int solde)
        {
            var c = new Compte(numéro, "D", solde);
            c.Geler();
            Assert.AreEqual(true, c.EstGelé);
            Assert.AreEqual(StatutCompte.Gelé, c.Statut);
            Assert.AreEqual(numéro, c.Numéro);
            Assert.AreEqual(solde, c.Solde);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(10, 10)]
        public void T12_Dégeler(int numéro, int solde)
        {
            var c = new Compte(numéro, "D", solde, StatutCompte.Gelé);
            c.Dégeler();
            Assert.AreEqual(false, c.EstGelé);
            Assert.AreEqual(StatutCompte.Ok, c.Statut);
            Assert.AreEqual(numéro, c.Numéro);
            Assert.AreEqual(solde, c.Solde);
        }
    }
}