using Caliburn.Micro;

namespace MCS.Desktop.ViewModels
{
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public ShellViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        private readonly IWindowManager windowManager;
    }
}