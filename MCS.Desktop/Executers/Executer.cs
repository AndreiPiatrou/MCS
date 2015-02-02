using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MCS.Desktop.Executers
{
    public class Executer : IUiInvoker
    {
        public Executer()
        {
            Contract.Ensures(dispatcher != null);

            dispatcher = Application.Current.Dispatcher;
        }

        public static Task<TResult> ExecuteAsync<TResult>(Func<IUiInvoker, TResult> action)
        {
            Contract.Requires(action != null);

            var invoker = DefaultInvoker;

            return Task.Run(() => action(invoker));
        }

        public static IUiInvoker QueueAction(Action<IUiInvoker> action)
        {
            return QueueAction(action, () => { });
        }

        public static IUiInvoker QueueAction(Action<IUiInvoker> action, Action completedAction)
        {
            var invoker = DefaultInvoker;

            ThreadPool.QueueUserWorkItem(state =>
                                         {
                                             action(invoker);
                                             invoker.SendToUi(completedAction);
                                         });

            return invoker;
        }

        public static IUiInvoker QueueAction<TResult>(Func<IUiInvoker, TResult> action)
        {
            return QueueAction(action, result => { });
        }

        public static IUiInvoker QueueAction<TResult>(Func<IUiInvoker, TResult> action, Action<TResult> completedAction)
        {
            Contract.Requires(action != null);
            Contract.Requires(completedAction != null);

            var invoker = DefaultInvoker;
            ThreadPool.QueueUserWorkItem(state =>
                                         {
                                             var result = action(invoker);
                                             invoker.SendToUi(() => completedAction(result));
                                         });

            return invoker;
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
        }

        public bool IsCancelled
        {
            get
            {
                return isCancelled;
            }
        }

        private static IUiInvoker DefaultInvoker
        {
            get
            {
                return new Executer();
            }
        }

        private readonly Dispatcher dispatcher;
        private bool isCancelled;
    }
}
