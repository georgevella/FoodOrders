using System.Threading.Tasks;
using System.Web.Mvc;
using FoodOrder.DataAccess.Model;
using FoodOrder.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace FoodOrder.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IOwinContext _context;
        private readonly ApplicationUserManager _userManager;

        public AccountController(IOwinContext context, ApplicationUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Logout()
        {
            _context.Authentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var user = await _userManager.FindAsync(viewModel.Email, viewModel.Password);
            if (user != null)
            {
                await SignInAsync(user, viewModel.RememberMe);
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(returnUrl);                
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new NewUserViewModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(NewUserViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var user = new User
            { 
                DisplayName = model.DisplayName,
                PhoneNumber = model.Phone,
                UserName = model.Email, 
                EmailAddress = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
                
            if (result.Succeeded)
            {
                await SignInAsync(user, isPersistent: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Index", "Home");
            }
            
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            _context.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            _context.Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = isPersistent
            }, await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
        }
    }
}

namespace FoodOrder.ViewModels
{
}