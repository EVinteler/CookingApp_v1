using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingApp_v1.Models;

namespace CookingApp_v1
{
    /*
     * FRIDGE.LIST.PAGE ne va arata ingredientele utilizatorului sub forma de lista
     * FUNCTII:
     *      - scrie o lista cu functiile continute si ce face fiecare
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FridgeListPage : ContentPage
    {
        Utilizatori m_utilizator;
        Frigidere m_frigider;
        string m_categorie;
        public FridgeListPage(Utilizatori utilizator, Frigidere frigider, string categorie)
        {
            InitializeComponent();
            m_utilizator = utilizator;
            m_frigider = frigider;
            m_categorie = categorie;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //m_frigider = await App.Database.GetFrigiderFromUtilizatorAsync(m_utilizator);
            //await DisplayAlert("ONAPP FRIDGE","m_frigider id: " + m_frigider.F_id,"oke.");
            showPageData(m_frigider);
        }
        void showPageData (Frigidere m_frigider)
        {
            // pentru ca listView... din .xaml sa stie care lista sa o afiseze, folosim functia din CookingDatabase

            // elementele de la listViewIngredient vor avea valorile primite din GetFrigiderIngredientListAsync, metoda din CookingDatabase

            // luam utilizatorul transmis prin binding context de la pagina de Login
            // folosim functia getFrigiderFromUtilizator sa selectam frigiderul care corespunde utilizatorului curent
            // si il transmitem la functia care ne afiseaza ingredientele din frigider
            
            // daca m_categorie e null, va scoate toate ingredientele
            // daca m_categorie este numele unei categorii, va scoate doar categoria respectiva
            listViewIngredient.ItemsSource = App.Database.GetFrigiderIngredientListAsync(m_frigider,m_categorie);

            //await DisplayAlert("Alerta:","ID:" + m_utilizator.U_nume,"ok??");
            //App.Database.GetFrigiderIngredientListAsync(m_utilizator);
        }
        async void OnRecipesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Recipes care ne arat lista de retete disponibile

            await Navigation.PushAsync(new RecipesPage
            {
                // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
                BindingContext = m_utilizator
            });
        }
        async void OnFridgeCategoriesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul FridgeCategories care ne arata categoriile de ingrediente

            await Navigation.PushAsync(new FridgeCategoriesPage(m_utilizator,m_frigider));
        }
        async void OnSearchListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul SearchList care ne arata o lista de ingrediente
            // initial, ne trimite spre o lista generala de ingrediente

            // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
            // initial, nu aveam nimic la categorie, deci am trimis-o ca null DAR
            // acum vom face astfel incat daca suntem pe pagina numai unei categorii de ingrediente
            // cand mergem sa adaugam mai multe ingrediente vom cauta automat doar printre cele din categoria aceasta
            //string m_categorie = null;
            await Navigation.PushAsync(new SearchListPage(m_utilizator,m_frigider,m_categorie));
        }
        async void OnIngredientDeleteItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //await DisplayAlert("OnIngredientDeleteItemSelected", "Opened [OnIngredientDeleteItemSelected].", "Ok.");

            // sterge elementul selectat din FRIGIDERul virtual, nu din toata lista de ingrediente

            // cautam elementul in lista si facem o lista noua fara el
            // pe care apoi o copiem in lista initiala si updatam this.frigiderul in baza de date
            // am incercat numai sa sterg elementul dar da eroare lol, nu cred ca pot "lucra"
            // pe lista in sine, si doar sa ii atribui la final o noua valoare
            // pt ca aveam erori similare si la adaugarea unui nou element, si nu am mai avut
            // erori doar in momentul in care am facut tot asa (pagina SearchListPage)

            Ingrediente ingredient = e.SelectedItem as Ingrediente;
            List<Ingrediente> n_frigider_ingrediente = new List<Ingrediente> { };

            foreach (Ingrediente m_ingredient in m_frigider.F_ingrediente)
                if (m_ingredient.N_id != ingredient.N_id)
                {
                    n_frigider_ingrediente.Add(m_ingredient);
                }

            m_frigider.F_ingrediente = n_frigider_ingrediente;

            await App.Database.AddUpdateFrigiderAsync(m_frigider);


            // cream o pagina noua si o adaugam inainte de aceasta, si apoi ii facem pop la pagina curenta
            // pentru a o updata si sa dispara ingredientul sters din frigider
            var n_FridgeListPage = new FridgeListPage(m_utilizator,m_frigider,m_categorie); 
            Navigation.InsertPageBefore(n_FridgeListPage, this);
            Navigation.PopAsync();


            /*
             m_frigider = App.Database.DeleteIngredientFromFridgeAsync(m_frigider, m_ingredient);
             await App.Database.AddUpdateFrigiderAsync(m_frigider);
             */
        }
    }
}