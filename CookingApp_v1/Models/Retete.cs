using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CookingApp_v1.Models
{
    public class Retete
    {
        [PrimaryKey, AutoIncrement]
        public int R_id { get; set; }
        public string R_nume { get; set; }
        public string R_link { get; set; }
        public string R_cultura { get; set; }
        public string R_descriere { get; set; }


        // daca nu merge cu 1:M, schimba in string si converteste la cautare
        [OneToMany]
        public List<Ingrediente> R_ingrediente_carne { get; set; }
        [OneToMany]
        public List<Ingrediente> R_ingrediente_lactate { get; set; }
        [OneToMany]
        public List<Ingrediente> R_ingrediente_pastecereale { get; set; }
        [OneToMany]
        public List<Ingrediente> R_ingrediente_legume { get; set; }
        [OneToMany]
        public List<Ingrediente> R_ingrediente_sosuri { get; set; }
        [OneToMany]
        public List<Ingrediente> R_ingrediente_condimente { get; set; }
        [OneToMany]
        public List<Ingrediente> R_ingrediente_fructe { get; set; }
        [OneToMany]
        public List<Ingrediente> R_ingrediente_dulciuri { get; set; }
    }
}
