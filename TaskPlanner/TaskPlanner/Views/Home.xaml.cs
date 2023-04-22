using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TaskPlanner.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        // URL de l'API, avec un paramètre pour l'ID utilisateur
        private const string apiUrl = "https://xamarinn.alwaysdata.net/taskplanner/controllers/read/readTask.php?id_utilisateur={0}";

        // Instance de la classe HttpClient pour effectuer les requêtes HTTP
        private HttpClient client = new HttpClient();

        // Liste pour stocker les tâches récupérées depuis l'API
        private List<Task> tasks = new List<Task>();

        public Home()
        {
            InitializeComponent();

            // Appel de la méthode GetTasks pour rafraîchir la liste des tâches lorsqu'on revient sur la page
            GetTasks();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetTasks();
        }

        private async void GetTasks()
        {
            try
            {
                // Récupérer l'ID utilisateur depuis les paramètres globaux
                int userId = AppSettings.UserId;

                // On appelle l'API pour récupérer les tâches pour cet utilisateur
                var response = await client.GetAsync(string.Format(apiUrl, userId));
                response.EnsureSuccessStatusCode();

                // On convertit la réponse en une liste de tâches
                string json = await response.Content.ReadAsStringAsync();
                tasks = JsonConvert.DeserializeObject<List<Task>>(json);

                // On affiche les tâches dans la ListView
                TasksListView.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", "Une erreur est survenue : " + ex.Message, "OK");
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null)
                return;

            // définir la couleur de fond sur Transparent
            ((ListView)sender).SelectedItem = null;
        }
    }
}