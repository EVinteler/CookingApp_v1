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
    public partial class LoginErrorPage : ContentPage
    {
        public LoginErrorPage()
        {
            InitializeComponent();
        }
        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // vom inchide pagina de eroare si ne trimite la pagina de register

            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new RegisterPage());
        }
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // vom inchide pagina de eroare

            await Navigation.PopModalAsync();
        }
    }
}