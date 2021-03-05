using System;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp_v1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FridgeRecipesTabbedPage : Xamarin.Forms.TabbedPage
    {
        public FridgeRecipesTabbedPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom); // vroiam sa fie jos la android :(

            Children.Add(new FridgeListPage() { Title = "Fridge List" });
            Children.Add(new RecipesPage() { Title = "Recipes" });
        }
    }
}