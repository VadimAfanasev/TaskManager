using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Client.Services;
using TaskManager.Common.Models;

namespace TaskManager.Client.ViewModels
{
    public class DeskTasksPageViewModel : BindableBase
    {
        private AuthToken _token;
        private DeskModel _desk;
        private UsersRequestService _usersRequestService;
        private TasksRequestService _tasksRequestService;
        private CommonViewService _viewService;
        private MainWindowViewModel _mainWindowVM;

        public DeskTasksPageViewModel(AuthToken token, DeskModel desk, MainWindowViewModel mainWindowVM)
        {
            _token = token;
            _desk = desk;

            _viewService = new CommonViewService();
            _usersRequestService = new UsersRequestService();
            _tasksRequestService = new TasksRequestService();
            _mainWindowVM = mainWindowVM;

            TasksByColumns = GetTasksByColumns(_desk.Id);
        }

        #region PROPERTIES
        
        private Dictionary<string, List<TaskClient>> _tasksByColumns = new Dictionary<string, List<TaskClient>>();

        public Dictionary<string, List<TaskClient>> TasksByColumns
        {
            get => _tasksByColumns;
            set 
            { 
                _tasksByColumns = value; 
                RaisePropertyChanged(nameof(TasksByColumns));
            }
        }
        
        #endregion

        #region METHODS

        private Dictionary<string, List<TaskClient>> GetTasksByColumns(int deskId)
        {
            var tasksByColumns = new Dictionary<string, List<TaskClient>>();
            var allTasks = _tasksRequestService.GetTaskByDesk(_token, deskId);
            foreach (string column in _desk.Columns)
            {
                tasksByColumns.Add(column, allTasks
                    .Where(t => t.Column == column)
                    .Select(t => new TaskClient(t)).ToList());
            }
            return tasksByColumns;
        }

        #endregion

    }
}
