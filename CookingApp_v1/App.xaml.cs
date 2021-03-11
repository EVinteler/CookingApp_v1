using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingApp_v1.Data;

namespace CookingApp_v1
{
    public partial class App : Application
    {
        static CookingDatabase database;
        public static CookingDatabase Database
        {
            get
            {
                if (database == null)
                {
                    // daca nu exista baza de date de tipul nostru o cream, folosind path-ul corespunzator
                    database = new CookingDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
                   LocalApplicationData), "Worldbuilding.db3"));
                }
                // returnam baza de date existenta sau cea creata mai sus
                return database;
            }
        }
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
