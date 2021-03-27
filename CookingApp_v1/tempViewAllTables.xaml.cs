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
            listViewFrigidere.ItemsSource = await App.Database.GetFrigiderListAsync();
        }
        async void OnRetetaItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var m_reteta = e.SelectedItem as Retete;
                //m_reteta.R_ingrediente = App.Database.GetRetetaIngredientListAsync(m_reteta);

                //await DisplayAlert(">>>Alerta:", "before ModReteta", "okae");

                // trimitem reteta curenta spre pagina de inserate retete pt a o modifica
                await Navigation.PushAsync(new tempReteteInsert
                {
                    BindingContext = m_reteta
                });

                //await Navigation.PopAsync();
            }
        }
    }
}