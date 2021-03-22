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
        public FridgeListPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // pentru ca listView... din .xaml sa stie care lista sa o afiseze, folosim functia din CookingDatabase

            // elementele de la listViewIngredient vor avea valorile primite din GetFrigiderIngredientListAsync, metoda din CookingDatabase

            // luam utilizatorul transmis prin binding context de la pagina de Login
            // si il transmitem la functia care ne afiseaza ingredientele din frigiderul utilizatorului nostru

            var m_utilizator = (Utilizatori)BindingContext;
            //await DisplayAlert("Alerta:","ID:" + m_utilizator.U_nume,"ok??");
            App.Database.GetFrigiderIngredientListAsync(m_utilizator);
            //listViewIngredient.ItemsSource = await App.Database.GetFrigiderIngredientListAsync(m_utilizator);
        }
        async void OnRecipesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Recipes care ne arat lista de retete disponibile

            await Navigation.PushAsync(new RecipesPage());
        }
        async void OnFridgeCategoriesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul FridgeCategories care ne arata categoriile de ingrediente

            await Navigation.PushAsync(new FridgeCategoriesPage());
        }
        async void OnSearchListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul SearchList care ne arata o lista de ingrediente
            // initial, ne trimite spre o lista generala de ingrediente

            await Navigation.PushAsync(new SearchListPage());
        }
        /*async void OnIngredientDeleteItemSelected(object sender, EventArgs e)
        {
            await DisplayAlert("OnIngredientDeleteItemSelected", "Opened [OnIngredientDeleteItemSelected].", "Ok.");

            // vom avea un buton de adaugat
        }*/
    }
}