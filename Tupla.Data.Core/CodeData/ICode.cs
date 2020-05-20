using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.CodeData
{
    public interface ICode
    {
        Code GetById(string CodeId);
        IEnumerable<Code> GetCodeByOrderId(int OrderId);
        Code Add(Code addCode);
        void Delete(Code delCode);
        int Commit();
        Task<int> CommitAsync();
    }
}
