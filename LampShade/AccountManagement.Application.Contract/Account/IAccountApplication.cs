using _0_Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contract.Account
{
    public interface  IAccountApplication
    {
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePasswordAccount command);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        EditAccount GetDetails(long id);
        OperationResult Login(Login command);
        void Logout();
    }
}
