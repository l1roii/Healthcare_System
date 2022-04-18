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
    public class AppointmentController : Controller
    {

        private readonly HealthcareDbContext _context;

        public AppointmentController(HealthcareDbContext context)
        {
            _context = context;
        }

        public IActionResult Appointment(string sortOrder, string currentFilter, string searchString, int? page)
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

            var appointment = _context.Appointment.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                appointment = appointment.Where(s => s.appointmentdate.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "date_desc":
                    appointment = appointment.OrderByDescending(s => s.appointmentdate);
                    break;
                case "username":
                    appointment = appointment.OrderByDescending(s => s.username);
                    break;
                case "userlastname":
                    appointment = appointment.OrderByDescending(s => s.userlastname);
                    break;
                case "useremail":
                    appointment = appointment.OrderByDescending(s => s.useremail);
                    break;
                case "department":
                    appointment = appointment.OrderByDescending(s => s.department);
                    break;
                case "deoctor":
                    appointment = appointment.OrderByDescending(s => s.doctor);
                    break;
                case "textmessage":
                    appointment = appointment.OrderByDescending(s => s.textmessage);
                    break;
                default:
                    appointment = appointment.OrderByDescending(s => s.appointmentdate);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(appointment.ToPagedList(pageNumber, pageSize));
        }


        //Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.appointmentid == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("appointmentid,username,userlastname,userphone,useremail,appointmentdate,textmessage,department,doctor")] Appointment appointment)
        {
            if (id != appointment.appointmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.appointmentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Appointment));
            }
            return View(appointment);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.appointmentid == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Appointment));
        }

        private bool AppointmentExists(Guid id)
        {
            return _context.Appointment.Any(e => e.appointmentid == id);
        }

    }
}
