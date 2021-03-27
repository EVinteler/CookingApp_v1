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
     * LOGIN.PAGE este pagina de autentificare
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            // am luat informatiile din transmise prin editoare din xaml convertite la
            // tipul unei inregistrari a tabelului Utilizatori si le-am pus in m_utilizator
            var m_utilizator = (Utilizatori)BindingContext;

            /*
            var m_frigider = await App.Database.GetFrigiderFromUtilizatorAsync(m_utilizator);
            await DisplayAlert("be4 FRIDGE", "m_frigider id: " + m_frigider.F_id, "oke.");

            foreach (Ingrediente ing in m_frigider.F_ingrediente)
                System.Diagnostics.Debug.WriteLine(">>>2LOGing: " + ing.N_nume);

            var first = m_frigider.F_ingrediente.First();
            await DisplayAlert("be4 FRIDGE", "m_frigider id: " + m_frigider.F_id + " " + first.N_id, "oke.");
            */

            // apelam functia de autentificare cu informatiile transmise
            // vom "converti" (desface) de la Task<int> la int folosind await
            // in cazul in care result e 1, vom deschide pagina FridgeList
            // daca e 0 sau -1, vom afisa un mesaj de eroare si vom deschide o pagina de eroare specifica
            // daca e -2, vom afisa un mesaj de eroare si vom deschide o alta pagina de eroare specifica
            var result = await App.Database.CheckLoginAsync(m_utilizator);

            if (result == 1)
            {
                await DisplayAlert("SUCCES!", "Logarea a avut succes!", "Ok.");
                // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
                // adaugam o pagina de tipul Fridge pentru Frigider deoarece initial dupa reg/login ne va trimite la pagina cu ingredientele

                
                // vom lua frigiderul din functie la logare si il vom trimite in restul aplicatiei
                // ca parametru la fiecare pagina, deoarece altfel se pierde lista de ingrediente
                // (da, e ciudat, stiu, am testat)
                var m_frigider = await App.Database.GetFrigiderFromUtilizatorAsync(m_utilizator);

                //await DisplayAlert("ONAPP FRIDGE", "m_frigider id: " + m_frigider.F_id, "oke.");

                //foreach (Ingrediente ing in m_frigider.F_ingrediente)
                    //System.Diagnostics.Debug.WriteLine(">>>2LOGing: " + ing.N_nume);

                //var first = m_frigider.F_ingrediente.First();
                //await DisplayAlert("ONAPP FRIDGE", "m_frigider id: " + m_frigider.F_id + " " + first.N_id, "oke.");
                
                await Navigation.PushAsync(new FridgeListPage(m_utilizator,m_frigider));
            }
            else if (result == 0)
            {
                await DisplayAlert("ESEC!", "Nu exista numele de utilizator.", "Ok.");
            }
            else if (result == -1)
            {
                await DisplayAlert("ESEC!", "Parola nu corespunde cu numele de utilizator.", "Ok.");
            }
            else
            {
                await DisplayAlert("ESEC!", "Eroare nespecificata. Incercati din nou.", "Ok.");
            }
        }

    }
}