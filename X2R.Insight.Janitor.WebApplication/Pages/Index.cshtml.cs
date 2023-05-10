using Microsoft.AspNetCore.Mvc.RazorPages;
using X2R.Insight.Janitor.WebApi.Controllers;

namespace X2R.Insight.Janitor.WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }
    }
}