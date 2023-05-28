using EBeats.Models;
using EBeats.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBeats.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        public RoleManager<IdentityRole> RoleManager;
        public IEnumerable<IdentityRole> Roles { get; set; }


        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;

        }


        public IActionResult Index()
        {
            var users = UserManager.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                Firstname=user.Firstname,
                Lastname=user.Lastname,
                Birthday=user.Birthday,
                Email = user.Email,
                
                Role = string.Join(",", UserManager.GetRolesAsync(user).Result.ToArray())
            }).ToList();
            return View(users);
        }
       
        [HttpGet]
        public async Task<ActionResult> Create(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {

                return BadRequest("User nuk gjendet");
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email
            };

            var roles = RoleManager.Roles;
            ViewBag.Roles = new SelectList(roles.ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserRole(UserViewModel u)
        {
            var name = Convert.ToString(await RoleManager.FindByIdAsync(u.Role));
            var user = await UserManager.FindByEmailAsync(u.Email);

            if (user == null)
            {
                return BadRequest("User nuk ekziston" + user);
            }
            var currentRoles = await UserManager.GetRolesAsync(user);
            await UserManager.RemoveFromRolesAsync(user, currentRoles);  

            
            await UserManager.AddToRoleAsync(user, name);

            return RedirectToAction("Index");

        }



        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                await UserManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ShowEditAsync(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {

                return BadRequest("User nuk gjendet");
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthday = user.Birthday,
                Email = user.Email,

            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);

            if (user == null)
            {

                return BadRequest("User nuk gjendet" + model.Id);
            }
            else
            {
                user.Email = model.Email;
                user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.Birthday = model.Birthday;

                var result = await UserManager.UpdateAsync(user);

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
    }
}
