using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pagae.Data;
using Pagae.Models;
using Pagae.Models.HomeViewModels;
using Pagae.Models.PunishmentViewModels;
using Pagae.Models.VoteViewModels;
using System.Linq;
using System;

namespace Pagae.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public VoteController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var punishments = _context.Punishment
                .Where(x => x.Status == PunishmentStatus.Voting)
                .Where(x => x.Date.AddDays(2).Date > DateTime.Now.Date)
                .ToList();
            Punishment punishmentToVote = null;
            foreach(var p in punishments)
            {
                var v = _context.Vote.Where(x => x.PunishmentId == p.Id).Where(x => x.UserId == user.Id).FirstOrDefault();
                if(v == null)
                {
                    punishmentToVote = p;
                    break;
                }
            }

            if(punishmentToVote == null)
            {
                TempData["Message"] = $"Nothing to vote...";
                return RedirectToAction(nameof(Index), "Home");
            }

            var punished = _userManager.FindByIdAsync(punishmentToVote.PunishedId).Result;
            var vote = new VoteViewModel()
            {
                PunishmentId = punishmentToVote.Id,
                Question = $"Would you like to punish {punished.Name} because {punishmentToVote.Reason}?",
                InFavor = true
            };

            return View(vote);
        }

        [HttpPost]
        public IActionResult Index(VoteViewModel model)
        {
            var punishment = _context.Punishment.Where(x => x.Id == model.PunishmentId).FirstOrDefault();
            var user = _userManager.GetUserAsync(User).Result;
            var vote = new Vote()
            {
                Punishment = punishment,
                User = user,
                InFavor = model.InFavor
            };
            _context.Vote.Add(vote);
            _context.SaveChanges();

            var allVotes = _context.Vote.Where(x => x.PunishmentId == model.PunishmentId);
            var positiveVotes = allVotes.Where(x => x.InFavor == true).Count();
            var negativeVotes = allVotes.Where(x => x.InFavor == false).Count();

            var usersCount = _userManager.Users.Count();
            if(positiveVotes > usersCount / 2)
            {
                user.Credits = user.Credits - 1;
                var updateCurrentUser = _userManager.UpdateAsync(user);
                updateCurrentUser.Wait();

                punishment.Status = PunishmentStatus.Approved;
                _context.SaveChanges();
            }
            if(negativeVotes > usersCount / 2)
            {
                punishment.Status = PunishmentStatus.Rejected;
                _context.SaveChanges();
            }

            TempData["Message"] = "Thanks! We received your vote!";
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}