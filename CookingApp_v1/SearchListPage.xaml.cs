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
     * SEARCH.LIST.PAGE este o lista de ingrediente dintre care putem alege
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchListPage : ContentPage
    {
        public SearchListPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // pentru ca listView din .xaml sa stie care lista sa o afiseze, folosim functia din CookingDatabase

            // elementele de la listViewIngredient vor avea valorile primite din GetIngredientListAsync, metoda din CookingDatabase

            listViewIngredient.ItemsSource = await App.Database.GetIngredientListAsync();
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
            Utilizatori m_utilizator = (Utilizatori)BindingContext;
            // folosim functia pentru a selecta frigiderul care corespunde utilizatorului curent
            Frigidere m_frigider = await App.Database.GetFrigiderFromUtilizatorAsync(m_utilizator);

            //await DisplayAlert("Alerta:","nume: " + m_utilizator.U_nume,"okae");
            if (e.SelectedItem != null) // daca elementul Ingredient selectat nu este null
            {
                // preluam elementul ingredient selectat de pe view in m_ingredient
                Ingrediente m_ingredient = e.SelectedItem as Ingrediente;

                await App.Database.AddSaveFrigiderAsync(m_frigider,m_ingredient);

                // cream un element nou de tip Ingredient in care vom copia informatiile din m_ingredient
                /*var new_ingredient = new Ingrediente()
                {
                    N_id = m_ingredient.N_id,
                    N_nume = m_ingredient.N_nume,
                    N_categorie = m_ingredient.N_categorie,
                    N_subcategorie = m_ingredient.N_subcategorie,
                    N_descriere = m_ingredient.N_descriere,
                    N_link_imagine = m_ingredient.N_link_imagine
                };
                // 
                m_frigider.F_ingrediente = new List<Ingrediente> { m_ingredient };
                await App.Database.AddSaveFrigiderAsync(m_frigider);
                await Navigation.PopAsync();*/
                //await DisplayAlert("Alerta:","m_ID: " + m_ingredient.N_id + " n_ID: " + new_ingredient.N_id,"okae");
            }
        }
    }
}