using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.ApplicationInsights.WindowsServer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Services;
using Restaurant.Services.Identity;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Controllers
{
    [Authorize]
    [ClaimsAuthorize(ClaimTypes.Role, "Admin")]
    public class AdminController : AsyncController
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: Admin
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var user = new User()
            {
                UserName = viewModel.Login,
                FirstName = viewModel.FirstName,
                SecondName = viewModel.SecondName,
                MiddleName = viewModel.MiddleName,
                Position = viewModel.Position
            };

            IdentityResult result = UserManager.Create(user, viewModel.Password);

            if (result.Succeeded)
                return RedirectToAction("Index");

            AddErrorsFromResult(result);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
                return HttpNotFound();

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index");

            return View("Error");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return RedirectToAction("Index");

            var userViewModel = new UserFormViewModel()
            {
                Id = id,
                Login = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                MiddleName = user.MiddleName,
                Position = user.Position
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserFormViewModel viewModel)
        {
            var user = await UserManager.FindByIdAsync(viewModel.Id);
            if (user != null)
            {
                user.UserName = viewModel.Login;
                user.FirstName = viewModel.FirstName;
                user.SecondName = viewModel.SecondName;
                user.MiddleName = viewModel.MiddleName;

                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(viewModel.Password))
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(viewModel.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(viewModel.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded
                                                                    && !string.IsNullOrEmpty(viewModel.Password) && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(viewModel);
        }
    }
}