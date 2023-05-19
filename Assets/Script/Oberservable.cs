using System;
using UnityEngine;

namespace RocketMvvmBase
{
    public class Oberservable<T>
    {
        private T data;
        private Action<T> OnUpdateData;

        public T Data
        {
            get => data;
            set
            {
                data = value;
                OnUpdateData?.Invoke(value);
            }
        }

        public void register(Action<T> source)
        {
            OnUpdateData += source;
        }
    }
}