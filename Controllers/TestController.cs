using HealthcareSystem.Data;
using HealthcareSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HealthcareSystem.Controllers
{
    public class TestController : Controller
    {

        private readonly HealthcareDbContext _context;

        public TestController(HealthcareDbContext context)
        {
            _context = context;
        }

        public IActionResult Test(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "test_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var test = _context.Test.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                test = test.Where(s => s.testname.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "test_desc":
                    test = test.OrderByDescending(s => s.testname);
                    break;
                case "price":
                    test = test.OrderByDescending(s => s.testprice);
                    break;
                case "description_desc":
                    test = test.OrderByDescending(s => s.testdescription);
                    break;
                default:
                    test = test.OrderByDescending(s => s.testname);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(test.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }


        //Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .FirstOrDefaultAsync(m => m.testid == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("testid,testname,testprice,testdescription")] Test test)
        {
            if (ModelState.IsValid)
            {
                test.testid = Guid.NewGuid();
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Test));
            }
            return View(test);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("testid,testname,testprice,testdescription")] Test test)
        {
            if (id != test.testid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.testid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Test));
            }
            return View(test);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Test
                .FirstOrDefaultAsync(m => m.testid == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var test = await _context.Test.FindAsync(id);
            _context.Test.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Test));
        }

        private bool TestExists(Guid id)
        {
            return _context.Test.Any(e => e.testid == id);
        }

    }
}
