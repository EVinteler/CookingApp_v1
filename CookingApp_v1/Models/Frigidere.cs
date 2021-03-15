using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CookingApp_v1.Models
{
    public class Frigidere
    {
        // id-ul la frigider nu va fi autoincrement deoarece odata ce este creat
        // un nou utilizator, vom crea si un frigider pentru el
        [PrimaryKey, AutoIncrement]
        public int F_id { get; set; }
        public int F_utilizator_id { get; set; }

        // daca nu merge cu 1:M, schimba in string si converteste la cautare
        [OneToMany]
        public List<Ingrediente> F_ingrediente { get; set; }
    }
}
