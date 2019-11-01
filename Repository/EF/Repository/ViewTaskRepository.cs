using Model;
using Repository.EF.Base;
using System.Collections.Generic;
using System.Linq;

namespace Repository.EF.Repository
{
    public class ViewTaskRepository : EFBaseRepository<ViewTask>
    {
        public IEnumerable<ViewTask> Select(int index, int count)
        {
            var ViewTaskList = from ViewTask in Context.ViewTasks
                               select ViewTask;

            return ViewTaskList.OrderBy(A => A.Name).Skip(index).Take(count).ToArray();
        }
        public IEnumerable<ViewTask> GetViewTasks(string taskName = "")
        {

            var taskList = from task in Context.ViewTasks
                           select task;

            if (taskName != "")
            {
                taskList = taskList.Where(t => t.Name.Contains(taskName));
            }

            return taskList.ToArray();
        }

        public IEnumerable<ViewTask> GetViewTaskById(int id)
        {

            return Context.ViewTasks.Where(t => t.Id == id);
        }

        public IEnumerable<ViewTask> GetViewTaskByIds(int[] ids)
        {
            return Context.ViewTasks.Where(t => ids.Contains(t.Id));
        }
    }
}
