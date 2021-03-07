using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace CookingApp_v1.Models
{
    public class Utilizatori
    {
        [PrimaryKey, AutoIncrement]
        public int U_id { get; set; }
        public string U_nume { get; set; }
        public string U_email { get; set; }
        public string U_parola { get; set; }

        // daca nu merge cu 1:M, schimba in string si converteste la cautare
        public string U_frigider { get; set; }
        public string U_filtre { get; set; }
    }
}
