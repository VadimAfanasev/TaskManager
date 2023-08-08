using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services
{
    internal class DesksViewService
    {
        AuthToken _token;
        DesksRequestService _desksRequestService;
        public DesksViewService(AuthToken authToken, DesksRequestService desksRequestService) 
        {
            _token = authToken;
            _desksRequestService = desksRequestService;
        }

        public ModelClient<DeskModel> GetDeskClientById(object deskId)
        {
            try
            {
                int id = (int)deskId;
                DeskModel desk = _desksRequestService.GetDeskById(_token, id);
                return new ModelClient<DeskModel>(desk);
            }
            catch (FormatException) 
            {
                return new ModelClient<DeskModel>(null);
            }
        }

        public List<ModelClient<DeskModel>> GetDesks(int projectId)
        {
            var result = new List<ModelClient<DeskModel>>();
            var desks = _desksRequestService.GetDeskByProject(_token, projectId);
            if (desks != null)
            {
                result = desks.Select(d => new ModelClient<DeskModel>(d)).ToList();
            }
            return result;

        }

    }
}
