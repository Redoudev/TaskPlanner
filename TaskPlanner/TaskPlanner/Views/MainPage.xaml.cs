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
            // Masquer le bouton de retour en arrière
            NavigationPage.SetHasBackButton(this, false);
        }

        private void OnEyeIconTapped(object sender, EventArgs e)
        {
            // Si le champ mot de passe est actuellement masqué
            if (txtMotDePasse.IsPassword)
            {
                // Affiche le mot de passe
                txtMotDePasse.IsPassword = false;

                // Change l'icone pour montrer que le mot de passe est maintenant visible
                eyeIcon.Source = "eye_open.png";
            }
            // Si le champ mot de passe est actuellement visible
            else
            {
                // Masque le mot de passe
                txtMotDePasse.IsPassword = true;

                // Change l'icone pour montrer que le mot de passe est maintenant caché
                eyeIcon.Source = "eye_closed.png";
            }
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
                // Récupérer le contenu de la réponse
                var responseContent = await response.Content.ReadAsStringAsync();

                // Désérialiser le contenu JSON de la réponse en un objet UserResponse
                var userResponse = JsonConvert.DeserializeObject<User>(responseContent);

                // Enregistrer l'ID utilisateur dans les paramètres globaux
                AppSettings.UserId = userResponse.id_utilisateur;

                // Naviguer vers la page Navigation
                await Navigation.PushAsync(new Navigation());

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

        // Cette méthode est appelée lorsque la page est affichée à l'écran.
        // Elle cache l'icône de l'œil et masque le mot de passe par défaut.
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Cacher l'icône de l'oeil
            eyeIcon.Source = "eye_closed.png";
            txtMotDePasse.IsPassword = true;
        }

        // Méthode pour gérer le clic sur le lien "S'inscrire"
        private async void LabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }

        // Cette méthode est appelée lorsque l'utilisateur appuie sur le bouton de retour en arrière dans l'application.
        protected override bool OnBackButtonPressed()
        {
            // Indiquer que l'événement a été géré
            return true;
        }
    }
}
