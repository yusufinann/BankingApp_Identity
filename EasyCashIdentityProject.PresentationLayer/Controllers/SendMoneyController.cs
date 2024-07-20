using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DtoLayer.Dtos.CustomerAccountProcessDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class SendMoneyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerAccountProcessService _customerAccountProcessService;

        public SendMoneyController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
        }

        [HttpGet]
        public IActionResult Index(string mycurrency)
        {
            ViewBag.currency = mycurrency;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SendMoneyForCustomerAccountProcessDto sendMoneyForCustomerAccountProcessDto)
        {
            var context = new Context();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var receiverAccount = context.CustomerAccounts.FirstOrDefault(x => x.CustomerAccountNumber == sendMoneyForCustomerAccountProcessDto.ReceiverAccountNumber);
            var senderAccount = context.CustomerAccounts.FirstOrDefault(x => x.AppUserID == user.Id && x.CustomerAccountCurrency == "Türk Lirası");

            //Aynı zamanda birimleri de kontrol edelim.yani tl den tl ye dolardan dolara gönderim
            if (receiverAccount == null || senderAccount == null)
            {
                ModelState.AddModelError("", "Gönderici veya alıcı hesap bulunamadı.");
                return View();
            }

            if (receiverAccount.CustomerAccountCurrency != senderAccount.CustomerAccountCurrency)
            {
                ModelState.AddModelError("", "Farklı para birimlerinde olan hesaplar arasında para transferi yapılamaz.");
                return View();
            }

            var values = new CustomerAccountProcess
            {
                ProcessDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                SenderID = senderAccount.CustomerAccountID,
                ProcessType = "Havale",
                ReceiverID = receiverAccount.CustomerAccountID,
                Amount = sendMoneyForCustomerAccountProcessDto.Amount,
                Description = sendMoneyForCustomerAccountProcessDto.Description
            };

            _customerAccountProcessService.TInsert(values);

            return RedirectToAction("Index", "Deneme");
        }
    }
}
