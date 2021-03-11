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

            // citim informatiile din fisiere excel si le copiem in tabele
            // initial, frigidere si utilizatori sunt goale
            // avem informatii doar in filtre, ingrediente si retete
        }

        /*** UTILIZATORI ***/
        // functii pt utilizatori: add/save (register, pt a retine filtrele si frigiderele)
        // check pt nume, email si parola (register&login)
        // save (pentru a retine filtrele si id-ul frigiderului)


        public int CheckRegisterAsync(string nume_utilizator, string email, string parola)
        {
            // dorim sa nu mai existe numele de utilizator si email-ul
            // daca exista, returnam 0
            // daca nu exista, inseram parola in tabelul Utilizatori

            //!toreview!

            _database.Table<Utilizatori>()
                     .Where(i => i.U_nume == nume_utilizator)
                     .FirstAsync();
            _database.Table<Utilizatori>()
                     .Where(i => i.U_email == email)
                     .FirstAsync();

            if(false)
            {
                Utilizatori utilizator;
                utilizator.U_nume = nume_utilizator;
                utilizator.U_email = email;
                utilizator.U_parola = parola;
                _database.UpdateAsync(utilizator);
                // cream un nou frigider care sa ii corespunda utilizatorului
                Frigidere frigider_nou;
                frigider_nou.F_id = utilizator.U_frigider;
                frigider_nou.F_utilizator_id = utilizator.U_id;
                _database.InsertAsync(frigider_nou);
            }

            return 0;      
        }
        public int CheckLoginAsync(string nume_utilizator, string email, string parola)
        {

            //!toreview!

            // dorim sa returneze 1 daca s-a gasit utilizatorul cu parola corespunzatoare
            // 0 daca nu s-a gasit utilizatorul cu numele sau email-ul
            // si -1 daca nu corespunde parola cu numele de utilizator sau email-ul

            //!toreview!
            //Utilizatori utilizator;
            //utilizator=_database.Table<Utilizatori>()
            //         .Where(i => i.U_nume == nume_utilizator || i.U_email == email)
              //       .FirstAsync();

            if (false) //(utilizator is null)
                return 0;

            // verificarea pt parola

            return 1;
        }

        public Task<int> SaveUtilizatorAsync(Utilizatori utilizator)
         {
            // pentru updatare in cazul in care se adauga un filtru&frigider
            if (utilizator.U_id != 0)
            {
                return _database.UpdateAsync(utilizator);
            }
                // pentru adaugarea unui utilizator nou la register:
            else
            {
                return _database.InsertAsync(utilizator);
            }
         }

        /*** FRIGIDERE ***/
        // functii pt frigidere: save, update si delete (pt ingrediente); afisam toate ingredientele ca lista
        // afisam toate ingredientele dintr-o categorie

        /* //!toreview!
        public Task<int> SaveFrigiderAsync(???)
        {
            // va fi facut automat la adaugarea unui ingredient nou
            if (story.StoryID != 0)
                {
                    // in cazul in care exista un element Story cu id-ul resp, doar facem update la Story in loc sa cream unul nou
                    return _database.UpdateAsync(story);
                }
                else
                {
                    // in cazut in care nu mai exista element Story cu id-ul resp, il cream
                    return _database.InsertAsync(story);
                }
            }
        }
        public Task<int> DeleteIngredientFrigiderAsync(???)
        {
            // nu cred ca e un delete ci un update la lista ingredientului respectiv din frigider?
            return _database.DeleteAsync(???);
        }
        public Task<List<Frigidere>> GetFrigiderCategorieListAsync(string categorie)
        {
            // returneaza o lista de obiecte Frigider din categoria trimisa
          
            // teoretic ai o lista de liste deci o sa ai un fel de double query (foloseste afisarea unei liste pe elemente de la Ingrediente v)

        }*/
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
