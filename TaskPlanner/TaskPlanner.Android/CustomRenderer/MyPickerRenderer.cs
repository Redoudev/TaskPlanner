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

// Cette annotation indique que cette classe est un renderer personnalisé pour MyCustomePicker et qu'elle est destinée à être utilisée sur Android
[assembly: ExportRenderer(typeof(MyCustomePicker), typeof(MyPickerRenderer))]
namespace TaskPlanner.Droid.CustomRenderer
{
    public class MyPickerRenderer : PickerRenderer
    {
        // Constructeur qui prend en paramètre un contexte Android
        public MyPickerRenderer(Context context) : base(context)
        {

        }
        // Méthode appelée chaque fois qu'un changement est apporté à l'élément Picker correspondant dans le code Xamarin.Forms
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
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