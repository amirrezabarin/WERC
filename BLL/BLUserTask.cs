using BLL.Base;
using Model;
using Model.ViewModels.Task;
using Repository.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLUserTask : BLBase
    {

        public IEnumerable<VmTask> GetUserTasksByUser(string id)
        {
            var viewUserTaskRepository = UnitOfWork.GetRepository<ViewUserTaskRepository>();

            var viewUserTaskList = viewUserTaskRepository.GetTasksByUser(id);
            var vmTaskList = from Task in viewUserTaskList
                             select new VmTask
                             {
                                 Id = Task.TaskId,
                                 Name = Task.TaskName,
                                 ImageUrl = Task.ImageUrl,
                             };

            return vmTaskList;
        }

        public IEnumerable<string> GetUsersByTask(int taskId)
        {
            var viewUserTaskRepository = UnitOfWork.GetRepository<ViewUserTaskRepository>();
            var userIds = viewUserTaskRepository.GetUsersByTask(taskId);


            return userIds;
        }

        public IEnumerable<VmUserTask> GetUserTasksByUsers(string[] ids)
        {
            var viewUserTaskRepository = UnitOfWork.GetRepository<ViewUserTaskRepository>();

            var viewUserTaskList = viewUserTaskRepository.GetTasksByUsers(ids);
            var vmTaskList = from Task in viewUserTaskList
                             select new VmUserTask
                             {
                                 Id = Task.TaskId,
                                 Name = Task.Name,
                                 TaskName = Task.TaskName,
                                 UserId = Task.UserId
                             };

            return vmTaskList;
        }

        public bool AssignTasksToUser(string userId, int[] taskIds)
        {
            try
            {
                var userTaskRepository = UnitOfWork.GetRepository<UserTaskRepository>();

                var newAssignedTasks = new List<int>();

                var oldUserTasks = userTaskRepository.GetUserTasks(userId);

                if (taskIds != null)
                {
                    foreach (var item in taskIds)
                    {
                        if (oldUserTasks.Where(a => a.Id == item).Count() == 0)
                        {
                            newAssignedTasks.Add(item);
                        }
                    }
                }

                foreach (var item in oldUserTasks)
                {
                    if (taskIds != null && taskIds.Contains(item.Id) == false)
                    {
                        userTaskRepository.DeleteTasksUser(item.Id);
                    }
                    else
                    if (taskIds == null)
                    {
                        userTaskRepository.DeleteTasksUser(item.Id);
                    }
                }

                if (newAssignedTasks.Count > 0)
                {
                    foreach (var item in newAssignedTasks)
                    {
                        userTaskRepository.CreateTasksUser(
                            new UserTask
                            {
                                TaskId = item,
                                UserId = userId,
                            });
                    }

                }

                UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}