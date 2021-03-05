using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingApp_v1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // incepem stack-ul de pagini de navigare si adaugam o pagina de tipul StartPage
            
            MainPage = new NavigationPage(new StartPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
