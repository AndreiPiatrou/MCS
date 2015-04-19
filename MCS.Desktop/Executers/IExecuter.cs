using System;
using System.Reactive.Subjects;
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
        
        ISubject<bool> IsRunningChangedSubject { get; } 

        bool IsCancelled { get; }

        bool IsRunning { get; }
    }
}