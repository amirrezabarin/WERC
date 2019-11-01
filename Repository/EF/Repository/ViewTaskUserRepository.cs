using Model;
using Repository.EF.Base;
using System.Collections.Generic;
using System.Linq;

namespace Repository.EF.Repository
{
    public class ViewUserTaskRepository : EFBaseRepository<ViewUserTask>
    {
        public IEnumerable<ViewUserTask> GetTasksByUser(string userId)
        {
            var viewUserTaskList = from ut in Context.ViewUserTasks
                                   where ut.UserId == userId
                                   select ut;

            return viewUserTaskList.ToArray();
        }

        public IEnumerable<string> GetUsersByTask(int taskId)
        {
            var viewUserTaskList = from ut in Context.ViewUserTasks
                                   where ut.TaskId == taskId
                                   select ut;

            return viewUserTaskList.Select(u => u.UserId).ToArray();
        }

        public IEnumerable<ViewUserTask> GetTasksByUsers(string[] userIds)
        {
            var viewUserTaskList = from ut in Context.ViewUserTasks
                                   where userIds.Contains(ut.UserId)
                                   select ut;

            return viewUserTaskList.ToArray();
        }

    }
}
