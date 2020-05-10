using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.Tag
{
    public interface IGameTag
    {
        GameTag GetById(int gameid, int tagid);
        IEnumerable<GameTag> GetPopularTagOfGame(int gameid);
        IEnumerable<GameTag> GetByUsername(int gameid, string username);
        GameTag Add(GameTag newGameTag);
        void Delete(GameTag deleteGameTag);
        int Commit();
        Task<int> CommitAsync();
    }
}
