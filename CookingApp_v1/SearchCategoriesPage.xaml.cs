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
     * SEARCH.CATEGORIES.PAGE este o pagina cu mai multe categorii de ingrediente dintre care putem alege
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchCategoriesPage : ContentPage
    {
        public SearchCategoriesPage()
        {
            InitializeComponent();
        }
        async void OnSearchListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul SearchList care cauta 
            // mai tarziu o vom schimba sa ne caute ingrediente doar  din categoria noastra
            /*await Navigation.PushAsync(new ListPage
                {
                    BindingContext = new World()
                });
            */

            await Navigation.PopAsync();
            await Navigation.PushAsync(new SearchListPage());
        }
    }
}