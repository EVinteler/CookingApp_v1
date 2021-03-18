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
    public partial class tempFiltreInsert : ContentPage
    {
        public tempFiltreInsert()
        {
            InitializeComponent();
        }
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var m_filtru = (Filtre)BindingContext;
            await App.Database.tempAddFiltreAsync(m_filtru);
        }
    }
}