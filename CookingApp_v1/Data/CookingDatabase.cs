using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Threading.Tasks; // pt async
using CookingApp_v1.Models;
using System.Linq;
using System.Collections.ObjectModel;

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

            /*_database.DropTableAsync<Filtre>().Wait();
            _database.DropTableAsync<Ingrediente>().Wait();
            _database.DropTableAsync<Retete>().Wait();
            _database.DropTableAsync<Utilizatori>().Wait();
            _database.DropTableAsync<Frigidere>().Wait();*/

            //_database.ExecuteAsync("DELETE FROM Ingrediente");

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


        public Task<List<Utilizatori>> GetUtilizatoriListAsync()
        {
            // returneaza o lista de obiecte Frigider
            return _database.Table<Utilizatori>().ToListAsync();
        }

        public async Task<int> SaveUtilizatorAsync(Utilizatori utilizator)
         {
            // pentru updatare in cazul in care se adauga un filtru&frigider
            if (utilizator.U_id != 0)
            {
                return await _database.UpdateAsync(utilizator);
            }
            else
                return 0;
         }

        //functia ne va returna un element de tip Task<int>
        public async Task<int> CheckRegisterAsync(Utilizatori utilizator)
        {

            //!toreview! - utilizatorul se poate inregistra cu nume/email null

            /*//await _database.ExecuteAsync("DELETE FROM Utilizatori");*/
            /*//await _database.ExecuteAsync("DELETE FROM Frigidere");*/

            // dorim sa nu mai existe numele de utilizator si email-ul
            // daca exista, returnam 0
            // daca nu exista, inseram parola in tabelul Utilizatori si returnam 1
            // daca apare o eroare, returnam -1
            // daca utilizatorul nu a introdus nimic, returnam -2


            // vom folosi un try catch
            // in cazul in care exista erori, sa nu intram in modul de break
            // astfel vom transmite urmatoarele informatii prin try catch:
            // pe partea de try: returneaza 1 daca inregistrarea a avut succes
            // returneaza 0 daca a fost gasit macar un utilizator cu numele sau emailul respectiv
            // pe partea de catch: in cazul unei erori nespecificate, dorim ca tot sa nu se
            // poata face inregistrarea
            try
            {
                // daca nu a fost introdus nimic, afisam un mesaj de eroare specific pe
                // pagina de register
                if (utilizator.U_nume == null || utilizator.U_email == null || utilizator.U_parola == null)
                    return -2;

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


        /*** FRIGIDERE ***/
        // functii pt frigidere: save, update si delete (pt ingrediente); afisam toate ingredientele ca lista
        // afisam toate ingredientele dintr-o categorie

        //!toreview!
        public Task<int> AddUpdateFrigiderAsync(Frigidere frigider)
        {
            if (frigider.F_id != 0)
            {
                return _database.UpdateAsync(frigider);
            }
            else
            {/*
                var m_ingredient = new Ingrediente()
                {
                    N_id = 0,
                    N_nume = "",
                    N_categorie = "",
                    N_subcategorie = "",
                    N_descriere = "",
                    N_link_imagine = ""
                };

                // inseram ingredientul in lista din frigider
                frigider.F_ingrediente = new List<Ingrediente> { m_ingredient };*/
                /*foreach (Ingrediente ing in frigider.F_ingrediente)
                    System.Diagnostics.Debug.WriteLine(">>>FCTing: " + ing.N_nume);

                _database.InsertAsync(frigider);

                foreach (Ingrediente ing in frigider.F_ingrediente)
                    System.Diagnostics.Debug.WriteLine(">>>2FCTing: " + ing.N_nume);*/

                return _database.InsertAsync(frigider);
            }
        }
        public Task<int> AddIngredientFrigiderAsync(Frigidere frigider, Ingrediente ingredient)
        {
            System.Diagnostics.Debug.WriteLine(">>>Informatii din ingredient: " + ingredient.N_nume);
            System.Diagnostics.Debug.WriteLine(">>>Informatii din Frigider: " + frigider.F_id);

            // cream un nou m_ingredient cu informatiile preluate de la ingredientul transmis
            /*var m_ingredient = new Ingrediente()
            {
                N_id = ingredient.N_id,
                N_nume = ingredient.N_nume,
                N_categorie = ingredient.N_categorie,
                N_subcategorie = ingredient.N_subcategorie,
                N_descriere = ingredient.N_descriere,
                N_link_imagine = ingredient.N_link_imagine
            };
            var m_ingredient = new Ingrediente()
            {
                N_id = 1,
                N_nume = "ingredient",
                N_categorie = "ingredient",
                N_subcategorie = "ingredient",
                N_descriere = "ingredient",
                N_link_imagine = "ingredient"
            };*/

            System.Diagnostics.Debug.WriteLine(">>>Informatii din M_ingredient: " + ingredient.N_nume);

            // inseram ingredientul in lista din frigider
            // NU, aici doar cream o lista noua lol: frigider.F_ingrediente = new List<Ingrediente> { m_ingredient };
            frigider.F_ingrediente.Add(ingredient);

            //foreach (Ingrediente ing in frigider.F_ingrediente)
                //System.Diagnostics.Debug.WriteLine(">>>ing: " + ing.N_nume);

            // updatam frigiderul
            return _database.UpdateAsync(frigider);
        }
        /*
        public Task<int> DeleteIngredientFrigiderAsync(???)
        {
            // nu cred ca e un delete ci un update la lista ingredientului respectiv din frigider?
        //!toreview!
            return _database.DeleteAsync(???);
        }*/
        public async Task<Frigidere> GetFrigiderFromUtilizatorAsync(Utilizatori utilizator)
        {
            //System.Diagnostics.Debug.WriteLine("AAAAAAA NUME:" + utilizator.U_nume);

            // binding context nu ne transmite si u_frigider, el e in tabel
            // asa ca intai va trebui sa gasim frigiderul utilizatorului cu numele transmis

            // QueryAsync ne va returna toate elementele Utilizatori care indeplinesc conditia, insa deoarece
            // numele este unic, stim ca exista doar un rezultat, asa ca il putem converti
            // cu .FirstOrDefault()

            //System.Diagnostics.Debug.WriteLine(">>>0000PAS1: ");

            var m_utilizatori = await _database.QueryAsync<Utilizatori>
                ("select * from Utilizatori where U_nume = '" + utilizator.U_nume + "'");

            //System.Diagnostics.Debug.WriteLine(">>>0000PAS2: ");

            var m_utilizator = m_utilizatori.First();

            //System.Diagnostics.Debug.WriteLine(">>>0000PAS3: ");


            // vom lua frigiderul cu id-ul corespunzator utilizatorului trimis (folosim firstordefault deoarece
            // id-urile sunt unice)
            var m_frigidere = await _database.QueryAsync<Frigidere>
               ("select * from Frigidere where F_id = " + m_utilizator.U_frigider);

            //System.Diagnostics.Debug.WriteLine(">>>0000PAS4: ");

            var m_frigider = m_frigidere.First();

            //System.Diagnostics.Debug.WriteLine(">>>0000PAS5: " + m_frigider.F_id);

            // cream lista inainte de a o afisa pt ca altfel nu merge, lmao
            m_frigider.F_ingrediente = new List<Ingrediente>{};


            //foreach (Ingrediente ing in m_frigider.F_ingrediente)
                //System.Diagnostics.Debug.WriteLine(">>>LOGing: " + ing.N_nume);

            return m_frigider;
        }

        public List<Ingrediente> GetFrigiderIngredientListAsync(Frigidere frigider, string categorie)
        {
            // returneaza o lista de obiecte Ingredient
            // daca string e null, atunci returnam toate ingredientele
            // daca string nu e null, returnam doar cele din categoria corespunzatoare

            /*
            var m_ingredient = new Ingrediente()
            {
                N_id = 1,
                N_nume = "ingredient",
                N_categorie = "ingredient",
                N_subcategorie = "ingredient",
                N_descriere = "ingredient",
                N_link_imagine = "ingredient"
            };

            // inseram ingredientul in lista din frigider
            frigider.F_ingrediente = new List<Ingrediente> { m_ingredient };

            // updatam frigiderul
            _database.UpdateAsync(frigider);*/

            // apoi vom returna lista de ingrediente salvata in frigider
            /*
            var m_ingrediente = frigider.F_ingrediente;
            */
            /*if (frigider.F_ingrediente != null)
            {
                foreach (Ingrediente ing in frigider.F_ingrediente)
                    System.Diagnostics.Debug.WriteLine(">>>GETing: " + ing.N_nume);
            }*/

            // daca nu avem categorie, returnam toate ingredientele
            if (categorie == null)
                return frigider.F_ingrediente;
            else
            {
                // daca avem categorie, returnam doar ingredientele care apartin categoriei respective
                // cream o lista noua
                var ingredienete_cat = new List<Ingrediente> { };

                // verificam daca fiecare ingredient are categoria dorita
                foreach (Ingrediente ing in frigider.F_ingrediente)
                    if (ing.N_categorie == categorie)
                    {
                        // daca da, atunci il adaugam in lista dinainte
                        ingredienete_cat.Add(ing);
                        //System.Diagnostics.Debug.WriteLine(">>>GETing: " + ing.N_nume);
                    }

                // returnam lista
                return ingredienete_cat;
            }
        }

        public bool CheckIngredientInFridgeAsync(Frigidere frigider, int id)
        {
            // returneaza true daca mai exista ingredientul in frigider
            // si false daca nu mai exista

            int numar_ingrediente = 0;

            foreach (Ingrediente ing in frigider.F_ingrediente)
                if (ing.N_id == id)
                    numar_ingrediente++;

            if (numar_ingrediente == 0)
                return false;
            else
                return true;
        }

        /*** INGREDIENTE 
        // functii pt ingrediente: afisam toate ingredientele ca lista; afisam toate ingredientele dintr-o categorie
        // returnam un singur ingredient (pt a il adauga la un frigider)
        // cautare dupa nume, categorie si/sau subcategorie cu LIKE

        
        public async Task<int> AddIngredientAsync (Ingrediente ingredient)
        {
            return await _database.InsertAsync(ingredient);
        }
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
        }*/

        public async Task<List<Ingrediente>> GetIngredientListAsync(string search, string categorie)
        {
            // returneaza o lista de obiecte Ingredient
            // daca categorie e null, returnam toata lista
            // daca categorie nu e null, returnam doar ingredientele din categoria respectiva
            // separat, daca search e null, returnam toata lista (cu conditiile de la categorie, daca exista)
            // daca search nu e null, returnam doar ingredientele cu numele, subcategoria sau categoria din search

            // intai copiem tabelul bazei de date intr-o lista, si nu o modificam pt ca ne da erori daca facem asta, lol

            List<Ingrediente> lista_ingrediente = new List<Ingrediente> { };
            
            // daca categoria e nula, vom selecta ingredientele ce le dorim din toata lista
            // daca nu e nula, vom selecta din tabel doar ingredientele cu categoria trimisa
            // si vom prelucra mai apoi search-ul pe lista rezultata de aici
            if (categorie == null)
                lista_ingrediente = await _database.Table<Ingrediente>().ToListAsync();
            else
                lista_ingrediente = await _database.Table<Ingrediente>()
                                    .Where(i => i.N_categorie == categorie)
                                    .ToListAsync();

            List<Ingrediente> ingrediente_rezultate = new List<Ingrediente> { };

            try
            {
                if (search == null && categorie != null)
                {
                        ingrediente_rezultate = lista_ingrediente;
                }
                else if (search != null)
                {
                    //https://stackoverflow.com/questions/36526414/how-to-check-if-a-string-contains-some-part-of-another-string-in-c

                    // in functia de cautare vom verifica daca numele, subcategoria sau orice cuvant din
                    // descrierea ingredientului au macar 3 litere in comun cu ce a cautat utilizatorul
                    foreach (Ingrediente ingredient in lista_ingrediente)
                    {
                        bool cond_1 = ingredient.N_nume.ToCharArray().Intersect(search.ToCharArray()).ToList().Count() >= 4;
                        // uneori subcategorie poate sa fie null, deci pentru a nu avea erori, daca este null consideram ca
                        // conditia este falsa
                        bool cond_2 = (ingredient.N_subcategorie == null) ? (false) : (ingredient.N_subcategorie.ToCharArray().Intersect(search.ToCharArray()).ToList().Count() >= 4);

                        // vom prelua lista din N_descriere care contine alte metode de scriere a cuvantului cat si typos
                        // si o vom sparge intr-un array de string-uri (dupa spatiu) pe care apoi le vom verifica pe rand
                        // sa vedem daca cuvantul cautat are macar 3 litere comune cu vreunul dintre string-uri

                        // daca descrierea e nula, vom considera conditia falsa
                        string phrase = ingredient.N_descriere;
                        bool cond_3 = false;

                        if (phrase != null)
                        {
                            string[] words = phrase.Split(' ');

                            foreach (string word in words)
                                if (word.ToCharArray().Intersect(search.ToCharArray()).ToList().Count() >= 4)
                                {
                                    cond_3 = true;
                                    break;
                                    // daca gasim macar un cuvant care corespunde conditiei, atunci putem iesi din for
                                }
                        }

                        //System.Diagnostics.Debug.WriteLine(">>>>>>>>>>" + cond_1 + " " + cond_2 +" " + cond_3 + " = " + (cond_1 || cond_2 || cond_3));
                        
                        // daca oricare din aceste conditii e indeplinita, adaugam la lista ingredientelor rezultate
                        if (cond_1 || cond_2 || cond_3)
                        {
                            ingrediente_rezultate.Add(ingredient);
                        }
                    }
                }
                
                return ingrediente_rezultate;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(">>>Exception: " + e);
                // daca ne apare o eroare, returnam o lista nula pentru ca sa nu ne dea crash aplicatia
                //ingrediente_rezultate = new List<Ingrediente> { };
                return new List<Ingrediente> { };
            }
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

        public List<Ingrediente> GetRetetaIngredientListAsync(Retete reteta)
        {
            // returneaza o lista de obiecte Ingredient

            //foreach (Ingrediente i in reteta.R_ingrediente)
            //System.Diagnostics.Debug.WriteLine(">>>1INGLISTing: " + i.N_nume);

            return reteta.R_ingrediente;
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

        /* temp: INSERARE TEMPORARA (manuala) */
        // inseram in tabelul Filtre, Ingrediente si Retete

        public Task<int> tempAddFiltreAsync(Filtre filtru)
        {
            return _database.InsertAsync(filtru);
        }
        public Task<int> tempAddUpdateReteteAsync(Retete reteta)
        {
            if (reteta.R_id != 0)
                return _database.UpdateAsync(reteta);
            else
                return _database.InsertAsync(reteta);
        }
        public Retete tempGetNewReteta(Retete reteta)
        {
            reteta.R_ingrediente = new List<Ingrediente> { 
                new Ingrediente{ N_id=5, N_nume="aaa"}
            };

            foreach (Ingrediente i in reteta.R_ingrediente)
                System.Diagnostics.Debug.WriteLine(">>>TGNRing: " + i.N_nume);

            return reteta;
        }
        public Task<int> tempAddIngredienteAsync(Ingrediente ingredient)
        {
            return _database.InsertAsync(ingredient);
        }
        public Task<List<Frigidere>> GetFrigiderListAsync()
        {
            return _database.Table<Frigidere>().ToListAsync();
        }
    }
}
