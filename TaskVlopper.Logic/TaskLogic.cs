using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Enums;
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
        private readonly IWorklogRepository WorklogRepository;

        public TaskLogic(ITaskRepository taskRepository, IUserTaskAssignmentRepository userTaskAssignmentRepository,
            ITaskSerialize serializer, IProjectRepository projectRepository, IWorklogRepository worklogRepository)
        {
            TaskRepository = taskRepository;
            UserTaskAssignmentRepository = userTaskAssignmentRepository;
            Serializer = serializer;
            ProjectRepository = projectRepository;
            WorklogRepository = worklogRepository;
        }


        public IEnumerable<Base.Model.Task> GetAllTasksForGivenProjectAndCurrentUser(int projectId, string userId)
        {
            return UserTaskAssignmentRepository.GetTaskAssignmentByUserIdAndProjectId(userId, projectId)
                .Select(assingment => TaskRepository.GetTaskByIdWithTracking(assingment.TaskID));
        }

        #region Voids

        public void HandleTaskAdd(Base.Model.Task task, int projectId, string userId)
        {
            //Base.Model.Task task = Serializer.Serialize(form);
            task.ProjectID = projectId;
            TaskRepository.Add(task);

            UserTaskAssignment assignment = new UserTaskAssignment();
            assignment.UserID = userId;
            assignment.TaskID = task.ID;
            assignment.ProjectID = projectId;
            UserTaskAssignmentRepository.Add(assignment);
        }

        public void HandleTaskDelete(int projectId, int id, string userId)
        {
            Base.Model.Task task = TaskRepository.GetTaskByIdWithTracking(id);
            TaskRepository.Remove(task);

            var assignment = UserTaskAssignmentRepository.
                GetTaskAssignmentByTaskId(id);

            UserTaskAssignmentRepository.RemoveMany(assignment);
        }

        public void HandleTaskEdit(Base.Model.Task task, int projectId, int id)
        {
            task.ProjectID = projectId;
            task.ID = id;
            TaskRepository.Update(task);
        }

        public void AssignUserToProjectTask(int projectId, int taskId, string userId)
        {
            UserTaskAssignment assignment = new UserTaskAssignment();
            assignment.TaskID = taskId;
            assignment.UserID = userId;
            assignment.ProjectID = projectId;
            UserTaskAssignmentRepository.Add(assignment);
        }

        public void UnassignUserFromTask(int id, string userId)
        {
            var model = UserTaskAssignmentRepository.GetAll()
                .Where(x => x.TaskID == id && x.UserID == userId);
            if (model.Any())
            {
                UserTaskAssignmentRepository.Remove(model.Single());
            }
        }

        #endregion

        public Base.Model.Task HandleTaskGet(int projectId, int id)
        {
            return TaskRepository.GetTaskByIdWithoutTracking(id);
        }

        public IEnumerable<string> GetAllUsersForGivenTask(int projectId, int taskId)
        {
            return UserTaskAssignmentRepository.GetAllUsersIDsForGivenTaskProject(projectId, taskId);
        }

        public int CountAllTasksForGivenProjectAndCurrentUser(int projectId, string userId)
        {
            return GetAllTasksForGivenProjectAndCurrentUser(projectId, userId).Count();
        }

        public IEnumerable<string> GetTaskStatuses()
        {
            List<string> statuses = new List<string>();
            foreach (var status in Enum.GetValues(typeof(TaskStatusEnum)))
                statuses.Add(status.ToString());

            return statuses;
        }

        public int GetHoursWorkedOnTask(int projectId, int taskId)
        {
            return WorklogRepository.GetAll().Where(x => x.TaskID == taskId).Sum(x => x.Hours);
        }

        public int GetTaskProgress(int projectId)
        {
            if (!TaskRepository.GetTasksForGivenProjectId(projectId).Any())
                return 0;

            var allTasks = TaskRepository.GetTasksForGivenProjectId(projectId);
            var finishedTasks = allTasks.Where(y => y.Status == Base.Enums.TaskStatusEnum.Closed
                                        || y.Status == Base.Enums.TaskStatusEnum.Resolved
                                        || y.Status == Base.Enums.TaskStatusEnum.Removed)
                                    .Count();

            return finishedTasks / allTasks.Count() * 100;
        }
    }
}
