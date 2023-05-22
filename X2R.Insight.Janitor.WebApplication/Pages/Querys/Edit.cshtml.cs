using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X2R.Insight.Janitor.WebApi.Data;
using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApplication.Pages.Querys
{
    public class EditModel : PageModel
    {
        private readonly X2R.Insight.Janitor.WebApi.Data.QueryContext _context;

        public EditModel(X2R.Insight.Janitor.WebApi.Data.QueryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public _Querys _Querys { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Querys == null)
            {
                return NotFound();
            }

            var _querys =  await _context.Querys.FirstOrDefaultAsync(m => m.TaskId == id);
            if (_querys == null)
            {
                return NotFound();
            }
            _Querys = _querys;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(_Querys).State = EntityState.Modified;

            if(_Querys.RecurringSchedule == null)
            {
                _Querys.RecurringSchedule = Request.Form["RecurringSchedule"].ToString();
                if(_Querys.RecurringSchedule == "1")
                {
                    _Querys.RecurringSchedule = "Once";
                }
                else
                {
                    _Querys.RecurringSchedule = "Multiple";
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_QuerysExists(_Querys.TaskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool _QuerysExists(int id)
        {
          return (_context.Querys?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
