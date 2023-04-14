using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPlanner.Models
{
    public class User
    {
        // Identifiant unique de l'utilisateur
        public string id_utilisateur { get; set; }

        // Nom de l'utilisateur
        public string nom_utilisateur { get; set; }

        // Mot de passe de l'utilisateur
        public string mot_de_passe { get; set; }
    }
}
