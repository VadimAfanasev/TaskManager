using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Client.Models;
using TaskManager.Client.Views.AddWindows;
using TaskManager.Common.Models;

namespace TaskManager.Client.Services
{
    internal class DesksViewService
    {
        private AuthToken _token;
        private DesksRequestService _desksRequestService;
        private CommonViewService _viewService;

        public DesksViewService(AuthToken authToken, DesksRequestService desksRequestService) 
        {
            _token = authToken;
            _desksRequestService = desksRequestService;
            _viewService = new CommonViewService();
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

        public void OpenViewDeskInfo(object deskId, BindableBase context)
        {
            var wnd = new CreateOrUpdateDeskWindow();
            _viewService.OpenWindow(wnd, context);
        }

        public void UpdateDesk(DeskModel desk)
        {
            var resultAction = _desksRequestService.UpdateDesk(_token, desk);
            _viewService.ShowActionResult(resultAction, "New desk is updated");
        }

        public void DeleteDesk(int deskId)
        {
            var resultAction = _desksRequestService.DeleteDesk(_token, deskId);
            _viewService.ShowActionResult(resultAction, "New desk is deleted");
        }

        public void SelectPhotoForDesk(ModelClient<DeskModel> selectedDesk)
        {
            _viewService.SetPhotoForObject(selectedDesk.Model);
            selectedDesk = new ModelClient<DeskModel>(selectedDesk.Model);
        }

    }
}