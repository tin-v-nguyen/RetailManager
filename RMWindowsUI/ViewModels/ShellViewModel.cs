using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace RMWindowsUI.ViewModels
{
    
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;
        public ShellViewModel(LoginViewModel loginVM)
        {
            // constructor injection to pass in an instance loginVM and store it in _loginVM
            _loginVM = loginVM;
            ActivateItemAsync(_loginVM);
        }
    }
}
