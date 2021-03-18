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
    public partial class tempViewAllTables : ContentPage
    {
        public tempViewAllTables()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // pentru ca listView din .xaml sa stie care lista sa o afiseze, folosim functia din CookingDatabase
            // care ne returneaza elemente de tip world, character si story

            // elementele de la listViewIngredient vor avea valorile primite din GetIngredientListAsync, metoda din CookingDatabase

            listViewUtilizatori.ItemsSource = await App.Database.GetUtilizatoriListAsync();
            listViewIngrediente.ItemsSource = await App.Database.GetIngredientListAsync();
            listViewFiltre.ItemsSource = await App.Database.GetFiltruListAsync();
            listViewRetete.ItemsSource = await App.Database.GetRetetaListAsync();
            //listViewFrigidere.ItemsSource = await App.Database.GetFrigiderListAsync();
        }

        // cand selectam un element, dorim sa primim o alerta cu informatiile care le contine
        /*async void OnUtilizatoriViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await DisplayAlert("OnCharacterViewItemSelected", 
                "Opened [OnCharacterViewItemSelected].", "Ok.");
        }*/
    }
}