using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using X2R.Insight.Janitor.WebApi.Data;
using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApplication.Pages.Querys
{
    public class CreateModel : PageModel
    {
        private readonly X2R.Insight.Janitor.WebApi.Data.QueryContext _context;

        public CreateModel(X2R.Insight.Janitor.WebApi.Data.QueryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public _Querys _Querys { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Querys == null || _Querys == null)
            {
                return Page();
            }

            if (_Querys.RecurringSchedule == null)
            {
                _Querys.RecurringSchedule = Request.Form["RecurringSchedule"].ToString();
                if (_Querys.RecurringSchedule == "1")
                {
                    _Querys.RecurringSchedule = "Once";
                }
                else
                {
                    _Querys.RecurringSchedule = "Multiple";
                }
            }

            _context.Querys.Add(_Querys);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
