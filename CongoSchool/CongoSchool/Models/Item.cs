using System;
using SQLite;

namespace CongoSchool.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NatureCandidat { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Examen { get; set; }
        public string Serie { get; set; }

        public string Sexe { get; set; }
        public DateTime DateNaissance { get; set; }
        public string LieuNaissance { get; set; }
        public string PaysNaissance { get; set; }

        public string Image { get; set; }
    }
}
