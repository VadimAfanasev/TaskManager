using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Client.Services;
using TaskManager.Client.Views.AddWindows;
using TaskManager.Common.Models;

namespace TaskManager.Client.ViewModels
{
    class UserDesksPageViewModel : BindableBase
    {
        private AuthToken _token;
        private DesksRequestService _desksRequestService;
        private UsersRequestService _usersRequestService;
        private DesksViewService _desksViewService;

        #region COMMANDS

        public DelegateCommand OpenEditDeskCommand { get; private set; }
        public DelegateCommand CreateOrUpdateDeskCommand { get; private set; }
        public DelegateCommand DeleteDeskCommand { get; private set; }
        public DelegateCommand SelectPhotoForDeskCommand { get; private set; }
        public DelegateCommand AddNewColumnItemCommand { get; private set; }
        public DelegateCommand<object> RemoveColumnItemCommand { get; private set; }


        #endregion

        public UserDesksPageViewModel(AuthToken token) 
        {
            _token = token;
            _desksRequestService = new DesksRequestService();
            _usersRequestService = new UsersRequestService();
            _desksViewService = new DesksViewService(_token, _desksRequestService);

            OpenEditDeskCommand = new DelegateCommand(OpenUpdateDesk);
            CreateOrUpdateDeskCommand = new DelegateCommand(UpdateDesk);
            SelectPhotoForDeskCommand = new DelegateCommand(SelectPhotoForDesk);

            ContextMenuCommands.Add("Edit", OpenEditDeskCommand);
        }

        #region PROPERTIES
        public List<ModelClient<DeskModel>> AllDesks
        {
            get => _desksRequestService.GetAllDesks(_token).Select(desk => new ModelClient<DeskModel>(desk)).ToList();
        }

        private ModelClient<DeskModel> _selectedDesk;
        public ModelClient<DeskModel> SelectedDesk
        {
            get => _selectedDesk;
            set
            {
                _selectedDesk = value;
                RaisePropertyChanged(nameof(SelectedDesk));
            }
        }

        private Dictionary<string, DelegateCommand> _contextMenuCommands = new Dictionary<string, DelegateCommand>();
        public Dictionary<string, DelegateCommand> ContextMenuCommands
        {
            get => _contextMenuCommands;
            set 
            { 
                _contextMenuCommands = value; 
                RaisePropertyChanged(nameof(ContextMenuCommands));
            }
        }

        #endregion


        #region METHODS

        private void OpenUpdateDesk()
        {
            SelectedDesk = _desksViewService.GetDeskClientById(SelectedDesk.Model.Id);
            _desksViewService.OpenViewDeskInfo(SelectedDesk.Model.Id, this);
        }

        private void UpdateDesk()
        {
            _desksViewService.UpdateDesk(SelectedDesk.Model);
        }

        

        #endregion




    }
}
