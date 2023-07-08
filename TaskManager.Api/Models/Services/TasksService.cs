using TaskManager.Api.Models.Abstractions;
using TaskManager.Api.Models.Data;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class TasksService : AbstractionService, ICommonService<TaskModel>
    {
        private readonly ApplicationContext _db;
        public TasksService(ApplicationContext db)
        {
            _db = db;
        }
        public bool Create(TaskModel model)
        {
            bool result = DoAction(delegate ()
            {
                Task newTask = new Task(model);
                _db.Tasks.Add(newTask);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TaskModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, TaskModel model)
        {
            throw new NotImplementedException();
        }
    }
}
