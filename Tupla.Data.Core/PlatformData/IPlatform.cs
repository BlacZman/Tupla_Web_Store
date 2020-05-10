using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.PlatformData
{
    public interface IPlatform
    {
        Platform GetById(int id);
        IEnumerable<Platform> GetAllByName(string name);
        Platform Add(Platform newPlatform);
        void Delete(Platform deletePlatform);
        int Commit();
        Task<int> CommitAsync();
    }
}
