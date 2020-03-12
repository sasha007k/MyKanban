using System.Threading.Tasks;
using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyKanban.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userservice;

        public UserController(IUserService userservice)
        {
            this.userservice = userservice;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            ProfileModel profile = await this.userservice.GetUserInfo(this.User.Identity.Name).ConfigureAwait(true);
            return this.View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileModel model)
        {
            await this.userservice.UpdateUser(model).ConfigureAwait(true);

            return this.RedirectToAction("Profile", "User");
        }
    }
}