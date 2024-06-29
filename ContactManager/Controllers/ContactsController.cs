using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10; // Set the number of items per page
            var contacts = await _context.Contacts.ToPagedListAsync(pageNumber, pageSize);
            return View(contacts);
        }

        public IActionResult Create()
        {
            return PartialView("_Create", new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                //bool emailExists = await _context.Contacts.AnyAsync(c => c.Email == contact.Email);

                //if (emailExists)
                //{
                //    return Json(new { success = false, message = "This email already exists." });
                //}

                try
                {
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return Json(new { success = false, message = "An error occurred while saving data." });
                }
            }

            return PartialView("_Create", contact);
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicate(string email)
        {
            bool emailExists = await _context.Contacts.AnyAsync(c => c.Email == email);
            return Json(new { emailExists });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return PartialView("_Delete", contact);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return PartialView("_Details", contact);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_Edit", contact);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
