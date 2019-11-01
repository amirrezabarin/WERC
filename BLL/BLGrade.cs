
using BLL.Base;
using Model;
using Model.ToolsModels.DropDownList;
using Model.ViewModels.Grade;
using Model.ViewModels.Grade.Report;
using Model.ViewModels.Team;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLGrade : BLBase
    {
        public IEnumerable<VmSelectListItem> GetGradeSelectListItem(int index, int count)
        {
            var GradeRepository = UnitOfWork.GetRepository<GradeRepository>();

            var gradeList = GradeRepository.Select(index, count);
            var vmSelectListItem = (from grade in gradeList
                                    select new VmSelectListItem
                                    {
                                        Value = grade.Id.ToString(),
                                        Text = grade.Name,
                                    });

            return vmSelectListItem;
        }

        public IEnumerable<VmGrade> GetGradeList(string gradeName = "")
        {
            var gradeRepository = UnitOfWork.GetRepository<GradeRepository>();

            var gradeList = gradeRepository.GetGrades(gradeName);

            var vmGradeList = from grade in gradeList
                              select new VmGrade
                              {
                                  Id = grade.Id,
                                  Name = grade.Name,
                              };

            return vmGradeList;
        }

        public IEnumerable<VmTaskBaseGrade> GetGradeReportList(string judgeUserId)
        {

            var viewTaskTeamRepository = UnitOfWork.GetRepository<ViewTaskTeamRepository>();

            IEnumerable<ViewTaskTeam> taskTeamRepositoryList;

            if (!string.IsNullOrWhiteSpace(judgeUserId))
            {
                taskTeamRepositoryList = viewTaskTeamRepository.GetTaskTeamByJudge(judgeUserId).ToList();
            }
            else
            {
                taskTeamRepositoryList = viewTaskTeamRepository.Select(0, int.MaxValue).ToList();
            }

            var viewTaskRepository = UnitOfWork.GetRepository<ViewTaskRepository>();
            var taskGradeList = viewTaskRepository.GetViewTaskByIds(taskTeamRepositoryList.Select(t => t.TaskId).Distinct().ToArray()).ToList();

            var blTeamGradeDetail = new BLTeamGradeDetail();
            var teamGradeDetailList = blTeamGradeDetail.GetAllTeamGradeDetailByTaskIds(taskTeamRepositoryList.Select(t => t.TaskId).Distinct().ToArray()).ToList();

            var taskBaseGradeList = (from task in taskTeamRepositoryList
                                     group task by new { task.TaskId, task.TaskName } into g
                                     select new VmTaskBaseGrade
                                     {
                                         TaskId = g.Key.TaskId,
                                         TaskName = g.Key.TaskName,

                                         GradeList = (from gt in taskGradeList
                                                      where gt.Id == g.Key.TaskId
                                                      select new VmGrade
                                                      {
                                                          Id = gt.GradeId,
                                                          Name = gt.Grade
                                                      }).ToList(),

                                         TeamGradeList = (from team in g.ToList()
                                                          group team by new { team.TeamId, team.TeamName } into tg
                                                          select new VmTeamGrade
                                                          {
                                                              TeamId = tg.Key.TeamId,
                                                              TeamName = tg.Key.TeamName,
                                                          }).ToList()
                                     }).ToList();


            foreach (var taskBaseGrade in taskBaseGradeList)
            {
                foreach (var team in taskBaseGrade.TeamGradeList)
                {
                    team.GradeReportList = new List<VmGradeReport>();

                    foreach (var grade in taskBaseGrade.GradeList)
                    {
                        team.GradeReportList.Add(new VmGradeReport
                        {
                            GradeId = grade.Id,
                            GradeType = grade.Name,
                            Average = GetAverageTotal(teamGradeDetailList, taskBaseGrade.TaskId, team.TeamId, grade.Id)
                        });
                    }
                }
            }

            return taskBaseGradeList;
        }
        public IEnumerable<VmTaskBaseGrade> GetStudentGradeReportList(string studentUserId, int teamId = -1)
        {

            var viewTaskTeamRepository = UnitOfWork.GetRepository<ViewTaskTeamRepository>();

            IEnumerable<ViewTaskTeam> taskTeamRepositoryList;

            var blTeamMember = new BLTeamMember();
            VmTeamMember teamMember = null;

            if (teamId == -1)
            {
                teamMember = blTeamMember.GetTeamMemberByUserId(studentUserId);
                teamId = teamMember.TeamId;
            }
            else
            {
                teamMember = blTeamMember.GetTeamMemberByUserAndTeamId(studentUserId, teamId);

            }

            var taskId = teamMember.TaskId.Value;

            var blUserTask = new BLUserTask();

            var judgeUserTaskList = blUserTask.GetUsersByTask(taskId);
            taskTeamRepositoryList = viewTaskTeamRepository.GetTaskTeamByJudgesAndTeam(judgeUserTaskList.ToArray(), teamId).ToList();

            var viewTaskRepository = UnitOfWork.GetRepository<ViewTaskRepository>();

            var taskGradeList = viewTaskRepository.GetViewTaskByIds(new int[] { taskId }).ToList();

            var blTeamGradeDetail = new BLTeamGradeDetail();
            var teamGradeDetailList = blTeamGradeDetail.GetAllTeamGradeDetailByTaskIds(taskTeamRepositoryList.Select(t => t.TaskId).Distinct().ToArray()).ToList();

            var taskBaseGradeList = (from task in taskTeamRepositoryList
                                     group task by new { task.TaskId, task.TaskName } into g
                                     select new VmTaskBaseGrade
                                     {
                                         TaskId = g.Key.TaskId,
                                         TaskName = g.Key.TaskName,

                                         GradeList = (from gt in taskGradeList
                                                      where gt.Id == g.Key.TaskId
                                                      select new VmGrade
                                                      {
                                                          Id = gt.GradeId,
                                                          Name = gt.Grade
                                                      }).ToList(),

                                         TeamGradeList = (from team in g.ToList()
                                                          group team by new { team.TeamId, team.TeamName } into tg
                                                          select new VmTeamGrade
                                                          {
                                                              TeamId = tg.Key.TeamId,
                                                              TeamName = tg.Key.TeamName,
                                                          }).ToList()
                                     }).ToList();


            foreach (var taskBaseGrade in taskBaseGradeList)
            {
                foreach (var team in taskBaseGrade.TeamGradeList)
                {
                    team.GradeReportList = new List<VmGradeReport>();

                    foreach (var grade in taskBaseGrade.GradeList)
                    {
                        team.GradeReportList.Add(new VmGradeReport
                        {
                            GradeId = grade.Id,
                            GradeType = grade.Name,
                            Average = GetAverageTotal(teamGradeDetailList, taskBaseGrade.TaskId, team.TeamId, grade.Id)
                        });
                    }
                }
            }

            return taskBaseGradeList;
        }
        public List<VmTaskBaseGrade> GetStudentOtherTeamGradeReportList(string studentUserId, int teamId)
        {

            var viewTaskTeamRepository = UnitOfWork.GetRepository<ViewTaskTeamRepository>();

            IEnumerable<ViewTaskTeam> taskTeamRepositoryList;

            var blTeamMember = new BLTeamMember();
            var teamMember = blTeamMember.GetTeamMemberByUserIdAndTeamId(studentUserId, teamId);
            var taskId = teamMember.TaskId.Value;


            var blUserTask = new BLUserTask();
            var judgeUserTaskList = blUserTask.GetUsersByTask(taskId);

            var bTeam = new BLTeam();
            var teamIdList = bTeam.GetTeamIdsByTask(taskId);


            taskTeamRepositoryList = viewTaskTeamRepository.GetTaskTeamByJudgesAndteamIds(judgeUserTaskList.ToArray(), teamIdList.ToArray()).ToList();

            var viewTaskRepository = UnitOfWork.GetRepository<ViewTaskRepository>();

            var taskGradeList = viewTaskRepository.GetViewTaskByIds(new int[] { taskId }).ToList();

            var blTeamGradeDetail = new BLTeamGradeDetail();
            var teamGradeDetailList = blTeamGradeDetail.GetAllTeamGradeDetailByTaskIds(taskTeamRepositoryList.Select(t => t.TaskId).Distinct().ToArray()).ToList();

            var taskBaseGradeList = (from task in taskTeamRepositoryList
                                     group task by new { task.TaskId, task.TaskName } into g
                                     select new VmTaskBaseGrade
                                     {
                                         TaskId = g.Key.TaskId,
                                         TaskName = g.Key.TaskName,

                                         GradeList = (from gt in taskGradeList
                                                      where gt.Id == g.Key.TaskId
                                                      select new VmGrade
                                                      {
                                                          Id = gt.GradeId,
                                                          Name = gt.Grade
                                                      }).ToList(),

                                         TeamGradeList = (from team in g.ToList()
                                                          group team by new { team.TeamId, team.TeamName } into tg
                                                          select new VmTeamGrade
                                                          {
                                                              TeamId = tg.Key.TeamId,
                                                              TeamName = tg.Key.TeamName,
                                                          }).ToList()
                                     }).ToList();


            foreach (var taskBaseGrade in taskBaseGradeList)
            {
                foreach (var team in taskBaseGrade.TeamGradeList)
                {
                    team.GradeReportList = new List<VmGradeReport>();

                    foreach (var grade in taskBaseGrade.GradeList)
                    {
                        team.GradeReportList.Add(new VmGradeReport
                        {
                            GradeId = grade.Id,
                            GradeType = grade.Name,
                            Average = GetAverageTotal(teamGradeDetailList, taskBaseGrade.TaskId, team.TeamId, grade.Id)
                        });
                    }
                }
            }

            return taskBaseGradeList;
        }

        private double GetAverageTotal(IEnumerable<VmTeamGradeDetail> teamGradeDetailList, int taskId, int teamId, int gradeId)
        {
            var jugeUserList = teamGradeDetailList.Select(t => t.JudgeUserId).Distinct().ToArray();
            var total = 0d;
            int evaluatorJudgeCount = 0;

            foreach (var judgeUser in jugeUserList)
            {
                var judgeUserAllData = teamGradeDetailList.Where(t => t.JudgeUserId == judgeUser && t.TaskId == taskId && t.TeamId == teamId && t.GradeId == gradeId);

                if (judgeUserAllData.Count() > 0)
                {
                    var judgeUserNullDataCount = teamGradeDetailList.Where(t => t.JudgeUserId == judgeUser && t.TaskId == taskId && t.TeamId == teamId && t.GradeId == gradeId && t.Point == null).Count();

                    if (judgeUserNullDataCount != judgeUserAllData.Count())
                    {
                        evaluatorJudgeCount++;
                        total += judgeUserAllData.Sum(t => (t.Point ?? 0) * t.Coefficient);
                    }
                }

            }
            return Math.Round(total / evaluatorJudgeCount, 2);
        }
        public VmGrade GetGradeById(int id)
        {
            var gradeRepository = UnitOfWork.GetRepository<GradeRepository>();

            var grade = gradeRepository.GetGradeById(id);

            var vmGrade = new VmGrade
            {
                Id = grade.Id,
                Name = grade.Name,
            };

            return vmGrade;
        }
        public VmGrade GetGradeWithDetailsById(int id)
        {
            var viewGradeDetailRepository = UnitOfWork.GetRepository<ViewGradeDetailRepository>();

            var gradeDetails = viewGradeDetailRepository.GetGradeDetailsByGrade(id);

            var evaluationItems = string.Join("■", from e in gradeDetails select e.EvaluationItem);
            var points = string.Join("■", from e in gradeDetails select e.Point);
            var coefficients = string.Join("■", from e in gradeDetails select e.Coefficient);

            var vmGrade = new VmGrade
            {
                Id = gradeDetails.First().Id,
                Name = gradeDetails.First().Name,
                EvaluationItems = evaluationItems,
                Points = points,
                Coefficients = coefficients,
            };

            return vmGrade;
        }

        public IEnumerable<VmGrade> GetAllGrade()
        {
            var GradeRepository = UnitOfWork.GetRepository<GradeRepository>();

            var gradeList = GradeRepository.Select(0, int.MaxValue);
            var vmGradeList = from grade in gradeList
                              select new VmGrade
                              {
                                  Id = grade.Id,
                                  Name = grade.Name,
                              };

            return vmGradeList;
        }

        public int CreateGrade(VmGrade vmGrade)
        {
            var result = -1;
            try
            {
                var Points = vmGrade.Points.Split(new char[] { '■' }, StringSplitOptions.RemoveEmptyEntries);
                var Coefficients = vmGrade.Coefficients.Split(new char[] { '■' }, StringSplitOptions.RemoveEmptyEntries);

                var gradeRepository = UnitOfWork.GetRepository<GradeRepository>();

                var newGradeDetailList = new List<GradeDetail>();
                var i = 0;
                foreach (var item in vmGrade.EvaluationItems.Split(new char[] { '■' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    newGradeDetailList.Add(new GradeDetail
                    {
                        EvaluationItem = item,
                        Point = double.Parse(Points[i]),
                        Coefficient = double.Parse(Coefficients[i]),
                    });
                    i++;
                }
                var newGrade = new Grade
                {
                    Id = vmGrade.Id,
                    Name = vmGrade.Name,
                    GradeDetails = newGradeDetailList
                };

                gradeRepository.CreateGrade(newGrade);

                UnitOfWork.Commit();

                result = newGrade.Id;
            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;
        }
        public bool UpdateGrade(VmGrade vmGrade)
        {
            try
            {
                //var viewTeamGradeDetailRepository = UnitOfWork.GetRepository<ViewTeamGradeDetailRepository>();
                //if (viewTeamGradeDetailRepository.GetTeamGradeDetailsCountByGraeId(vmGrade.Id) > 0)
                //{
                //    return false;
                //}

                var gradeRepository = UnitOfWork.GetRepository<GradeRepository>();

                var newGradeDetailList = new List<GradeDetail>();
                var i = 0;
                var Points = vmGrade.Points.Split(new char[] { '■' }, StringSplitOptions.RemoveEmptyEntries);
                var Coefficients = vmGrade.Coefficients.Split(new char[] { '■' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in vmGrade.EvaluationItems.Split(new char[] { '■' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    newGradeDetailList.Add(new GradeDetail
                    {
                        EvaluationItem = item,
                        Point = double.Parse(Points[i]),
                        Coefficient = double.Parse(Coefficients[i]),
                    });
                    i++;
                }

                var updateableGrade = new Grade
                {
                    Id = vmGrade.Id,
                    Name = vmGrade.Name,
                    GradeDetails = newGradeDetailList
                };

                var gradeDetailRepository = UnitOfWork.GetRepository<GradeDetailRepository>();

                gradeDetailRepository.DeleteGradeDetailsByGrade(vmGrade.Id);

                gradeRepository.UpdateGrade(updateableGrade);

                return UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool DeleteGrade(int gradeId)
        {
            try
            {
                var gradeRepository = UnitOfWork.GetRepository<GradeRepository>();

                if (gradeRepository.DeleteGrade(gradeId) == true)
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

    }
}
