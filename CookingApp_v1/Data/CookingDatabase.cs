using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Threading.Tasks; // pt async
using CookingApp_v1.Models;

namespace CookingApp_v1.Data
{
    public class CookingDatabase
    {
        // cream un element de tip conexiune la o baza de date
        readonly SQLiteAsyncConnection _database;
        public CookingDatabase(string dbPath)
        {
            // initializam _db cu un nou element conexiune la baza de date din path-ul precizat
            _database = new SQLiteAsyncConnection(dbPath);

            // cream tabelele pentru filtre, frigidere, ingrediente, retete si utilizatori
            _database.CreateTableAsync<Filtre>().Wait();
            _database.CreateTableAsync<Frigidere>().Wait();
            _database.CreateTableAsync<Ingrediente>().Wait();
            _database.CreateTableAsync<Retete>().Wait();
            _database.CreateTableAsync<Utilizatori>().Wait();
        }

        /*** UTILIZATORI ***/
        // functii pt utilizatori: add/save (register, pt a retine filtrele si frigiderele)
        // check pt nume, email si parola (register&login)
        // save (pentru a retine filtrele si id-ul frigiderului)
        /*** FRIGIDERE ***/
        // functii pt frigidere: save, update si delete (pt ingrediente); afisam toate ingredientele ca lista
        // afisam toate ingredientele dintr-o categorie

        public Task<List<Frigidere>> GetFrigiderCategorieListAsync(string categorie)
        {
            // returneaza o lista de obiecte Frigider din categoria trimisa
            
            //!toreview!
            // teoretic ai o lista de liste deci o sa ai un fel de double query (foloseste afisarea unei liste pe elemente de la Ingrediente v)

        }
        public Task<List<Frigidere>> GetFrigiderListAsync()
        {
            // returneaza o lista de obiecte Frigider
            return _database.Table<Frigidere>().ToListAsync();
        }






        /*** INGREDIENTE ***/
        // functii pt ingrediente: afisam toate ingredientele ca lista; afisam toate ingredientele dintr-o categorie
        // returnam un singur ingredient (pt a il adauga la un frigider)

        // Task returneaza un obiect async, in cazul nostru de tip Ingredient
        public Task<Ingrediente> GetIngredientAsync(int id)
        {
            // returneaza un obiect de tip Ingredient dupa id
            return _database.Table<Ingrediente>()
            .Where(i => i.N_id == id)
           .FirstOrDefaultAsync();
        }
        public Task<List<Ingrediente>> GetIngredientCategorieListAsync(string categorie)
        {
            // returneaza o lista de obiecte Ingredient din categoria trimisa
            return _database.Table<Ingrediente>()
            .Where(i => i.N_categorie == categorie)
            .ToListAsync();
        }
        public Task<List<Ingrediente>> GetIngredientListAsync()
        {
            // returneaza o lista de obiecte Ingredient
            return _database.Table<Ingrediente>().ToListAsync();
        }



        /*** RETETE ***/
        // functii pt retete: afisam toate retetele ca lista; (?) returnam o singura reteta

        // Task returneaza un obiect async, in cazul nostru de tip Reteta
        public Task<Retete> GetRetetaAsync(int id)
        {
            // returneaza un obiect de tip Reteta dupa id
            return _database.Table<Retete>()
            .Where(i => i.R_id == id)
           .FirstOrDefaultAsync();
        }
        public Task<List<Retete>> GetRetetaListAsync()
        {
            // returneaza o lista de obiecte Reteta
            return _database.Table<Retete>().ToListAsync();
        }


        /*** FILTRE ***/
        // functii pt filtre: afisam toate filtrele ca lista (vom schimba sa fie pe categorii dupa ce ma decid ce filtre sunt finale)
        // returnam un singur filtru (pt a il copia in tabelul utilizatori)

        // Task returneaza un obiect async, in cazul nostru de tip Filtru
        public Task<Filtre> GetFiltruAsync(int id)
        {
            // returneaza un obiect de tip Filtru dupa id
            return _database.Table<Filtre>()
            .Where(i => i.FT_id == id)
           .FirstOrDefaultAsync();
        }
        public Task<List<Filtre>> GetFiltruListAsync()
        {
            // returneaza o lista de obiecte Filtru
            return _database.Table<Filtre>().ToListAsync();
        }
    }
}
