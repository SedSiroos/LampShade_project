using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x=>x.UserName==command.UserName || x.Mobile==command.Mobile ))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var path = $"ProfilePhotos";
            var pictureName = _fileUploader.Upload(command.ProfilePhoto, path);
            var password = _passwordHasher.Hash(command.Password);

            var addAccount = new Account(command.FullName,command.UserName, command.Email, command.Mobile,
                password, pictureName, command.RoleId);
            _accountRepository.Create(addAccount);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_accountRepository.Exists(x =>
               (x.UserName == command.UserName || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var path = $"ProfilePhotos";
            var pictureName = _fileUploader.Upload(command.ProfilePhoto,path);

            account.Edit(command.FullName, command.UserName, command.Email, command.Mobile, pictureName, command.RoleId);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePasswordAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessage.PasswordNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.GetByUserName(command.UserName);
            if (account == null)
                return operation.Failed(ApplicationMessage.UserNotExist);

            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessage.PasswordNotFound);

            var permission = _roleRepository.Get(account.RoleId)
                .Permissions.Select(x => x.Code).ToList();

            var authViewModel = new AuthViewModel(account.Id,account.RoleId,account.FullName,account.UserName,
                account.Email,account.Mobile,permission);
            _authHelper.Signin(authViewModel);

            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
