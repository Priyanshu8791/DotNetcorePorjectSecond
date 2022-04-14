using DotNetcorePorjectSecond.DB_Context;
using DotNetcorePorjectSecond.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetcorePorjectSecond.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<PersonModels> mod = new List<PersonModels>();
            AnujContext ent = new AnujContext();
            var res = ent.PersonDetails.ToList();
            foreach (var item in res)
            {
                mod.Add(new PersonModels
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    City = item.City
                });
            }

            return View(mod);
        }
        [HttpGet]
        public IActionResult ADD_Person()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ADD_Person(PersonModels mod)
        {
            AnujContext ent = new AnujContext();
            PersonDetail tbl = new PersonDetail();
            tbl.Id = mod.Id;
            tbl.Name = mod.Name;
            tbl.Email = mod.Email;
            tbl.City = mod.City;
            if (mod.Id == 0)
            {
                ent.PersonDetails.Add(tbl);
                ent.SaveChanges();
            }
            else
            {
                ent.Entry(tbl).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ent.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit(int id)
        {
            PersonModels mod = new PersonModels();
            AnujContext ent = new AnujContext();
            var edt = ent.PersonDetails.Where(m => m.Id == id).First();
            mod.Id = edt.Id;
            mod.Name = edt.Name;
            mod.Email = edt.Email;
            mod.City = edt.City;
            return View("ADD_Person", mod);

        }
        public IActionResult Delete(int id)
        {
            AnujContext ent = new AnujContext();
            var det = ent.PersonDetails.Where(m => m.Id == id).First();
            ent.PersonDetails.Remove(det);
            ent.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
