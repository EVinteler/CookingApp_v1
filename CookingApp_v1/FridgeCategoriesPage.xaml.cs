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
     * FRIDGE.CATEGORIES.PAGE ne va arata categoriile posibile, si daca apasam pe una ne deschide o noua
     *                        pagina FridgeListPage care ne arata doar ingredientele din categoria aleasa
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FridgeCategoriesPage : ContentPage
    {
        Utilizatori m_utilizator;
        Frigidere m_frigider;
        public FridgeCategoriesPage(Utilizatori utilizator, Frigidere frigider)
        {
            InitializeComponent();
            m_utilizator = utilizator;
            m_frigider = frigider;
        }
        async void OnRecipesButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Recipes care ne arat lista de retete disponibile

            var m_utilizator = (Utilizatori)BindingContext;
            await Navigation.PushAsync(new RecipesPage
            {
                // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
                BindingContext = m_utilizator
            });
        }
        async void OnFridgeListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul FridgeCategories care ne arata categoriile de ingrediente

            // cream o categorie nulla
            string m_categorie = null;
            await Navigation.PushAsync(new FridgeListPage(m_utilizator,m_frigider,m_categorie));
        }
        async void OnFridgeListItemButtonClicked(object sender, SelectedItemChangedEventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul FridgeList care va arata ingredientele sub forma de lista
            // mai tarziu o vom modifica astfel ca aceasta lista sa contina numai ingredientele din categoria aleasa

            // preluam categoria de la buton
            string m_categorie = null;

            await Navigation.PushAsync(new FridgeListPage(m_utilizator, m_frigider, m_categorie));
        }
    }
}