using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMWindowsUI.EventModels;

namespace RMWindowsUI.ViewModels
{
    
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>
    {
        
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private SimpleContainer _container;

        // dont need to save loginVM "state"
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, SimpleContainer container)
        {
            // constructor injection to pass in an instance loginVM and store it in _loginVM
            
            _events = events;
            _salesVM = salesVM;
            _container = container;

            _events.SubscribeOnPublishedThread(this);

            // get new loginvm and replaces it each time, clear sensitive information
            ActivateItemAsync(_container.GetInstance<LoginViewModel>());
        }

        public async Task HandleAsync(LogOnEventModel message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesVM);
            
            
        }
    }
}
