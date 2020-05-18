using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PlatformData;
using Tupla.Data.Core.Shopping.OrderDetailData;
using Tupla.Data.Core.Shopping.TransactionData;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.u
{
    [Authorize]
    public class OrderModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IGame gamedb;
        private readonly IPlatform platformdb;
        private readonly ITransaction transdb;
        private readonly IOrderDetail orderdetaildb;

        public OrderModel(UserManager<User> userManager,
            IGame gamedb,
            IPlatform platformdb,
            ITransaction transdb,
            IOrderDetail orderdetaildb)
        {
            this.userManager = userManager;
            this.gamedb = gamedb;
            this.platformdb = platformdb;
            this.transdb = transdb;
            this.orderdetaildb = orderdetaildb;
        }

        public Dictionary<OrderDetail, Game> Summary { get; set; }
        public decimal Total { get; set; }
        public int OrderId { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderid)
        {
            var username = userManager.GetUserName(User);
            var trans = transdb.GetById(orderid).Username;
            if (trans != username) return RedirectToPage("../Index");
            await Task.Run(()=>
            {
                Summary = new Dictionary<OrderDetail, Game> { };
                var detail = orderdetaildb.GetByOrderId(orderid);
                decimal total = 0;
                foreach (var r in detail)
                {
                    var game = gamedb.GetById(r.GameId);
                    Summary.Add(r, game);
                    total = total + (game.Price * r.Quantity);
                }
                Total = total;
                OrderId = orderid;
            });
            return Page();
        }
    }
}