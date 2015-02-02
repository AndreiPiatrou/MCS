using System;
using System.Threading.Tasks;

namespace MCS.Desktop.Executers
{
    public interface IExecuter
    {
        Task<TResult> ExecuteAsync<TResult>(Func<IExecuter, TResult> action);

        void QueueAction(Action<IExecuter> action);

        void QueueAction(Action<IExecuter> action, Action completedAction);

        void QueueAction<TResult>(Func<IExecuter, TResult> action);

        void QueueAction<TResult>(Func<IExecuter, TResult> action, Action<TResult> completedAction);

        void SendToUi(Action action);

        void Cancel();

        event EventHandler<IsRunningChangedEventArgs> IsRunningChanged;

        bool IsCancelled { get; }

        bool IsRunning { get; }
    }
}