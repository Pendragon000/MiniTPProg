using BanqueLib;
using System.Text.Json;
namespace WinFormsSimulerCompte
{
    internal static class Program
    {
        private const string JsonFile = "compte.json";
        private static Compte mod�le;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (File.Exists(JsonFile))
            {
                mod�le = mod�le.D�s�rialiserCompte(); 
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