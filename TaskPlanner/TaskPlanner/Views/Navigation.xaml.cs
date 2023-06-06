using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPlanner.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : TabbedPage
    {

        public Navigation()
        {
            InitializeComponent();
        }

        // Méthode pour gérer le clic sur l'icone "Log Out"
        private async void Log_Out(object sender, EventArgs e)
        {
            var result = await this.DisplayAlert("Confirmation", "Voulez-vous vraiment vous déconnecter ?", "Oui", "Non");

            if (result)
            {
                // L'utilisateur a cliqué sur "Oui", donc se déconnecter
                await Navigation.PushAsync(new MainPage());
            }
        }


        // Cette méthode est appelée lorsque l'utilisateur appuie sur le bouton de retour en arrière dans l'application.
        protected override bool OnBackButtonPressed()
        {
            // Indiquer que l'événement a été géré
            return true;
        }

    }
}