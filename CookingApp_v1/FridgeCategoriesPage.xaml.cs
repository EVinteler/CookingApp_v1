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
     * FRIDGE.CATEGORIES.PAGE ne va arata ingredientele utilizatorului sub forma de lista
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FridgeCategoriesPage : ContentPage
    {
        public FridgeCategoriesPage()
        {
            InitializeComponent();
        }
        async void OnFridgeListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul FridgeList care va arata ingredientele sub forma de lista
            // mai tarziu o vom modifica astfel ca aceasta lista sa contina numai ingredientele din categoria aleasa
                    /*await Navigation.PushAsync(new ListPage
                        {
                            BindingContext = new World()
                        });
                    */

            await Navigation.PushAsync(new FridgeListPage());
        }
    }
}