using AutoMapper;
using Caliburn.Micro;
using RMWindowsUI.Helpers;
using RMWindowsUI.Library.Api;
using RMWindowsUI.Library.Helpers;
using RMWindowsUI.Library.Models;
using RMWindowsUI.Models;
using RMWindowsUI.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace RMWindowsUI
{
    // setup caliburn.micro
    public class Bootstrapper : BootstrapperBase
    {
        // dependency injection container handles instantiation of all classes
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();

            // this Helper function is from stack overflow https://stackoverflow.com/questions/30631522/caliburn-micro-support-for-passwordbox
            // implements a binding convention so that PasswordBox binding in Caliburn.Micro works
            // Caliburn.Micro doesn't support PasswordBox so it is never stored in clear text
            // we are bypassing that, it does reduce security slightly, password is stored in RAM temporarily, potential for reading password,
            // If someone has access to your ram though, they will have access to your keystrokes, it reduces security in an insignificant way
            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");
        }

        private IMapper ConfigureAutoMapper()
        {
            // does reflection once to know how to map models
            var AMConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
            });

            var output = AMConfig.CreateMapper();
            return output;
        }

        protected override void Configure()
        {
            // create singleton for mapper
            _container.Instance<IMapper>(ConfigureAutoMapper());

            // create singleton for _container
            _container.Instance(_container)
                .PerRequest<IProductEndpoint, ProductEndpoint>()
                .PerRequest<ISaleEndpoint, SaleEndpoint>();
            

            _container
                // be careful about using singletons unless you know you should, aren't great on memory usage
                // creates one instance for all requests, .PerRequest creates one instance for each request
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IConfigHelper, ConfigHelper>()
                .Singleton<IAPIHelper, APIHelper>();
            

            // handle view models that connect to views
            // reflection, relfection is slow, usually dont use it
            // Configure only runs once at the startup, so no need to worry about tiny performance hit
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    // similar to Singleton format, first arg could be interface of viewModel and then viewModelType,
                    // might need to change later to do unit testing
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // on start up launch ShellViewModel as base view
            DisplayRootViewForAsync<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
