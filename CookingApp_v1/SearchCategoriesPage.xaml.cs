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
    /*
     * SEARCH.CATEGORIES.PAGE este o pagina cu mai multe categorii de ingrediente dintre care putem alege
     */
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchCategoriesPage : ContentPage
    {
        Utilizatori m_utilizator;
        Frigidere m_frigider;
        public SearchCategoriesPage(Utilizatori utilizator, Frigidere frigider)
        {
            InitializeComponent();
            m_utilizator = utilizator;
            m_frigider = frigider;
        }
        void OnFructeButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "fructe";
            NewSearchPageByCategory(m_categorie);
        }
        void OnLegumeButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "legume";
            NewSearchPageByCategory(m_categorie);
        }
        void OnCarneButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "carne";
            NewSearchPageByCategory(m_categorie);
        }
        void OnFainoaseButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "fainoase";
            NewSearchPageByCategory(m_categorie);
        }
        void OnSosuriButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "sosuri";
            NewSearchPageByCategory(m_categorie);
        }
        void OnCondimenteButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "condimente";
            NewSearchPageByCategory(m_categorie);
        }
        void OnLactateButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "lactate";
            NewSearchPageByCategory(m_categorie);
        }
        void OnDulciuriButtonClicked(object sender, EventArgs e)
        {
            // preluam categoria de la buton
            string m_categorie = "dulciuri";
            NewSearchPageByCategory(m_categorie);
        }
        async void NewSearchPageByCategory(string n_categorie)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul SearchList care va arata ingredientele sub forma de lista
            // in cazul de fata, vom arata doar elementele din categoria preluata
            Navigation.PopAsync();
            await Navigation.PushAsync(new SearchListPage(m_utilizator, m_frigider, n_categorie));
        }
        /*void OnSearchListButtonClicked(object sender, EventArgs e)
        {
            // PUSHasync ne adauga o noua pagina pe stack-ul de pagini de navigare
            // adaugam o pagina de tipul SearchList care cauta 
            // mai tarziu o vom schimba sa ne caute ingrediente doar  din categoria noastra
            /*await Navigation.PushAsync(new ListPage
                {
                    BindingContext = new World()
                });

            // facem Pop pentru a nu intra intr-un loop de Search Pages
            // nu facem await pt ca atunci nu continua cu Push
            Navigation.PopAsync();

            Utilizatori m_utilizator = (Utilizatori)BindingContext;
            /*await Navigation.PushAsync(new SearchListPage
            {
                // vom transmite informatiile din utilizator (luate inca de la logare) in continuare
                BindingContext = m_utilizator
            });
        }*/
    }
}