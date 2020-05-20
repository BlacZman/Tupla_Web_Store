using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Context;
using Tupla.Data.Core.PlatformData;

namespace Tupla_Web_Store.Pages.t.test
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IPlatform db;

        public CreateModel(IPlatform db)
        {
            this.db = db;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Platform Platform { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            db.Add(Platform);
            await db.CommitAsync();

            return RedirectToPage("./Index");
        }
    }
}
