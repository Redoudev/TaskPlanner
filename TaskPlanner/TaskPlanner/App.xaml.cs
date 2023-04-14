using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskPlanner.Views;

// Exporte les fichiers de police de caractères dans l'application avec des alias pour une utilisation facile
[assembly: ExportFont("OpenSans-Bold.ttf", Alias = "OpenSansBold")]
[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "OpenSansRegular")]
[assembly: ExportFont("Sitka.ttc", Alias = "Sitka")]
namespace TaskPlanner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Définit la page principale de l'application comme une instance de NavigationPage avec MainPage comme page racine
            MainPage = MainPage = new NavigationPage(new MainPage());
        }

        // Méthode appelée lorsque l'application démarre
        protected override void OnStart()
        {
        }

        // Méthode appelée lorsque l'application est mise en pause
        protected override void OnSleep()
        {
        }

        // Méthode appelée lorsque l'application est reprise après une mise en pause
        protected override void OnResume()
        {
        }
    }
}
