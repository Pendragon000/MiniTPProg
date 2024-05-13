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
            Assert.AreEqual(1, ban.ProchainNuméro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesDépôts);
            Assert.AreEqual(0, ban.Comptes.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        public void T03_Construction_ProchainNuméro(int prochain)
        {
            var ban = new Banque("T03", prochain);
            Assert.AreEqual("T03", ban.Nom);
            Assert.AreEqual(prochain, ban.ProchainNuméro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesDépôts);
            Assert.AreEqual(0, ban.Comptes.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        // Comptes désordonnées
        [DataRow(2, 5, 4, 3)]
        [DataRow(45, 20, 50, 30)]
        public void T04a_Construction_ArrayDeComptes(params int[] numéros)
        {
            var ban = new Banque("T04", numéros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            Array.Sort(numéros);
            Assert.AreEqual("T04", ban.Nom);
            Assert.AreEqual(numéros[^1] + 1, ban.ProchainNuméro);
            Assert.AreEqual(numéros.Length, ban.NombreDeComptes);
            Assert.AreEqual(numéros.Sum(), ban.TotalDesDépôts);
            Assert.AreEqual(numéros.Length, ban.Comptes.Count);
            for (int i = 0; i < numéros.Length; i++)
            {
                Assert.AreEqual(numéros[i], ban.Comptes[i].Numéro);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        // Comptes désordonnées
        [DataRow(2, 5, 4, 3)]
        [DataRow(45, 20, 50, 30)]
        public void T04b_Construction_ListeDeComptes(params int[] numéros)
        {
            var ban = new Banque("T04", numéros.Select(n => new Compte(n, "D" + n, n)).ToList());
            Array.Sort(numéros);
            Assert.AreEqual("T04", ban.Nom);
            Assert.AreEqual(numéros[^1] + 1, ban.ProchainNuméro);
            Assert.AreEqual(numéros.Length, ban.NombreDeComptes);
            Assert.AreEqual(numéros.Sum(), ban.TotalDesDépôts);
            Assert.AreEqual(numéros.Length, ban.Comptes.Count);
            for (int i = 0; i < numéros.Length; i++)
            {
                Assert.AreEqual(numéros[i], ban.Comptes[i].Numéro);
            }
        }

        [TestMethod]
        [DataRow("B1")]
        [DataRow("B2")]
        public void T04c_Construction_ListeNull(string nom)
        {
            var ban = new Banque(nom, null);
            Assert.AreEqual(nom.Trim(), ban.Nom);
            Assert.AreEqual(1, ban.ProchainNuméro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesDépôts);
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
            Assert.AreEqual(prochain, ban.ProchainNuméro);
            Assert.AreEqual(0, ban.NombreDeComptes);
            Assert.AreEqual(0m, ban.TotalDesDépôts);
            Assert.AreEqual(0, ban.Comptes.Count);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3, 1)]
        [DataRow(31, 20, 30)]
        // Comptes désordonnées
        [DataRow(8, 5, 4, 2, 3)]
        [DataRow(51, 20, 50, 30)]
        public void T05a_Construction_Prochain_et_ArrayDeComptes(int prochain, params int[] numéros)
        {
            var ban = new Banque("T05", prochain, numéros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            Array.Sort(numéros);
            Assert.AreEqual("T05", ban.Nom);
            Assert.AreEqual(prochain, ban.ProchainNuméro);
            Assert.AreEqual(numéros.Length, ban.NombreDeComptes);
            Assert.AreEqual(numéros.Sum(), ban.TotalDesDépôts);
            Assert.AreEqual(numéros.Length, ban.Comptes.Count);
            for (int i = 0; i < numéros.Length; i++)
            {
                Assert.AreEqual(numéros[i], ban.Comptes[i].Numéro);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3, 1)]
        [DataRow(31, 20, 30)]
        // Comptes désordonnées
        [DataRow(8, 5, 4, 2, 3)]
        [DataRow(51, 20, 50, 30)]
        public void T05b_Construction_Prochain_et_ListeDeComptes(int prochain, params int[] numéros)
        {
            var ban = new Banque("T05", prochain, numéros.Select(n => new Compte(n, "D" + n, n)).ToList());
            Array.Sort(numéros);
            Assert.AreEqual("T05", ban.Nom);
            Assert.AreEqual(prochain, ban.ProchainNuméro);
            Assert.AreEqual(numéros.Length, ban.NombreDeComptes);
            Assert.AreEqual(numéros.Sum(), ban.TotalDesDépôts);
            Assert.AreEqual(numéros.Length, ban.Comptes.Count);
            for (int i = 0; i < numéros.Length; i++)
            {
                Assert.AreEqual(numéros[i], ban.Comptes[i].Numéro);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        public void T06a_DescriptionSommaire(params int[] numéros)
        {
            var desc = new Banque("T06", numéros.Select(n => new Compte(n, "D" + n, n)).ToArray()).DescriptionSommaire();
            StringAssert.Contains(desc, "T06");
            StringAssert.Contains(desc, "Prochain numéro:  " + (numéros[^1] + 1));
            StringAssert.Contains(desc, "Nombre de comptes:  " + numéros.Length);
            StringAssert.Contains(desc, $"Total des dépôts:  {numéros.Sum():C}");
        }

        [TestMethod]
        [DataRow(StatutCompte.Ok, 1)]
        [DataRow(StatutCompte.Gelé, 2, 3)]
        [DataRow(StatutCompte.Ok, 5, 7, 13)]
        [DataRow(StatutCompte.Gelé, 41, 42, 43)]
        public void T06b_DescriptionDesComptes(StatutCompte statut, params int[] numéros)
        {
            var desc = new Banque("T06", numéros.Select(n => new Compte(n, "D" + n, 2 * n, statut)).ToArray()).DescriptionDesComptes();
            foreach (int n in numéros)
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
        public void T06c_DescriptionComplète(params int[] numéros)
        {
            var ban = new Banque("T06", numéros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            StringAssert.Contains(ban.DescriptionComplète(), ban.DescriptionSommaire());
            StringAssert.Contains(ban.DescriptionComplète(), ban.DescriptionDesComptes());
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
        public void T07_ChercherCompte(int numéro, bool succès)
        {
            var ban = new Banque("T07", new[] {10, 20, 30, 40, 50}.Select(n => new Compte(n, "D" + n, n)).ToArray());
            var compte = ban.ChercherCompte(numéro);
            Assert.AreEqual(succès, compte is not null);
            if (compte is not null)
            {
                Assert.IsTrue(ban.Comptes.Contains(compte));
                Assert.AreEqual(numéro, compte.Numéro);
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
        public void T08a_PeutSupprimer_Numéro(int numéro, bool succès)
        {
            var ban = new Banque("T08a", 
                        [new(1, "D1"), 
                        new(3, "D3", 6), 
                        new(7, "D7"), 
                        new(11, "D11", 0, StatutCompte.Gelé),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gelé)]);
            Assert.AreEqual(succès, ban.PeutSupprimerCompte(numéro));
        }

        [TestMethod]
        [DataRow(0, true)]
        [DataRow(1, false)]
        [DataRow(2, true)]
        [DataRow(3, false)]
        [DataRow(4, true)]
        [DataRow(5, false)]
        [DataRow(-1, false)]
        public void T08a_PeutSupprimer_Référence(int i, bool succès)
        {
            var ban = new Banque("T08b",
                        [new(1, "D1"),
                        new(3, "D3", 6),
                        new(7, "D7"),
                        new(11, "D11", 0, StatutCompte.Gelé),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gelé)]);
            Assert.AreEqual(succès, ban.PeutSupprimerCompte(
                ban.Comptes.ElementAtOrDefault(i) ?? new Compte(1, "D1")));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2, 3)]
        [DataRow(10, 20, 30)]
        public void T09_CréerCompte(params int[] numéros)
        {
            var ban = new Banque("T09", numéros.Select(n => new Compte(n, "D" + n, n)).ToArray());
            var compte = ban.CréerCompte("D");
            compte.Déposer(100);
            Assert.AreEqual("D", compte.Détenteur);
            Assert.AreEqual(numéros[^1] + 1, compte.Numéro);
            Assert.AreEqual(100m, compte.Solde);
            Assert.AreEqual(StatutCompte.Ok, compte.Statut);
            Assert.AreEqual(numéros[^1] + 2, ban.ProchainNuméro);
            Assert.AreEqual(numéros.Length + 1, ban.NombreDeComptes);
            Assert.AreEqual(numéros.Sum() + 100, ban.TotalDesDépôts);
            Assert.AreSame(compte, ban.Comptes[^1]);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(7)]
        [DataRow(17)]
        public void T10a_SupprimerCompte_Numéro(int numéro)
        {
            var ban = new Banque("T10a",
                        [new(1, "D1"),
                        new(3, "D3", 6),
                        new(7, "D7"),
                        new(11, "D11", 0, StatutCompte.Gelé),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gelé)]);
            var compte = ban.ChercherCompte(numéro);
            Assert.IsTrue(ban.Comptes.Contains(compte));
            ban.SupprimerCompte(numéro);
            Assert.IsFalse(ban.Comptes.Contains(compte));
            Assert.AreEqual(24, ban.ProchainNuméro);
            Assert.AreEqual(5, ban.NombreDeComptes);
            Assert.AreEqual(52, ban.TotalDesDépôts);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2)]
        [DataRow(4)]
        public void T10b_SupprimerCompte_Référence(int indice)
        {
            var ban = new Banque("T10b",
                        [new(1, "D1"),
                        new(3, "D3", 6),
                        new(7, "D7"),
                        new(11, "D11", 0, StatutCompte.Gelé),
                        new(17, "D17"),
                        new(23, "D23", 46, StatutCompte.Gelé)]);
            var compte = ban.Comptes[indice];
            ban.SupprimerCompte(compte);
            Assert.IsFalse(ban.Comptes.Contains(compte));
            Assert.AreEqual(24, ban.ProchainNuméro);
            Assert.AreEqual(5, ban.NombreDeComptes);
            Assert.AreEqual(52, ban.TotalDesDépôts);
        }
    }
}