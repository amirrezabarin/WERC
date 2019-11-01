﻿using Model;
using Repository.EF.Base;
using System.Collections.Generic;
using System.Linq;

namespace Repository.EF.Repository
{
    public class UserRepository : EFBaseRepository<AspNetUser>
    {
        public IEnumerable<AspNetUser> GetAllUsers()
        {

            var aspNetUserList = from user in Context.AspNetUsers
                                 orderby user.Email
                                 select user;

            return aspNetUserList.ToArray();

        }
        public AspNetUser GetUserById(string userId)
        {

            var aspNetUser = Context.AspNetUsers.Find(userId);

            return aspNetUser;

        }
        public IEnumerable<AspNetUser> GetUserByFiler(string searchText)
        {

            var aspNetUserList = from user in Context.AspNetUsers
                                 where
                                    user.UserName.Contains(searchText) ||
                                    user.Email.Contains(searchText)
                                 orderby user.Email
                                 select user;

            return aspNetUserList.ToArray();

        }
        public void UpdatePhoneUserNumber(string userId, string phoneNumber)
        {

            var aspNetUser = Context.AspNetUsers.Find(userId);
            aspNetUser.PhoneNumber = phoneNumber;

        }
    }
}
