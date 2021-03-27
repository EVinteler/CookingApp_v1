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
        public FridgeListPage(Utilizatori utilizator, Frigidere frigider)
        {
            InitializeComponent();
            m_utilizator = utilizator;
            m_frigider = frigider;
        }
        protected override async void OnAppearing()
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

            listViewIngredient.ItemsSource = App.Database.GetFrigiderIngredientListAsync(m_frigider);

            /*
            // convertim lista la collection pentru ca sa se updatateze automat cand o modificam
            List<Ingrediente> l_ingrediente = await App.Database.GetIngredientListAsync();
            ObservableCollection<Ingrediente> c_ingrediente = new ObservableCollection<Ingrediente>(l_ingrediente);
            */

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

            await Navigation.PushAsync(new FridgeCategoriesPage
            {
                // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
                BindingContext = m_utilizator
            });
        }
        async void OnSearchListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul SearchList care ne arata o lista de ingrediente
            // initial, ne trimite spre o lista generala de ingrediente

            // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
            await Navigation.PushAsync(new SearchListPage(m_utilizator,m_frigider));
        }
        async void OnIngredientDeleteItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await DisplayAlert("OnIngredientDeleteItemSelected", "Opened [OnIngredientDeleteItemSelected].", "Ok.");

            // vom avea un buton de adaugat
        }
    }
}