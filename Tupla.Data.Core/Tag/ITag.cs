using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.Tag
{
    public interface ITag
    {
        Tag GetById(int id);
        Task<Tag> GetByIdAsync(int id);
        IEnumerable<Tag> GetByUsername(string username);
        Tag Add(Tag newTag);
        void Delete(Tag deleteTag);
        int Commit();
        Task<int> CommitAsync();
    }
}
