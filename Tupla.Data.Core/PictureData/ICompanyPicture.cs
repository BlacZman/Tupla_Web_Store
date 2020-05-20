using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.PictureData
{
    public interface ICompanyPicture
    {
        IEnumerable<CompanyPicture> GetById(int id);
        IEnumerable<CompanyPicture> GetById(int id, ImageType type);
        CompanyPicture GetIconById(int id);
        CompanyPicture Add(CompanyPicture newPicture);
        CompanyPicture AddIcon(CompanyPicture newPicture);
        void Delete(CompanyPicture deletePicture);
        int Commit();
        Task<int> CommitAsync();
    }
}
