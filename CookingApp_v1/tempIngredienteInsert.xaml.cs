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
    public partial class tempIngredienteInsert : ContentPage
    {
        public tempIngredienteInsert()
        {
            InitializeComponent();
        }
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var m_ingredient = (Ingrediente)BindingContext;
            await App.Database.tempAddIngredienteAsync(m_ingredient);
        }
    }
}