using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TaskPlanner.CustomeRenderer
{
    // La classe MyCustomEntry étend la classe Entry de Xamarin.Forms pour créer un contrôle de saisie de texte personnalisé.
    public class MyCustomeDatePicker : DatePicker
    {
        // Définit le constructeur de la classe "MyCustomeDatePicker".
        public MyCustomeDatePicker()
        {
            // Définit la propriété "MinimumDate" de l'objet "MyCustomeDatePicker"
            // avec la date et l'heure actuelles en utilisant la classe "DateTime".

           // MinimumDate = DateTime.Now;
        }
    }
}
