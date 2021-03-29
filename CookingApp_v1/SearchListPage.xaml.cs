using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingApp_v1.Models;
using System.Collections.ObjectModel;

namespace CookingApp_v1
{
    /*
     * SEARCH.LIST.PAGE este o lista de ingrediente dintre care putem alege
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchListPage : ContentPage
    {
        Utilizatori m_utilizator;
        Frigidere m_frigider;
        string m_categorie;
        public SearchListPage(Utilizatori utilizator, Frigidere frigider, string categorie)
        {
            InitializeComponent();
            m_utilizator = utilizator;
            m_frigider = frigider;
            m_categorie = categorie;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // initial, nu cautam nimic
            string m_search = null;
            ShowPageData(m_search);
        }
        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            string m_search = searchBar.Text;

            // daca stergem textul din search bar dorim ca search sa fie considerat null si sa ne arate iar tot
            if (searchBar.Text == "" || searchBar.Text == " ")
                m_search = null;

            ShowPageData(m_search);
        }
        protected async void ShowPageData(string m_search)
        {
            // pentru ca listView din .xaml sa stie care lista sa o afiseze, folosim functia din CookingDatabase
            // elementele de la listViewIngredient vor avea valorile primite din GetIngredientListAsync, metoda din CookingDatabase

            // vom trimite un string numit search care initial e null (adica nu se cauta nimic)
            // si un string numit categorie care initial e tot null (adica se cauta prin toata lista, nu doar cea din categoria resp)
            listViewIngrediente.ItemsSource = await App.Database.GetIngredientListAsync(m_search,m_categorie);
        }

        async void OnSearchCategoriesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul SearchCategories care ne lasa sa alegem categoria de ingredient cautat

            // facem Pop pentru a nu intra intr-un loop de Search Pages
            // nu facem await pt ca atunci nu continua cu Push
            Navigation.PopAsync();
            await Navigation.PushAsync(new SearchCategoriesPage());
        }

        async void OnIngredientAddItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //await DisplayAlert("OnIngredientAddItemSelected", "Opened [OnIngredientAddItemSelected].", "Ok.");

            // luam utilizatorul transmis prin binding context
            //Utilizatori m_utilizator = (Utilizatori)BindingContext;
            // folosim functia pentru a selecta frigiderul care corespunde utilizatorului curent
            //Frigidere m_frigider = await App.Database.GetFrigiderFromUtilizatorAsync(m_utilizator);

            //await DisplayAlert("Alerta:","u_nume: " + m_utilizator.U_nume,"okae");
            //await DisplayAlert("Alerta:","f_id: " + m_frigider.F_id,"okae");

            if (e.SelectedItem != null) // daca elementul Ingredient selectat nu este null
            {
                // preluam elementul ingredient selectat de pe view in m_ingredient
                Ingrediente ingredient = e.SelectedItem as Ingrediente;

                var m_ingredient = new Ingrediente()
                {
                    N_id = ingredient.N_id,
                    N_nume = ingredient.N_nume,
                    N_categorie = ingredient.N_categorie,
                    N_subcategorie = ingredient.N_subcategorie,
                    N_descriere = ingredient.N_descriere,
                    N_link_imagine = ingredient.N_link_imagine
                };

                // verificam daca mai exista ingredientul (cu id-ul respectiv) in lista frigiderului nostru
                // daca mai exista, nu il adaugam
                // daca nu mai exista, il adaugam
                bool exista = App.Database.CheckIngredientInFridgeAsync(m_frigider,m_ingredient.N_id);


                if (!exista)
                {
                    //await DisplayAlert("Alerta:", "nume: " + m_ingredient.N_nume, "okae");

                    // il transmitem functiei care il va copia intr-un nou element si il va adauga frigiderului
                    //await App.Database.AddIngredientFrigiderAsync(m_frigider,m_ingredient);

                    // merge daca cream o lista noua pe pagina asta, because fuck me thats why :)
                    //m_frigider.F_ingrediente = new List<Ingrediente> { m_ingredient };
                    //m_frigider.F_ingrediente.Add(m_ingredient);


                    //await DisplayAlert(">>>Alerta:", "before getFrig", "okae");
                    // vom crea o categorie nula
                    string categorie = null;
                    List<Ingrediente> m_lista_ingr_1 = App.Database.GetFrigiderIngredientListAsync(m_frigider, categorie);

                    //foreach (Ingrediente ing in m_lista_ingr_1)
                    //    System.Diagnostics.Debug.WriteLine(">>>ing: " + ing.N_nume);

                    //await DisplayAlert(">>>Alerta:", "before list 2", "okae");
                    List<Ingrediente> m_lista_ingr_2 = new List<Ingrediente> { m_ingredient };
                    //await DisplayAlert(">>>Alerta:", "before copy", "okae");
                    m_frigider.F_ingrediente = m_lista_ingr_1.Concat(m_lista_ingr_2).ToList();

                    //await DisplayAlert(">>>Alerta:", "added ingr?", "okae");

                    //foreach (Ingrediente ing in m_frigider.F_ingrediente)
                    //System.Diagnostics.Debug.WriteLine(">>>ing: " + ing.N_nume);

                    await App.Database.AddUpdateFrigiderAsync(m_frigider);
                    await Navigation.PopAsync();
                }
                else
                    await DisplayAlert("Opa!", "Mai exista ingredientul.", "No, bine.");
            }
        }
    }
}