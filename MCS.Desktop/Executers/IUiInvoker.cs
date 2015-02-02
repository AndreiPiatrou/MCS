using System;

namespace MCS.Desktop.Executers
{
    public interface IUiInvoker
    {
        void SendToUi(Action action);

        void Cancel();

        bool IsCancelled { get; }
    }
}