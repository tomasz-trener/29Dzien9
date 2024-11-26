﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P04Zawodnicy.Shared.Services
{
    public enum Jednostka
    {
        Celsjusz,
        Kelvin,
        Farenheit
    }


    public class ManagerPogody
    {
        // string szukanyZnak = "°";
        private string znakKoncowy = ">";

        public string SzukanyZnak { get; set; } = "°";

        string adresPodstawowy = "https://www.google.com/search?q=pogoda ";

        public string AdresPodstawowy
        {
            get
            {
                return adresPodstawowy;
            }
            set
            {
                adresPodstawowy = value;
            }
        }

        private Jednostka jednostka;

        public ManagerPogody(Jednostka jednostka)
        {
            this.jednostka = jednostka;
        }
        public double PodajTemperature(string miasto)
        {
            //      string adresPodstawowy = "https://www.google.com/search?q=pogoda ";
            //      string szukanyZnak = "°";
            //      string znakKoncowy = ">";

            string adres = adresPodstawowy + miasto;

            WebClient wc = new WebClient();
            string dane = wc.DownloadString(adres);

            int indx = dane.IndexOf(SzukanyZnak);
            int aktualnaPozycja = indx;

            while (dane.Substring(aktualnaPozycja, 1) != znakKoncowy)
                aktualnaPozycja--;

            string wynik = dane.Substring(aktualnaPozycja + 1, indx - aktualnaPozycja - 1);
            return transformujTemperature(jednostka, Convert.ToDouble(wynik));

        }

        private double transformujTemperature(Jednostka jednostka, double temp)
        {
            if (jednostka == Jednostka.Celsjusz)
                return temp;

            if (jednostka == Jednostka.Farenheit)
                return temp * 1.8 + 32;

            if (jednostka == Jednostka.Kelvin)
                return temp + 273.15;

            throw new ArgumentException("Nieznana jednostka");


        }
    }
}
