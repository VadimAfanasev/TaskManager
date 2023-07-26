using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TaskManager.Client.Models;
using TaskManager.Client.Services;
using TaskManager.Common.Models;

namespace TaskManager.Client.ViewModels
{
    public class ProjectsPageViewModel : BindableBase
    {
        private AuthToken _token;
        private UsersRequestService _usersRequestService;
        private ProjectsRequestService _projectsRequestService;
        private CommonViewService _viewService;

        #region COMMANDS

        public DelegateCommand OpenNewProjectCommand;
        public DelegateCommand<object> OpenUpdateProjectCommand;
        public DelegateCommand<object> ShowProjectInfoCommand;    

        #endregion

        public ProjectsPageViewModel(AuthToken token)
        {
            _viewService = new CommonViewService();
            _usersRequestService = new UsersRequestService();
            _projectsRequestService = new ProjectsRequestService();

            _token = token;

            OpenNewProjectCommand = new DelegateCommand(OpenNewProject);
            OpenUpdateProjectCommand = new DelegateCommand<object>(OpenUpdateProject);
            ShowProjectInfoCommand = new DelegateCommand<object>(ShowProjectInfo);
        }

        #region PROPERTIES

        public List<ModelClient<ProjectModel>> UserProjects
        {
            get => _projectsRequestService.GetAllProjects(_token).Select(project => new ModelClient<ProjectModel>(project)).ToList();
        }

        private ModelClient<ProjectModel> _selectedProject;

        public ModelClient<ProjectModel> SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                RaisePropertyChanged(nameof(SelectedProject));
    
                if ( SelectedProject.Model.AllUsersIds != null || SelectedProject.Model.AllUsersIds.Count > 0) 
                    ProjectUsers = SelectedProject.Model.AllUsersIds?.Select(userId => _usersRequestService.GetUserById(_token, userId)).ToList();
            }
        }

        private List<UserModel> _projectUsers = new List<UserModel>();

        public List<UserModel> ProjectUsers
        {
            get => _projectUsers;
            set 
            { 
                _projectUsers = value; 
                RaisePropertyChanged(nameof(ProjectUsers));
            }
        }


        #endregion

        #region METHODS
        private void OpenNewProject()
        {
            _viewService.ShowMessage(nameof(OpenNewProject));
        }
        private void OpenUpdateProject(object param)
        {
            var selectedProject = param as ModelClient<ProjectModel>;
            SelectedProject = selectedProject;

            _viewService.ShowMessage(nameof(OpenUpdateProject));
        }
        private void ShowProjectInfo(object param)
        {
            var selectedProject = param as ModelClient<ProjectModel>;
            SelectedProject = selectedProject;

            _viewService.ShowMessage(nameof(ShowProjectInfo));
        }

        #endregion
    }
}
