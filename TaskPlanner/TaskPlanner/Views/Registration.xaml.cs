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
            // Vérifier si les champs nom d'utilisateur et mot de passe sont vides
            if (string.IsNullOrEmpty(txtNomUtilisateur.Text) || string.IsNullOrEmpty(txtMotDePasse.Text))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
                return;
            }

            // Récupérer les valeurs des champs nom d'utilisateur et mot de passe
            var nom_utilisateur = txtNomUtilisateur.Text;
            var mot_de_passe = txtMotDePasse.Text;

            // Hacher le mot de passe avec un algorithme de hachage sécurisé
            var hashedPassword = HashPassword(mot_de_passe.ToString());

            // Créer un dictionnaire avec les données à envoyer
            var data = new Dictionary<string, string>
            {
               {"nom_utilisateur", nom_utilisateur},
               {"mot_de_passe", hashedPassword}
            };

            // Convertir les données en format JSON et les ajouter au contenu de la requête HTTP POST
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Envoyer la requête HTTP POST pour ajouter l'utilisateur à la base de données
            var response = await _client.PostAsync("https://xamarinn.alwaysdata.net/taskplanner/controllers/add/addUser.php", content);

            // Vérifier si la requête HTTP POST a réussi
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Succès", "L'utilisateur a été ajouté avec succès", "OK");
                // Réinitialiser les champs du formulaire
                txtNomUtilisateur.Text = "";
                txtMotDePasse.Text = "";
            }
            else
            {
                await DisplayAlert("Erreur", "Une erreur s'est produite lors de l'ajout de l'utilisateur", "OK");
            }

            // Méthode pour hacher le mot de passe
            string HashPassword(string motdepasse)
            {
                // Créer un sel aléatoire
                byte[] salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                // Concaténer le mot de passe avec le sel
                byte[] passwordBytes = Encoding.UTF8.GetBytes(motdepasse);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];
                Array.Copy(passwordBytes, saltedPassword, passwordBytes.Length);
                Array.Copy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hacher le mot de passe concaténé avec l'algorithme SHA256
                byte[] hashedBytes;
                using (var sha256 = SHA256.Create())
                {
                    hashedBytes = sha256.ComputeHash(saltedPassword);
                }

                // Concaténer le sel et le mot de passe haché
                byte[] saltedHash = new byte[hashedBytes.Length + salt.Length];
                Array.Copy(hashedBytes, saltedHash, hashedBytes.Length);
                Array.Copy(salt, 0, saltedHash, hashedBytes.Length, salt.Length);

                // Convertir le sel et le mot de passe haché en chaîne de caractères hexadécimale
                string saltedHashString = BitConverter.ToString(saltedHash).Replace("-", "").ToLower();
                return saltedHashString;
            }


        }
    }
}