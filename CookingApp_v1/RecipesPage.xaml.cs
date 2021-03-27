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

            Utilizatori m_utilizator = (Utilizatori)BindingContext;
            await Navigation.PushAsync(new RecipeDetailPage
            {
                // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
                BindingContext = m_utilizator
            });
        }
        void OnFridgeListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul FridgeCategories care ne arata categoriile de ingrediente

            Utilizatori m_utilizator = (Utilizatori)BindingContext;
            /*await Navigation.PushAsync(new FridgeListPage
            {
                // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
                BindingContext = m_utilizator
            });*/
        }
    }
}