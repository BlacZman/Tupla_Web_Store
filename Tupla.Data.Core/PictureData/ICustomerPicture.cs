using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.PictureData
{
    public interface ICustomerPicture
    {
        IEnumerable<CustomerPicture> GetById(string username);
        IEnumerable<CustomerPicture> GetById(string username, ImageType type);
        CustomerPicture GetIconById(string username);
        CustomerPicture Add(CustomerPicture newPicture);
        CustomerPicture AddIcon(CustomerPicture newPicture);
        void Delete(CustomerPicture deletePicture);
        int Commit();
        Task<int> CommitAsync();
    }
}
