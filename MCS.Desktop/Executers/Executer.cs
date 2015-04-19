using System;
using System.Diagnostics.Contracts;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MCS.Desktop.Executers
{
    public class Executer : IExecuter
    {
        public Executer()
        {
            Contract.Ensures(dispatcher != null);

            dispatcher = Application.Current.Dispatcher;
            IsRunningChangedSubject = new Subject<bool>();
        }

        public Task<TResult> ExecuteAsync<TResult>(Func<IExecuter, TResult> action)
        {
            Contract.Requires(action != null);

            return Task.Run(() => action(this));
        }

        public void QueueAction(Action<IExecuter> action)
        {
            QueueAction(action, () => { });
        }

        public void QueueAction(Action<IExecuter> action, Action completedAction)
        {
            Contract.Requires(action != null);
            Contract.Requires(completedAction != null);

            IsRunning = true;
            ThreadPool.QueueUserWorkItem(state =>
                                         {
                                             action(this);
                                             SendToUi(completedAction);
                                             IsRunning = false;
                                         });
        }

        public void QueueAction<TResult>(Func<IExecuter, TResult> action)
        {
            QueueAction(action, result => { });
        }

        public void QueueAction<TResult>(Func<IExecuter, TResult> action, Action<TResult> completedAction)
        {
            Contract.Requires(action != null);
            Contract.Requires(completedAction != null);

            IsRunning = true;
            ThreadPool.QueueUserWorkItem(state =>
                                         {
                                             var result = action(this);
                                             SendToUi(() => completedAction(result));
                                             IsRunning = false;
                                         });
        }

        public void SendToUi(Action action)
        {
            Contract.Requires(action != null);
            Contract.Requires(dispatcher != null);

            dispatcher.BeginInvoke(action);
        }

        public void Cancel()
        {
            isCancelled = true;
            IsRunning = false;
        }

        public ISubject<bool> IsRunningChangedSubject { get; private set; }
        
        public bool IsCancelled
        {
            get
            {
                return isCancelled;
            }
        }

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }

            private set
            {
                isRunning = value;
                IsRunningChangedSubject.OnNext(isRunning);
            }
        }

        private readonly Dispatcher dispatcher;
        private bool isCancelled;
        private bool isRunning;
    }
}
