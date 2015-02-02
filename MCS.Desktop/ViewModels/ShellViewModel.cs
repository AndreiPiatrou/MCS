using Caliburn.Micro;

using MCS.Desktop.Executers;

namespace MCS.Desktop.ViewModels
{
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, IExecuter executer)
        {
            this.windowManager = windowManager;
            this.eventAggregator = eventAggregator;
            this.executer = executer;

            this.executer.IsRunningChanged += (s, e) => InProcess = e.IsRunning;
        }

        public void Test()
        {
            executer.QueueAction(invoker =>
                                 {
                                     System.Threading.Thread.Sleep(5000);
                                     return 1;
                                 }, i => windowManager.ShowWindow(this, i));
        }

        public bool InProcess
        {
            get
            {
                return inProcess;
            }

            set
            {
                if (value.Equals(inProcess))
                {
                    return;
                }

                inProcess = value;
                NotifyOfPropertyChange(() => InProcess);
            }
        }

        private readonly IWindowManager windowManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IExecuter executer;
        private bool inProcess;
    }
}