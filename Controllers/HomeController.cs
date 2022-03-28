using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private BowlingDbContext _context { get; set; }

        public HomeController(BowlingDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {

            ViewBag.Teams = _context.teams.ToList();

            var BowlingInfo = _context.Bowlers.ToList();

            return View(BowlingInfo);
        }


        public IActionResult FilteredIndex(int TeamIDNumber)
        {
            ViewBag.Teams = _context.teams.ToList();

            ViewBag.TeamNumber = TeamIDNumber;

            var BowlingInfo = _context.Bowlers.ToList();

            return View(BowlingInfo);
        }


        [HttpGet]
        public IActionResult CreateBowler()
        {
            ViewBag.Teams = _context.teams.ToList();

            var BowlingInfo = _context.Bowlers.ToList();

            ViewBag.lengthIndex = BowlingInfo.Count + 1;

           var x = new Bowler();

            return View(x);
        }

        [HttpPost]
        public IActionResult CreateBowler(Bowler b)
        {

            if (ModelState.IsValid)
            {
                _context.Add(b);
                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Delete(int idNumber)
        {

            var bowler = _context.Bowlers.Single(x => x.BowlerID == idNumber);


            return View(bowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler bowlerDeleted)
        {
            _context.Bowlers.Remove(bowlerDeleted);
            _context.SaveChanges();
                       
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int idNumber)
        {
            ViewBag.Teams = _context.teams.ToList();

            var submission = _context.Bowlers.Single(x => x.BowlerID == idNumber);

            return View("Edit", submission);
        }

        [HttpPost]
        public IActionResult Edit(Bowler changedBowler)
        {
            _context.Update(changedBowler);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
