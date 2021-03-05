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
     * RECIPES.PAGE ne va arata o lista cu retetele disponibile
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesPage : ContentPage
    {
        public RecipesPage()
        {
            InitializeComponent();
        }
        async void OnRecipeDetailButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul RecipeDetailPage pentru detalii despre o reteta
            // mai tarziu o vom schimba sa ne arate detailul in functie de reteta anume aleasa
                /*await Navigation.PushAsync(new ListPage
                    {
                        BindingContext = new World()
                    });
                */

            await Navigation.PushAsync(new RecipeDetailPage());
        }
        async void OnFridgeListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Fridge pentru Frigider

            await Navigation.PushAsync(new FridgeListPage());
        }
    }
}