using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StringFiltering.Data;

namespace StringFiltering.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var contacts = await context.Contacts.ToListAsync();

            // Like Queries
            // Allows usages of wildcards that were not possible using string function translation 
            // that was previously the only option.
            var likeQuery = await context.Contacts
                .Where(c => EF.Functions.Like(c.LastName, "S_it%"))
                .ToListAsync();

            // Greater Than/Less Than
            // It is important to not try and use any of the overloads of String.Compare, 
            // or you will end up with client-side evaluation of your query. 
            var stringCompareQuery = await context.Contacts
                // ReSharper disable once StringCompareIsCultureSpecific.1
                .Where(c => String.Compare(c.FirstName, "D") > 0)
                .ToListAsync();

            // SELECT[c].[Id], [c].[Address], [c].[City], [c].[FirstName], [c].[LastName], [c].[State], [c].[Zip]
            // FROM[Contacts] AS[c]
            // WHERE[c].[FirstName] > N'D'

            return Page();
        }
    }
}