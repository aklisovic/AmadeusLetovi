using Letovi.DAL;
using Letovi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Letovi.Controllers
{
    public class PopisLetovaController : Controller
    {
        private LetoviContext db = new LetoviContext();

        public async Task<ActionResult> Pretraga(int? brojOdraslih, int? brojDjece, string polazniAerodrom = "", string odredisniAerodrom = "", string datumPutovanja = "", string valuta = "")
        {
            var rezultat = await DohvatLetova.Pretrazi(polazniAerodrom, odredisniAerodrom, datumPutovanja, brojOdraslih, brojDjece, valuta);
            var zaVratiti = DohvatLetova.Premapiraj(rezultat);

            var listaIata = db.IataAirports.Select(i => new
            {
                IataID = i.IATA_Code,
                IataINaziv = i.IATA_Code + " - " + i.Name_Airport
            }).OrderBy(item => item.IataID);

            ViewBag.Iata = new SelectList(listaIata, "IataID", "IataINaziv");

            return View(zaVratiti);
        }
    }
}