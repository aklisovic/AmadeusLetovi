using Letovi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Letovi.DAL
{
    public class LetoviInitializer : DropCreateDatabaseAlways<LetoviContext>
    {
        protected override void Seed(LetoviContext context)
        {
            var iataAirportJSONFilePath = HttpContext.Current.Server.MapPath("~/Content/IATAAirport.json");
            var fileData = File.ReadAllText(iataAirportJSONFilePath);
            var objects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IataAirportCode>>(fileData);

            foreach (var o in objects)
                context.IataAirports.Add(o);

            base.Seed(context);
        }
    }
}