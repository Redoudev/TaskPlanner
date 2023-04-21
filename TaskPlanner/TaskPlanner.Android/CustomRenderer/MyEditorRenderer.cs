﻿using Android.App;
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

// Indique que cette classe est un renderer personnalisé pour MyCustomeEditor et qu'elle est destinée à être utilisée sur Android
[assembly: ExportRenderer(typeof(MyCustomeEditor), typeof(MyEditorRenderer))]
namespace TaskPlanner.Droid.CustomRenderer
{
    // Cette classe hérite de la classe MyEditorRenderer de Xamarin.Forms.Platform.Android, qui fournit une implémentation par défaut
    // de la création de l'interface utilisateur pour Editor
    public class MyEditorRenderer : EditorRenderer
    {
        // Constructeur qui prend en paramètre un contexte Android
        public MyEditorRenderer(Context context) : base(context)
        {

        }

        // Méthode appelée chaque fois qu'un changement est apporté à l'élément Editor correspondant dans le code Xamarin.Forms
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
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