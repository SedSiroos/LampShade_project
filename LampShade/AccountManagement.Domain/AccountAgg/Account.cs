﻿using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string FullName { get; private set; }                        
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string ProfilePhoto { get; private set; }
        public long RoleId { get; private set; }
        public Role Roles { get; private set; }

        public Account(string fullName, string userName, string email, string mobile, string password, string profilePhoto, long roleId)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            Mobile = mobile;
            Password = password;
            ProfilePhoto = profilePhoto;
            RoleId = roleId;

            if (roleId == 0)
                RoleId = 2;
        }

        public void Edit (string fullName, string userName, string email, string mobile,
            string profilePhoto, long roleId)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            Mobile = mobile;
             
            if(!string.IsNullOrWhiteSpace(profilePhoto))
                 ProfilePhoto = profilePhoto;

            RoleId = roleId;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
