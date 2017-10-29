using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Letovi.Models
{
    public class IataAirportCode
    {
        public int ID { get; set; }

        [JsonProperty("iata")]
        public string IATA_Code { get; set; }

        [JsonProperty("name")]
        public string Name_Airport { get; set; }
    }
}