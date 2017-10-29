using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Letovi.Models
{
    public class PopisLetova
    {
        [Required(ErrorMessage = "Polje Polazni aerodrom je obvezno polje za odabir!")]
        [Display(Name= "Polazni aerodrom")]
        public string PolazniAerodrom { get; set; }

        [Required(ErrorMessage = "Polje Odredišni aerodrom je obvezno polje za odabir!")]
        [Display(Name = "Odredišni aerodrom")]
        public string OdredisniAerodrom { get; set; }

        [Required(ErrorMessage = "Polje Datum polaska/povratka je obvezno polje za unos!")]
        [Display(Name = "Datum polaska/povratka")]
        public string DatumPolaskaPovratka { get; set; }

        [Display(Name = "Broj presjedanja u odlaznom/povratnom putovanju")]
        public int BrojPresjedanjaUOdlaznomPovratnomPutovanju { get; set; }

        [Display(Name = "Broj putnika")]
        public int BrojPutnika { get; set; }

        public string Valuta { get; set; }

        [Display(Name = "Ukupna cijena")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public string UkupnaCijena { get; set; }
    }
}