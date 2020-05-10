using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.PlatformData;

namespace Tupla.Data.Context
{
    public class SqlPlatformOfGameData : IPlatformOfGame
    {
        private readonly TuplaContext db;

        public SqlPlatformOfGameData(TuplaContext db)
        {
            this.db = db;
        }
        public PlatformOfGame Add(PlatformOfGame newGamePlatform)
        {
            db.Add(newGamePlatform);
            return newGamePlatform;
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

        public void Delete(PlatformOfGame deleteGamePlatform)
        {
            db.Remove(deleteGamePlatform);
        }

        public IEnumerable<PlatformOfGame> GetAllSupportedPlatform(int gameid)
        {
            var query = from r in db.PlatformOfGame
                        where r.GameId == gameid
                        orderby r.PlatformId
                        select r;
            return query;
        }

        public PlatformOfGame GetById(int gameid, int platformid)
        {
            return db.PlatformOfGame.FirstOrDefault(r => r.GameId == gameid && r.PlatformId == platformid);
        }
    }
}
