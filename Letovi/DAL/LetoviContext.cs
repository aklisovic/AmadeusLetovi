using Letovi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Letovi.DAL
{
    public class LetoviContext : DbContext
    {
        public DbSet<IataAirportCode> IataAirports { get; set; }
    }
}