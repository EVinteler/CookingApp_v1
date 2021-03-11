using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CookingApp_v1.Models
{
    public class Ingrediente
    {
        [PrimaryKey, AutoIncrement]
        public int N_id { get; set; }
        public string N_nume { get; set; }
        public string N_categorie { get; set; }
        public string N_subcategorie { get; set; }
        public string N_descriere { get; set; }
        public string N_link_imagine { get; set; }
    }
}
