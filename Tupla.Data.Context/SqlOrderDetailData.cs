using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.Shopping.OrderDetailData;

namespace Tupla.Data.Context
{
    public class SqlOrderDetailData : IOrderDetail
    {
        private readonly TuplaContext db;

        public SqlOrderDetailData(TuplaContext db)
        {
            this.db = db;
        }
        public OrderDetail Add(OrderDetail newOrderDetail)
        {
            db.Add(newOrderDetail);
            return newOrderDetail;
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

        public void Delete(OrderDetail deleteOrderDetail)
        {
            db.Remove(deleteOrderDetail);
        }

        public OrderDetail GetById(int OrderId, int GameId, int PlatformId)
        {
            return db.OrderDetail.FirstOrDefault(r => r.OrderId == OrderId && r.GameId == GameId && r.PlatformId == PlatformId);
        }

        public async Task<OrderDetail> GetByIdAsync(int OrderId, int GameId, int PlatformId)
        {
            return await db.OrderDetail.FirstOrDefaultAsync(r => r.OrderId == OrderId && r.GameId == GameId && r.PlatformId == PlatformId);
        }

        public IEnumerable<OrderDetail> GetByOrderId(int id)
        {
            var query = from r in db.OrderDetail
                        where r.OrderId == id
                        orderby r.GameId
                        select r;
            return query;
        }
    }
}
