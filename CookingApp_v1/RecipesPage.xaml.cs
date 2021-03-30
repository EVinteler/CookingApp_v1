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
        Utilizatori m_utilizator;
        Frigidere m_frigider;
        Filtre m_filtru;
        public RecipesPage(Utilizatori utilizator, Frigidere frigider)
        {
            InitializeComponent();
            m_utilizator = utilizator;
            m_frigider = frigider;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //afisam rezultatele retetelor
            //toate retetele: listViewReteteResults.ItemsSource = await App.Database.GetRetetaListAsync();

            listViewReteteResults.ItemsSource = await App.Database.GetReteteResultsListAsync(m_frigider);
        }
        async void OnFridgeButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // mergem la pagina generala pentru frigider, adica cea fara nicio categorie

            string m_categorie = null;
            await Navigation.PushAsync(new FridgeListPage(m_utilizator, m_frigider, m_categorie));
        }
        async void OnRecipeDetailButtonClicked(object sender, SelectedItemChangedEventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul RecipeDetailPage pentru detalii despre reteta transmisa

            Retete reteta = e.SelectedItem as Retete;

            await Navigation.PushAsync(new RecipeDetailPage
            {
                BindingContext = reteta
            });
        }
    }
}