using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.Controllers
{
    public class NoteController : Controller
    {

        private readonly ApplicationDbContext _db;

        public NoteController(ApplicationDbContext db)
        {
            _db = db;
        }

        public Note Note { get; set; }

        // GET: /<controller>/
        public async Task<IActionResult> ViewNote(int id)
        {
            Note = await _db.Notes.FirstOrDefaultAsync(u => u.Id == id);
            if (Note == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}
