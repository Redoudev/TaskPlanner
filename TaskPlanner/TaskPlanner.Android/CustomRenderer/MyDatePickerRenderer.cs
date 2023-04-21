using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TaskPlanner.CustomeRenderer;
using TaskPlanner.Droid.CustomRenderer;

// Indique que cette classe est un renderer personnalisé pour MyCustomeDatePicker et qu'elle est destinée à être utilisée sur Android
[assembly: ExportRenderer(typeof(MyCustomeDatePicker), typeof(MyDatePickerRenderer))]
namespace TaskPlanner.Droid.CustomRenderer
{
    // Cette classe hérite de la classe EntryRenderer de Xamarin.Forms.Platform.Android, qui fournit une implémentation par défaut
    // de la création de l'interface utilisateur pour DatePicker
    public class MyDatePickerRenderer : DatePickerRenderer
    {
        // Constructeur qui prend en paramètre un contexte Android
        public MyDatePickerRenderer(Context context) : base(context)
        {

        }

        // Méthode appelée chaque fois qu'un changement est apporté à l'élément Picker correspondant dans le code Xamarin.Forms
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            // Vérifie si l'élément natif Android a été créé
            if (Control != null)
            {
                // Définit la couleur de fond de l'élément natif Android sur transparent
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }


}