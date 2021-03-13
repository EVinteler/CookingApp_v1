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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }
        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Register pentru Inregistrare

            //await DisplayAlert("OnRegisterButtonClicked", "Opened [OnRegisterButtonClicked].", "Ok.");
            await Navigation.PushAsync(new RegisterPage
            {
                BindingContext = new Utilizatori()
            });
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul Login pentru Autentificare

            //await DisplayAlert("OnLoginButtonClicked", "Opened [OnLoginButtonClicked].", "Ok.");
            await Navigation.PushAsync(new LoginPage());
        }
    }
}