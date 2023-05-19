using System;

namespace RocketMvvmBase
{
    public class RelayCommand
    {
        public readonly Action<object> ExecuteAction;

        public RelayCommand(Action<object> execute)
        {
            ExecuteAction = execute;
        }

#nullable enable
        public void Execute(object? parameter) => ExecuteAction(parameter);
#nullable disable
    }
}