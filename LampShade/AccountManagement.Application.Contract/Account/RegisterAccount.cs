using System.Collections.Generic;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contract.Account
{
    public class RegisterAccount
    {
        public string FullName { get;  set; }
        public string UserName { get;  set; }
        public string Email { get;  set; }
        public string Mobile { get;  set; }
        public string Password { get;  set; }
        public IFormFile ProfilePhoto { get; set; }
        public long RoleId { get;  set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
