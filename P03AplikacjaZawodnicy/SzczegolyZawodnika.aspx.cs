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
        protected void Page_Load(object sender, EventArgs e)
        {
            string idStr = Request["id"];
            if (!string.IsNullOrEmpty(idStr))
            {
                IManagerZawodnikow mz = new ManagerZawodnikowLINQ();
                var zawodnik = mz.podajzaw
            }

        }
    }
}