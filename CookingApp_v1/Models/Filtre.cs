using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CookingApp_v1.Models
{
    public class Filtre
    {
        // id-urile pentru tipurile de filtre merg din 100 in 100 pentru a putea adauga mai multe
        [PrimaryKey]
        public int FT_id { get; set; }
        public string FT_descriere { get; set; }
    }
}
