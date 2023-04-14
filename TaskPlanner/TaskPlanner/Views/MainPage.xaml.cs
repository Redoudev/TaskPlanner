using TaskPlanner.Models;
using TaskPlanner.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaskPlanner.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Vérifie si les champs nom_utilisateur et mot_de_passe sont vides ou nuls
            if (string.IsNullOrEmpty(txtNomUtilisateur.Text) || string.IsNullOrEmpty(txtMotDePasse.Text))
            {
                // Affiche une alerte avec le message "Veuillez remplir tous les champs" si l'un ou les deux champs sont vides
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
                return;
            }
            // Créer une instance de la classe User avec les valeurs entrées dans les champs nom_utilisateur et mot_de_passe
            User uti = new User
            {
                nom_utilisateur = txtNomUtilisateur.Text,
                mot_de_passe = txtMotDePasse.Text
            };
            // Définir l'adresse de la requête
            Uri RequestUri = new Uri("https://xamarinn.alwaysdata.net/taskplanner/controllers/other/loginUser.php");

            // Créer une nouvelle instance de HttpClient pour envoyer la requête
            var client = new HttpClient();

            // Convertir l'objet User en format JSON
            var json = JsonConvert.SerializeObject(uti);

            // Créer un contenu Http en utilisant l'objet JSON et le type de contenu "application/json"
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            // Envoyer la requête POST et attendre une réponse
            var response = await client.PostAsync(RequestUri, contentJson);

            // Si la réponse a le code de statut "OK" (200)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Naviguer vers la page Home
                await Navigation.PushAsync(new Home());

                // Effacer le contenu des champs nom_utilisateur et mot_de_passe
                txtNomUtilisateur.Text = string.Empty; // Ajout de cette ligne pour vider le champ txtNomUtilisateur
                txtMotDePasse.Text = string.Empty; // Ajout de cette ligne pour vider le champ txtMotDePasse
            }
            else
            {
                // Afficher une alerte indiquant que la connexion a échoué en raison d'un nom d'utilisateur ou d'un mot de passe incorrect
                await DisplayAlert("Connexion", "Nom d'utilisateur ou mot de passe incorrect", "OK");

                // Effacer le contenu du champ mot_de_passe
                txtMotDePasse.Text = string.Empty; // Ajout de cette ligne pour vider le champ txtMotDePasse

            }
        }
    }
}
