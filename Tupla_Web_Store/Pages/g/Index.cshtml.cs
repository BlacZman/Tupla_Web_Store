using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;

namespace Tupla_Web_Store.Pages.g
{
    public class IndexModel : PageModel
    {
        private readonly IGame db;

        public IndexModel(IGame db)
        {
            this.db = db;
        }

        public IEnumerable<Game> Game { get; private set; }
        [BindProperty(SupportsGet = true)]
        public string search { get; set; }

        public void OnGet()
        {
            Game = db.GetGamesByName(search);
        }
    }
}
