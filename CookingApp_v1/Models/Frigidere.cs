using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CookingApp_v1.Models
{
    public class Frigidere
    {
        [PrimaryKey, AutoIncrement]
        public int F_id { get; set; }


        // daca nu merge cu 1:M, schimba in string si converteste la cautare
        [OneToMany]
        public List<Ingrediente> F_ingrediente_carne { get; set; }
        [OneToMany]
        public List<Ingrediente> F_ingrediente_lactate { get; set; }
        [OneToMany]
        public List<Ingrediente> F_ingrediente_pastecereale { get; set; }
        [OneToMany]
        public List<Ingrediente> F_ingrediente_legume { get; set; }
        [OneToMany]
        public List<Ingrediente> F_ingrediente_sosuri { get; set; }
        [OneToMany]
        public List<Ingrediente> F_ingrediente_condimente { get; set; }
        [OneToMany]
        public List<Ingrediente> F_ingrediente_fructe { get; set; }
        [OneToMany]
        public List<Ingrediente> F_ingrediente_dulciuri { get; set; }
    }
}
