using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.GameData
{
    public interface IGame
    {
        IEnumerable<Game> GetGamesByName(string name);
        IEnumerable<Game> GetGamesByCompanyId(int id);
        Game GetById(int? id);
        Task<Game> GetByIdAsync(int? id);
        Game Update(Game updateGame);
        Game Add(Game newGame);
        void Delete(Game deleteGame);
        int Commit();
        Task<int> CommitAsync();
    }
}
