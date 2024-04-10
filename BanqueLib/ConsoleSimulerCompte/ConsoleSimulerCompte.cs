
using BanqueLib;
using System.Xml;
using System.Text.Json;
string filePath = "compte.json";
Compte compte = new Compte(Random.Shared.Next(100,1000),"Isaak Fortin");
while (true)
{
    Console.Clear();
    Console.WriteLine(compte.Description());
    Console.WriteLine(
        "1 - Modifier détendeur\n" +
        "2 - Peut déposer\n" +
        "3 - Peut retirer\n" +
        "4 - Peut retirer (montant)\n" +
        "5 - Déposer (montant)\n" +
        "6 - Retirer (montant)\n" +
        "7 - Vider\n" +
        "8 - Geler\n" +
        "9 - Dégeler\n" +
        "0 - To Json\n" +
        "= - From Json" +
        "q - Quitter\n" +
        "r - Reset\n" +
        "\nVotre choix, Isaak Fortin?\n");
    switch (Console.ReadKey(true).KeyChar)
    {
        case '1':
            string tempDétenteur = compte.Détenteur + Random.Shared.Next(1,100);
            Console.WriteLine($"** Détenteur modifier pour: {tempDétenteur}");
            compte.SetDétenteur(tempDétenteur);
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '2':
            Console.Write("** Peut déposer?");
            switch (compte.PeutDéposer())
            {
                case true:
                    Console.Write(" Oui\n");
                    break;
                case false:
                    Console.Write(" Non\n");
                    break;
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '3':
            Console.Write("** Peut retirer?");
            switch (compte.PeutRetirer())
            {
                case true:
                    Console.Write(" Oui");
                    break;
                case false:
                    Console.Write(" Non");
                    break;
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '4':
            decimal depot4 = RandomDec();
            Console.Write($"** Peut retirer {depot4} $");
            switch (compte.PeutRetirer(depot4))
            {
                case true:
                Console.Write(" Oui");
                break;
                case false:
                Console.Write(" Non");
                break;
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '5':
            decimal depot5 = RandomDec();
            try
            {
                compte.Déposer(depot5);
                Console.WriteLine($"** dépot de {depot5} $");
            }
            catch (Exception)
            {
                Console.WriteLine($"** impossible de déposer {depot5} $");
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '6':
            decimal depot6 = RandomDec();
            try
            {
                compte.Retirer(depot6);
                Console.WriteLine($"retrait de {depot6} $");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"impossible de retirer {depot6} $");
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break ;
        case '7':
            try
            {
                Console.WriteLine($"** Retrait complet de {compte.Vider()}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("** impossible de vider un compte vide ou geler");    
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '8':
            try
            {
                compte.Geler();
                Console.WriteLine("** Compte a été gelé.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("** Impossible de geler un compte déja gelé.");
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '9':
            try
            {
                compte.Dégeler();
                Console.WriteLine("** Compte a été dégelé.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("** Impossible de dégeler un compte non gelé.");
            }
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '0':
            compte.SérialiserCompte();
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case '=':
            compte = compte.DésérialiserCompte();
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        case 'q':
            Environment.Exit(0);
            break;
        case 'r':
            compte = new Compte(Random.Shared.Next(100, 1000), "Isaak Fortin");
            Console.WriteLine("un nouveau compte a été créé");
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERREUR dans l'entrée du choix");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nAppuyer sur ENTER pour continuer...");
            Console.ReadLine();
            break;
            
    }
}

///<summary>
///un chiffre décimal aléatoire
///</summary>
///<returns>retourne un chiffre decimal entre 0.01 et 99.99</returns>
static decimal RandomDec()
{
    decimal virgule = Random.Shared.Next(0, 100);
    int entier = Random.Shared.Next(0, 100);
    virgule = 0.01m * virgule;
    return entier + virgule;
}