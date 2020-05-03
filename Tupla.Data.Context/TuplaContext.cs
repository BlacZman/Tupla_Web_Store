using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tupla.Data.Core.CompanyData;
using Tupla.Data.Core.CustomerData;
using Tupla.Data.Core.GameData;

namespace Tupla.Data.Context
{
    public class TuplaContext : DbContext
    {
        public TuplaContext(DbContextOptions<TuplaContext> options) : base (options)
        {

        }
        public DbSet<Company> Company { get; set; }
        public DbSet<Game> Game { get; set; }
    }

}
