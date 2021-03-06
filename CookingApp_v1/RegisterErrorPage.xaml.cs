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
    public partial class RegisterErrorPage : ContentPage
    {
        public RegisterErrorPage()
        {
            InitializeComponent();
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // vom inchide pagina de eroare si ne trimite la pagina de login

            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new LoginPage());
        }
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // vom inchide pagina de eroare

            await Navigation.PopModalAsync();
        }
    }
}