using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Base.Repository.Serialize;

namespace TaskVlopper.Logic
{
    public class TaskLogic : ITaskLogic
    {

        private readonly ITaskRepository TaskRepository;
        private readonly IUserTaskAssignmentRepository UserTaskAssignmentRepository;
        private readonly ITaskSerialize Serializer;
        private readonly IProjectRepository ProjectRepository;

        public TaskLogic(ITaskRepository taskRepository, IUserTaskAssignmentRepository userTaskAssignmentRepository,
            ITaskSerialize serializer, IProjectRepository projectRepository)
        {
            TaskRepository = taskRepository;
            UserTaskAssignmentRepository = userTaskAssignmentRepository;
            Serializer = serializer;
            ProjectRepository = projectRepository;
        }


        public IEnumerable<Base.Model.Task> GetAllTasksForGivenProjectAndCurrentUser(int projectId, string userId)
        {
            var assignments = UserTaskAssignmentRepository.GetTaskAssignmentByUserIdAndProjectId(userId, projectId);

            List<Base.Model.Task> taskList = new List<Base.Model.Task>();
            foreach (var assign in assignments)
            {
                taskList.Add(TaskRepository.GetTaskByIdWithTracking(assign.TaskID));
            }

            return taskList.AsEnumerable();
        }

        public void HandleTaskAdd(Base.Model.Task task, int projectId, string userId)
        {
            //Base.Model.Task task = Serializer.Serialize(form);
            task.ProjectID = projectId;
            TaskRepository.Add(task);

            UserTaskAssignment assignment = new UserTaskAssignment();
            assignment.UserID = userId;
            assignment.TaskID = task.ID;
            UserTaskAssignmentRepository.Add(assignment);
        }

        public void HandleTaskDelete(int projectId, int id, string userId)
        {
            Base.Model.Task task = TaskRepository.GetTaskByIdWithTracking(id);
            TaskRepository.Remove(task);

            UserTaskAssignment assignment = UserTaskAssignmentRepository.
                GetTaskAssignmentByUserIdAndProjectIdAndTaskId(userId, projectId, id);
            UserTaskAssignmentRepository.Remove(assignment);
        }

        public void HandleTaskEdit(Base.Model.Task task, int projectId, int id)
        {
            //Base.Model.Task task = Serializer.Serialize(form);

            TaskRepository.Update(task);
        }

        public Base.Model.Task HandleTaskGet(int projectId, int id)
        {
            return TaskRepository.GetTaskByIdWithoutTracking(id);
        }
    }
}
