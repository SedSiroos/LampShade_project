using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [TempData] public string LoginMessage { get; set; }
        [TempData] public string RegisterMessage { get; set; }

        private readonly IAccountApplication _accountApplication;
        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login entity)
        {
            var result = _accountApplication.Login(entity);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            result.Message = LoginMessage;
            return RedirectToPage("./Account");
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostRegister(RegisterAccount entity)
        {
            var result=_accountApplication.Register(entity);
            if (result.IsSucceeded)
                return RedirectToPage("./Account");

            RegisterMessage = result.Message;
            return RedirectToPage("./Account");
        }
    }
}
