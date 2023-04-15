using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using System.Security.Cryptography;

namespace TaskPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        // Déclaration d'une instance de HttpClient pour les requêtes HTTP
        private HttpClient _client = new HttpClient();


        // Constructeur de la page d'inscription
        public Registration()
        {
            InitializeComponent();
        }

        // Méthode pour gérer le clic sur l'icône pour le mot de passe (cacher/visible)
        private void OnEyeIconTapped(object sender, EventArgs e)
        {
            if (txtMotDePasse.IsPassword)
            {
                txtMotDePasse.IsPassword = false;
                eyeIcon.Source = "eye_open.png";
            }
            else
            {
                txtMotDePasse.IsPassword = true;
                eyeIcon.Source = "eye_closed.png";
            }
        }

        // Méthode pour gérer le clic sur le lien "Connexion"
        private async void LabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        // Méthode pour gérer le clic sur le bouton "S'inscrire"
        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Vérifier si les champs de texte pour le nom d'utilisateur et le mot de passe sont vides
            if (string.IsNullOrEmpty(txtNomUtilisateur.Text) || string.IsNullOrEmpty(txtMotDePasse.Text))
            {
                // Afficher une alerte pour remplir tous les champs et quitter la méthode
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
                return;
            }

            // Récupérer le nom d'utilisateur et le mot de passe
            var nom_utilisateur = txtNomUtilisateur.Text;
            var mot_de_passe = txtMotDePasse.Text;



            // Vérifier si le nom d'utilisateur est déjà présent dans la base de données
            var checkData = new Dictionary<string, string>
    {
        {"nom_utilisateur", nom_utilisateur}
    };
            // Créer un objet StringContent en utilisant le contenu JSON du dictionnaire
            var checkContent = new StringContent(JsonConvert.SerializeObject(checkData), Encoding.UTF8, "application/json");

            // Envoyer une requête POST à une URL spécifique
            var checkResponse = await _client.PostAsync("https://xamarinn.alwaysdata.net/taskplanner/controllers/other/existingUser.php", checkContent);

            if (checkResponse.IsSuccessStatusCode)
            {
                // Afficher une alerte pour indiquer que le nom d'utilisateur est déjà pris et quitter la méthode
                await DisplayAlert("Erreur", "Le nom d'utilisateur est déjà pris", "OK");
                return;
            }

            // Créer un dictionnaire contenant le nom d'utilisateur et le mot de passe
            var data = new Dictionary<string, string>
    {
        {"nom_utilisateur", nom_utilisateur},
        {"mot_de_passe", mot_de_passe}
    };

            // Créer un objet StringContent en utilisant le contenu JSON du dictionnaire
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Envoyer une autre requête POST à une autre URL pour ajouter l'utilisateur à la base de données
            var response = await _client.PostAsync("https://xamarinn.alwaysdata.net/taskplanner/controllers/add/addUser.php", content);

            if (response.IsSuccessStatusCode)
            {
                // Afficher une alerte pour indiquer que l'opération a réussi
                await DisplayAlert("Succès", "L'utilisateur a été ajouté avec succès", "OK");

                // Réinitialiser les champs de texte pour le nom d'utilisateur et le mot de passe
                txtNomUtilisateur.Text = "";
                txtMotDePasse.Text = "";
            }
            else
            {
                // Afficher une alerte pour indiquer qu'une erreur s'est produite lors de l'ajout de l'utilisateur
                await DisplayAlert("Erreur", "Une erreur s'est produite lors de l'ajout de l'utilisateur", "OK");
            }


        }
    }
}