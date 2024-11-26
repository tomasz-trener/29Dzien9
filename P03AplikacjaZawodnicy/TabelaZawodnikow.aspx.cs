using P04Zawodnicy.Shared.Domains;
using P04Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P03AplikacjaZawodnicy
{
    public partial class TabelaZawodnikow : System.Web.UI.Page
    {
        public List<Zawodnik> Zawodnicy { get; set; }
        public int? IdPodswietlanego { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
            Zawodnicy = mz.WczytajZawodnikow();

            //podswietelenie edytowanego zawodnika 
            string idPodswietlanego = Request["podswietlonyId"];
            if (!string.IsNullOrEmpty(idPodswietlanego))
                IdPodswietlanego = Convert.ToInt32(idPodswietlanego);
        }
    }
}