using TaskManager.Api.Models.Abstractions;
using TaskManager.Api.Models.Data;
using TaskManager.Common.Models;

namespace TaskManager.Api.Models.Services
{
    public class ProjectsService : AbstractionService, ICommonService<ProjectModel>
    {
        private readonly ApplicationContext _db;
        public ProjectsService(ApplicationContext db)
        {
            _db = db;
        }
        public bool Create(ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = new Project(model);
                _db.Projects.Add(newProject);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(x=> x.Id == id);
                _db.Projects.Remove(newProject);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Update(int id, ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(x => x.Id == id);
                newProject.Name = newProject.Name;
                newProject.Description = newProject.Description;
                newProject.Photo = newProject.Photo;
                newProject.Status = newProject.Status;
                newProject.AdminId = newProject.AdminId;

                _db.Projects.Update(newProject);
                _db.SaveChanges();
            });
            return result;
        }
    }
}
