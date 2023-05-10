using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X2R.Insight.Janitor.WebApi.Data;
using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApplication.Pages.Querys
{
    public class IndexModel : PageModel
    {
        private readonly X2R.Insight.Janitor.WebApi.Data.QueryContext _context;

        public IndexModel(X2R.Insight.Janitor.WebApi.Data.QueryContext context)
        {
            _context = context;
        }

        public IList<_Querys> _Querys { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Querys != null)
            {
                _Querys = await _context.Querys.ToListAsync();
            }
        }
    }
}
