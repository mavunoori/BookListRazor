using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Book> Books { get; private set; }

        public async Task OnGet()
        {
            Books = await _dbContext.Book.ToListAsync<Book>();
        }

        public async Task<ActionResult> OnPostDelete(int id)
        {
            var book = await _dbContext.Book.FindAsync(id);
            if(book ==null)
            {
                return NotFound();
            }
            _dbContext.Book.Remove(book);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");

        }

    }
}