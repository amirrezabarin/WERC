﻿using System.Collections.Generic;
using Repository.EF.Base;
using System.Linq;
using Model;
using System;
using Model.ViewModels;
using DAL;
using Model.ViewModels.User;
using Model.ApplicationDomainModels;

namespace Repository.EF.Repository
{
    public class ViewUserRoleRepository : EFBaseRepository<ViewUserRole>/*, IcountryRepository*/
    {
        public IEnumerable<ViewUserRole> GetAllUserRoles()
        {

            var userRoleList = from userRole in Context.ViewUserRoles
                               select userRole;

            return userRoleList.ToArray();
        }

        public IEnumerable<ViewUserRole> GetUsersByRole(string roleId)
        {
            var userRoleList = from userRole in Context.ViewUserRoles
                               where userRole.Id == roleId
                               select userRole;

            return userRoleList.ToArray();
        }
        public IEnumerable<ViewUserRole> GetUsersByRoleName(string roleName)
        {
            var userRoleList = from userRole in Context.ViewUserRoles
                               where userRole.RoleName == roleName
                               select userRole;

            return userRoleList.ToArray();
        }
        public IEnumerable<ViewUserRole> GetUsersById(string userId)
        {
            var userRoleList = from userRole in Context.ViewUserRoles
                               where userRole.UserId == userId
                               select userRole;

            return userRoleList.ToArray();
        }

    }
}
