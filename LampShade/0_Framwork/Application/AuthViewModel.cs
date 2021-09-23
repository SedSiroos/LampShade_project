using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public AuthViewModel(){}


        public long AccountId { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<int> Permission { get; set; }   

        public AuthViewModel(long accountId, long roleId, string fullName, string userName, string email,
            string mobile,List<int> permission)
        {
            AccountId = accountId;
            RoleId = roleId;
            FullName = fullName;
            UserName = userName;
            Email = email;
            Mobile = mobile;
            Permission = permission;
        }
    }
}