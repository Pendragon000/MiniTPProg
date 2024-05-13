using BanqueLib;

namespace TestBanque
{
    [TestClass]
    [TestCategory("Isaak F.")]
    public class TestsC_BanqueOk
    {
        [TestMethod]
        public void T01a_Compilation()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void T01b_Encapsulation()
        {
            // Aucun champ public svp
            Assert.AreEqual(0, typeof(Banque).GetFields().Count(f => f.IsPublic));
        }

        [TestMethod]
        [DataRow("Banque Royale")]
        // [DataRow("      Banque Nationale     ")]
        public void T02_Construction_Simple_et_Getters(string nom)
        {
            var ban = new Banque(nom);
            Assert.AreEqual(nom.Trim(), ban.Nom);
            Assert.AreEqual(1, ban.ProchainNum�ro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesD�p�ts);
            Assert.AreEqual(0, ban.Comptes.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        public void T03_Construction_ProchainNum�ro(int prochain)
        {
            var ban = new Banque("T03", prochain);
            Assert.AreEqual("T03", ban.Nom);
            Assert.AreEqual(prochain, ban.ProchainNum�ro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesD�p�ts);
            Assert.AreEqual(0, ban.Comptes.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        // Comptes d�sordonn�es
        [DataRow(2, 5, 4, 3)]
        [DataRow(45, 20, 50, 30)]
        public void T04a_Construction_ArrayDeComptes(params int[] num�ros)
        {
            var ban = new Banque("T04", num�ros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            Array.Sort(num�ros);
            Assert.AreEqual("T04", ban.Nom);
            Assert.AreEqual(num�ros[^1] + 1, ban.ProchainNum�ro);
            Assert.AreEqual(num�ros.Length, ban.NombreDeComptes);
            Assert.AreEqual(num�ros.Sum(), ban.TotalDesD�p�ts);
            Assert.AreEqual(num�ros.Length, ban.Comptes.Count);
            for (int i = 0; i < num�ros.Length; i++)
            {
                Assert.AreEqual(num�ros[i], ban.Comptes[i].Num�ro);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        // Comptes d�sordonn�es
        [DataRow(2, 5, 4, 3)]
        [DataRow(45, 20, 50, 30)]
        public void T04b_Construction_ListeDeComptes(params int[] num�ros)
        {
            var ban = new Banque("T04", num�ros.Select(n => new Compte(n, "D" + n, n)).ToList());
            Array.Sort(num�ros);
            Assert.AreEqual("T04", ban.Nom);
            Assert.AreEqual(num�ros[^1] + 1, ban.ProchainNum�ro);
            Assert.AreEqual(num�ros.Length, ban.NombreDeComptes);
            Assert.AreEqual(num�ros.Sum(), ban.TotalDesD�p�ts);
            Assert.AreEqual(num�ros.Length, ban.Comptes.Count);
            for (int i = 0; i < num�ros.Length; i++)
            {
                Assert.AreEqual(num�ros[i], ban.Comptes[i].Num�ro);
            }
        }

        [TestMethod]
        [DataRow("B1")]
        [DataRow("B2")]
        public void T04c_Construction_ListeNull(string nom)
        {
            var ban = new Banque(nom, null);
            Assert.AreEqual(nom.Trim(), ban.Nom);
            Assert.AreEqual(1, ban.ProchainNum�ro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesD�p�ts);
            Assert.AreEqual(0, ban.Comptes.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        public void T04d_Construction_Prochain_ListeNull(int prochain)
        {
            var ban = new Banque("T03", prochain, null);
            Assert.AreEqual("T03", ban.Nom);
            Assert.AreEqual(prochain, ban.ProchainNum�ro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesD�p�ts);
            Assert.AreEqual(0, ban.Comptes.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3, 1)]
        [DataRow(31, 20, 30)]
        // Comptes d�sordonn�es
        [DataRow(8, 5, 4, 2, 3)]
        [DataRow(51, 20, 50, 30)]
        public void T05a_Construction_Prochain_et_ArrayDeComptes(int prochain, params int[] num�ros)
        {
            var ban = new Banque("T05", prochain, num�ros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            Array.Sort(num�ros);
            Assert.AreEqual("T05", ban.Nom);
            Assert.AreEqual(prochain, ban.ProchainNum�ro);
            Assert.AreEqual(num�ros.Length, ban.NombreDeComptes);
            Assert.AreEqual(num�ros.Sum(), ban.TotalDesD�p�ts);
            Assert.AreEqual(num�ros.Length, ban.Comptes.Count);
            for (int i = 0; i < num�ros.Length; i++)
            {
                Assert.AreEqual(num�ros[i], ban.Comptes[i].Num�ro);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3, 1)]
        [DataRow(31, 20, 30)]
        // Comptes d�sordonn�es
        [DataRow(8, 5, 4, 2, 3)]
        [DataRow(51, 20, 50, 30)]
        public void T05b_Construction_Prochain_et_ListeDeComptes(int prochain, params int[] num�ros)
        {
            var ban = new Banque("T05", prochain, num�ros.Select(n => new Compte(n, "D" + n, n)).ToList());
            Array.Sort(num�ros);
            Assert.AreEqual("T05", ban.Nom);
            Assert.AreEqual(prochain, ban.ProchainNum�ro);
            Assert.AreEqual(num�ros.Length, ban.NombreDeComptes);
            Assert.AreEqual(num�ros.Sum(), ban.TotalDesD�p�ts);
            Assert.AreEqual(num�ros.Length, ban.Comptes.Count);
            for (int i = 0; i < num�ros.Length; i++)
            {
                Assert.AreEqual(num�ros[i], ban.Comptes[i].Num�ro);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        public void T06a_DescriptionSommaire(params int[] num�ros)
        {
            var desc = new Banque("T06", num�ros.Select(n => new Compte(n, "D" + n, n)).ToArray()).DescriptionSommaire();
            StringAssert.Contains(desc, "T06");
            StringAssert.Contains(desc, "Prochain num�ro:  " + (num�ros[^1] + 1));
            StringAssert.Contains(desc, "Nombre de comptes:  " + num�ros.Length);
            StringAssert.Contains(desc, $"Total des d�p�ts:  {num�ros.Sum():C}");
        }

        [TestMethod]
        [DataRow(StatutCompte.Ok, 1)]
        [DataRow(StatutCompte.Gel�, 2, 3)]
        [DataRow(StatutCompte.Ok, 5, 7, 13)]
        [DataRow(StatutCompte.Gel�, 41, 42, 43)]
        public void T06b_DescriptionDesComptes(StatutCompte statut, params int[] num�ros)
        {
            var desc = new Banque("T06", num�ros.Select(n => new Compte(n, "D" + n, 2 * n, statut)).ToArray()).DescriptionDesComptes();
            foreach (int n in num�ros)
            {
                StringAssert.Contains(desc, "# " + n);
                StringAssert.Contains(desc, "D" + n);
                StringAssert.Contains(desc, $"{2*n:C}");
                StringAssert.Contains(desc, $"{statut}");
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        public void T06c_DescriptionCompl�te(params int[] num�ros)
        {
            var ban = new Banque("T06", num�ros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            StringAssert.Contains(ban.DescriptionCompl�te(), ban.DescriptionSommaire());
            StringAssert.Contains(ban.DescriptionCompl�te(), ban.DescriptionDesComptes());
        }

        [TestMethod]
        [DataRow(9, false)]
        [DataRow(10, true)]
        [DataRow(11, false)]
        [DataRow(20, true)]
        [DataRow(40, true)]
        [DataRow(49, false)]
        [DataRow(50, true)]
        [DataRow(51, false)]
        public void T07_ChercherCompte(int num�ro, bool succ�s)
        {
            var ban = new Banque("T07", new[] {10, 20, 30, 40, 50}.Select(n => new Compte(n, "D" + n, n)).ToArray());
            var compte = ban.ChercherCompte(num�ro);
            Assert.AreEqual(succ�s, compte is not null);
            if (compte is not null)
            {
                Assert.IsTrue(ban.Comptes.Contains(compte));
                Assert.AreEqual(num�ro, compte.Num�ro);
            }
        }

        [TestMethod]
        [DataRow(1, true)]
        [DataRow(3, false)]
        [DataRow(7, true)]
        [DataRow(11, false)]
        [DataRow(17, true)]
        [DataRow(23, false)]
        [DataRow(2, false)]
        [DataRow(24, false)]
        [DataRow(13, false)]
        public void T08a_PeutSupprimer_Num�ro(int num�ro, bool succ�s)
        {
            var ban = new Banque("T08a", 
                        [new(1, "D1"), 
                        new(3, "D3", 6), 
                        new(7, "D7"), 
                        new(11, "D11", 0, StatutCompte.Gel�),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gel�)]);
            Assert.AreEqual(succ�s, ban.PeutSupprimerCompte(num�ro));
        }

        [TestMethod]
        [DataRow(0, true)]
        [DataRow(1, false)]
        [DataRow(2, true)]
        [DataRow(3, false)]
        [DataRow(4, true)]
        [DataRow(5, false)]
        [DataRow(-1, false)]
        public void T08a_PeutSupprimer_R�f�rence(int i, bool succ�s)
        {
            var ban = new Banque("T08b",
                        [new(1, "D1"),
                        new(3, "D3", 6),
                        new(7, "D7"),
                        new(11, "D11", 0, StatutCompte.Gel�),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gel�)]);
            Assert.AreEqual(succ�s, ban.PeutSupprimerCompte(
                ban.Comptes.ElementAtOrDefault(i) ?? new Compte(1, "D1")));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        public void T09_Cr�erCompte(params int[] num�ros)
        {
            var ban = new Banque("T09", num�ros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            var compte = ban.Cr�erCompte("D");
            compte.D�poser(100);
            Assert.AreEqual("D", compte.D�tenteur);
            Assert.AreEqual(num�ros[^1] + 1, compte.Num�ro);
            Assert.AreEqual(100m, compte.Solde);
            Assert.AreEqual(StatutCompte.Ok, compte.Statut);
            Assert.AreEqual(num�ros[^1] + 2, ban.ProchainNum�ro);
            Assert.AreEqual(num�ros.Length + 1, ban.NombreDeComptes);
            Assert.AreEqual(num�ros.Sum() + 100, ban.TotalDesD�p�ts);
            Assert.AreSame(compte, ban.Comptes[^1]);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(7)]
        [DataRow(17)]
        public void T10a_SupprimerCompte_Num�ro(int num�ro)
        {
            var ban = new Banque("T10a",
                        [new(1, "D1"),
                        new(3, "D3", 6),
                        new(7, "D7"),
                        new(11, "D11", 0, StatutCompte.Gel�),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gel�)]);
            var compte = ban.ChercherCompte(num�ro);
            Assert.IsTrue(ban.Comptes.Contains(compte));
            ban.SupprimerCompte(num�ro);
            Assert.IsFalse(ban.Comptes.Contains(compte));
            Assert.AreEqual(24, ban.ProchainNum�ro);
            Assert.AreEqual(5, ban.NombreDeComptes);
            Assert.AreEqual(52, ban.TotalDesD�p�ts);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2)]
        [DataRow(4)]
        public void T10b_SupprimerCompte_R�f�rence(int indice)
        {
            var ban = new Banque("T10b",
                        [new(1, "D1"),
                        new(3, "D3", 6),
                        new(7, "D7"),
                        new(11, "D11", 0, StatutCompte.Gel�),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gel�)]);
            var compte = ban.Comptes[indice];
            ban.SupprimerCompte(compte);
            Assert.IsFalse(ban.Comptes.Contains(compte));
            Assert.AreEqual(24, ban.ProchainNum�ro);
            Assert.AreEqual(5, ban.NombreDeComptes);
            Assert.AreEqual(52, ban.TotalDesD�p�ts);
        }
    }
}