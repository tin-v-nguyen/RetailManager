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

        private UserDisplayModel _selectedUser;

        public UserDisplayModel SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                SelectedUserName = value.Email;
                SelectedUserRoles = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());
                // TODO: Figure out how to load available roles without this async in a sync function
                LoadAvailableRoles();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        private string _selectedUserName;

        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set 
            { 
                _selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        private BindingList<string> _selectedUserRoles = new BindingList<string>();

        public BindingList<string> SelectedUserRoles
        {
            get { return _selectedUserRoles; }
            set 
            { 
                _selectedUserRoles = value;
                NotifyOfPropertyChange(() => SelectedUserRoles);
            }
        }

        private string _selectedRoleToUnassign;

        public string SelectedRoleToUnassign
        {
            get { return _selectedRoleToUnassign; }
            set 
            { 
                _selectedRoleToUnassign = value;
                NotifyOfPropertyChange(() => SelectedRoleToUnassign);
            }
        }

        private string _selectedRoleToAssign;

        public string SelectedRoleToAssign
        {
            get { return _selectedRoleToAssign; }
            set
            {
                _selectedRoleToAssign = value;
                NotifyOfPropertyChange(() => SelectedRoleToAssign);
            }
        }


        private BindingList<string> _availableRoles = new BindingList<string>();

        public BindingList<string> AvailableRoles
        {
            get { return _availableRoles; }
            set 
            { 
                _availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
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

        private async Task LoadAvailableRoles()
        {
            var roles = await _userEndpoint.GetAllRoles();
            foreach (var role in roles)
            {
                if (SelectedUserRoles.IndexOf(role.Value) < 0) 
                {
                    AvailableRoles.Add(role.Value);
                }
            }
        }

        public async void AssignSelectedRole()
        {
            await _userEndpoint.AddUserToRole(SelectedUser.Id, SelectedRoleToAssign);            
            // need to either edit UserDisplayModel and reload, or Reload the User from the DB after changes are made
            SelectedUserRoles.Add(SelectedRoleToAssign);
            AvailableRoles.Remove(SelectedRoleToAssign);
        }

        // TODO BUG When a cashier/admin logs into the sales page, and then remove cashier role, going back to the sales page you can still see the products
        // When a role is changed, force refresh the UI?
        public async void UnassignSelectedRole()
        {
            
            await _userEndpoint.RemoveUserFromRole(SelectedUser.Id, SelectedRoleToUnassign);            
            AvailableRoles.Add(SelectedRoleToUnassign);
            SelectedUserRoles.Remove(SelectedRoleToUnassign);
            
        }
    }
}
