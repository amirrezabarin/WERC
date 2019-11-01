using BLL.Base;
using Model;
using Model.ViewModels.Invoice;
using Model.ViewModels.Team;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static Model.ApplicationDomainModels.ConstantObjects;

namespace BLL
{
    public class BLTeam : BLBase
    {
        public VmTeam GetTeamById(int id)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();

            var team = teamRepository.GetTeamById(id);
            var vmTeam = new VmTeam
            {
                Id = team.Id,
                TaskId = team.TaskId,
                Name = team.Name,
                TeamNumber = team.TeamNumber,
                Task = team.Task,
                TeamState = team.TeamState.Value,
                TeamImageUrl = team.TeamImageUrl,
                TeamStateDescription = GetTeamStateDescription(team.TeamState),
                RegistrationStatus = team.RegistrationStatus,
                Survey = team.Survey,
                MemberName = team.MemberName,
                PhoneNumber = team.PhoneNumber,
                Identifier = team.Identifier,
                Sex = team.Sex,
                BirthDate = team.BirthDate,
                UserName = team.UserName,
                Email = team.Email,
                RegisterDate = team.RegisterDate,
                RoleName = team.RoleName,
                RoleId = team.RoleId,
                UserDefiner = team.UserDefiner,
                LastSignIn = team.LastSignIn,
                UniversityId = team.UniversityId,
                University = team.University,
                JacketSizeId = team.JacketSizeId,
                JacketSize = team.JacketSize ?? "",
                DietTypeId = team.DietTypeId,
                DietType = team.DietType ?? "",
                StreetLine1 = team.StreetLine1,
                StreetLine2 = team.StreetLine2,
                City = team.City,
                State = team.State,
                ZipCode = team.ZipCode,
                EmgyPersonRelationship = team.EmgyPersonRelationship,
                EmgyPersonPhoneNumber = team.EmgyPersonPhoneNumber,
                EmgyPersonLastName = team.EmgyPersonLastName,
                EmgyPersonFirstName = team.EmgyPersonFirstName,
                ShortBio = team.ShortBio,
                T_Shirt_Size = team.T_Shirt_Size,
                ProfilePictureUrl = team.ProfilePictureUrl,
                UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                LabResultUrl = team.LabResultUrl,

                WrittenReportUrl = (team.RoleName.ToLower().Contains("advisor") || team.RoleName.ToLower().Contains("leader"))
                    ?
                    string.IsNullOrWhiteSpace(team.WrittenReportUrl) ? "-?CT=application_pdf.png" : team.WrittenReportUrl
                    :
                    string.IsNullOrWhiteSpace(team.WrittenReportUrl) ? "-?CT=Stylish_not_ok.png" : "-?CT=Stylish_ok.png",

                WrittenReportUrlForMember = team.WrittenReportUrl,
                WrittenReportDate = team.WrittenReportDate,
                Shipping = "",
                ResumeUrl = team.ResumeUrl,
                Date = team.Date,
                EmailConfirmed = team.EmailConfirmed,
                LockoutEnabled = team.LockoutEnabled,
                PayStatus = team.PayStatus,
                SuppressScoring = team.SuppressScoring,
                Deactivate = team.Deactivate,

            };


            return vmTeam;
        }

        public IEnumerable<VmTeam> GetLabTeams(string labUserId, string teamName)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTaskTeamRepository>();

