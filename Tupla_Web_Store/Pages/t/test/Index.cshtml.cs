using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.PlatformData;

namespace Tupla_Web_Store.Pages.t.test
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IPlatform db;

        public IndexModel(IPlatform db)
        {
            this.db = db;
        }

        public IEnumerable<Platform> Platform { get;set; }

        public async Task OnGetAsync()
        {
            await Task.Run(()=> 
            {
                Platform = db.GetAllByName("");
            });
        }
    }
}
