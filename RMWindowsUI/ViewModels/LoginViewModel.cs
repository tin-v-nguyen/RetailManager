using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void LogIn()
        {
            Console.WriteLine();
        }



    }
}
