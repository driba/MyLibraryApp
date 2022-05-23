using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ispit.Books.Data;
using Ispit.Books.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ispit.Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Book
        public IActionResult Index(string? msg)
        {
            ViewBag.Msg = msg;

            var books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.User)
                .ToList();
            return View(books);
        }

        // GET: Admin/Book/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.User)
                .FirstOrDefault(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Admin/Book/Create
        public IActionResult Create(string? msg)
        {
            ViewBag.Msg = msg;

            ViewBag.Users = _context.Users.ToList();
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();
            
            return View();
        }

        // POST: Admin/Book/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {            
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == book.AuthorId);
            var publisher = _context.Publishers.FirstOrDefault(p => p.PublisherId == book.PublisherId);
            if (author == null || publisher == null)
            {
                return Create("Došlo je do greške! Ponovite upis...");
            }

            book.User = _context.Users.FirstOrDefault(u => u.Id == book.UserId);
            book.Author = author;
            book.Publisher = publisher;


            if (_context.Books.Find(book.BookId) == null)
            {
                _context.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { msg = "Zapis uspješno kreiran!" });
            }

            

            return View(book);
        }

        // GET: Admin/Book/Edit/5
        public IActionResult Edit(int? id, string? msg)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.User)
                .FirstOrDefault(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Users = _context.Users.ToList();
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();
            return View(book);
        }

        // POST: Admin/Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            book.User = _context.Users.FirstOrDefault(u => u.Id == book.UserId);

            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == book.AuthorId);
            var publisher = _context.Publishers.FirstOrDefault(p => p.PublisherId == book.PublisherId);
            if (author == null || publisher == null)
            {
                return View(book);
            }
           
            _context.Update(book);
            _context.SaveChanges();
                   
            return RedirectToAction(nameof(Index), new { msg = "Ažuriranje uspješno!"});
        
        }

        // GET: Admin/Book/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.User)
                .FirstOrDefault(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Admin/Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), new { msg = "Zapis izbrisan!"});
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
