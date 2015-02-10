using System;

namespace MCS.Desktop.DataModels
{
    public class DataChangedEventArgs<T> : EventArgs
    {
        public DataChangedEventArgs(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }
}