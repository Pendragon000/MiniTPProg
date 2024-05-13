using BanqueLib;

while (true)
{
    Console.Clear();
    Console.WriteLine("""
    
     TESTER BANQUE

     1 - Création d'une banque simple
     2 - Création d'une banque avec prochain numéro
     3 - Création d'une banque avec prochain et liste null
     4 - Création d'une banque avec prochain et comptes (liste)
     5 - Création d'une banque avec prochain et comptes (array)
     6 - Champs calculés
     7 - Recherche d'un compte
     8 - Création d'un compte
     9 - Supression d'un compte (par référence)
     0 - Supression d'un compte (par numéro)
     a - Exceptions (création de compte)
     b - Exceptions (numéro de compte invalide)
     c - Exceptions (référence null)
     d - Exceptions (construction: nom de la banque)
     e - Exceptions (construction: prochain numéro)
     f - Exceptions (construction: liste des comptes)
     q - Quitter

     Votre choix, Isaak F. ?

    """);

    switch (Console.ReadKey(true).KeyChar)
    {
        case '1':
            Console.WriteLine(new Banque("Banque simple").DescriptionSommaire());
            break;
        case '2':
            Console.WriteLine(new Banque("Banque avec prochain", 10).DescriptionSommaire());
            break;
        case '3':
            {
                var banque = new Banque("Banque avec prochain et liste null", 20, null);
                Console.WriteLine(banque.DescriptionSommaire());
                Console.WriteLine();
                Console.Write(banque.DescriptionDesComptes());
            }
            break;
        case '4':
            {
                var banque = new Banque("Banque avec prochain et comptes (liste)", 100, new List<Compte> {
                                        new(12, "Détenteur 12"),
                                        new(7, "Détenteur 7", 400),
                                        new(3, "Détenteur 3", 300, StatutCompte.Gelé),
                                        new(5, "Détenteur 5", 800, StatutCompte.Gelé)});
                Console.Write(banque.DescriptionComplète());
            }
            break;
        case '5':
            {
                var banque = new Banque("Banque avec prochain et comptes (array)", 200, new Compte[] {
                                        new(12, "Détenteur 12"),
                                        new(7, "Détenteur 7", 500),
                                        new(2, "Détenteur 2", 200),
                                        new(55, "Détenteur 55", 900, StatutCompte.Gelé)});
                Console.Write(banque.DescriptionComplète());
            }
            break;
        case '6': // Champs calculés
            {
                var banque = new Banque("Banque de champs calculés",
                                        [new(1, "Détenteur 1"),
                                        new(6, "Détenteur 6", 300),
                                        new(4, "Détenteur 4", 700, StatutCompte.Gelé)]);
                Console.WriteLine(banque.DescriptionComplète());
                Console.WriteLine($"               Nom: {banque.Nom}");
                Console.WriteLine($"   Prochain numéro: {banque.ProchainNuméro}");
                Console.WriteLine($"  Total des dépôts: {banque.TotalDesDépôts:C}");
                Console.WriteLine($" Nombre de comptes: {banque.NombreDeComptes}");
                Console.WriteLine($"    Premier numéro: {banque.Comptes[0].Numéro}\n");
            }
            break;
        case '7': // Recherche d'un compte
            {
                var banque = new Banque("Banque de recherche", 200, new Compte[] {
                                        new(12, "Détenteur 12"),
                                        new(7, "Détenteur 7", 500),
                                        new(2, "Détenteur 2", 200),
                                        new(55, "Détenteur 55", 900, StatutCompte.Gelé)});
                Console.WriteLine(banque.ChercherCompte(23)?.Description() ?? " null\n");
                Console.WriteLine(banque.ChercherCompte(12)?.Description() ?? " null\n");
            }
            break;
        case '8': // Création d'un compte
            {
                var banque = new Banque("Banque de création", new Compte[] {
                                        new(12, "Détenteur 12"),
                                        new(7, "Détenteur 7", 500)});
                Compte compte = banque.CréerCompte("Han Solo");
                Console.WriteLine(" Création du compte numéro: {0}\n", compte.Numéro);
                Console.Write(banque.DescriptionComplète());
            }
            break;
        case '9': // Suppression d'un compte (par référence)
            {
                var banque = new Banque("Banque de suppression", new Compte[] {
                                        new(19, "Détenteur 19", 1000),
                                        new(12, "Détenteur 12"),
                                        new(1, "Détenteur 1", 0, StatutCompte.Gelé),
                                        new(7, "Détenteur 7", 500)});
                Console.WriteLine(" Peut supprimer 1er      : " + banque.PeutSupprimerCompte(banque.Comptes[0]));
                Console.WriteLine(" Peut supprimer 2e       : " + banque.PeutSupprimerCompte(banque.Comptes[1]));
                Console.WriteLine(" Peut supprimer 3e       : " + banque.PeutSupprimerCompte(banque.Comptes[2]));
                Console.WriteLine(" Peut supprimer étranger : " + banque.PeutSupprimerCompte(new Compte(1, "D")));
                banque.SupprimerCompte(banque.Comptes[1]);
                Console.WriteLine("\n Le 2e compte a été supprimé\n");
                Console.Write(banque.DescriptionComplète());
            }
            break;
        case '0': // Suppression d'un compte (par numéro)
            {
                var banque = new Banque("Banque de suppression", new Compte[] {
                                        new(19, "Détenteur 19", 1000),
                                        new(12, "Détenteur 12"),
                                        new(1, "Détenteur 1", 0, StatutCompte.Gelé),
                                        new(7, "Détenteur 7", 500)});
                Console.WriteLine(" Peut supprimer 1er        : " + banque.PeutSupprimerCompte(1));
                Console.WriteLine(" Peut supprimer 2e         : " + banque.PeutSupprimerCompte(7));
                Console.WriteLine(" Peut supprimer 3e         : " + banque.PeutSupprimerCompte(12));
                Console.WriteLine(" Peut supprimer inexistant : " + banque.PeutSupprimerCompte(99));
                banque.SupprimerCompte(12);
                Console.WriteLine("\n Le compte numéro 12 a été supprimé\n");
                Console.Write(banque.DescriptionComplète());
            }
            break;
        case 'a':  // Erreurs de création de compte
            {
                var banque = new Banque("Santander");
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => banque.CréerCompte(null!)),
                    Utile.ExceptMsg(() => banque.CréerCompte("")),
                    Utile.ExceptMsg(() => banque.CréerCompte("       ")),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'b':  // erreurs de numéro de compte
            {
                var banque = new Banque("Santander");
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => banque.ChercherCompte(0)),
                    Utile.ExceptMsg(() => banque.ChercherCompte(-10)),
                    Utile.ExceptMsg(() => banque.PeutSupprimerCompte(0)),
                    Utile.ExceptMsg(() => banque.PeutSupprimerCompte(-10)),
                    Utile.ExceptMsg(() => banque.SupprimerCompte(0)),
                    Utile.ExceptMsg(() => banque.SupprimerCompte(-10)),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'c':  // erreurs de référence null
            {
                var banque = new Banque("Santander");
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => banque.PeutSupprimerCompte(null!)),
                    Utile.ExceptMsg(() => banque.SupprimerCompte(null!)),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'd':  // erreurs de construction (nom de la banque)
            {
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => new Banque(null!)),
                    Utile.ExceptMsg(() => new Banque("")),
                    Utile.ExceptMsg(() => new Banque("     ")),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'e':  // erreurs de construction (prochain numéro)
            {
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => new Banque("B", 0)),
                    Utile.ExceptMsg(() => new Banque("B", -10)),
                    Utile.ExceptMsg(() => new Banque("B", 3, [new Compte(5, "D")])),
                    Utile.ExceptMsg(() => new Banque("B", 7, [new Compte(1, "D1"), new Compte(7, "D2"), new Compte(5, "D3")])),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'f':  // erreurs de construction (liste des comptes)
            {
                var compte = new Compte(1, "D");
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => new Banque("B", [compte, null!, new Compte(3, "D2")])),
                    Utile.ExceptMsg(() => new Banque("B", [compte, new Compte(3, "D2"), null!])),
                    Utile.ExceptMsg(() => new Banque("B", [compte, new Compte(3, "D2"), compte])),
                    Utile.ExceptMsg(() => new Banque("B", [compte, new Compte(3, "D2"), new Compte(3, "D3")])),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'q':
            Environment.Exit(0); break;
        default:
            Console.WriteLine(" Mauvais choix"); break;
    }
    Console.WriteLine("\n Appuyer sur ENTER pour continuer...");
    Console.ReadLine();
}

#pragma warning disable S3903 // Types should be defined in named namespaces
static class Utile
{
    public static (string, string?) ExceptMsg(Action action)
    {
        try
        {
            action();
            return ("EXCEPTION attendue", null);
        }
        catch (Exception ex)
        {
            return (ex.GetType().Name, ex.Message);
        }
    }
}
