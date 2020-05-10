using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.PictureData;
using Microsoft.EntityFrameworkCore;

namespace Tupla.Data.Context
{
    public class SqlCustomerPictureData : ICustomerPicture
    {
        private readonly TuplaContext db;

        public SqlCustomerPictureData(TuplaContext db)
        {
            this.db = db;
        }
        public CustomerPicture Add(CustomerPicture newPicture)
        {
            db.Add(newPicture);
            return newPicture;
        }

        public CustomerPicture AddIcon(CustomerPicture newPicture)
        {
            var old = this.GetIconById(newPicture.Username);
            db.Add(newPicture);
            if (old != null) this.Delete(old);
            return newPicture;
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

        public void Delete(CustomerPicture deletePicture)
        {
            db.Remove(deletePicture);
        }

        public IEnumerable<CustomerPicture> GetById(string username)
        {
            var query = from r in db.CustomerPicture
                        where r.Username == username
                        orderby r.imageType
                        select r;
            return query;
        }

        public IEnumerable<CustomerPicture> GetById(string username, ImageType type)
        {
            var query = from r in db.CustomerPicture
                        where r.Username == username && r.imageType == type
                        orderby r.imageType
                        select r;
            return query;
        }

        public CustomerPicture GetIconById(string username)
        {
            return db.CustomerPicture.FirstOrDefault(r => r.Username == username && r.imageType == ImageType.Icon);
        }
    }
}
