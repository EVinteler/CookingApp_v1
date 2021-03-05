using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp_v1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FridgeRecipesTabbedPage : TabbedPage
    {
        public FridgeRecipesTabbedPage()
        {
            InitializeComponent();
            this.Children.Add(new FridgeListPage() { Title = "Fridge List" });
            this.Children.Add(new RecipesPage() { Title = "Recipes" });
        }
    }
}