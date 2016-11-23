using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pagae.Data;
using Pagae.Models;
using Pagae.Models.HomeViewModels;
using Pagae.Models.PunishmentViewModels;
using System.Linq;
using System;

namespace Pagae.Controllers
{
    [Authorize]
    public class PunishmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PunishmentController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(PunishmentViewModel model)
        {
            var daniel =  _userManager.FindByNameAsync("dbarros86.db@gmail.com").Result;

            var punishment = new Punishment()
            {
                Punished = daniel,
                Reason = model.Reason,
                Status = PunishmentStatus.Voting,
                Date = DateTime.Now
            };

            _context.Punishment.Add(punishment);
            _context.SaveChanges();

            var user = _userManager.GetUserAsync(User).Result;
            var vote = new Vote()
            {
                Punishment = punishment,
                User = user,
                InFavor = true
            };
            _context.Vote.Add(vote);
            _context.SaveChanges();

            TempData["Message"] = $"Great! Let's vote!";
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult SomeoneElse()
        {
            var model = new PunishmentViewModel();
            foreach (var user in _userManager.Users.Where(x => x.Id != _userManager.GetUserId(User)))
            {
                model.Users.Add(new UserViewModel() { Id = user.Id, Name = user.Name, Selected = true });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult SomeoneElse(PunishmentViewModel model)
        {
            var punished = _userManager.FindByIdAsync(model.PunishedId).Result;

            var punishment = new Punishment()
            {
                Punished = punished,
                Reason = model.Reason,
                Status = PunishmentStatus.Voting,
                Date = DateTime.Now
            };

            _context.Punishment.Add(punishment);
            _context.SaveChanges();

            var user = _userManager.GetUserAsync(User).Result;
            var vote = new Vote()
            {
                Punishment = punishment,
                User = user,
                InFavor = true
            };
            _context.Vote.Add(vote);
            _context.SaveChanges();

            TempData["Message"] = "Thanks! We received your vote!";
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}