            var teamList = teamRepository.GetMemberUserTeams(labUserId, teamName);
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.TeamId,
                                 TaskId = team.TaskId,
                                 Name = team.TeamName,
                                 Task = team.TaskName,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 University = team.University,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 Date = team.Date,
                                 TaskImageUrl = team.TaskImageUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 PayStatus = team.PayStatus,
                             };

            return vmTeamList;
        }

        public IEnumerable<VmTeam> GetLabTeams(string labUserId)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTaskTeamRepository>();

            var teamList = teamRepository.GetMemberUserTeams(labUserId);

            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.TeamId,
                                 TaskId = team.TaskId,
                                 Name = team.TeamName,
                                 MemberName = team.MemberName,
                                 Task = team.TaskName,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 University = team.University,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 Date = team.Date,
                                 TaskImageUrl = team.TaskImageUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 PayStatus = team.PayStatus,
                             };

            return vmTeamList;
        }

        public IEnumerable<VmTeam> GetAdvisorTeams(string advisorId)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();

            var teamList = teamRepository.GetMemberUserTeams(advisorId);
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.Id,
                                 TaskId = team.TaskId,
                                 Name = team.Name,
                                 TeamNumber = team.TeamNumber,
                                 Task = team.Task,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 MemberName = team.MemberName,
                                 PhoneNumber = team.PhoneNumber,
                                 Identifier = team.Identifier,
                                 Sex = team.Sex,
                                 BirthDate = team.BirthDate,
                                 UserName = team.UserName,
                                 Email = team.Email,
                                 RegisterDate = team.RegisterDate,
                                 RoleName = team.RoleName,
                                 RoleId = team.RoleId,
                                 UserDefiner = team.UserDefiner,
                                 LastSignIn = team.LastSignIn,
                                 UniversityId = team.UniversityId,
                                 University = team.University,
                                 JacketSizeId = team.JacketSizeId,
                                 JacketSize = team.JacketSize ?? "",
                                 DietTypeId = team.DietTypeId,
                                 DietType = team.DietType ?? "",
                                 StreetLine1 = team.StreetLine1,
                                 StreetLine2 = team.StreetLine2,
                                 City = team.City,
                                 State = team.State,
                                 ZipCode = team.ZipCode,
                                 EmgyPersonRelationship = team.EmgyPersonRelationship,
                                 EmgyPersonPhoneNumber = team.EmgyPersonPhoneNumber,
                                 EmgyPersonLastName = team.EmgyPersonLastName,
                                 EmgyPersonFirstName = team.EmgyPersonFirstName,
                                 ShortBio = team.ShortBio,
                                 T_Shirt_Size = team.T_Shirt_Size,
                                 ProfilePictureUrl = team.ProfilePictureUrl,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 LabResultUrl = team.LabResultUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 Shipping = "",
                                 ResumeUrl = team.ResumeUrl,
                                 Date = team.Date,
                                 EmailConfirmed = team.EmailConfirmed,
                                 LockoutEnabled = team.LockoutEnabled,
                                 PayStatus = team.PayStatus,
                                 SuppressScoring = team.SuppressScoring,
                                 Deactivate = team.Deactivate,

                             };

            return vmTeamList;
        }
        public IEnumerable<VmTeam> GetTeamsByLeader(string leaderId)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();
            var viewTeamMemberRepository = UnitOfWork.GetRepository<ViewTeamMemberRepository>();

            var teamId = viewTeamMemberRepository.GetTeamMember(leaderId).TeamId;

            var advisorId = teamRepository.GetTeamById(teamId).MemberUserId;

            var teamList = teamRepository.GetMemberUserTeams(advisorId);
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.Id,
                                 TaskId = team.TaskId,
                                 Name = team.Name,
                                 TeamNumber = team.TeamNumber,
                                 Task = team.Task,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 MemberName = team.MemberName,
                                 PhoneNumber = team.PhoneNumber,
                                 Identifier = team.Identifier,
                                 Sex = team.Sex,
                                 BirthDate = team.BirthDate,
                                 UserName = team.UserName,
                                 Email = team.Email,
                                 RegisterDate = team.RegisterDate,
                                 RoleName = team.RoleName,
                                 RoleId = team.RoleId,
                                 UserDefiner = team.UserDefiner,
                                 LastSignIn = team.LastSignIn,
                                 UniversityId = team.UniversityId,
                                 University = team.University,
                                 JacketSizeId = team.JacketSizeId,
                                 JacketSize = team.JacketSize ?? "",
                                 DietTypeId = team.DietTypeId,
                                 DietType = team.DietType ?? "",
                                 StreetLine1 = team.StreetLine1,
                                 StreetLine2 = team.StreetLine2,
                                 City = team.City,
                                 State = team.State,
                                 ZipCode = team.ZipCode,
                                 EmgyPersonRelationship = team.EmgyPersonRelationship,
                                 EmgyPersonPhoneNumber = team.EmgyPersonPhoneNumber,
                                 EmgyPersonLastName = team.EmgyPersonLastName,
                                 EmgyPersonFirstName = team.EmgyPersonFirstName,
                                 ShortBio = team.ShortBio,
                                 T_Shirt_Size = team.T_Shirt_Size,
                                 ProfilePictureUrl = team.ProfilePictureUrl,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 LabResultUrl = team.LabResultUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 Shipping = "",
                                 ResumeUrl = team.ResumeUrl,
                                 Date = team.Date,
                                 EmailConfirmed = team.EmailConfirmed,
                                 LockoutEnabled = team.LockoutEnabled,
                                 PayStatus = team.PayStatus,
                                 SuppressScoring = team.SuppressScoring,
                                 Deactivate = team.Deactivate,

                             };

            return vmTeamList;
        }
        public IEnumerable<VmTeam> GetTeamsByCoAdvisor(string coAdvisorId)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();
            var viewTeamMemberRepository = UnitOfWork.GetRepository<ViewTeamMemberRepository>();

            var teamId = viewTeamMemberRepository.GetTeamMember(coAdvisorId).TeamId;

            var advisorId = teamRepository.GetTeamById(teamId).MemberUserId;

            var teamList = teamRepository.GetMemberUserTeams(advisorId);
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.Id,
                                 TaskId = team.TaskId,
                                 Name = team.Name,
                                 TeamNumber = team.TeamNumber,
                                 Task = team.Task,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 MemberName = team.MemberName,
                                 PhoneNumber = team.PhoneNumber,
                                 Identifier = team.Identifier,
                                 Sex = team.Sex,
                                 BirthDate = team.BirthDate,
                                 UserName = team.UserName,
                                 Email = team.Email,
                                 RegisterDate = team.RegisterDate,
                                 RoleName = team.RoleName,
                                 RoleId = team.RoleId,
                                 UserDefiner = team.UserDefiner,
                                 LastSignIn = team.LastSignIn,
                                 UniversityId = team.UniversityId,
                                 University = team.University,
                                 JacketSizeId = team.JacketSizeId,
                                 JacketSize = team.JacketSize ?? "",
                                 DietTypeId = team.DietTypeId,
                                 DietType = team.DietType ?? "",
                                 StreetLine1 = team.StreetLine1,
                                 StreetLine2 = team.StreetLine2,
                                 City = team.City,
                                 State = team.State,
                                 ZipCode = team.ZipCode,
                                 EmgyPersonRelationship = team.EmgyPersonRelationship,
                                 EmgyPersonPhoneNumber = team.EmgyPersonPhoneNumber,
                                 EmgyPersonLastName = team.EmgyPersonLastName,
                                 EmgyPersonFirstName = team.EmgyPersonFirstName,
                                 ShortBio = team.ShortBio,
                                 T_Shirt_Size = team.T_Shirt_Size,
                                 ProfilePictureUrl = team.ProfilePictureUrl,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 LabResultUrl = team.LabResultUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 Shipping = "",
                                 ResumeUrl = team.ResumeUrl,
                                 Date = team.Date,
                                 EmailConfirmed = team.EmailConfirmed,
                                 LockoutEnabled = team.LockoutEnabled,
                                 PayStatus = team.PayStatus,
                                 SuppressScoring = team.SuppressScoring,
                                 Deactivate = team.Deactivate,

                             };

            return vmTeamList;
        }

        public IEnumerable<VmTeam> GetAdvisorTeams(string advisorId, string name = "")
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();

            var teamList = teamRepository.GetMemberUserTeams(advisorId, name);
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.Id,
                                 TaskId = team.TaskId,
                                 Name = team.Name,
                                 TeamNumber = team.TeamNumber,
                                 Task = team.Task,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 MemberName = team.MemberName,
                                 PhoneNumber = team.PhoneNumber,
                                 Identifier = team.Identifier,
                                 Sex = team.Sex,
                                 BirthDate = team.BirthDate,
                                 UserName = team.UserName,
                                 Email = team.Email,
                                 RegisterDate = team.RegisterDate,
                                 RoleName = team.RoleName,
                                 RoleId = team.RoleId,
                                 UserDefiner = team.UserDefiner,
                                 LastSignIn = team.LastSignIn,
                                 UniversityId = team.UniversityId,
                                 University = team.University,
                                 JacketSizeId = team.JacketSizeId,
                                 JacketSize = team.JacketSize ?? "",
                                 DietTypeId = team.DietTypeId,
                                 DietType = team.DietType ?? "",
                                 StreetLine1 = team.StreetLine1,
                                 StreetLine2 = team.StreetLine2,
                                 City = team.City,
                                 State = team.State,
                                 ZipCode = team.ZipCode,
                                 EmgyPersonRelationship = team.EmgyPersonRelationship,
                                 EmgyPersonPhoneNumber = team.EmgyPersonPhoneNumber,
                                 EmgyPersonLastName = team.EmgyPersonLastName,
                                 EmgyPersonFirstName = team.EmgyPersonFirstName,
                                 ShortBio = team.ShortBio,
                                 T_Shirt_Size = team.T_Shirt_Size,
                                 ProfilePictureUrl = team.ProfilePictureUrl,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 LabResultUrl = team.LabResultUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 Shipping = "",//team.Shipping
                                 ResumeUrl = team.ResumeUrl,
                                 Date = team.Date,
                                 EmailConfirmed = team.EmailConfirmed,
                                 LockoutEnabled = team.LockoutEnabled,
                                 PayStatus = team.PayStatus,
                                 SuppressScoring = team.SuppressScoring,
                                 Deactivate = team.Deactivate,
                             };

            return vmTeamList;
        }
        public IEnumerable<VmTeam> GetJudgeTeams(string judgeId)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTaskTeamRepository>();

            var teamList = teamRepository.GetMemberUserTeams(judgeId);

            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.TeamId,
                                 TaskId = team.TaskId,
                                 Name = team.TeamName,
                                 MemberName = team.MemberName,
                                 Task = team.TaskName,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 University = team.University,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 Date = team.Date,
                                 TaskImageUrl = team.TaskImageUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 PayStatus = team.PayStatus,
                                 SuppressScoring = team.SuppressScoring,
                             };

            return vmTeamList;
        }
        public IEnumerable<VmTeam> GetJudgeTeams(string judgeId, string name = "")
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTaskTeamRepository>();

            var teamList = teamRepository.GetMemberUserTeams(judgeId, name);
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.TeamId,
                                 TaskId = team.TaskId,
                                 Name = team.TeamName,
                                 Task = team.TaskName,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 University = team.University,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 Date = team.Date,
                                 TaskImageUrl = team.TaskImageUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 PayStatus = team.PayStatus,
                             };

            return vmTeamList;
        }
        public IEnumerable<VmTeam> GetTeamList(string name = "")
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();

            var teamList = teamRepository.GetTeams(name);
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.Id,
                                 TaskId = team.TaskId,
                                 Name = team.Name,
                                 TeamNumber = team.TeamNumber,
                                 Task = team.Task,
                                 TeamState = team.TeamState.Value,
                                 TeamImageUrl = team.TeamImageUrl,
                                 TeamStateDescription = GetTeamStateDescription(team.TeamState),
                                 RegistrationStatus = team.RegistrationStatus,
                                 Survey = team.Survey,
                                 MemberName = team.MemberName,
                                 PhoneNumber = team.PhoneNumber,
                                 Identifier = team.Identifier,
                                 Sex = team.Sex,
                                 BirthDate = team.BirthDate,
                                 UserName = team.UserName,
                                 Email = team.Email,
                                 RegisterDate = team.RegisterDate,
                                 RoleName = team.RoleName,
                                 RoleId = team.RoleId,
                                 UserDefiner = team.UserDefiner,
                                 LastSignIn = team.LastSignIn,
                                 UniversityId = team.UniversityId,
                                 University = team.University,
                                 JacketSizeId = team.JacketSizeId,
                                 JacketSize = team.JacketSize ?? "",
                                 DietTypeId = team.DietTypeId,
                                 DietType = team.DietType ?? "",
                                 StreetLine1 = team.StreetLine1,
                                 StreetLine2 = team.StreetLine2,
                                 City = team.City,
                                 State = team.State,
                                 ZipCode = team.ZipCode,
                                 EmgyPersonRelationship = team.EmgyPersonRelationship,
                                 EmgyPersonPhoneNumber = team.EmgyPersonPhoneNumber,
                                 EmgyPersonLastName = team.EmgyPersonLastName,
                                 EmgyPersonFirstName = team.EmgyPersonFirstName,
                                 ShortBio = team.ShortBio,
                                 T_Shirt_Size = team.T_Shirt_Size,
                                 ProfilePictureUrl = team.ProfilePictureUrl,
                                 UniversityPictureUrl = team.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                 LabResultUrl = team.LabResultUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 Shipping = "",
                                 ResumeUrl = team.ResumeUrl,
                                 Date = team.Date,
                                 EmailConfirmed = team.EmailConfirmed,
                                 LockoutEnabled = team.LockoutEnabled,
                                 PayStatus = team.PayStatus,
                                 SuppressScoring = team.SuppressScoring,
                                 Deactivate = team.Deactivate,
                             };

            return vmTeamList;
        }
        public IEnumerable<VmTeam> GetTeamList()
        {
            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

            var teamList = teamRepository.GetAllTeam();
            var vmTeamList = from team in teamList
                             select new VmTeam
                             {
                                 Id = team.Id,
                                 TaskId = team.TaskId,
                                 Name = team.Name,
                                 TeamNumber = team.TeamNumber,
                                 LabResultUrl = team.LabResultUrl,
                                 WrittenReportUrl = team.WrittenReportUrl ?? "?CT=Stylish_not_ok.png",
                                 WrittenReportUrlForMember = team.WrittenReportUrl,
                                 WrittenReportDate = team.WrittenReportDate,
                                 Shipping = "",
                                 Date = team.Date,
                                 PayStatus = team.PayStatus,
                                 SuppressScoring = team.SuppressScoring,
                                 Deactivate = team.Deactivate,
                             };

            return vmTeamList;
        }
        public IEnumerable<int> GetTeamIdsByTask(int taskId)
        {
            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

            var teamList = teamRepository.GetTeamsByTask(taskId);

            var teamIds = from team in teamList select team.Id;

            return teamIds.ToArray();
        }

        public int GetLeaderTeam(string leaderId)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();
            var viewTeamMemberRepository = UnitOfWork.GetRepository<ViewTeamMemberRepository>();

            return viewTeamMemberRepository.GetTeamMember(leaderId).TeamId;
        }
        public int GetCoAdvisorTeam(string coAdvisorId)
        {
            var teamRepository = UnitOfWork.GetRepository<ViewTeamRepository>();
            var viewTeamMemberRepository = UnitOfWork.GetRepository<ViewTeamMemberRepository>();

            return viewTeamMemberRepository.GetTeamMember(coAdvisorId).TeamId;
        }

        public int CreateTeam(VmTeam vmTeam)
        {
            var result = -1;
            try
            {
                var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

                var currentDate = DateTime.Now;

                var teamMembers = new List<TeamMember>()
                {
                    new TeamMember
                    {
                         MemberUserId = vmTeam.CurrentUserId,
                         RegistrationStatus = true,
                    }
                };

                var teamNumber = teamRepository.FindFirstEmptyTeamNumber();

                if (teamNumber == 100)
                {
                    return -1;
                }

                var universityWords = vmTeam.University.Split(' ');
                var abbreviation = "";
                var count = 0;
                for (int i = 0; i < universityWords.Length; i++)
                {
                    if (universityWords[i].Length > 2)
                    {
                        abbreviation += universityWords[i][0];
                        count++;
                    }

                }

                var teamName = teamNumber.ToString("d2") + abbreviation + "-" + vmTeam.Task.Split(' ')[1];

                var newTeam = new Team
                {
                    Id = vmTeam.Id,
                    TaskId = vmTeam.TaskId,
                    Date = DateTime.Now,
                    Name = teamName,
                    State = 0,
                    PayStatus = false,
                    ImageUrl = vmTeam.TeamImageUrl,
                    TeamMembers = teamMembers,
                    TeamNumber = teamNumber,
                    Deactivate = false,
                };


                teamRepository.CreateTeam(newTeam);

                UnitOfWork.Commit();

                result = newTeam.Id;


            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;
        }
        public void UpdateTeamName(int teamId, string name)
        {
            var TeamRepository = UnitOfWork.GetRepository<TeamRepository>();

            TeamRepository.UpdateTeamName(teamId, name);
            UnitOfWork.Commit();
        }
        public bool UploadWrittenReport(int teamId, string writtenReportUrl)
        {
            try
            {

                var teamRepository = UnitOfWork.GetRepository<TeamRepository>();
                teamRepository.UpdateWrittenReport(teamId, writtenReportUrl);

                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateTeam(VmTeam vmTeam)
        {
            try
            {
                var TeamRepository = UnitOfWork.GetRepository<TeamRepository>();

                var updateableTeam = new Team
                {
                    Id = vmTeam.Id,
                    TaskId = vmTeam.TaskId,
                    Name = vmTeam.Name,
                    State = vmTeam.TeamState,
                    ImageUrl = vmTeam.TeamImageUrl,
                    Deactivate = vmTeam.Deactivate,
                };

                TeamRepository.UpdateTeam(updateableTeam);

                return UnitOfWork.Commit();
            }
            catch
            {
                return false;
            }

        }
        public bool UpdatePayStatus(int teamId, bool payStatus)
        {
            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

            teamRepository.UpdatePayStatus(teamId, payStatus);
            return UnitOfWork.Commit();
        }
        public bool UpdateTeamActivation(int teamId, bool deactivation)
        {
            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

            teamRepository.UpdateTeamActivation(teamId, deactivation);
            return UnitOfWork.Commit();
        }
        public bool ReverseTeamActivation(int teamId)
        {
            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

            var lastStatus = teamRepository.ReverseTeamActivation(teamId);
            UnitOfWork.Commit();
            return lastStatus;
        }
        public bool ReverseTeamSuppressScoring(int teamId)
        {
            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

            var lastStatus = teamRepository.ReverseTeamSuppressScoring(teamId);
            UnitOfWork.Commit();
            return lastStatus;
        }
        public bool UpdatePayStatus(List<VmTeamSelection> teamSelectionList, bool payStatus)
        {
            var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

            var teamIds = teamSelectionList.Where(t => t.Checked == true).Select(t => t.TeamId).ToArray();

            teamRepository.UpdatePayStatus(teamIds, payStatus);

            return UnitOfWork.Commit();
        }
        public bool DeleteTeam(int teamId)
        {
            try
            {
                var teamRepository = UnitOfWork.GetRepository<TeamRepository>();

                if (teamRepository.DeleteTeam(teamId) == true)
                {
                    return UnitOfWork.Commit();
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
        public bool UpdateTeamState(int teamId, TeamState teamState)
        {
            try
            {
                var TeamRepository = UnitOfWork.GetRepository<TeamRepository>();

                TeamRepository.UpdateTeamState(teamId, teamState);

                UnitOfWork.Commit();

                return true;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<VmTeamFullInfo> GetTeamFullInfoByFilter(VmTeamFullInfo filterItem)
        {
            var viewTeamFullInfoRepository = UnitOfWork.GetRepository<ViewTeamFullInfoRepository>();

            var viewFilterItem = new VmTeamFullInfo
            {
                Name = filterItem.Name,
                Leader = filterItem.Leader,
                Advisor = filterItem.Advisor,
                TaskName = filterItem.TaskName,
                Judges = filterItem.Judges,
                PayStatus = filterItem.PayStatus,
                Deactivate = filterItem.Deactivate,
                RegistrationStatus = filterItem.RegistrationStatus,
                //SafetyStatus = filterItem.SafetyStatus,
                //SafetyStatus = filterItem.WrittenReportStatus,
                //Shipping = filterItem.Shipping,
                //Survey = filterItem.Survey,
            };

            var viewteamFullInfoList = viewTeamFullInfoRepository.Select(viewFilterItem, 0, int.MaxValue);

            var vmTeamFullInfoList = from teamFullInfo in viewteamFullInfoList
                                     select new VmTeamFullInfo
                                     {
                                         Id = teamFullInfo.Id,
                                         TaskId = teamFullInfo.TaskId,
                                         Name = teamFullInfo.Name,
                                         TaskName = teamFullInfo.TaskName,
                                         TeamImageUrl = teamFullInfo.TeamImageUrl,
                                         TeamState = teamFullInfo.TeamState.Value,
                                         TeamStateDescription = teamFullInfo.TeamState.ToString(),
                                         SafetyStatus = /*teamFullInfo.SafetyStatus*/ false,
                                         PayStatus = teamFullInfo.PayStatus,
                                         PayStatusDescription = (teamFullInfo.PayStatus == true) ? PayStatus.Payed.ToString() : PayStatus.NotPayed.ToString(),
                                         RegistrationStatus = teamFullInfo.RegistrationStatus,
                                         Survey = teamFullInfo.Survey,
                                         Advisor = teamFullInfo.Advisor,
                                         Leader = teamFullInfo.Leader,
                                         Judges = teamFullInfo.Judges,
                                         PhoneNumber = teamFullInfo.PhoneNumber,
                                         Identifier = teamFullInfo.Identifier,
                                         Sex = teamFullInfo.Sex,
                                         BirthDate = teamFullInfo.BirthDate,
                                         UserName = teamFullInfo.UserName,
                                         Email = teamFullInfo.Email,
                                         RegisterDate = teamFullInfo.RegisterDate,
                                         RoleName = teamFullInfo.RoleName,
                                         RoleId = teamFullInfo.RoleId,
                                         UserDefiner = teamFullInfo.UserDefiner,
                                         LastSignIn = teamFullInfo.LastSignIn,
                                         UniversityId = teamFullInfo.UniversityId,
                                         University = teamFullInfo.University,
                                         JacketSizeId = teamFullInfo.JacketSizeId,
                                         JacketSize = teamFullInfo.JacketSize ?? "",
                                         DietTypeId = teamFullInfo.DietTypeId,
                                         DietType = teamFullInfo.DietType ?? "",
                                         StreetLine1 = teamFullInfo.StreetLine1,
                                         StreetLine2 = teamFullInfo.StreetLine2,
                                         City = teamFullInfo.City,
                                         State = teamFullInfo.State,
                                         ZipCode = teamFullInfo.ZipCode,
                                         EmgyPersonRelationship = teamFullInfo.EmgyPersonRelationship,
                                         EmgyPersonPhoneNumber = teamFullInfo.EmgyPersonPhoneNumber,
                                         EmgyPersonLastName = teamFullInfo.EmgyPersonLastName,
                                         EmgyPersonFirstName = teamFullInfo.EmgyPersonFirstName,
                                         ShortBio = teamFullInfo.ShortBio,
                                         T_Shirt_Size = teamFullInfo.T_Shirt_Size,
                                         ProfilePictureUrl = teamFullInfo.ProfilePictureUrl,
                                         UniversityPictureUrl = teamFullInfo.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                         LabResultUrl = teamFullInfo.LabResultUrl,
                                         WrittenReportUrl = string.IsNullOrWhiteSpace(teamFullInfo.WrittenReportUrl)
                                                ? "-?CT=Stylish_not_ok.png" : "-?CT=Stylish_ok.png",
                                         WrittenReportUrlForMember = teamFullInfo.WrittenReportUrl,
                                         WrittenReportDate = teamFullInfo.WrittenReportDate,
                                         Shipping = "",
                                         ResumeUrl = teamFullInfo.ResumeUrl,
                                         Date = teamFullInfo.Date,
                                         EmailConfirmed = teamFullInfo.EmailConfirmed,
                                         LockoutEnabled = teamFullInfo.LockoutEnabled,
                                         Deactivate = teamFullInfo.Deactivate,
                                         SubmitStatus = teamFullInfo.SubmitStatus,
                                         Status = teamFullInfo.Status,
                                         Approved = teamFullInfo.Approved,
                                     };

            return vmTeamFullInfoList;
        }
        public IEnumerable<VmTeamFullInfo> GetTeamFullInfoByFilterByAdvisor(string advisorUserId, VmTeamFullInfo filterItem)
        {
            var viewTeamFullInfoRepository = UnitOfWork.GetRepository<ViewTeamFullInfoRepository>();

            var viewFilterItem = new VmTeamFullInfo
            {
                Name = filterItem.Name,
                Leader = filterItem.Leader,
                Advisor = filterItem.Advisor,
                TaskName = filterItem.TaskName,
                Judges = filterItem.Judges,
                PayStatus = filterItem.PayStatus,
            };

            var viewteamFullInfoList = viewTeamFullInfoRepository.SelectByByAdvisor(advisorUserId, viewFilterItem, 0, int.MaxValue);

            var vmTeamFullInfoList = from teamFullInfo in viewteamFullInfoList
                                     select new VmTeamFullInfo
                                     {
                                         Id = teamFullInfo.Id,
                                         TaskId = teamFullInfo.TaskId,
                                         Name = teamFullInfo.Name,
                                         TaskName = teamFullInfo.TaskName,
                                         TeamImageUrl = teamFullInfo.TeamImageUrl,
                                         TeamState = teamFullInfo.TeamState.Value,
                                         TeamStateDescription = teamFullInfo.TeamState.ToString(),
                                         SafetyStatus = /*teamFullInfo.SafetyStatus*/ false,
                                         PayStatus = teamFullInfo.PayStatus,
                                         PayStatusDescription = (teamFullInfo.PayStatus == true) ? PayStatus.Payed.ToString() : PayStatus.NotPayed.ToString(),
                                         RegistrationStatus = teamFullInfo.RegistrationStatus,
                                         Survey = teamFullInfo.Survey,
                                         Advisor = teamFullInfo.Advisor,
                                         Leader = teamFullInfo.Leader,
                                         Judges = teamFullInfo.Judges,
                                         PhoneNumber = teamFullInfo.PhoneNumber,
                                         Identifier = teamFullInfo.Identifier,
                                         Sex = teamFullInfo.Sex,
                                         BirthDate = teamFullInfo.BirthDate,
                                         UserName = teamFullInfo.UserName,
                                         Email = teamFullInfo.Email,
                                         RegisterDate = teamFullInfo.RegisterDate,
                                         RoleName = teamFullInfo.RoleName,
                                         RoleId = teamFullInfo.RoleId,
                                         UserDefiner = teamFullInfo.UserDefiner,
                                         LastSignIn = teamFullInfo.LastSignIn,
                                         UniversityId = teamFullInfo.UniversityId,
                                         University = teamFullInfo.University,
                                         JacketSizeId = teamFullInfo.JacketSizeId,
                                         JacketSize = teamFullInfo.JacketSize ?? "",
                                         DietTypeId = teamFullInfo.DietTypeId,
                                         DietType = teamFullInfo.DietType ?? "",
                                         StreetLine1 = teamFullInfo.StreetLine1,
                                         StreetLine2 = teamFullInfo.StreetLine2,
                                         City = teamFullInfo.City,
                                         State = teamFullInfo.State,
                                         ZipCode = teamFullInfo.ZipCode,
                                         EmgyPersonRelationship = teamFullInfo.EmgyPersonRelationship,
                                         EmgyPersonPhoneNumber = teamFullInfo.EmgyPersonPhoneNumber,
                                         EmgyPersonLastName = teamFullInfo.EmgyPersonLastName,
                                         EmgyPersonFirstName = teamFullInfo.EmgyPersonFirstName,
                                         ShortBio = teamFullInfo.ShortBio,
                                         T_Shirt_Size = teamFullInfo.T_Shirt_Size,
                                         ProfilePictureUrl = teamFullInfo.ProfilePictureUrl,
                                         UniversityPictureUrl = teamFullInfo.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                                         LabResultUrl = teamFullInfo.LabResultUrl,
                                         WrittenReportUrl = string.IsNullOrWhiteSpace(teamFullInfo.WrittenReportUrl)
                                                ? "-?CT=Stylish_not_ok.png" : "-?CT=Stylish_ok.png",
                                         WrittenReportUrlForMember = teamFullInfo.WrittenReportUrl,
                                         WrittenReportDate = teamFullInfo.WrittenReportDate,
                                         Shipping = "",
                                         ResumeUrl = teamFullInfo.ResumeUrl,
                                         Date = teamFullInfo.Date,
                                         EmailConfirmed = teamFullInfo.EmailConfirmed,
                                         LockoutEnabled = teamFullInfo.LockoutEnabled,
                                         Deactivate = teamFullInfo.Deactivate,
                                         SubmitStatus = teamFullInfo.SubmitStatus,
                                         Status = teamFullInfo.Status,
                                         Approved = teamFullInfo.Approved,
                                     };
            return vmTeamFullInfoList;
        }
        public VmTeamFullInfo GetTeamFullInfoById(int teamId)
        {
            var viewTeamFullInfoRepository = UnitOfWork.GetRepository<ViewTeamFullInfoRepository>();



            var teamFullInfo = viewTeamFullInfoRepository.GetTeamFullInfoById(teamId);

            var vmTeamFullInfoList = new VmTeamFullInfo
            {
                Id = teamFullInfo.Id,
                TaskId = teamFullInfo.TaskId,
                Name = teamFullInfo.Name,
                TaskName = teamFullInfo.TaskName,
                TeamImageUrl = teamFullInfo.TeamImageUrl,
                TeamState = teamFullInfo.TeamState.Value,
                TeamStateDescription = teamFullInfo.TeamState.ToString(),
                SafetyStatus = /*teamFullInfo.SafetyStatus*/ false,
                PayStatus = teamFullInfo.PayStatus,
                PayStatusDescription = (teamFullInfo.PayStatus == true) ? PayStatus.Payed.ToString() : PayStatus.NotPayed.ToString(),
                RegistrationStatus = teamFullInfo.RegistrationStatus,
                Survey = teamFullInfo.Survey,
                Advisor = teamFullInfo.Advisor,
                Leader = teamFullInfo.Leader,
                Judges = teamFullInfo.Judges,
                PhoneNumber = teamFullInfo.PhoneNumber,
                Identifier = teamFullInfo.Identifier,
                Sex = teamFullInfo.Sex,
                BirthDate = teamFullInfo.BirthDate,
                UserName = teamFullInfo.UserName,
                Email = teamFullInfo.Email,
                RegisterDate = teamFullInfo.RegisterDate,
                RoleName = teamFullInfo.RoleName,
                RoleId = teamFullInfo.RoleId,
                UserDefiner = teamFullInfo.UserDefiner,
                LastSignIn = teamFullInfo.LastSignIn,
                UniversityId = teamFullInfo.UniversityId,
                University = teamFullInfo.University,
                JacketSizeId = teamFullInfo.JacketSizeId,
                JacketSize = teamFullInfo.JacketSize ?? "",
                DietTypeId = teamFullInfo.DietTypeId,
                DietType = teamFullInfo.DietType ?? "",
                StreetLine1 = teamFullInfo.StreetLine1,
                StreetLine2 = teamFullInfo.StreetLine2,
                City = teamFullInfo.City,
                State = teamFullInfo.State,
                ZipCode = teamFullInfo.ZipCode,
                EmgyPersonRelationship = teamFullInfo.EmgyPersonRelationship,
                EmgyPersonPhoneNumber = teamFullInfo.EmgyPersonPhoneNumber,
                EmgyPersonLastName = teamFullInfo.EmgyPersonLastName,
                EmgyPersonFirstName = teamFullInfo.EmgyPersonFirstName,
                ShortBio = teamFullInfo.ShortBio,
                T_Shirt_Size = teamFullInfo.T_Shirt_Size,
                ProfilePictureUrl = teamFullInfo.ProfilePictureUrl,
                UniversityPictureUrl = teamFullInfo.UniversityPictureUrl ?? "/Resources/Images/university-empty-pic.png",
                LabResultUrl = teamFullInfo.LabResultUrl,
                WrittenReportUrl = string.IsNullOrWhiteSpace(teamFullInfo.WrittenReportUrl)
                                                ? "-?CT=Stylish_not_ok.png" : "-?CT=Stylish_ok.png",
                WrittenReportUrlForMember = teamFullInfo.WrittenReportUrl,
                WrittenReportDate = teamFullInfo.WrittenReportDate,
                Shipping = "",
                ResumeUrl = teamFullInfo.ResumeUrl,
                Date = teamFullInfo.Date,
                EmailConfirmed = teamFullInfo.EmailConfirmed,
                LockoutEnabled = teamFullInfo.LockoutEnabled,
                Deactivate = teamFullInfo.Deactivate,
                SubmitStatus = teamFullInfo.SubmitStatus,
                Status = teamFullInfo.Status,
                Approved = teamFullInfo.Approved,
            };
            return vmTeamFullInfoList;
        }

    }

}