﻿using Caliburn.Micro;
using RMWindowsUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RMWindowsUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        // private backing fields, naming convention for storing value of properties
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;

        // on startup this requests and IAPIHelper, filled by Singleton IAPIHelper, save in private var
        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value; 
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get 
            {
                bool output = false;

                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
       }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        // Caliburn.Micro convention, CanLogIn keeps LogIn box grayed out until true
        public bool CanLogIn
        {
            get 
            {
                bool output = false;

                // ? checks for null
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
            
        }

        // Caliburn.Micro convention, LogIn is connected to the LogIn button in LoginView.xaml
        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(UserName, Password);
            } catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            

        }



    }
}
