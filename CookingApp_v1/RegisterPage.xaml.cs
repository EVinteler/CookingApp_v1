
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
     * REGISTER.PAGE este pagina de inregistrare
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Fridge pentru Frigider deoarece initial dupa reg/login ne va trimite la pagina cu ingredientele

            await Navigation.PushAsync(new FridgeListPage());
        }
        async void OnErrorButtonClicked(object sender, EventArgs e)
        {
            // pushMODALasync ne adauga o noua pagina de tip modal pe stack-ul de pagini de navigare
            // aceasta pagina ii spune utilizatorului ca a aparut o eroare

            await Navigation.PushModalAsync(new RegisterErrorPage());
        }
    }
}