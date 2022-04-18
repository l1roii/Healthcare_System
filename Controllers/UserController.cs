using HealthcareSystem.Data;
using HealthcareSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HealthcareSystem.Areas.Identity.Data;
using System.Security.Claims;

namespace HealthcareSystem.Controllers
{
    public class UserController : Controller
    {


        private readonly HealthcareDbContext _context;

        public UserController(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> User()
        {
            return View(await _context.Users.ToListAsync());

        }

    }
}
