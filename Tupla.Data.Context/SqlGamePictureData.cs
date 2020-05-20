using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.PictureData;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Tupla.Data.Context
{
    public class SqlGamePictureData : IGamePicture
    {
        private readonly TuplaContext db;

        public SqlGamePictureData(TuplaContext db)
        {
            this.db = db;
        }
        public GamePicture Add(GamePicture newPicture)
        {
            db.Add(newPicture);
            return newPicture;
        }

        public GamePicture AddIcon(GamePicture newPicture)
        {
            var old = this.GetIconById(newPicture.GameId);
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

        public void Delete(GamePicture deletePicture)
        {
            db.Remove(deletePicture);
        }

        public IEnumerable<GamePicture> GetById(int? id)
        {
            var query = from r in db.GamePicture
                        where r.GameId == id
                        orderby r.Path
                        select r;
            return query;
        }

        public IEnumerable<GamePicture> GetById(int? id, ImageType type)
        {
            var query = from r in db.GamePicture
                        where r.GameId == id && r.imageType == type
                        orderby r.imageType
                        select r;
            return query;
        }

        public GamePicture GetIconById(int? id)
        {
            return db.GamePicture.FirstOrDefault(r => r.GameId == id && r.imageType == ImageType.Icon);
        }
    }
}
