using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pagae.Models;
using Pagae.Models.HomeViewModels;
using System.Linq;

namespace Pagae.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.OrderBy(x => x.Credits).ToList());
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
