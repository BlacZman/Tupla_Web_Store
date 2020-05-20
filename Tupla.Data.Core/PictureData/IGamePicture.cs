using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.PictureData
{
    public interface IGamePicture
    {
        IEnumerable<GamePicture> GetById(int? id);
        IEnumerable<GamePicture> GetById(int? id, ImageType type);
        GamePicture GetIconById(int? id);
        GamePicture Add(GamePicture newPicture);
        GamePicture AddIcon(GamePicture newPicture);
        void Delete(GamePicture deletePicture);
        int Commit();
        Task<int> CommitAsync();
    }
}
