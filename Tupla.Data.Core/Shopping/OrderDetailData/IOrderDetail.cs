using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.Shopping.OrderDetailData
{
    public interface IOrderDetail
    {
        OrderDetail GetById(int OrderId, int GameId, int PlatformId);
        Task<OrderDetail> GetByIdAsync(int OrderId, int GameId, int PlatformId);
        IEnumerable<OrderDetail> GetByOrderId(int id);
        OrderDetail Add(OrderDetail newOrderDetail);
        void Delete(OrderDetail deleteOrderDetail);
        int Commit();
        Task<int> CommitAsync();
    }
}
