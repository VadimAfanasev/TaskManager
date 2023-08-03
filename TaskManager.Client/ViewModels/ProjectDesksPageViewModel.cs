using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Client.Models;
using TaskManager.Client.Services;
using TaskManager.Client.Views.AddWindows;
using TaskManager.Client.Views.Pages;
using TaskManager.Common.Models;

namespace TaskManager.Client.ViewModels
{
    internal class ProjectDesksPageViewModel : BindableBase
    {
        private CommonViewService _viewService;
        private DesksRequestService _desksRequestService;

        #region COMMANDS
        public DelegateCommand OpenNewDeskCommand { get; private set; }


        #endregion

        public ProjectDesksPageViewModel(AuthToken token, ProjectModel project)
        {
            _token = token;
            _project = project;

            _desksRequestService = new DesksRequestService();

            ProjectDesks = GetDesks(_project.Id);

            OpenNewDeskCommand = new DelegateCommand(OpenNewDesk);
        }

        #region PROPERTIES

        private AuthToken _token;
        private ProjectModel _project;

        private List<ModelClient<DeskModel>> _projectDesks = new List<ModelClient<DeskModel>>();

        public List<ModelClient<DeskModel>> ProjectDesks
        {
            get => _projectDesks;
            set 
            { 
                _projectDesks = value; 
                RaisePropertyChanged(nameof(ProjectDesks));
            }
        }

        #endregion

        #region METHODS

        private List<ModelClient<DeskModel>> GetDesks(int projectId)
        {
            var result = new List<ModelClient<DeskModel>>();
            var desks = _desksRequestService.GetDeskByProject(_token, _project.Id);
            if (desks != null)
            {
                result = desks.Select(d => new ModelClient<DeskModel>(d)).ToList();
            }
            return result;
            
        }

        private void OpenNewDesk()
        {

        }

        #endregion

    }
}
