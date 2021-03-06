using System;
using System.Windows.Input;

namespace DddInPractice.WpfClient.Common;

public class Command<T> : ICommand
{
    private readonly Func<T?, bool> _canExecute;
    private readonly Action<T?> _execute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }


    public Command(Action<T?> execute)
        : this(_ => true, execute)
    {
    }


    public Command(Func<T?, bool> canExecute, Action<T?> execute)
    {
        this._execute = execute;
        this._canExecute = canExecute;
    }


    public bool CanExecute(object? parameter)
    {
        if (parameter == null && typeof(T).IsValueType)
            return false;

        return _canExecute((T?)parameter);
    }


    public void Execute(object? parameter)
    {
        _execute((T?)parameter);
    }
}


public class Command : Command<object>
{
    public Command(Func<bool> canExecute, Action execute)
        : base(_ => canExecute(), _ => execute())
    {
    }


    public Command(Action execute)
        : this(() => true, execute)
    {
    }
}
