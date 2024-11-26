﻿using P04Zawodnicy.Shared.Domains;
using P04Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P03AplikacjaZawodnicy
{
    public partial class SzczegolyZawodnika : System.Web.UI.Page
    {
        enum TrybOperacji
        {
            Tworzenie,
            Edycja
        }

        private TrybOperacji trybOperacji => string.IsNullOrEmpty(Request["id"]) ? TrybOperacji.Tworzenie : TrybOperacji.Edycja;

        protected void Page_Load(object sender, EventArgs e)
        {
            WczytajTrenerow();
          
            string idStr = Request["id"];
            if (!string.IsNullOrEmpty(idStr) && !Page.IsPostBack)
            {
                IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
                var zawodnik = mz.PodajZawodnika(Convert.ToInt32(idStr));
                
                txtId.Text = Convert.ToString(zawodnik.Id_zawodnika);
                txtImie.Text = zawodnik.Imie;
                txtNazwisko.Text = zawodnik.Nazwisko;
                txtKraj.Text = zawodnik.Kraj;
                txtWaga.Text = Convert.ToString(zawodnik.Waga);
                txtWzrost.Text = Convert.ToString(zawodnik.Wzrost);
                txtDataUr.Text = zawodnik.DataUrodzenia?.ToString("dd-MM-yyyy");

               
                ddlTrener.SelectedValue = zawodnik.Id_trenera.ToString();
            }
           
            btnUsun.Visible = trybOperacji == TrybOperacji.Edycja;

        }

        private void WczytajTrenerow()
        {
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
            var trenerzy = mz.PodajTrenerow();
            ddlTrener.DataSource = trenerzy;
            ddlTrener.DataTextField = "Nazwisko";
            ddlTrener.DataValueField = "Id";
            ddlTrener.DataBind();
        }

        protected void btnZapisz_Click(object sender, EventArgs e)
        {
            Zawodnik zawodnik = new Zawodnik();
            zawodnik.Imie = txtImie.Text;
            zawodnik.Nazwisko = txtNazwisko.Text;
            zawodnik.Kraj = txtKraj.Text;
            zawodnik.DataUrodzenia = Convert.ToDateTime(txtDataUr.Text);
            zawodnik.Waga = Convert.ToInt32(txtWaga.Text);
            zawodnik.Wzrost = Convert.ToInt32(txtWzrost.Text);
            zawodnik.Id_trenera = Convert.ToInt32(ddlTrener.SelectedValue);
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();

           

            if(trybOperacji == TrybOperacji.Tworzenie)
            {
                mz.Dodaj(zawodnik);

                List<int> nowoDodaniZawodnicy = (List<int>)Session["nowoDodaniZawodnicy"];
                if(nowoDodaniZawodnicy == null)
                    nowoDodaniZawodnicy = new List<int>();

                nowoDodaniZawodnicy.Add(zawodnik.Id_zawodnika);
                Session["nowoDodaniZawodnicy"] = nowoDodaniZawodnicy;
            }
            else if (trybOperacji == TrybOperacji.Edycja)
            {
                zawodnik.Id_zawodnika = Convert.ToInt32(txtId.Text);
                mz.Edytuj(zawodnik);
            }

            Response.Redirect($"TabelaZawodnikow.aspx?podswietlonyId={zawodnik.Id_zawodnika}");
        }

        protected void btnUsun_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
            mz.Usun(id);
            Response.Redirect("TabelaZawodnikow.aspx");
        }
    }
}