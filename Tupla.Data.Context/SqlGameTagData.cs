using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.Tag;

namespace Tupla.Data.Context
{
    public class SqlGameTagData : IGameTag
    {
        private readonly TuplaContext db;

        public SqlGameTagData(TuplaContext db)
        {
            this.db = db;
        }

        public GameTag Add(GameTag newGameTag)
        {
            db.Add(newGameTag);
            return newGameTag;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await db.SaveChangesAsync();
        }

        public void Delete(GameTag deleteGameTag)
        {
            db.Remove(deleteGameTag);
        }

        public GameTag GetById(int gameid, int tagid)
        {
            return db.GameTag.FirstOrDefault(r => r.GameId == gameid && r.TagId == tagid);
        }

        public IEnumerable<GameTag> GetByUsername(int gameid, string username)
        {
            var query = from r in db.GameTag
                        where r.GameId == gameid && (string.IsNullOrEmpty(username) || r.Username == username)
                        orderby r.TagId
                        select r;
            return query;
        }

        public IEnumerable<GameTag> GetPopularTagOfGame(int gameid)
        {
            var query = from r in db.GameTag
                        where r.GameId == gameid
                        orderby r.TagId
                        select r;
            return query;
        }
    }
}
