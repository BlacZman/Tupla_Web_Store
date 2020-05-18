using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Context;
using Tupla.Data.Core.CodeData;
using Tupla.Data.Core.CreditCard;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PlatformData;
using Tupla.Data.Core.Shopping.CartData;
using Tupla.Data.Core.Shopping.OrderDetailData;
using Tupla.Data.Core.Shopping.TransactionData;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.c
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly TuplaContext context;
        private readonly IGame gamedb;
        private readonly IPlatform platformdb;
        private readonly IPlatformOfGame gameplatformdb;
        private readonly ICreditCard creditdb;
        private readonly ICart cartdb;
        private readonly IOrderDetail orderdetaildb;
        private readonly ITransaction transactiondb;
        private readonly ICode codedb;

        public CheckOutModel(UserManager<User> userManager,
            TuplaContext Context,
            IGame gamedb,
            IPlatform platformdb,
            IPlatformOfGame gameplatformdb,
            ICreditCard creditdb,
            ICart cartdb,
            IOrderDetail orderdetaildb,
            ITransaction transactiondb,
            ICode codedb)
        {
            this.userManager = userManager;
            context = Context;
            this.gamedb = gamedb;
            this.platformdb = platformdb;
            this.gameplatformdb = gameplatformdb;
            this.creditdb = creditdb;
            this.cartdb = cartdb;
            this.orderdetaildb = orderdetaildb;
            this.transactiondb = transactiondb;
            this.codedb = codedb;
        }
        public IEnumerable<SelectListItem> CreditCards { get; set; }
        public Dictionary<Cart, Game> Summary { get; set; }
        public decimal Total { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            IEnumerable<CreditCard> credits;
            string username = userManager.GetUserName(User);
            credits = creditdb.GetCreditCardByUsername(username);
            if (!credits.Any()) return RedirectToPage("/Account/Manage/Creditcard", new { area = "Identity" });
            CreditCards = new SelectList(credits, "Username", "CreditId");
            Summary = new Dictionary<Cart, Game> { };
            var listcartitem = cartdb.GetByCartId(username);
            if (!listcartitem.Any()) return RedirectToPage("../g/Index");
            decimal total = 0;
            await Task.Run(()=> 
            {
                foreach (var item in listcartitem)
                {
                    var Game = gamedb.GetById(item.GameId);
                    total += Game.Price * (decimal)item.Quantity;
                    Summary.Add(item, Game);
                }
                Total = Math.Round(total, 2, MidpointRounding.ToEven);
            });
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await Task.Run(async () =>
            {
                string username = userManager.GetUserName(User);
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        IEnumerable<Cart> cartitem = cartdb.GetByCartId(username);
                        var transactionOrder = new Transac
                        {
                            Username = username,
                            OrderDate = DateTime.Now
                        };
                        transactiondb.Add(transactionOrder);
                        await transactiondb.CommitAsync();
                        foreach(var r in cartitem)
                        {
                            var orderdetail = new OrderDetail 
                            {
                                OrderId = transactionOrder.OrderId,
                                GameId = r.GameId,
                                PlatformId = r.PlatformId,
                                Quantity = r.Quantity,
                            };
                            orderdetaildb.Add(orderdetail);
                            await orderdetaildb.CommitAsync();

                            //Generate code
                            for(var i = 0; i < orderdetail.Quantity; i++)
                            {
                                var codeidgenerated = Guid.NewGuid().ToString();
                                var code = new Code
                                {
                                    CodeId = codeidgenerated,
                                    OrderId = orderdetail.OrderId,
                                    GameId = orderdetail.GameId,
                                    PlatformId = orderdetail.PlatformId
                                };
                                codedb.Add(code);
                                await codedb.CommitAsync();
                            }
                        }

                        cartdb.DeleteItemInCart(username);
                        await cartdb.CommitAsync();

                        await transaction.CommitAsync();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                        await transaction.RollbackAsync();
                    }
                }
            });
            return RedirectToPage("../u/Library");
        }
    }
}