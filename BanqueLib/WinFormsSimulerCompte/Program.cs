using BanqueLib;
using System.Text.Json;
namespace WinFormsSimulerCompte
{
    internal static class Program
    {
        private const string JsonFile = "compte.json";
        private static Compte mod�le = new Compte(12345,"testd�tenteur");
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
                int Num�ro = tempObject.GetProperty("Num�ro").GetInt32();
                string D�tenteur = tempObject.GetProperty("D�tenteur").GetString()!;
                decimal Solde = tempObject.GetProperty("Solde").GetDecimal();
                int Statut = tempObject.GetProperty("Statut").GetInt32();
                mod�le = new Compte(Num�ro, D�tenteur, Solde, (StatutCompte)Statut);
            }
            Application.ApplicationExit += Application_ApplicationExit;
            ApplicationConfiguration.Initialize();
            Application.Run(new FromIsaakFortin(mod�le));
        }

        private static void Application_ApplicationExit(object? sender, EventArgs e)
        {
            mod�le.S�rialiserCompte();
        }
    }
}