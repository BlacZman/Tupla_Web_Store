using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.PlatformData
{
    public interface IPlatformOfGame
    {
        PlatformOfGame GetById(int gameid, int platformid);
        IEnumerable<PlatformOfGame> GetAllSupportedPlatform(int gameid);
        PlatformOfGame Add(PlatformOfGame newGamePlatform);
        void Delete(PlatformOfGame deleteGamePlatform);
        int Commit();
        Task<int> CommitAsync();
    }
}
