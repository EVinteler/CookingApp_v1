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
     * RECIPE.DETAIL.PAGE ne va arata detaliile retetei alese din pagina RecipesPage
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
        }
    }
}