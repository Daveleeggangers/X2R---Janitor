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
    public class DetailsModel : PageModel
    {
        private readonly X2R.Insight.Janitor.WebApi.Data.QueryContext _context;

        public DetailsModel(X2R.Insight.Janitor.WebApi.Data.QueryContext context)
        {
            _context = context;
        }

      public _Querys _Querys { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Querys == null)
            {
                return NotFound();
            }

            var _querys = await _context.Querys.FirstOrDefaultAsync(m => m.TaskId == id);
            if (_querys == null)
            {
                return NotFound();
            }
            else 
            {
                _Querys = _querys;
            }
            return Page();
        }
    }
}
