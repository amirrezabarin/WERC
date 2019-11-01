using Model;
using Model.ApplicationDomainModels;
using Model.ViewModels.User;
using Repository.EF.Repository;
using System.Collections.Generic;
using BLL.Base;
using System.Linq;
using System;
using Model.ViewModels.Admin;

namespace BLL
{
    public class BLUser : BLBase
    {
        public VmUserList GetAllUsers()
        {
            var userRepository = UnitOfWork.GetRepository<UserRepository>();
            var aspNetUserList = userRepository.GetAllUsers();
            var userList = from user in aspNetUserList
                           orderby user.Email
                           select new VmUserFullInfo
                           {
                               UserName = user.UserName,
                               Email = user.Email,
                               Roles = from role in user.AspNetRoles select role.Name,
                               RegisterDate = user.RegisterDate.Value,
                           };

            var vmUserList = new VmUserList
            {
                Users = userList.ToArray()
            };

            return vmUserList;
        }
        public VmUserList GetUserByFiler(string searchText)
        {
            var userRepository = UnitOfWork.GetRepository<UserRepository>();
            var aspNetUserList = userRepository.GetUserByFiler(searchText);
            var userList = from user in aspNetUserList
                           orderby user.Email
                           select new VmUserFullInfo
                           {
                               Id = user.Id,
                               UserName = user.UserName,
                               Email = user.Email,
                               Roles = from role in user.AspNetRoles select role.Name,
                               RegisterDate = user.RegisterDate.Value,
                           };

            var vmUserList = new VmUserList
            {
                Users = userList.ToArray()
            };

            return vmUserList;
        }
        public IEnumerable<SmUserRoles> GetAllUserRoles()
        {
            var viewUserRoleRepository = UnitOfWork.GetRepository<ViewUserRoleRepository>();

            var userRoleList = viewUserRoleRepository.GetAllUserRoles();

            var smUserRoleList = from userRoles in userRoleList
                                 select new SmUserRoles
                                 {
                                     Id = userRoles.Id,
                                     UserId = userRoles.UserId,
                                     UserName = userRoles.UserName,
                                     RoleName = userRoles.RoleName,
                                 };

            return smUserRoleList;
        }

        public IEnumerable<SmUserRoles> GetUsersByRole(string roleId)
        {
            var viewUserRoleRepository = UnitOfWork.GetRepository<ViewUserRoleRepository>();

            var userRoleList = viewUserRoleRepository.GetUsersByRole(roleId);

            var smUserRoleList = from userRoles in userRoleList
                                 select new SmUserRoles
                                 {
                                     Id = userRoles.Id,
                                     UserId = userRoles.UserId,
                                     UserName = userRoles.UserName,
                                     RoleName = userRoles.RoleName,
                                 };

            return smUserRoleList;

        }

        public IEnumerable<SmUserRoles> GetUsersByRoleName(string roleName)
        {
            var viewUserRoleRepository = UnitOfWork.GetRepository<ViewUserRoleRepository>();

            var userRoleList = viewUserRoleRepository.GetUsersByRoleName(roleName);

            var smUserRoleList = from userRoles in userRoleList
                                 select new SmUserRoles
                                 {
                                     Id = userRoles.Id,
                                     UserId = userRoles.UserId,
                                     UserName = userRoles.UserName,
                                     RoleName = userRoles.RoleName,
                                     Email = userRoles.Email,
                                 };

            return smUserRoleList;

        }
        public void UpdatePhoneUserNumber(string userId, string phoneNumber)
        {
            var userRepository = UnitOfWork.GetRepository<UserRepository>();
            userRepository.UpdatePhoneUserNumber(userId, phoneNumber);
            UnitOfWork.Commit();
        }

    }
}
