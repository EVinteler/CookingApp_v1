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
    public partial class tempReteteInsert : ContentPage
    {
        public tempReteteInsert()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // pentru ca listView din .xaml sa stie care lista sa o afiseze, folosim functia din CookingDatabase
            // care ne returneaza elemente de tip world, character si story

            // elementele de la listViewIngredient vor avea valorile primite din GetIngredientListAsync, metoda din CookingDatabase

            listViewIngredient.ItemsSource = await App.Database.GetIngredientListAsync();
        }
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var m_reteta = (Retete)BindingContext;
            await App.Database.tempAddUpdateReteteAsync(m_reteta);
        }
        async void OnIngredientAddItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var m_reteta = (Retete)BindingContext;
            Ingrediente ing;
            if (e.SelectedItem != null)
            {
                ing = e.SelectedItem as Ingrediente;
                m_reteta.R_ingrediente.Add(ing);
                await App.Database.tempAddUpdateReteteAsync(m_reteta);

                await Navigation.PopAsync();
            }
        }
    }
}