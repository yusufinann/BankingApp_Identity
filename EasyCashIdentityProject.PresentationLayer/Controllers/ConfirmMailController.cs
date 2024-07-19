using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class ConfirmMailController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
		public IActionResult Index()
		{
			var value = TempData["Mail"];
            ViewBag.v=value;
            //  confirmMailViewModel.Mail = value.ToString();
            return View();
		}

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {
            var users = await _userManager.Users.Where(u => u.Email == confirmMailViewModel.Mail).ToListAsync();
            if (users.Count > 1)
            {
                // Aynı e-posta adresine sahip birden fazla kullanıcı bulundu.
                TempData["ErrorMessage"] = "Birden fazla kullanıcı aynı e-posta adresine sahip.";
                return RedirectToAction("Index");
            }

            var user = users.SingleOrDefault();
            if (user == null)
            {
                // Kullanıcı bulunamadı.
                TempData["ErrorMessage"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index");
            }

            if (user.ConfirmCode == confirmMailViewModel.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Login");
            }

            TempData["ErrorMessage"] = "Doğrulama kodu geçersiz.";
            return RedirectToAction("Index");
        }

    //    var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);
    //        if (user.ConfirmCode == confirmMailViewModel.ConfirmCode)
    //        {
    //            user.EmailConfirmed = true;
    //            await _userManager.UpdateAsync(user);
    //            return RedirectToAction("Index", "Login");
    //}
    //        return View();
}
}
