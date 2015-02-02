using System;

namespace MCS.Desktop.Executers
{
    public class IsRunningChangedEventArgs : EventArgs
    {
        public IsRunningChangedEventArgs(bool isRunning)
        {
            IsRunning = isRunning;
        }

        public bool IsRunning { get; private set; }
    }
}
