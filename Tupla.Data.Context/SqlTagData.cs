using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.Tag;

namespace Tupla.Data.Context
{
    public class SqlTagData : ITag
    {
        private readonly TuplaContext db;

        public SqlTagData(TuplaContext db)
        {
            this.db = db;
        }

        public Tag Add(Tag newTag)
        {
            db.Add(newTag);
            return newTag;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await db.SaveChangesAsync();
        }

        public void Delete(Tag deleteTag)
        {
            db.Remove(deleteTag);
        }

        public Tag GetById(int id)
        {
            return db.Tag.FirstOrDefault(r => r.TagId == id);
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await db.Tag.FirstOrDefaultAsync(r => r.TagId == id);
        }

        public IEnumerable<Tag> GetByUsername(string username)
        {
            var query = from r in db.Tag
                        where string.IsNullOrEmpty(username) || r.Username == username
                        orderby r.TagName
                        select r;
            return query;
        }
    }
}
