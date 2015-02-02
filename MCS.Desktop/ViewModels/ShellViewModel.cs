using Caliburn.Micro;

using MCS.Desktop.Executers;

namespace MCS.Desktop.ViewModels
{
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            this.windowManager = windowManager;
            this.eventAggregator = eventAggregator;
        }

        public void Test()
        {
            Executer.QueueAction(invoker => 1, i => windowManager.ShowWindow(this, i));
        }

        private readonly IWindowManager windowManager;
        private readonly IEventAggregator eventAggregator;
    }
}