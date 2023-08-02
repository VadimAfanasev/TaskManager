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



        #endregion

        public ProjectDesksPageViewModel(AuthToken token, ProjectModel project)
        {
            _token = token;
            _project = project;

            ProjectDesks = GetDesks(_project.Id);
        }

        private AuthToken _token;
        private ProjectModel _project;

        private List<ModelClient<DeskModel>> _projectDesks;

        public List<ModelClient<DeskModel>> ProjectDesks
        {
            get => _projectDesks;
            set 
            { 
                _projectDesks = value; 
                RaisePropertyChanged(nameof(ProjectDesks));
            }
        }

        private List<ModelClient<DeskModel>> GetDesks(int projectId)
        {
            return _desksRequestService.GetDeskByProject(_token, _project.Id).Select(d => new ModelClient<DeskModel>(d)).ToList();
        }

    }
}
