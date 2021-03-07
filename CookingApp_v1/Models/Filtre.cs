using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CookingApp_v1.Models
{
    public class Filtre
    {
        [PrimaryKey, AutoIncrement]
        public int FT_id { get; set; }
        public string FT_descriere { get; set; }
    }
}
