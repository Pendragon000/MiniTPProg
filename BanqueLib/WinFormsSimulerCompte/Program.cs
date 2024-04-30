using BanqueLib;
using System.Text.Json;
namespace WinFormsSimulerCompte
{
    internal static class Program
    {
        private const string JsonFile = "compte.json";
        private static Compte modèle = new Compte(12345,"testdétenteur");
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (File.Exists(JsonFile))
            {
                string Ejson = File.ReadAllText(JsonFile);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var tempObject = JsonSerializer.Deserialize<JsonElement>(Ejson);
                int Numéro = tempObject.GetProperty("Numéro").GetInt32();
                string Détenteur = tempObject.GetProperty("Détenteur").GetString()!;
                decimal Solde = tempObject.GetProperty("Solde").GetDecimal();
                int Statut = tempObject.GetProperty("Statut").GetInt32();
                modèle = new Compte(Numéro, Détenteur, Solde, (StatutCompte)Statut);
            }
            Application.ApplicationExit += Application_ApplicationExit;
            ApplicationConfiguration.Initialize();
            Application.Run(new FromIsaakFortin(modèle));
        }

        private static void Application_ApplicationExit(object? sender, EventArgs e)
        {
            modèle.SérialiserCompte();
        }
    }
}