using BLL.Base;
using Model;
using Model.ViewModels.Admin;
using Model.ViewModels.Judge;
using Model.ViewModels.Person;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace BLL
{
    public class BLPerson : BLBase
    {
        public bool CreatePerson(VmPerson vmPerson)
        {
            try
            {
                if (PersonIsExistByUserId(vmPerson.UserId) == false)
                {
                    var personRepository = UnitOfWork.GetRepository<PersonRepository>();

                    personRepository.CreatePerson(
                        new Person
                        {
                            UserId = vmPerson.UserId,
                            Sex = vmPerson.Sex,
                            FirstName = vmPerson.FirstName,
                            LastName = vmPerson.LastName,
                            UniversityId = vmPerson.UniversityId == 0 ? null : vmPerson.UniversityId,
                            JacketSizeId = vmPerson.JacketSizeId,
                            DietTypeId = vmPerson.DietTypeId,
                            Allergies = vmPerson.Allergies,

                            WelcomeDinner = vmPerson.WelcomeDinner,
                            LunchOnMonday = vmPerson.LunchOnMonday,
                            LunchOnTuesday = vmPerson.LunchOnTuesday,
                            ReceptionNetworkOnTuesday = vmPerson.ReceptionNetworkOnTuesday,
                            AwardBanquet = vmPerson.AwardBanquet,
                            NoneOfTheAbove = vmPerson.NoneOfTheAbove,
                            SecondaryEmail = vmPerson.SecondaryEmail,
                            Agreement = vmPerson.Agreement,
                        });

                    UnitOfWork.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool PersonIsExistByUserId(string userId)
        {
            try
            {
                var personRepository = UnitOfWork.GetRepository<PersonRepository>();

                return personRepository.PersonIsExistByUserId(userId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public VmPerson GetPersonByUserId(string userId)
        {
            try
            {
                var personInRoleRepository = UnitOfWork.GetRepository<ViewPersonInRoleRepository>();
                var userTaskRepository = UnitOfWork.GetRepository<UserTaskRepository>();

                var person = personInRoleRepository.GetUsersById(userId);
                var taskIds = userTaskRepository.GetUserTaskIds(userId);

                var vwPerson = new VmPerson
                {
                    Id = person.Id,
                    RoleId = person.RoleId,
                    SizeId = person.SizeId,
                    Sex = person.Sex,
                    UniversityId = person.UniversityId,
                    University = person.University ?? "",
                    JacketSizeId = person.JacketSizeId,
                    JacketSize = person.JacketSize ?? "",
                    DietTypeId = person.DietTypeId,
                    DietType = person.DietType ?? "",
                    Allergies = person.Allergies ?? "",
                    UserId = person.UserId,
                    ProfilePictureUrl = person.ProfilePictureUrl ?? "",
                    UniversityPictureUrl = person.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                    ResumeUrl = person.ResumeUrl ?? "",
                    FirstName = person.FirstName ?? "",
                    LastName = person.LastName ?? "",
                    RoleName = person.RoleName ?? "",
                    Email = person.Email,
                    SecondaryEmail = person.SecondaryEmail,
                    T_Shirt_Size = person.T_Shirt_Size ?? "",
                    PhoneNumber = person.PhoneNumber ?? "",
                    StreetLine1 = person.StreetLine1 ?? "",
                    StreetLine2 = person.StreetLine2 ?? "",
                    City = person.City ?? "",
                    State = person.State ?? "",
                    ZipCode = person.ZipCode ?? "",
                    EmgyPersonFirstName = person.EmgyPersonFirstName ?? "",
                    EmgyPersonLastName = person.EmgyPersonLastName ?? "",
                    EmgyPersonPhoneNumber = person.EmgyPersonPhoneNumber ?? "",
                    EmgyPersonRelationship = person.EmgyPersonRelationship ?? "",
                    ShortBio = person.ShortBio ?? "",

                    WelcomeDinner = person.WelcomeDinner,
                    LunchOnMonday = person.LunchOnMonday,
                    LunchOnTuesday = person.LunchOnTuesday,
                    ReceptionNetworkOnTuesday = person.ReceptionNetworkOnTuesday,
                    AwardBanquet = person.AwardBanquet,
                    NoneOfTheAbove = person.NoneOfTheAbove,
                    Agreement = person.Agreement ?? false,
                    TaskIds = taskIds,
                    ClientTaskIds = string.Join(",", taskIds),
                };

                return vwPerson;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string[] GetEmailsByUserIds(string[] userIds)
        {
            try
            {
                var personInRoleRepository = UnitOfWork.GetRepository<ViewPersonInRoleRepository>();

                return personInRoleRepository.GetEmailsByUserIds(userIds);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdatePerson(VmPerson vmPerson)
        {
            try
            {
                var personRepository = UnitOfWork.GetRepository<PersonRepository>();
                var dietTypeRepository = UnitOfWork.GetRepository<DietTypeRepository>();

                var person = new Person
                {
                    Id = vmPerson.Id,
                    SizeId = vmPerson.SizeId,
                    Sex = vmPerson.Sex,
                    UniversityId = vmPerson.UniversityId,
                    JacketSizeId = vmPerson.JacketSizeId,
                    UserId = vmPerson.UserId,
                    ResumeUrl = vmPerson.ResumeUrl,
                    FirstName = vmPerson.FirstName,
                    LastName = vmPerson.LastName,
                    StreetLine1 = vmPerson.StreetLine1,
                    StreetLine2 = vmPerson.StreetLine2,
                    City = vmPerson.City,
                    State = vmPerson.State,
                    ZipCode = vmPerson.ZipCode,
                    EmgyPersonFirstName = vmPerson.EmgyPersonFirstName,
                    EmgyPersonLastName = vmPerson.EmgyPersonLastName,
                    EmgyPersonPhoneNumber = vmPerson.EmgyPersonPhoneNumber,
                    EmgyPersonRelationship = vmPerson.EmgyPersonRelationship,
                    ShortBio = vmPerson.ShortBio,

                    WelcomeDinner = vmPerson.WelcomeDinner,
                    LunchOnMonday = vmPerson.LunchOnMonday,
                    LunchOnTuesday = vmPerson.LunchOnTuesday,
                    ReceptionNetworkOnTuesday = vmPerson.ReceptionNetworkOnTuesday,
                    AwardBanquet = vmPerson.AwardBanquet,
                    NoneOfTheAbove = vmPerson.NoneOfTheAbove,
                    SecondaryEmail = vmPerson.SecondaryEmail,
                    Agreement = vmPerson.Agreement,

                };

                if (vmPerson.DietTypeId == 5)
                {
                    var dietType = new DietType
                    {
                        Id = dietTypeRepository.GetDietTypeNewId(),
                        Name = vmPerson.DietType,
                        Display = true,
                    };

                    person.DietTypeId = dietType.Id;

                    dietTypeRepository.CreateDietType(dietType);
                }
                else
                {
                    person.DietTypeId = vmPerson.DietTypeId;
                }

                person.Allergies = vmPerson.Allergies;

                personRepository.UpdatePerson(person);

                //Do not delete its for next year
                //if (vmPerson.RoleId == "4d7951d8-8eda-4452-8ff1-dfc9076d8bbe")
                //{
                //    var userTaskRepository = UnitOfWork.GetRepository<UserTaskRepository>()

                //    userTaskRepository.DeleteUserTasks(vmPerson.UserId);
                //    userTaskRepository.CreateTasksUser(vmPerson.UserId, vmPerson.TaskIds);
                //}

                var teamMemberRepository = UnitOfWork.GetRepository<TeamMemberRepository>();

                var updateableTeamMember = new TeamMember
                {

                    MemberUserId = vmPerson.UserId,
                    RegistrationStatus = true,
                };

                teamMemberRepository.UpdateTeamMemberRegistrationStatusByUserId(updateableTeamMember);


                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UploadProfileImage(string userId, string profilePictureUrl)
        {
            try
            {

                var personRepository = UnitOfWork.GetRepository<PersonRepository>();
                personRepository.UpdateProfileImage(userId, profilePictureUrl);

                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UploadResume(string userId, string resumeUrl)
        {
            try
            {

                var personRepository = UnitOfWork.GetRepository<PersonRepository>();
                personRepository.UpdateResume(userId, resumeUrl);

                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<VmApprovalReject> GetUsersByRoleNames(string[] roleNames)
        {
            var viewPersonInRoleRepository = UnitOfWork.GetRepository<ViewPersonInRoleRepository>();

            var usersList = viewPersonInRoleRepository.GetUsersByRoleNames(roleNames);

            var vmApprovalRejectList = from user in usersList
                                       select new VmApprovalReject
                                       {
                                           UserId = user.UserId,
                                           Sex = user.Sex,
                                           University = user.University,
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           Name = user.Name,
                                           RoleName = user.RoleName,
                                           Email = user.Email,
                                           EmailConfirmed = user.EmailConfirmed,
                                           PhoneNumber = user.PhoneNumber,
                                           UserName = user.UserName,
                                           LockoutEnabled = user.LockoutEnabled,
                                           ProfilePictureUrl = user.ProfilePictureUrl,
                                           Approval = (user.EmailConfirmed == false && user.LockoutEnabled == false) ? (int)Approval.Pending
                                           : (user.EmailConfirmed == false && user.LockoutEnabled == true) ? 2 : 1,

                                           WelcomeDinner = user.WelcomeDinner,
                                           LunchOnMonday = user.LunchOnMonday,
                                           LunchOnTuesday = user.LunchOnTuesday,
                                           ReceptionNetworkOnTuesday = user.ReceptionNetworkOnTuesday,
                                           AwardBanquet = user.AwardBanquet,
                                           NoneOfTheAbove = user.NoneOfTheAbove,
                                           Agreement = user.Agreement ?? false,
                                       };

            return vmApprovalRejectList;

        }
        public IEnumerable<VmApprovalReject> GetUsersByFilterAndRoleNames(string[] roleNames, VmApprovalReject filterItem = null)
        {
            var viewPersonInRoleRepository = UnitOfWork.GetRepository<ViewPersonInRoleRepository>();

            var usersList = viewPersonInRoleRepository.Select(roleNames, filterItem, 0, int.MaxValue);

            var vmApprovalRejectList = from user in usersList
                                       select new VmApprovalReject
                                       {
                                           UserId = user.UserId,
                                           Sex = user.Sex,
                                           University = user.University,
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           Name = user.Name,
                                           RoleName = user.RoleName,
                                           Email = user.Email,
                                           EmailConfirmed = user.EmailConfirmed,
                                           PhoneNumber = user.PhoneNumber,
                                           UserName = user.UserName,
                                           LockoutEnabled = user.LockoutEnabled,
                                           ProfilePictureUrl = user.ProfilePictureUrl,
                                           Approval = (user.EmailConfirmed == false && user.LockoutEnabled == false) ? (int)Approval.Pending
                                           : (user.EmailConfirmed == false && user.LockoutEnabled == true) ? 2 : 1,

                                           WelcomeDinner = user.WelcomeDinner,
                                           LunchOnMonday = user.LunchOnMonday,
                                           LunchOnTuesday = user.LunchOnTuesday,
                                           ReceptionNetworkOnTuesday = user.ReceptionNetworkOnTuesday,
                                           AwardBanquet = user.AwardBanquet,
                                           NoneOfTheAbove = user.NoneOfTheAbove,
                                           Agreement = user.Agreement ?? false,
                                       };

            return vmApprovalRejectList.OrderBy(p => p.Approval);

        }
        public IEnumerable<VmPerson> GetUsersByFilter(VmPerson filterItem = null)
        {
            var viewPersonInRoleRepository = UnitOfWork.GetRepository<ViewPersonInRoleRepository>();

            var usersList = viewPersonInRoleRepository.Select(filterItem, 0, int.MaxValue);

            var vmApprovalRejectList = from person in usersList
                                       select new VmPerson
                                       {
                                           Id = person.Id,
                                           RoleId = person.RoleId,
                                           SizeId = person.SizeId,
                                           Sex = person.Sex,
                                           UniversityId = person.UniversityId,
                                           University = person.University ?? "",
                                           JacketSizeId = person.JacketSizeId,
                                           JacketSize = person.JacketSize ?? "",
                                           DietTypeId = person.DietTypeId,
                                           DietType = person.DietType ?? "",
                                           Allergies = person.Allergies ?? "",
                                           UserId = person.UserId,
                                           ProfilePictureUrl = person.ProfilePictureUrl ?? "",
                                           UniversityPictureUrl = person.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                           ResumeUrl = person.ResumeUrl ?? "",
                                           FirstName = person.FirstName ?? "",
                                           LastName = person.LastName ?? "",
                                           RoleName = person.RoleName ?? "",
                                           Email = person.Email,
                                           SecondaryEmail = person.SecondaryEmail,
                                           T_Shirt_Size = person.T_Shirt_Size ?? "",
                                           PhoneNumber = person.PhoneNumber ?? "",
                                           StreetLine1 = person.StreetLine1 ?? "",
                                           StreetLine2 = person.StreetLine2 ?? "",
                                           City = person.City ?? "",
                                           State = person.State ?? "",
                                           ZipCode = person.ZipCode ?? "",
                                           EmgyPersonFirstName = person.EmgyPersonFirstName ?? "",
                                           EmgyPersonLastName = person.EmgyPersonLastName ?? "",
                                           EmgyPersonPhoneNumber = person.EmgyPersonPhoneNumber ?? "",
                                           EmgyPersonRelationship = person.EmgyPersonRelationship ?? "",
                                           ShortBio = person.ShortBio ?? "",

                                           WelcomeDinner = person.WelcomeDinner,
                                           LunchOnMonday = person.LunchOnMonday,
                                           LunchOnTuesday = person.LunchOnTuesday,
                                           ReceptionNetworkOnTuesday = person.ReceptionNetworkOnTuesday,
                                           AwardBanquet = person.AwardBanquet,
                                           NoneOfTheAbove = person.NoneOfTheAbove,
                                           Agreement = person.Agreement ?? false,
                                       };

            return vmApprovalRejectList.OrderBy(p => p.LastName);

        }
        public IEnumerable<VmJudgeFullInfo> GetJudgeFullInfoByFilter(VmJudgeFullInfo filterItem)
        {
            var viewJudgeFullInfoRepository = UnitOfWork.GetRepository<ViewJudgeFullInfoRepository>();

            var viewFilterItem = new ViewJudgeFullInfo
            {
                FirstName = filterItem.FirstName,
                LastName = filterItem.LastName,
                Email = filterItem.Email,
                Tasks = filterItem.Tasks,
                Teams = filterItem.Teams,
            };

            var viewjudgeFullInfoList = viewJudgeFullInfoRepository.Select(viewFilterItem, 0, int.MaxValue);

            var vmJudgeFullInfoList = from judgeFullInfo in viewjudgeFullInfoList
                                      select new VmJudgeFullInfo
                                      {
                                          Id = judgeFullInfo.Id,
                                          PhoneNumber = judgeFullInfo.PhoneNumber,
                                          Sex = judgeFullInfo.Sex,
                                          UserId = judgeFullInfo.UserId,
                                          UserName = judgeFullInfo.UserName,
                                          Email = judgeFullInfo.Email,
                                          RoleName = judgeFullInfo.RoleName,
                                          RoleId = judgeFullInfo.RoleId,
                                          DietTypeId = judgeFullInfo.DietTypeId,
                                          DietType = judgeFullInfo.DietType ?? "",
                                          StreetLine1 = judgeFullInfo.StreetLine1,
                                          StreetLine2 = judgeFullInfo.StreetLine2,
                                          City = judgeFullInfo.City,
                                          State = judgeFullInfo.State,
                                          ZipCode = judgeFullInfo.ZipCode,
                                          ShortBio = judgeFullInfo.ShortBio,
                                          T_Shirt_Size = judgeFullInfo.T_Shirt_Size,
                                          ProfilePictureUrl = judgeFullInfo.ProfilePictureUrl,
                                          ResumeUrl = judgeFullInfo.ResumeUrl,
                                          EmailConfirmed = judgeFullInfo.EmailConfirmed,
                                          FirstName = judgeFullInfo.FirstName,
                                          LastName = judgeFullInfo.LastName,
                                          SizeId = judgeFullInfo.SizeId,
                                          Tasks = judgeFullInfo.Tasks,
                                          Teams = judgeFullInfo.Teams,
                                          Agreement = judgeFullInfo.Agreement ?? false,

                                      };
            return vmJudgeFullInfoList;
        }

    }
}