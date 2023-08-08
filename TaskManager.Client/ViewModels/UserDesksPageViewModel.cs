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

        #region COMMANDS

        public DelegateCommand OpenEditDeskCommand { get; private set; }


        #endregion

        public UserDesksPageViewModel(AuthToken token) 
        {
            _token = token;
            _desksRequestService = new DesksRequestService();
            _usersRequestService = new UsersRequestService();

            OpenEditDeskCommand = new DelegateCommand(OpenEditDesk);

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

        //private void OpenEditDesk(object deskId)
        //{
        //    SelectedDesk = GetDeskClientById(deskId);
        //    if (CurrentUser.Id != SelectedDesk.Model.AdminId)
        //    {
        //        _viewService.ShowMessage("You are not admin");
        //        return;
        //    }

        //    TypeActionWithDesk = ClientAction.Update;

        //    var wnd = new CreateOrUpdateDeskWindow();
        //    _viewService.OpenWindow(wnd, this);
        //}

        #endregion




    }
}
