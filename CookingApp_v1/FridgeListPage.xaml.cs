using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp_v1
{
    /*
     * FRIDGE.LIST.PAGE ne va arata ingredientele utilizatorului sub forma de lista
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FridgeListPage : ContentPage
    {
        public FridgeListPage()
        {
            InitializeComponent();
        }
        async void OnRecipesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Recipes care ne arata lista de retete

            await Navigation.PushAsync(new RecipesPage());
        }
        async void OnFridgeCategoriesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul FridgeCategories care ne arata categoriile de ingrediente

            await Navigation.PushAsync(new RecipesPage());
        }
        
    }
}