using P04Zawodnicy.Shared.Domains;
using P04Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P01AplikacjaWebowaWstep
{
    public partial class ZawodnicyFormularz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
            List<Zawodnik> zawodnicy = mz.WczytajZawodnikow();

            foreach (var zawodnik in zawodnicy)
                lbDane.Items.Add(zawodnik.ImieNazwisko);

        }
    }
}