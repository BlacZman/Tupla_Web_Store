using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.GameData;

namespace Tupla.Data.Context
{
    public class SqlGameData : IGame
    {
        private readonly TuplaContext db;

        public SqlGameData(TuplaContext db)
        {
            this.db = db;
        }
        public Game Add(Game newGame)
        {
            db.Add(newGame);
            return newGame;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await db.SaveChangesAsync();
        }

        public void Delete(Game deleteGame)
        {
            db.Remove(deleteGame);
        }

        public Game GetById(int? id)
        {
            return db.Game.FirstOrDefault(r => r.GameId == id);
        }

        public async Task<Game> GetByIdAsync(int? id)
        {
            return await db.Game.FirstOrDefaultAsync(r => r.GameId == id);
        }

        public IEnumerable<Game> GetGamesByCompanyId(int id)
        {
            var query = from r in db.Game
                        where r.CompanyID == id
                        orderby r.GameName
                        select r;
            return query;
        }

        public IEnumerable<Game> GetGamesByName(string name)
        {
            var query = from r in db.Game
                        where r.GameName.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.GameName
                        select r;
            return query;
        }

        public Game Update(Game updateGame)
        {
            var entity = db.Game.Attach(updateGame);
            entity.State = EntityState.Modified;
            return updateGame;
        }
    }
}
