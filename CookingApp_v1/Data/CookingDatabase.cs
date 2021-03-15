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
            
            /*_database.DropTableAsync<Filtre>().Wait();
            _database.DropTableAsync<Frigidere>().Wait();
            _database.DropTableAsync<Ingrediente>().Wait();
            _database.DropTableAsync<Retete>().Wait();
            _database.DropTableAsync<Utilizatori>().Wait();*/

            // citim informatiile din fisiere excel si le copiem in tabele
            // initial, frigidere si utilizatori sunt goale
            // avem informatii doar in filtre, ingrediente si retete
        }

        /*** UTILIZATORI ***/
        // functii pt utilizatori: add/save (register, pt a retine filtrele si frigiderele)
        // check pt nume, email si parola (register&login)
        // save (pentru a retine filtrele si id-ul frigiderului)


        public Task<List<Utilizatori>> GetUtilizatoriListAsync()
        {
            // returneaza o lista de obiecte Frigider
            return _database.Table<Utilizatori>().ToListAsync();
        }

        //functia ne va returna un element de tip Task<int>
        public async Task<int> CheckRegisterAsync(Utilizatori utilizator, Frigidere frigider)
        {
            // dorim sa nu mai existe numele de utilizator si email-ul
            // daca exista, returnam 0
            // daca nu exista, inseram parola in tabelul Utilizatori si returnam 1
            // daca apare o eroare, returnam -1

            //!toreview!

            // vom folosi un try catch
            // in cazul in care exista erori, sa nu intram in modul de break
            // astfel vom transmite urmatoarele informatii prin try catch:
                // pe partea de try: returneaza 1 daca inregistrarea a avut succes
                // returneaza 0 daca a fost gasit macar un utilizator cu numele sau emailul respectiv
                // pe partea de catch: in cazul unei erori nespecificate, dorim ca tot sa nu se
                // poata face inregistrarea
            try
            {
                // functia SQL va selecta toti utilizatorii care au numele sau email-ul
                // trimis prin elementul la care i-am facut bind (utilizator)
                // apelam functia care ne face comanda si ne returneaza o lista cu utilizatorii (<Utilizatori>
                // va iesi ca List<Utilizatori>) care indeplinesc
                // care o vom salva in result
                var result = await _database.QueryAsync<Utilizatori>("select * from Utilizatori where U_nume=? OR U_email=?",utilizator.U_nume,utilizator.U_email);

                // result.Count va numara elementele din lista cu utilizatorii
                var counter = (result==null)?0:result.Count;
                // daca nu mai exista persoana cu numele sau email-ul dat atunci o inseram
                // in tabel si returnam 1, care ne va lasa sa trecem pe pagina urmatoare
                if (counter == 0)
                {
                    //System.Diagnostics.Debug.WriteLine("Output for counter == 0");

                    // adaugam un utilizator nou
                    // criptam parola
                    //string c_parola = fct_cript(utilizator.U_parola);
                    //utilizator.U_parola = c_parola;

                    await _database.InsertAsync(utilizator);

                    // cream un nou frigider care sa ii corespunda utilizatorului
                    frigider.F_utilizator_id = utilizator.U_id;
                    await _database.InsertAsync(frigider);

                    return 1;
                }
                // daca mai exista, atunci returnam 0, care ne va da un mesaj de eroare
                // specific pe pagina register
                else
                {
                    //System.Diagnostics.Debug.WriteLine("Output for counter != 0");
                    return 0;
                }
            }
            catch (Exception e)
            {
                // daca apare o eroare returnam -1, care ne va da un mesaj de eroare
                // specific pe pagina register
                System.Diagnostics.Debug.WriteLine("No results whoopsie!" + e);
                return -1;
            }
        }


        public async Task<int> CheckLoginAsync(Utilizatori utilizator)
        {
            // functia va returna 1 daca s-a gasit utilizatorul cu parola corespunzatoare
            // va returna 0 daca nu corespunde parola cu numele dat
            // va returna -1 daca nu s-a gasit utilizatorul cu numele dat
            // va returna -2 in caz de eroare generala
            
            try
            {
                // functia SQL va selecta utilizatorul care are numele
                // trimis prin elementul la care i-am facut bind (utilizator)
                // apelam functia care ne face comanda si ne returneaza un element
                // de tip Utilizator
                var result_r = await _database.Table<Utilizatori>()
                                   .Where(i => i.U_nume == utilizator.U_nume)
                                   .FirstAsync();

                // daca exista persoana cu numele dat atunci
                // vom trece la verificarea parolei
                if (result_r != null)
                {
                    // functia SQL va selecta utilizatorul cu numele si parola trimise
                    var result_p = _database.Table<Utilizatori>()
                                            .Where(i => i.U_nume == utilizator.U_nume && i.U_parola == utilizator.U_parola)
                                            .FirstAsync();
                    // pentru criptare:
                    // c_parola = fct_cript(utilizator.U_parola)
                    //.Where(i => i.U_nume == utilizator.U_nume && i.U_parola == c_parola)


                    // daca exista utilizator cu numele si parola trimise vom return 1 (succes)
                    if (result_p != null)
                        return 1;
                    // altfel vom returna -1, care ne va da un mesaj de eroare
                    // specific pe pagina login
                    else
                        return -1;
                }
                // daca nu exista, atunci returnam 0, care ne va da un mesaj de eroare
                // specific pe pagina login
                else
                {
                    //System.Diagnostics.Debug.WriteLine("Output for counter != 0");
                    return 0;
                }
            }
            catch (Exception e)
            {
                // daca apare o eroare returnam -2, care ne va da un mesaj de eroare
                // specific pe pagina logare
                System.Diagnostics.Debug.WriteLine("No results whoopsie!" + e);
                return -2;
            }
        }

        /*
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

        }
        public Task<List<Frigidere>> GetFrigiderListAsync()
        {
            // returneaza o lista de obiecte Frigider
            return _database.Table<Frigidere>().ToListAsync();
        }



        /*** INGREDIENTE 
        // functii pt ingrediente: afisam toate ingredientele ca lista; afisam toate ingredientele dintr-o categorie
        // returnam un singur ingredient (pt a il adauga la un frigider)
        // cautare dupa nume, categorie si/sau subcategorie cu LIKE


        // Task returneaza un obiect async, in cazul nostru de tip Ingredient
        public Task<Ingrediente> SearchIngredientAsync(string nume_inserat)
        {
            // !toreview!
            // returneaza un obiect de tip Ingredient dupa nume, categorie, subcategorie sau descriere

            // substring comparisons nu ==
            return _database.Table<Ingrediente>()
            .Where(i => i.N_nume == nume_inserat || i.N_categorie == nume_inserat || i.N_subcategorie == nume_inserat ||
            i.N_descriere == nume_inserat) // !toreview! nume inserat e un string cu mai multe cuvinte, trebuie delimitat dupa caracterul space
           .FirstOrDefaultAsync();
        }
        public Task<Ingrediente> SearchIngredientCategorieAsync(string categorie, string nume_inserat)
        {
            // !toreview!
            // returneaza un obiect de tip Ingredient dupa nume/etc, dar doar din categoria dorita
        
            // substring comparisons nu ==
            return _database.Table<Ingrediente>()
            .Where(i => i.N_categorie == categorie && ( i.N_nume == nume_inserat || i.N_categorie == nume_inserat 
            || i.N_subcategorie == nume_inserat || i.N_descriere == nume_inserat)) 
            // !toreview! nume inserat e un string cu mai multe cuvinte, trebuie delimitat dupa caracterul space
           .FirstOrDefaultAsync();
        }
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



        /*** RETETE 
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


        /*** FILTRE 
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
        */
    }
}
