using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Client.Services;
using TaskManager.Common.Models;

namespace TaskManager.Client.ViewModels
{
    public class UsersPageViewModel : BindableBase
    {
        private AuthToken _token;
        private UsersRequestService _usersRequestService;
        private CommonViewService _viewService;

        #region COMMANDS

        public DelegateCommand<object> OpenUpdateUserCommand { get; private set; }
        public DelegateCommand OpenNewUserCommand { get; private set; }
        public DelegateCommand<object> DeleteUserCommand { get; private set; }
        public DelegateCommand CreateOrUpdateUserCommand { get; private set; }
        public DelegateCommand OpenSelectUsersFromExcelCommand { get; private set; }
        public DelegateCommand GetUsersFromExcelCommand { get; private set; }
        public DelegateCommand AddUsersFromExcelCommand { get; private set; }

        #endregion

        public UsersPageViewModel(AuthToken token)
        { 
            _token = token;
            _usersRequestService = new UsersRequestService();
            _viewService = new CommonViewService();

            OpenUpdateUserCommand = new DelegateCommand<object>(OpenUpdateUser);
            OpenNewUserCommand = new DelegateCommand(OpenNewUser);
            DeleteUserCommand = new DelegateCommand<object>(DeleteUser);
            CreateOrUpdateUserCommand = new DelegateCommand(CreateOrUpdateUser);
            OpenSelectUsersFromExcelCommand = new DelegateCommand(OpenSelectUsersFromExcel);
            GetUsersFromExcelCommand = new DelegateCommand(GetUsersFromExcel);
            AddUsersFromExcelCommand = new DelegateCommand(AddUsersFromExcel);
        }

        #region PROPERTIES

        public List<UserModel> AllUsers
        {
            get => _usersRequestService.GetAllUsers(_token);
        }

        private List<UserModel> _selectedUsersFromExcel;

        public List<UserModel> SelectedUsersFromExcel
        {
            get => _selectedUsersFromExcel; 
            set 
            { 
                _selectedUsersFromExcel = value; 
                RaisePropertyChanged(nameof(SelectedUsersFromExcel));
            }
        }

        private UserModel _selectedUser;

        public UserModel SelectedUser
        {
            get => _selectedUser;
            set 
            { 
                _selectedUser = value; 
            }
        }



        #endregion

        #region METHODS

        private void OpenUpdateUser(object userId)
        {

        }

        private void OpenNewUser()
        {

        }

        private void DeleteUser(object userId)
        {

        }

        private void CreateOrUpdateUser()
        {

        }

        private void OpenSelectUsersFromExcel()
        {

        }

        private void GetUsersFromExcel()
        {

        }

        private void AddUsersFromExcel()
        {

        }

        #endregion
    }
}
