using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.PictureData;
using Microsoft.EntityFrameworkCore;

namespace Tupla.Data.Context
{
    public class SqlCompanyPictureData : ICompanyPicture
    {
        private readonly TuplaContext db;

        public SqlCompanyPictureData(TuplaContext db)
        {
            this.db = db;
        }
        public CompanyPicture Add(CompanyPicture newPicture)
        {
            db.Add(newPicture);
            return newPicture;
        }

        public CompanyPicture AddIcon(CompanyPicture newPicture)
        {
            var old = this.GetIconById(newPicture.CompanyId);
            db.Add(newPicture);
            if(old != null) this.Delete(old);
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

        public void Delete(CompanyPicture deletePicture)
        {
            db.Remove(deletePicture);
        }

        public IEnumerable<CompanyPicture> GetById(int id)
        {
            var query = from r in db.CompanyPicture
                        where r.CompanyId == id
                        orderby r.imageType
                        select r;
            return query;
        }

        public IEnumerable<CompanyPicture> GetById(int id, ImageType type)
        {
            var query = from r in db.CompanyPicture
                        where r.CompanyId == id && r.imageType == type
                        orderby r.imageType
                        select r;
            return query;
        }

        public CompanyPicture GetIconById(int id)
        {
            return db.CompanyPicture.FirstOrDefault(r => r.CompanyId == id && r.imageType == ImageType.Icon);
        }
    }
}
