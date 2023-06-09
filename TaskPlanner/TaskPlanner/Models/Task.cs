﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPlanner.Models
{
    // Ce code déclare une classe "Task" avec quatre propriétés:
    public class Task
    {
        // Identifiant unique de la tache
        public int id_tache { get; set; }

        // Titre de la tache
        public string titre { get; set; }

        // Date echeance de la tache
        public string date_echeance { get; set; }

        // Nom de la categorie de la tache
        public string nom_categorie { get; set; }

        // Nom de du statut de la tache
        public string nom_statut { get; set; }

        // Note de la tache
        public string note { get; set; }
    }
}
