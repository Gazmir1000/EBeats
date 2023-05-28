using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EBeats.Models;
using EBeats.Models.ViewModels;

namespace EBeats.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ProfileController(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = usermanager;
            _signInManager = signInManager;

        }
       
        public IActionResult Index()
        {
            var id = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(id).Result;
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            return View(user);

        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);


            return RedirectToPage("/Account/Login", new { area = "Identity" });

        }

        public IActionResult Edit(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {

                return BadRequest("User nuk gjendet");
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.Firstname,
                Birthday = user.Birthday,
                Lastname = user.Lastname,

            };

            return View(model);
        }

        public async Task<IActionResult> EditConfirmed(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {

                return BadRequest("User nuk gjendet" + model.Id);
            }
            else
            {
                user.Email = model.Email;
                user.Firstname = model.Firstname;
                user.Birthday = model.Birthday;
                user.Lastname = model.Lastname;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);

            }
        }

        public IActionResult ShowPasswordForm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var id = _userManager.GetUserId(HttpContext.User);
                var user = _userManager.FindByIdAsync(id).Result;
                if (user == null)
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }


                var result = await _userManager.ChangePasswordAsync(user,
                    model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Index");

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View("ShowPasswordForm", model);
        }

    }
}
