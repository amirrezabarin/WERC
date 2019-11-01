using BLL.Base;
using Model;
using Model.ToolsModels.DropDownList;
using Model.ViewModels.Task;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLTask : BLBase
    {
        public IEnumerable<VmSelectListItem> GetTaskSelectListItem(int index, int count)
        {
            var taskRepository = UnitOfWork.GetRepository<TaskRepository>();

            var taskList = taskRepository.Select(index, count);
            var vmSelectListItem = (from task in taskList
                                    select new VmSelectListItem
                                    {
                                        Value = task.Id.ToString(),
                                        Text = task.Name,
                                    });

            return vmSelectListItem;
        }
        public IEnumerable<VmSelectListItem> GetLabUserTaskSelectListItem(string userId)
        {
            var taskRepository = UnitOfWork.GetRepository<ViewUserTaskRepository>();

            var taskList = taskRepository.GetTasksByUser(userId);
            var vmSelectListItem = (from task in taskList
                                    select new VmSelectListItem
                                    {
                                        Value = task.TaskId.ToString(),
                                        Text = task.TaskName + ": " + task.TaskDescription,
                                    });

            return vmSelectListItem;
        }
        public IEnumerable<VmSelectListItem> GetTaskSelectListItemWithDescription(int index, int count)
        {
            var taskRepository = UnitOfWork.GetRepository<TaskRepository>();

            var taskList = taskRepository.Select(index, count);
            var vmSelectListItem = (from task in taskList
                                    select new VmSelectListItem
                                    {
                                        Value = task.Id.ToString(),
                                        Text = task.Name + " : " + task.Description,
                                    });

            return vmSelectListItem;
        }
        public IEnumerable<VmTask> GetTaskList(string taskName = "")
        {
            var viewTaskRepository = UnitOfWork.GetRepository<ViewTaskRepository>();

            var taskList = viewTaskRepository.GetViewTasks(taskName);

            var vmTaskList = from task in taskList
                             group new { task.GradeId, task.Grade } by new
                             {
                                 task.Id,
                                 task.Name,
                                 task.ImageUrl,
                                 task.Description,
                             }
                             into taskGroup
                             select new VmTask
                             {
                                 Id = taskGroup.Key.Id,
                                 Name = taskGroup.Key.Name,
                                 ImageUrl = taskGroup.Key.ImageUrl,
                                 GradeIds = (from g in taskGroup select g.GradeId).ToArray(),
                                 Grades = (from g in taskGroup select g.Grade).ToArray(),
                                 Description = taskGroup.Key.Description,
                             };

            var viewTestRepository = UnitOfWork.GetRepository<ViewTestRepository>();

            var testList = viewTestRepository.GetViewTests(taskName);

            foreach (var item in vmTaskList)
            {
                item.Tests = testList.Where(t => t.TaskId == item.Id).Select(t => t.Task).ToArray();
                item.TestIds = testList.Where(t => t.TaskId == item.Id).Select(t => t.Id).ToArray();
            }

            return vmTaskList;
        }
        public VmTask GetTaskById(int id)
        {
            var viewTaskRepository = UnitOfWork.GetRepository<ViewTaskRepository>();

            var taskList = viewTaskRepository.GetViewTaskById(id);

            var vmTask = (from task in taskList
                          group new { task.GradeId, task.Grade } by new
                          {
                              task.Id,
                              task.Name,
                              task.ImageUrl,
                              task.Description,
                          }
                             into taskGroup
                          select new VmTask
                          {
                              Id = taskGroup.Key.Id,
                              Name = taskGroup.Key.Name,
                              ImageUrl = taskGroup.Key.ImageUrl,
                              GradeIds = (from g in taskGroup select g.GradeId).ToArray(),
                              Grades = (from g in taskGroup select g.Grade).ToArray(),
                              Description = taskGroup.Key.Description,
                          }).SingleOrDefault();

            var viewTestRepository = UnitOfWork.GetRepository<ViewTestRepository>();

            var testList = viewTestRepository.GetViewTestByTask(taskList.First().Id);


            vmTask.Tests = testList.Where(t => t.TaskId == vmTask.Id).Select(t => t.Task).ToArray();
            vmTask.TestIds = testList.Where(t => t.TaskId == vmTask.Id).Select(t => t.Id).ToArray();

            return vmTask;
        }
        public IEnumerable<VmTask> GetAllTask()
        {
            var viewTaskRepository = UnitOfWork.GetRepository<ViewTaskRepository>();

            var taskList = viewTaskRepository.Select(0, int.MaxValue);

            var vmTaskList = from task in taskList
                             group new { task.GradeId, task.Grade } by new
                             {
                                 task.Id,
                                 task.Name,
                                 task.ImageUrl,
                                 task.Description,
                             }
                             into taskGroup
                             select new VmTask
                             {
                                 Id = taskGroup.Key.Id,
                                 Name = taskGroup.Key.Name,
                                 ImageUrl = taskGroup.Key.ImageUrl,
                                 GradeIds = (from g in taskGroup select g.GradeId).ToArray(),
                                 Grades = (from g in taskGroup select g.Grade).ToArray(),
                                 Description = taskGroup.Key.Description,
                             };

            var viewTestRepository = UnitOfWork.GetRepository<ViewTestRepository>();

            var testList = viewTestRepository.GetViewTests();

            foreach (var item in vmTaskList)
            {
                item.Tests = testList.Where(t => t.TaskId == item.Id).Select(t => t.Task).ToArray();
                item.TestIds = testList.Where(t => t.TaskId == item.Id).Select(t => t.Id).ToArray();
            }

            return vmTaskList;
        }
        public int CreateTask(VmTask vmTask)
        {
            var result = -1;
            try
            {
                var taskRepository = UnitOfWork.GetRepository<TaskRepository>();

                var gradeList = new List<TaskGrade>();

                foreach (var item in vmTask.ClientGradeIds.Split(','))
                {
                    gradeList.Add(new TaskGrade { TaskId = vmTask.Id, GradeId = int.Parse(item) });
                }

                var newTask = new Task
                {
                    Id = vmTask.Id,
                    Name = vmTask.Name,
                    ImageUrl = vmTask.ImageUrl,
                    TaskGrades = gradeList,
                    Description = vmTask.Description,
                };

                taskRepository.CreateTask(newTask);

                UnitOfWork.Commit();

                result = newTask.Id;
            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;
        }
        public bool UpdateTask(VmTask vmTask)
        {
            try
            {
                var taskRepository = UnitOfWork.GetRepository<TaskRepository>();
                var taskGradeRepository = UnitOfWork.GetRepository<TaskGradeRepository>();
                var taskTestRepository = UnitOfWork.GetRepository<TaskTestRepository>();

                taskGradeRepository.DeleteTasksGrades(vmTask.Id);
                taskTestRepository.DeleteTasksTests(vmTask.Id);

                var gradeList = new List<TaskGrade>();

                foreach (var item in vmTask.ClientGradeIds.Split(','))
                {
                    gradeList.Add(new TaskGrade { TaskId = vmTask.Id, GradeId = int.Parse(item) });
                }

                var testList = new List<TaskTest>();

                foreach (var item in vmTask.ClientTestIds.Split(','))
                {
                    testList.Add(new TaskTest { TaskId = vmTask.Id, TestId = int.Parse(item) });
                }

                var updateableTask = new Task
                {
                    Id = vmTask.Id,
                    Name = vmTask.Name,
                    ImageUrl = vmTask.ImageUrl,
                    TaskGrades = gradeList,
                    Description = vmTask.Description,
                    TaskTests = testList,
                };

                taskRepository.UpdateTask(updateableTask);

                return UnitOfWork.Commit();
            }
            catch
            {
                return false;
            }

        }
        public bool DeleteTask(int taskId)
        {
            try
            {
                var TaskRepository = UnitOfWork.GetRepository<TaskRepository>();


                if (TaskRepository.DeleteTask(taskId) == true)
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
        public IEnumerable<VmTaskFullInfo> GetTaskFullInfoByFilter(VmTaskFullInfo filterItem)
        {
            var viewTaskFullInfoRepository = UnitOfWork.GetRepository<ViewTaskFullInfoRepository>();

            var viewFilterItem = new ViewTaskFullInfo
            {
                Name = filterItem.Name,
                Grades = filterItem.Grades,
                Judges = filterItem.Judges,
                Description = filterItem.Description,
            };

            var viewtaskFullInfoList = viewTaskFullInfoRepository.Select(viewFilterItem, 0, int.MaxValue);

            var vmTaskFullInfoList = from taskFullInfo in viewtaskFullInfoList
                                     select new VmTaskFullInfo
                                     {
                                         Id = taskFullInfo.Id,
                                         Name = taskFullInfo.Name,
                                         Grades = taskFullInfo.Grades,
                                         Judges = taskFullInfo.Judges,
                                         Description = taskFullInfo.Description,
                                         ImageUrl = taskFullInfo.ImageUrl,
                                     };
            return vmTaskFullInfoList;
        }

    }
}
