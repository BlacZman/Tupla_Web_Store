using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.PlatformData;
using Microsoft.EntityFrameworkCore;

namespace Tupla.Data.Context
{
    public class SqlPlatformData : IPlatform
    {
        private readonly TuplaContext db;

        public SqlPlatformData(TuplaContext db)
        {
            this.db = db;
        }
        public Platform Add(Platform newPlatform)
        {
            db.Add(newPlatform);
            return newPlatform;
        }

        public int Commit()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Platform deletePlatform)
        {
            db.Remove(deletePlatform);
        }

        public IEnumerable<Platform> GetAllByName(string name)
        {
            var query = from r in db.Platform
                        where string.IsNullOrEmpty(name) || r.Platform_name.StartsWith(name)
                        orderby r.Platform_name
                        select r;
            return query;
        }

        public Platform GetById(int id)
        {
            return db.Platform.FirstOrDefault(r => r.PlatformId == id);
        }
    }
}
