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
        [DataRow(30001, "Yoda", "123.45", StatutCompte.Gel�)]
        public void T02_Constructeurs_et_Getters(int num�ro, string d�tenteur, string _solde, StatutCompte statut)
        {
            decimal solde = DecimalParse(_solde);
            var c1 = new Compte(num�ro, d�tenteur);
            var c2 = new Compte(num�ro, d�tenteur, solde);
            var c3 = new Compte(num�ro, d�tenteur, solde, statut);
            Assert.AreEqual(num�ro, c1.Num�ro);
            Assert.AreEqual(num�ro, c2.Num�ro);
            Assert.AreEqual(num�ro, c3.Num�ro);
            Assert.AreEqual(d�tenteur, c1.D�tenteur);
            Assert.AreEqual(d�tenteur, c2.D�tenteur);
            Assert.AreEqual(d�tenteur, c3.D�tenteur);
            Assert.AreEqual(0m, c1.Solde);
            Assert.AreEqual(solde, c2.Solde);
            Assert.AreEqual(solde, c3.Solde);
            Assert.AreEqual(default, c1.Statut);
            Assert.AreEqual(default, c2.Statut);
            Assert.AreEqual(statut, c3.Statut);
            Assert.AreEqual(false, c1.EstGel�);
            Assert.AreEqual(false, c2.EstGel�);
            Assert.AreEqual(statut == StatutCompte.Gel�, c3.EstGel�);
        }

        [TestMethod]
        [DataRow("Han", StatutCompte.Ok)]
        [DataRow("Yoda", StatutCompte.Ok)]
        [DataRow("Vador", StatutCompte.Gel�)]
        [DataRow("Palpatine", StatutCompte.Gel�)]
        public void T03_SetD�tenteur(string d�tenteur, StatutCompte statut)
        {
            var c = new Compte(1, "D", default, statut);
            c.SetD�tenteur(d�tenteur);
            Assert.AreEqual(d�tenteur, c.D�tenteur);
        }

        [TestMethod]
        [DataRow("   D   ")]
        [DataRow("     Han     ")]
        [DataRow("     Yoda     ")]
        public void T04a_Trimage_SetD�tenteur(string d�tenteur)
        {
            var c = new Compte(1, "H");
            c.SetD�tenteur(d�tenteur);
            Assert.AreEqual(d�tenteur.Trim(), c.D�tenteur);
        }

        [TestMethod]
        [DataRow("   D   ")]
        [DataRow("     Han     ")]
        [DataRow("     Yoda     ")]
        public void T04b_Trimage_Constructeur(string d�tenteur)
        {
            var c = new Compte(1, d�tenteur);
            Assert.AreEqual(d�tenteur.Trim(), c.D�tenteur);
        }

        [TestMethod]
        [DataRow(1, "D", "0", StatutCompte.Ok)]
        [DataRow(201, "Han", "12.3", StatutCompte.Ok)]
        [DataRow(30001, "Yoda", "123.45", StatutCompte.Gel�)]
        public void T05_Description(int num�ro, string d�tenteur, string _solde, StatutCompte statut)
        {
            decimal solde = DecimalParse(_solde);
            var d = new Compte(num�ro, d�tenteur, solde, statut).Description();
            StringAssert.Contains(d, $"COMPTE  {num�ro}");
            StringAssert.Contains(d, $"De:  {d�tenteur}");
            StringAssert.Contains(d, $"Solde:  {solde:C}");
            StringAssert.Contains(d, $"Statut:  {statut}");
        }

        [TestMethod]
        [DataRow("0.01", 0, StatutCompte.Ok, true)]
        [DataRow("0.01", 234, StatutCompte.Ok, true)]
        [DataRow("100", 0, StatutCompte.Ok, true)]
        [DataRow("0.01", 0, StatutCompte.Gel�, false)]
        [DataRow("100", 0, StatutCompte.Gel�, false)]
        [DataRow("100", 123, StatutCompte.Gel�, false)]
        public void T06_PeutD�poser(string _montant, int solde, StatutCompte statut, bool r�ponseAttendue)
        {
            var montant = DecimalParse(_montant);
            var c1 = new Compte(1, "D", solde, statut);
            Assert.AreEqual(r�ponseAttendue, c1.PeutD�poser());
            Assert.AreEqual(r�ponseAttendue, c1.PeutD�poser(montant));
        }

        [TestMethod]
        [DataRow("0.01", 0, StatutCompte.Ok, false, false)]
        [DataRow("0.01", 234, StatutCompte.Ok, true, true)]
        [DataRow("100", 10, StatutCompte.Ok, true, false)]
        [DataRow("100", 100, StatutCompte.Ok, true, true)]
        [DataRow("100", 1000, StatutCompte.Ok, true, true)]
        [DataRow("0.01", 0, StatutCompte.Gel�, false, false)]
        [DataRow("0.01", 10, StatutCompte.Gel�, false, false)]
        [DataRow("100", 1000, StatutCompte.Gel�, false, false)]
        [DataRow("100", 123, StatutCompte.Gel�, false, false)]
        public void T07_PeutRetirer(string _montant, int solde, StatutCompte statut, bool r�ponse1, bool r�ponse2)
        {
            var montant = DecimalParse(_montant);
            var c = new Compte(1, "D", solde, statut);
            Assert.AreEqual(r�ponse1, c.PeutRetirer());
            Assert.AreEqual(r�ponse2, c.PeutRetirer(montant));
        }

        [TestMethod]
        [DataRow("0.01", "0", "0.01")]
        [DataRow("123.45", "0", "123.45")]
        [DataRow("0.01", "123.45", "123.46")]
        [DataRow("1111.11", "123.45", "1234.56")]
        public void T08_D�poser(string _montant, string _soldeAvant, string _soldeApr�s)
        {
            var montant = DecimalParse(_montant);
            var soldeAvant = DecimalParse(_soldeAvant);
            var soldeApr�s = DecimalParse(_soldeApr�s);
            var c = new Compte(1, "D", soldeAvant);
            c.D�poser(montant);
            Assert.AreEqual(soldeApr�s, c.Solde);
        }

        [TestMethod]
        [DataRow("0.01", "100", "99.99")]
        [DataRow("0.01", "0.01", "0")]
        [DataRow("123.45", "1000", "876.55")]
        public void T09_Retirer(string _montant, string _soldeAvant, string _soldeApr�s)
        {
            var montant = DecimalParse(_montant);
            var soldeAvant = DecimalParse(_soldeAvant);
            var soldeApr�s = DecimalParse(_soldeApr�s);
            var c = new Compte(1, "D", soldeAvant);
            c.Retirer(montant);
            Assert.AreEqual(soldeApr�s, c.Solde);
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
        public void T11_Geler(int num�ro, int solde)
        {
            var c = new Compte(num�ro, "D", solde);
            c.Geler();
            Assert.AreEqual(true, c.EstGel�);
            Assert.AreEqual(StatutCompte.Gel�, c.Statut);
            Assert.AreEqual(num�ro, c.Num�ro);
            Assert.AreEqual(solde, c.Solde);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(10, 10)]
        public void T12_D�geler(int num�ro, int solde)
        {
            var c = new Compte(num�ro, "D", solde, StatutCompte.Gel�);
            c.D�geler();
            Assert.AreEqual(false, c.EstGel�);
            Assert.AreEqual(StatutCompte.Ok, c.Statut);
            Assert.AreEqual(num�ro, c.Num�ro);
            Assert.AreEqual(solde, c.Solde);
        }
    }
}