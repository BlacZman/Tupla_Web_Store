using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.CodeData;

namespace Tupla.Data.Context
{
    public class SqlCodeData : ICode
    {
        private readonly TuplaContext db;

        public SqlCodeData(TuplaContext db)
        {
            this.db = db;
        }
        public Code Add(Code addCode)
        {
            db.Add(addCode);
            return addCode;
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

        public void Delete(Code delCode)
        {
            db.Remove(delCode);
        }

        public Code GetById(string CodeId)
        {
            return db.Code.FirstOrDefault(r => r.CodeId == CodeId);
        }

        public IEnumerable<Code> GetCodeByOrderId(int OrderId)
        {
            var query = from r in db.Code
                        where r.OrderId == OrderId
                        orderby r.GameId, r.PlatformId
                        select r;
            return query;
        }
    }
}
