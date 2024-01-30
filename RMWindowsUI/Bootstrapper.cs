using Caliburn.Micro;
using RMWindowsUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMWindowsUI
{
    // setup caliburn.micro
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // on start up launch ShellViewModel as base view
            DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
