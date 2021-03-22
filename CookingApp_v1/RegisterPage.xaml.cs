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
            // vom "converti" (desface) de la Task<int> la int folosind await
            // in cazul in care result e 1, vom deschide pagina FridgeList
            // daca e 0, vom afisa un mesaj de eroare si vom deschide o pagina de eroare specifica
            // daca e -1, vom afisa un mesaj de eroare si vom deschide o alta pagina de eroare specifica
            var result = await App.Database.CheckRegisterAsync(m_utilizator);
            
            if (result == 1)
            {
                // cream un frigider nou care il vom insera in tabelul frigiderelor
                // si apoi il vom transmite la o functie pentru a updata utilizatorul curent
                // si a ii adauga acest frigider (trebuie intai sa adaugam frigiderul in tabel
                // pentru a folosi proprietatea frigider.F_id)
                Frigidere m_frigider = new Frigidere();
                m_frigider.F_utilizator_id = m_utilizator.U_id;
                await App.Database.AddUpdateFrigiderAsync(m_frigider);
                m_utilizator.U_frigider = m_frigider.F_id;
                await App.Database.SaveUtilizatorAsync(m_utilizator);


               // await DisplayAlert("Alerta:", "m_utilizator:" + m_utilizator.U_frigider, "ok??");


                await DisplayAlert("SUCCES!","Inregistrarea a avut succes!","Ok.");

                // vom iesi de pe pagina de register
                await Navigation.PopAsync();

                // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
                // adaugam o pagina de tipul Fridge pentru Frigider deoarece initial dupa reg/login ne va trimite la pagina cu ingredientele
                //await Navigation.PushAsync(new FridgeListPage());
            }
            else if (result == 0)
            {
                await DisplayAlert("ESEC!", "Mai exista numele de utilizator sau email-ul.", "Ok.");
            }
            else if (result == -2)
            {
                await DisplayAlert("ESEC!", "Va rugam introduceti informatii in toate campurile.", "Ok.");
            }
            else
            {
                await DisplayAlert("ESEC!", "Eroare nespecificata. Incercati din nou.", "Ok.");
            }
        }
    }
}