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
            // am luat informatiile din transmise prin editoare din xaml convertite la
            // tipul unei inregistrari a tabelului Utilizatori si le-am pus in m_utilizator
            var m_utilizator = (Utilizatori)BindingContext;
            // apelam functia de inregistrare cu informatiile transmise
            App.Database.CheckRegisterAsync(m_utilizator);
            await DisplayAlert("Did the function","WO: ","ok??");
            
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