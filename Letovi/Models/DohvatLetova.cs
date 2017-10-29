using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Letovi.Models
{
    public class DohvatLetova
    {
        static string apiKey = "NcNT1kkKGWcmW6cY4o1ucgMPCTlRRvL7";
        static string baseUrl = "https://api.sandbox.amadeus.com/v1.2/flights/low-fare-search";
        static HttpClient client;
        static Dictionary<string, RootObject> CacherineVrijednosti = new Dictionary<string, RootObject>();

        static DohvatLetova()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<RootObject> Pretrazi(string polazniAerodrom, string odredisniAerodrom, string datumPutovanja, int? brojOdraslih, int? brojDjece, string valuta)
        {
            string path = "?apikey=" + apiKey;

            if (polazniAerodrom == "" || odredisniAerodrom == "" || datumPutovanja == "")
                return null;

            if (polazniAerodrom != "")
                path = path + "&origin=" + polazniAerodrom;
            if (odredisniAerodrom != "")
                path = path + "&destination=" + odredisniAerodrom;
            if (datumPutovanja != "")
                path = path + "&departure_date=" + datumPutovanja;
            if (brojOdraslih.HasValue)
                path = path + "&adults=" + brojOdraslih.Value;
            if (brojDjece.HasValue)
                path = path + "&children=" + brojDjece.Value;
            if (valuta != "")
                path = path + "&currency=" + valuta;

            if (CacherineVrijednosti.ContainsKey(path))
                return CacherineVrijednosti[path];

            RootObject osnovniObjekt = null;
            HttpResponseMessage rezultatUpita = await client.GetAsync(path);
            if (rezultatUpita.IsSuccessStatusCode)
            {
                osnovniObjekt = await rezultatUpita.Content.ReadAsAsync<RootObject>();
                if (!CacherineVrijednosti.ContainsKey(path))
                    CacherineVrijednosti.Add(path, osnovniObjekt);
            }
            return osnovniObjekt;
        }

        public static List<PopisLetova> Premapiraj(RootObject rezultat)
        {
            List<PopisLetova> PopisLetova = new List<PopisLetova>();
            if (rezultat == null)
                return PopisLetova;
            foreach (var result in rezultat.results)
            {
                string valuta = rezultat.currency;
                string ukupnaCijena = result.fare.total_price;
                foreach (var res in result.itineraries)
                {
                    PopisLetova let = new PopisLetova();
                    let.Valuta = valuta;
                    let.UkupnaCijena = ukupnaCijena;
                    let.PolazniAerodrom = res.outbound.flights[0].origin.airport;
                    let.OdredisniAerodrom = res.outbound.flights[0].destination.airport;
                    let.BrojPresjedanjaUOdlaznomPovratnomPutovanju = res.outbound.flights.Count - 1;
                    //  let.BrojPresjedanjaUOdlaznom = res.outbound.flights.Count - 1;
                    //  let.BrojPresjedanjaUPovratnom = res.inbound.flights.Count - 1;
                    let.BrojPutnika = res.outbound.flights[0].booking_info.seats_remaining;
                    let.DatumPolaskaPovratka = res.outbound.flights[0].departs_at.Replace("T", " ");
                    //  let.DatumPolaska = res.outbound.flights[0].departs_at.Replace("T", " ");
                    //  let.DatumPovratka = res.inbound.flights[0].departs_at.Replace("T", " ");

                    PopisLetova.Add(let);
                }
            }
            return PopisLetova;
        }
    }
}