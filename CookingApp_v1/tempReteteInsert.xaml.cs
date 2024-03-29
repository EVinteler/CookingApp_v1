﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingApp_v1.Models;

namespace CookingApp_v1
{
    /***
     * basically cum functioneaza prostia asta e ca tre sa alegi ingredientele
     * fiecare pe rand FARA sa iesi de pe pg asta, de fiecare data cand dai click pe o reteta
     * se re-creaza lista ingredientelor retetei lol
     ***/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class tempReteteInsert : ContentPage
    {
        //Retete m_reteta = null;
        public tempReteteInsert()
        {
            // constructor fara argumente pt cand inseram o reteta noua (care e creata la butonul submit)
            InitializeComponent();
        }
        public tempReteteInsert(Retete reteta)
        {
            InitializeComponent();


            //m_reteta = reteta;

            //foreach (Ingrediente i in m_reteta.R_ingrediente)
                //System.Diagnostics.Debug.WriteLine(">>>TRI_CSTing: " + i.N_nume);
        }
        protected override async void OnAppearing()
        {
            // comenteaza pt inserare retete
            // de aici
            Retete m_reteta = (Retete)BindingContext;
            Retete n_reteta = await App.Database.GetRetetaAsync(m_reteta.R_id);
            System.Diagnostics.Debug.WriteLine(">>>UP: " + n_reteta.R_nume);
            foreach (Ingrediente i in m_reteta.R_ingrediente)
                System.Diagnostics.Debug.WriteLine(">>>UPING: " + i.N_nume);
            // aici

            base.OnAppearing();

            // pentru ca listView din .xaml sa stie care lista sa o afiseze, folosim functia din CookingDatabase

            // elementele de la listViewIngredient vor avea valorile primite din GetIngredientListAsync, metoda din CookingDatabase

            // selectam toate ingredientele deci search si categorie vor fi null
            string m_search = null;
            string m_categorie = null;

            listViewIngredient.ItemsSource = await App.Database.GetIngredientListAsync(m_search,m_categorie);
            //Retete m_reteta = (Retete)BindingContext;
            //listViewRetetaIngredient.ItemsSource = App.Database.GetRetetaIngredientListAsync(m_reteta);

            // vom afisa informatiile luate din m_reteta (daca exista)
        }
        async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            /*Retete n_reteta = new Retete()
            {
                R_nume = e_nume.Text,
                R_link = e_link.Text,
                R_cultura = e_cultura.Text,
                R_descriere = e_descriere.Text
            };

            //n_reteta = App.Database.tempGetNewReteta(n_reteta);*/

            // await DisplayAlert("INFO RETETA:","nume: " + n_reteta.R_nume,"ok");

            Retete m_reteta = (Retete)BindingContext;
            //await DisplayAlert("INFO RETETA:", "nume: " + m_reteta.R_nume, "ok");

            await App.Database.tempAddUpdateReteteAsync(m_reteta);

            //await Navigation.PopAsync();
        }
        async void OnIngredientAddItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            //foreach (Ingrediente i in m_reteta.R_ingrediente)
            //System.Diagnostics.Debug.WriteLine(">>>TRI_ADDing: " + i.N_nume);


            Retete m_reteta = (Retete)BindingContext;

            Ingrediente ing;
            if (e.SelectedItem != null)
            {
                ing = e.SelectedItem as Ingrediente;

                /*
                List<Ingrediente> m_lista_ingr_1 = App.Database.GetRetetaIngredientListAsync(m_reteta);
                foreach (Ingrediente i in m_lista_ingr_1)
                    System.Diagnostics.Debug.WriteLine(">>>1INGLISTing: " + i.N_nume);

                m_reteta.R_ingrediente = new List<Ingrediente> { ing };
                foreach (Ingrediente i in m_reteta.R_ingrediente)
                    System.Diagnostics.Debug.WriteLine(">>>TRI_ADDing: " + i.N_nume);
                await App.Database.tempAddUpdateReteteAsync(m_reteta);

                List<Ingrediente> m_lista_ingr_2 = App.Database.GetRetetaIngredientListAsync(m_reteta);
                foreach (Ingrediente i in m_lista_ingr_2)
                    System.Diagnostics.Debug.WriteLine(">>>2INGLISTing: " + i.N_nume);
                */

                //await DisplayAlert(">>>Alerta:", "before getReteta", "okae");
                List<Ingrediente> m_lista_ingr_1 = App.Database.GetRetetaIngredientListAsync(m_reteta);
                foreach (Ingrediente i in m_lista_ingr_1)
                    System.Diagnostics.Debug.WriteLine(">>>M_LTEMPRINing: " + i.N_nume);
                //await DisplayAlert(">>>Alerta:", "before NewIng", "okae");
                List<Ingrediente> m_lista_ingr_2 = new List<Ingrediente> { ing };
                //await DisplayAlert(">>>Alerta:", "before Concat", "okae");
                m_reteta.R_ingrediente = m_lista_ingr_1.Concat(m_lista_ingr_2).ToList();
                await App.Database.tempAddUpdateReteteAsync(m_reteta);

                foreach (Ingrediente i in m_reteta.R_ingrediente)
                    System.Diagnostics.Debug.WriteLine(">>>TEMPRINing: " + i.N_nume);


                Retete n_reteta = await App.Database.GetRetetaAsync(m_reteta.R_id);
                System.Diagnostics.Debug.WriteLine(">>>2RETETA: " + n_reteta.R_nume);
                foreach (Ingrediente i in m_reteta.R_ingrediente)
                    System.Diagnostics.Debug.WriteLine(">>>2RETETAing: " + i.N_nume);

                //NU activa pop, basically cum functioneaza prostia asta e ca tre sa alegi ingredientele
                // fiecare pe rand FARA sa iesi de pe pg asta, de fiecare data cand dai click pe o reteta
                // se re-creaza lista ingredientelor retetei lol
                //await Navigation.PopAsync();

                // de ce nu mi se salveaza reteta in DB cand ies de pe pagina? :'O
            }
        }
    }
}