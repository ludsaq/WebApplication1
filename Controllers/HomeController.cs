using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (IsTrueData(user))
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else 
            {
                ModelState.AddModelError("login","Недопустимое значение строки. Проерьте чтобы строка была не больше 20 символов.");
                ModelState.AddModelError("referens", "Недопустимое значение строки. Проерьте чтобы строка была не больше 20 символов.");
                ModelState.AddModelError("description", "Недопустимое значение строки. Проерьте чтобы строка была не больше 300 символов.");
            }

            return View(user);

        }

        #nullable enable
        [HttpPost]
        public async Task<IActionResult> Edit(string? login)
        {
            if (login == null)
            {
                return NotFound();
            }
            else
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.login == login);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string? login)
        {
            db.Users.Find(login);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool IsTrueData(User user) 
        {
            bool isValidData = ModelState.IsValid;
            bool loginLess20 = (user.login.Length < 20);
            bool referensLess20 = (user.referens.Length < 20);
            bool descriptionLess300 = (user.description.Length < 300);
           
            return isValidData && loginLess20 && referensLess20 && descriptionLess300;
        }
    }
}
