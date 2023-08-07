using Newtonsoft.Json.Linq;
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
    class UserDesksPageViewModel
    {
        private AuthToken _token;
        private DesksRequestService _desksRequestService;
        private UsersRequestService _usersRequestService;

        public UserDesksPageViewModel(AuthToken token) 
        {
            _token = token;
            _desksRequestService = new DesksRequestService();
            _usersRequestService = new UsersRequestService();
        }

        public List<ModelClient<DeskModel>> AllDesks
        {
            get => _desksRequestService.GetAllDesks(_token).Select(desk => new ModelClient<DeskModel>(desk)).ToList();  
        }
    }
}
