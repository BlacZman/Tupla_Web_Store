using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tupla.Data.Core.CustomerData;

namespace Tupla.Data.Context
{
    public class TuplaContext : DbContext
    {
        public TuplaContext(DbContextOptions<TuplaContext> options) : base (options)
        {

        }
    }

}
