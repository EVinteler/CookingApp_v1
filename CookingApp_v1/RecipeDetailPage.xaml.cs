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
     * RECIPE.DETAIL.PAGE ne va arata detaliile retetei alese din pagina RecipesPage
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
        }
        protected override /*async*/ void OnAppearing()
        {
            base.OnAppearing();

            //afisam ingredientele retetei
            Retete m_reteta = (Retete)BindingContext;
            //listViewRetetaIngredient.ItemsSource = await App.Database.GetRetetaIngredientVar2ListAsync(m_reteta);
        }
    }
}