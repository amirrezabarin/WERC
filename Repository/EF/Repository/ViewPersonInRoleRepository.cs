using Model;
using Model.ViewModels.Admin;
using Model.ViewModels.Person;
using Repository.EF.Base;
using System.Collections.Generic;
using System.Linq;

namespace Repository.EF.Repository
{
    public class ViewPersonInRoleRepository : EFBaseRepository<ViewPersonInRoleRepository>/*, IcountryRepository*/
    {
        public IEnumerable<ViewPersonInRole> GetAllpersons()
        {

            var personInRoleList = from person in Context.ViewPersonInRoles
                                   select person;

            return personInRoleList.ToArray();
        }

        public IEnumerable<ViewPersonInRole> GetUsersByRole(string roleId)
        {
            var personInRoleList = from person in Context.ViewPersonInRoles
                                   where person.UserId == roleId
                                   select person;

            return personInRoleList.ToArray();
        }
        public IEnumerable<ViewPersonInRole> GetUsersByRoleNames(string[] roleNames)
        {
            var personInRoleList = from person in Context.ViewPersonInRoles
                                   where roleNames.Contains(person.RoleName)
                                   select person;

            return personInRoleList.ToArray();
        }
        public ViewPersonInRole GetUsersById(string userId)
        {
            var personInRole = from person in Context.ViewPersonInRoles
                               where person.UserId == userId
                               select person;

            return personInRole.FirstOrDefault();
        }
        public string[] GetEmailsByUserIds(string[] userIds)
        {
            var personInRole = from person in Context.ViewPersonInRoles
                               where userIds.Contains(person.UserId)
                               select person.Email;

            return personInRole.ToArray();
        }
        public IEnumerable<ViewPersonInRole> Select(string[] roleNames, VmApprovalReject filterItem, int index, int count)
        {
            var personInRoleList = from person in Context.ViewPersonInRoles
                                   where roleNames.Contains(person.RoleName)
                                   select person;

            if (filterItem.University != null)
            {
                personInRoleList = personInRoleList.Where(t => t.University.Contains(filterItem.University));
            }

            if (filterItem.Email != null)
            {
                personInRoleList = personInRoleList.Where(t => t.Email.Contains(filterItem.Email));
            }

            if (filterItem.Name != null)
            {
                personInRoleList = personInRoleList.Where(t => t.FirstName.Contains(filterItem.Name));
            }

            if (filterItem.PhoneNumber != null)
            {
                personInRoleList = personInRoleList.Where(t => t.PhoneNumber.Contains(filterItem.PhoneNumber));
            }

            return personInRoleList.OrderBy(t => t.FirstName).Skip(index).Take(count).ToArray();

        }
        public IEnumerable<ViewPersonInRole> Select(VmPerson filterItem, int index, int count)
        {
            var personInRoleList = from person in Context.ViewPersonInRoles

                                   select person;

            if (filterItem.University != null)
            {
                personInRoleList = personInRoleList.Where(t => t.University.Contains(filterItem.University));
            }

            if (filterItem.Email != null)
            {
                personInRoleList = personInRoleList.Where(t => t.Email.Contains(filterItem.Email));
            }

            if (filterItem.FirstName != null)
            {
                personInRoleList = personInRoleList.Where(t => t.FirstName.Contains(filterItem.FirstName));
            }

            if (filterItem.LastName != null)
            {
                personInRoleList = personInRoleList.Where(t => t.LastName.Contains(filterItem.LastName));
            }

            if (filterItem.PhoneNumber != null)
            {
                personInRoleList = personInRoleList.Where(t => t.PhoneNumber.Contains(filterItem.PhoneNumber));
            }

            if (filterItem.DietType != null)
            {
                personInRoleList = personInRoleList.Where(t => t.DietType.Contains(filterItem.DietType));
            }

            if (filterItem.Allergies != null)
            {
                personInRoleList = personInRoleList.Where(t => t.Allergies.Contains(filterItem.Allergies));
            }

            if (filterItem.RoleName != null)
            {
                personInRoleList = personInRoleList.Where(t => t.RoleName.Contains(filterItem.RoleName));
            }

            if (filterItem.Agreement != null)
            {
                personInRoleList = personInRoleList.Where(t => t.Agreement == filterItem.Agreement);
            }

            if (filterItem.Sex != null)
            {
                personInRoleList = personInRoleList.Where(t => t.Sex == filterItem.Sex);
            }

            if (filterItem.JacketSize != null)
            {
                personInRoleList = personInRoleList.Where(t => t.JacketSize == filterItem.JacketSize);
            }
             if (filterItem.T_Shirt_Size != null)
            {
                personInRoleList = personInRoleList.Where(t => t.T_Shirt_Size == filterItem.T_Shirt_Size);
            }

            return personInRoleList.OrderBy(t => t.FirstName).Skip(index).Take(count).ToArray();

        }
    }
}
