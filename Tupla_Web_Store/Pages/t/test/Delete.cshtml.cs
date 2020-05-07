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
    public class DeleteModel : PageModel
    {
        private readonly IPlatform db;

        public DeleteModel(IPlatform db)
        {
            this.db = db;
        }

        [BindProperty]
        public Platform Platform { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await Task.Run(()=> 
            {
                Platform = db.GetById((int)id);
            });
            

            if (Platform == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await Task.Run(() =>
            {
                Platform = db.GetById((int)id);
            });

            if (Platform != null)
            {
                db.Delete(Platform);
                await db.CommitAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
