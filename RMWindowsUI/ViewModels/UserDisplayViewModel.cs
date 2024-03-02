using AutoMapper;
using Caliburn.Micro;
using RMWindowsUI.Library.Api;
using RMWindowsUI.Library.Models;
using RMWindowsUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMWindowsUI.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _window;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IMapper _mapper;

        public UserDisplayViewModel(StatusInfoViewModel status, IWindowManager window, IUserEndpoint userEndpoint, IMapper mapper)
        {
            this._status = status;
            this._window = window;
            this._userEndpoint = userEndpoint;
            this._mapper = mapper;
        }

        private BindingList<UserDisplayModel> _users;
        public BindingList<UserDisplayModel> Users 
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            } 
        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartUpLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You do not have permission to the User Management Form.");
                    await _window.ShowDialogAsync(_status, null, settings);
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    await _window.ShowDialogAsync(_status, null, settings);
                }
                await TryCloseAsync();
            }

        }

        // need to add a loading indicator on frontend
        private async Task LoadUsers()
        {
            var userList = await _userEndpoint.GetAll();
            var users = _mapper.Map<List<UserDisplayModel>>(userList);         
            Users = new BindingList<UserDisplayModel>(users);
        }
    }
}
