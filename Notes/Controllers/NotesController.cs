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

        [BindProperty]
        public Note ControllerNote { get; set; }

        public NotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Note> AllNotes { get; set; }

        public Note SingleNote { get; set; }

        public async Task<IActionResult> Index()
        {
            AllNotes = await _db.Notes.ToListAsync();
            return View(AllNotes);
        }

        public IActionResult Note(int id)
        {
            SingleNote = _db.Notes.FirstOrDefault(u => u.Id == id);
            if (SingleNote == null)
            {
                return NotFound();
            }
            return View(SingleNote);
        }

        public IActionResult Upsert(int? id)
        {
            ControllerNote = new Note();
            if (id == null)
            {
                // create
                return View(ControllerNote);
            }
            //update
            ControllerNote = _db.Notes.FirstOrDefault(u => u.Id == id);
            if (ControllerNote == null)
            {
                return NotFound();
            }
            return View(ControllerNote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (ControllerNote.Id == 0)
                {
                    // create
                    _db.Notes.Add(ControllerNote);
                }
                else
                {
                    _db.Notes.Update(ControllerNote);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ControllerNote);
        }

    }
}
