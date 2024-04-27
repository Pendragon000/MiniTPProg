using BanqueLib;
using System.Text.Json;
namespace WinFormsSimulerCompte
{
    internal static class Program
    {
        private const string JsonFile = "compte.json";
        private static Compte modèle;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (File.Exists(JsonFile))
            {
                modèle = modèle.DésérialiserCompte(); 
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