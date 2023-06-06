using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http; // Importer la classe HttpClient
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskPlanner.Models;  // Importer la classe Category
using Newtonsoft.Json;    // Importer la classe JsonConvert
using Xamarin.Essentials;

namespace TaskPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTask : ContentPage
    {
        // Récupérer l'ID utilisateur depuis les paramètres globaux
        int userId = AppSettings.UserId;

        // Définir l'URL de l'API pour la partie catégories (FR)
        private const string ApiUrlCategory = "https://xamarinn.alwaysdata.net/taskplanner/controllers/read/readCategory.php";
        // Définir l'URL de l'API pour la partie statuts (FR)
        private const string ApiUrlStatus = "https://xamarinn.alwaysdata.net/taskplanner/controllers/read/readStatus.php";

        private readonly HttpClient _client = new HttpClient(); //
        public AddTask()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////// CATEGORIES ////////////////////////////////////////////////////////////////

        // Méthode appelée lorsqu'on clique sur le picker de catégories
        private async void OnCategoryPickerFocused(object sender, EventArgs e)
        {
            // Utiliser un objet HttpClient pour envoyer une requête GET à l'API
            using (var client = new HttpClient())
            {
                try
                {
                    // Envoyer une requête GET à l'API pour récupérer les données des catégories
                    var response = await client.GetAsync(ApiUrlCategory); // Envoyer la requête

                    // Si la requête a réussi, désérialiser les données JSON en objets CategoryLists
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync(); // Lire la réponse
                        var categoryLists = JsonConvert.DeserializeObject<List<List<CategoryLists>>>(content);  // Désérialiser les données JSON

                        // Concaténer les listes des catégories en une seule liste
                        var categories = categoryLists.SelectMany(cl => cl).ToList();

                        // Afficher les données des catégories dans le picker
                        categoryPicker.ItemsSource = categories;
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Impossible de récupérer les contacts", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Impossible de récupérer les contacts : {ex.Message}", "OK");
                }
            }
        }

        ////////////////////////////////////////////// STATUS ////////////////////////////////////////////////////////////////

        // Méthode appelée lorsqu'on clique sur le picker de statut
        private async void OnStatusesPickerFocused(object sender, EventArgs e)
        {
            // Utiliser un objet HttpClient pour envoyer une requête GET à l'API
            using (var client = new HttpClient())
            {
                try
                {
                    // Envoyer une requête GET à l'API pour récupérer les données des statuts
                    var response = await client.GetAsync(ApiUrlStatus); // Envoyer la requête

                    // Si la requête a réussi, désérialiser les données JSON en objets Status
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync(); // Lire la réponse
                        var statusLists = JsonConvert.DeserializeObject<List<List<Status>>>(content);  // Désérialiser les données JSON

                        // Concaténer les listes de statuts en une seule liste
                        var statuses = statusLists.SelectMany(cl => cl).ToList();

                        // Afficher les données de statut dans le picker
                        statutPicker.ItemsSource = statuses;
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Impossible de récupérer les contacts", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Impossible de récupérer les contacts : {ex.Message}", "OK");
                }
            }
        }

        // Méthode appelée lorsque la page est chargée 
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Afficher l'ID utilisateur dans un pop-up
            //DisplayAlert("ID utilisateur", "L'ID de l'utilisateur est : " + userId, "OK");

            // Catégories
            // Utiliser un objet HttpClient pour envoyer une requête GET à l'API
            using (var client = new HttpClient())
            {
                try
                {
                    // Envoyer une requête GET à l'API pour récupérer les données des catégories
                    var response = await client.GetAsync(ApiUrlCategory); // Envoyer la requête

                    // Si la requête a réussi, désérialiser les données JSON en objets CategoryLists
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync(); // Lire la réponse
                        var categoryLists = JsonConvert.DeserializeObject<List<List<CategoryLists>>>(content); // Désérialiser les données JSON

                        // Concaténer les listes des catégories en une seule liste
                        var categories = categoryLists.SelectMany(cl => cl).ToList();

                        // Afficher les données des catégories dans le picker
                        categoryPicker.ItemsSource = categories;
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Impossible de récupérer les contacts", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Impossible de récupérer les contacts : {ex.Message}", "OK");
                }
            }

            // Statuts
            // Utiliser un objet HttpClient pour envoyer une requête GET à l'API
            using (var client = new HttpClient())
            {
                try
                {
                    // Envoyer une requête GET à l'API pour récupérer les données des statuts
                    var response = await client.GetAsync(ApiUrlStatus); // Envoyer la requête

                    // Si la requête a réussi, désérialiser les données JSON en objets Status
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync(); // Lire la réponse
                        var statusLists = JsonConvert.DeserializeObject<List<List<Status>>>(content); // Désérialiser les données JSON

                        // Concaténer les listes des statuts en une seule liste
                        var statuses = statusLists.SelectMany(cl => cl).ToList();

                        // Afficher les données des statuts dans le picker
                        statutPicker.ItemsSource = statuses;
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Impossible de récupérer les contacts", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Impossible de récupérer les contacts : {ex.Message}", "OK");
                }
            }
        }

        // AJOUTE DE LA TACHE

        private async void BtnAdd_Task(Object sender, EventArgs e)
        {
            // Vérification que les champs sont remplis
            if (string.IsNullOrWhiteSpace(txtTitre.Text) ||
                categoryPicker.SelectedItem == null || string.IsNullOrWhiteSpace(categoryPicker.SelectedItem.ToString()) ||
                statutPicker.SelectedItem == null || string.IsNullOrWhiteSpace(statutPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
                return;
            }

            // Récupérer l'ID du statut sélectionné dans le contrôle de sélection de statut personnalisé
            var selectedCategory = (CategoryLists)categoryPicker.SelectedItem;

            // Stocker l'ID du statut sélectionné dans une variable
            var categoryId = selectedCategory.id_categorie;


            // Récupérer l'ID de la catégorie sélectionné dans le contrôle de sélection de catégorie personnalisé
            var selectedStatus = (Status)statutPicker.SelectedItem;

            // Stocker l'ID de la catégorie sélectionné dans une variable
            var idStatut = selectedStatus.id_statut;



            // Récupérer le titre, date_echeance et note
            var titre = txtTitre.Text;
            var date_echeance = txtDateEcheance.Date.ToString("yyyy-MM-dd");
            // Récupération de la valeur de la note depuis le champ de texte "txtNote"
            var note = string.IsNullOrWhiteSpace(txtNote.Text) ? null : txtNote.Text;

            // Vérification si la note récupérée est null (ou vide ou composée d'espaces uniquement)
            if (note == null)
            {
                // Si la note est null, on la remplace par une chaîne de caractères contenant un espace vide.
                // Vous pouvez choisir n'importe quelle autre valeur par défaut pour la note ici.
                note = " ";
            }

            // Créer un dictionnaire contenant titre, date_echeance, note, id_categorie, id_statut, id_utilisateur
            var data = new Dictionary<string, string>
              {
                  {"titre", titre},
                  {"date_echeance",date_echeance},
                  {"note", note },
                  {"id_categorie", categoryId.ToString()},
                  {"id_statut", idStatut.ToString()},
                  {"id_utilisateur",userId.ToString()}
              };

            // Créer un objet StringContent en utilisant le contenu JSON du dictionnaire
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Envoyer une autre requête POST à une autre URL pour ajouter la tache à la base de données
            var response = await _client.PostAsync("https://xamarinn.alwaysdata.net/taskplanner/controllers/create/createTask.php", content);

            if (response.IsSuccessStatusCode)
            {
                // Afficher une alerte pour indiquer que l'opération a réussi
                await DisplayAlert("Succès", "La tache a été ajouté avec succès", "OK");

                // Réinitialisation des champs du formulaire
                txtTitre.Text = "";
                txtNote.Text = "";
            }
            else
            {
                // Afficher une alerte pour indiquer qu'une erreur s'est produite lors de l'ajout de la tache
                await DisplayAlert("Erreur", "Une erreur s'est produite lors de l'ajout du contact", "OK");
            }
        }
    }
}