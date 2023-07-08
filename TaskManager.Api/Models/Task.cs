using Newtonsoft.Json;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models
{
    public class Task : CommonObject
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[]? File { get; set; }
        public int? DeskId { get; set; }
        public Desk? Desk { get; set; }
        public string? Column { get; set; }
        public int? CreatorId { get; set; }
        public User Creator { get; set; }
        public int? ExecutorId { get; set; }

        public Task()
        {

        }

        public Task(DeskModel taskModel) : base(taskModel)
        {
            Id = taskModel.Id;
            AdminId = taskModel.AdminId;
            IsPrivate = taskModel.IsPrivate;
            ProjectId = taskModel.ProjectId;

            if (taskModel.Columns.Any())
            {
                Column = JsonConvert.SerializeObject(taskModel.Columns);
            }
        }

        public TaskModel ToDto()
        {
            return new TaskModel()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CreationDate = this.CreationDate,
                Photo = this.Photo,
                AdminId = this.AdminId,
                IsPrivate = this.IsPrivate,
                Columns = JsonConvert.DeserializeObject<string[]>(this.Columns),
                ProjectId = this.ProjectId
            };
        }
    }

}