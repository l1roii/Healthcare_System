using HealthcareSystem.Models;
using HealthcareSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using X.PagedList;


namespace HealthcareSystem.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly HealthcareDbContext  _context;

        public DepartmentController(HealthcareDbContext context)
        {
            _context = context;
        }

        // GET: Department
        public IActionResult Department(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var department = _context.Department.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                department = department.Where(s => s.departmentname.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_desc":
                    department = department.OrderByDescending(s => s.departmentname);
                    break;
                case "Date":
                    department = department.OrderByDescending(s => s.departmentrooms);
                    break;
                case "date_desc":
                    department = department.OrderByDescending(s => s.departmentdescription);
                    break;
                default:
                    department = department.OrderByDescending(s => s.departmentname);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(department.ToPagedList(pageNumber, pageSize));
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("departmentid,departmentname,departmentrooms,departmentdescription")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.departamentid = Guid.NewGuid();
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Department));
            }
            return View(department);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.departamentid == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("departamentid,departmentname,departmentrooms,departmentdescription")] Department department)
        {
            if (id != department.departamentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.departamentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Department));
            }
            return View(department);
        }


        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.departamentid == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var department = await _context.Department.FindAsync(id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Department));
        }


        private bool DepartmentExists(Guid id)
        {
            return _context.Department.Any(e => e.departamentid == id);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        public IPagedList<Department> MyDepartment { get; set; }

    }
}
