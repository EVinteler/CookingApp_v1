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
            // cream un frigider nou care il vom transmite la functie pentru a insera informatii
            // de la utilizatorul nou in el
            Frigidere m_frigider = null;

            // apelam functia de inregistrare cu informatiile transmise
            // vom "converti" (desface) de la Task<int> la int folosind await
            // in cazul in care result e 1, vom deschide pagina FridgeList
            // daca e 0, vom afisa un mesaj de eroare si vom deschide o pagina de eroare specifica
            // daca e -1, vom afisa un mesaj de eroare si vom deschide o alta pagina de eroare specifica
            var result = await App.Database.CheckRegisterAsync(m_utilizator, m_frigider);
            
            if (result == 1)
            {
                await DisplayAlert("SUCCES!","Inregistrarea a avut succes!","Ok.");
                // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
                // adaugam o pagina de tipul Fridge pentru Frigider deoarece initial dupa reg/login ne va trimite la pagina cu ingredientele
                await Navigation.PushAsync(new FridgeListPage());
            }
            else if (result == 0)
            {
                await DisplayAlert("ESEC!", "Mai exista numele de utilizator sau email-ul.", "Ok.");
            }
            else
            {
                await DisplayAlert("ESEC!", "Eroare nespecificata. Incercati din nou.", "Ok.");
            }
        }
        async void OnErrorButtonClicked(object sender, EventArgs e)
        {
            // pushMODALasync ne adauga o noua pagina de tip modal pe stack-ul de pagini de navigare
            // aceasta pagina ii spune utilizatorului ca a aparut o eroare

            await Navigation.PushModalAsync(new RegisterErrorPage());
        }
    }
}