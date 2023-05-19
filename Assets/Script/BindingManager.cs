using UnityEngine.UI;
using System;

namespace RocketMvvmBase
{
    public static class BindingManager
    {
        public static void BindingData<T, S>(T targetData, Oberservable<S> sourceData, IConverter<T, S> converter)
        {
            sourceData.register(d => targetData = converter.From(d));
        }

        public static void BindingData<S>(Action<S> targetDataSetter, Oberservable<S> sourceData)
        {
            sourceData.register(targetDataSetter);
        }

#nullable enable
        public static void BindingCommand<T>(Button button, RelayCommand execute, object? parameter = null)
        {
            button.onClick.AddListener(() => execute.Execute(parameter));
        }
#nullable disable
    }
}