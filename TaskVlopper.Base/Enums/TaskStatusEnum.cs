using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskVlopper.Base.Enums
{
    public static class Extension
    {
        public static TaskStatusEnum Parse(this TaskStatusEnum enumeration, string input)
        {
            if (input == "Active" || int.Parse(input) == (int)TaskStatusEnum.Active)
                return TaskStatusEnum.Active;
            if (input == "Started" || int.Parse(input) == (int)TaskStatusEnum.Started)
                return TaskStatusEnum.Started;
            if (input == "Resolved" || int.Parse(input) == (int)TaskStatusEnum.Resolved)
                return TaskStatusEnum.Resolved;
            if (input == "Closed" || int.Parse(input) == (int)TaskStatusEnum.Closed)
                return TaskStatusEnum.Closed;
            if (input == "Removed" || int.Parse(input) == (int)TaskStatusEnum.Removed)
                return TaskStatusEnum.Removed;

            throw new ArgumentException("TaskStatusEnum.Parse input argument invalid!");
        }
    }

    public enum TaskStatusEnum
    {
        Active = 0,
        Started = 1,
        Resolved = 2,
        Closed = 3,
        Removed = 4
    }
}
