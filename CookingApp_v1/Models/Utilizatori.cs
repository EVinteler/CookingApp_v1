using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CookingApp_v1.Models
{
    public class Utilizatori
    {
        [PrimaryKey, AutoIncrement]
        public int U_id { get; set; }
        public string U_nume { get; set; }
        public string U_email { get; set; }
        public string U_parola { get; set; }

        [AutoIncrement]
        // U_frigider va fi F_id-ul unui frigider si se va incremeneta automat
        public int U_frigider { get; set; }

        // daca nu merge cu 1:M, schimba in string si converteste in functia de cautare
        [OneToMany]
        public string U_filtre { get; set; }
    }
}
