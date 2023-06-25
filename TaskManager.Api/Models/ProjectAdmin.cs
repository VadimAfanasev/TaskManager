namespace TaskManager.Api.Models
{
    public class ProjectAdmin
    {
        public int Id { get; set; }
        public int UsertId { get; set; }
        public User User { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public Project Admin(User user)
        {
            User.Id = user.Id;
            User = user;
        }
    }
}
