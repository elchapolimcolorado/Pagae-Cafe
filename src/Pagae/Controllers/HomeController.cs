using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pagae.Data;
using Pagae.Models;
using Pagae.Models.HomeViewModels;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pagae.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            //Verifica se existe alguma punição em votação
            var punishments = _context.Punishment
                .Where(x => x.Status == PunishmentStatus.Voting)
                .Where(x => x.Date.AddDays(3).Date > DateTime.Now.Date)
                .ToList();
            Punishment punishmentToVote = null;
            foreach(var p in punishments)
            {
                var v = _context.Vote.Where(x => x.PunishmentId == p.Id).Where(x => x.UserId == _userManager.GetUserId(User)).FirstOrDefault();
                if(v == null)
                {
                    punishmentToVote = p;
                    break;
                }
            }
            if(punishmentToVote != null)
            {
                return RedirectToAction(nameof(Index), "Vote");
            }

            var ApprovedPunishments = _context.Punishment
                .Where(x => x.Status == PunishmentStatus.Approved)
                .Where(x => x.Date.AddDays(20).Date >= DateTime.Now.Date)
                .ToList();
            var punishmentMessages = new List<string>();
            foreach(var p in ApprovedPunishments){
                var punished = _userManager.FindByIdAsync(p.PunishedId).Result;
                punishmentMessages.Add($"{punished.Name} was punished by 1 credit because {p.Reason}");
            }
            TempData["PunishmentMessages"] = punishmentMessages;
            return View(_userManager.Users.OrderBy(x =>  x.Credits).ThenBy(y => y.LastUpdateDate).ToList());
        }

        public IActionResult Pay()
        {
            var model = new PayViewModel();

            foreach (var user in _userManager.Users.Where(x => x.Id != _userManager.GetUserId(User)))
            {
                model.Users.Add(new UserViewModel() { Id = user.Id, Name = user.Name, Selected = true });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Pay(PayViewModel model)
        {
            var selectedUsersIds = model.Users.Where(x => x.Selected).Select(x => x.Id).ToList();
            foreach (var selectedUserId in selectedUsersIds)
            {
                var user = _userManager.FindByIdAsync(selectedUserId).Result;
                user.Credits--;
                var update = _userManager.UpdateAsync(user);
                update.Wait();
            }
            var get = _userManager.GetUserAsync(User);
            get.Wait();
            var currentUser = get.Result;
            currentUser.Credits = currentUser.Credits + selectedUsersIds.Count;
            currentUser.LastUpdateDate = DateTime.Now;
            var updateCurrentUser = _userManager.UpdateAsync(currentUser);
            updateCurrentUser.Wait();

            TempData["Message"] = $"Hooray! You won {selectedUsersIds.Count} credit(s)!";

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult PaySomeoneElse()
        {
            var model = new PaySomeoneElseViewModel();

            foreach (var user in _userManager.Users)
            {
                model.Users.Add(new UserViewModel() { Id = user.Id, Name = user.Name, Selected = true });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult PaySomeoneElse(PaySomeoneElseViewModel model)
        {
            var selectedUsersIds = model.Users.Where(x => x.Id != model.PayerId).Where(x => x.Selected).Select(x => x.Id).ToList();
            foreach (var selectedUserId in selectedUsersIds)
            {
                var user = _userManager.FindByIdAsync(selectedUserId).Result;
                user.Credits--;
                var update = _userManager.UpdateAsync(user);
                update.Wait();
            }
            var get = _userManager.FindByIdAsync(model.PayerId);
            get.Wait();
            var currentUser = get.Result;
            currentUser.Credits = currentUser.Credits + selectedUsersIds.Count;
            currentUser.LastUpdateDate = DateTime.Now;
            var updateCurrentUser = _userManager.UpdateAsync(currentUser);
            updateCurrentUser.Wait();

            TempData["Message"] = $"Hooray! {currentUser.Name} wons {selectedUsersIds.Count} credit(s)!";

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
