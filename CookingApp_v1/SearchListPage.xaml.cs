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
        async void OnIngredientAddItemSelected(object sender, EventArgs e)
        {
            await DisplayAlert("OnIngredientAddItemSelected", "Opened [OnIngredientAddItemSelected].", "Ok.");

            // vom avea un buton de adaugat
        }
    }
}