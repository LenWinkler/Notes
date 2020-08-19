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
    public class NotesController : Controller
    {

        private readonly ApplicationDbContext _db;

        public NotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Note> AllNotes { get; set; }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            AllNotes = await _db.Notes.ToListAsync();
            return View(AllNotes);
        }
    }
}
