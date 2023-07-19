using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Threading.Tasks;
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
            bool result = DoAction(delegate ()
            {
                Task task = _db.Tasks.FirstOrDefault(x => x.Id == id);
                _db.Tasks.Remove(task);
                _db.SaveChanges();
            });
            return result;
        }

        public TaskModel Get(int id)
        {
            Task task = _db.Tasks.FirstOrDefault(x => x.Id == id);
            return task?.ToDto();
        }

        public IQueryable<TaskModel> GetTasksForUser(int userId)
        {
            return _db.Tasks.Where(x => x.CreatorId == userId || x.ExecutorId == userId).Select(x=>x.ToDto());
        }

        public bool Update(int id, TaskModel model)
        {
            bool result = DoAction(delegate ()
            {
                Task task = _db.Tasks.FirstOrDefault(x => x.Id == id);

                task.Name = model.Name;
                task.Description = model.Description;
                task.Photo = model.Photo;
                task.StartDate = model.CreationDate;
                task.EndDate = model.EndDate;
                task.File = model.File;
                task.Column = model.Column;
                task.ExecutorId = model.ExecutorId;

                _db.Tasks.Update(task);
                _db.SaveChanges();
            });
            return result;
        }

        public IQueryable<TaskModel> GetAll(int deskId)
        {
            return _db.Tasks.Where(d => d.DeskId == deskId).Select(x => x.ToShortDto());
        }
    }
}
